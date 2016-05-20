using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MP3_Auto_Tagger_GUI
{
    public partial class MusicChart : MetroUserControl
    {
        public class DistanceCheck
        {
            int _maxDistance;
            string _s1;

            public DistanceCheck(string s1, int percCorrect)
            {
                this._maxDistance = s1.Length - (int)(s1.Length * percCorrect / 100);
                this._s1 = s1.ToLower();
            }

            public bool Check(string s2)
            {
                s2 = s2.ToLower();

                int nDiagonal = _s1.Length - System.Math.Min(_s1.Length, s2.Length);
                int mDiagonal = s2.Length - System.Math.Min(_s1.Length, s2.Length);

                if (_s1.Length == 0) return s2.Length <= _maxDistance;
                if (s2.Length == 0) return _s1.Length <= _maxDistance;

                int[,] matrix = new int[_s1.Length + 1, s2.Length + 1];

                for (int i = 0; i <= _s1.Length; matrix[i, 0] = i++) ;
                for (int j = 0; j <= s2.Length; matrix[0, j] = j++) ;

                int cost;

                for (int i = 1; i <= _s1.Length; i++)
                {
                    for (int j = 1; j <= s2.Length; j++)
                    {
                        if (s2.Substring(j - 1, 1) == _s1.Substring(i - 1, 1))
                        {
                            cost = 0;
                        }
                        else {
                            cost = 1;
                        }

                        int valueAbove = matrix[i - 1, j];
                        int valueLeft = matrix[i, j - 1] + 1;
                        int valueAboveLeft = matrix[i - 1, j - 1];
                        matrix[i, j] = Min(valueAbove + 1, valueLeft + 1, valueAboveLeft + cost);
                    }

                    if (i >= nDiagonal)
                    {
                        if (matrix[nDiagonal, mDiagonal] > _maxDistance)
                        {
                            return false;
                        }
                        else
                        {
                            nDiagonal++;
                            mDiagonal++;
                        }
                    }
                }

                return true;
            }

            private int Min(int n1, int n2, int n3)
            {
                return System.Math.Min(n1, System.Math.Min(n2, n3));
            }
        }


        public DateTime LastSeen { get; set; }
        public MusicChart(string artist, string title, Image img, bool isChecked, DateTime lastSeen)
        {
            InitializeComponent();
            lbl_Artist.Text = artist;
            lbl_Title.Text = title;
            Checked = isChecked;
            btn_ClearSong.Visible = !isChecked;
            image.Image = img;
            LastSeen = lastSeen;
            label1.Text = (DateTime.Now - lastSeen).Days.ToString();
        }

        public Image SongImage
        {
            set
            {
                image.Image = value;
            }
        }

        public bool Checked { get; set; }

        public bool ChartHasSimilar(string comparisonStr)
        {
            string s = new TrontorMp3File(lbl_Artist.Text + " - " + lbl_Title.Text).MetaFixFilename(false).Trim();
            var splitParts = s.Split('-'); 
            var percentage = 0;
            percentage = splitParts.Length > 2
                ? Math.Max(new SimilarityCheck(comparisonStr, splitParts[1]).Percentage(),
                    new SimilarityCheck(comparisonStr, splitParts[2]).Percentage())
                : new SimilarityCheck(comparisonStr, splitParts[1]).Percentage();

            return percentage > 70;
        }

        private void btn_LoadVideo_Click(object sender, EventArgs e)
        {
            Process.Start(
                $"https://www.youtube.com/results?search_query=" + lbl_Artist.Text + "+-+" + lbl_Title.Text);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {


        }

        private void btn_ClearSong_Click(object sender, EventArgs e)
        {

        }
    }
    public class SimilarityCheck
    {
        int _maxDistance;
        string _s1;
        string _s2;
        bool _splitHeifenAndReturnLargest = false;
        public SimilarityCheck(string s1, string s2, bool splitF2AndReturnLargest = false)
        {
            this._s1 = s1.ToLower().Trim();
            this._s2 = s2.ToLower().Trim();
            _splitHeifenAndReturnLargest = splitF2AndReturnLargest;
        }

        public int Percentage()
        {
            var splitParts = _s2.Split('-');
            var percentage = 0;
            if (splitParts.Length > 2)
            {
                _s2 = splitParts[1];
                if (CalculatePercentage() > percentage)
                    percentage = CalculatePercentage();
                _s2 = splitParts[2];
                if (CalculatePercentage() > percentage)
                    percentage = CalculatePercentage();
            }
            else if (splitParts.Length == 1)
            {
                return CalculatePercentage();
            }
            else
            {
                _s2 = splitParts[1];
                if (CalculatePercentage() > percentage)
                    percentage = CalculatePercentage();
            }
            return percentage; 
        }

        public int CalculatePercentage()
        {
            if ((_s1.Length > 5 && _s1.Contains(_s2)) || (_s2.Length > 5 && _s2.Contains(_s1)))
                return 100;
            int n = _s1.Length;
            int m = _s2.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (_s2[j - 1] == _s1[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            int bigger = Math.Max(_s1.Length, _s2.Length);
            double pct = ((double)(bigger - d[n, m]) / bigger) * 100;
            //  Debug.WriteLine(s1 + " " + s2 + (int) pct);
            return (int)pct;
        }
    }
}
