using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static string PathProgramData =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Rohyl's MP3 File Manager");
        private string[] _files;
        private bool _allowClose;
        private bool _filter;
        private bool _finishedScanningAria, _finishedScanningBillboard, _finishedScanningShazam;
        private bool _firstLoad = true;
        private bool _getInput;
        private int _index;
        private int _processedArtwork;
        private bool _showBalloon = true;
        private LyricSite _siteSearched = LyricSite.AzLyrics;
        private List<string> _songsWithoutLyrics;
        private int _tableWidth = 77;
        public new ContextMenu Menu;
        public NotifyIcon NotificationIcon;

        public List<WebView> browserTabs = new List<WebView>();
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            SetStyle(ControlStyles.ResizeRedraw, true);
            StartPosition = FormStartPosition.Manual;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            ChangeMonitorPath(GetMusicPath());
            _songsWithoutLyrics = new List<string>();
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
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

        private static List<MusicChart> ScrapedSongs = new List<MusicChart>();
        private readonly List<string> _fileChangedExceptions = new List<string>();

        private static Dictionary<string, DateTime> _chartDates = new Dictionary<string, DateTime>();
        private readonly string _pathChartDates = Path.Combine(PathProgramData, "SeenChartSongs.xml");

        private static List<string> _chartSongs = new List<string>();
        private readonly string _pathChartSongs = Path.Combine(PathProgramData, "ChartSongs.xml");

        private static List<string> _lyricsException = new List<string>();
        private static readonly string PathLyricsExceptions = Path.Combine(PathProgramData, "LyricExceptions.xml");


        private static List<string> _songArtworkApplied = new List<string>();
        private static readonly string _pathSongArtworkApplied = Path.Combine(PathProgramData, "SongArtworkApplied.xml");

        private void SaveChartDates()
        {
            XElement xElem = new XElement(
                       "items",
                       _chartDates.Select(x => new XElement("item", new XAttribute("id", x.Key), new XAttribute("value", x.Value)))
                    );
            string xml = xElem.ToString();
            xElem.Save(_pathChartDates);
        }

        private void LoadChartDates()
        {
            XElement xElem2 = new XElement("items");
            if (System.IO.File.Exists(_pathChartDates))
                xElem2 = XElement.Load(_pathChartDates);
            else
                xElem2.Save(_pathChartDates);
            _chartDates = xElem2.Descendants("item")
                                .ToDictionary(x => (string)x.Attribute("id"), x => (DateTime)x.Attribute("value"));
        }

        private void SaveChartDictionary()
        {
            XElement xElem = new XElement(
                "items",
                _chartSongs.Select(x => new XElement("item", x)));
            string xml = xElem.ToString();
            xElem.Save(_pathChartSongs);
        }

        private void LoadChartDictionary()
        {
            XElement xElem2 = new XElement("items");
            if (System.IO.File.Exists(_pathChartSongs))
                xElem2 = XElement.Load(_pathChartSongs);
            else
                xElem2.Save(_pathChartSongs);
            _chartSongs = xElem2.Descendants("item").Select(x => HttpUtility.HtmlDecode(x.Value)).ToList();
        }

        private static void SaveLyricExceptions()
        {
            XElement xElem = new XElement(
                "items",
                _lyricsException.Distinct().ToList().Select(x => new XElement("item", x)));
            string xml = xElem.ToString();
            xElem.Save(PathLyricsExceptions);
        }

        private static void LoadLyricExceptions()
        {
            XElement xElem2 = new XElement("items");
            if (System.IO.File.Exists(PathLyricsExceptions))
                xElem2 = XElement.Load(PathLyricsExceptions);
            else
                xElem2.Save(PathLyricsExceptions);
            _lyricsException = xElem2.Descendants("item").Select(x => x.Value).ToList();
        }

        private static void SaveSongArtworks()
        {
            XElement xElem = new XElement(
                "items",
                _songArtworkApplied.Distinct().ToList().Select(x => new XElement("item", x)));
            xElem.Save(_pathSongArtworkApplied);
        }

        private static void LoadSongArtwork()
        {
            XElement xElem2 = new XElement("items");
            if (System.IO.File.Exists(_pathSongArtworkApplied))
                xElem2 = XElement.Load(_pathSongArtworkApplied);
            else
                xElem2.Save(_pathSongArtworkApplied);
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
            List<Exception> exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
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
        ///     Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private bool GoogleImageSearch(string query, bool showdialog = false, bool bypassExistingCheck = false)
        {
            string filePath = Path.Combine(GetMusicPath(), query + ".mp3");
            string entitized = HttpUtility.UrlEncode(query);
            string url = string.Format("https://www.google.com.au/search?q={0}&tbm=isch",
                entitized + " song cover artwork");
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                client.Headers.Add("user-agent",
                    "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                string htmlCode = client.DownloadString(url);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);
                for (int index = 0;
                    index < doc.DocumentNode.SelectSingleNode("//*[@id=\"images\"]").ChildNodes.Count;
                    index++)
                {
                    if (index < 0)
                        index = 0;
                    HtmlNode eleImg = doc.DocumentNode.SelectSingleNode("//*[@id=\"images\"]").ChildNodes[index];
                    string thumbNailUrl = eleImg.FirstChild.Attributes["src"].Value;
                    string tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();

                    client.DownloadFile(thumbNailUrl, tempPath);

                    Screen rightmost = Screen.AllScreens[0];
                    foreach (
                        Screen screen in
                            Screen.AllScreens.Where(screen => screen.WorkingArea.Right > rightmost.WorkingArea.Right))
                    {
                        rightmost = screen;
                    }


                    string landingPg = HtmlEntity.DeEntitize(eleImg.Attributes["href"].Value);
                    HtmlDocument innerDoc = new HtmlDocument();
                    client.Headers.Add("user-agent",
                        "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                    string inrHTml = client.DownloadString(landingPg);
                    innerDoc.LoadHtml(inrHTml);

                    foreach (
                        HtmlNode element in
                            innerDoc.DocumentNode.Descendants()
                                .Where(
                                    element =>
                                        element.InnerText.Contains("full size") && element.Attributes.Contains("href")))
                    {
                        using (WebClient imgClient = new WebClient()) // WebClient class inherits IDisposable
                        {
                            tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
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
                    bool noArtwork = false;
                    bool applyArtwork = false;
                    if (Settings.Default.LastSize.Height == 0 || Settings.Default.LastSize.Width == 0)
                    {
                        Settings.Default.LastSize = new Size(500, 500);
                    }

                    using (File file = File.Create(filePath))
                    {
                        IPicture artwork = new Picture(tempPath);
                        long currentSize = 0, newSize = 1;
                        if (file.Tag.Pictures.Any())
                        {
                            IPicture currentArtwork = file.Tag.Pictures[0];
                            using (MemoryStream ms = new MemoryStream(currentArtwork.Data.Data))
                            {
                                currentSize = ms.Length;
                            }
                            using (MemoryStream ms = new MemoryStream(artwork.Data.Data))
                            {
                                newSize = ms.Length;
                            }
                        }
                        if (currentSize == newSize)
                            return false;
                    }

                    if (showdialog && _songArtworkApplied.Contains(filePath) || bypassExistingCheck)
                    {
                        Form f = new Form
                        {
                            FormBorderStyle = FormBorderStyle.SizableToolWindow,
                            Size = Settings.Default.LastSize,
                            UseWaitCursor = false
                        };
                        f.Resize += (i, u) =>
                        {
                            Control control = (Control)i;
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
                            Timer t = new Timer { Interval = 10000 };
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
                doc = null;
                GC.Collect();
                return false;
            }
        }

        public bool AddArtwork(string filePath, string imgPath)
        {
            using (File file = File.Create(filePath))
            {
                IPicture artwork = new Picture(imgPath);
                long currentSize = 0, newSize = 1;
                if (file.Tag.Pictures.Any())
                {
                    IPicture currentArtwork = file.Tag.Pictures[0];
                    using (MemoryStream ms = new MemoryStream(currentArtwork.Data.Data))
                    {
                        currentSize = ms.Length;
                    }
                    using (MemoryStream ms = new MemoryStream(artwork.Data.Data))
                    {
                        newSize = ms.Length;
                    }
                }
                if (currentSize != newSize)
                {
                    string nameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                    Console("The file artwork for ", Color.Black);
                    Console(nameWithoutExtension, Color.Blue, false);
                    Console(" has been changed.", Color.Black, false);
                    file.Tag.Pictures = new IPicture[1] { artwork };
                    SaveTagLibFile(file, filePath);
                    if (!_songArtworkApplied.Contains(filePath))
                        _songArtworkApplied.Add(filePath);
                    SaveSongArtworks();
                }
                return currentSize == newSize;
            }
        }

        private void SaveTagLibFile(File file, string filePath)
        {
            DialogResult res = DialogResult.Cancel;
            if (IsFileLocked(new FileInfo(filePath)))
                while (true)
                {
                    res =
                        MessageBox.Show(
                            string.Format(
                                "The file {0} is current in use by another application." +
                                "{1}Please close any applications that are using this file then press OK to continue.",
                                Path.GetFileNameWithoutExtension(filePath), Environment.NewLine),
                            "File Access Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    if (res == DialogResult.Cancel)
                        break;
                    if (res == DialogResult.OK)
                        if (!IsFileLocked(new FileInfo(filePath)))
                        {
                            file.Save();
                            break;
                        }
                }
            else
                file.Save();
        }

        private string LyricSearchString(string query, bool reverseString, LyricSite site = LyricSite.MusixMatch)
        {
            _siteSearched = site;
            if (reverseString)
            {
                int index = query.IndexOf("?");
                if (index > 0)
                    query = query.Substring(0, index);
                return HttpUtility.UrlDecode(query);
            }
            return
            string.Format(
                "https://www.google.com.au/search?q={0}+site:" +
                (site == LyricSite.AzLyrics ? "azlyrics.com" : "www.musixmatch.com/lyrics/"),
                HttpUtility.UrlEncode(query));
        }

        /// <summary>
        ///     Calculate percentage similarity of two strings
        ///     <param name="source">Source String to Compare with</param>
        ///     <param name="target">Targeted String to Compare</param>
        ///     <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        private double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return 1.0 - stepsToSame / (double)Math.Max(source.Length, target.Length);
        }

        /// <summary>
        ///     Returns the number of steps required to transform the source string
        ///     into the target string.
        /// </summary>
        private static int ComputeLevenshteinDistance(string source, string target)
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
                    int cost = target[j - 1] == source[i - 1] ? 0 : 1;

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
                using (File fiel = File.Create(filePath))
                {
                    fiel.Tag.Lyrics = "";
                    SaveTagLibFile(fiel, filePath);
                }
        }

        private static void AddLyricException(string filePath)
        {
            _lyricsException.Add(filePath);
            SaveLyricExceptions();
        }

        private void FoundLyrics(string path)
        {
            Space();
            Space();
            Console("♫ ", Color.Green, false);
            Console("Lyrics have been found and applied for: " + Path.GetFileNameWithoutExtension(path), Color.Red, false);
            Console(" ♫ ", Color.Green, false);
            Space();
            Space();

        }

        private void NoLyrics(string path)
        {
            Space();
            Space();
            Console("♫ ", Color.Blue, false);
            Console("No lyrics currently available for the file: " + Path.GetFileNameWithoutExtension(path), Color.Red, false);
            Console(" ♫ ", Color.Blue, false);
        }

        private void FindSongLyrics(string filePath, LyricSite site = LyricSite.MusixMatch)
        {
            WebView LyricsBrowser = WebCore.CreateWebView(512, 384, WebViewType.Offscreen);
            browserTabs.Add(LyricsBrowser);
            string fileNameNoArtists = new TrontorMp3File(filePath).MetaFixFilename(false);
            Uri searchString = new Uri(LyricSearchString(fileNameNoArtists, false, site));
            LyricsBrowser.LoadingFrameComplete += (o, e) =>
            {
                if (!e.IsMainFrame) return;
                HtmlDocument document = new HtmlDocument();
                int searchResults = 0;
                string sourceHtml =
                    LyricsBrowser.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML");
                document.LoadHtml(sourceHtml);
                if (document.DocumentNode.InnerHtml.Contains("To continue, please"))
                {
                    MessageBox.Show(@"There have been too many requests to search for lyrics.\n Google is blocking our searches :(.\n Please wait a while before trying again.");

                    Space();
                    Console("Captcha blocked lyric search. Please wait.", Color.Blue);
                    Space();

                    return;
                }
                if (document.DocumentNode.InnerHtml.Contains("\"resultStats\""))
                {
                    string amountOfResults =
                        Regex.Replace(document.DocumentNode.SelectSingleNode("//*[@id=\"resultStats\"]").InnerText,
                            @" ?\(.*?\)", string.Empty);

                    int removeIndx = amountOfResults.IndexOf("results", StringComparison.Ordinal);
                    if (removeIndx > 0)
                        amountOfResults = amountOfResults.Substring(0, removeIndx);
                    searchResults = int.Parse(Regex.Replace(amountOfResults, "[^0-9.]", ""));
                }
                if (string.IsNullOrEmpty(searchResults.ToString()) || searchResults == 0)
                {
                    AddLyricException(filePath);
                    NoLyrics(filePath);
                    return;
                }
                int maximumSearchIterations = searchResults > 2 ? 2 : searchResults;
                bool LyricsAvailable = false;
                for (int i = 1; i <= maximumSearchIterations; i++)
                {
                    HtmlNode aTag;

                    if (searchResults == 1)
                        aTag = document.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/h3/a");
                    else
                        aTag =
                            document.DocumentNode.SelectSingleNode(
                                string.Format("//*[@id=\"rso\"]/div/div[{0}]/div/h3/a", i));

                    string description;
                    if (searchResults == 1)
                        description =
                            document.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/div/div/span").InnerText;
                    else
                        description =
                            document.DocumentNode.SelectSingleNode(
                                string.Format("//*[@id=\"rso\"]/div/div[{0}]/div/div/div/span", i)).InnerText;

                    if (description.Contains("Lyrics not available"))
                    {
                        if (i == maximumSearchIterations)
                        {
                            AddLyricException(filePath);
                            NoLyrics(filePath);
                        }
                        continue;
                    }
                    string resultText =
                            new TrontorMp3File(HttpUtility.HtmlDecode(
                                aTag.InnerText.Replace(" - A-Z Lyrics", "")
                                    .Replace("LYRICS", "")
                                    .Replace("| Musixmatch", "")
                                    .Replace("lyrics", ""))).MetaFixFilename(false);
                    string url = aTag.Attributes["href"].Value;
                    // string fixedSearchFileName = new TrontorMp3File(filePath).MetaFixFilename(false);
                    double similarity = CalculateSimilarity(fileNameNoArtists.ToLower(), resultText.ToLower()) * 100;
                    if (similarity < 15)
                    {
                        NoLyrics(filePath);
                        continue;
                    }
                    if (!(similarity >= 70) &&
                        MessageBox.Show(
                            "Does this look like the right title?" + Environment.NewLine +
                            Path.GetFileNameWithoutExtension(filePath) + Environment.NewLine + resultText,
                            "Lyric Verification", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        if (maximumSearchIterations == 1 || i + 1 == maximumSearchIterations)
                            NoLyrics(filePath);
                        continue;
                    }

                    WebView lyricsPageBrowserLocal = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
                    browserTabs.Add(lyricsPageBrowserLocal);
                    string lyricsUrl = string.Format(url);
                    lyricsPageBrowserLocal.Source = new Uri(lyricsUrl);
                    lyricsPageBrowserLocal.LoadingFrameComplete += (pol, kol) =>
                    {
                        if (kol.IsMainFrame)
                        {
                            HtmlDocument lyricsdoc = new HtmlDocument();
                            lyricsdoc.LoadHtml(
                                lyricsPageBrowserLocal.ExecuteJavascriptWithResult(
                                    "document.getElementsByTagName('html')[0].innerHTML"));
                            string rawLyrics = "";
                            switch (_siteSearched)
                            {
                                case LyricSite.AzLyrics:
                                    rawLyrics =
                                        lyricsdoc.DocumentNode.SelectSingleNode("/body/div[3]/div/div[2]/div[6]")
                                            .InnerText;
                                    break;
                                case LyricSite.MusixMatch:
                                    List<HtmlNode> counts =
                                        lyricsdoc.DocumentNode.Descendants()
                                            .Where(
                                                x =>
                                                    x.Attributes.Contains("class") &&
                                                    x.Attributes["class"].Value == "mxm-empty__title")
                                            .ToList();
                                    if (counts.Count != 0)
                                    {
                                        NoLyrics(filePath);
                                        return;
                                    }
                                    rawLyrics =
                                        lyricsdoc.DocumentNode.Descendants()
                                            .Where(
                                                x =>
                                                    x.Attributes.Contains("class") &&
                                                    x.Attributes["class"].Value == "mxm-lyrics__content")
                                            .ToList()[0].InnerText;
                                    break;
                            }
                            string cleanLyrics = _siteSearched == LyricSite.AzLyrics
                                ? rawLyrics.Remove(rawLyrics.IndexOf("<!--"), rawLyrics.IndexOf("-->") + 3)
                                : rawLyrics;
                            if (!System.IO.File.Exists(filePath))
                                return;
                            using (File fiel = File.Create(filePath))
                            {
                                FoundLyrics(filePath);
                                fiel.Tag.Lyrics = cleanLyrics;
                                SaveTagLibFile(fiel, filePath);
                            }
                            ScanLocalLyrics();
                        }
                    };
                    break;
                }
            };
            LyricsBrowser.Source = searchString;
        }

        //private void FindSongLyrics(string certainSong = "", LyricSite site = LyricSite.MusixMatch)
        //{
        //    _index = 0;
        //    LoadLyricExceptions();
        //    WebView searchView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
        //    if (certainSong != "")
        //    {
        //        _songsWithoutLyrics = new List<string> { certainSong };
        //    }
        //    else
        //        _songsWithoutLyrics = _files.Where(x => !_lyricsException.Contains(x)).Where(x =>
        //        {
        //            using (var fiel = File.Create(x))
        //            {
        //                return fiel.Tag.Lyrics == null || fiel.Tag.Lyrics.Length < 20;
        //            }
        //        }).ToList();
        //    if (!_songsWithoutLyrics.Any())
        //    {
        //        ScanLocalLyrics();
        //        return;
        //    }
        //    string metafixed = new TrontorMp3File(certainSong == "" ? _songsWithoutLyrics[0] : certainSong).MetaFixFilename(false);
        //    searchView.LoadingFrameComplete += SearchView_LoadingFrameComplete;
        //    searchView.Source = LyricSearchString(metafixed, site);
        //}

        //private void SearchView_LoadingFrameComplete(object sender, FrameEventArgs e)
        //{
        //    if (!e.IsMainFrame) return;
        //    int maxIndex = _songsWithoutLyrics.Count - 1;
        //    try
        //    {
        //        var doc = new HtmlDocument();
        //        string html = lyricsBrowser.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML");
        //        doc.LoadHtml(html);
        //        //The results obtained through the search
        //        var searchResults = 0;
        //        if (doc.DocumentNode.InnerHtml.Contains("To continue, please"))
        //        {
        //            MessageBox.Show("Captcha!!");
        //            return;
        //        }
        //        if (doc.DocumentNode.InnerHtml.Contains("\"resultStats\""))
        //        {
        //            string amountResults =
        //                Regex.Replace(doc.DocumentNode.SelectSingleNode("//*[@id=\"resultStats\"]").InnerText,
        //                    @" ?\(.*?\)", string.Empty);

        //            int removeIndx = amountResults.IndexOf("results", StringComparison.Ordinal);
        //            if (removeIndx > 0)
        //                amountResults = amountResults.Substring(0, removeIndx);
        //            searchResults = int.Parse(Regex.Replace(amountResults, "[^0-9.]", ""));
        //        }
        //        if (string.IsNullOrEmpty(searchResults.ToString()) || searchResults == 0)
        //        {
        //            _lyricsException.Add(_songsWithoutLyrics[_index]);
        //            if (_index < maxIndex)
        //            {
        //                if (_siteSearched == LyricSite.MusixMatch)
        //                {
        //                    FindSongLyrics(LyricSearchString(new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false), LyricSite.AzLyrics).ToString());
        //                }
        //                else
        //                {
        //                    _index++;
        //                    lyricsBrowser.Source =
        //                        LyricSearchString(new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false));
        //                }
        //            }
        //            SaveLyricExceptions();
        //        }
        //        int maximumSearchIterations = searchResults > 2 ? 2 : searchResults;
        //        for (var i = 1; i <= maximumSearchIterations; i++)
        //        {
        //            HtmlNode aTag;
        //            if (searchResults == 1)
        //                aTag = doc.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/h3/a");
        //            else
        //                aTag =
        //                    doc.DocumentNode.SelectSingleNode(string.Format("//*[@id=\"rso\"]/div/div[{0}]/div/h3/a", i));
        //            string description;
        //            if (searchResults == 1)
        //                description =
        //                    doc.DocumentNode.SelectSingleNode("//*[@id=\"rso\"]/div/div/div/div/span").InnerText;
        //            else
        //                description = doc.DocumentNode.SelectSingleNode(string.Format(
        //                    "//*[@id=\"rso\"]/div/div[{0}]/div/div/div/span", i)).InnerText;
        //            if (description.Contains("Lyrics not available"))
        //                continue;
        //            string title =
        //                new TrontorMp3File(HttpUtility.HtmlDecode(
        //                    aTag.InnerText.Replace(" - A-Z Lyrics", "")
        //                        .Replace("LYRICS", "")
        //                        .Replace("| Musixmatch", "")
        //                        .Replace("lyrics", ""))).MetaFixFilename(false);
        //            string url = aTag.Attributes["href"].Value;
        //            string fixFname = new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false);
        //            double similarity = CalculateSimilarity(fixFname.ToLower(), title.ToLower()) * 100;
        //            if (similarity < 15)
        //            {
        //                return;
        //            }
        //            bool levehCheck = new SimilarityCheck(title.ToLower(), fixFname.ToLower(), true).Percentage() > 70 ||
        //                              new SimilarityCheck(title.ToLower(),
        //                                  Path.GetFileNameWithoutExtension(_songsWithoutLyrics[_index]), true)
        //                                  .Percentage() >
        //                              70;
        //            if (levehCheck ||
        //                MessageBox.Show(
        //                    "Does this look like the right title?" + Environment.NewLine +
        //                    Path.GetFileNameWithoutExtension(_songsWithoutLyrics[_index]) + Environment.NewLine + title,
        //                    "Lyric Verification", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //            {
        //                var lyricsPageBrowserLocal = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);

        //                string lyricsUrl = string.Format(url);
        //                lyricsPageBrowserLocal.Source = new Uri(lyricsUrl);
        //                lyricsPageBrowserLocal.LoadingFrameComplete += (pol, kol) =>
        //                {
        //                    if (kol.IsMainFrame)
        //                    {
        //                        var lyricsdoc = new HtmlDocument();
        //                        lyricsdoc.LoadHtml(
        //                            lyricsPageBrowserLocal.ExecuteJavascriptWithResult(
        //                                "document.getElementsByTagName('html')[0].innerHTML"));
        //                        var rawLyrics = "";
        //                        switch (_siteSearched)
        //                        {
        //                            case LyricSite.AzLyrics:
        //                                rawLyrics =
        //                                    lyricsdoc.DocumentNode.SelectSingleNode("/body/div[3]/div/div[2]/div[6]")
        //                                        .InnerText;
        //                                break;
        //                            case LyricSite.MusixMatch:
        //                                var counts =
        //                                    lyricsdoc.DocumentNode.Descendants()
        //                                        .Where(
        //                                            x =>
        //                                                x.Attributes.Contains("class") &&
        //                                                x.Attributes["class"].Value == "mxm-empty__title")
        //                                        .ToList();
        //                                if (counts.Count != 0)
        //                                {
        //                                    _index++;
        //                                    if (_songsWithoutLyrics.Count < _index - 1)
        //                                        lyricsBrowser.Source =
        //                                            LyricSearchString(
        //                                                new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(
        //                                                    false));
        //                                    return;
        //                                }
        //                                rawLyrics =
        //                                    lyricsdoc.DocumentNode.Descendants()
        //                                        .Where(
        //                                            x =>
        //                                                x.Attributes.Contains("class") &&
        //                                                x.Attributes["class"].Value == "mxm-lyrics__content")
        //                                        .ToList()[0].InnerText;
        //                                break;
        //                        }
        //                        string cleanLyrics = _siteSearched == LyricSite.AzLyrics
        //                            ? rawLyrics.Remove(rawLyrics.IndexOf("<!--"), rawLyrics.IndexOf("-->") + 3)
        //                            : rawLyrics;
        //                        if (!System.IO.File.Exists(_songsWithoutLyrics[_index]))
        //                            return;
        //                        using (var fiel = File.Create(_songsWithoutLyrics[_index]))
        //                        {
        //                            Console(
        //                                "Saving lyrics for " + fixFname + " | Loaded " +
        //                                lyricsUrl.Replace("http://www.azlyrics.com/lyrics/", "")
        //                                    .Replace("https://www.musixmatch.com/lyrics/", ""), Color.Green);

        //                            fiel.Tag.Lyrics = cleanLyrics;
        //                            SaveTagLibFile(fiel, _songsWithoutLyrics[_index]);
        //                        }
        //                        if (_index < maxIndex)
        //                        {
        //                            _index++;
        //                            lyricsBrowser.Source =
        //                                LyricSearchString(
        //                                    new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false));
        //                        }
        //                        else if (_index == maxIndex)
        //                            ScanLocalLyrics();
        //                    }
        //                };
        //                break;
        //            }
        //            if (i + 1 == maximumSearchIterations || maximumSearchIterations == 1)
        //            {
        //                _lyricsException.Add(_songsWithoutLyrics[_index]);
        //                if (_index < maxIndex)
        //                {
        //                    _index++;
        //                    lyricsBrowser.Source =
        //                        LyricSearchString(new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false));
        //                }
        //                SaveLyricExceptions();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console("Caught lyrics exception :(", Color.Orange);
        //        if (_index < maxIndex && _index < _songsWithoutLyrics.Count - 1)
        //        {
        //            _index++;
        //            lyricsBrowser.Source =
        //                LyricSearchString(new TrontorMp3File(_songsWithoutLyrics[_index]).MetaFixFilename(false));
        //        }
        //    }
        //    ScanLocalLyrics();
        //}

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
                    using (File fiel = File.Create(x))
                    {
                        if (fiel.Tag.Lyrics == null)
                        {
                            if (_lyricsException.Contains(x))
                                info.Add(new SongInformation<string, string, string, DateTime>(x, "--", "No Lyrics Available",
                               System.IO.File.GetLastAccessTime(x)));
                            else
                                info.Add(new SongInformation<string, string, string, DateTime>(x, "No", "",
                                    System.IO.File.GetLastAccessTime(x)));
                        }
                        else
                            info.Add(new SongInformation<string, string, string, DateTime>(x, "Yes", fiel.Tag.Lyrics,
                                System.IO.File.GetLastAccessTime(x)));
                        return fiel.Tag.Lyrics == null || fiel.Tag.Lyrics.Length < 20;
                    }
                }).ToList();
                //foreach (string s in _lyricsException.Where(x => info.All(z => z.First != x)).Where(System.IO.File.Exists))
                //{
                //    info.Add(new SongInformation<string, string, string, DateTime>(s, "Go", "", System.IO.File.GetLastAccessTime(s)));
                //}
            }
            catch
            {
                // ignored
            }
            return info;
        }

        private void ScanLocalLyrics()
        {
            Invoke(new Action(() =>
            {
                PopulateFileList();
                int scrolloffset = lyricsView.VerticalScrollingOffset;

                lyricsView.Rows.Clear();
                List<SongInformation<string, string, string, DateTime>> information = LyricInformation();
                for (int i = 0; i < information.Count; i++)
                {
                    lyricsView.Rows.Add(Path.GetFileNameWithoutExtension(information[i].First), information[i].Second,
                        information[i].Third, information[i].Fourth);
                    if (information[i].Second == "Yes")
                        lyricsView.Rows[i].Cells[1].Style.BackColor = Color.Green;
                    else if (information[i].Second == "--")
                        lyricsView.Rows[i].Cells[1].Style.BackColor = Color.Orange;
                    else
                        lyricsView.Rows[i].Cells[1].Style.BackColor = Color.Firebrick;
                }
                foreach (DataGridViewColumn col in lyricsView.Columns)
                {
                    if (col.HeaderCell.Style.Alignment != DataGridViewContentAlignment.MiddleCenter)
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                lyricsView.Sort(lyricsView.Columns["clm_LastMod"], ListSortDirection.Descending);
                PropertyInfo verticalOffset = lyricsView.GetType()
                    .GetProperty("VerticalOffset", BindingFlags.NonPublic | BindingFlags.Instance);
                verticalOffset.SetValue(lyricsView, scrolloffset, null);
            }));
        }

        /// <summary>Returns true if the current application has focus, false otherwise</summary>
        public static bool ApplicationIsActivated()
        {
            IntPtr activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false; // No window is currently activated
            }

            int procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private void PopulateChartLayout()
        {
            _chartSongsPanel.Controls.Clear();
            _chartSongsPanel.Invalidate();
            foreach (MusicChart chart in ScrapedSongs.Where(chart => !chk_hideCheckedCharts.Checked || !chart.Checked))
            {
                _chartSongsPanel.Controls.Add(chart);
            }
            if (!_showBalloon) return;
            new Thread(() =>
            {
                foreach (MusicChart c in ScrapedSongs.Where(c => !c.Checked))
                {
                    if (!ApplicationIsActivated()) return;
                    NotificationIcon.ShowBalloonTip(5000, "New Song", c.lbl_Artist.Text + " - " + c.lbl_Title.Text,
                        ToolTipIcon.Info);
                    Thread.Sleep(5000);
                }
            }).Start();

            _chartSongsPanel.Invalidate();
            _showBalloon = false;
        }

        private void ScanCharts()
        {
            _finishedScanningAria = false;
            _finishedScanningBillboard = false;
            _finishedScanningShazam = false;
            pnl_EmptyCharts.Visible = false;
            pnlProcess.Visible = true;
            pnl_searchingCharts.Visible = true;
            ScrapedSongs.Clear();
            LoadChartDates();
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
                    pnl_EmptyCharts.Visible = _chartSongsPanel.Controls.Count == 0;
                    PopulateChartLayout();
                    Space();
                    SaveChartDates();
                }));
            })
            { IsBackground = true }.Start();
        }

        private void ScanShazamTop100()
        {
            Console("Started Scanning Shazam Charts", Color.CadetBlue);

            WebView shazamView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            browserTabs.Add(shazamView);
            string siteUrl = "http://www.shazam.com/charts/top-100/australia";
            shazamView.Source = new Uri(siteUrl);
            shazamView.LoadingFrameComplete += (s, e) =>
            {
                if (!e.IsMainFrame) return;
                int foundSounds = 0;
                string html = shazamView.HTML;
                LoadChartDictionary();
                HtmlDocument shazamDoc = new HtmlDocument();
                shazamDoc.LoadHtml(html);
                new Thread(() =>
                {
                    string tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
                    IEnumerable<HtmlNode> node =
                        shazamDoc.DocumentNode.Descendants("article")
                            .Where(
                                d =>
                                    d.Attributes.Contains("class") &&
                                    d.Attributes["class"].Value.Contains("ti__container"));

                    IEnumerable<HtmlNode> findclasses =
                        shazamDoc.DocumentNode.Descendants("div")
                            .Where(
                                d =>
                                    d.Attributes.Contains("class") &&
                                    d.Attributes["class"].Value == "flexgrid__item--large");
                    HtmlNodeCollection nodes = shazamDoc.DocumentNode.SelectNodes("flexgrid__item--large");
                    foreach (HtmlNode topkeknode in findclasses)
                    {
                        string artist = "", title = "", both = "", imgUrl = "";
                        foreach (HtmlNode nodule in topkeknode.ChildNodes[0].ChildNodes)
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
                                            nodule.ChildNodes.Where(x => x.Name == "p").ToList()[1].ChildNodes.Any(x => x.Name == "a"))
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
                        if (!_chartDates.ContainsKey(both))
                            _chartDates.Add(both, DateTime.Now);
                        List<MusicChart> enumuerable = new List<MusicChart>(ScrapedSongs);
                        bool isChecked = _chartSongs.Count(str => new SimilarityCheck(title, str, true).Percentage() > 70) > 0;
                        if (enumuerable.Any(chart => chart.ChartHasSimilar(title)))
                            continue;
                        Image img = Resources.question_sign_on_person_head;
                        using (WebClient imgClient = new WebClient())
                        {
                            try
                            {
                                tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
                                Debug.WriteLine(imgUrl);
                                imgClient.DownloadFile(imgUrl, tempPath);
                            }
                            catch (AggregateException)
                            {
                            }
                        }
                        img = Image.FromFile(tempPath);

                        MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked, _chartDates[both]);
                        Chart.Size = new Size(_chartSongsPanel.Size.Width - 25, Chart.Height);
                        Chart.btn_ClearSong.Click += (i, y) =>
                        {
                            ScrapedSongs.Remove(
                                (MusicChart)_chartSongsPanel.Controls[_chartSongsPanel.Controls.IndexOf(Chart)]);
                            if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                            SaveChartDictionary();
                            PopulateChartLayout();
                        };
                        Chart.btn_Reorder.Click +=
                            (i, y) =>
                            {
                                _chartSongsPanel.Controls.SetChildIndex(Chart, _chartSongsPanel.Controls.Count - 1);
                            };
                        Invoke(new Action(() => ScrapedSongs.Add(Chart)));
                    }
                    _finishedScanningShazam = true;
                    if (foundSounds > 0)
                        Console("Finished Scanning Shazam Charts, found:" + foundSounds, Color.Blue);
                    Invoke(new Action(() => shazamView.Dispose()));
                })
                { IsBackground = true }.Start();
            };
        }

        private void ScanAriaCharts()
        {
            string siteUrl = "http://www.ariacharts.com.au/Charts/Singles-Chart";
            WebView ariaView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            browserTabs.Add(ariaView);
            ariaView.Source = new Uri(siteUrl);
            Console("Started Scanning Aria Charts", Color.CadetBlue);
            ariaView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    LoadChartDictionary();
                    HtmlDocument
                        doc = new HtmlDocument();

                    doc.LoadHtml(
                        ariaView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        int foundSounds = 0;
                        string siteXPath = "//*[@id=\"dvChartItems\"]";
                        if (doc.DocumentNode.SelectSingleNode(siteXPath) == null)
                        {
                            Invoke(new Action(() => Console("Scanning Aria charts failed. Another scan will be performed in a minute.", Color.Red)));
                            new Thread(() =>
                            {
                                Thread.Sleep(1000);
                                ScanAriaCharts();
                            }).Start();
                            _finishedScanningAria = true;
                            return;
                        }
                        HtmlNode node =
                            doc.DocumentNode.SelectSingleNode(siteXPath)
                                .ChildNodes.Where(x => x.Name == "table")
                                .ToList()[0].ChildNodes.ToList()[1];
                        if (node != null)
                            foreach (HtmlNode itemRow in node.ChildNodes)
                            {
                                foreach (HtmlNode nodes in itemRow.ChildNodes)
                                {
                                    if (nodes.Attributes.Contains("class") &&
                                        nodes.Attributes["class"].Value.Contains("title-artist"))
                                    {
                                        string tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
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
                                        if (!_chartDates.ContainsKey(both))
                                            _chartDates.Add(both, DateTime.Now);
                                        List<MusicChart> enumuerable = new List<MusicChart>(ScrapedSongs);
                                        List<string> strred = new List<string>();
                                        bool isChecked =
                                            _chartSongs.Count(
                                                str =>
                                                    new SimilarityCheck(title, GetFixedFileName(str), true).Percentage() >
                                                    70) > 0;

                                        if (enumuerable.Any(chart => chart.ChartHasSimilar(title))) continue;
                                        foreach (
                                            HtmlNode cNode in
                                                itemRow.ChildNodes.Where(
                                                    x => x.FirstChild != null && x.FirstChild.Name == "img"))
                                        {
                                            {
                                                if (artist == "Kung")
                                                {
                                                    Debug.WriteLine("");
                                                }
                                                string url = cNode.FirstChild.Attributes["src"].Value;
                                                using (WebClient imgClient = new WebClient())
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

                                        MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked, _chartDates[both]);
                                        Chart.Size = new Size(_chartSongsPanel.Size.Width - 25, Chart.Height);
                                        Chart.btn_ClearSong.Click += (i, y) =>
                                        {
                                            ScrapedSongs.Remove(
                                                (MusicChart)
                                                    _chartSongsPanel.Controls[_chartSongsPanel.Controls.IndexOf(Chart)]);
                                            if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                                            SaveChartDictionary();
                                            PopulateChartLayout();
                                        };
                                        Chart.btn_Reorder.Click +=
                                            (i, y) =>
                                            {
                                                _chartSongsPanel.Controls.SetChildIndex(Chart,
                                                    _chartSongsPanel.Controls.Count - 1);
                                            };
                                        Invoke(new Action(() => ScrapedSongs.Add(Chart)));
                                    }
                                }
                            }
                        node = null;
                        Space();
                        Invoke(new Action(() => ariaView.Dispose()));
                        Console("Finished Scanning Aria Charts, found:" + foundSounds, Color.Red);
                        _finishedScanningAria = true;
                    })
                    { IsBackground = true }.Start();
                }
            };
        }

        private void FailedBillboardHot100()
        {
            _finishedScanningBillboard = true;
            Console("Failed to Scan the Billboard Hot 100", Color.Red);
        }

        private void ScanBillboardHot100()
        {
            string siteUrl = "http://www.billboard.com/charts/hot-100";
            WebView billboardView = WebCore.CreateWebView(1024, 768, WebViewType.Window);
            browserTabs.Add(billboardView);
            billboardView.Source = new Uri(siteUrl);
            Console("Started Scanning Billboard Hot 100", Color.CadetBlue);
            billboardView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    int foundSounds = 0;
                    LoadChartDictionary();
                    HtmlDocument
                        doc = new HtmlDocument();

                    doc.LoadHtml(
                        billboardView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        IEnumerable<HtmlNode> node =
                            doc.DocumentNode.Descendants("article")
                                .Where(
                                    d =>
                                        d.Attributes.Contains("class") &&
                                        d.Attributes["class"].Value.Contains("chart-row"));
                        foreach (HtmlNode eleArticle in node.Take(50))
                        {
                            HtmlNode eleChartRowMainDisplay =
                                eleArticle.SelectSingleNode("div[1]/div[@class='chart-row__main-display']");
                            HtmlNode eleChartRowTitle =
                                eleChartRowMainDisplay.SelectSingleNode(
                                    @"div[@class='chart-row__container']/div[@class='chart-row__title']");
                            HtmlNode eleImage = eleChartRowMainDisplay.SelectSingleNode(@"div[@class='chart-row__image']");

                            string tempPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
                            string artist;
                            if (eleChartRowTitle == null)
                            {
                                FailedBillboardHot100();
                                return;
                            }
                            if (eleChartRowTitle.SelectSingleNode(@"*[@class='chart-row__artist']") != null)
                                artist =
                                    HttpUtility.HtmlDecode(
                                        eleChartRowTitle.SelectSingleNode(@"*[@class='chart-row__artist']")
                                            .InnerText.Replace("\n", "")
                                            .Trim());
                            else return;
                            //else
                            //    artist =
                            //        HttpUtility.HtmlDecode(
                            //            eleChartrowtitle.SelectSingleNode("h3[1]").InnerText.Replace("\n", "").Trim());
                            string title =
                                HttpUtility.HtmlDecode(
                                    eleChartRowTitle.SelectSingleNode(@"*[@class='chart-row__song']")
                                        .InnerText.Replace("\n", "")
                                        .Trim());
                            string both = GetFixedFileName(artist + " - " + title);
                            foundSounds++;

                            if (!_chartDates.ContainsKey(both))
                                _chartDates.Add(both, DateTime.Now);

                            List<MusicChart> enumuerable = new List<MusicChart>(ScrapedSongs);
                            bool isChecked =
                                _chartSongs.Count(str => new SimilarityCheck(title, str, true).Percentage() > 70) > 0;
                            if (enumuerable.Any(chart => chart.ChartHasSimilar(title)))
                                continue;
                            Image img = Resources.question_sign_on_person_head;
                            if (eleImage.Attributes.Count > 1)
                            {
                                string url =
                                    eleImage.Attributes[
                                        eleImage.Attributes["style"] == null ? "data-imagesrc" : "style"].Value
                                        .Replace("background-image: url(", "").Replace(")", "");
                                using (WebClient imgClient = new WebClient())
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


                            MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44), isChecked, _chartDates[both]);
                            Chart.Size = new Size(_chartSongsPanel.Size.Width - 25, Chart.Height);
                            Chart.btn_ClearSong.Click += (i, y) =>
                            {
                                ScrapedSongs.Remove(
                                    (MusicChart)_chartSongsPanel.Controls[_chartSongsPanel.Controls.IndexOf(Chart)]);
                                if (!_chartSongs.Contains(both)) _chartSongs.Add(both);
                                SaveChartDictionary();
                                PopulateChartLayout();
                            };
                            Chart.btn_Reorder.Click +=
                                (i, y) =>
                                {
                                    _chartSongsPanel.Controls.SetChildIndex(Chart, _chartSongsPanel.Controls.Count - 1);
                                };
                            Invoke(new Action(() => ScrapedSongs.Add(Chart)));

                            eleChartRowMainDisplay = null;
                            eleChartRowTitle = null;
                            eleImage = null;
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
                _songsWithoutLyrics.Clear();
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
                PopulateFileList();

                foreach (string filePath in _files)
                {
                    Invoke(new Action(() => SetSubstatus(Path.GetFileNameWithoutExtension(filePath))));
                    if (!filePath.Contains(".ini"))
                        using (File file = File.Create(filePath))
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
                    float percentage = fileIndex / (float)fileAmount * 100;
                    System.Console.WriteLine(percentage);
                    Invoke(new Action(() =>
                    {
                        metroProgressSpinner1.Value = (int)Math.Ceiling(percentage);
                        lbl_Percentage.Text = Math.Ceiling(percentage).ToString();
                    }));
                }
                Invoke(new Action(() =>
                {
                    //FindSongLyrics();
                    ScanCharts();
                    lblResults.Text = "";
                    if (changedFiles.Any())
                    {
                        lblResults.Text += "The following names have been changed:";
                        foreach (KeyValuePair<string, string> s in changedFiles)
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
                    Invoke(new Action(() =>
                    {
                        pnlProcess.Visible = false;
                        panelControls.Visible = true;
                        pnlOutcome.Visible = true;
                    }));
                    focusMe.Focus();
                    ScanLocalLyrics();
                }));
                changedFiles = null;
            })
            { IsBackground = true }.Start();
        }

        private void PopulateFileList()
        {
            _files = Directory.GetFiles(GetMusicPath(), "*.mp3", SearchOption.TopDirectoryOnly);
        }

        private string GetMusicPath()
        {
            if (!Directory.Exists(Properties.Settings.Default.folder))
                PromptMonitorSelection();
            return Properties.Settings.Default.folder;
        }
        private bool ProcessArtwork(string filename, bool showdiag = true, bool bypassExistingCheck = false)
        {
            return filename.Contains("-") && GoogleImageSearch(Path.GetFileNameWithoutExtension(filename), showdiag, bypassExistingCheck);
        }

        private void DumpArtwork()
        {
            string spath = @"D:\Artwork Dump";

            _files = Directory.GetFiles(GetMusicPath(), "*.mp3", SearchOption.TopDirectoryOnly);
            Directory.CreateDirectory(spath);
            foreach (string filePath in _files)
            {
                using (File file = File.Create(filePath))
                {
                    if (!file.Tag.Pictures.Any()) continue;
                    using (MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data))
                    {
                        if (filePath.EndsWith(".ini.mp3")) continue;
                        string savePath = Path.Combine(spath, Path.GetFileNameWithoutExtension(filePath) + ".jpg");
                        Image img = Image.FromStream(ms);
                        img.Save(savePath);
                    }
                }
            }
        }

        private void NotificationIcon_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (WindowState != FormWindowState.Normal)
                {
                    ShowInTaskbar = !ShowInTaskbar;
                    WindowState = FormWindowState.Normal;
                }

                Activate();
            }));
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
            Invoke(
                new Action(
                    () => { AppendText(logBox, (newline ? Environment.NewLine : "") + text, color ?? Color.Black); }));
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            _allowClose = true;

            NotificationIcon.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private string FixFile(string filePath)
        {
            TrontorMp3File file = new TrontorMp3File(filePath);
            string finalName = file.FixFileName();
            string movePath = Path.Combine(GetMusicPath(), finalName + ".mp3");
            if (filePath != movePath && !System.IO.File.Exists(movePath))
            {
                _fileChangedExceptions.Add(Path.GetFileNameWithoutExtension(movePath));
                System.IO.File.Move(filePath, movePath);
            }
            return finalName;
        }

        private void PrintRow(params string[] columns)
        {
            int width = (_tableWidth - columns.Length) / columns.Length;
            string row = "|";

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

            _files = Directory.GetFiles(GetMusicPath(), "*.mp3", SearchOption.TopDirectoryOnly);
            List<string> list = new List<string>();
            List<string> titles = new List<string>();
            foreach (string filePath in _files)
            {
                if (filePath.Contains(".ini")) continue;
                using (File file = File.Create(filePath))
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
            List<string[]> strings = DivideStrings(7, list.ToArray());
            foreach (string[] strs in strings)
            {
                PrintRow(strs.ToArray());
            }
        }

        private T[] CopyPart<T>(T[] array, int index, int length)
        {
            T[] newArray = new T[length];
            Array.Copy(array, index, newArray, 0, length);
            return newArray;
        }

        private List<string[]> DivideStrings(int expectedStringsPerArray, string[] allStrings)
        {
            List<string[]> arrays = new List<string[]>();

            int arrayCount = allStrings.Length / expectedStringsPerArray;

            int elemsRemaining = allStrings.Length;
            for (int arrsRemaining = arrayCount; arrsRemaining >= 1; arrsRemaining--)
            {
                int elementCount = elemsRemaining / arrsRemaining;

                string[] array = CopyPart(allStrings, elemsRemaining - elementCount, elementCount);
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

                _files = Directory.GetFiles(GetMusicPath(), "*.mp3", SearchOption.TopDirectoryOnly);
                string baseFileName = Path.GetFileNameWithoutExtension(e.FullPath);
                if (baseFileName != null && baseFileName.Contains("-"))
                {
                    #endregion

                    string monitorTxt = string.Format("A file has been {0}. The filename ",
                        e.ChangeType.ToString().ToLower());
                    Color clr = new Color();

                    Invoke(new Action(() =>
                    {
                        switch (e.ChangeType)
                        {
                            case WatcherChangeTypes.Created:
                                monitorTxt += "is ";
                                break;
                            default:
                                monitorTxt += "was ";
                                break;
                        }
                        monitorTxt += baseFileName;
                        lblMonitorStatus.Text = monitorTxt;
                        switch (e.ChangeType)
                        {
                            case WatcherChangeTypes.Deleted:
                                clr = Color.Red;
                                ScanLocalLyrics();
                                break;
                            case WatcherChangeTypes.Created:
                                clr = Color.Green;
                                break;
                            default:
                                if (e.ChangeType == WatcherChangeTypes.Changed)
                                    clr = Color.Blue;
                                break;
                        }
                    }));
                    if (e.ChangeType == WatcherChangeTypes.Deleted)
                    {
                        Invoke(new Action(() => Console(monitorTxt, clr)));
                        return;
                    }
                    string fixedFileName = FixFile(e.FullPath);

                    if (!(_fileChangedExceptions.Contains(Path.GetFileNameWithoutExtension(e.FullPath))
                          || _fileChangedExceptions.Contains(fixedFileName)))
                    {
                        Invoke(new Action(() => Console(monitorTxt, clr)));
                        _fileChangedExceptions.Add(fixedFileName);
                    }

                    if (baseFileName != fixedFileName)
                    {
                        Console("The file ", Color.Magenta);
                        Console(baseFileName, Color.Blue, false);
                        Console(" has been changed to ", Color.Magenta, false);
                        Console(fixedFileName + ".", Color.Blue, false);
                        Space();
                    }
                    string path_fixedFile = Path.Combine(Path.GetDirectoryName(e.FullPath), fixedFileName + ".mp3");
                    ProcessArtwork(path_fixedFile);
                    FindSongLyrics(path_fixedFile);
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
                ScanLocalLyrics();
                // ignored
            }
        }

        private void ChangeMonitorPath(string pathdir)
        {
            Properties.Settings.Default.folder = pathdir;
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
                    foreach (KeyValuePair<string, string> s in commandList)
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
            commandList = null;
            _getInput = true;
            logInput.WaterMarkColor = Color.Black;
            logInput.Text = "";
        }

        public string GetFixedFileName(string name)
        {
            return new TrontorMp3File(name).MetaFixFilename(false).Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("TEMP", "mp3tagger");
            if (!Directory.Exists(PathProgramData))
                Directory.CreateDirectory(PathProgramData);
            WindowState = FormWindowState.Minimized;
            SetStatus("Initialising Application", Status.Normal, false);
            if (_filter)
                ChangeMonitorPath(@"D:\Music - Copy");
            Thread notifyThread = new Thread(delegate ()
            {
                Menu = new ContextMenu();

                Menu.MenuItems.Add(0, new MenuItem("List Artists", mnuArtists_Click));
                Menu.MenuItems.Add(1, new MenuItem("Open Folder", (o, d) => { Process.Start(PathProgramData); }));
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
            _files = Directory.GetFiles(GetMusicPath(), "*.mp3", SearchOption.TopDirectoryOnly);
            notifyThread.Start();

            SetStatus("Starting monitor component...");
            FileSystemWatcher monitor = new FileSystemWatcher(GetMusicPath(), "*.mp3") { EnableRaisingEvents = true };
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!_firstLoad)
            {
                MessageBox.Show(WindowState.ToString());
                if (WindowState == FormWindowState.Minimized)
                {
                    ShowInTaskbar = false;
                }
                ShowInTaskbar = true;
            }
            else
                _firstLoad = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //Invalidate();
            //  CenterToScreen();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            ScanCharts();
        }

        private void lyricsView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = lyricsView.HitTest(e.X, e.Y);
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
                    string filePath =
                        (string)
                            lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0]
                                .Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    Process.Start(path);
                }));
                m.MenuItems.Add(new MenuItem("Scan Artwork", (o, h) =>
                {
                    string filePath =
                        (string)
                            lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0]
                                .Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    ProcessArtwork(path, true, true);
                }));
                m.MenuItems.Add(new MenuItem("Scan Lyrics on MusixMatch (Reccommended)", (o, h) =>
                {
                    string filePath =
                        (string)
                            lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0]
                                .Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    if (System.IO.File.Exists(path))
                    {
                        FindSongLyrics(path);
                    }
                }));
                m.MenuItems.Add(new MenuItem("Scan Lyrics on AzLyrics", (o, h) =>
                {
                    string filePath =
                        (string)
                            lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0]
                                .Value;
                    string path = Path.Combine(@"D:\Music", filePath + ".mp3");
                    if (System.IO.File.Exists(path))
                    {
                        FindSongLyrics(path, LyricSite.AzLyrics);
                    }
                }));
                m.MenuItems.Add(new MenuItem("Delete Lyrics", (o, h) =>
                {
                    string filePath =
                        (string)
                            lyricsView.Rows[lyricsView.Rows.GetFirstRow(DataGridViewElementStates.Selected)].Cells[0]
                                .Value;
                    if (
                        MessageBox.Show("Are you sure you want to delete the lyrics for " + filePath + " ?",
                            "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
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

        private void LyricsBrowsersLoadingFrameComplete(object sender, FrameEventArgs e)
        {
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void tmr_ScanCharts_Tick(object sender, EventArgs e)
        {
            ScanCharts();
        }

        private void chk_hideCheckedCharts_CheckedChanged(object sender, EventArgs e)
        {
            PopulateChartLayout();
        }

        private void btn_orderCharts_Click(object sender, EventArgs e)
        {
            SortedList<string, Control> sl = new SortedList<string, Control>();
            foreach (MusicChart i in _chartSongsPanel.Controls)
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
                _chartSongsPanel.Controls.SetChildIndex(j, _chartSongsPanel.Controls.Count - 1);
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
            _chartSongsPanel.SuspendLayout();
            foreach (Control ctrl in _chartSongsPanel.Controls)
            {
                if (ctrl is MusicChart)
                    ctrl.Width = _chartSongsPanel.ClientSize.Width;
            }
            _chartSongsPanel.ResumeLayout();
        }

        private void ariaFlow_ControlAdded(object sender, ControlEventArgs e)
        {
            lbl_chartCount.Text = string.Format("{0} songs listed.", _chartSongsPanel.Controls.Count);
            if (_chartSongsPanel.Controls.Count > 0)
            {
                pnl_EmptyCharts.Visible = false;
                pnl_searchingCharts.Visible = false;
            }
        }

        private void ariaFlow_ControlRemoved(object sender, ControlEventArgs e)
        {
            lbl_chartCount.Text = string.Format("{0} songs listed.", _chartSongsPanel.Controls.Count);
            if (_chartSongsPanel.Controls.Count == 0)
            {
                pnl_EmptyCharts.Visible = true;
            }
        }

        private void logInput_Click(object sender, EventArgs e)
        {
        }

        private enum Status
        {
            Good,
            Normal,
            Bad
        }

        public class Item
        {
            [XmlAttribute]
            public string Key;

            [XmlAttribute]
            public string Value;
        }

        private void btn_orderBySeen_Click(object sender, EventArgs e)
        {
            List<MusicChart> sl = new List<MusicChart>();
            List<double> randomlyGeneratedMs = new List<double> { 0 };
            foreach (MusicChart i in _chartSongsPanel.Controls)
            {
                sl.Add(i);
            }
            _chartSongsPanel.Controls.Clear();
            IOrderedEnumerable<MusicChart> Ordered = sl.OrderByDescending(x => x.LastSeen);
            foreach (MusicChart chart in Ordered)
            {
                _chartSongsPanel.Controls.Add(chart);
            }

        }

        private void chk_hideCheckedCharts_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void btn_chooseFolder_Click(object sender, EventArgs e)
        {
            PromptMonitorSelection();
        }

        private void PromptMonitorSelection()
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                ofd.Description =
                    "Please select the folder containing the music you would like to continuously monitor!";
                ofd.ShowNewFolderButton = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.folder = ofd.SelectedPath;
                    ChangeMonitorPath(Properties.Settings.Default.folder);
                    AnalyseAllFiles();
                    Properties.Settings.Default.Save();
                }
                else if (Properties.Settings.Default.folder == "")
                {
                    MessageBox.Show("You must set a folder! Exiting application");
                    Environment.Exit(0);
                }
            }
        }

        private enum LyricSite
        {
            AzLyrics,
            MusixMatch
        }

        public struct SongInformation<TW, TX, TY, TZ>
        {
            public TW First;
            public TX Second;
            public TY Third;
            public TZ Fourth;

            public SongInformation(TW w, TX x, TY y, TZ z)
            {
                First = w;
                Second = x;
                Third = y;
                Fourth = z;
            }
        }
    }
}