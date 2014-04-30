﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form
    {
        TcpServers tcpServers;
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;


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

            InitializeComponent();

            this.Text = "Máy chính: " + Variable.IP + ":" + Variable.PORT;

            tcpServers = new TcpServers(this);
            Variable.SERVERSERVICES = new List<ServerService>();
            Variable.THREADS = new List<Thread>();

            // Add Event to handle when a client is connected
            Changed += new ChangedEventHandler(tcpServers.ClientAdded);

            tcpServers.StartServer();
            tmrServer.Enabled = true;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpServers.StopServer();
        }

        private void tmrServer_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.Second.ToString();

            setFormFromClientJson();
        }

        public string getServerJsonString()
        {
            ServerInfo serverInfo = new ServerInfo
            {
                Math = lblNumberMatch.Text,
                Sex = lblSex.Text,
                Time = DateTime.Now.Second.ToString(),
                Weight = lblWeight.Text,
                Sec = 2
            };
            Variable.SEC = serverInfo.Sec;
            return serverInfo.getClientJson(serverInfo);
        }

        public void setFormFromClientJson()
        {
            try
            {
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }


                if (Variable.RECEIVETEXT1 != null)
                {
                    ClientInfo clientInfo1 = new ClientInfo(Variable.RECEIVETEXT1);
                    lblStatusScoreM1.Visible = true;
                    lblRefereeM1.Text = clientInfo1.Referee;
                    lblScoreRedM1.Text = clientInfo1.ScoreRed.ToString();
                    lblScoreBlueM1.Text = clientInfo1.ScoreBlue.ToString();
                    ChangeStatus(lblStatusM1, true);
                    if (clientInfo1.EndMath == 1)
                    {
                        ChangeSatusMath(lblStatusScoreM1, true);
                    }
                    else
                    {
                        //Đã chấm xong
                        ChangeSatusMath(lblStatusScoreM1, false);
                        btnIncRedM1.Visible = true;
                        btnDecRedM1.Visible = true;
                        btnIncBlueM1.Visible = true;
                        btnDecBlueM1.Visible = true;
                    }
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
                    Variable.RECEIVETEXT1 = null;
                }
                else
                {
                    ChangeStatus(lblStatusM1, false);
                    lblScoreRedM1.Text = "0";
                    lblScoreBlueM1.Text = "0";
                    lblRefereeM1.Text = "-";
                    lblPlusRedM1.Visible = false;
                    lblPlusBlueM1.Visible = false;
                    lblStatusScoreM1.Visible = false;


                }
                if (Variable.RECEIVETEXT2 != null)
                {
                    ClientInfo clientInfo2 = new ClientInfo(Variable.RECEIVETEXT2);
                    lblStatusScoreM2.Visible = true;
                    lblRefereeM2.Text = clientInfo2.Referee;
                    lblScoreRedM2.Text = clientInfo2.ScoreRed.ToString();
                    lblScoreBlueM2.Text = clientInfo2.ScoreBlue.ToString();
                    ChangeStatus(lblStatusM2, true);
                    if (clientInfo2.EndMath == 1)
                    {
                        ChangeSatusMath(lblStatusScoreM2, true);
                    }
                    else
                    {
                        ChangeSatusMath(lblStatusScoreM2, false);
                        btnIncRedM2.Visible = true;
                        btnDecRedM2.Visible = true;
                        btnIncBlueM2.Visible = true;
                        btnDecBlueM2.Visible = true;
                    }
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
                    Variable.RECEIVETEXT2 = null;
                }
                else
                {
                    ChangeStatus(lblStatusM2, false);
                    lblScoreRedM2.Text = "0";
                    lblScoreBlueM2.Text = "0";
                    lblRefereeM2.Text = "-";
                    lblPlusRedM2.Visible = false;
                    lblPlusBlueM2.Visible = false;
                    lblStatusScoreM2.Visible = false;
                }
                if (Variable.RECEIVETEXT3 != null)
                {
                    ClientInfo clientInfo3 = new ClientInfo(Variable.RECEIVETEXT3);
                    lblStatusScoreM3.Visible = true;
                    lblRefereeM3.Text = clientInfo3.Referee;
                    lblScoreRedM3.Text = clientInfo3.ScoreRed.ToString();
                    lblScoreBlueM3.Text = clientInfo3.ScoreBlue.ToString();
                    ChangeStatus(lblStatusM3, true);
                    if (clientInfo3.EndMath == 1)
                    {
                        ChangeSatusMath(lblStatusScoreM3, true);
                    }
                    else
                    {
                        ChangeSatusMath(lblStatusScoreM3, false);
                        btnIncRedM3.Visible = true;
                        btnDecRedM3.Visible = true;
                        btnIncBlueM3.Visible = true;
                        btnDecBlueM3.Visible = true;
                    }
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
                    Variable.RECEIVETEXT3 = null;
                }
                else
                {
                    ChangeStatus(lblStatusM3, false);
                    lblScoreRedM3.Text = "0";
                    lblScoreBlueM3.Text = "0";
                    lblRefereeM3.Text = "-";
                    lblPlusRedM3.Visible = false;
                    lblPlusBlueM3.Visible = false;
                    lblStatusScoreM3.Visible = false;
                }

                UpdateScore();
            }
            catch (Exception ex) { }
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
                label.Text = "Chờ...";
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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = true;
        }

        private void btnHideSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = false;

            setFormSetting();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Lấy từ database trận đấu tiếp theo

            setFormSetting();
        }

        //Từ khung setting, đổ các dữ liệu về lại form chính
        public void setFormSetting()
        {
            lblRefereeMain.Text = txtRefereeMain.Text;
            lblSecretary.Text = txtSecretary.Text;

            lblWeight.Text = txtWeight.Text;
            lblSex.Text = txtSex.Text;
            lblNumberMatch.Text = txtNumberMatch.Text;

            lblNameRed.Text = txtNameRed.Text;
            lblIdRed.Text = txtIdRed.Text;
            lblClassRed.Text = txtClassRed.Text;
            lblNameBlue.Text = txtNameBlue.Text;
            lblIdBlue.Text = txtIdBlue.Text;
            lblClassBlue.Text = txtClassBlue.Text;
        }

        private void btnIncBlueM1_Click(object sender, EventArgs e)
        {
            lblScoreBlueM1.Text = (Int32.Parse(lblScoreBlueM1.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnIncRedM1_Click(object sender, EventArgs e)
        {
            lblScoreRedM1.Text = (Int32.Parse(lblScoreRedM1.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnDecRedM1_Click(object sender, EventArgs e)
        {
            lblScoreRedM1.Text = (Int32.Parse(lblScoreRedM1.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecBlueM1_Click(object sender, EventArgs e)
        {
            lblScoreBlueM1.Text = (Int32.Parse(lblScoreBlueM1.Text) - 1).ToString();
            UpdateScore();
        }
    }
}
