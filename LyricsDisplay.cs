using System.Drawing;
using System.Windows.Forms;

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
