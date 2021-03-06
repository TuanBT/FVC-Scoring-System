﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using FSSServer.Data;
using Server;

namespace FSSServer
{
    public partial class ServerForm : Form
    {
        TcpServers tcpServers;
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;
        private FillData fillData;
        int s = 0;
        int m = 0;

        public ServerForm()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in localIPs)
            {
                if (ip.ToString().IndexOf('.') > 0)
                {
                    Variable.IP = ip.ToString();
                }
            }

            //Variable.IP = "127.0.0.1";

            InitializeComponent();

            this.Text = "Trọng tài chính: " + Variable.IP + ":" + Variable.PORT;

            tcpServers = new TcpServers(this);
            Variable.SERVERSERVICES = new List<ServerService>();
            Variable.THREADS = new List<Thread>();

            // Add Event to handle when a client is connected
            Changed += new ChangedEventHandler(tcpServers.ClientAdded);

            tcpServers.StartServer();
            //tmrServer.Enabled = true;

            //Chuẩn bị database
            Server.Variable.FIGHTERS = TextDataUltil.ReadFighterText();
            Server.Variable.MATCHES = TextDataUltil.ReadMatchText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pnlMain.Left = (this.Width - pnlMain.Width) / 2;
            //pnlMain.Top = (this.Height - pnlMain.Height) / 2;
            pnlSetting.Width = pnlMain.Width;
            pnlSetting.Height = pnlMain.Height;
            pnlSetting.Left = 0;
            pnlSetting.Top = 0;

            fillData = new FillData(nmrNumberMatch, cbbSex, cbbWeight,
                txtNameRed, txtNameBlue, txtIdRed, txtClassBlue, txtIdBlue, txtClassRed);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                this.Left = 0;
                this.Top = 0;
                //this.Bounds = Screen.PrimaryScreen.Bounds;
                this.TopMost = true;
            }
            pnlMain.Left = (this.ClientSize.Width - pnlMain.Width) / 2;
            pnlMain.Top = (this.ClientSize.Height - pnlMain.Height) / 2;
            //pnlSetting.Left = 0;
            //pnlSetting.Top = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thực sự muốn tắt phần mềm?", "Tắt phần mềm!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                tcpServers.StopServer();
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void tmrServer_Tick(object sender, EventArgs e)
        {
            //Nếu máy con chưa được cấp quyền chấm điểm.
            btnSent.Visible = Variable.SEC == -1;
            if (Variable.SEC == -1)
                btnSent.Visible = true;
            else
                btnSent.Visible = false;

            try
            {
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }
            }
            catch (Exception)
            {
                //Không tồn tại kết nối nào
            }

            setFormFromClientJson();
        }

        private void tmrServerReceive_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    //Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }
            }
            catch (Exception)
            {
                //Không tồn tại kết nối nào
            }
        }

        private void tmrTimeout_Tick(object sender, EventArgs e)
        {
            try
            {
                Variable.RECEIVETEXT1 = Variable.RECEIVETEXT1.Replace("\"EndMath\":1", "\"EndMath\":-1");
                Variable.RECEIVETEXT1 = Variable.RECEIVETEXT1.Replace("\"EndMath\":0", "\"EndMath\":-1");

                Variable.RECEIVETEXT2 = Variable.RECEIVETEXT2.Replace("\"EndMath\":1", "\"EndMath\":-1");
                Variable.RECEIVETEXT2 = Variable.RECEIVETEXT2.Replace("\"EndMath\":0", "\"EndMath\":-1");

                Variable.RECEIVETEXT3 = Variable.RECEIVETEXT3.Replace("\"EndMath\":1", "\"EndMath\":-1");
                Variable.RECEIVETEXT3 = Variable.RECEIVETEXT3.Replace("\"EndMath\":0", "\"EndMath\":-1");
            }
            catch (Exception) { }


            //Variable.RECEIVETEXT2 = "";
            //Variable.RECEIVETEXT3= "";
            //Variable.RECEIVETEXTCLOCK = "";
        }

        public string getServerJsonString()
        {
            ServerInfo serverInfo = new ServerInfo
            {
                Math = lblNumberMatch.Text,
                Sex = lblSex.Text,
                Time = (m / 10).ToString() + (m % 10).ToString() + ":" + (s / 10).ToString() + (s % 10).ToString(),
                Weight = lblWeight.Text,
                Sec = Variable.SEC,
                State = Variable.STATE
            };
            return serverInfo.getClientJson(serverInfo);
        }

        public void setFormFromClientJson()
        {
            try
            {
                if (Variable.RECEIVETEXT1 != "")
                {

                    try
                    {
                        ClientInfo clientInfo1 = new ClientInfo(Variable.RECEIVETEXT1);
                        //Kiểm tra việc có đang chấm hay không?
                        if (clientInfo1.EndMath == 1)
                        {
                            //Đang chấm
                            ChangeSatusMath(lblStatusScoreM1, true);

                            lblStatusScoreM1.Visible = true;
                            lblRefereeM1.Text = clientInfo1.Referee;
                            lblScoreRedM1.Text = clientInfo1.ScoreRed.ToString();
                            lblScoreBlueM1.Text = clientInfo1.ScoreBlue.ToString();
                            lblWinFormM1.Text = clientInfo1.WinForm;
                            ChangeStatus(lblStatusM1, true);

                            //Kiểm tra thắng
                            if (clientInfo1.Win == "RED")
                            {
                                lblPlusRedM1.Visible = true;
                                lblPlusBlueM1.Visible = false;
                            }
                            if (clientInfo1.Win == "BLUE")
                            {
                                lblPlusRedM1.Visible = false;
                                lblPlusBlueM1.Visible = true;
                            }
                            //Variable.RECEIVETEXT1 = null;
                        }
                        //clientInfo1.EndMath == 0
                        else if (clientInfo1.EndMath == 0)
                        {
                            //Đã chấm xong
                            ChangeSatusMath(lblStatusScoreM1, false);
                            ChangeStatus(lblStatusM1, true);
                            // btnIncRedM1.Visible = true;
                            //btnDecRedM1.Visible = true;
                            // btnIncBlueM1.Visible = true;
                            // btnDecBlueM1.Visible = true;
                        }
                        else if (clientInfo1.EndMath == -1)
                        {
                            ChangeStatus(lblStatusM1, false);
                        }
                    }
                    catch (Exception) { }
                }
                //Không nhận được dữ liệu nữa
                else
                {
                    ChangeStatus(lblStatusM1, false);
                    lblScoreRedM1.Text = "0";
                    lblScoreBlueM1.Text = "0";
                    lblWinFormM1.Text = "-";
                    lblRefereeM1.Text = "-";
                    lblPlusRedM1.Visible = false;
                    lblPlusBlueM1.Visible = false;
                    lblStatusScoreM1.Visible = false;
                }

                //Máy 2
                if (Variable.RECEIVETEXT2 != "")
                {
                    try
                    {
                        ClientInfo clientInfo2 = new ClientInfo(Variable.RECEIVETEXT2);
                        if (clientInfo2.EndMath == 1)
                        {
                            ChangeSatusMath(lblStatusScoreM2, true);

                            lblStatusScoreM2.Visible = true;
                            lblRefereeM2.Text = clientInfo2.Referee;
                            lblScoreRedM2.Text = clientInfo2.ScoreRed.ToString();
                            lblScoreBlueM2.Text = clientInfo2.ScoreBlue.ToString();
                            lblWinFormM2.Text = clientInfo2.WinForm;
                            ChangeStatus(lblStatusM2, true);

                            if (clientInfo2.Win == "RED")
                            {
                                lblPlusRedM2.Visible = true;
                                lblPlusBlueM2.Visible = false;
                            }
                            if (clientInfo2.Win == "BLUE")
                            {
                                lblPlusRedM2.Visible = false;
                                lblPlusBlueM2.Visible = true;
                            }
                            //Variable.RECEIVETEXT2 = null;
                        }
                        else if (clientInfo2.EndMath == 0)
                        {
                            ChangeSatusMath(lblStatusScoreM2, false);
                            ChangeStatus(lblStatusM2, true);
                            // btnIncRedM2.Visible = true;
                            //  btnDecRedM2.Visible = true;
                            //  btnIncBlueM2.Visible = true;
                            // btnDecBlueM2.Visible = true;
                        }
                        else if (clientInfo2.EndMath == -1)
                        {
                            ChangeStatus(lblStatusM2, false);
                        }
                    }
                    catch (Exception) { }

                }
                else
                {
                    ChangeStatus(lblStatusM2, false);
                    lblScoreRedM2.Text = "0";
                    lblScoreBlueM2.Text = "0";
                    lblWinFormM2.Text = "-";
                    lblRefereeM2.Text = "-";
                    lblPlusRedM2.Visible = false;
                    lblPlusBlueM2.Visible = false;
                    lblStatusScoreM2.Visible = false;
                }

                //Máy 3
                if (Variable.RECEIVETEXT3 != "")
                {
                    try
                    {
                        ClientInfo clientInfo3 = new ClientInfo(Variable.RECEIVETEXT3);
                        if (clientInfo3.EndMath == 1)
                        {
                            ChangeSatusMath(lblStatusScoreM3, true);
                            lblStatusScoreM3.Visible = true;
                            lblRefereeM3.Text = clientInfo3.Referee;
                            lblScoreRedM3.Text = clientInfo3.ScoreRed.ToString();
                            lblScoreBlueM3.Text = clientInfo3.ScoreBlue.ToString();
                            lblWinFormM3.Text = clientInfo3.WinForm;
                            ChangeStatus(lblStatusM3, true);

                            if (clientInfo3.Win == "RED")
                            {
                                lblPlusRedM3.Visible = true;
                                lblPlusBlueM3.Visible = false;
                            }
                            if (clientInfo3.Win == "BLUE")
                            {
                                lblPlusRedM3.Visible = false;
                                lblPlusBlueM3.Visible = true;
                            }
                            //Variable.RECEIVETEXT3 = null;
                        }
                        else if (clientInfo3.EndMath == 0)
                        {
                            ChangeSatusMath(lblStatusScoreM3, false);
                            ChangeStatus(lblStatusM3, true);
                            //btnIncRedM3.Visible = true;
                            //btnDecRedM3.Visible = true;
                            //btnIncBlueM3.Visible = true;
                            //btnDecBlueM3.Visible = true;
                        }
                        else if (clientInfo3.EndMath == -1)
                        {
                            ChangeStatus(lblStatusM3, false);
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    ChangeStatus(lblStatusM3, false);
                    lblScoreRedM3.Text = "0";
                    lblScoreBlueM3.Text = "0";
                    lblWinFormM3.Text = "-";
                    lblRefereeM3.Text = "-";
                    lblPlusRedM3.Visible = false;
                    lblPlusBlueM3.Visible = false;
                    lblStatusScoreM3.Visible = false;
                }

                if (Variable.RECEIVETEXTCLOCK != null)
                {
                    setClockFormClockJson();
                }
                else
                {
                    lblClock.Text = "00:00";
                    lblClock.BackColor = Color.LightGray;
                }

                UpdateScore();
            }
            catch (Exception ex) { }
        }

        public void setClockFormClockJson()
        {
            try
            {
                ClockInfo clockInfo = new ClockInfo(Variable.RECEIVETEXTCLOCK);
                if (clockInfo.State == "Standing")
                {
                    lblClock.BackColor = Color.LightGray;
                    lblSec.BackColor = Color.LightGray;
                    Variable.STATE = "Standing";
                }
                else if (clockInfo.State == "Running")
                {
                    lblClock.BackColor = Color.White;
                    lblSec.BackColor = Color.White;
                    Variable.STATE = "Running";
                }
                else if (clockInfo.State == "Pausing")
                {
                    lblClock.BackColor = Color.Yellow;
                    lblSec.BackColor = Color.Yellow;
                    Variable.STATE = "Pausing";
                }
                else if (clockInfo.State == "Stopping")
                {
                    lblClock.BackColor = Color.Red;
                    lblSec.BackColor = Color.Red;
                    Variable.STATE = "Stopping";
                }

                if (clockInfo.Type == "H1")
                {
                    lblSec.Text = "Hiệp 1";
                    Variable.SEC = 1;
                }
                else if (clockInfo.Type == "GL")
                {
                    lblSec.Text = "Giải lao";
                    Variable.SEC = 0;
                }
                else if (clockInfo.Type == "H2")
                {
                    lblSec.Text = "Hiệp 2";
                    Variable.SEC = 2;
                }

                if (clockInfo.TimeState == "Decrease ")
                {

                }
                else if (clockInfo.TimeState == "Increase")
                {

                }
                s = clockInfo.S;
                m = clockInfo.M;
                lblClock.Text = (m / 10).ToString() + (m % 10).ToString() + ":" + (s / 10).ToString() + (s % 10).ToString();
            }
            catch (Exception)
            {
            }
        }

        public void ChangeStatus(Label label, bool status)
        {
            if (status)
            {
                label.Text = "Kết nối";
                label.BackColor = Color.Green;
            }
            else if (!status)
            {
                label.Text = "...";
                label.BackColor = Color.FromArgb(255, 128, 0);
            }
        }

        public void ChangeSatusMath(Label label, bool status)
        {
            if (status)
            {
                label.Text = "Đang chấm";
                label.BackColor = Color.FromArgb(255, 128, 0);
            }
            else if (!status)
            {
                label.Text = "Đã chấm";
                label.BackColor = Color.Green;

            }
        }

        public void UpdateScore()
        {
            lblTotalScoreRed.Text = (Int32.Parse(lblScoreRedM1.Text) + Int32.Parse(lblScoreRedM2.Text) + Int32.Parse(lblScoreRedM3.Text)).ToString();
            lblTotalScoreBlue.Text = (Int32.Parse(lblScoreBlueM1.Text) + Int32.Parse(lblScoreBlueM2.Text) + Int32.Parse(lblScoreBlueM3.Text)).ToString();

            int redWin = 0;
            int blueWin = 0;

            if (lblPlusRedM1.Visible)
                redWin++;
            if (lblPlusBlueM1.Visible)
                blueWin++;
            if (lblPlusRedM2.Visible)
                redWin++;
            if (lblPlusBlueM2.Visible)
                blueWin++;
            if (lblPlusRedM3.Visible)
                redWin++;
            if (lblPlusBlueM3.Visible)
                blueWin++;

            lblTotalWinRed.Text = redWin.ToString();
            lblTotalWinBlue.Text = blueWin.ToString();

            if (redWin > blueWin)
            {
                Variable.WIN = "RED";
                lblPlusRed.Visible = true;
                lblPlusBlue.Visible = false;
            }
            else if (redWin < blueWin)
            {
                Variable.WIN = "BLUE";
                lblPlusRed.Visible = false;
                lblPlusBlue.Visible = true;
            }
            else
            {
                Variable.WIN = "";
            }
            if (Variable.WIN == "RED")
            {
                btnWinRed.BackColor = Color.Red;
                btnWinBlue.BackColor = Color.Silver;
                lblWinName.Text = lblNameRed.Text;
                lblWinId.Text = lblIdRed.Text;
                lblWinClass.Text = lblClassRed.Text;
                lblWinName.ForeColor = Color.Red;
                lblWinId.ForeColor = Color.Red;
                lblWinClass.ForeColor = Color.Red;
            }
            else if (Variable.WIN == "BLUE")
            {
                btnWinRed.BackColor = Color.Silver;
                btnWinBlue.BackColor = Color.Blue;
                lblWinName.Text = lblNameBlue.Text;
                lblWinId.Text = lblIdBlue.Text;
                lblWinClass.Text = lblClassBlue.Text;
                lblWinName.ForeColor = Color.Blue;
                lblWinId.ForeColor = Color.Blue;
                lblWinClass.ForeColor = Color.Blue;
            }
            //Hoa
            else
            {
                btnWinRed.BackColor = Color.Silver;
                btnWinBlue.BackColor = Color.Silver;
                lblWinName.Text = "";
                lblWinId.Text = "";
                lblWinClass.Text = "";
                lblPlusRed.Visible = false;
                lblPlusBlue.Visible = false;
            }
        }

        public void setFormSetting()
        {
            lblRefereeMain.Text = txtRefereeMain.Text;
            lblSecretary.Text = txtSecretary.Text;

            lblWeight.Text = cbbWeight.Text;
            lblSex.Text = cbbSex.Text;
            lblNumberMatch.Text = nmrNumberMatch.Value.ToString();

            lblNameRed.Text = txtNameRed.Text;
            lblIdRed.Text = txtIdRed.Text;
            lblClassRed.Text = txtClassRed.Text;
            lblNameBlue.Text = txtNameBlue.Text;
            lblIdBlue.Text = txtIdBlue.Text;
            lblClassBlue.Text = txtClassBlue.Text;

            btnSent.Visible = true;
        }

        private void nmrNumberMatch_ValueChanged(object sender, EventArgs e)
        {
            fillData.FillFromMatch(nmrNumberMatch);
            lblNameWin.Text = fillData.FillWinname(nmrNumberMatch);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int numMatch = Convert.ToInt32(nmrNumberMatch.Value);
            if (numMatch == 100)
                return;
            nmrNumberMatch.Value = numMatch + 1;
            fillData.FillFromMatch(nmrNumberMatch);
            lblNameWin.Text = fillData.FillWinname(nmrNumberMatch);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int numMatch = Convert.ToInt32(nmrNumberMatch.Value);
            if (numMatch == 0)
                return;
            nmrNumberMatch.Value = numMatch - 1;
            fillData.FillFromMatch(nmrNumberMatch);
            lblNameWin.Text = fillData.FillWinname(nmrNumberMatch);
        }

        private void btnSettingOk_Click(object sender, EventArgs e)
        {
            //Nếu 1 trong 2 tên chưa được điền thì bỏ qua
            if (txtNameRed.Text == "" || txtNameBlue.Text == "")
            {
                MessageBox.Show("Phải chọn cả hai vận động viên để thi đấu!");
                return;
            }
            //Nếu trùng tên thì bỏ qua
            if (txtNameRed.Text == txtNameBlue.Text)
            {
                MessageBox.Show("Phải chọn 2 vận động viên khác nhau!");
                return;
            }
            //Nếu trận đấu đã được chấm
            if (fillData.isFighted(nmrNumberMatch.Value.ToString()))
            {
                DialogResult dialogResult = MessageBox.Show("Trận đấu đã được chấm điểm, bạn thực sự muốn chấm lại?", "Tiếp tục chấm?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            pnlSetting.Visible = false;
            lblScoreRedM1.Text = "0";
            lblScoreBlueM1.Text = "0";
            lblRefereeM1.Text = "-";
            lblWinFormM1.Text = "-";
            lblPlusRedM1.Visible = false;
            lblPlusBlueM1.Visible = false;
            lblStatusScoreM1.Visible = false;

            lblScoreRedM2.Text = "0";
            lblScoreBlueM2.Text = "0";
            lblRefereeM2.Text = "-";
            lblWinFormM2.Text = "-";
            lblPlusRedM2.Visible = false;
            lblPlusBlueM2.Visible = false;
            lblStatusScoreM2.Visible = false;

            lblScoreRedM3.Text = "0";
            lblScoreBlueM3.Text = "0";
            lblRefereeM3.Text = "-";
            lblWinFormM3.Text = "-";
            lblPlusRedM3.Visible = false;
            lblPlusBlueM3.Visible = false;
            lblStatusScoreM3.Visible = false;

            lblClock.Text = "00:00";
            lblClock.BackColor = Color.LightGray;

            UpdateScore();

            setFormSetting();
            Variable.SEC = -1;
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            Variable.SEC = 1;
            tmrServer.Enabled = true;
            tmrTimeout.Enabled = true;
            //tmrServerReceive.Enabled = true;
        }

        private void btnWinBlue_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn dừng chấm điểm và để XANH thắng?", "Xanh thắng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                btnWinRed.BackColor = Color.Silver;
                btnWinBlue.BackColor = Color.Blue;
                lblWinName.Text = lblNameBlue.Text;
                lblWinId.Text = lblIdBlue.Text;
                lblWinClass.Text = lblClassBlue.Text;
                lblWinName.ForeColor = Color.Blue;
                lblWinId.ForeColor = Color.Blue;
                lblWinClass.ForeColor = Color.Blue;

                //tmrServerReceive.Enabled = false;
                tmrServer.Enabled = false;
                Variable.SEC = -1;
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnWinRed_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn dừng chấm điểm và để Đỏ thắng?", "Đỏ thắng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                btnWinRed.BackColor = Color.Red;
                btnWinBlue.BackColor = Color.Silver;
                lblWinName.Text = lblNameRed.Text;
                lblWinId.Text = lblIdRed.Text;
                lblWinClass.Text = lblClassRed.Text;
                lblWinName.ForeColor = Color.Red;
                lblWinId.ForeColor = Color.Red;
                lblWinClass.ForeColor = Color.Red;

                //tmrServerReceive.Enabled = false;
                tmrServer.Enabled = false;
                Variable.SEC = -1;
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Nếu như màu sắc của 1 trong 2 không phải bạc. Tức là có được 1 người thắng.
            if (btnWinBlue.BackColor != Color.Silver || btnWinRed.BackColor != Color.Silver)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn lưu kết quả và chuyển tới trận kế tiếp?", "Lưu kết quả", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Save kết quả vào database
                    fillData.SaveMath(lblWinId.Text);

                    nmrNumberMatch.Value = nmrNumberMatch.Value + 1;


                    pnlSetting.Visible = true;

                    //Tạm chưa cho máy con chấm điểm
                    // tmrServerReceive.Enabled = false;
                    tmrServer.Enabled = false;
                    Variable.SEC = -1;

                    //Gửi chuỗi -1 qua cho client để reset lại điểm
                    try
                    {
                        foreach (var sv in Variable.SERVERSERVICES)
                        {
                            Variable.SENTTEXT = getServerJsonString();
                            sv.SentText(Variable.SENTTEXT);
                        }
                    }
                    catch (Exception)
                    {
                        //Không có kết nối với bất kỳ client nào
                    }
                }
                else if (dialogResult == DialogResult.No) { }
            }
            //Chưa chọn được ai thắng cả
            else
            {
                MessageBox.Show("Phải chọn một người chiến thắng!");
            }
        }

        private void picSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = true;
        }

        private void picHideSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = false;
        }

    }
}
