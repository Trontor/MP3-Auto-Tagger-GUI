using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MP3_Auto_Tagger_GUI
{
    public partial class LyricsDisplay : Form
    {
        public LyricsDisplay(Image artwurk, string felnem, string lirics)
        {
            InitializeComponent();
            pics.Image = artwurk;
            Text = felnem;
            lyrics.Text = lirics;
        }
    }
}
