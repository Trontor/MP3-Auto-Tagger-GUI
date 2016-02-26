namespace MP3_Auto_Tagger_GUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.lbl_FileStatus = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.logInput = new MetroFramework.Controls.MetroTextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.lblSubstatus = new MetroFramework.Controls.MetroLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Controls.Add(this.metroTabPage5);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(460, 320);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.metroProgressSpinner1);
            this.metroTabPage1.Controls.Add(this.lblSubstatus);
            this.metroTabPage1.Controls.Add(this.lbl_FileStatus);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(452, 278);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Status";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            this.metroTabPage1.Click += new System.EventHandler(this.metroTabPage1_Click);
            // 
            // lbl_FileStatus
            // 
            this.lbl_FileStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_FileStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_FileStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_FileStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_FileStatus.Location = new System.Drawing.Point(0, 0);
            this.lbl_FileStatus.Name = "lbl_FileStatus";
            this.lbl_FileStatus.Size = new System.Drawing.Size(452, 39);
            this.lbl_FileStatus.TabIndex = 2;
            this.lbl_FileStatus.Text = "Searching Files";
            this.lbl_FileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_FileStatus.UseCustomForeColor = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(452, 278);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "File Names";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(452, 278);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Artwork";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 10;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(452, 278);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Aria Charts";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 10;
            // 
            // metroTabPage5
            // 
            this.metroTabPage5.Controls.Add(this.logInput);
            this.metroTabPage5.Controls.Add(this.logBox);
            this.metroTabPage5.HorizontalScrollbarBarColor = true;
            this.metroTabPage5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.HorizontalScrollbarSize = 10;
            this.metroTabPage5.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage5.Name = "metroTabPage5";
            this.metroTabPage5.Size = new System.Drawing.Size(452, 278);
            this.metroTabPage5.TabIndex = 4;
            this.metroTabPage5.Text = "Process Log";
            this.metroTabPage5.VerticalScrollbarBarColor = true;
            this.metroTabPage5.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.VerticalScrollbarSize = 10;
            // 
            // logInput
            // 
            // 
            // 
            // 
            this.logInput.CustomButton.Image = null;
            this.logInput.CustomButton.Location = new System.Drawing.Point(430, 1);
            this.logInput.CustomButton.Name = "";
            this.logInput.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.logInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.logInput.CustomButton.TabIndex = 1;
            this.logInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.logInput.CustomButton.UseSelectable = true;
            this.logInput.CustomButton.Visible = false;
            this.logInput.Lines = new string[0];
            this.logInput.Location = new System.Drawing.Point(0, 252);
            this.logInput.MaxLength = 32767;
            this.logInput.Name = "logInput";
            this.logInput.PasswordChar = '\0';
            this.logInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.logInput.SelectedText = "";
            this.logInput.SelectionLength = 0;
            this.logInput.SelectionStart = 0;
            this.logInput.Size = new System.Drawing.Size(452, 23);
            this.logInput.TabIndex = 1;
            this.logInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.logInput.UseSelectable = true;
            this.logInput.WaterMark = "Console Input";
            this.logInput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.logInput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.logInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.logInput_KeyDown);
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Location = new System.Drawing.Point(0, 3);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(449, 244);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // lblSubstatus
            // 
            this.lblSubstatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSubstatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblSubstatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblSubstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblSubstatus.Location = new System.Drawing.Point(-4, 32);
            this.lblSubstatus.Name = "lblSubstatus";
            this.lblSubstatus.Size = new System.Drawing.Size(452, 20);
            this.lblSubstatus.TabIndex = 3;
            this.lblSubstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubstatus.UseCustomForeColor = true;
            this.lblSubstatus.Click += new System.EventHandler(this.lblSubstatus_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(193, 120);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(50, 50);
            this.metroProgressSpinner1.Spinning = false;
            this.metroProgressSpinner1.TabIndex = 4;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "Form1";
            this.Text = "Rohyl\'s Personal MP3 File Auto-Tagger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private MetroFramework.Controls.MetroLabel lbl_FileStatus;
        private System.Windows.Forms.RichTextBox logBox;
        private MetroFramework.Controls.MetroTextBox logInput;
        private MetroFramework.Controls.MetroLabel lblSubstatus;
        private System.Windows.Forms.Timer timer1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}

