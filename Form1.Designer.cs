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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
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
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.lblMonitorStatus = new MetroFramework.Controls.MetroLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblMonitorIntialised = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.pnl_chartControls = new System.Windows.Forms.Panel();
            this.btn_orderCharts = new System.Windows.Forms.Button();
            this.btn_refreshCharts = new System.Windows.Forms.Button();
            this.chk_hideCheckedCharts = new MetroFramework.Controls.MetroCheckBox();
            this.pnl_searchingCharts = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_chartProgress = new MetroFramework.Controls.MetroLabel();
            this.pnl_EmptyCharts = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ariaFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.logInput = new MetroFramework.Controls.MetroTextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.browserLayouts = new System.Windows.Forms.FlowLayoutPanel();
            this.lyricsBrowser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.metroTabPage6 = new MetroFramework.Controls.MetroTabPage();
            this.lyricsView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_LyricStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_ShowLyrics = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_LastMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shazamBrowser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.tmr_ScanCharts = new System.Windows.Forms.Timer(this.components);
            this.lblMonitoringDirectory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_Initialised = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_chartCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.pnl_chartControls.SuspendLayout();
            this.pnl_searchingCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnl_EmptyCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.metroTabPage5.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.browserLayouts.SuspendLayout();
            this.metroTabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyricsView)).BeginInit();
            this.pnl_Initialised.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.metroTabPage1);
            this.tabControl.Controls.Add(this.metroTabPage3);
            this.tabControl.Controls.Add(this.metroTabPage4);
            this.tabControl.Controls.Add(this.metroTabPage5);
            this.tabControl.Controls.Add(this.metroTabPage2);
            this.tabControl.Controls.Add(this.metroTabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 1;
            this.tabControl.Size = new System.Drawing.Size(660, 420);
            this.tabControl.TabIndex = 0;
            this.tabControl.UseSelectable = true;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.pnlProcess);
            this.metroTabPage1.Controls.Add(this.pnlOutcome);
            this.metroTabPage1.Controls.Add(this.panelControls);
            this.metroTabPage1.Controls.Add(this.lblSubstatus);
            this.metroTabPage1.Controls.Add(this.lbl_FileStatus);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(909, 467);
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
            this.pnlOutcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutcome.Location = new System.Drawing.Point(0, 59);
            this.pnlOutcome.Name = "pnlOutcome";
            this.pnlOutcome.Size = new System.Drawing.Size(909, 361);
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
            this.pictureBox1.Size = new System.Drawing.Size(909, 274);
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
            this.lblResults.Location = new System.Drawing.Point(0, 274);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(909, 87);
            this.lblResults.TabIndex = 9;
            this.lblResults.Text = "No files have been changed.";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResults.UseCustomForeColor = true;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.Transparent;
            this.panelControls.Controls.Add(this.layout_Controls);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 420);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(909, 47);
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
            this.layout_Controls.Size = new System.Drawing.Size(909, 47);
            this.layout_Controls.TabIndex = 1;
            // 
            // rescanCharts
            // 
            this.rescanCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rescanCharts.Location = new System.Drawing.Point(703, 6);
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
            this.metroButton2.Location = new System.Drawing.Point(99, 6);
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
            this.btn_RescanAll.Location = new System.Drawing.Point(401, 6);
            this.btn_RescanAll.Name = "btn_RescanAll";
            this.btn_RescanAll.Size = new System.Drawing.Size(104, 34);
            this.btn_RescanAll.TabIndex = 1;
            this.btn_RescanAll.Text = "Rescan Lyrics";
            this.btn_RescanAll.UseSelectable = true;
            this.btn_RescanAll.Click += new System.EventHandler(this.btn_RescanAll_Click);
            // 
            // focusMe
            // 
            this.focusMe.Location = new System.Drawing.Point(909, 3);
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
            this.lblSubstatus.Size = new System.Drawing.Size(909, 20);
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
            this.lbl_FileStatus.Size = new System.Drawing.Size(909, 39);
            this.lbl_FileStatus.TabIndex = 2;
            this.lbl_FileStatus.Text = "Searching Files";
            this.lbl_FileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_FileStatus.UseCustomForeColor = true;
            // 
            // pnlProcess
            // 
            this.pnlProcess.Controls.Add(this.lbl_Percentage);
            this.pnlProcess.Controls.Add(this.metroProgressSpinner1);
            this.pnlProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProcess.HorizontalScrollbarBarColor = true;
            this.pnlProcess.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProcess.HorizontalScrollbarSize = 10;
            this.pnlProcess.Location = new System.Drawing.Point(0, 59);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(909, 361);
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
            this.lbl_Percentage.Location = new System.Drawing.Point(442, 171);
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
            this.metroProgressSpinner1.Location = new System.Drawing.Point(429, 152);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(50, 50);
            this.metroProgressSpinner1.Spinning = false;
            this.metroProgressSpinner1.TabIndex = 4;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.BackColor = System.Drawing.Color.White;
            this.metroTabPage3.Controls.Add(this.pnl_Initialised);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(652, 378);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Monitor";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // lblMonitorStatus
            // 
            this.lblMonitorStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMonitorStatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblMonitorStatus.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblMonitorStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblMonitorStatus.Location = new System.Drawing.Point(262, 97);
            this.lblMonitorStatus.Name = "lblMonitorStatus";
            this.lblMonitorStatus.Size = new System.Drawing.Size(136, 25);
            this.lblMonitorStatus.TabIndex = 12;
            this.lblMonitorStatus.Text = "Ready for file changes";
            this.lblMonitorStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMonitorStatus.UseCustomForeColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(262, 125);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 128);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // lblMonitorIntialised
            // 
            this.lblMonitorIntialised.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMonitorIntialised.AutoSize = true;
            this.lblMonitorIntialised.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblMonitorIntialised.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblMonitorIntialised.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblMonitorIntialised.Location = new System.Drawing.Point(208, 72);
            this.lblMonitorIntialised.Name = "lblMonitorIntialised";
            this.lblMonitorIntialised.Size = new System.Drawing.Size(246, 25);
            this.lblMonitorIntialised.TabIndex = 3;
            this.lblMonitorIntialised.Text = "Monitor Succesfully Initialised";
            this.lblMonitorIntialised.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonitorIntialised.UseCustomForeColor = true;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.pnl_chartControls);
            this.metroTabPage4.Controls.Add(this.pnl_searchingCharts);
            this.metroTabPage4.Controls.Add(this.pnl_EmptyCharts);
            this.metroTabPage4.Controls.Add(this.ariaFlow);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 10;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(909, 467);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Charts";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 10;
            // 
            // pnl_chartControls
            // 
            this.pnl_chartControls.BackColor = System.Drawing.Color.Transparent;
            this.pnl_chartControls.Controls.Add(this.tableLayoutPanel1);
            this.pnl_chartControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_chartControls.Location = new System.Drawing.Point(0, 435);
            this.pnl_chartControls.Name = "pnl_chartControls";
            this.pnl_chartControls.Size = new System.Drawing.Size(909, 32);
            this.pnl_chartControls.TabIndex = 4;
            this.pnl_chartControls.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_chartControls_Paint);
            // 
            // btn_orderCharts
            // 
            this.btn_orderCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_orderCharts.Location = new System.Drawing.Point(522, 4);
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
            this.btn_refreshCharts.Location = new System.Drawing.Point(295, 4);
            this.btn_refreshCharts.Name = "btn_refreshCharts";
            this.btn_refreshCharts.Size = new System.Drawing.Size(90, 23);
            this.btn_refreshCharts.TabIndex = 16;
            this.btn_refreshCharts.Text = "Refresh Charts";
            this.btn_refreshCharts.UseVisualStyleBackColor = true;
            this.btn_refreshCharts.Click += new System.EventHandler(this.btn_refreshCharts_Click);
            // 
            // chk_hideCheckedCharts
            // 
            this.chk_hideCheckedCharts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_hideCheckedCharts.AutoSize = true;
            this.chk_hideCheckedCharts.Checked = true;
            this.chk_hideCheckedCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_hideCheckedCharts.Location = new System.Drawing.Point(729, 8);
            this.chk_hideCheckedCharts.Name = "chk_hideCheckedCharts";
            this.chk_hideCheckedCharts.Size = new System.Drawing.Size(132, 15);
            this.chk_hideCheckedCharts.TabIndex = 15;
            this.chk_hideCheckedCharts.Text = "Hide Checked Songs";
            this.chk_hideCheckedCharts.UseSelectable = true;
            this.chk_hideCheckedCharts.CheckedChanged += new System.EventHandler(this.chk_hideCheckedCharts_CheckedChanged);
            // 
            // pnl_searchingCharts
            // 
            this.pnl_searchingCharts.BackColor = System.Drawing.Color.Transparent;
            this.pnl_searchingCharts.Controls.Add(this.panel1);
            this.pnl_searchingCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_searchingCharts.Location = new System.Drawing.Point(0, 0);
            this.pnl_searchingCharts.Name = "pnl_searchingCharts";
            this.pnl_searchingCharts.Size = new System.Drawing.Size(909, 467);
            this.pnl_searchingCharts.TabIndex = 10;
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
            // pnl_EmptyCharts
            // 
            this.pnl_EmptyCharts.BackColor = System.Drawing.Color.Transparent;
            this.pnl_EmptyCharts.Controls.Add(this.pictureBox3);
            this.pnl_EmptyCharts.Controls.Add(this.metroLabel1);
            this.pnl_EmptyCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_EmptyCharts.Location = new System.Drawing.Point(0, 0);
            this.pnl_EmptyCharts.Name = "pnl_EmptyCharts";
            this.pnl_EmptyCharts.Size = new System.Drawing.Size(909, 467);
            this.pnl_EmptyCharts.TabIndex = 9;
            this.pnl_EmptyCharts.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(390, 129);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(128, 128);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel1.Location = new System.Drawing.Point(90, 185);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(272, 25);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "all current chart songs processed";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // ariaFlow
            // 
            this.ariaFlow.AutoScroll = true;
            this.ariaFlow.BackColor = System.Drawing.Color.Transparent;
            this.ariaFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ariaFlow.Location = new System.Drawing.Point(1, 1);
            this.ariaFlow.Name = "ariaFlow";
            this.ariaFlow.Size = new System.Drawing.Size(451, 263);
            this.ariaFlow.TabIndex = 2;
            this.ariaFlow.WrapContents = false;
            this.ariaFlow.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.ariaFlow_ControlAdded);
            this.ariaFlow.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.ariaFlow_ControlRemoved);
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
            this.metroTabPage5.Size = new System.Drawing.Size(909, 467);
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
            this.logBox.BackColor = System.Drawing.Color.White;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Location = new System.Drawing.Point(0, 3);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(449, 244);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.browserLayouts);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(909, 467);
            this.metroTabPage2.TabIndex = 5;
            this.metroTabPage2.Text = "Browser";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // browserLayouts
            // 
            this.browserLayouts.Controls.Add(this.lyricsBrowser);
            this.browserLayouts.Location = new System.Drawing.Point(1, 1);
            this.browserLayouts.Name = "browserLayouts";
            this.browserLayouts.Size = new System.Drawing.Size(451, 293);
            this.browserLayouts.TabIndex = 3;
            // 
            // lyricsBrowser
            // 
            this.lyricsBrowser.Location = new System.Drawing.Point(3, 3);
            this.lyricsBrowser.Size = new System.Drawing.Size(451, 293);
            this.lyricsBrowser.TabIndex = 0;
            this.lyricsBrowser.LoadingFrameComplete += new Awesomium.Core.FrameEventHandler(this.LyricsBrowsersLoadingFrameComplete);
            // 
            // metroTabPage6
            // 
            this.metroTabPage6.Controls.Add(this.lyricsView);
            this.metroTabPage6.HorizontalScrollbarBarColor = true;
            this.metroTabPage6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.HorizontalScrollbarSize = 10;
            this.metroTabPage6.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage6.Name = "metroTabPage6";
            this.metroTabPage6.Size = new System.Drawing.Size(909, 467);
            this.metroTabPage6.TabIndex = 6;
            this.metroTabPage6.Text = "Lyrics";
            this.metroTabPage6.VerticalScrollbarBarColor = true;
            this.metroTabPage6.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.VerticalScrollbarSize = 10;
            // 
            // lyricsView
            // 
            this.lyricsView.AllowUserToAddRows = false;
            this.lyricsView.AllowUserToResizeRows = false;
            this.lyricsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lyricsView.BackgroundColor = System.Drawing.Color.Black;
            this.lyricsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lyricsView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lyricsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.lyricsView.ColumnHeadersHeight = 25;
            this.lyricsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.lyricsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.clm_LyricStatus,
            this.clm_ShowLyrics,
            this.clm_LastMod});
            this.lyricsView.EnableHeadersVisualStyles = false;
            this.lyricsView.Location = new System.Drawing.Point(1, 1);
            this.lyricsView.Name = "lyricsView";
            this.lyricsView.RowHeadersVisible = false;
            this.lyricsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lyricsView.Size = new System.Drawing.Size(451, 293);
            this.lyricsView.TabIndex = 2;
            this.lyricsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lyricsView_CellContentClick);
            this.lyricsView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lyricsView_MouseClick);
            this.lyricsView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lyricsView_MouseDown);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 164.3486F;
            this.Column1.HeaderText = "File Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // clm_LyricStatus
            // 
            this.clm_LyricStatus.FillWeight = 35F;
            this.clm_LyricStatus.HeaderText = "Status";
            this.clm_LyricStatus.Name = "clm_LyricStatus";
            this.clm_LyricStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // clm_ShowLyrics
            // 
            this.clm_ShowLyrics.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clm_ShowLyrics.FillWeight = 69.65433F;
            this.clm_ShowLyrics.HeaderText = "Lyrics";
            this.clm_ShowLyrics.Name = "clm_ShowLyrics";
            this.clm_ShowLyrics.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clm_LastMod
            // 
            this.clm_LastMod.FillWeight = 69.97974F;
            this.clm_LastMod.HeaderText = "Last Modified";
            this.clm_LastMod.Name = "clm_LastMod";
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
            this.tmr_ScanCharts.Interval = 3000;
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
            // pnl_Initialised
            // 
            this.pnl_Initialised.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Initialised.Controls.Add(this.pictureBox2);
            this.pnl_Initialised.Controls.Add(this.lblMonitorStatus);
            this.pnl_Initialised.Controls.Add(this.lblMonitorIntialised);
            this.pnl_Initialised.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Initialised.Location = new System.Drawing.Point(0, 0);
            this.pnl_Initialised.Name = "pnl_Initialised";
            this.pnl_Initialised.Size = new System.Drawing.Size(652, 378);
            this.pnl_Initialised.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.lbl_chartProgress);
            this.panel1.Location = new System.Drawing.Point(331, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 201);
            this.panel1.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 32);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // lbl_chartCount
            // 
            this.lbl_chartCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_chartCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chartCount.Location = new System.Drawing.Point(3, 0);
            this.lbl_chartCount.Name = "lbl_chartCount";
            this.lbl_chartCount.Size = new System.Drawing.Size(221, 32);
            this.lbl_chartCount.TabIndex = 19;
            this.lbl_chartCount.Text = "0";
            this.lbl_chartCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMonitoringDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "Form1";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Text = "Rohyl\'s Personal MP3 File Manager";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.metroTabPage4.ResumeLayout(false);
            this.pnl_chartControls.ResumeLayout(false);
            this.pnl_searchingCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnl_EmptyCharts.ResumeLayout(false);
            this.pnl_EmptyCharts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.browserLayouts.ResumeLayout(false);
            this.metroTabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lyricsView)).EndInit();
            this.pnl_Initialised.ResumeLayout(false);
            this.pnl_Initialised.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private MetroFramework.Controls.MetroButton rescanCharts;
        private MetroFramework.Controls.MetroButton focusMe;
        private MetroFramework.Controls.MetroLabel lblResults;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel lblMonitorIntialised;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroLabel lblMonitorStatus;
        private FlowLayoutPanel ariaFlow;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage6;
        private DataGridView lyricsView;
        private Panel pnl_EmptyCharts;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private PictureBox pictureBox3;
        private FlowLayoutPanel browserLayouts;
        private Awesomium.Windows.Forms.WebControl lyricsBrowser;
        private Panel pnl_searchingCharts;
        private Awesomium.Windows.Forms.WebControl shazamBrowser;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn clm_LyricStatus;
        private DataGridViewTextBoxColumn clm_ShowLyrics;
        private DataGridViewTextBoxColumn clm_LastMod;
        private Timer tmr_ScanCharts;
        private PictureBox pictureBox4;
        private MetroFramework.Controls.MetroLabel lbl_chartProgress;
        private Panel pnl_chartControls;
        private MetroFramework.Controls.MetroCheckBox chk_hideCheckedCharts;
        private Button btn_orderCharts;
        private Button btn_refreshCharts;
        private Label label1;
        protected Label lblMonitoringDirectory;
        private Panel pnl_Initialised;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbl_chartCount;
        private Button button1;
    }
}

