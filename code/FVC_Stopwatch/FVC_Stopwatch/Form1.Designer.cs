namespace FVC_Stopwatch
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
            this.tmrIncrease = new System.Windows.Forms.Timer(this.components);
            this.tmrDecrease = new System.Windows.Forms.Timer(this.components);
            this.pnBack = new System.Windows.Forms.Panel();
            this.pnOption = new System.Windows.Forms.Panel();
            this.gbButton = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeafault = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.gbMedia = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnOpenSoundFile = new System.Windows.Forms.Button();
            this.pibPlaySound = new System.Windows.Forms.PictureBox();
            this.txtSoundPath = new System.Windows.Forms.TextBox();
            this.pibMenuSub = new System.Windows.Forms.PictureBox();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbMinuteG = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSecondG = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMinuteH = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSecondH = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pibReset = new System.Windows.Forms.PictureBox();
            this.pibFullScreen = new System.Windows.Forms.PictureBox();
            this.pibIncrease = new System.Windows.Forms.PictureBox();
            this.pibMain = new System.Windows.Forms.PictureBox();
            this.pibDecrease = new System.Windows.Forms.PictureBox();
            this.pibH2 = new System.Windows.Forms.PictureBox();
            this.pibGL = new System.Windows.Forms.PictureBox();
            this.pibH1 = new System.Windows.Forms.PictureBox();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.pibMenu = new System.Windows.Forms.PictureBox();
            this.pibMinuteFirst = new System.Windows.Forms.PictureBox();
            this.pibMinuteLast = new System.Windows.Forms.PictureBox();
            this.pibDot = new System.Windows.Forms.PictureBox();
            this.pibSecondFirst = new System.Windows.Forms.PictureBox();
            this.pibSecondLast = new System.Windows.Forms.PictureBox();
            this.pibSound = new System.Windows.Forms.PictureBox();
            this.opdSound = new System.Windows.Forms.OpenFileDialog();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.pnBack.SuspendLayout();
            this.pnOption.SuspendLayout();
            this.gbButton.SuspendLayout();
            this.gbMedia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibPlaySound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMenuSub)).BeginInit();
            this.gbTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibFullScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibIncrease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibDecrease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibH2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibGL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibH1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMinuteFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMinuteLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSound)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrIncrease
            // 
            this.tmrIncrease.Interval = 1000;
            this.tmrIncrease.Tick += new System.EventHandler(this.tmrIncrease_Tick);
            // 
            // tmrDecrease
            // 
            this.tmrDecrease.Interval = 1000;
            this.tmrDecrease.Tick += new System.EventHandler(this.tmrDecrease_Tick);
            // 
            // pnBack
            // 
            this.pnBack.AutoSize = true;
            this.pnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnBack.Controls.Add(this.pnOption);
            this.pnBack.Controls.Add(this.pibReset);
            this.pnBack.Controls.Add(this.pibFullScreen);
            this.pnBack.Controls.Add(this.pibIncrease);
            this.pnBack.Controls.Add(this.pibMain);
            this.pnBack.Controls.Add(this.pibDecrease);
            this.pnBack.Controls.Add(this.pibH2);
            this.pnBack.Controls.Add(this.pibGL);
            this.pnBack.Controls.Add(this.pibH1);
            this.pnBack.Controls.Add(this.pibStatus);
            this.pnBack.Controls.Add(this.pibMenu);
            this.pnBack.Controls.Add(this.pibMinuteFirst);
            this.pnBack.Controls.Add(this.pibMinuteLast);
            this.pnBack.Controls.Add(this.pibDot);
            this.pnBack.Controls.Add(this.pibSecondFirst);
            this.pnBack.Controls.Add(this.pibSecondLast);
            this.pnBack.Controls.Add(this.pibSound);
            this.pnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBack.Location = new System.Drawing.Point(0, 0);
            this.pnBack.Name = "pnBack";
            this.pnBack.Size = new System.Drawing.Size(960, 540);
            this.pnBack.TabIndex = 26;
            // 
            // pnOption
            // 
            this.pnOption.BackColor = System.Drawing.SystemColors.Control;
            this.pnOption.Controls.Add(this.gbButton);
            this.pnOption.Controls.Add(this.gbMedia);
            this.pnOption.Controls.Add(this.pibMenuSub);
            this.pnOption.Controls.Add(this.gbTime);
            this.pnOption.Controls.Add(this.label3);
            this.pnOption.Location = new System.Drawing.Point(12, 15);
            this.pnOption.Name = "pnOption";
            this.pnOption.Size = new System.Drawing.Size(134, 135);
            this.pnOption.TabIndex = 36;
            this.pnOption.Visible = false;
            // 
            // gbButton
            // 
            this.gbButton.Controls.Add(this.btnOK);
            this.gbButton.Controls.Add(this.btnCancel);
            this.gbButton.Controls.Add(this.btnDeafault);
            this.gbButton.Controls.Add(this.btnAbout);
            this.gbButton.Location = new System.Drawing.Point(111, 170);
            this.gbButton.Name = "gbButton";
            this.gbButton.Size = new System.Drawing.Size(581, 68);
            this.gbButton.TabIndex = 40;
            this.gbButton.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(497, 21);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(416, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeafault
            // 
            this.btnDeafault.Location = new System.Drawing.Point(6, 21);
            this.btnDeafault.Name = "btnDeafault";
            this.btnDeafault.Size = new System.Drawing.Size(75, 27);
            this.btnDeafault.TabIndex = 13;
            this.btnDeafault.Text = "Mặc định";
            this.btnDeafault.UseVisualStyleBackColor = true;
            this.btnDeafault.Click += new System.EventHandler(this.btnDeafault_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(204, 21);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 27);
            this.btnAbout.TabIndex = 15;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // gbMedia
            // 
            this.gbMedia.Controls.Add(this.btnConnect);
            this.gbMedia.Controls.Add(this.label11);
            this.gbMedia.Controls.Add(this.label10);
            this.gbMedia.Controls.Add(this.txtPort);
            this.gbMedia.Controls.Add(this.txtIP);
            this.gbMedia.Controls.Add(this.btnOpenSoundFile);
            this.gbMedia.Controls.Add(this.pibPlaySound);
            this.gbMedia.Controls.Add(this.txtSoundPath);
            this.gbMedia.Location = new System.Drawing.Point(350, 15);
            this.gbMedia.Name = "gbMedia";
            this.gbMedia.Size = new System.Drawing.Size(342, 149);
            this.gbMedia.TabIndex = 38;
            this.gbMedia.TabStop = false;
            this.gbMedia.Text = "Thay đổi tùy chọn";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(194, 80);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(103, 28);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(87, 97);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 4;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(87, 70);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 3;
            // 
            // btnOpenSoundFile
            // 
            this.btnOpenSoundFile.Location = new System.Drawing.Point(193, 29);
            this.btnOpenSoundFile.Name = "btnOpenSoundFile";
            this.btnOpenSoundFile.Size = new System.Drawing.Size(104, 27);
            this.btnOpenSoundFile.TabIndex = 2;
            this.btnOpenSoundFile.Text = "Tìm âm khác";
            this.btnOpenSoundFile.UseVisualStyleBackColor = true;
            this.btnOpenSoundFile.Click += new System.EventHandler(this.btnOpenSoundFile_Click);
            // 
            // pibPlaySound
            // 
            this.pibPlaySound.Image = global::FVC_Stopwatch.Properties.Resources.PlaySound;
            this.pibPlaySound.Location = new System.Drawing.Point(303, 27);
            this.pibPlaySound.Name = "pibPlaySound";
            this.pibPlaySound.Size = new System.Drawing.Size(30, 30);
            this.pibPlaySound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibPlaySound.TabIndex = 1;
            this.pibPlaySound.TabStop = false;
            this.pibPlaySound.Visible = false;
            this.pibPlaySound.Click += new System.EventHandler(this.pibPlaySound_Click);
            this.pibPlaySound.MouseEnter += new System.EventHandler(this.pibPlaySound_MouseEnter);
            this.pibPlaySound.MouseLeave += new System.EventHandler(this.pibPlaySound_MouseLeave);
            // 
            // txtSoundPath
            // 
            this.txtSoundPath.Location = new System.Drawing.Point(6, 30);
            this.txtSoundPath.Name = "txtSoundPath";
            this.txtSoundPath.ReadOnly = true;
            this.txtSoundPath.Size = new System.Drawing.Size(181, 20);
            this.txtSoundPath.TabIndex = 0;
            // 
            // pibMenuSub
            // 
            this.pibMenuSub.Image = global::FVC_Stopwatch.Properties.Resources.Menu_Click;
            this.pibMenuSub.Location = new System.Drawing.Point(15, 15);
            this.pibMenuSub.Name = "pibMenuSub";
            this.pibMenuSub.Size = new System.Drawing.Size(90, 90);
            this.pibMenuSub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibMenuSub.TabIndex = 37;
            this.pibMenuSub.TabStop = false;
            this.pibMenuSub.Click += new System.EventHandler(this.pibMenuSub_Click);
            this.pibMenuSub.MouseEnter += new System.EventHandler(this.pibMenuSub_MouseEnter);
            this.pibMenuSub.MouseLeave += new System.EventHandler(this.pibMenuSub_MouseLeave);
            // 
            // gbTime
            // 
            this.gbTime.Controls.Add(this.label9);
            this.gbTime.Controls.Add(this.label8);
            this.gbTime.Controls.Add(this.label5);
            this.gbTime.Controls.Add(this.label6);
            this.gbTime.Controls.Add(this.cbMinuteG);
            this.gbTime.Controls.Add(this.label7);
            this.gbTime.Controls.Add(this.cbSecondG);
            this.gbTime.Controls.Add(this.label4);
            this.gbTime.Controls.Add(this.label1);
            this.gbTime.Controls.Add(this.cbMinuteH);
            this.gbTime.Controls.Add(this.label2);
            this.gbTime.Controls.Add(this.cbSecondH);
            this.gbTime.Location = new System.Drawing.Point(111, 15);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(231, 149);
            this.gbTime.TabIndex = 10;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Thời gian đối kháng";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Giải lao";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Phút";
            // 
            // cbMinuteG
            // 
            this.cbMinuteG.FormattingEnabled = true;
            this.cbMinuteG.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cbMinuteG.Location = new System.Drawing.Point(98, 115);
            this.cbMinuteG.Name = "cbMinuteG";
            this.cbMinuteG.Size = new System.Drawing.Size(45, 21);
            this.cbMinuteG.TabIndex = 5;
            this.cbMinuteG.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Giây";
            // 
            // cbSecondG
            // 
            this.cbSecondG.FormattingEnabled = true;
            this.cbSecondG.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cbSecondG.Location = new System.Drawing.Point(163, 115);
            this.cbSecondG.Name = "cbSecondG";
            this.cbSecondG.Size = new System.Drawing.Size(45, 21);
            this.cbSecondG.TabIndex = 6;
            this.cbSecondG.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Hiệp đấu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Phút";
            // 
            // cbMinuteH
            // 
            this.cbMinuteH.FormattingEnabled = true;
            this.cbMinuteH.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cbMinuteH.Location = new System.Drawing.Point(98, 41);
            this.cbMinuteH.Name = "cbMinuteH";
            this.cbMinuteH.Size = new System.Drawing.Size(45, 21);
            this.cbMinuteH.TabIndex = 0;
            this.cbMinuteH.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Giây";
            // 
            // cbSecondH
            // 
            this.cbSecondH.FormattingEnabled = true;
            this.cbSecondH.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cbSecondH.Location = new System.Drawing.Point(163, 41);
            this.cbSecondH.Name = "cbSecondH";
            this.cbSecondH.Size = new System.Drawing.Size(45, 21);
            this.cbSecondH.TabIndex = 1;
            this.cbSecondH.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 9;
            // 
            // pibReset
            // 
            this.pibReset.Image = global::FVC_Stopwatch.Properties.Resources.Reset1;
            this.pibReset.Location = new System.Drawing.Point(30, 450);
            this.pibReset.Name = "pibReset";
            this.pibReset.Size = new System.Drawing.Size(60, 60);
            this.pibReset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibReset.TabIndex = 35;
            this.pibReset.TabStop = false;
            this.pibReset.Click += new System.EventHandler(this.pibReset_Click);
            this.pibReset.MouseEnter += new System.EventHandler(this.pibReset_MouseEnter);
            this.pibReset.MouseLeave += new System.EventHandler(this.pibReset_MouseLeave);
            // 
            // pibFullScreen
            // 
            this.pibFullScreen.Image = global::FVC_Stopwatch.Properties.Resources.FullScreen;
            this.pibFullScreen.Location = new System.Drawing.Point(900, 0);
            this.pibFullScreen.Name = "pibFullScreen";
            this.pibFullScreen.Size = new System.Drawing.Size(30, 30);
            this.pibFullScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibFullScreen.TabIndex = 34;
            this.pibFullScreen.TabStop = false;
            this.pibFullScreen.Click += new System.EventHandler(this.pibFullScreen_Click);
            this.pibFullScreen.MouseEnter += new System.EventHandler(this.pibFullScreen_MouseEnter);
            this.pibFullScreen.MouseLeave += new System.EventHandler(this.pibFullScreen_MouseLeave);
            // 
            // pibIncrease
            // 
            this.pibIncrease.Image = global::FVC_Stopwatch.Properties.Resources.Increase;
            this.pibIncrease.Location = new System.Drawing.Point(570, 420);
            this.pibIncrease.Name = "pibIncrease";
            this.pibIncrease.Size = new System.Drawing.Size(60, 60);
            this.pibIncrease.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibIncrease.TabIndex = 33;
            this.pibIncrease.TabStop = false;
            this.pibIncrease.Click += new System.EventHandler(this.pibIncrease_Click);
            this.pibIncrease.MouseEnter += new System.EventHandler(this.pibIncrease_MouseEnter);
            this.pibIncrease.MouseLeave += new System.EventHandler(this.pibIncrease_MouseLeave);
            // 
            // pibMain
            // 
            this.pibMain.Image = global::FVC_Stopwatch.Properties.Resources.Start;
            this.pibMain.Location = new System.Drawing.Point(435, 390);
            this.pibMain.Name = "pibMain";
            this.pibMain.Size = new System.Drawing.Size(120, 120);
            this.pibMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibMain.TabIndex = 32;
            this.pibMain.TabStop = false;
            this.pibMain.Click += new System.EventHandler(this.pibMain_Click);
            this.pibMain.MouseEnter += new System.EventHandler(this.pibMain_MouseEnter);
            this.pibMain.MouseLeave += new System.EventHandler(this.pibMain_MouseLeave);
            // 
            // pibDecrease
            // 
            this.pibDecrease.Image = global::FVC_Stopwatch.Properties.Resources.Decrease;
            this.pibDecrease.Location = new System.Drawing.Point(330, 420);
            this.pibDecrease.Name = "pibDecrease";
            this.pibDecrease.Size = new System.Drawing.Size(60, 60);
            this.pibDecrease.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibDecrease.TabIndex = 31;
            this.pibDecrease.TabStop = false;
            this.pibDecrease.Click += new System.EventHandler(this.pibDecrease_Click);
            this.pibDecrease.MouseEnter += new System.EventHandler(this.pibDecrease_MouseEnter);
            this.pibDecrease.MouseLeave += new System.EventHandler(this.pibDecrease_MouseLeave);
            // 
            // pibH2
            // 
            this.pibH2.Image = global::FVC_Stopwatch.Properties.Resources.H2;
            this.pibH2.Location = new System.Drawing.Point(555, 90);
            this.pibH2.Name = "pibH2";
            this.pibH2.Size = new System.Drawing.Size(60, 60);
            this.pibH2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibH2.TabIndex = 30;
            this.pibH2.TabStop = false;
            this.pibH2.Click += new System.EventHandler(this.pibH2_Click);
            this.pibH2.MouseEnter += new System.EventHandler(this.pibH2_MouseEnter);
            this.pibH2.MouseLeave += new System.EventHandler(this.pibH2_MouseLeave);
            // 
            // pibGL
            // 
            this.pibGL.Image = global::FVC_Stopwatch.Properties.Resources.GL;
            this.pibGL.Location = new System.Drawing.Point(465, 90);
            this.pibGL.Name = "pibGL";
            this.pibGL.Size = new System.Drawing.Size(60, 60);
            this.pibGL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibGL.TabIndex = 29;
            this.pibGL.TabStop = false;
            this.pibGL.Click += new System.EventHandler(this.pibGL_Click);
            this.pibGL.MouseEnter += new System.EventHandler(this.pibGL_MouseEnter);
            this.pibGL.MouseLeave += new System.EventHandler(this.pibGL_MouseLeave);
            // 
            // pibH1
            // 
            this.pibH1.Image = global::FVC_Stopwatch.Properties.Resources.H1;
            this.pibH1.Location = new System.Drawing.Point(375, 90);
            this.pibH1.Name = "pibH1";
            this.pibH1.Size = new System.Drawing.Size(60, 60);
            this.pibH1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibH1.TabIndex = 28;
            this.pibH1.TabStop = false;
            this.pibH1.Click += new System.EventHandler(this.pibH1_Click);
            this.pibH1.MouseEnter += new System.EventHandler(this.pibH1_MouseEnter);
            this.pibH1.MouseLeave += new System.EventHandler(this.pibH1_MouseLeave);
            // 
            // pibStatus
            // 
            this.pibStatus.Image = global::FVC_Stopwatch.Properties.Resources.Status_Standing;
            this.pibStatus.Location = new System.Drawing.Point(390, 15);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(180, 60);
            this.pibStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibStatus.TabIndex = 27;
            this.pibStatus.TabStop = false;
            // 
            // pibMenu
            // 
            this.pibMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibMenu.Image = global::FVC_Stopwatch.Properties.Resources.Menu;
            this.pibMenu.Location = new System.Drawing.Point(30, 30);
            this.pibMenu.Name = "pibMenu";
            this.pibMenu.Size = new System.Drawing.Size(90, 90);
            this.pibMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibMenu.TabIndex = 8;
            this.pibMenu.TabStop = false;
            this.pibMenu.Click += new System.EventHandler(this.pibMenu_Click);
            this.pibMenu.MouseEnter += new System.EventHandler(this.pibMenu_MouseEnter);
            this.pibMenu.MouseLeave += new System.EventHandler(this.pibMenu_MouseLeave);
            // 
            // pibMinuteFirst
            // 
            this.pibMinuteFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibMinuteFirst.Image = global::FVC_Stopwatch.Properties.Resources._8;
            this.pibMinuteFirst.Location = new System.Drawing.Point(76, 181);
            this.pibMinuteFirst.Name = "pibMinuteFirst";
            this.pibMinuteFirst.Size = new System.Drawing.Size(180, 180);
            this.pibMinuteFirst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibMinuteFirst.TabIndex = 0;
            this.pibMinuteFirst.TabStop = false;
            // 
            // pibMinuteLast
            // 
            this.pibMinuteLast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibMinuteLast.BackColor = System.Drawing.Color.Transparent;
            this.pibMinuteLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pibMinuteLast.Image = global::FVC_Stopwatch.Properties.Resources._8;
            this.pibMinuteLast.Location = new System.Drawing.Point(556, 181);
            this.pibMinuteLast.Name = "pibMinuteLast";
            this.pibMinuteLast.Size = new System.Drawing.Size(180, 180);
            this.pibMinuteLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibMinuteLast.TabIndex = 1;
            this.pibMinuteLast.TabStop = false;
            // 
            // pibDot
            // 
            this.pibDot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibDot.Image = global::FVC_Stopwatch.Properties.Resources.Dot;
            this.pibDot.Location = new System.Drawing.Point(436, 181);
            this.pibDot.Name = "pibDot";
            this.pibDot.Size = new System.Drawing.Size(120, 180);
            this.pibDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibDot.TabIndex = 23;
            this.pibDot.TabStop = false;
            // 
            // pibSecondFirst
            // 
            this.pibSecondFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibSecondFirst.Image = global::FVC_Stopwatch.Properties.Resources._8;
            this.pibSecondFirst.Location = new System.Drawing.Point(256, 181);
            this.pibSecondFirst.Name = "pibSecondFirst";
            this.pibSecondFirst.Size = new System.Drawing.Size(180, 180);
            this.pibSecondFirst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibSecondFirst.TabIndex = 3;
            this.pibSecondFirst.TabStop = false;
            // 
            // pibSecondLast
            // 
            this.pibSecondLast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibSecondLast.Image = global::FVC_Stopwatch.Properties.Resources._8;
            this.pibSecondLast.Location = new System.Drawing.Point(736, 181);
            this.pibSecondLast.Name = "pibSecondLast";
            this.pibSecondLast.Size = new System.Drawing.Size(180, 180);
            this.pibSecondLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibSecondLast.TabIndex = 4;
            this.pibSecondLast.TabStop = false;
            // 
            // pibSound
            // 
            this.pibSound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pibSound.Image = global::FVC_Stopwatch.Properties.Resources.Sound;
            this.pibSound.Location = new System.Drawing.Point(886, 451);
            this.pibSound.Name = "pibSound";
            this.pibSound.Size = new System.Drawing.Size(60, 60);
            this.pibSound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pibSound.TabIndex = 17;
            this.pibSound.TabStop = false;
            this.pibSound.Click += new System.EventHandler(this.pibSound_Click);
            this.pibSound.MouseEnter += new System.EventHandler(this.pibSound_MouseEnter);
            this.pibSound.MouseLeave += new System.EventHandler(this.pibSound_MouseLeave);
            // 
            // opdSound
            // 
            this.opdSound.Filter = "|*.wav";
            // 
            // tmrClock
            // 
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(960, 540);
            this.Controls.Add(this.pnBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FVC Stopwatch 2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnBack.ResumeLayout(false);
            this.pnOption.ResumeLayout(false);
            this.pnOption.PerformLayout();
            this.gbButton.ResumeLayout(false);
            this.gbMedia.ResumeLayout(false);
            this.gbMedia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibPlaySound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMenuSub)).EndInit();
            this.gbTime.ResumeLayout(false);
            this.gbTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibFullScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibIncrease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibDecrease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibH2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibGL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibH1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMinuteFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibMinuteLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibSound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pibMinuteFirst;
        private System.Windows.Forms.Timer tmrIncrease;
        private System.Windows.Forms.Timer tmrDecrease;
        private System.Windows.Forms.PictureBox pibMinuteLast;
        private System.Windows.Forms.PictureBox pibSecondLast;
        private System.Windows.Forms.PictureBox pibSecondFirst;
        private System.Windows.Forms.PictureBox pibSound;
        private System.Windows.Forms.PictureBox pibDot;
        private System.Windows.Forms.PictureBox pibMenu;
        private System.Windows.Forms.Panel pnBack;
        private System.Windows.Forms.PictureBox pibStatus;
        private System.Windows.Forms.PictureBox pibH1;
        private System.Windows.Forms.PictureBox pibH2;
        private System.Windows.Forms.PictureBox pibGL;
        private System.Windows.Forms.PictureBox pibIncrease;
        private System.Windows.Forms.PictureBox pibMain;
        private System.Windows.Forms.PictureBox pibDecrease;
        private System.Windows.Forms.PictureBox pibFullScreen;
        private System.Windows.Forms.PictureBox pibReset;
        private System.Windows.Forms.Panel pnOption;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbMinuteG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSecondG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMinuteH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSecondH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeafault;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pibMenuSub;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbMedia;
        private System.Windows.Forms.Button btnOpenSoundFile;
        private System.Windows.Forms.PictureBox pibPlaySound;
        private System.Windows.Forms.TextBox txtSoundPath;
        private System.Windows.Forms.OpenFileDialog opdSound;
        private System.Windows.Forms.GroupBox gbButton;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Timer tmrClock;
    }
}

