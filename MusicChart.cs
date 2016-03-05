using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MP3_Auto_Tagger_GUI
{
    public partial class MusicChart : MetroUserControl
    {
        public MusicChart(string Artist, string Title, Image img)
        {
            InitializeComponent();
            lbl_Artist.Text = Artist;
            lbl_Title.Text = Title;
            image.Image = img;
        }

        public string Both()
        {
            return new TrontorMP3File(lbl_Artist.Text + " - " + lbl_Title.Text).MetaFixFilename();
        }
    }
}
