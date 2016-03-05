using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.pnlOutcome = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblResults = new MetroFramework.Controls.MetroLabel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.layout_Controls = new System.Windows.Forms.TableLayoutPanel();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.btn_RescanAll = new MetroFramework.Controls.MetroButton();
            this.focusMe = new MetroFramework.Controls.MetroButton();
            this.lblSubstatus = new MetroFramework.Controls.MetroLabel();
            this.lbl_FileStatus = new MetroFramework.Controls.MetroLabel();
            this.pnlProcess = new MetroFramework.Controls.MetroPanel();
            this.lbl_Percentage = new System.Windows.Forms.Label();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.lblMonitorStatus = new MetroFramework.Controls.MetroLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblMonitorIntialised = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.ariaFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.logInput = new MetroFramework.Controls.MetroTextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblMonitoringDirectory = new System.Windows.Forms.Label();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
            this.tabControl.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.pnlOutcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelControls.SuspendLayout();
            this.layout_Controls.SuspendLayout();
            this.pnlProcess.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.metroTabPage1);
            this.tabControl.Controls.Add(this.metroTabPage3);
            this.tabControl.Controls.Add(this.metroTabPage4);
            this.tabControl.Controls.Add(this.metroTabPage5);
            this.tabControl.Controls.Add(this.metroTabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 2;
            this.tabControl.Size = new System.Drawing.Size(460, 336);
            this.tabControl.TabIndex = 0;
            this.tabControl.UseSelectable = true;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            this.tabControl.TabIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.pnlOutcome);
            this.metroTabPage1.Controls.Add(this.panelControls);
            this.metroTabPage1.Controls.Add(this.lblSubstatus);
            this.metroTabPage1.Controls.Add(this.lbl_FileStatus);
            this.metroTabPage1.Controls.Add(this.pnlProcess);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(452, 294);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Status";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            this.metroTabPage1.Click += new System.EventHandler(this.metroTabPage1_Click);
            // 
            // pnlOutcome
            // 
            this.pnlOutcome.BackColor = System.Drawing.Color.Transparent;
            this.pnlOutcome.Controls.Add(this.pictureBox1);
            this.pnlOutcome.Controls.Add(this.lblResults);
            this.pnlOutcome.Location = new System.Drawing.Point(0, 55);
            this.pnlOutcome.Name = "pnlOutcome";
            this.pnlOutcome.Size = new System.Drawing.Size(452, 173);
            this.pnlOutcome.TabIndex = 8;
            this.pnlOutcome.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(194, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // lblResults
            // 
            this.lblResults.BackColor = System.Drawing.Color.Transparent;
            this.lblResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblResults.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblResults.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblResults.Location = new System.Drawing.Point(0, 86);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(452, 87);
            this.lblResults.TabIndex = 9;
            this.lblResults.Text = "No files have been changed.";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResults.UseCustomForeColor = true;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.Transparent;
            this.panelControls.Controls.Add(this.layout_Controls);
            this.panelControls.Location = new System.Drawing.Point(0, 228);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(452, 47);
            this.panelControls.TabIndex = 7;
            this.panelControls.Visible = false;
            // 
            // layout_Controls
            // 
            this.layout_Controls.ColumnCount = 4;
            this.layout_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.30003F));
            this.layout_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.30004F));
            this.layout_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.30004F));
            this.layout_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.09990012F));
            this.layout_Controls.Controls.Add(this.metroButton3, 2, 0);
            this.layout_Controls.Controls.Add(this.metroButton2, 0, 0);
            this.layout_Controls.Controls.Add(this.btn_RescanAll, 1, 0);
            this.layout_Controls.Controls.Add(this.focusMe, 3, 0);
            this.layout_Controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_Controls.Location = new System.Drawing.Point(0, 0);
            this.layout_Controls.Name = "layout_Controls";
            this.layout_Controls.RowCount = 1;
            this.layout_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.layout_Controls.Size = new System.Drawing.Size(452, 47);
            this.layout_Controls.TabIndex = 1;
            // 
            // metroButton3
            // 
            this.metroButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroButton3.Location = new System.Drawing.Point(323, 6);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(104, 34);
            this.metroButton3.TabIndex = 3;
            this.metroButton3.Text = "Rescan Artwork";
            this.metroButton3.UseSelectable = true;
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroButton2.Location = new System.Drawing.Point(23, 6);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(104, 34);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "Rescan Filenames";
            this.metroButton2.UseSelectable = true;
            // 
            // btn_RescanAll
            // 
            this.btn_RescanAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_RescanAll.BackColor = System.Drawing.Color.Transparent;
            this.btn_RescanAll.Location = new System.Drawing.Point(173, 6);
            this.btn_RescanAll.Name = "btn_RescanAll";
            this.btn_RescanAll.Size = new System.Drawing.Size(104, 34);
            this.btn_RescanAll.TabIndex = 1;
            this.btn_RescanAll.Text = "Rescan All";
            this.btn_RescanAll.UseSelectable = true;
            this.btn_RescanAll.Click += new System.EventHandler(this.btn_RescanAll_Click);
            // 
            // focusMe
            // 
            this.focusMe.Location = new System.Drawing.Point(453, 3);
            this.focusMe.Name = "focusMe";
            this.focusMe.Size = new System.Drawing.Size(1, 23);
            this.focusMe.TabIndex = 1;
            this.focusMe.Text = "metroButton1";
            this.focusMe.UseSelectable = true;
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
            // pnlProcess
            // 
            this.pnlProcess.Controls.Add(this.lbl_Percentage);
            this.pnlProcess.Controls.Add(this.metroProgressSpinner1);
            this.pnlProcess.HorizontalScrollbarBarColor = true;
            this.pnlProcess.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProcess.HorizontalScrollbarSize = 10;
            this.pnlProcess.Location = new System.Drawing.Point(0, 55);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(452, 220);
            this.pnlProcess.TabIndex = 6;
            this.pnlProcess.VerticalScrollbarBarColor = true;
            this.pnlProcess.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProcess.VerticalScrollbarSize = 10;
            // 
            // lbl_Percentage
            // 
            this.lbl_Percentage.AutoSize = true;
            this.lbl_Percentage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Percentage.Location = new System.Drawing.Point(215, 110);
            this.lbl_Percentage.Name = "lbl_Percentage";
            this.lbl_Percentage.Size = new System.Drawing.Size(25, 13);
            this.lbl_Percentage.TabIndex = 5;
            this.lbl_Percentage.Text = "100";
            this.lbl_Percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.EnsureVisible = false;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(202, 91);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(50, 50);
            this.metroProgressSpinner1.Spinning = false;
            this.metroProgressSpinner1.TabIndex = 4;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.lblMonitorStatus);
            this.metroTabPage3.Controls.Add(this.pictureBox2);
            this.metroTabPage3.Controls.Add(this.lblMonitorIntialised);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(452, 294);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Monitor";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // lblMonitorStatus
            // 
            this.lblMonitorStatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblMonitorStatus.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblMonitorStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblMonitorStatus.Location = new System.Drawing.Point(0, 70);
            this.lblMonitorStatus.Name = "lblMonitorStatus";
            this.lblMonitorStatus.Size = new System.Drawing.Size(452, 25);
            this.lblMonitorStatus.TabIndex = 12;
            this.lblMonitorStatus.Text = "Ready for file changes";
            this.lblMonitorStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMonitorStatus.UseCustomForeColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(166, 113);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 128);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // lblMonitorIntialised
            // 
            this.lblMonitorIntialised.AutoSize = true;
            this.lblMonitorIntialised.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblMonitorIntialised.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblMonitorIntialised.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblMonitorIntialised.Location = new System.Drawing.Point(107, 33);
            this.lblMonitorIntialised.Name = "lblMonitorIntialised";
            this.lblMonitorIntialised.Size = new System.Drawing.Size(246, 25);
            this.lblMonitorIntialised.TabIndex = 3;
            this.lblMonitorIntialised.Text = "Monitor Succesfully Initialised";
            this.lblMonitorIntialised.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonitorIntialised.UseCustomForeColor = true;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.ariaFlow);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 10;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(452, 294);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Charts";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 10;
            // 
            // ariaFlow
            // 
            this.ariaFlow.AutoScroll = true;
            this.ariaFlow.BackColor = System.Drawing.Color.Transparent;
            this.ariaFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ariaFlow.Location = new System.Drawing.Point(1, 1);
            this.ariaFlow.Name = "ariaFlow";
            this.ariaFlow.Size = new System.Drawing.Size(451, 293);
            this.ariaFlow.TabIndex = 2;
            this.ariaFlow.WrapContents = false;
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
            this.metroTabPage5.Size = new System.Drawing.Size(452, 294);
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
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Location = new System.Drawing.Point(0, 3);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(449, 244);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(21, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitoring Directory:";
            // 
            // lblMonitoringDirectory
            // 
            this.lblMonitoringDirectory.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMonitoringDirectory.ForeColor = System.Drawing.Color.Teal;
            this.lblMonitoringDirectory.Location = new System.Drawing.Point(120, 395);
            this.lblMonitoringDirectory.Name = "lblMonitoringDirectory";
            this.lblMonitoringDirectory.Size = new System.Drawing.Size(358, 18);
            this.lblMonitoringDirectory.TabIndex = 2;
            this.lblMonitoringDirectory.Text = "Ttt";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.webControl1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(452, 294);
            this.metroTabPage2.TabIndex = 5;
            this.metroTabPage2.Text = "Browser";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // webControl1
            // 
            this.webControl1.Location = new System.Drawing.Point(1, 1);
            this.webControl1.Size = new System.Drawing.Size(451, 293);
            this.webControl1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 416);
            this.Controls.Add(this.lblMonitoringDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Text = "Rohyl\'s Personal MP3 File Auto-Tagger";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.pnlOutcome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.layout_Controls.ResumeLayout(false);
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private MetroFramework.Controls.MetroLabel lbl_FileStatus;
        private System.Windows.Forms.RichTextBox logBox;
        private MetroFramework.Controls.MetroTextBox logInput;
        private MetroFramework.Controls.MetroLabel lblSubstatus;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Label lbl_Percentage;
        private MetroFramework.Controls.MetroPanel pnlProcess;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel pnlOutcome;
        private System.Windows.Forms.TableLayoutPanel layout_Controls;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton btn_RescanAll;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroButton focusMe;
        private MetroFramework.Controls.MetroLabel lblResults;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private MetroFramework.Controls.MetroLabel lblMonitorIntialised;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroLabel lblMonitorStatus;
        private Label label1;
        private Label lblMonitoringDirectory;
        private FlowLayoutPanel ariaFlow;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private Awesomium.Windows.Forms.WebControl webControl1;
    }
}

