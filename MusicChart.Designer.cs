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
            this.btn_LoadVideo = new System.Windows.Forms.Button();
            this.btn_Reorder = new MetroFramework.Controls.MetroButton();
            this.image = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Artist = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_ClearSong = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btn_LoadVideo, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Reorder, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.image, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ClearSong, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(0, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 48);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btn_LoadVideo
            // 
            this.btn_LoadVideo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_LoadVideo.BackColor = System.Drawing.Color.White;
            this.btn_LoadVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_LoadVideo.BackgroundImage")));
            this.btn_LoadVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_LoadVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadVideo.ForeColor = System.Drawing.Color.LightGray;
            this.btn_LoadVideo.Location = new System.Drawing.Point(362, 5);
            this.btn_LoadVideo.Name = "btn_LoadVideo";
            this.btn_LoadVideo.Size = new System.Drawing.Size(40, 38);
            this.btn_LoadVideo.TabIndex = 14;
            this.btn_LoadVideo.UseVisualStyleBackColor = false;
            this.btn_LoadVideo.Click += new System.EventHandler(this.btn_LoadVideo_Click);
            // 
            // btn_Reorder
            // 
            this.btn_Reorder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Reorder.BackColor = System.Drawing.Color.White;
            this.btn_Reorder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Reorder.BackgroundImage")));
            this.btn_Reorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Reorder.Location = new System.Drawing.Point(411, 5);
            this.btn_Reorder.Name = "btn_Reorder";
            this.btn_Reorder.Size = new System.Drawing.Size(16, 38);
            this.btn_Reorder.TabIndex = 12;
            this.btn_Reorder.UseSelectable = true;
            this.btn_Reorder.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // image
            // 
            this.image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image.Location = new System.Drawing.Point(3, 3);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(44, 42);
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
            this.panel1.Size = new System.Drawing.Size(203, 42);
            this.panel1.TabIndex = 5;
            // 
            // lbl_Artist
            // 
            this.lbl_Artist.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_Artist.Font = new System.Drawing.Font("Segoe UI", 7.75F);
            this.lbl_Artist.ForeColor = System.Drawing.Color.Silver;
            this.lbl_Artist.Location = new System.Drawing.Point(0, 27);
            this.lbl_Artist.Name = "lbl_Artist";
            this.lbl_Artist.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_Artist.Size = new System.Drawing.Size(203, 15);
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
            this.lbl_Title.Size = new System.Drawing.Size(203, 29);
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
            this.btn_ClearSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearSong.ForeColor = System.Drawing.Color.LightGray;
            this.btn_ClearSong.Location = new System.Drawing.Point(315, 5);
            this.btn_ClearSong.Name = "btn_ClearSong";
            this.btn_ClearSong.Size = new System.Drawing.Size(33, 38);
            this.btn_ClearSong.TabIndex = 6;
            this.btn_ClearSong.UseVisualStyleBackColor = false;
            this.btn_ClearSong.Click += new System.EventHandler(this.btn_ClearSong_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(262, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(42, 42);
            this.panel2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 11);
            this.label3.TabIndex = 16;
            this.label3.Text = "days ago";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Seen";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "60";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MusicChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.Chocolate;
            this.Name = "MusicChart";
            this.Size = new System.Drawing.Size(432, 48);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label lbl_Title;
        public System.Windows.Forms.Label lbl_Artist;
        public System.Windows.Forms.Button btn_ClearSong;
        public MetroFramework.Controls.MetroButton btn_Reorder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btn_LoadVideo;
    }
}
