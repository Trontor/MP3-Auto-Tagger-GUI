namespace MP3_Auto_Tagger_GUI
{
    partial class LyricsDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pics = new System.Windows.Forms.PictureBox();
            this.lyrics = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pics)).BeginInit();
            this.SuspendLayout();
            // 
            // pics
            // 
            this.pics.Location = new System.Drawing.Point(23, 63);
            this.pics.Name = "pics";
            this.pics.Size = new System.Drawing.Size(256, 256);
            this.pics.TabIndex = 0;
            this.pics.TabStop = false;
            // 
            // lyrics
            // 
            this.lyrics.Location = new System.Drawing.Point(285, 63);
            this.lyrics.Multiline = true;
            this.lyrics.Name = "lyrics";
            this.lyrics.Size = new System.Drawing.Size(431, 256);
            this.lyrics.TabIndex = 1;
            this.lyrics.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LyricsDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 335);
            this.Controls.Add(this.lyrics);
            this.Controls.Add(this.pics);
            this.Name = "LyricsDisplay";
            this.Text = "LyricsDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.pics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pics;
        private System.Windows.Forms.TextBox lyrics;
    }
}