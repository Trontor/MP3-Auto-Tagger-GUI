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
            int maxDistance;
            string s1;

            public DistanceCheck(string s1, int percCorrect)
            {
                this.maxDistance = s1.Length - (int)(s1.Length * percCorrect / 100);
                this.s1 = s1.ToLower();
            }

            public bool Check(string s2)
            {
                s2 = s2.ToLower();

                int nDiagonal = s1.Length - System.Math.Min(s1.Length, s2.Length);
                int mDiagonal = s2.Length - System.Math.Min(s1.Length, s2.Length);

                if (s1.Length == 0) return s2.Length <= maxDistance;
                if (s2.Length == 0) return s1.Length <= maxDistance;

                int[,] matrix = new int[s1.Length + 1, s2.Length + 1];

                for (int i = 0; i <= s1.Length; matrix[i, 0] = i++) ;
                for (int j = 0; j <= s2.Length; matrix[0, j] = j++) ;

                int cost;

                for (int i = 1; i <= s1.Length; i++)
                {
                    for (int j = 1; j <= s2.Length; j++)
                    {
                        if (s2.Substring(j - 1, 1) == s1.Substring(i - 1, 1))
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
                        if (matrix[nDiagonal, mDiagonal] > maxDistance)
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
        public MusicChart(string Artist, string Title, Image img, bool IsChecked)
        {
            InitializeComponent();
            lbl_Artist.Text = Artist;
            lbl_Title.Text = Title;
            Checked = IsChecked;
            btn_ClearSong.Visible = !IsChecked;
            image.Image = img;
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
            string s = new TrontorMP3File(lbl_Artist.Text + " - " + lbl_Title.Text).MetaFixFilename(false).Trim();
            return new SimilarityCheck(comparisonStr, s).Percentage() > 70;
        }

        private void btn_LoadVideo_Click(object sender, EventArgs e)
        {
            Process.Start(
                $"https://www.youtube.com/results?search_query=" + lbl_Artist.Text + "+-+" + lbl_Title.Text);
        }
    }
    public class SimilarityCheck
    {
        int maxDistance;
        string s1;
        string s2;
        public SimilarityCheck(string s1, string s2)
        {
            this.s1 = s1.ToLower().Trim();
            this.s2 = s2.ToLower().Trim();
        }

        public int Percentage()
        {
            if ((s1.Length > 5 && s1.Contains(s2)) || ( s2.Length > 5 && s2.Contains(s1)))
                return 100;
            int n = s1.Length;
            int m = s2.Length;
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
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            int bigger = Math.Max(s1.Length, s2.Length);
            double pct = ((double)(bigger - d[n, m]) /bigger) * 100;
          //  Debug.WriteLine(s1 + " " + s2 + (int) pct);
            return (int)pct;
        }
    }
}
