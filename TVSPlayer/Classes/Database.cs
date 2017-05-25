﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Packaging;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace TVSPlayer {
    class Database {
#region Shows

        /// <summary>
        /// Saves list of TVShows
        /// </summary>
        /// <param name="list">List of TVShows</param>
        public static void SaveTVShows(List<TVShow> list) {
            string file = Helper.PathToSettings + "TVShows.TVSData";
            string json = JsonConvert.SerializeObject(list);
            if (!Directory.Exists(Helper.PathToSettings + "Data\\")) {
                Directory.CreateDirectory(Helper.PathToSettings + "Data\\");
            }
            File.WriteAllText(file, json);
        }
        /// <summary>
        /// Adds TV show to Database
        /// </summary>
        /// <param name="show">TVShow that gets added</param>
        public static void AddTVShow(TVShow show) {
            List<TVShow> l = GetTVShow();
            l.Add(show);
            SaveTVShows(l);
        }

        /// <summary>
        /// Returns list of TVShows
        /// </summary>
        /// <returns></returns>
        public static List<TVShow> GetTVShow() {
            string file = Helper.PathToSettings + "\\TVShows.TVSData";
            string json = File.ReadAllText(file);
            List<TVShow> list = JsonConvert.DeserializeObject(json) as List<TVShow>;
            return list;
        }

        /// <summary>
        /// Returns TVShow with by ID
        /// </summary>
        /// <param name="id">TVShow id</param>
        /// <returns></returns>
        private static TVShow GetTVShowByID(int id) {
            List<TVShow> list = GetTVShow();
            foreach (TVShow s in list) {
                if (id == s.id) {
                    return s;
                }
            }
            return null;

        }
        #endregion

#region Episodes
        /// <summary>
        /// Saves episodes in package. If hasImage is false it tries to search for image
        /// </summary>
        /// <param name="show">TVShow at least "id" must not be null</param>
        /// <param name="list">List of Episodes</param>
        public static void SaveEpisodes(TVShow show, List<Episode> list) {
            if (!Directory.Exists(Helper.PathToSettings + "Data\\")) {
                Directory.CreateDirectory(Helper.PathToSettings + "Data\\");
            }
            string file = Helper.PathToSettings + "Data\\" + show.id + ".TVSPackage";
            List<Tuple<Stream, Episode>> ls = new List<Tuple<Stream, Episode>>();
            foreach (Episode e in list) {
                if (e.image != null && !e.image.hasImage) {
                    if (e.image.medium != null) {
                        Stream str = GetStreamFromUrl(e.image.medium);
                        ls.Add(new Tuple<Stream, Episode>(str, e));
                        e.image.hasImage = true;
                    } else {
                        Stream str = GetStreamFromUrl(e.image.original);
                        ls.Add(new Tuple<Stream, Episode>(str, e));
                        e.image.hasImage = true;
                    }
                }             

            }
            foreach (Tuple<Stream, Episode> t in ls) {
                AddToPackage(t.Item1, t.Item2.getNaming() + "\\image.jpg", file);
            }
            string json = JsonConvert.SerializeObject(list);
            AddToPackage(StringToStream(json), "Episodes.TVSData", file);
        }

        /// <summary>
        /// Reads and returns list of episodes from database
        /// </summary>
        /// <param name="show">TVShow from which you want to return episodes</param>
        /// <returns></returns>
        public static List<Episode> GetEpisodes(TVShow show) {
            List<Episode> list = new List<Episode>();
            string file = Helper.PathToSettings + "Data\\" + show.id + ".TVSPackage";
            MemoryStream s = ReadPackage(file, "Episodes.TVSData");
            StreamReader sr = new StreamReader(s);
            string json = sr.ReadToEnd();
            JArray jo = new JArray();
            try {
                jo = JArray.Parse(json);
            } catch {

            }
            foreach (JToken jt in jo) {
                list.Add(jt.ToObject<Episode>());
            }
            return list;
        }


        /// <summary>
        /// Reads and returns list of tupples which contain Episode info and bitmap image
        /// </summary>
        /// <param name="s">TVShow from which you want to return eps and Images</param>
        /// <returns>List of Tuples. Item1 is Episode Item2 is BitmapIamge</returns>
        public static List<Tuple<Episode, BitmapImage>> GetEpisodesWithImages(TVShow show) {
            List<Tuple<Episode, BitmapImage>> list = new List<Tuple<Episode, BitmapImage>>();
            List<Episode> listEP = GetEpisodes(show);
            foreach (Episode ep in listEP) {
                if (ep.image != null && ep.image.hasImage) {
                    string file = Helper.PathToSettings + "Data\\" + show.id + ".TVSPackage";
                    MemoryStream str = ReadPackage(file, ep.getNaming()+"\\image.jpg");
                    BitmapImage bmp = new BitmapImage();
                    bmp.StreamSource = str;
                    str.Close();
                    list.Add(new Tuple<Episode, BitmapImage>(ep, bmp));
                }
            }
            return list;
        }


#endregion
        /// <summary>
        /// Adds stream to package. Uncompressed.
        /// </summary>
        /// <param name="fileStream">Stream that will be added</param>
        /// <param name="PathInPackage">Path in package e.g.(Images\\image.jpg)</param>
        /// <param name="PathToPackage">Where to save package</param>
        private static void AddToPackage(Stream fileStream, string PathInPackage, string PathToPackage) {
            using (Package zip = Package.Open(PathToPackage, FileMode.OpenOrCreate)) {
                string destFilename = ".\\" + PathInPackage;
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri)) {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.NotCompressed);
                using (Stream dest = part.GetStream()) {
                    fileStream.CopyTo(dest);
                }
            }
        }


        /// <summary>
        /// Converts memory stream that contains some bitmap to BitmapImage
        /// </summary>
        /// <param name="s">Stream that will be converted to BitmapImage</param>
        /// <returns>BitmapImage created from stream</returns>
        private static BitmapImage GetBitmapFromStream(MemoryStream s) {
            try
            {
                using (MemoryStream memoryStream = s)
                {
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = memoryStream;
                    imageSource.EndInit();
                    return imageSource;
                }
            }
            catch (Exception e) {
                return null;
            }
        }


        /// <summary>
        /// Reads package and returns Stream of data
        /// </summary>
        /// <param name="PackageName">Path TO package</param>
        /// <param name="PathInPackage">Path INSIDE package</param>
        /// <returns></returns>
        private static MemoryStream ReadPackage(string PackageName, string PathInPackage) {
            using (Package zip = Package.Open(PackageName, FileMode.OpenOrCreate)) { 
                string destFilename = ".\\" + PathInPackage;
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                PackagePart part = zip.GetPart(uri);
                Stream s = part.GetStream();
                MemoryStream ms = new MemoryStream();
                s.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }                 
        }
        
        /// <summary>
        /// Input is string, output is Stream. Thats it
        /// </summary>
        /// <param name="s">Any string</param>
        /// <returns></returns>
        private static Stream StringToStream(string s) {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Returns stream from URL
        /// Used when you need to get image
        /// </summary>
        /// <param name="url">URL with image</param>
        /// <returns></returns>
        private static Stream GetStreamFromUrl(string url) {
            byte[] imageData = null;
            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData(url);
            return new MemoryStream(imageData);
        }
    }
}
