﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace TVSPlayer {

    public static class ThemeSwitcher {
        private static bool theme = false;

        public static void SwitchTheme() {
            Action a = () => Elapsed();
            Thread t = new Thread(a.Invoke);
            t.Start();
        }

        private static void Elapsed() {
            if (theme) {
                Application.Current.Resources["BackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                Application.Current.Resources["TextColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
                Application.Current.Resources["LighterBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#444444"));
                Application.Current.Resources["LightestBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
                Application.Current.Resources["TransparentBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC202020"));
                LightIcons();
                theme = false;
            } else {
                Application.Current.Resources["BackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDDDDD"));
                Application.Current.Resources["TextColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#202020"));
                Application.Current.Resources["LighterBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
                Application.Current.Resources["LightestBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBBBBB"));
                Application.Current.Resources["TransparentBG"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCBBBBBB"));

                DarkIcons();
                theme = true;
            }
        }

        public static void DarkIcons() {
            Application.Current.Resources["MenuIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-menu-dark.png", UriKind.Absolute));
            Application.Current.Resources["MoreIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-more-dark.png", UriKind.Absolute));
            Application.Current.Resources["SearchIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-search-dark.png", UriKind.Absolute));
            Application.Current.Resources["PlayIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-play-dark.png", UriKind.Absolute));
            Application.Current.Resources["ImportIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-import-dark.png" , UriKind.Absolute));
            Application.Current.Resources["NewFileIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-newfile-dark.png" , UriKind.Absolute));
            Application.Current.Resources["RemoveIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-remove-dark.png", UriKind.Absolute));
            Application.Current.Resources["QuestionIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-question-dark.png", UriKind.Absolute));
            Application.Current.Resources["CheckSquareIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-checksquare-dark.png", UriKind.Absolute));
            Application.Current.Resources["CheckIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-checked-dark.png", UriKind.Absolute));
            Application.Current.Resources["ForwardIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-forward-dark.png", UriKind.Absolute));
            Application.Current.Resources["ReloadIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-reload-dark.png", UriKind.Absolute));
            Application.Current.Resources["ForwardIconHQ"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-forward-dark-hq.png", UriKind.Absolute));
            Application.Current.Resources["FolderIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-folder-dark.png", UriKind.Absolute));
            Application.Current.Resources["BackIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-back-dark.png", UriKind.Absolute));
            Application.Current.Resources["PlusIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-add-dark.png", UriKind.Absolute));
            Application.Current.Resources["EditIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-edit-dark.png", UriKind.Absolute));
            Application.Current.Resources["NoPoster"] = new BitmapImage(new Uri("pack://application:,,,/Icons/poster-light.png", UriKind.Absolute));
            Application.Current.Resources["ThemeIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-theme-dark.png", UriKind.Absolute));
            Application.Current.Resources["PosterIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-poster-dark.png", UriKind.Absolute));
            Application.Current.Resources["ListIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-list-dark.png", UriKind.Absolute));
            Application.Current.Resources["CalendarIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-calendar-dark.png", UriKind.Absolute));
            Application.Current.Resources["AlphabeticalIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-alpha-dark.png", UriKind.Absolute));
            Application.Current.Resources["AlphabeticalReverseIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-alphareverse-dark.png", UriKind.Absolute));
            Application.Current.Resources["DownloadIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-download-dark.png", UriKind.Absolute));
            Application.Current.Resources["BroadcastIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-broadcast-dark.png", UriKind.Absolute));
            Application.Current.Resources["CogIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-cog-dark.png", UriKind.Absolute));
            Application.Current.Resources["StatisticsIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-statistics-dark.png", UriKind.Absolute));

        }

        private static void LightIcons() {
            Application.Current.Resources["MenuIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-menu-light.png", UriKind.Absolute));
            Application.Current.Resources["MoreIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-more-light.png", UriKind.Absolute));
            Application.Current.Resources["SearchIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-search-light.png", UriKind.Absolute));
            Application.Current.Resources["PlayIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-play-light.png", UriKind.Absolute));
            Application.Current.Resources["ImportIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-import-light.png" , UriKind.Absolute));
            Application.Current.Resources["NewFileIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-newfile-light.png" , UriKind.Absolute));
            Application.Current.Resources["RemoveIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-remove-light.png", UriKind.Absolute));
            Application.Current.Resources["QuestionIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-question-light.png", UriKind.Absolute));
            Application.Current.Resources["CheckSquareIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-checksquare-light.png", UriKind.Absolute));
            Application.Current.Resources["CheckIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-checked-light.png", UriKind.Absolute));
            Application.Current.Resources["ForwardIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-forward-light.png", UriKind.Absolute));
            Application.Current.Resources["ReloadIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-reload-light.png", UriKind.Absolute));
            Application.Current.Resources["ForwardIconHQ"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-forward-light-hq.png", UriKind.Absolute));
            Application.Current.Resources["FolderIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-folder-light.png", UriKind.Absolute));
            Application.Current.Resources["BackIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-back-light.png", UriKind.Absolute));
            Application.Current.Resources["NoPoster"] = new BitmapImage(new Uri("pack://application:,,,/Icons/poster-light.png", UriKind.Absolute));
            Application.Current.Resources["PlusIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-add-light.png", UriKind.Absolute));
            Application.Current.Resources["EditIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-edit-dark.png", UriKind.Absolute));
            Application.Current.Resources["ThemeIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-theme-light.png", UriKind.Absolute));
            Application.Current.Resources["PosterIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-poster-light.png", UriKind.Absolute));
            Application.Current.Resources["ListIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-list-light.png", UriKind.Absolute));
            Application.Current.Resources["CalendarIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-calendar-light.png", UriKind.Absolute));
            Application.Current.Resources["AlphabeticalIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-alpha-light.png", UriKind.Absolute));
            Application.Current.Resources["AlphabeticalReverseIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-alphareverse-light.png", UriKind.Absolute));
            Application.Current.Resources["DownloadIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-download-light.png", UriKind.Absolute));
            Application.Current.Resources["BroadcastIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-broadcast-light.png", UriKind.Absolute));
            Application.Current.Resources["CogIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-cog-light.png", UriKind.Absolute));
            Application.Current.Resources["StatisticsIcon"] = new BitmapImage(new Uri("pack://application:,,,/Icons/ico-statistics-light.png", UriKind.Absolute));

        }
    }
}