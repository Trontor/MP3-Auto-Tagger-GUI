namespace MP3_Auto_Tagger_GUI
{
    partial class MusicChart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicChart));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.image = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Artist = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_ClearSong = new MetroFramework.Controls.MetroButton();
            this.btn_LoadVideo = new MetroFramework.Controls.MetroButton();
            this.btn_Reorder = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Reorder, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_LoadVideo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.image, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ClearSong, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 50);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // image
            // 
            this.image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image.Location = new System.Drawing.Point(3, 3);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(44, 44);
            this.image.TabIndex = 0;
            this.image.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbl_Artist);
            this.panel1.Controls.Add(this.lbl_Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(53, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 44);
            this.panel1.TabIndex = 5;
            // 
            // lbl_Artist
            // 
            this.lbl_Artist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_Artist.Font = new System.Drawing.Font("Segoe UI", 7.75F);
            this.lbl_Artist.ForeColor = System.Drawing.Color.Silver;
            this.lbl_Artist.Location = new System.Drawing.Point(0, 29);
            this.lbl_Artist.Name = "lbl_Artist";
            this.lbl_Artist.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_Artist.Size = new System.Drawing.Size(253, 15);
            this.lbl_Artist.TabIndex = 3;
            this.lbl_Artist.Text = "Artist";
            this.lbl_Artist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Title
            // 
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI Semibold", 12.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(253, 29);
            this.lbl_Title.TabIndex = 2;
            this.lbl_Title.Text = "7 Years";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_ClearSong
            // 
            this.btn_ClearSong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_ClearSong.BackColor = System.Drawing.Color.White;
            this.btn_ClearSong.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearSong.BackgroundImage")));
            this.btn_ClearSong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ClearSong.Location = new System.Drawing.Point(364, 6);
            this.btn_ClearSong.Name = "btn_ClearSong";
            this.btn_ClearSong.Size = new System.Drawing.Size(40, 38);
            this.btn_ClearSong.TabIndex = 6;
            this.btn_ClearSong.UseSelectable = true;
            // 
            // btn_LoadVideo
            // 
            this.btn_LoadVideo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_LoadVideo.BackColor = System.Drawing.Color.White;
            this.btn_LoadVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_LoadVideo.BackgroundImage")));
            this.btn_LoadVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_LoadVideo.Location = new System.Drawing.Point(314, 6);
            this.btn_LoadVideo.Name = "btn_LoadVideo";
            this.btn_LoadVideo.Size = new System.Drawing.Size(40, 38);
            this.btn_LoadVideo.TabIndex = 9;
            this.btn_LoadVideo.UseSelectable = true;
            // 
            // btn_Reorder
            // 
            this.btn_Reorder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Reorder.BackColor = System.Drawing.Color.White;
            this.btn_Reorder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Reorder.BackgroundImage")));
            this.btn_Reorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Reorder.Location = new System.Drawing.Point(413, 6);
            this.btn_Reorder.Name = "btn_Reorder";
            this.btn_Reorder.Size = new System.Drawing.Size(16, 38);
            this.btn_Reorder.TabIndex = 10;
            this.btn_Reorder.UseSelectable = true;
            // 
            // MusicChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MusicChart";
            this.Size = new System.Drawing.Size(434, 50);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Artist;
        private System.Windows.Forms.Label lbl_Title;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public MetroFramework.Controls.MetroButton btn_LoadVideo;
        public MetroFramework.Controls.MetroButton btn_ClearSong;
        public MetroFramework.Controls.MetroButton btn_Reorder;
    }
}
