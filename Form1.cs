using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using Awesomium.Core;
using HtmlAgilityPack;
using MetroFramework.Forms;
using MP3_Auto_Tagger_GUI.Properties;
using TagLib;
using File = TagLib.File;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Timer = System.Windows.Forms.Timer;

namespace MP3_Auto_Tagger_GUI
{
    public partial class Form1 : MetroForm
    {
        private const int SwHide = 0;
        private const int SwShow = 5;
        public new ContextMenu Menu;
        public NotifyIcon NotificationIcon;
        private string _path = @"D:\Music";
        private bool _filter;
        private string[] _files;
        public static string path_programData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rohyl's MP3 File Manager");

        private int _tableWidth = 77;

        private int _processedArtwork;

        public Form1()
        {
            InitializeComponent();
            ChangeMonitorPath(_path);
            songsWithoutLyrics = new List<string>();
        }

        enum Status
        {
            Good,
            Normal,
            Bad
        }

        private void SetStatus(string status, Status statusType = Status.Normal, bool newLine = true)
        {
            Color clr = Color.Black;
            switch (statusType)
            {
                case Status.Bad:
                    clr = Color.Red;
                    break;
                case Status.Good:
                    clr = Color.Green;
                    break;
            }
            Console(status, clr, newLine);
            lbl_FileStatus.Text = status;
            lbl_FileStatus.ForeColor = clr;
        }

        private void SetSubstatus(string status, Status statusType = Status.Normal)
        {
            lblSubstatus.Visible = true;
            Color clr = Color.PapayaWhip;
            switch (statusType)
            {
                case Status.Bad:
                    clr = Color.Red;
                    break;
                case Status.Normal:
                    clr = Color.Orange;
                    break;
                case Status.Good:
                    clr = Color.Green;
                    break;
            }
            lblSubstatus.Text = status;
            lblSubstatus.ForeColor = clr;
        }

        private void HideSubstatus()
        {
            lblSubstatus.Visible = false;
        }

        public class Item
        {
            [XmlAttribute]
            public string Key;

            [XmlAttribute]
            public string Value;
        }

        private static List<MusicChart> _scrapedSongs = new List<MusicChart>();

        private static List<string> _chartSongs = new List<string>();
        private string path_chartSongs = Path.Combine(path_programData, "ChartSongs.xml");
        private void SaveChartDictionary()
        {
            var xElem = new XElement(
                "items",
                _chartSongs.Select(x => new XElement("item", x)));
            string xml = xElem.ToString();
            xElem.Save(path_chartSongs);
        }
        private void LoadChartDictionary()
        {
            var xElem2 = new XElement("items");
            if (System.IO.File.Exists(path_chartSongs))
                xElem2 = XElement.Load(path_chartSongs);
            else
                xElem2.Save(path_chartSongs);
            _chartSongs = xElem2.Descendants("item").Select(x => x.Value).ToList();
        }

        private static List<string> _lyricsException = new List<string>();
        private static string path_lyricsExceptions = Path.Combine(path_programData, "LyricExceptions.xml");
        private static void SaveLyricExceptions()
        {
            var xElem = new XElement(
                "items",
                _lyricsException.Distinct().ToList().Select(x => new XElement("item", x)));
            string xml = xElem.ToString();
            xElem.Save(path_lyricsExceptions);
        }
        private static void LoadLyricExceptions()
        {
            var xElem2 = new XElement("items");
            if (System.IO.File.Exists(path_lyricsExceptions))
                xElem2 = XElement.Load(path_lyricsExceptions);
            else
                xElem2.Save(path_lyricsExceptions);
            _lyricsException = xElem2.Descendants("item").Select(x => x.Value).ToList();
        }

        public void Do(
            Action action,
            TimeSpan retryInterval,
            int retryCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (var retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                        Thread.Sleep(retryInterval);
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }


        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private bool GoogleImageSearch(string query, bool showdialog = false)
        {
            string filePath = Path.Combine(_path, query + ".mp3");
            string entitized = HttpUtility.UrlEncode(query);
            string url = string.Format("https://www.google.com.au/search?q={0}&tbm=isch", entitized + " song cover artwork");
            using (var client = new WebClient()) // WebClient class inherits IDisposable
            {
                client.Headers.Add("user-agent",
                    "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                string htmlCode = client.DownloadString(url);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);
                for (var index = 0;
                    index < doc.DocumentNode.SelectSingleNode("//*[@id=\"images\"]").ChildNodes.Count;
                    index++)
                {

                    if (index < 0)
                        index = 0;
                    var eleImg = doc.DocumentNode.SelectSingleNode("//*[@id=\"images\"]").ChildNodes[index];
                    string thumbNailUrl = eleImg.FirstChild.Attributes["src"].Value;
                    string tempPath = Path.GetTempFileName();

                    client.DownloadFile(thumbNailUrl, tempPath);

                    var rightmost = Screen.AllScreens[0];
                    foreach (
                        var screen in
                            Screen.AllScreens.Where(screen => screen.WorkingArea.Right > rightmost.WorkingArea.Right))
                    {
                        rightmost = screen;
                    }


                    string landingPg = HtmlEntity.DeEntitize(eleImg.Attributes["href"].Value);
                    var innerDoc = new HtmlDocument();
                    client.Headers.Add("user-agent",
                        "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                    string inrHTml = client.DownloadString(landingPg);
                    innerDoc.LoadHtml(inrHTml);

                    foreach (
                        var element in
                            innerDoc.DocumentNode.Descendants()
                                .Where(
                                    element =>
                                        element.InnerText.Contains("full size") && element.Attributes.Contains("href")))
                    {
                        using (var imgClient = new WebClient()) // WebClient class inherits IDisposable
                        {
                            tempPath = Path.GetTempFileName();
                            try
                            {
                                Do(() => imgClient.DownloadFile(element.Attributes["href"].Value, tempPath),
                                    TimeSpan.FromSeconds(1));
                            }
                            catch (AggregateException)
                            {
                                return false;
                            }
                        }
                    }
                    var noArtwork = false;
                    var applyArtwork = false;
                    if (Settings.Default.LastSize.Height == 0 || Settings.Default.LastSize.Width == 0)
                    {
                        Settings.Default.LastSize = new Size(500, 500);
                    }

                    using (var file = File.Create(filePath))
                    {
                        IPicture artwork = new Picture(tempPath);
                        long currentSize = 0, newSize = 1;
                        if (file.Tag.Pictures.Any())
                        {
                            IPicture currentArtwork = file.Tag.Pictures[0];
                            using (var ms = new MemoryStream(currentArtwork.Data.Data))
                            {
                                currentSize = ms.Length;
                            }
                            using (var ms = new MemoryStream(artwork.Data.Data))
                            {
                                newSize = ms.Length;
                            }
                        }
                        if (currentSize == newSize)
                            return false;
                    }

                    if (showdialog)
                    {
                        var f = new Form
                        {
                            FormBorderStyle = FormBorderStyle.SizableToolWindow,
                            Size = Settings.Default.LastSize,
                            UseWaitCursor = false
                        };
                        f.Resize += (i, u) =>
                        {
                            var control = (Control)i;
                            control.Width = control.Height;
                        };
                        //f.Size = Image.FromFile(tempPath).Size;
                        f.BackgroundImage = ResizeImage(Image.FromFile(tempPath), f.Width, f.Height);
                        f.FormClosing += (l, m) =>
                        {
                            Settings.Default.LastSize = f.Size;
                            Settings.Default.LastLocation = f.Location;
                        };
                        f.KeyDown += (k, fs) =>
                        {
                            if (fs.KeyCode == Keys.Enter)
                            {
                                applyArtwork = true;
                            }
                            else if (fs.KeyCode == Keys.Escape)
                            {
                                noArtwork = true;
                            }
                            else if (fs.KeyCode == Keys.Left)
                            {
                                index -= 2;
                            }
                            f.Close();
                        };

                        f.Text = query;
                        f.Load += (o, e) =>
                        {
                            if (Settings.Default.LastLocation.X == 0)
                            {
                                f.Left = rightmost.WorkingArea.Right - f.Width;
                                f.Top = rightmost.WorkingArea.Bottom - f.Height;
                            }
                            else
                                f.Location = Settings.Default.LastLocation;
                            f.TopMost = true;
                            var t = new Timer { Interval = 10000 };
                            t.Tick += (k, z) =>
                            {
                                applyArtwork = true;
                                f.Close();
                            };
                            t.Enabled = true;
                        };
                        f.TopMost = true;
                        f.ShowDialog();
                    }
                    else
                        applyArtwork = true;
                    Settings.Default.Save();
                    if (noArtwork)
                        return false;
                    if (!applyArtwork) continue;
                    AddArtwork(filePath, tempPath);
                    return true;
                }
                return false;
            }
        }

        public bool AddArtwork(string filePath, string imgPath)
        {
            using (var file = File.Create(filePath))
            {
                IPicture artwork = new Picture(imgPath);
                long currentSize = 0, newSize = 1;
                if (file.Tag.Pictures.Any())
                {
                    IPicture currentArtwork = file.Tag.Pictures[0];
                    using (var ms = new MemoryStream(currentArtwork.Data.Data))
                    {
                        currentSize = ms.Length;
                    }
                    using (var ms = new MemoryStream(artwork.Data.Data))
                    {
                        newSize = ms.Length;
                    }
                }
                if (currentSize != newSize)
                {
                    Console("The file artwork for ", Color.Black);
                    Console(Path.GetFileNameWithoutExtension(filePath), Color.Blue, false);
                    Console(" has been changed.", Color.Black, false);
                    file.Tag.Pictures = new IPicture[1] { artwork };
                    fileChangedExceptions.Add(filePath);
                    file.Save();
                }
                return (currentSize == newSize);
            }
        }

        enum LyricSite
        {
            AzLyrics,
            MusixMatch
        }

        private Uri LyricSearchString(string songName, LyricSite site = LyricSite.MusixMatch)
        {
            siteSearched = site;
            return new Uri(string.Format("https://www.google.com.au/search?q={0}+site:" + (site == LyricSite.AzLyrics ? "azlyrics.com" : "www.musixmatch.com/lyrics/"), HttpUtility.UrlEncode(songName)));
        }

        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        /// <summary>
        /// Returns the number of steps required to transform the source string
        /// into the target string.
        /// </summary>
        int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        private void DeleteLyrics(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                using (var fiel = File.Create(filePath))
                {
                    fiel.Tag.Lyrics = "";
                    fiel.Save();
                }
        }

        List<string> songsWithoutLyrics;

        private string lyricsCertainSong = "";
        private LyricSite siteSearched = LyricSite.AzLyrics;

        private void OldFindSongLyrics(string filePath, LyricSite site = LyricSite.MusixMatch)
        {
            if (!System.IO.File.Exists(filePath))
            {
                Console("An attempt to search lyrics for a file " + filePath +
                        " has been made, but no such file exists!");
                return;
            }
            string songName = new TrontorMP3File(System.IO.Path.GetFileNameWithoutExtension(filePath)).MetaFixFilename(false);
            WebView lyricSearchView = WebCore.CreateWebView(1024, 768, WebViewType.Window);
            lyricSearchView.Source = LyricSearchString(songName);
            lyricSearchView.LoadingFrameComplete += (o, e) =>
            {
                if (!e.IsMainFrame) return;
                HtmlDocument doc = new HtmlDocument();
                string html = lyricSearchView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML");
                doc.LoadHtml(html);
                int searchResults = 0;
                if (doc.DocumentNode.InnerHtml.Contains("To continue, please"))
                {
                    MessageBox.Show("Captcha! Nooo!");
                    return;
                }
                //About 1,190,000,000 results (0.58 seconds) 
                if (doc.DocumentNode.InnerHtml.Contains("\"resultStats\""))
                {
                    //About 1,190,000,000 results
                    string baseText = Regex.Replace(doc.DocumentNode.SelectSingleNode("//*[@id=\"resultStats\"]").InnerText, @" ?\(.*?\)", string.Empty);
                    int removeIndex = baseText.IndexOf("results");
                    //About 1,900,000,000 
                    if (removeIndex > 0)
                        baseText = baseText.Substring(0, removeIndex);
                    //1900000000 
                    searchResults = int.Parse(Regex.Replace(baseText, "[^0-9.]", ""));
                }
                if (string.IsNullOrEmpty(searchResults.ToString()) || searchResults == 0)
                {
                    _lyricsException.Add(songsWithoutLyrics[index]);
                    SaveLyricExceptions();
                    return;
                }
                int maximumSearchIterations = (searchResults > 2 ? 2 : searchResults);
                for (int i = 1; i <= maximumSearchIterations; i++)
                {
                    HtmlNode aTag;
                    if (searchResults == 1)
                        aTag = doc.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/h3/a");
                    else
                        aTag = doc.DocumentNode.SelectSingleNode(string.Format("//*[@id=\"rso\"]/div/div[{0}]/div/h3/a", i));
                    string title =
                        HttpUtility.HtmlDecode(aTag.InnerText
                            .Replace(" - A-Z Lyrics", "")
                            .Replace("LYRICS", "")
                            .Replace("| Musixmatch", "")
                            .Replace("lyrics", ""));
                    string url = aTag.Attributes["href"].Value;
                    double similarity = CalculateSimilarity(songName.ToLower(), title.ToLower()) * 100;
                    if (similarity < 15)
                    {
                        return;
                    }
                    bool levehCheck = similarity > 70;
                    if (levehCheck || MessageBox.Show("Does this look like the right title?" +
                                                      Environment.NewLine +
                                                      Path.GetFileNameWithoutExtension(songsWithoutLyrics[index]) +
                                                      Environment.NewLine +
                                                      title, "Lyric Verification", MessageBoxButtons.YesNo) ==
                        DialogResult.Yes)
                    {
                        WebView lyricsPageBrowserLocal = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);

                        string lyricsUrl = string.Format(url);
                        lyricsPageBrowserLocal.Source = new Uri(lyricsUrl);
                        lyricsPageBrowserLocal.LoadingFrameComplete += (pol, kol) =>
                        {
                            if (kol.IsMainFrame)
                            {
                                if (lyricsCertainSong != "")
                                    Console(index + 1 + "/" + songsWithoutLyrics.Count, Color.Gray);
                                var lyricsdoc = new HtmlDocument();
                                lyricsdoc.LoadHtml(
                                    lyricsPageBrowserLocal.ExecuteJavascriptWithResult(
                                        "document.getElementsByTagName('html')[0].innerHTML"));
                                string rawLyrics = "";
                                switch (siteSearched)
                                {
                                    case LyricSite.AzLyrics:
                                        rawLyrics =
                                            lyricsdoc.DocumentNode.SelectSingleNode("/body/div[3]/div/div[2]/div[6]")
                                                .InnerText;
                                        break;
                                    case LyricSite.MusixMatch:
                                        var counts = (lyricsdoc.DocumentNode.Descendants()
                                            .Where(
                                                x =>
                                                    x.Attributes.Contains("class") &&
                                                    x.Attributes["class"].Value == "mxm-empty__title")
                                            .ToList());
                                        if (counts.Count != 0)
                                            return;
                                        rawLyrics =
                                            lyricsdoc.DocumentNode.Descendants()
                                                .Where(
                                                    x =>
                                                        x.Attributes.Contains("class") &&
                                                        x.Attributes["class"].Value == "mxm-lyrics__content")
                                                .ToList()[0].InnerText;
                                        break;
                                }
                                string cleanLyrics = (siteSearched == LyricSite.AzLyrics
                                    ? rawLyrics.Remove(rawLyrics.IndexOf("<!--"), rawLyrics.IndexOf("-->") + 3)
                                    : rawLyrics);
                                using (var fiel = File.Create(filePath))
                                {
                                    Console("Saving lyrics for " + songName + " | Loaded " +
                                            lyricsUrl.Replace("http://www.azlyrics.com/lyrics/", "")
                                                .Replace("https://www.musixmatch.com/lyrics/", ""), Color.Green);

                                    fiel.Tag.Lyrics = cleanLyrics;
                                    fiel.Save();
                                }
                                //if (index < maxIndex)
                                //{
                                //    index++;
                                //    string searchUrl = LyricSearchString(songsWithoutLyrics[index]);
                                //    lyricsBrowser.Source = new Uri(searchUrl);
                                //}
                                //else if (index == maxIndex)
                                //    ScanLocalLyrics();
                            }
                        };
                        break;
                    }
                    Debug.WriteLine("Mainframe breached");
                    if (i + 1 == maximumSearchIterations)
                    {
                        _lyricsException.Add(songsWithoutLyrics[index]);
                        //if (index < maxIndex)
                        //{
                        //    index++;

                        //    string searchUrl = LyricSearchString(songsWithoutLyrics[index]);
                        //    lyricsBrowser.Source = new Uri(searchUrl); 
                        //}
                        SaveLyricExceptions();
                    }
                }
            };
        }

        private void LyricSearchView_LoadingFrame(object sender, LoadingFrameEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FindSongLyrics(string certainSong = "", LyricSite site = LyricSite.MusixMatch)
        {
            index = 0;
            lyricsCertainSong = certainSong;
            LoadLyricExceptions();
            //WebView searchView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen); 
            if (certainSong != "")
            {
                songsWithoutLyrics = new List<string>() { certainSong };
            }
            else
                songsWithoutLyrics = _files.Where(x => !_lyricsException.Contains(x)).Where(x =>
                {
                    using (var fiel = File.Create(x))
                    {
                        return fiel.Tag.Lyrics == null || fiel.Tag.Lyrics.Length < 20;
                    }
                }).ToList();
            if (!songsWithoutLyrics.Any())
            {
                ScanLocalLyrics();
                return;
            }
            string metafixed = new TrontorMP3File(certainSong == "" ? songsWithoutLyrics[0] : certainSong).MetaFixFilename(false);
            lyricsBrowser.Source = LyricSearchString(metafixed, site);
        }

        public struct SongInformation<W, X, Y, Z>
        {
            public W First;
            public X Second;
            public Y Third;
            public Z Fourth;

            public SongInformation(W w, X x, Y y, Z z)
            {
                First = w;
                Second = x;
                Third = y;
                Fourth = z;
            }
        }

        private List<SongInformation<string, string, string, DateTime>> LyricInformation()
        {
            LoadLyricExceptions();
            List<SongInformation<string, string, string, DateTime>> info =
                new List<SongInformation<string, string, string, DateTime>>();
            try
            {
                //.Where(x => !_lyricsException.Contains(x))
                /*  var songsWithoutLyrics = */
                _files.Where(x =>
{
    using (var fiel = File.Create(x))
    {
        if (fiel.Tag.Lyrics == null)
        {
            info.Add(new SongInformation<string, string, string, DateTime>(x, "No", "",
               System.IO.File.GetLastAccessTime(x)));
        }
        else
            info.Add(new SongInformation<string, string, string, DateTime>(x, "Yes", fiel.Tag.Lyrics,
               System.IO.File.GetLastAccessTime(x)));
        return fiel.Tag.Lyrics == null || fiel.Tag.Lyrics.Length < 20;
    }
}).ToList();
                foreach (string s in _lyricsException.Where(x => info.All(z => z.First != x)))
                {
                    if (System.IO.File.Exists(s))
                        info.Add(new SongInformation<string, string, string, DateTime>(s, "No", "",
                            System.IO.File.GetLastAccessTime(s)));
                }
            }
            catch
            {

            }
            return info;
        }

        private void ScanLocalLyrics()
        {
            int scrolloffset = lyricsView.VerticalScrollingOffset;

            lyricsView.Rows.Clear();
            var information = LyricInformation();
            for (int i = 0; i < information.Count; i++)
            {
                lyricsView.Rows.Add(Path.GetFileNameWithoutExtension(information[i].First), information[i].Second,
                    information[i].Third, information[i].Fourth);
                if (information[i].Second == "Yes")
                    lyricsView.Rows[i].Cells[1].Style.BackColor = Color.Green;
                else
                    lyricsView.Rows[i].Cells[1].Style.BackColor = Color.Firebrick;

            }
            foreach (DataGridViewColumn col in lyricsView.Columns)
            {
                if (col.HeaderCell.Style.Alignment != DataGridViewContentAlignment.MiddleCenter)
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            lyricsView.Sort(lyricsView.Columns["clm_LastMod"], System.ComponentModel.ListSortDirection.Descending);
            PropertyInfo verticalOffset = lyricsView.GetType().GetProperty("VerticalOffset", BindingFlags.NonPublic | BindingFlags.Instance);
            verticalOffset.SetValue(this.lyricsView, scrolloffset, null);
        }

        private bool _finishedScanningAria, _finishedScanningBillboard, _finishedScanningShazam;

        private void PopulateChartLayout()
        {
            ariaFlow.Controls.Clear();
            foreach (var chart in _scrapedSongs)
            {
                if (chk_hideCheckedCharts.Checked && chart.Checked) continue;
                ariaFlow.Controls.Add(chart);

            }
        }
        private void ScanCharts()
        {
            _finishedScanningAria = false;
            _finishedScanningBillboard = false;
            _finishedScanningShazam = false;
            pnl_EmptyCharts.Visible = false;
            pnlProcess.Visible = true;
            _scrapedSongs.Clear();
            ScanAriaCharts();
            ScanBillboardHot100();
            ScanShazamTop100();
            new Thread(() =>
            {
                while (!_finishedScanningAria || !_finishedScanningBillboard || !_finishedScanningShazam)
                {
                }
                Invoke(new Action(() =>
                {
                    pnl_searchingCharts.Visible = false;
                    pnl_EmptyCharts.Visible = ariaFlow.Controls.Count == 0;
                    PopulateChartLayout();
                }));
                Space();
            })
            { IsBackground = true }.Start();
        }

        private void ScanShazamTop100()
        {
            Console("Started Scanning Shazam Charts", Color.CadetBlue);
            string siteUrl = "http://www.shazam.com/charts/top-100/australia";
            WebView shazamView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            shazamView.Source = new Uri(siteUrl);
            shazamView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    int foundSounds = 0;
                    string html = shazamView.HTML;
                    LoadChartDictionary();
                    var shazamDoc = new HtmlDocument();
                    shazamDoc.LoadHtml(html);
                    new Thread(() =>
                    {
                        string tempPath = Path.GetTempFileName();
                        var node =
                            shazamDoc.DocumentNode.Descendants("article")
                                .Where(
                                    d =>
                                        d.Attributes.Contains("class") &&
                                        d.Attributes["class"].Value.Contains("ti__container"));

                        var findclasses =
                            shazamDoc.DocumentNode.Descendants("div")
                                .Where(
                                    d =>
                                        d.Attributes.Contains("class") &&
                                        d.Attributes["class"].Value == "flexgrid__item--large");
                        var nodes = shazamDoc.DocumentNode.SelectNodes("flexgrid__item--large");
                        foreach (var topkeknode in findclasses)
                        {
                            string artist = "", title = "", both = "", imgUrl = "";
                            foreach (var nodule in topkeknode.ChildNodes[0].ChildNodes)
                            {
                                if (nodule.Attributes.Contains("class"))
                                {
                                    switch (nodule.Attributes["class"].Value)
                                    {
                                        case "ti__cover-art":
                                            imgUrl = nodule.ChildNodes["a"].ChildNodes["img"].Attributes["src"].Value;
                                            break;
                                        case "ti__details":
                                            title =
                                                HttpUtility.HtmlDecode(
                                                    nodule.ChildNodes.Where(x => x.Name == "p").ToList()[0].ChildNodes[
                                                        "a"].InnerText.Trim());
                                            if (
                                                nodule.ChildNodes.Where(x => x.Name == "p").ToList()[1].ChildNodes.Where
                                                    (x => x.Name == "a").Any())
                                                artist =
                                                    HttpUtility.HtmlDecode(
                                                        nodule.ChildNodes.Where(x => x.Name == "p").ToList()[1]
                                                            .ChildNodes["a"].InnerText.Trim());
                                            else
                                                artist =
                                                    HttpUtility.HtmlDecode(
                                                        nodule.ChildNodes.Where(x => x.Name == "p").ToList()[1]
                                                            .InnerText.Trim());
                                            break;
                                    }
                                }
                            }
                            both = GetFixedFileName(artist + " - " + title);
                            foundSounds++;
                            Debug.WriteLine(both);
                            var enumuerable = new List<MusicChart>(_scrapedSongs); bool isChecked = _chartSongs.Count(str => new DistanceCheck(both, 70).Check(str)) > 0;
                            if (enumuerable.Any(chart => chart.HasSimilar(both)))
                                continue;
                            Image img = Properties.Resources.question_sign_on_person_head;
                            using (var imgClient = new WebClient())
                            {
                                try
                                {
                                    tempPath = Path.GetTempFileName();
                                    Debug.WriteLine(imgUrl);
                                    imgClient.DownloadFile(imgUrl, tempPath);
                                }
                                catch (AggregateException)
                                {
                                }
                            }
                            img = Image.FromFile(tempPath);

                            MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked);
                            Chart.Size = new Size(ariaFlow.Size.Width - 25, Chart.Height);
                            Chart.btn_ClearSong.Click += (i, y) =>
                            {
                                ariaFlow.Controls.Remove(ariaFlow.Controls[ariaFlow.Controls.IndexOf(Chart)]);
                                if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                                SaveChartDictionary();
                            };
                            Chart.btn_Reorder.Click +=
                                (i, y) => { ariaFlow.Controls.SetChildIndex(Chart, ariaFlow.Controls.Count - 1); };
                            Invoke(new Action(() => _scrapedSongs.Add(Chart)));
                        }
                        _finishedScanningShazam = true;
                        if (foundSounds > 0)
                            Console("Finished Scanning Shazam Charts, found:" + foundSounds, Color.Blue);
                    })
                    { IsBackground = true }.Start();
                }
            };
        }

        private void ScanAriaCharts()
        {
            string siteUrl = "http://www.ariacharts.com.au/Charts/Singles-Chart";
            WebView ariaView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            ariaView.Source = new Uri(siteUrl);
            Console("Started Scanning Aria Charts", Color.CadetBlue);
            ariaView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    LoadChartDictionary();
                    var
                        doc = new HtmlDocument();

                    doc.LoadHtml(
                        ariaView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        int foundSounds = 0;
                        string siteXPath = "//*[@id=\"dvChartItems\"]";
                        if (doc.DocumentNode.SelectSingleNode(siteXPath) == null)
                        {
                            Invoke(new Action(() => Console("Scanning Aria charts failed.", Color.Red)));
                            MessageBox.Show("An error occured while scanning the aria charts :(");
                            _finishedScanningAria = true;
                            return;
                        }
                        var node = (HtmlNode)doc.DocumentNode.SelectSingleNode(siteXPath).ChildNodes.Where(x => x.Name == "table").ToList()[0].ChildNodes.ToList()[1];
                        if (node != null)
                            foreach (var itemRow in node.ChildNodes)
                            {
                                foreach (var nodes in itemRow.ChildNodes)
                                {
                                    if (nodes.Attributes.Contains("class") &&
                                        nodes.Attributes["class"].Value.Contains("title-artist"))
                                    {
                                        string tempPath = Path.GetTempFileName();
                                        if (nodes.ChildNodes.Count < 2)
                                        {
                                            Invoke(new Action(() => Console("Scanning Aria charts failed.", Color.Red)));
                                            MessageBox.Show("An error occured while scanning the aria charts :(");
                                            _finishedScanningAria = true;
                                            return;
                                        }
                                        string artist = HttpUtility.HtmlDecode(nodes.ChildNodes[1].InnerText);
                                        string title = HttpUtility.HtmlDecode(nodes.ChildNodes[0].InnerText);
                                        foundSounds++;
                                        string both = GetFixedFileName(artist + " - " + title);
                                        Debug.WriteLine("Aria:" + both);
                                        var enumuerable = new List<MusicChart>(_scrapedSongs);
                                        bool isChecked = _chartSongs.Count(str => new DistanceCheck(both, 70).Check(str)) > 0;
                                        if (!isChecked)
                                            MessageBox.Show(both + "aria");
                                        if (enumuerable.Any(chart => chart.HasSimilar(both))) continue;
                                        foreach (var cNode in itemRow.ChildNodes.Where(x => x.FirstChild != null && x.FirstChild.Name == "img"))
                                        {
                                            {
                                                string url = cNode.FirstChild.Attributes["src"].Value;
                                                using (var imgClient = new WebClient())
                                                // WebClient class inherits IDisposable
                                                {
                                                    try
                                                    {
                                                        Do(() => imgClient.DownloadFile(url, tempPath),
                                                            TimeSpan.FromSeconds(1));
                                                    }
                                                    catch (AggregateException)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        Image img = Image.FromFile(tempPath);

                                        MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked);
                                        Chart.Size = new Size(ariaFlow.Size.Width - 25, Chart.Height);
                                        Chart.btn_ClearSong.Click += (i, y) =>
                                        {
                                            ariaFlow.Controls.Remove(ariaFlow.Controls[ariaFlow.Controls.IndexOf(Chart)]);
                                            if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                                            SaveChartDictionary();
                                        };
                                        Chart.btn_Reorder.Click += (i, y) =>
                                        {
                                            ariaFlow.Controls.SetChildIndex(Chart, ariaFlow.Controls.Count - 1);
                                        };
                                        Invoke(new Action(() => _scrapedSongs.Add(Chart)));
                                    }
                                }
                            }
                        Space();
                        Invoke(new Action(() => ariaView.Dispose()));
                        Console("Finished Scanning Aria Charts, found:" + foundSounds, Color.Red);
                        _finishedScanningAria = true;
                    })
                    { IsBackground = true }.Start();
                }
            };
        }

        private void ScanBillboardHot100()
        {
            string siteUrl = "http://www.billboard.com/charts/hot-100";
            WebView billboardView = WebCore.CreateWebView(1024, 768, WebViewType.Window);
            billboardView.Source = new Uri(siteUrl);
            Console("Started Scanning Billboard Hot 100", Color.CadetBlue);
            billboardView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    int foundSounds = 0;
                    LoadChartDictionary();
                    var
                        doc = new HtmlDocument();

                    doc.LoadHtml(
                        billboardView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        var node =
                            doc.DocumentNode.Descendants("article")
                                .Where(
                                    d =>
                                        d.Attributes.Contains("class") &&
                                        d.Attributes["class"].Value.Contains("chart-row"));
                        foreach (var eleArticle in node.Take(50))
                        {
                            HtmlNode eleChartrowPrimary = eleArticle.SelectSingleNode("div[1]");
                            HtmlNode eleChartrowtitle = eleChartrowPrimary.SelectSingleNode(@"div[@class='chart-row__container']/div[@class='chart-row__title']");
                            HtmlNode eleImage = eleChartrowPrimary.SelectSingleNode(@"div[@class='chart-row__image']");

                            string tempPath = Path.GetTempFileName();
                            string artist;
                            if (eleChartrowtitle.SelectSingleNode("h3[1]").ChildNodes.Count > 1)
                                artist =
                                    HttpUtility.HtmlDecode(
                                        eleChartrowtitle.SelectSingleNode("h3[1]")
                                            .SelectSingleNode("a")
                                            .InnerText.Replace("\n", "")
                                            .Trim());
                            else
                                artist =
                                    HttpUtility.HtmlDecode(
                                        eleChartrowtitle.SelectSingleNode("h3[1]").InnerText.Replace("\n", "").Trim());
                            string title =
                                HttpUtility.HtmlDecode(
                                    eleChartrowtitle.SelectSingleNode("h2[1]").InnerText.Replace("\n", "").Trim());
                            string both = GetFixedFileName(artist + " - " + title);
                            foundSounds++;
                            var enumuerable = new List<MusicChart>(_scrapedSongs);
                            bool isChecked = _chartSongs.Count(str => new DistanceCheck(both, 70).Check(str)) > 0;
                            if (enumuerable.Any(chart => chart.HasSimilar(both)))
                                continue;
                            Image img = Properties.Resources.question_sign_on_person_head;
                            if (eleImage.Attributes.Count > 1)
                            {
                                string url =
                                    eleImage.Attributes[
                                        eleImage.Attributes["style"] == null ? "data-imagesrc" : "style"].Value
                                        .Replace("background-image: url(", "").Replace(")", "");
                                using (var imgClient = new WebClient())
                                // WebClient class inherits IDisposable
                                {
                                    try
                                    {
                                        Do(() => imgClient.DownloadFile(url, tempPath),
                                            TimeSpan.FromSeconds(1));
                                    }
                                    catch (AggregateException)
                                    {
                                    }
                                }
                                img = Image.FromFile(tempPath);
                            }


                            MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked);
                            Chart.Size = new Size(ariaFlow.Size.Width - 25, Chart.Height);
                            Chart.btn_ClearSong.Click += (i, y) =>
                            {
                                ariaFlow.Controls.Remove(ariaFlow.Controls[ariaFlow.Controls.IndexOf(Chart)]);
                                if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                                SaveChartDictionary();
                            };
                            Chart.btn_Reorder.Click += (i, y) =>
                            {
                                ariaFlow.Controls.SetChildIndex(Chart, ariaFlow.Controls.Count - 1);
                            };
                            Invoke(new Action(() => _scrapedSongs.Add(Chart)));
                        }
                        _finishedScanningBillboard = true;
                        Console("Finished Scanning Billboard Charts, found:" + foundSounds, Color.Black);
                        Invoke(new Action(() => billboardView.Dispose()));
                    })
                    { IsBackground = true }.Start();
                }
            };
        }

        private void AnalyseAllFiles()
        {
            new Thread(() =>
              {
                  Invoke(new Action(() => SetStatus("Analysing music files...")));
                  Dictionary<string, string> changedFiles = new Dictionary<string, string>();
                  songsWithoutLyrics.Clear();
                  Invoke(new Action(() =>
                {
                    SetStatus("Processing song names...");
                    metroProgressSpinner1.Value = 0;
                    metroProgressSpinner1.Visible = true;
                    lbl_Percentage.Text = "0";
                    lbl_Percentage.Visible = true;

                    pnlOutcome.Visible = false;
                    pnlProcess.Visible = true;
                    panelControls.Visible = false;
                }));
                  _filter = false;
                  if (_filter)
                      ChangeMonitorPath(Path.Combine(@"D:\Music - Copy", "filter"));
                  _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);

                  foreach (string filePath in _files)
                  {
                      Invoke(new Action(() => SetSubstatus(Path.GetFileNameWithoutExtension(filePath))));
                      if (!filePath.Contains(".ini"))
                          using (var file = File.Create(filePath))
                          {
                              if (!file.Tag.Pictures.Any())
                              {
                                  new Thread(() =>
                                {
                                    try
                                    {
                                        Do(() => ProcessArtwork(filePath), TimeSpan.FromSeconds(2));
                                    }
                                    catch
                                    {
                                        // ignored
                                    }
                                })
                                  { IsBackground = true }.Start();
                              }

                              //if (file.Tag.Lyrics == null || file.Tag.Lyrics.Count() < 5)
                              //{
                              //    songsWithoutLyrics.Add(file.Name);
                              //}
                          }
                      //string lyrics = RetreiveLyrics(filePath);
                      //if (lyrics != "")
                      //{
                      //    Console.WriteLine(filePath);
                      //    Console.Write(lyrics);
                      //    Console.ReadKey();
                      //}
                      string fixedFileName = FixFile(filePath);
                      if (fixedFileName != Path.GetFileNameWithoutExtension(filePath))
                      {
                          changedFiles.Add(Path.GetFileNameWithoutExtension(filePath), fixedFileName);
                      }
                      int fileIndex = Array.IndexOf(_files, filePath) + 1;
                      int fileAmount = _files.Count();
                      float percentage = ((float)fileIndex / (float)fileAmount) * 100;
                      System.Console.WriteLine(percentage);
                      Invoke(new Action(() =>
                    {
                        metroProgressSpinner1.Value = (int)Math.Ceiling(percentage);
                        lbl_Percentage.Text = Math.Ceiling(percentage).ToString();
                    }));
                  }
                  Invoke(new Action(() =>
                  {
                      FindSongLyrics();
                      ScanCharts();
                      lblResults.Text = "";
                      if (changedFiles.Any())
                      {
                          lblResults.Text += "The following names have been changed:";
                          foreach (var s in changedFiles)
                          {
                              lblResults.Text += Environment.NewLine + "Old: " + Path.GetFileNameWithoutExtension(s.Key) +
                                                 ", ";

                              lblResults.Text += Environment.NewLine + "New: " + s.Value;
                          }
                      }
                      else
                      {
                          lblResults.Text += "No names changes have been made.";
                      }
                      SetStatus("Finished processing your music files", Status.Good);
                      SetSubstatus(_files.Length + " song files processed succesfully.", Status.Good);
                      lbl_Percentage.Visible = false;
                      metroProgressSpinner1.Visible = false;
                      pnlProcess.Visible = false;
                      panelControls.Visible = true;
                      pnlOutcome.Visible = true;
                      focusMe.Focus();
                      ScanLocalLyrics();
                  }));
              })
            { IsBackground = true }.Start();
        }

        private bool ProcessArtwork(string filename, bool showdiag = true)
        {
            return filename.Contains("-") && GoogleImageSearch(Path.GetFileNameWithoutExtension(filename), showdiag);
        }

        private void DumpArtwork()
        {
            var spath = @"D:\Artwork Dump";

            _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
            Directory.CreateDirectory(spath);
            foreach (string filePath in _files)
            {
                using (var file = File.Create(filePath))
                {
                    if (!file.Tag.Pictures.Any()) continue;
                    using (var ms = new MemoryStream(file.Tag.Pictures[0].Data.Data))
                    {
                        if (filePath.EndsWith(".ini.mp3")) continue;
                        string savePath = Path.Combine(spath, Path.GetFileNameWithoutExtension(filePath) + ".jpg");
                        var img = Image.FromStream(ms);
                        img.Save(savePath);
                    }
                }
            }
        }

        private void NotificationIcon_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => WindowState = FormWindowState.Normal));
        }

        public void Space()
        {
            Console("");
        }

        public static void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        public void Console(string text, Color? color = null, bool newline = true)
        {
            Invoke(new Action(() =>
            {
                AppendText(logBox, (newline ? Environment.NewLine : "") + text, color ?? Color.Black);
            }));
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {

            allowClose = true;

            NotificationIcon.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private List<String> fileChangedExceptions = new List<string>();
        private string FixFile(string filePath)
        {
            var file = new TrontorMP3File(filePath);
            string finalName = file.FixFileName();
            string movePath = Path.Combine(_path, finalName + ".mp3");
            if (filePath != movePath && !System.IO.File.Exists(movePath))
            {
                fileChangedExceptions.Add(movePath);
                System.IO.File.Move(filePath, movePath);
            }
            return finalName;
        }

        private void PrintRow(params string[] columns)
        {
            int width = (_tableWidth - columns.Length) / columns.Length;
            var row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            System.Console.WriteLine(row);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            return string.IsNullOrEmpty(text)
                ? new string(' ', width)
                : text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }

        private void mnuArtists_Click(object sender, EventArgs e)
        {
            ShowWindow(GetConsoleWindow(), 3);

            _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
            var list = new List<string>();
            var titles = new List<string>();
            foreach (string filePath in _files)
            {
                if (filePath.Contains(".ini")) continue;
                using (var file = File.Create(filePath))
                {
                    foreach (string artist in file.Tag.Performers.Where(artist => !list.Contains(artist)))
                    {
                        list.Add(artist.Trim());
                    }
                    if (titles.Contains(file.Tag.Title))
                    {
                        Console("Duplicate Title:" + filePath, Color.Yellow);
                    }
                    else
                        titles.Add(file.Tag.Title);
                    file.Dispose();
                }
            }
            string longeststring = list.OrderByDescending(s => s.Length).First();
            _tableWidth = longeststring.Length * 7 + 5;
            var strings = DivideStrings(7, list.ToArray());
            foreach (var strs in strings)
            {
                PrintRow(strs.ToArray());
            }
        }

        private T[] CopyPart<T>(T[] array, int index, int length)
        {
            var newArray = new T[length];
            Array.Copy(array, index, newArray, 0, length);
            return newArray;
        }

        private List<string[]> DivideStrings(int expectedStringsPerArray, string[] allStrings)
        {
            var arrays = new List<string[]>();

            int arrayCount = allStrings.Length / expectedStringsPerArray;

            int elemsRemaining = allStrings.Length;
            for (int arrsRemaining = arrayCount; arrsRemaining >= 1; arrsRemaining--)
            {
                int elementCount = elemsRemaining / arrsRemaining;

                var array = CopyPart(allStrings, elemsRemaining - elementCount, elementCount);
                arrays.Insert(0, array);

                elemsRemaining -= elementCount;
            }

            return arrays;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void monitor_CreatedOrChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                #region Part 1: Fixing dat filename
                _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
                string base_FileName = Path.GetFileNameWithoutExtension(e.FullPath);
                if (base_FileName.Contains("-"))
                {
                    string fixed_FileName = FixFile(e.FullPath);
                    #endregion

                    Invoke(new Action(() =>
                    {
                        string monitorTxt = string.Format("A file has been {0}. The filename ", e.ChangeType.ToString().ToLower());
                        switch (e.ChangeType)
                        {
                            case WatcherChangeTypes.Created:
                                monitorTxt += "is ";
                                break;
                            default:
                                monitorTxt += "was ";
                                break;
                        }
                        if (!fileChangedExceptions.Contains(e.FullPath))
                        {
                            monitorTxt += base_FileName;
                            lblMonitorStatus.Text = monitorTxt;
                            Console(monitorTxt, Color.OrangeRed);
                        }
                    }));
                    if (base_FileName != fixed_FileName)
                    {
                        Console("The file ", Color.Magenta);
                        Console(base_FileName, Color.Blue, false);
                        Console(" has been changed to ", Color.Magenta, false);
                        Console(fixed_FileName + ".", Color.Blue, false);
                        Space();
                    }
                    ProcessArtwork(e.FullPath);
                    FindSongLyrics(fixed_FileName);
                    ScanLocalLyrics();
                    new Thread(() =>
                    {
                        Thread.Sleep(5000);
                        Invoke(new Action(() => { lblMonitorStatus.Text = "Waiting for file changes..."; }));
                    })
                    { IsBackground = true }.Start();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {
        }

        private bool _getInput;

        private void ChangeMonitorPath(string pathdir)
        {
            _path = pathdir;
            lblMonitoringDirectory.Text = pathdir;
        }

        private void logInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                RunConsoleCommand(logInput.Text);
            }
        }

        private void RunConsoleCommand(string command)
        {
            Dictionary<string, string> commandList = new Dictionary<string, string>();

            commandList.Add("filter", "Changes the file monitoring/analysis path to your filter path.");
            commandList.Add("regular", "Changes the file monitoring/analysis path back to normal.");
            commandList.Add("analyse", "Analyses all files in the set monitoring path.");
            commandList.Add("dump artwork", "Dumps artwork into a SPECIFIED DIRECTORY");
            commandList.Add("help", "I don't know man");

            switch (command)
            {
                case "filter":
                    _filter = true;
                    ChangeMonitorPath(@"D:\Music - Copy\filter");
                    Console("Filter has been enabled.", Color.Black);
                    break;
                case "regular":
                    _filter = false;
                    ChangeMonitorPath(@"D:\Music");
                    Console("Filter has been disabled.", Color.Black);
                    break;
                case "analyse":
                    AnalyseAllFiles();
                    break;
                case "dump artwork":
                    DumpArtwork();
                    break;
                case "help":
                    Console("Here's a list of available commands:", Color.DarkSeaGreen);
                    foreach (var s in commandList)
                    {
                        Console(s.Key, Color.PaleVioletRed, false);

                        Console(" : " + s.Value, Color.Black);
                    }
                    break;
                default:
                    Console("Command: " + logInput.Text + " not recognised :^(", Color.Red);
                    Console("Type 'help' for a command list.", Color.Black);
                    break;
            }
            _getInput = true;
            logInput.WaterMarkColor = Color.Black;
            logInput.Text = "";
        }

        public string GetFixedFileName(string name)
        {
            return new TrontorMP3File(name).MetaFixFilename().Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(path_programData))
                Directory.CreateDirectory(path_programData);
            this.WindowState = FormWindowState.Minimized;
            SetStatus("Initialising Application", Status.Normal, false);
            if (_filter)
                ChangeMonitorPath(@"D:\Music - Copy");
            var notifyThread = new Thread(delegate ()
            {
                Menu = new ContextMenu();

                Menu.MenuItems.Add(0, new MenuItem("List Artists", mnuArtists_Click));
                Menu.MenuItems.Add(1, new MenuItem("Open Folder", (o, d) =>
                {
                    Process.Start(path_programData);
                }));
                Menu.MenuItems.Add(2, new MenuItem("Exit", mnuExit_Click));

                NotificationIcon = new NotifyIcon
                {
                    Icon = Resources.mp3,
                    ContextMenu = Menu,
                    Text = "MP3 File Monitor -- Made for Rohyl, by Rohyl"
                };
                NotificationIcon.DoubleClick += NotificationIcon_Click;

                NotificationIcon.Visible = true;
                Application.Run();
            });
            _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
            notifyThread.Start();

            SetStatus("Starting monitor component...");
            var monitor = new FileSystemWatcher(_path, "*.mp3") { EnableRaisingEvents = true };
            monitor.Created += monitor_CreatedOrChanged;
            monitor.Deleted += monitor_CreatedOrChanged;
            monitor.Changed += monitor_CreatedOrChanged;
            SetStatus("Preparing music file analysis");
            AnalyseAllFiles();
            //FindSongLyrics();
            SetStatus("Type help for console commands.", Status.Good);
            ScanLocalLyrics();
        }

        private void btn_RescanAll_Click(object sender, EventArgs e)
        {
            ScanLocalLyrics();
        }

        bool _firstLoad = true;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!_firstLoad)
            {
                MessageBox.Show(this.WindowState.ToString());
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = false;
                }
                this.ShowInTaskbar = true;
            }
            else
                _firstLoad = true;
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text == "Process Log")
            {
                logInput.Focus();
            }
            else if (tabControl.SelectedTab.Text == "Status")
            {
                pnlOutcome.Visible = false;

                pnlOutcome.Visible = true;
            }
        }

        bool allowClose = false;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Invalidate();
            //  CenterToScreen();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            pnl_searchingCharts.Visible = true;
            ScanCharts();
        }

        private void lyricsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lyricsView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = lyricsView.HitTest(e.X, e.Y);
                lyricsView.ClearSelection();
                if (hti.RowY > 0 && hti.ColumnX > 0)
                    lyricsView.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void lyricsView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Open File", (o, h) =>
                {
                    string filePath = (string)lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0].Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    Process.Start(path);
                }));
                m.MenuItems.Add(new MenuItem("Scan Artwork", (o, h) =>
                {
                    string filePath = (string)lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0].Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    ProcessArtwork(path);
                }));
                m.MenuItems.Add(new MenuItem("Scan Lyrics on MusixMatch (Reccommended)", (o, h) =>
                {
                    string filePath = (string)lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0].Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    if (System.IO.File.Exists(path))
                    {
                        FindSongLyrics(path);
                    }
                }));
                m.MenuItems.Add(new MenuItem("Scan Lyrics on AzLyrics", (o, h) =>
                {
                    string filePath = (string)lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0].Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    if (System.IO.File.Exists(path))
                    {
                        FindSongLyrics(path, LyricSite.AzLyrics);
                    }
                }));
                m.MenuItems.Add(new MenuItem("Delete Lyrics", (o, h) =>
                {
                    string filePath = (string)lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0].Value;
                    if (MessageBox.Show("Are you sure you want to delete the lyrics for " + filePath + " ?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                        DeleteLyrics(path);
                        ScanLocalLyrics();
                    }
                }));
                m.Show(lyricsView, new Point(e.X, e.Y));
            }
        }

        private void ShazamBrowserLoadingFrameComplete(object sender, FrameEventArgs e)
        {

        }

        int index = 0;

        private void LyricsBrowsersLoadingFrameComplete(object sender, FrameEventArgs e)
        {
            if (!e.IsMainFrame) return;
            int maxIndex = songsWithoutLyrics.Count - 1;
            try
            {
                var doc = new HtmlDocument();
                string html = lyricsBrowser.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML");
                doc.LoadHtml(html);
                //The results obtained through the search
                int searchResults = 0;
                if (doc.DocumentNode.InnerHtml.Contains("To continue, please"))
                {
                    MessageBox.Show("Captcha!!");
                    return;
                }
                if (doc.DocumentNode.InnerHtml.Contains("\"resultStats\""))
                {
                    string amountResults = Regex.Replace(doc.DocumentNode.SelectSingleNode("//*[@id=\"resultStats\"]").InnerText, @" ?\(.*?\)", string.Empty);

                    int removeIndx = amountResults.IndexOf("results");
                    if (removeIndx > 0)
                        amountResults = amountResults.Substring(0, removeIndx);
                    searchResults = int.Parse(Regex.Replace(amountResults, "[^0-9.]", ""));
                }
                if (string.IsNullOrEmpty(searchResults.ToString()) || searchResults == 0)
                {
                    _lyricsException.Add(songsWithoutLyrics[index]);
                    if (index < maxIndex)
                    {
                        if (siteSearched == LyricSite.MusixMatch)
                        {
                            lyricsBrowser.Source =
                                   LyricSearchString(new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false), LyricSite.AzLyrics);
                        }
                        else
                        {
                            index++;
                            lyricsBrowser.Source =
                                LyricSearchString(new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false));
                        }
                    }
                    SaveLyricExceptions();
                }
                int maximumSearchIterations = (searchResults > 2 ? 2 : searchResults);
                for (int i = 1; i <= maximumSearchIterations; i++)
                {
                    HtmlNode aTag;
                    if (searchResults == 1)
                        aTag = doc.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/h3/a");
                    else
                        aTag = doc.DocumentNode.SelectSingleNode(string.Format("//*[@id=\"rso\"]/div/div[{0}]/div/h3/a", i));
                    string title =
                        new TrontorMP3File(HttpUtility.HtmlDecode(
                            aTag.InnerText.Replace(" - A-Z Lyrics", "")
                                .Replace("LYRICS", "")
                                .Replace("| Musixmatch", "")
                                .Replace("lyrics", ""))).MetaFixFilename(false);
                    string url = aTag.Attributes["href"].Value;
                    string fixFname = new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false);
                    var similarity = CalculateSimilarity(fixFname.ToLower(), title.ToLower()) * 100;
                    if (similarity < 15)
                    {
                        return;
                    }
                    var levehCheck = new DistanceCheck(fixFname.ToLower(), 70).Check(title.ToLower()) || new DistanceCheck(Path.GetFileNameWithoutExtension(songsWithoutLyrics[index]).ToLower(), 70).Check(title.ToLower());
                    if (levehCheck || MessageBox.Show("Does this look like the right title?" + Environment.NewLine + Path.GetFileNameWithoutExtension(songsWithoutLyrics[index]) + Environment.NewLine + title, "Lyric Verification", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        WebView lyricsPageBrowserLocal = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);

                        string lyricsUrl = string.Format(url);
                        lyricsPageBrowserLocal.Source = new Uri(lyricsUrl);
                        lyricsPageBrowserLocal.LoadingFrameComplete += (pol, kol) =>
                        {
                            if (kol.IsMainFrame)
                            {
                                if (lyricsCertainSong != "")
                                    Console(index + 1 + "/" + songsWithoutLyrics.Count, Color.Gray);
                                var lyricsdoc = new HtmlDocument();
                                lyricsdoc.LoadHtml(lyricsPageBrowserLocal.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                                string rawLyrics = "";
                                switch (siteSearched)
                                {
                                    case LyricSite.AzLyrics:
                                        rawLyrics =
                                            lyricsdoc.DocumentNode.SelectSingleNode("/body/div[3]/div/div[2]/div[6]")
                                                .InnerText;
                                        break;
                                    case LyricSite.MusixMatch:
                                        var counts =
                                            (lyricsdoc.DocumentNode.Descendants()
                                                .Where(
                                                    x =>
                                                        x.Attributes.Contains("class") &&
                                                        x.Attributes["class"].Value == "mxm-empty__title")
                                                .ToList());
                                        if (counts.Count != 0)
                                        {
                                            index++;
                                            lyricsBrowser.Source =
                                                LyricSearchString(
                                                    new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false));
                                            return;
                                        }
                                        rawLyrics = lyricsdoc.DocumentNode.Descendants().Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "mxm-lyrics__content").ToList()[0].InnerText;
                                        break;
                                }
                                string cleanLyrics = (siteSearched == LyricSite.AzLyrics ? rawLyrics.Remove(rawLyrics.IndexOf("<!--"), rawLyrics.IndexOf("-->") + 3) : rawLyrics);
                                using (var fiel = File.Create(songsWithoutLyrics[index]))
                                {
                                    Console("Saving lyrics for " + fixFname + " | Loaded " + lyricsUrl.Replace("http://www.azlyrics.com/lyrics/", "").Replace("https://www.musixmatch.com/lyrics/", ""), Color.Green);

                                    fiel.Tag.Lyrics = cleanLyrics;
                                    fiel.Save();
                                }
                                if (index < maxIndex)
                                {
                                    index++;
                                    lyricsBrowser.Source = LyricSearchString(new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false));
                                }
                                else if (index == maxIndex)
                                    ScanLocalLyrics();
                            }
                        };
                        break;
                    }
                    Debug.WriteLine("Mainframe breached");
                    if (i + 1 == maximumSearchIterations || maximumSearchIterations == 1)
                    {
                        _lyricsException.Add(songsWithoutLyrics[index]);
                        if (index < maxIndex)
                        {
                            index++;
                            lyricsBrowser.Source = LyricSearchString(new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false));
                        }
                        SaveLyricExceptions();
                    }
                }
            }
            catch (Exception ex)
            {
                Console("Caught lyrics exception :(", Color.Orange);
                if (index < maxIndex && index < songsWithoutLyrics.Count - 1)
                {
                    index++;
                    lyricsBrowser.Source = LyricSearchString(new TrontorMP3File(songsWithoutLyrics[index]).MetaFixFilename(false));
                }
            }
            ScanLocalLyrics();
        }
        private void tmr_ScanCharts_Tick(object sender, EventArgs e)
        {
            shazamBrowser.Reload(false);
            shazamBrowser.Refresh();
            ScanCharts();
        }


        private void chk_hideCheckedCharts_CheckedChanged(object sender, EventArgs e)
        {
            PopulateChartLayout();
        }

        private void btn_orderCharts_Click(object sender, EventArgs e)
        {
            SortedList<string, Control> sl = new SortedList<string, Control>();
            foreach (MusicChart i in ariaFlow.Controls)
            {
                try
                {
                    sl.Add(i.lbl_Artist.Text + i.lbl_Title.Text, i);
                }
                catch (Exception ex)
                {
                }
            }
            foreach (Control j in sl.Values)
            {
                ariaFlow.Controls.SetChildIndex(j, ariaFlow.Controls.Count - 1);
            }
        }

        private void btn_refreshCharts_Click(object sender, EventArgs e)
        {
            PopulateChartLayout();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            AnalyseAllFiles();
        }

        private void ariaFlow_SizeChanged(object sender, EventArgs e)
        {
            ariaFlow.SuspendLayout();
            foreach (Control ctrl in ariaFlow.Controls)
            {
                if (ctrl is MusicChart)
                    ctrl.Width = ariaFlow.ClientSize.Width;
            }
            ariaFlow.ResumeLayout();
        }

        private void ariaFlow_ControlAdded(object sender, ControlEventArgs e)
        {
            lbl_chartCount.Text = string.Format("{0} songs listed.", ariaFlow.Controls.Count);
            if (ariaFlow.Controls.Count > 0)
            {
                pnl_EmptyCharts.Visible = false;
                pnl_searchingCharts.Visible = false;
            }
        }

        private void ariaFlow_ControlRemoved(object sender, ControlEventArgs e)
        {
            lbl_chartCount.Text = string.Format("{0} songs listed.", ariaFlow.Controls.Count);
            if (ariaFlow.Controls.Count == 0)
            {
                pnl_EmptyCharts.Visible = true;
            }
        }
    }
}

