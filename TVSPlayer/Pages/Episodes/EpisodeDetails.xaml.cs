﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TVS.API;
using static TVS.API.Episode;

namespace TVSPlayer
{
    /// <summary>
    /// Interaction logic for EpisodeDetails.xaml
    /// </summary>
    public partial class EpisodeDetails : UserControl
    {
        public EpisodeDetails(Episode episode)
        {
            InitializeComponent();
            this.episode = episode;
        }

        Episode episode;

        private async void Grid_Loaded(object sender, RoutedEventArgs e) {
            EpisodeThumb.Source = await Database.GetEpisodeThumbnail(Int32.Parse(episode.seriesId.ToString()), episode.id);
            EPName.Text = EpisodeName.Text = episode.episodeName;
            Season.Text = episode.airedSeason.ToString();
            Episode.Text = episode.airedEpisodeNumber.ToString();
            Rating.Text = episode.siteRating.ToString();
            if (!String.IsNullOrEmpty(episode.firstAired)) {
                Airdate.Text = DateTime.ParseExact(episode.firstAired, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd. MM. yyyy");
            }
            Writers.Text = null;
            for (int i = 0; i < episode.writers.Count; i++) {
                if (i == episode.writers.Count - 1) {
                    Writers.Text += episode.writers[i];
                } else {
                    Writers.Text += episode.writers[i] + ", ";
                }
            }
            Directors.Text = null;
            for (int i = 0; i < episode.directors.Count; i++) {
                if (i == episode.directors.Count - 1) {
                    Directors.Text += episode.directors[i];
                } else {
                    Directors.Text += episode.directors[i] + ", ";
                }
            }
            if (episode.files.Count > 0) {
                foreach (var item in episode.files) {
                    if (item.Type == TVS.API.Episode.ScannedFile.FileType.Video) {
                        Downloaded.Text = "Yes";
                        break;
                    }
                }
            }
            Overview.Text = episode.overview;
        }

        private void EPName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Process.Start("http://www.imdb.com/title/"+episode.imdbId+"/?ref_=tt_cl_i1");
        }

        bool isScrolling = false;
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private async void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            await Task.Run(() => {
                Thread.Sleep(100);
                isScrolling = true;
            });
            scrollMousePoint = e.GetPosition(ScrollView);
            hOff = ScrollView.HorizontalOffset;
            ScrollView.CaptureMouse();
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e) {
            if (ScrollView.IsMouseCaptured) {
                ScrollView.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(ScrollView).X));
            }
        }

        private async void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ScrollView.ReleaseMouseCapture();
            await Task.Run(() => {
                Thread.Sleep(500);
                isScrolling = false;
            });
        }

        private void BackIcon_MouseUp(object sender, MouseButtonEventArgs e) {

        }

        private async void Play_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            List<Episode.ScannedFile> list = new List<Episode.ScannedFile>();
            foreach (var item in episode.files) {
                if (item.Type == ScannedFile.FileType.Video) {
                    list.Add(item);
                }
            }
            List<FileInfo> infoList = new List<FileInfo>();
            foreach (var item in list) {
                infoList.Add(new FileInfo(item.NewName));
            }
            FileInfo info = infoList.OrderByDescending(ex => ex.Length).FirstOrDefault();
            if (info != null) {
                ScannedFile sf = list.Where(x => x.NewName == info.FullName).FirstOrDefault();
                //Used to release as many resources as possible to give all rendering power to video playback
                MainWindow.SetPage(new BlankPage());
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                await Task.Run(() => {
                    Thread.Sleep(500);
                });
                MainWindow.AddPage(new LocalPlayer(Database.GetSeries((int)episode.seriesId), episode, sf));
            }
        }
    }
}