using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
        private bool formShown = false;
        private bool _windowHidden;
        public ContextMenu Menu;
        public NotifyIcon NotificationIcon;
        private string _path = @"D:\Music";
        private bool _filter;
        private string[] _files;

        private int _tableWidth = 77;

        private int _processedArtwork;

        public Form1()
        {
            InitializeComponent();
            ChangeMonitorPath(_path);
        }

        enum Status
        {
            Good,
            Normal,
            Bad
        }

        private void SetStatus(string status, Status statusType = Status.Normal)
        {
            Color clr = Color.PapayaWhip;
            switch (statusType)
            {
                case Status.Bad:
                    clr = Color.Red;
                    break;
                case Status.Good:
                    clr = Color.Green;
                    break;
            }
            Console(clr, status);
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
                    clr = Color.LawnGreen;
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

        private static List<string> _ariaChartSongs = new List<string>();
        private static void SaveAriaDictionary()
        {
            var xElem = new XElement(
                "items",
                _ariaChartSongs.Select(x => new XElement("item", x)));
            string xml = xElem.ToString();
            xElem.Save("ChartSongs.xml");
        }

        private static void LoadChartDictionary()
        {
            var xElem2 = new XElement("items");
            if (System.IO.File.Exists("ChartSongs.xml"))
                xElem2 = XElement.Load("ChartSongs.xml");
            else
                xElem2.Save("ChartSongs.xml");
            _ariaChartSongs = xElem2.Descendants("item").Select(x => x.Value).ToList();
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

        private bool GoogleImageSearch(string query, bool showdialog = false)
        {
            string entitized = HttpUtility.UrlEncode(query);
            string url = string.Format("https://www.google.com.au/search?q={0}&tbm=isch",
                entitized + " song cover artwork");
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
                    client.Headers.Add("user-agent", "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
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
                    AddArtwork(Path.Combine(_path, query + ".mp3"), tempPath);
                    return true;
                }
                return false;
            }
        }

        private string RetreiveLyrics(string filePath)
        {
            string lyrics = "";
            TrontorMP3File file = new TrontorMP3File(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileTitle = fileName.Split('-')[0].ToLower();
            string fileArtist = fileName.Split('-')[1].ToLower();
            string entitized = HttpUtility.UrlEncode(fileArtist + " - " + fileTitle);
            string url = string.Format("https://www.google.com.au/search?&q={0}+site%3Aazlyrics.com", entitized);
            using (var client = new WebClient()) // WebClient class inherits IDisposable
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
                string htmlCode = client.DownloadString(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);
                var resultNodes =
                    doc.DocumentNode.SelectSingleNode("//*[@id=\"universal\"]")
                        .ChildNodes;

                var htmlNodes = (IList<HtmlNode>)resultNodes ?? resultNodes.ToList();
                for (var index = 0; index < htmlNodes.Count(); index++)
                {
                    var innerNode = htmlNodes[index].ChildNodes[0];
                    string searchTitle = innerNode.ChildNodes[0].InnerText.ToLower();
                    string azLyricsUrl = innerNode.ChildNodes[0].Attributes["href"].Value.Replace("/url?q=", "");
                    if (searchTitle.Contains(fileArtist) && searchTitle.Contains(fileTitle))
                    {
                        Thread.Sleep(1000);
                        using (var webclient = new WebClient()) // WebClient class inherits IDisposable
                        {
                            string lyricsHtml = webclient.DownloadString(azLyricsUrl.Split('&')[0]);
                            var lyricsDoc = new HtmlDocument();
                            lyricsDoc.LoadHtml(lyricsHtml);
                            var lyricNodes = lyricsDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[2]/div[6]");
                            lyrics = lyricNodes.InnerText.Replace(
                                    @"\r\n<!-- Usage of azlyrics.com content by any third-party lyrics provider is prohibited by our licensing agreement. Sorry about that. -->\r\n",
                                    "");
                        }
                    }
                }
            }
            return lyrics;
        }

        public bool AddArtwork(string filePath, string imgPath)
        {
            using (var file = File.Create(filePath))
            {
                IPicture artwork = new Picture(imgPath);
                Image currentImage;
                Image newImage;
                long currentSize = 0, newSize = 0;
                if (file.Tag.Pictures.Any())
                {
                    IPicture currentArtwork = file.Tag.Pictures[0];
                    using (var ms = new MemoryStream(currentArtwork.Data.Data))
                    {
                        currentImage = Image.FromStream(ms);
                        currentSize = ms.Length;
                    }
                    using (var ms = new MemoryStream(artwork.Data.Data))
                    {
                        newImage = Image.FromStream(ms);
                        newSize = ms.Length;
                    }
                }
                file.Tag.Pictures = new IPicture[1] { artwork };
                file.Save();
                return (currentSize == newSize);
            }
        }

        private void ScanAriaCharts()
        {
            string siteUrl = "http://www.ariacharts.com.au/Charts/Singles-Chart";
            WebView ariaView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            ariaView.Source = new Uri(siteUrl);
            ariaView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    LoadChartDictionary();
                    var
                        doc = new HtmlDocument();

                    doc.LoadHtml(ariaView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        string site_XPath = "//*[@id=\"dvChartItems\"]";
                        var node = doc.DocumentNode.SelectSingleNode(site_XPath);
                        if (node != null)
                            for (var index = 0; index < node.ChildNodes.Count; index++)
                            {
                                var itemRow = node.ChildNodes[index];
                                foreach (var nodes in itemRow.ChildNodes)
                                {
                                    if (nodes.Attributes["class"].Value.Contains("title-artist"))
                                    {
                                        string tempPath = Path.GetTempFileName();
                                        string artist = HttpUtility.HtmlDecode(nodes.ChildNodes[1].InnerText);
                                        string title = HttpUtility.HtmlDecode(nodes.ChildNodes[0].InnerText);
                                        string both = GetFixedFileName(artist + " - " + title);
                                        var enumuerable = ariaFlow.Controls.OfType<MusicChart>();
                                        if (_ariaChartSongs.Contains(both) || enumuerable.Any(x => x.Both() == both)) continue;
                                        if (
                                            itemRow.ChildNodes.Count(
                                                x => x.Attributes["class"].Value.Contains("title-image")) == 1)
                                        {
                                            string URL =
                                                itemRow.ChildNodes.Where(
                                                    x => x.Attributes["class"].Value.Contains("title-image"))
                                                    .ToArray()[
                                                        0]
                                                    .ChildNodes[0].ChildNodes[0].Attributes["src"].Value;
                                            using (var imgClient = new WebClient())
                                            // WebClient class inherits IDisposable
                                            {
                                                try
                                                {
                                                    Do(() => imgClient.DownloadFile(URL, tempPath),
                                                        TimeSpan.FromSeconds(1));
                                                }
                                                catch (AggregateException)
                                                {
                                                }
                                            }
                                        }
                                        Image img = Image.FromFile(tempPath);


                                        MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44));
                                        Chart.Size = new Size(ariaFlow.Size.Width - 25, Chart.Height);
                                        Chart.btn_ClearSong.Click += (i, y) =>
                                        {
                                            ariaFlow.Controls.Remove(ariaFlow.Controls[ariaFlow.Controls.IndexOf(Chart)]);
                                            _ariaChartSongs.Add(both);
                                            SaveAriaDictionary();
                                        };
                                        Chart.btn_Reorder.Click += (i, y) =>
                                        {
                                            ariaFlow.Controls.SetChildIndex(Chart, ariaFlow.Controls.Count - 1);
                                        };
                                        Chart.btn_LoadVideo.Click += (i, y) =>
                                        {
                                            Process.Start(
                                                $"https://www.youtube.com/results?search_query={artist}+-+{title}");
                                        };
                                        Invoke(new Action(() => ariaFlow.Controls.Add(Chart)));
                                    }
                                }
                            }
                        Debug.WriteLine("Finished Scanning Aria Charts");
                    }).Start();
                }
            };
        }


        private void ScanBillboardHot100()
        {
            string siteUrl = "http://www.billboard.com/charts/hot-100";
            WebView ariaView = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            ariaView.Source = new Uri(siteUrl);
            ariaView.LoadingFrameComplete += (s, e) =>
            {
                if (e.IsMainFrame)
                {
                    LoadChartDictionary();
                    var
                        doc = new HtmlDocument();

                    doc.LoadHtml(ariaView.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML"));
                    new Thread(() =>
                    {
                        var node = doc.DocumentNode.Descendants("article").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("chart-row"));
                        foreach (var ele_Article in node.Take(20))
                        {
                            HtmlNode ele_chartrow__primary = ele_Article.SelectSingleNode("div[1]");
                            HtmlNode ele_chartrowtitle = ele_chartrow__primary.SelectSingleNode("div[@class='chart-row__title']");
                            HtmlNode ele_image = ele_chartrow__primary.SelectSingleNode("div[@class='chart-row__image']");

                            string tempPath = Path.GetTempFileName();
                            string artist;
                            if (ele_chartrowtitle.SelectSingleNode("h3[1]").ChildNodes.Count > 1)
                                artist = HttpUtility.HtmlDecode(ele_chartrowtitle.SelectSingleNode("h3[1]").SelectSingleNode("a").InnerText.Replace("\n", "").Trim());
                            else
                                artist = HttpUtility.HtmlDecode(ele_chartrowtitle.SelectSingleNode("h3[1]").InnerText.Replace("\n", "").Trim());
                            string title = HttpUtility.HtmlDecode(ele_chartrowtitle.SelectSingleNode("h2[1]").InnerText.Replace("\n", "").Trim());
                            string both = GetFixedFileName(artist + " - " + title);
                            if (_ariaChartSongs.Contains(both)) continue;
                            Image img = Properties.Resources.question_sign_on_person_head; ;
                            if (ele_image.Attributes.Count > 1)
                            {
                                string URL =
                                    ele_image.Attributes[
                                        ele_image.Attributes["style"] == null ? "data-imagesrc" : "style"].Value
                                        .Replace("background-image: url(", "").Replace(")", "");
                                using (var imgClient = new WebClient())
                                // WebClient class inherits IDisposable
                                {
                                    try
                                    {
                                        Do(() => imgClient.DownloadFile(URL, tempPath),
                                            TimeSpan.FromSeconds(1));
                                    }
                                    catch (AggregateException)
                                    {
                                    }
                                }
                                img = Image.FromFile(tempPath);
                            }


                            MusicChart Chart = new MusicChart(artist, title, ResizeImage(img, 44, 44));
                            Chart.Size = new Size(ariaFlow.Size.Width - 25, Chart.Height);
                            Chart.btn_ClearSong.Click += (i, y) =>
                            {
                                ariaFlow.Controls.Remove(ariaFlow.Controls[ariaFlow.Controls.IndexOf(Chart)]);
                                _ariaChartSongs.Add(both);
                                SaveAriaDictionary();
                            };
                            Chart.btn_Reorder.Click += (i, y) =>
                            {
                                ariaFlow.Controls.SetChildIndex(Chart, ariaFlow.Controls.Count - 1);
                            };
                            Chart.btn_LoadVideo.Click += (i, y) =>
                            {
                                Process.Start(
                                    $"https://www.youtube.com/results?search_query={artist}+-+{title}");
                            };
                            Invoke(new Action(() => ariaFlow.Controls.Add(Chart)));
                        }
                        Debug.WriteLine("Finished Scanning Billboard Charts");
                    }).Start();
                }
            };
        }

        private void AnalyseAllFiles()
        {
            new Thread(() =>
            {
                Invoke(new Action(() => SetStatus("Analysing music files...")));
                Dictionary<string, string> changedFiles = new Dictionary<string, string>();
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
                                }).Start();
                                Thread.Sleep(1000);
                            }
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
                    lblResults.Text = "";
                    if (changedFiles.Any())
                    {
                        lblResults.Text += "The following names have been changed:";
                        foreach (var s in changedFiles)
                        {
                            lblResults.Text += Environment.NewLine + "Old: " + Path.GetFileNameWithoutExtension(s.Key) + ", ";

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
                }));
            }).Start();
        }

        private bool ProcessArtwork(string filename, bool showdiag = false)
        {
            if (filename.Contains("-"))
            {
                int fileCount = _files.Count();
                return GoogleImageSearch(Path.GetFileNameWithoutExtension(filename), showdiag);
            }
            return false;
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
            System.Console.WriteLine("");
        }

        public static void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public void Console(Color color, string text, bool newline = true)
        {
            AppendText(logBox, text + (newline ? Environment.NewLine : ""), color);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            NotificationIcon.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        private string FixFile(string filePath)
        {
            if (filePath.Contains("summer"))
            {
            }
            var file = new TrontorMP3File(filePath);
            string finalName = file.FixFileName();
            string movePath = Path.Combine(_path, finalName + ".mp3");
            if (filePath != movePath && !System.IO.File.Exists(movePath))
                System.IO.File.Move(filePath, movePath);
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

        private string AlignCentre(string text, int width)
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
                        Console(Color.Yellow, "Duplicate Title:" + filePath);
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
                _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
                string fixedFile = FixFile(e.FullPath);
                Invoke(new Action(() =>
                {
                    lblMonitorStatus.Text = "File Change Detected. See Process Log for details";
                }));

                new Thread(() =>
                {
                    Thread.Sleep(5000);
                    Invoke(new Action(() =>
                    {
                        lblMonitorStatus.Text = "Waiting for file changes...";
                    }));
                }).Start();
                string oldFile = Path.GetFileNameWithoutExtension(e.FullPath);
                if (oldFile != fixedFile)
                {
                    Console(Color.Magenta, "The following files has been detected and modified:");
                    Console(Color.DarkGreen, "Old: " + oldFile + ", ", true);
                    Console(Color.Green, "New: " + fixedFile);
                    Space();
                }
                if (ProcessArtwork(e.FullPath, true))
                {
                    Console(Color.Magenta, "The following file artwork has been detected and modified:");
                    Console(Color.Green, "File name: " + fixedFile);
                    Space();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private string str_Input = " ";
        private bool getInput = false;
        private string GetConsoleInput()
        {
            Task t = Task.Factory.StartNew(() =>
                {
                    while (!getInput)
                    {
                    }
                    getInput = false;

                });
            t.Wait();
            return str_Input;
        }

        private void ChangeMonitorPath(string pathdir)
        {
            _path = pathdir;
            lblMonitoringDirectory.Text = pathdir;
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}
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
                    Console(Color.PapayaWhip, "Filter has been enabled.");
                    break;
                case "regular":
                    _filter = false;
                    ChangeMonitorPath(@"D:\Music");
                    Console(Color.PapayaWhip, "Filter has been disabled.");
                    break;
                case "analyse":
                    AnalyseAllFiles();
                    break;
                case "dump artwork":
                    DumpArtwork();
                    break;
                case "help":
                    Console(Color.Green, "Here's a list of available commands:");
                    foreach (var s in commandList)
                    {
                        Console(Color.PaleVioletRed, s.Key, false);

                        Console(Color.White, " : " + s.Value);
                    }
                    break;
                default:
                    Console(Color.Red, "Command: " + logInput.Text + " not recognised :^(");
                    Console(Color.PapayaWhip, "Type 'help' for a command list.");
                    break;
            }
            str_Input = logInput.Text.Trim();
            getInput = true;
            logInput.WaterMarkColor = Color.White;
            logInput.Text = "";
        }

        public string GetFixedFileName(string name)
        {
            return new TrontorMP3File(name).MetaFixFilename();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            SetStatus("Initialising Application");
            if (_filter)
                ChangeMonitorPath(@"D:\Music - Copy");
            _windowHidden = true;
            var notifyThread = new Thread(
                delegate ()
                {
                    Menu = new ContextMenu();

                    Menu.MenuItems.Add(0, new MenuItem("List Artists", mnuArtists_Click));
                    Menu.MenuItems.Add(1, new MenuItem("Exit", mnuExit_Click));

                    NotificationIcon = new NotifyIcon
                    {
                        Icon = Resources.mp3,
                        ContextMenu = Menu,
                        Text = "MP3 File Monitor -- Made for Rohyl, by Rohyl"
                    };
                    NotificationIcon.DoubleClick += NotificationIcon_Click;

                    NotificationIcon.Visible = true;
                    Application.Run();
                }
                );
            _files = Directory.GetFiles(_path, "*.mp3", SearchOption.TopDirectoryOnly);
            notifyThread.Start();

            SetStatus("Starting monitor component...");
            var monitor = new FileSystemWatcher(_path, "*.mp3") { EnableRaisingEvents = true };
            monitor.Created += monitor_CreatedOrChanged;
            monitor.Renamed += monitor_CreatedOrChanged;
            SetStatus("Preparing music file analysis");
            AnalyseAllFiles();
            ScanAriaCharts();
            ScanBillboardHot100();
            timer1.Enabled = true;
            RunConsoleCommand("help");
        }

        private void btn_RescanAll_Click(object sender, EventArgs e)
        {
            AnalyseAllFiles();
        }
        bool firstLoad = true;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!firstLoad)
            {
                MessageBox.Show(this.WindowState.ToString());
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = false;
                }
                this.ShowInTaskbar = true;
            }
            else
                firstLoad = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void Awesomium_Windows_Forms_WebControl_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            CenterToScreen();
        }
    }
}
