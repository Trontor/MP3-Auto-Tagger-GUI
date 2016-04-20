using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MP3_Auto_Tagger_GUI
{
    public partial class MusicChart : MetroUserControl
    {
        public MusicChart(string Artist, string Title, Image img,  bool IsChecked)
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

        public bool HasSimilar(string comparisonStr)
        {
            string s = new TrontorMP3File(lbl_Artist.Text + " - " + lbl_Title.Text).MetaFixFilename().Trim(); 
            return new DistanceCheck(comparisonStr, 70).Check(s);
        }

        private void btn_LoadVideo_Click(object sender, EventArgs e)
        {
            Process.Start(
                $"https://www.youtube.com/results?search_query=" + lbl_Artist.Text + "+-+" + lbl_Title.Text);
        }
    }
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
}
