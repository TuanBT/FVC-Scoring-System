using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            InitializeComponent();
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
            try
            {
                foreach (var sv in Variable.SERVERSERVICES)
                {
                    Variable.SENTTEXT = getServerJsonString();
                    sv.SentText(Variable.SENTTEXT);
                }
                setFormFromClientJson(Variable.RECEIVETEXT);

                lblClock.Text = DateTime.Now.Second.ToString();
            }
            catch (Exception ex) { }
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

        public void setFormFromClientJson(string clientJson)
        {
            ClientInfo clientInfo = new ClientInfo(clientJson);
            ChangeStatus(lblStatusM1, false);
            ChangeStatus(lblStatusM2, false);
            ChangeStatus(lblStatusM3, false);
            ChangeSatusMath(lblStatusScoreM1, false);
            ChangeSatusMath(lblStatusScoreM2, false);
            ChangeSatusMath(lblStatusScoreM3, false);
            lblStatusScoreM1.Visible = false;
            lblStatusScoreM2.Visible = false;
            lblStatusScoreM3.Visible = false;
            lblPlusRedM1.Visible = false;
            lblPlusBlueM1.Visible = false;
            lblPlusRedM2.Visible = false;
            lblPlusBlueM2.Visible = false;
            lblPlusRedM2.Visible = false;
            lblPlusBlueM2.Visible = false;
            lblPlusRed.Visible = false;
            lblPlusBlue.Visible = false;

            int redWin = 0;
            int blueWin = 0;

            if (clientInfo.Computer == 1)
            {

                ChangeStatus(lblStatusM1, true);
                lblStatusScoreM1.Visible = true;
                if (clientInfo.EndMath != 0)
                {
                    ChangeSatusMath(lblStatusScoreM1, true);
                }
                lblRefereeM1.Text = clientInfo.Referee;
                lblScoreRedM1.Text = clientInfo.ScoreRed.ToString();
                lblScoreBlueM1.Text = clientInfo.ScoreBlue.ToString();
                if (clientInfo.Win == "RED")
                {
                    redWin++;
                    lblPlusRedM1.Visible = true;
                }
                if (clientInfo.Win == "BLUE")
                {
                    blueWin++;
                    lblPlusBlueM1.Visible = true;
                }
            }
            else if (clientInfo.Computer == 2)
            {
                ChangeStatus(lblStatusM2, true);
                lblStatusScoreM2.Visible = true;
                if (clientInfo.EndMath != 0)
                {
                    ChangeSatusMath(lblStatusScoreM2, true);
                }
                lblRefereeM2.Text = clientInfo.Referee;
                lblScoreRedM2.Text = clientInfo.ScoreRed.ToString();
                lblScoreBlueM2.Text = clientInfo.ScoreBlue.ToString();
                if (clientInfo.Win == "RED")
                {
                    redWin++;
                    lblPlusRedM2.Visible = true;
                }
                if (clientInfo.Win == "BLUE")
                {
                    blueWin++;
                    lblPlusBlueM2.Visible = true;
                }
            }
            else if (clientInfo.Computer == 3)
            {
                ChangeStatus(lblStatusM3, true);
                lblStatusScoreM3.Visible = true;
                if (clientInfo.EndMath != 0)
                {
                    ChangeSatusMath(lblStatusScoreM3, true);
                }
                lblRefereeM2.Text = clientInfo.Referee;
                lblScoreRedM3.Text = clientInfo.ScoreRed.ToString();
                lblScoreBlueM3.Text = clientInfo.ScoreBlue.ToString();
                if (clientInfo.Win == "RED")
                {
                    redWin++;
                    lblPlusRedM2.Visible = true;
                }
                if (clientInfo.Win == "BLUE")
                {
                    blueWin++;
                    lblPlusBlueM2.Visible = true;
                }
            }
            if (redWin > blueWin)
            {
                Variable.WIN = "RED";
                lblPlusRed.Visible = true;
            }
            else if (redWin < blueWin)
            {
                Variable.WIN = "BLUE";
                lblPlusBlue.Visible = true;
            }
            else
            {
                Variable.WIN = "";
            }
            UpdatScore();
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

        public void UpdatScore()
        {
            lblTotalScoreRed.Text = (Int32.Parse(lblScoreRedM1.Text) + Int32.Parse(lblScoreRedM2.Text) + Int32.Parse(lblScoreRedM3.Text)).ToString();
            lblTotalScoreBlue.Text = (Int32.Parse(lblScoreBlueM1.Text) + Int32.Parse(lblScoreBlueM2.Text) + Int32.Parse(lblScoreBlueM3.Text)).ToString();
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
            }
        }

    }
}
