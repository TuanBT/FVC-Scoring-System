using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FVC_Stopwatch
{
    public partial class Form1 : Form
    {
        Image[] imageTime = new Image[10]
        {Properties.Resources._0,Properties.Resources._1,Properties.Resources._2,Properties.Resources._3,
        Properties.Resources._4,Properties.Resources._5,Properties.Resources._6,Properties.Resources._7,
        Properties.Resources._8,Properties.Resources._9};
        int s = 0, m = 0;
        string fileSoundPath = "";
        static string fileIniPath = "FVC_SW_Config.ini";
        string mh = "1", sh = "30", mg = "1", sg = "0";
        private string state = "Stopping";
        ToolTip tt = new ToolTip();
        TcpClients tcpClients = new TcpClients();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPGlobalProperties network = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] connections = network.GetActiveTcpListeners();
            if (connections.Length > 0)
            {
                foreach (var connection in connections)
                {
                    if (connection.Port == Variable.PORT)
                    {
                        Variable.IP = connection.Address.ToString();
                    }
                }
                txtIP.Text = Variable.IP;
                txtPort.Text = Variable.PORT.ToString();
            }
            setObject();
            setDeafaultTime();
            setFormTime();
            ShowToolTip();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            setObject();
            setObjectSub();
        }

        #region set config to read ini file
        private string path = "";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);
        public Form1(string thePath)
        {
            this.path = thePath;
        }
        public string ReadValue(string section, string key)
        {
            StringBuilder tmp = new StringBuilder(255);
            long i = GetPrivateProfileString(section, key, "", tmp, 255, this.path);
            return tmp.ToString();
        }
        public void WriteValue(string section, string key, string values)
        {
            WritePrivateProfileString(section, key, values, this.path);
        }

        #endregion

        #region Time
        void increase()
        {
            int first, last;
            s += 1;
            if (s == 60)
            {
                s = 0;
                if (m == 60)
                {
                    m = 0;
                }
                m += 1;
                first = m / 10;
                pibMinuteFirst.Image = imageTime[first];
                last = m % 10;
                pibMinuteLast.Image = imageTime[last];
            }
            first = s / 10;
            pibSecondFirst.Image = imageTime[first];
            last = s % 10;
            pibSecondLast.Image = imageTime[last];
        }
        void decrease()
        {
            int first, last;
            if (s == 0)
            {
                s = 60;
                if (m == 0)
                {
                    m = 60;
                }
                m -= 1;
                first = m / 10;
                pibMinuteFirst.Image = imageTime[first];
                last = m % 10;
                pibMinuteLast.Image = imageTime[last];

            }
            s -= 1;
            first = s / 10;
            pibSecondFirst.Image = imageTime[first];
            last = s % 10;
            pibSecondLast.Image = imageTime[last];
            if (m == 0 && s == 0)
            {
                FormEndTime();
            }
        }
        private void tmrIncrease_Tick(object sender, EventArgs e)
        {
            increase();
        }
        private void tmrDecrease_Tick(object sender, EventArgs e)
        {
            decrease();
        }
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            try
            {
                tcpClients.Sent(getClockJsonString());
            }
            catch (Exception)
            {
            }

            Console.WriteLine(state);
        }
        void RunTimer()
        {
            tmrIncrease.Enabled = false;
            tmrDecrease.Enabled = false;
            if (mainState == "Pause")
            {
                GetTimerAction().Enabled = true;

            }
        }
        Timer GetTimerAction()
        {
            if (timeState == "Decrease")
            {
                if (mainState == "Pause")
                {
                    return tmrDecrease;
                }
                if (type == "H1" || type == "H2")
                {
                    m = Convert.ToInt32(cbMinuteH.Text);
                    s = Convert.ToInt32(cbSecondH.Text);
                }
                else //type = "GL"
                {
                    m = Convert.ToInt32(cbMinuteG.Text);
                    s = Convert.ToInt32(cbSecondG.Text);
                }
                return tmrDecrease;
            }
            else //timeState = "Increase"
            {
                if (mainState == "Pause")
                {
                    return tmrIncrease;
                }
                m = 0;
                s = 0;
                return tmrIncrease;
            }
        }
        public string getClockJsonString()
        {
            ClockInfo clock = new ClockInfo
            {
                State = state,
                TimeState = timeState,
                Type = type,
                M = m,
                S = s
            };
            return clock.getClockJson(clock);
        }


        #endregion

        #region Interface

        void FormEndTime()
        {
            if (checkSound == true)
            {
                Form1 file = new Form1(Application.StartupPath + "\\" + fileIniPath);
                string fp = file.ReadValue("Media", "EndSound");
                if (fp != "")
                {
                    try
                    {
                        SoundPlayer Sound = new SoundPlayer(fp);
                        Sound.Play();
                    }
                    catch
                    {
                        SoundPlayer Sound = new SoundPlayer(Properties.Resources.EndSound);
                        Sound.Play();
                    }
                }
                else
                {
                    SoundPlayer Sound = new SoundPlayer(Properties.Resources.EndSound);
                    Sound.Play();
                }
            }
            tmrDecrease.Enabled = false;
            setFormStatus("Stopping");
            mainState = "Stop";
            pibMain.Visible = false;
        }

        void setFormSize()
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width / 4;
            this.Top = Screen.PrimaryScreen.Bounds.Height / 4;
            this.Width = Screen.PrimaryScreen.Bounds.Width / 2;
            this.Height = Screen.PrimaryScreen.Bounds.Height / 2;
        }

        void setObject()
        {
            //int wUnit = this.Width/16;
            //int hUnit = this.Height/9;

            int wUnit = pnBack.Width / 16;
            int hUnit = pnBack.Height / 9;

            //pibMinuteFirst
            pibMinuteFirst.Left = wUnit * 1;
            pibMinuteFirst.Top = hUnit * 3; ;
            pibMinuteFirst.Height = hUnit * 3; ;
            pibMinuteFirst.Width = wUnit * 3; ;

            //pibMinuteLast
            pibMinuteLast.Left = wUnit * 4;
            pibMinuteLast.Top = hUnit * 3;
            pibMinuteLast.Height = hUnit * 3;
            pibMinuteLast.Width = wUnit * 3;

            //pibSecondFirst
            pibSecondFirst.Left = wUnit * 9;
            pibSecondFirst.Top = hUnit * 3;
            pibSecondFirst.Height = hUnit * 3;
            pibSecondFirst.Width = wUnit * 3;

            //pibSecondLast
            pibSecondLast.Left = wUnit * 12;
            pibSecondLast.Top = hUnit * 3;
            pibSecondLast.Height = hUnit * 3;
            pibSecondLast.Width = wUnit * 3;

            //pibDot
            pibDot.Left = wUnit * 7;
            pibDot.Top = hUnit * 3;
            pibDot.Height = hUnit * 3;
            pibDot.Width = wUnit * 2;

            //pibMenu
            pibMenu.Left = wUnit * 1 / 2;
            pibMenu.Top = hUnit * 1 / 2;
            pibMenu.Height = hUnit * 3 / 2;
            pibMenu.Width = wUnit * 3 / 2;

            //pibStatus
            pibStatus.Left = wUnit * 13 / 2;
            pibStatus.Top = hUnit * 1 / 4;
            pibStatus.Height = hUnit * 1;
            pibStatus.Width = wUnit * 3;

            //pibH1
            pibH1.Left = wUnit * 6;
            pibH1.Top = hUnit * 3 / 2;
            pibH1.Height = hUnit * 1;
            pibH1.Width = wUnit * 1;

            //pibGL
            pibGL.Left = wUnit * 15 / 2;
            pibGL.Top = hUnit * 3 / 2;
            pibGL.Height = hUnit * 1;
            pibGL.Width = wUnit * 1;

            //pibH2
            pibH2.Left = wUnit * 9;
            pibH2.Top = hUnit * 3 / 2;
            pibH2.Height = hUnit * 1;
            pibH2.Width = wUnit * 1;

            //pibReset
            pibReset.Left = wUnit * 1 / 2;
            pibReset.Top = hUnit * 15 / 2;
            pibReset.Height = hUnit * 1;
            pibReset.Width = wUnit * 1;

            //pibMain
            pibMain.Left = wUnit * 7;
            pibMain.Top = hUnit * 13 / 2;
            pibMain.Height = hUnit * 2;
            pibMain.Width = wUnit * 2;

            //pibIncrease
            pibIncrease.Left = wUnit * 19 / 2;
            pibIncrease.Top = hUnit * 7;
            pibIncrease.Height = hUnit * 1;
            pibIncrease.Width = wUnit * 1;

            //pibDecrease
            pibDecrease.Left = wUnit * 11 / 2;
            pibDecrease.Top = hUnit * 7;
            pibDecrease.Height = hUnit * 1;
            pibDecrease.Width = wUnit * 1;

            //pibFullScreen
            pibFullScreen.Left = wUnit * 15;
            pibFullScreen.Top = hUnit * 0;
            pibFullScreen.Height = hUnit * 1 / 2;
            pibFullScreen.Width = wUnit * 1 / 2;

            //pibSound
            pibSound.Left = wUnit * 29 / 2;
            pibSound.Top = hUnit * 15 / 2;
            pibSound.Height = hUnit * 1;
            pibSound.Width = wUnit * 1;

        }

        void setObjectSub()
        {
            int wUnit = pnOption.Width / 16;
            int hUnit = pnOption.Height / 9;

            //pibMenu
            pibMenuSub.Left = wUnit * 1 / 2;
            pibMenuSub.Top = hUnit * 1 / 2;
            pibMenuSub.Height = hUnit * 3 / 2;
            pibMenuSub.Width = wUnit * 3 / 2;

            //gbTime
            gbTime.Left = wUnit * 1 / 2 + pibMenuSub.Width;
            gbTime.Top = hUnit * 1 / 2;

            //gbMedia
            gbMedia.Left = gbTime.Left + gbTime.Width + 5;
            gbMedia.Top = gbTime.Top;

            //gbButton
            gbButton.Left = gbTime.Left;
            gbButton.Top = gbTime.Width;
        }

        Boolean checkNumber(string str)
        {
            try
            {
                if (Convert.ToInt32(str) >= 0 && Convert.ToInt32(str) < 60)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        void setDeafaultTime()
        {
            Form1 file = new Form1(Application.StartupPath + "\\" + fileIniPath);
            string mht, sht, mgt, sgt; //h: Hiệp, G: Giải lao, t: temp, m:Minute, s:Second

            mht = file.ReadValue("Time", "RoundMinute");
            sht = file.ReadValue("Time", "RoundSecond");
            mgt = file.ReadValue("Time", "FreeMinute");
            sgt = file.ReadValue("Time", "FreeSecond");

            if (checkNumber(mht)) { cbMinuteH.Text = mht; }
            else { cbMinuteH.Text = mh; }

            if (checkNumber(sht)) { cbSecondH.Text = sht; }
            else { cbSecondH.Text = sh; }

            if (checkNumber(mgt)) { cbMinuteG.Text = mgt; }
            else { cbMinuteG.Text = mg; }

            if (checkNumber(sgt)) { cbSecondG.Text = sgt; }
            else { cbSecondG.Text = sg; }
        }

        public void ShowToolTip()
        {
            tt.ToolTipTitle = "Phím tắt";
            tt.BackColor = Color.Red;
            tt.ForeColor = Color.Yellow;
            tt.AutomaticDelay = 500;

            tt.SetToolTip(pibMenu, "M");
            tt.SetToolTip(pibMenuSub, "M");
            tt.SetToolTip(pibH1, "1");
            tt.SetToolTip(pibH2, "2");
            tt.SetToolTip(pibGL, "0");
            tt.SetToolTip(pibReset, "Esc");
            tt.SetToolTip(pibDecrease, "↑");
            tt.SetToolTip(pibIncrease, "↓");
            tt.SetToolTip(pibMain, "Enter - Space");
            tt.SetToolTip(pibSound, "S");
            tt.SetToolTip(pibFullScreen, "F11");
        }

        public void setFormTime()
        {
            this.Focus();
            int first, last;
            mainState = "Start";
            pibMain.Image = Properties.Resources.Start;
            pibMain.Visible = true;
            setFormStatus("Standing");
            tmrIncrease.Enabled = false;
            tmrDecrease.Enabled = false;
            if (timeState == "Decrease")
            {
                pibIncrease.Image = Properties.Resources.Increase;
                pibDecrease.Image = Properties.Resources.Decrease_Click;

                if (type != "GL")
                {
                    if (type == "")
                    {
                        type = "H1";
                    }
                    if (type == "H1")
                    {
                        pibH1.Image = Properties.Resources.H1_click;
                    }
                    else
                    {
                        pibH2.Image = Properties.Resources.H2_Click;
                    }
                    m = Convert.ToInt32(cbMinuteH.Text);
                    s = Convert.ToInt32(cbSecondH.Text);
                    first = m / 10;
                    pibMinuteFirst.Image = imageTime[first];
                    last = m % 10;
                    pibMinuteLast.Image = imageTime[last];
                    first = s / 10;
                    pibSecondFirst.Image = imageTime[first];
                    last = s % 10;
                    pibSecondLast.Image = imageTime[last];
                }
                else //type = "GL"
                {
                    m = Convert.ToInt32(cbMinuteG.Text);
                    s = Convert.ToInt32(cbSecondG.Text);
                    first = m / 10;
                    pibMinuteFirst.Image = imageTime[first];
                    last = m % 10;
                    pibMinuteLast.Image = imageTime[last];
                    first = s / 10;
                    pibSecondFirst.Image = imageTime[first];
                    last = s % 10;
                    pibSecondLast.Image = imageTime[last];
                }
            }
            else //timeState = "Increase"
            {
                type = "";
                pibH1.Image = Properties.Resources.H1;
                pibGL.Image = Properties.Resources.GL;
                pibH2.Image = Properties.Resources.H2;
                m = 0;
                s = 0;
                first = m / 10;
                pibMinuteFirst.Image = imageTime[first];
                last = m % 10;
                pibMinuteLast.Image = imageTime[last];
                first = s / 10;
                pibSecondFirst.Image = imageTime[first];
                last = s % 10;
                pibSecondLast.Image = imageTime[last];
            }

        }

        void setFormStatus(string status)
        {
            if (status == "Standing")
            {
                state = "Standing";
                pibStatus.Image = Properties.Resources.Status_Standing;
                pnBack.BackColor = Color.LightGray;
                pibDot.Image = Properties.Resources.Dot;
                pibSound.Visible = true;
                pibDecrease.Visible = true;
                pibIncrease.Visible = true;
            }
            if (status == "Running")
            {
                state = "Running";
                pibStatus.Image = Properties.Resources.Status_Running;
                pnBack.BackColor = Color.White;
                pibSound.Visible = true;
                pibDecrease.Visible = true;
                pibIncrease.Visible = true;
            }
            if (status == "Pausing")
            {
                state = "Pausing";
                pibStatus.Image = Properties.Resources.Status_Pausing;
                pnBack.BackColor = Color.Yellow;
                pibSound.Visible = false;
                pibDecrease.Visible = false;
                pibIncrease.Visible = false;
            }
            if (status == "Stopping")
            {
                state = "Stopping";
                pibStatus.Image = Properties.Resources.Status_Stopping;
                pnBack.BackColor = Color.Red;
                pibMinuteFirst.Image = Properties.Resources._0Yellow;
                pibMinuteLast.Image = Properties.Resources._0Yellow;
                pibSecondFirst.Image = Properties.Resources._0Yellow;
                pibSecondLast.Image = Properties.Resources._0Yellow;
                pibDot.Image = Properties.Resources.DotYellow;
                pibSound.Visible = false;
                pibDecrease.Visible = false;
                pibIncrease.Visible = false;
            }
        }




        #endregion


        #region click button

        //pibFullScreen
        bool stateForm = false; //False nghĩa là form chưa full màn hình
        void pibFullScreenClick()
        {
            if (stateForm == false) //Đang resize
            {
                pibFullScreen.Image = Properties.Resources.FullScreenDown;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Left = 0;
                this.Top = 0;
                //--
                //this.Width = 1024;
                //this.Height = 768;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                //--
                this.TopMost = true;
                setObject();
                stateForm = true;
            }
            else //Đang fullScreen
            {
                pibFullScreen.Image = Properties.Resources.FullScreen;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
                setFormSize();
                setObject();
                stateForm = false;
            }
        }
        private void pibFullScreen_Click(object sender, EventArgs e)
        {
            pibFullScreenClick();
        }
        private void pibFullScreen_MouseEnter(object sender, EventArgs e)
        {
            if (stateForm == false) //Đang resize
            {
                pibFullScreen.Image = Properties.Resources.FullScreen_Mouse;
            }
            else //Đang fullScreen
            {
                pibFullScreen.Image = Properties.Resources.FullScreenDown_Mouse;
            }

        }
        private void pibFullScreen_MouseLeave(object sender, EventArgs e)
        {
            if (stateForm == false) //Đang resize
            {
                pibFullScreen.Image = Properties.Resources.FullScreen;
            }
            else //Đang fullScreen
            {
                pibFullScreen.Image = Properties.Resources.FullScreenDown;
            }
        }

        //pibReset
        int statusReset = 0;
        void pibResetClick()
        {
            if (statusReset == 0)
            {
                pibReset.Image = Properties.Resources.Reset1;
                statusReset = 1;
            }
            else
            {
                pibReset.Image = Properties.Resources.Reset2;
                statusReset = 0;
            }
            timeState = "Decrease";
            setFormTime();
        }
        private void pibReset_Click(object sender, EventArgs e)
        {
            pibResetClick();
        }
        private void pibReset_MouseEnter(object sender, EventArgs e)
        {
            pibReset.Image = Properties.Resources.Reset_Mouse;
        }
        private void pibReset_MouseLeave(object sender, EventArgs e)
        {
            pibReset.Image = Properties.Resources.Reset1;
        }

        String timeState = "Decrease"; //2 trạng thái thời gian Decrease và Increase
        //pibIncrease
        //Mỗi lần ấn thì cũng reset lại
        void pibIncreaseClick()
        {
            timeState = "Increase";
            setFormTime();
            pibIncrease.Image = Properties.Resources.Increase_Click;
            pibDecrease.Image = Properties.Resources.Decrease;
        }
        private void pibIncrease_Click(object sender, EventArgs e)
        {
            pibIncreaseClick();
        }
        private void pibIncrease_MouseEnter(object sender, EventArgs e)
        {
            if (timeState != "Increase") //Chưa được click
            {
                pibIncrease.Image = Properties.Resources.Increase_Mouse;
            }
            else //Đang được click
            {
                pibIncrease.Image = Properties.Resources.Increase_Click_Mouse;
            }
        }
        private void pibIncrease_MouseLeave(object sender, EventArgs e)
        {
            if (timeState != "Increase") //Chưa được click
            {
                pibIncrease.Image = Properties.Resources.Increase;
            }
            else //Đang được click
            {
                pibIncrease.Image = Properties.Resources.Increase_Click;
            }
        }

        //pibDecrease
        void pibDecreaseClick()
        {
            timeState = "Decrease";
            setFormTime();
            pibDecrease.Image = Properties.Resources.Decrease_Click;
            pibIncrease.Image = Properties.Resources.Increase;
        }
        private void pibDecrease_Click(object sender, EventArgs e)
        {
            pibDecreaseClick();
        }
        private void pibDecrease_MouseEnter(object sender, EventArgs e)
        {
            if (timeState != "Decrease") //Chưa được click
            {
                pibDecrease.Image = Properties.Resources.Decrease_Mouse;
            }
            else //Đang được click
            {
                pibDecrease.Image = Properties.Resources.Decrease_Click_Mouse;
            }
        }
        private void pibDecrease_MouseLeave(object sender, EventArgs e)
        {
            if (timeState != "Decrease") //Chưa được click
            {
                pibDecrease.Image = Properties.Resources.Decrease;
            }
            else //Đang được click
            {
                pibDecrease.Image = Properties.Resources.Decrease_Click;
            }
        }

        string type = "H1"; //Các kiểu Type: H1, GL, H2
        //pibbH1
        void pibH1Click()
        {
            type = "H1";
            timeState = "Decrease";
            setFormTime();
            pibH1.Image = Properties.Resources.H1_click;
            pibGL.Image = Properties.Resources.GL;
            pibH2.Image = Properties.Resources.H2;
        }
        private void pibH1_Click(object sender, EventArgs e)
        {
            pibH1Click();
        }
        private void pibH1_MouseEnter(object sender, EventArgs e)
        {
            if (type != "H1") //Chưa được click
            {
                pibH1.Image = Properties.Resources.H1_Mouse;
            }
            else if (timeState != "Increase") //Đang được click
            {
                pibH1.Image = Properties.Resources.H1_click_Mouse;
            }
        }
        private void pibH1_MouseLeave(object sender, EventArgs e)
        {
            if (type != "H1") //Chưa được click
            {
                pibH1.Image = Properties.Resources.H1;
            }
            else if (timeState != "Increase") //Đang được click
            {
                pibH1.Image = Properties.Resources.H1_click;
            }
        }

        //pibGL
        void pibGLClick()
        {
            type = "GL";
            timeState = "Decrease";
            setFormTime();
            pibGL.Image = Properties.Resources.GL_Click;
            pibH1.Image = Properties.Resources.H1;
            pibH2.Image = Properties.Resources.H2;
        }
        private void pibGL_Click(object sender, EventArgs e)
        {
            pibGLClick();
        }
        private void pibGL_MouseEnter(object sender, EventArgs e)
        {
            if (type != "GL")
            {
                pibGL.Image = Properties.Resources.GL_Mouse;
            }
            else if (timeState != "Increase")
            {
                pibGL.Image = Properties.Resources.GL_Click_Mouse;
            }
        }
        private void pibGL_MouseLeave(object sender, EventArgs e)
        {
            if (type != "GL")
            {
                pibGL.Image = Properties.Resources.GL;
            }
            else if (timeState != "Increase")
            {
                pibGL.Image = Properties.Resources.GL_Click;
            }
        }

        //pibH2
        void pibH2Click()
        {
            type = "H2";
            timeState = "Decrease";
            setFormTime();
            pibH2.Image = Properties.Resources.H2_Click;
            pibGL.Image = Properties.Resources.GL;
            pibH1.Image = Properties.Resources.H1;
        }
        private void pibH2_Click(object sender, EventArgs e)
        {
            pibH2Click();
        }
        private void pibH2_MouseEnter(object sender, EventArgs e)
        {
            if (type != "H2")
            {
                pibH2.Image = Properties.Resources.H2_Mouse;
            }
            else if (timeState != "Increase")
            {
                pibH2.Image = Properties.Resources.H2_Click_Mouse;
            }
        }
        private void pibH2_MouseLeave(object sender, EventArgs e)
        {
            if (type != "H2")
            {
                pibH2.Image = Properties.Resources.H2;
            }
            else if (timeState != "Increase")
            {
                pibH2.Image = Properties.Resources.H2_Click;
            }
        }

        //pibSound
        Boolean checkSound = true; //True tức là có tiếng
        void pibSoundClick()
        {
            if (checkSound)
            {
                checkSound = false;
                pibSound.Image = Properties.Resources.Sound_Mute;
            }
            else
            {
                checkSound = true;
                pibSound.Image = Properties.Resources.Sound;
            }
        }
        private void pibSound_Click(object sender, EventArgs e)
        {
            pibSoundClick();
        }
        private void pibSound_MouseEnter(object sender, EventArgs e)
        {
            if (checkSound)
            {
                pibSound.Image = Properties.Resources.Sound_Mouse;
            }
            else
            {
                pibSound.Image = Properties.Resources.Sound_Mute_Mouse;
            }
        }
        private void pibSound_MouseLeave(object sender, EventArgs e)
        {
            if (checkSound)
            {
                pibSound.Image = Properties.Resources.Sound;
            }
            else
            {
                pibSound.Image = Properties.Resources.Sound_Mute;
            }
        }

        //pibMenu
        void pibMenuClick()
        {
            pnOption.Visible = true;
            pnOption.Dock = DockStyle.Fill; //test
            setObjectSub();
        }
        private void pibMenu_Click(object sender, EventArgs e)
        {
            pibMenuClick();
        }
        private void pibMenu_MouseEnter(object sender, EventArgs e)
        {
            pibMenu.Image = Properties.Resources.Menu_mouse;
        }
        private void pibMenu_MouseLeave(object sender, EventArgs e)
        {
            pibMenu.Image = Properties.Resources.Menu;
        }

        string mainState = "Start"; //Trạng thái lúc ấn nút Run. Có các trạng thái: St (Start) và Pu (Pause) và Sto (Stop)
        //pibMain
        void pibMainClick()
        {
            if (mainState == "Stop")
            {
            }
            else if (mainState == "Start") //Hiện nút start
            {
                mainState = "Pause";
                RunTimer();
                pibMain.Image = Properties.Resources.Pause;
                setFormStatus("Running");
            }
            else //Hiện nút pause
            {
                mainState = "Start";
                RunTimer();
                pibMain.Image = Properties.Resources.Start;
                setFormStatus("Pausing");
            }
        }
        private void pibMain_Click(object sender, EventArgs e)
        {
            pibMainClick();
        }
        private void pibMain_MouseEnter(object sender, EventArgs e)
        {
            if (mainState == "Start") //Hiện nút start
            {
                pibMain.Image = Properties.Resources.Start_Mouse;
            }
            else //Hiện nút pause
            {
                pibMain.Image = Properties.Resources.Pause_Mouse;
            }
        }
        private void pibMain_MouseLeave(object sender, EventArgs e)
        {
            if (mainState == "Start") //Hiện nút start
            {
                pibMain.Image = Properties.Resources.Start;
            }
            else //Hiện nút pause
            {
                pibMain.Image = Properties.Resources.Pause;
            }
        }

        //pibMenuSub
        void pibMenuSubClick()
        {
            pnOption.Visible = false;
        }
        private void pibMenuSub_Click(object sender, EventArgs e)
        {
            pibMenuSubClick();
        }
        private void pibMenuSub_MouseEnter(object sender, EventArgs e)
        {
            pibMenuSub.Image = Properties.Resources.Menu_Click_Mouse;
        }
        private void pibMenuSub_MouseLeave(object sender, EventArgs e)
        {
            pibMenuSub.Image = Properties.Resources.Menu_Click;
        }

        //btnOK
        private void btnOK_Click(object sender, EventArgs e)
        {
            Form1 file = new Form1(Application.StartupPath + "\\" + fileIniPath);
            file.WriteValue("Time", "RoundMinute", cbMinuteH.Text);
            file.WriteValue("Time", "RoundSecond", cbSecondH.Text);
            file.WriteValue("Time", "FreeMinute", cbMinuteG.Text);
            file.WriteValue("Time", "FreeSecond", cbSecondG.Text);
            pnOption.Visible = false;
            setFormStatus("Standing");
            setFormTime();
        }

        //btnDeafault
        private void btnDeafault_Click(object sender, EventArgs e)
        {
            cbMinuteH.Text = mh;
            cbSecondH.Text = sh;
            cbMinuteG.Text = mg;
            cbSecondG.Text = sg;
            txtSoundPath.Text = "";
            pibPlaySound.Visible = false;
            Form1 file = new Form1(Application.StartupPath + "\\" + fileIniPath);
            file.WriteValue("Media", "EndSound", "");
            setFormStatus("Standing");
            setFormTime();
        }

        //btnCancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnOption.Visible = false;
            setDeafaultTime();
        }

        //btnOpenSoundFile
        private void btnOpenSoundFile_Click(object sender, EventArgs e)
        {
            DialogResult result = opdSound.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                fileSoundPath = opdSound.FileName;
                try
                {
                    pibPlaySound.Visible = true;
                    txtSoundPath.Text = fileSoundPath;
                    Form1 file = new Form1(Application.StartupPath + "\\" + fileIniPath);
                    file.WriteValue("Media", "EndSound", fileSoundPath);
                }
                catch
                {
                }
            }
        }

        //pibPlaySound
        private void pibPlaySound_Click(object sender, EventArgs e)
        {
            if (fileSoundPath != "")
            {
                pibPlaySound.Image = Properties.Resources.PlaySound_Click;
                SoundPlayer simpleSound = new SoundPlayer(fileSoundPath);
                simpleSound.Play();
            }
        }
        private void pibPlaySound_MouseEnter(object sender, EventArgs e)
        {
            pibPlaySound.Image = Properties.Resources.PlaySound_Mouse;
        }
        private void pibPlaySound_MouseLeave(object sender, EventArgs e)
        {
            pibPlaySound.Image = Properties.Resources.PlaySound;
        }

        //btnAbout
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Variable.IP = txtIP.Text;
            Variable.PORT =Convert.ToInt32(txtPort.Text);
             if (tcpClients.Connection())
             {
                 tmrClock.Enabled = true;
                 MessageBox.Show("Kết nối thành công tới máy trọng tài chính!");
             }
             else
             {
                 MessageBox.Show("Kết nối tới máy trọng tài chính thất bại!");
             }
        }

        #endregion


        #region keyDown
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                pibFullScreenClick();
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                pibMainClick();
            }
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                pibH1Click();
            }
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                pibGLClick();
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                pibH2Click();
            }
            if (e.KeyCode == Keys.Escape)
            {
                pibResetClick();
            }
            if (e.KeyCode == Keys.Up)
            {
                pibIncreaseClick();
            }
            if (e.KeyCode == Keys.Down)
            {
                pibDecreaseClick();
            }
            if (e.KeyCode == Keys.S)
            {
                pibSoundClick();
            }
            if (e.KeyCode == Keys.M)
            {
                if (pnOption.Visible == false)
                {
                    pibMenuClick();
                }
                else
                {
                    pibMenuSubClick();
                }
            }
        }
        #endregion





        #region test
        #endregion


    }
}
