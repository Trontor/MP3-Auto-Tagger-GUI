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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.shazamBrowser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.tmr_ScanCharts = new System.Windows.Forms.Timer(this.components);
            this.lblMonitoringDirectory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.pnlOutcome = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblResults = new MetroFramework.Controls.MetroLabel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.layout_Controls = new System.Windows.Forms.TableLayoutPanel();
            this.rescanCharts = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.btn_RescanAll = new MetroFramework.Controls.MetroButton();
            this.focusMe = new MetroFramework.Controls.MetroButton();
            this.lblSubstatus = new MetroFramework.Controls.MetroLabel();
            this.lbl_FileStatus = new MetroFramework.Controls.MetroLabel();
            this.pnlProcess = new MetroFramework.Controls.MetroPanel();
            this.lbl_Percentage = new System.Windows.Forms.Label();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.pnl_Initialised = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblMonitorStatus = new MetroFramework.Controls.MetroLabel();
            this.lblMonitorIntialised = new MetroFramework.Controls.MetroLabel();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.pnl_EmptyCharts = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.pnl_chartControls = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_chartCount = new System.Windows.Forms.Label();
            this.chk_hideCheckedCharts = new MetroFramework.Controls.MetroCheckBox();
            this.btn_orderCharts = new System.Windows.Forms.Button();
            this.btn_refreshCharts = new System.Windows.Forms.Button();
            this.pnl_searchingCharts = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_chartProgress = new MetroFramework.Controls.MetroLabel();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.logInput = new MetroFramework.Controls.MetroTextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.browserLayouts = new System.Windows.Forms.FlowLayoutPanel();
            this.lyricsBrowser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.lyricsView = new System.Windows.Forms.DataGridView();
            this.ariaFlow = new MP3_Auto_Tagger_GUI.BetterFlowLayout();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_LyricStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_ShowLyrics = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_LastMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.pnlOutcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelControls.SuspendLayout();
            this.layout_Controls.SuspendLayout();
            this.pnlProcess.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.pnl_Initialised.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.pnl_EmptyCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnl_chartControls.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_searchingCharts.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.TabPage5.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.browserLayouts.SuspendLayout();
            this.TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyricsView)).BeginInit();
            this.SuspendLayout();
            // 
            // shazamBrowser
            // 
            this.shazamBrowser.Location = new System.Drawing.Point(1, 6);
            this.shazamBrowser.Size = new System.Drawing.Size(10, 10);
            this.shazamBrowser.Source = new System.Uri("about:blank", System.UriKind.Absolute);
            this.shazamBrowser.TabIndex = 3;
            this.shazamBrowser.LoadingFrameComplete += new Awesomium.Core.FrameEventHandler(this.ShazamBrowserLoadingFrameComplete);
            // 
            // tmr_ScanCharts
            // 
            this.tmr_ScanCharts.Enabled = true;
            this.tmr_ScanCharts.Interval = 600000;
            this.tmr_ScanCharts.Tick += new System.EventHandler(this.tmr_ScanCharts_Tick);
            // 
            // lblMonitoringDirectory
            // 
            this.lblMonitoringDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMonitoringDirectory.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMonitoringDirectory.ForeColor = System.Drawing.Color.Teal;
            this.lblMonitoringDirectory.Location = new System.Drawing.Point(119, 483);
            this.lblMonitoringDirectory.Name = "lblMonitoringDirectory";
            this.lblMonitoringDirectory.Size = new System.Drawing.Size(252, 18);
            this.lblMonitoringDirectory.TabIndex = 5;
            this.lblMonitoringDirectory.Text = "Ttt";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(20, 483);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Monitoring Directory:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TabPage1);
            this.tabControl.Controls.Add(this.TabPage3);
            this.tabControl.Controls.Add(this.TabPage4);
            this.tabControl.Controls.Add(this.TabPage5);
            this.tabControl.Controls.Add(this.TabPage2);
            this.tabControl.Controls.Add(this.TabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 2;
            this.tabControl.Size = new System.Drawing.Size(660, 420);
            this.tabControl.TabIndex = 6;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Transparent;
            this.TabPage1.Controls.Add(this.pnlOutcome);
            this.TabPage1.Controls.Add(this.panelControls);
            this.TabPage1.Controls.Add(this.lblSubstatus);
            this.TabPage1.Controls.Add(this.lbl_FileStatus);
            this.TabPage1.Controls.Add(this.pnlProcess);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(652, 394);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Status";
            // 
            // pnlOutcome
            // 
            this.pnlOutcome.BackColor = System.Drawing.Color.White;
            this.pnlOutcome.Controls.Add(this.pictureBox1);
            this.pnlOutcome.Controls.Add(this.lblResults);
            this.pnlOutcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutcome.Location = new System.Drawing.Point(0, 59);
            this.pnlOutcome.Name = "pnlOutcome";
            this.pnlOutcome.Size = new System.Drawing.Size(652, 288);
            this.pnlOutcome.TabIndex = 8;
            this.pnlOutcome.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(64, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(652, 255);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
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
            this.lblResults.Location = new System.Drawing.Point(0, 255);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(652, 33);
            this.lblResults.TabIndex = 9;
            this.lblResults.Text = "No files have been changed.";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResults.UseCustomForeColor = true;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.White;
            this.panelControls.Controls.Add(this.layout_Controls);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 347);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(652, 47);
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
            this.layout_Controls.Controls.Add(this.rescanCharts, 2, 0);
            this.layout_Controls.Controls.Add(this.metroButton2, 0, 0);
            this.layout_Controls.Controls.Add(this.btn_RescanAll, 1, 0);
            this.layout_Controls.Controls.Add(this.focusMe, 3, 0);
            this.layout_Controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_Controls.Location = new System.Drawing.Point(0, 0);
            this.layout_Controls.Name = "layout_Controls";
            this.layout_Controls.RowCount = 1;
            this.layout_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.layout_Controls.Size = new System.Drawing.Size(652, 47);
            this.layout_Controls.TabIndex = 1;
            // 
            // rescanCharts
            // 
            this.rescanCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rescanCharts.Location = new System.Drawing.Point(490, 6);
            this.rescanCharts.Name = "rescanCharts";
            this.rescanCharts.Size = new System.Drawing.Size(104, 34);
            this.rescanCharts.TabIndex = 3;
            this.rescanCharts.Text = "Rescan Charts";
            this.rescanCharts.UseSelectable = true;
            this.rescanCharts.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroButton2.Location = new System.Drawing.Point(56, 6);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(104, 34);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "Rescan Filenames";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // btn_RescanAll
            // 
            this.btn_RescanAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_RescanAll.BackColor = System.Drawing.Color.Transparent;
            this.btn_RescanAll.Location = new System.Drawing.Point(273, 6);
            this.btn_RescanAll.Name = "btn_RescanAll";
            this.btn_RescanAll.Size = new System.Drawing.Size(104, 34);
            this.btn_RescanAll.TabIndex = 1;
            this.btn_RescanAll.Text = "Rescan Lyrics";
            this.btn_RescanAll.UseSelectable = true;
            this.btn_RescanAll.Click += new System.EventHandler(this.btn_RescanAll_Click);
            // 
            // focusMe
            // 
            this.focusMe.Location = new System.Drawing.Point(654, 3);
            this.focusMe.Name = "focusMe";
            this.focusMe.Size = new System.Drawing.Size(1, 23);
            this.focusMe.TabIndex = 1;
            this.focusMe.Text = "metroButton1";
            this.focusMe.UseSelectable = true;
            // 
            // lblSubstatus
            // 
            this.lblSubstatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSubstatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubstatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblSubstatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblSubstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblSubstatus.Location = new System.Drawing.Point(0, 39);
            this.lblSubstatus.Name = "lblSubstatus";
            this.lblSubstatus.Size = new System.Drawing.Size(652, 20);
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
            this.lbl_FileStatus.Size = new System.Drawing.Size(652, 39);
            this.lbl_FileStatus.TabIndex = 2;
            this.lbl_FileStatus.Text = "Searching Files";
            this.lbl_FileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_FileStatus.UseCustomForeColor = true;
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.Color.White;
            this.pnlProcess.Controls.Add(this.lbl_Percentage);
            this.pnlProcess.Controls.Add(this.metroProgressSpinner1);
            this.pnlProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProcess.HorizontalScrollbarBarColor = true;
            this.pnlProcess.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProcess.HorizontalScrollbarSize = 10;
            this.pnlProcess.Location = new System.Drawing.Point(0, 0);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(652, 394);
            this.pnlProcess.TabIndex = 6;
            this.pnlProcess.VerticalScrollbarBarColor = true;
            this.pnlProcess.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProcess.VerticalScrollbarSize = 10;
            // 
            // lbl_Percentage
            // 
            this.lbl_Percentage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Percentage.AutoSize = true;
            this.lbl_Percentage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Percentage.Location = new System.Drawing.Point(314, 188);
            this.lbl_Percentage.Name = "lbl_Percentage";
            this.lbl_Percentage.Size = new System.Drawing.Size(25, 13);
            this.lbl_Percentage.TabIndex = 5;
            this.lbl_Percentage.Text = "100";
            this.lbl_Percentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroProgressSpinner1.EnsureVisible = false;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(301, 169);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(50, 50);
            this.metroProgressSpinner1.Spinning = false;
            this.metroProgressSpinner1.TabIndex = 4;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.White;
            this.TabPage3.Controls.Add(this.pnl_Initialised);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(652, 394);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Monitor";
            // 
            // pnl_Initialised
            // 
            this.pnl_Initialised.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Initialised.Controls.Add(this.pictureBox2);
            this.pnl_Initialised.Controls.Add(this.lblMonitorStatus);
            this.pnl_Initialised.Controls.Add(this.lblMonitorIntialised);
            this.pnl_Initialised.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Initialised.Location = new System.Drawing.Point(0, 0);
            this.pnl_Initialised.Name = "pnl_Initialised";
            this.pnl_Initialised.Size = new System.Drawing.Size(652, 394);
            this.pnl_Initialised.TabIndex = 13;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(262, 133);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 128);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // lblMonitorStatus
            // 
            this.lblMonitorStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMonitorStatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblMonitorStatus.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblMonitorStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblMonitorStatus.Location = new System.Drawing.Point(262, 105);
            this.lblMonitorStatus.Name = "lblMonitorStatus";
            this.lblMonitorStatus.Size = new System.Drawing.Size(136, 25);
            this.lblMonitorStatus.TabIndex = 12;
            this.lblMonitorStatus.Text = "Ready for file changes";
            this.lblMonitorStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMonitorStatus.UseCustomForeColor = true;
            // 
            // lblMonitorIntialised
            // 
            this.lblMonitorIntialised.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMonitorIntialised.AutoSize = true;
            this.lblMonitorIntialised.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblMonitorIntialised.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblMonitorIntialised.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblMonitorIntialised.Location = new System.Drawing.Point(208, 80);
            this.lblMonitorIntialised.Name = "lblMonitorIntialised";
            this.lblMonitorIntialised.Size = new System.Drawing.Size(246, 25);
            this.lblMonitorIntialised.TabIndex = 3;
            this.lblMonitorIntialised.Text = "Monitor Succesfully Initialised";
            this.lblMonitorIntialised.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonitorIntialised.UseCustomForeColor = true;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Transparent;
            this.TabPage4.Controls.Add(this.pnl_EmptyCharts);
            this.TabPage4.Controls.Add(this.pnl_chartControls);
            this.TabPage4.Controls.Add(this.pnl_searchingCharts);
            this.TabPage4.Controls.Add(this.ariaFlow);
            this.TabPage4.Location = new System.Drawing.Point(4, 22);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(652, 394);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Charts";
            // 
            // pnl_EmptyCharts
            // 
            this.pnl_EmptyCharts.BackColor = System.Drawing.Color.White;
            this.pnl_EmptyCharts.Controls.Add(this.pictureBox3);
            this.pnl_EmptyCharts.Controls.Add(this.metroLabel1);
            this.pnl_EmptyCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_EmptyCharts.Location = new System.Drawing.Point(0, 0);
            this.pnl_EmptyCharts.Name = "pnl_EmptyCharts";
            this.pnl_EmptyCharts.Size = new System.Drawing.Size(652, 362);
            this.pnl_EmptyCharts.TabIndex = 9;
            this.pnl_EmptyCharts.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(262, 117);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(128, 128);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel1.Location = new System.Drawing.Point(180, 260);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(272, 25);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "all current chart songs processed";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // pnl_chartControls
            // 
            this.pnl_chartControls.BackColor = System.Drawing.Color.White;
            this.pnl_chartControls.Controls.Add(this.tableLayoutPanel1);
            this.pnl_chartControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_chartControls.Location = new System.Drawing.Point(0, 362);
            this.pnl_chartControls.Name = "pnl_chartControls";
            this.pnl_chartControls.Size = new System.Drawing.Size(652, 32);
            this.pnl_chartControls.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_chartCount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chk_hideCheckedCharts, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_orderCharts, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_refreshCharts, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 32);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // lbl_chartCount
            // 
            this.lbl_chartCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_chartCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chartCount.Location = new System.Drawing.Point(3, 0);
            this.lbl_chartCount.Name = "lbl_chartCount";
            this.lbl_chartCount.Size = new System.Drawing.Size(157, 32);
            this.lbl_chartCount.TabIndex = 19;
            this.lbl_chartCount.Text = "0";
            this.lbl_chartCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_hideCheckedCharts
            // 
            this.chk_hideCheckedCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_hideCheckedCharts.AutoSize = true;
            this.chk_hideCheckedCharts.Checked = true;
            this.chk_hideCheckedCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_hideCheckedCharts.Location = new System.Drawing.Point(504, 8);
            this.chk_hideCheckedCharts.Name = "chk_hideCheckedCharts";
            this.chk_hideCheckedCharts.Size = new System.Drawing.Size(132, 15);
            this.chk_hideCheckedCharts.TabIndex = 15;
            this.chk_hideCheckedCharts.Text = "Hide Checked Songs";
            this.chk_hideCheckedCharts.UseSelectable = true;
            this.chk_hideCheckedCharts.Click += new System.EventHandler(this.chk_hideCheckedCharts_CheckedChanged);
            // 
            // btn_orderCharts
            // 
            this.btn_orderCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_orderCharts.Location = new System.Drawing.Point(362, 4);
            this.btn_orderCharts.Name = "btn_orderCharts";
            this.btn_orderCharts.Size = new System.Drawing.Size(90, 23);
            this.btn_orderCharts.TabIndex = 17;
            this.btn_orderCharts.Text = "Order Charts";
            this.btn_orderCharts.UseVisualStyleBackColor = true;
            this.btn_orderCharts.Click += new System.EventHandler(this.btn_orderCharts_Click);
            // 
            // btn_refreshCharts
            // 
            this.btn_refreshCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_refreshCharts.Location = new System.Drawing.Point(199, 4);
            this.btn_refreshCharts.Name = "btn_refreshCharts";
            this.btn_refreshCharts.Size = new System.Drawing.Size(90, 23);
            this.btn_refreshCharts.TabIndex = 16;
            this.btn_refreshCharts.Text = "Refresh Charts";
            this.btn_refreshCharts.UseVisualStyleBackColor = true;
            this.btn_refreshCharts.Click += new System.EventHandler(this.btn_refreshCharts_Click);
            // 
            // pnl_searchingCharts
            // 
            this.pnl_searchingCharts.BackColor = System.Drawing.Color.White;
            this.pnl_searchingCharts.Controls.Add(this.panel1);
            this.pnl_searchingCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_searchingCharts.Location = new System.Drawing.Point(0, 0);
            this.pnl_searchingCharts.Name = "pnl_searchingCharts";
            this.pnl_searchingCharts.Size = new System.Drawing.Size(652, 394);
            this.pnl_searchingCharts.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.lbl_chartProgress);
            this.panel1.Location = new System.Drawing.Point(203, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 201);
            this.panel1.TabIndex = 14;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(58, 14);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(128, 128);
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // lbl_chartProgress
            // 
            this.lbl_chartProgress.BackColor = System.Drawing.Color.Transparent;
            this.lbl_chartProgress.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_chartProgress.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl_chartProgress.ForeColor = System.Drawing.Color.Green;
            this.lbl_chartProgress.Location = new System.Drawing.Point(13, 163);
            this.lbl_chartProgress.Name = "lbl_chartProgress";
            this.lbl_chartProgress.Size = new System.Drawing.Size(220, 25);
            this.lbl_chartProgress.TabIndex = 10;
            this.lbl_chartProgress.Text = "Scraping Chart Websites";
            this.lbl_chartProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_chartProgress.UseCustomForeColor = true;
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.Transparent;
            this.TabPage5.Controls.Add(this.logInput);
            this.TabPage5.Controls.Add(this.logBox);
            this.TabPage5.Location = new System.Drawing.Point(4, 22);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(652, 394);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Process Log";
            // 
            // logInput
            // 
            // 
            // 
            // 
            this.logInput.CustomButton.Image = null;
            this.logInput.CustomButton.Location = new System.Drawing.Point(630, 1);
            this.logInput.CustomButton.Name = "";
            this.logInput.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.logInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.logInput.CustomButton.TabIndex = 1;
            this.logInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.logInput.CustomButton.UseSelectable = true;
            this.logInput.CustomButton.Visible = false;
            this.logInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logInput.Lines = new string[0];
            this.logInput.Location = new System.Drawing.Point(0, 371);
            this.logInput.MaxLength = 32767;
            this.logInput.Name = "logInput";
            this.logInput.PasswordChar = '\0';
            this.logInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.logInput.SelectedText = "";
            this.logInput.SelectionLength = 0;
            this.logInput.SelectionStart = 0;
            this.logInput.Size = new System.Drawing.Size(652, 23);
            this.logInput.TabIndex = 1;
            this.logInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.logInput.UseSelectable = true;
            this.logInput.WaterMark = "Console Input";
            this.logInput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.logInput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.White;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(652, 372);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Transparent;
            this.TabPage2.Controls.Add(this.browserLayouts);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(652, 394);
            this.TabPage2.TabIndex = 5;
            this.TabPage2.Text = "Browser";
            // 
            // browserLayouts
            // 
            this.browserLayouts.Controls.Add(this.lyricsBrowser);
            this.browserLayouts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserLayouts.Location = new System.Drawing.Point(0, 0);
            this.browserLayouts.Name = "browserLayouts";
            this.browserLayouts.Size = new System.Drawing.Size(652, 394);
            this.browserLayouts.TabIndex = 3;
            // 
            // lyricsBrowser
            // 
            this.lyricsBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lyricsBrowser.Location = new System.Drawing.Point(3, 3);
            this.lyricsBrowser.Size = new System.Drawing.Size(451, 0);
            this.lyricsBrowser.TabIndex = 0;
            this.lyricsBrowser.LoadingFrameComplete += new Awesomium.Core.FrameEventHandler(this.LyricsBrowsersLoadingFrameComplete);
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Transparent;
            this.TabPage6.Controls.Add(this.lyricsView);
            this.TabPage6.Location = new System.Drawing.Point(4, 22);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(652, 394);
            this.TabPage6.TabIndex = 6;
            this.TabPage6.Text = "Lyrics";
            // 
            // lyricsView
            // 
            this.lyricsView.AllowUserToAddRows = false;
            this.lyricsView.AllowUserToResizeRows = false;
            this.lyricsView.BackgroundColor = System.Drawing.Color.Black;
            this.lyricsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lyricsView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lyricsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.lyricsView.ColumnHeadersHeight = 25;
            this.lyricsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.lyricsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.clm_LyricStatus,
            this.clm_ShowLyrics,
            this.clm_LastMod});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lyricsView.DefaultCellStyle = dataGridViewCellStyle3;
            this.lyricsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lyricsView.EnableHeadersVisualStyles = false;
            this.lyricsView.GridColor = System.Drawing.Color.Black;
            this.lyricsView.Location = new System.Drawing.Point(0, 0);
            this.lyricsView.Name = "lyricsView";
            this.lyricsView.RowHeadersVisible = false;
            this.lyricsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lyricsView.Size = new System.Drawing.Size(652, 394);
            this.lyricsView.TabIndex = 2;
            this.lyricsView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lyricsView_MouseClick);
            this.lyricsView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lyricsView_MouseDown);
            // 
            // ariaFlow
            // 
            this.ariaFlow.AutoScroll = true;
            this.ariaFlow.BackColor = System.Drawing.Color.White;
            this.ariaFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ariaFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ariaFlow.Location = new System.Drawing.Point(0, 0);
            this.ariaFlow.Name = "ariaFlow";
            this.ariaFlow.Size = new System.Drawing.Size(652, 394);
            this.ariaFlow.TabIndex = 2;
            this.ariaFlow.WrapContents = false;
            this.ariaFlow.SizeChanged += new System.EventHandler(this.ariaFlow_SizeChanged);
            this.ariaFlow.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.ariaFlow_ControlAdded);
            this.ariaFlow.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.ariaFlow_ControlRemoved);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 155.362F;
            this.Column1.HeaderText = "File Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 329;
            // 
            // clm_LyricStatus
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clm_LyricStatus.DefaultCellStyle = dataGridViewCellStyle2;
            this.clm_LyricStatus.FillWeight = 20F;
            this.clm_LyricStatus.HeaderText = "Status";
            this.clm_LyricStatus.Name = "clm_LyricStatus";
            this.clm_LyricStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clm_LyricStatus.Width = 42;
            // 
            // clm_ShowLyrics
            // 
            this.clm_ShowLyrics.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_ShowLyrics.FillWeight = 65.84564F;
            this.clm_ShowLyrics.HeaderText = "Lyrics";
            this.clm_ShowLyrics.Name = "clm_ShowLyrics";
            this.clm_ShowLyrics.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clm_LastMod
            // 
            this.clm_LastMod.FillWeight = 66.15325F;
            this.clm_LastMod.HeaderText = "Last Modified";
            this.clm_LastMod.Name = "clm_LastMod";
            this.clm_LastMod.Width = 140;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblMonitoringDirectory);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "Form1";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Rohyl\'s Personal MP3 File Manager";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.pnlOutcome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.layout_Controls.ResumeLayout(false);
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.pnl_Initialised.ResumeLayout(false);
            this.pnl_Initialised.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.pnl_EmptyCharts.ResumeLayout(false);
            this.pnl_EmptyCharts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnl_chartControls.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnl_searchingCharts.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.TabPage5.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.browserLayouts.ResumeLayout(false);
            this.TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lyricsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Awesomium.Windows.Forms.WebControl shazamBrowser;
        private Timer tmr_ScanCharts;
        private Label label1;
        protected Label lblMonitoringDirectory;
        private TabControl tabControl;
        private TabPage TabPage1;
        private MetroFramework.Controls.MetroPanel pnlProcess;
        private Label lbl_Percentage;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private Panel pnlOutcome;
        private Panel panelControls;
        private TableLayoutPanel layout_Controls;
        private MetroFramework.Controls.MetroButton rescanCharts;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton btn_RescanAll;
        private MetroFramework.Controls.MetroButton focusMe;
        private MetroFramework.Controls.MetroLabel lblSubstatus;
        private MetroFramework.Controls.MetroLabel lbl_FileStatus;
        private TabPage TabPage3;
        private Panel pnl_Initialised;
        private PictureBox pictureBox2;
        private MetroFramework.Controls.MetroLabel lblMonitorStatus;
        private MetroFramework.Controls.MetroLabel lblMonitorIntialised;
        private TabPage TabPage4;
        private Panel pnl_chartControls;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbl_chartCount;
        private MetroFramework.Controls.MetroCheckBox chk_hideCheckedCharts;
        private Button btn_orderCharts;
        private Button btn_refreshCharts;
        private Panel pnl_searchingCharts;
        private Panel panel1;
        private PictureBox pictureBox4;
        private MetroFramework.Controls.MetroLabel lbl_chartProgress;
        private BetterFlowLayout ariaFlow;
        private TabPage TabPage5;
        private MetroFramework.Controls.MetroTextBox logInput;
        private RichTextBox logBox;
        private TabPage TabPage2;
        private FlowLayoutPanel browserLayouts;
        private Awesomium.Windows.Forms.WebControl lyricsBrowser;
        private TabPage TabPage6;
        private DataGridView lyricsView;
        private Panel pnl_EmptyCharts;
        private PictureBox pictureBox3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel lblResults;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn clm_LyricStatus;
        private DataGridViewTextBoxColumn clm_ShowLyrics;
        private DataGridViewTextBoxColumn clm_LastMod;
    }
}

