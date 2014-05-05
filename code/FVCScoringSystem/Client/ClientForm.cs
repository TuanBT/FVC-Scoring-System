using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        TcpClients tcpClients = new TcpClients();
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            pnlSetting.Left = pnlMain.Location.X;
            pnlSetting.Top = pnlMain.Location.Y;
            pnlSetting.Width = pnlMain.Width;
            pnlSetting.Height = pnlMain.Height;
        }

        private void tmrClient_Tick(object sender, EventArgs e)
        {
            try
            {
                setFormFromServerJson(Variable.RECEIVETEXT);

                Variable.SENTTEXT = getClientJsonString();
                tcpClients.Sent(Variable.SENTTEXT);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Mất kết nối với Server. Click OK để kết nối lại!");
            }
        }

        public string getClientJsonString()
        {
            ClientInfo clientInfo = new ClientInfo
            {
                Computer = Int32.Parse(lblNumberClient.Text),
                Math = Int32.Parse(lblNumberMatch.Text),
                MinusBlueSec1 = Int32.Parse(lblMinusSec1Blue.Text),
                MinusBlueSec2 = Int32.Parse(lblMinusSec2Blue.Text),
                MinusRedSec1 = Int32.Parse(lblMinusSec1Red.Text),
                MinusRedSec2 = Int32.Parse(lblMinusSec2Red.Text),
                ScoreBlueSec1 = Int32.Parse(lblSec1Blue.Text),
                ScoreBlueSec2 = Int32.Parse(lblSec2Blue.Text),
                ScoreRedSec1 = Int32.Parse(lblSec1Red.Text),
                ScoreRedSec2 = Int32.Parse(lblSec2Red.Text),
                ScoreRed = Int32.Parse(lblSec1Red.Text) + Int32.Parse(lblSec2Red.Text) - Int32.Parse(lblMinusSec1Red.Text) - Int32.Parse(lblMinusSec2Red.Text),
                ScoreBlue = Int32.Parse(lblSec1Blue.Text) + Int32.Parse(lblSec2Blue.Text) - Int32.Parse(lblMinusSec1Blue.Text) - Int32.Parse(lblMinusSec2Blue.Text),
                Sex = lblSex.Text,
                Weight = lblWeight.Text,
                Win = Variable.WIN,
                WinForm = getWinform(),
                Referee = lblRefereeName.Text,
                EndMath = Variable.ENDMATH
            };
            return clientInfo.getClientJson(clientInfo);
        }

        public void setFormFromServerJson(string serverJson)
        {
            ServerInfo serverInfo = new ServerInfo(serverJson);
            //Máy con đang được cấp phép chấm điểm trận
            if (serverInfo.Sec != -1)
            {
                btnHideSetting.Enabled = true;
                btnHideSetting.BackColor = Color.Green;

                lblClock.Text = serverInfo.Time;
                lblWeight.Text = serverInfo.Weight;
                lblSex.Text = serverInfo.Sex;
                lblNumberMatch.Text = serverInfo.Math.ToString();
                this.Text = "Máy: " + Variable.COMPUTER + "---Trận số: " + serverInfo.Math.ToString();

                if (serverInfo.Sec == 1)
                {
                    EndSec2();
                }
                else if (serverInfo.Sec == 2)
                {
                    EndSec1();
                }
                else if (serverInfo.Sec == 0)
                {
                    EndSec1();
                    EndSec2();
                }
            }
            //sec = -1. Ý nghĩa là không cho máy con không được chấm điểm lúc này
            else
            {
                resetNewMath();
                Variable.ENDMATH = 0; //Chưa chấm
                pnlSetting.Visible = true;
                btnHideSetting.Enabled = false;
                btnHideSetting.BackColor = Color.Gray;
            }
        }

        public void EndSec1()
        {
            lblTotalSec1Red.BackColor = Color.Silver;
            lblMinusSec1Red.BackColor = Color.Silver;
            lblSec1Red.BackColor = Color.Silver;
            //label12.BackColor = Color.Silver;
            lblSec1Blue.BackColor = Color.Silver;
            lblMinusSec1Blue.BackColor = Color.Silver;
            lblTotalSec1Blue.BackColor = Color.Silver;
        }
        public void EndSec2()
        {
            lblTotalSec2Blue.BackColor = Color.Silver;
            lblMinusSec2Blue.BackColor = Color.Silver;
            lblSec2Blue.BackColor = Color.Silver;
            //label11.BackColor = Color.Silver;
            lblSec2Red.BackColor = Color.Silver;
            lblMinusSec2Red.BackColor = Color.Silver;
            lblTotalSec2Red.BackColor = Color.Silver;
        }

        public void resetNewMath()
        {
            lblSec1Red.Text = "0";
            lblMinusSec1Red.Text = "0";
            lblSec2Red.Text = "0";
            lblMinusSec2Red.Text = "0";
            lblMinusSec1Blue.Text = "0";
            lblSec2Blue.Text = "0";
            lblMinusSec2Blue.Text = "0";
            lblSec1Blue.Text = "0";

            //Variable.ENDMATH = 1;
            Variable.WIN = "";

            UpdateScore(); 
        }

        public string getWinform()
        {
            if (rdbWinPoint.Checked)
            {
                return "Điểm";
            }
            if (rdbWinAdvantage.Checked)
            {
                return "Ưu thế";
            }
            if (rdbWinGivingUp.Checked)
            {
                return "Bỏ cuộc";
            }
            if (rdbWinAbsolute.Checked)
            {
                return "Tuyệt đối";
            }
            if (rdbWinKnock.Checked)
            {
                return "Đo ván";
            }
            if (rdbWinDisqualification.Checked)
            {
                return "Truất quyền";
            }
            if (rdbWinStopping.Checked)
            {
                return "Dừng trận";
            }
            return "";
        }

        #region event click button
        public void UpdateScore()
        {
            lblTotalSec1Red.Text = (Int32.Parse(lblSec1Red.Text) - Int32.Parse(lblMinusSec1Red.Text)).ToString();
            lblTotalSec2Red.Text = (Int32.Parse(lblSec2Red.Text) - Int32.Parse(lblMinusSec2Red.Text)).ToString();
            lblTotalSec1Blue.Text = (Int32.Parse(lblSec1Blue.Text) - Int32.Parse(lblMinusSec1Blue.Text)).ToString();
            lblTotalSec2Blue.Text = (Int32.Parse(lblSec2Blue.Text) - Int32.Parse(lblMinusSec2Blue.Text)).ToString();

            lblTotalScoreRed.Text = (Int32.Parse(lblTotalSec1Red.Text) + Int32.Parse(lblTotalSec2Red.Text)).ToString();
            lblTotalScoreBlue.Text = (Int32.Parse(lblTotalSec1Blue.Text) + Int32.Parse(lblTotalSec2Blue.Text)).ToString();

            int ScoreRed = Int32.Parse(lblTotalSec1Red.Text) + Int32.Parse(lblTotalSec2Red.Text);
            int ScoreBlue = Int32.Parse(lblTotalSec1Blue.Text) + Int32.Parse(lblTotalSec2Blue.Text);

            if (ScoreRed > ScoreBlue)
            {
                rdbWinPoint.Checked = true;
                btnWinRed.BackColor = Color.Red;
                btnWinBlue.BackColor = Color.Silver;
                Variable.WIN = "RED";
            }
            else if (ScoreBlue > ScoreRed)
            {
                rdbWinPoint.Checked = true;
                btnWinBlue.BackColor = Color.Blue;
                btnWinRed.BackColor = Color.Silver;
                Variable.WIN = "BLUE";
            }
            //Bang nhau
            else
            {
                if (Variable.LASTSCORE == "RED")
                {
                    rdbWinPoint.Checked = true;
                    btnWinRed.BackColor = Color.Red;
                    btnWinBlue.BackColor = Color.Silver;
                    Variable.WIN = "RED";
                }
                else if (Variable.LASTSCORE == "BLUE")
                {
                    rdbWinPoint.Checked = true;
                    btnWinBlue.BackColor = Color.Blue;
                    btnWinRed.BackColor = Color.Silver;
                    Variable.WIN = "BLUE";
                }
                else
                {
                    btnWinRed.BackColor = Color.Silver;
                    btnWinBlue.BackColor = Color.Silver;
                }
            }
        }

        private void btnIncSec1Red_Click(object sender, EventArgs e)
        {
            lblSec1Red.Text = (Int32.Parse(lblSec1Red.Text) + 1).ToString();
            Variable.LASTSCORE = "RED";
            UpdateScore();
        }

        private void btnIncSec2Red_Click(object sender, EventArgs e)
        {
            lblSec2Red.Text = (Int32.Parse(lblSec2Red.Text) + 1).ToString();
            Variable.LASTSCORE = "RED";
            UpdateScore();
        }

        private void btnIncMinusSec1Red_Click(object sender, EventArgs e)
        {
            lblMinusSec1Red.Text = (Int32.Parse(lblMinusSec1Red.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnIncMinusSec2Red_Click(object sender, EventArgs e)
        {
            lblMinusSec2Red.Text = (Int32.Parse(lblMinusSec2Red.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnDecSec1Red_Click(object sender, EventArgs e)
        {
            lblSec1Red.Text = (Int32.Parse(lblSec1Red.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecSec2Red_Click(object sender, EventArgs e)
        {
            lblSec2Red.Text = (Int32.Parse(lblSec2Red.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecMinusSec1Red_Click(object sender, EventArgs e)
        {
            lblMinusSec1Red.Text = (Int32.Parse(lblMinusSec1Red.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecMinusSec2Red_Click(object sender, EventArgs e)
        {
            lblMinusSec2Red.Text = (Int32.Parse(lblMinusSec2Red.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnIncSec1Blue_Click(object sender, EventArgs e)
        {
            lblSec1Blue.Text = (Int32.Parse(lblSec1Blue.Text) + 1).ToString();
            Variable.LASTSCORE = "BLUE";
            UpdateScore();
        }

        private void btnIncMinusSec1Blue_Click(object sender, EventArgs e)
        {
            lblMinusSec1Blue.Text = (Int32.Parse(lblMinusSec1Blue.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnIncSec2Blue_Click(object sender, EventArgs e)
        {
            lblSec2Blue.Text = (Int32.Parse(lblSec2Blue.Text) + 1).ToString();
            Variable.LASTSCORE = "BLUE";
            UpdateScore();
        }

        private void btnIncMinusSec2Blue_Click(object sender, EventArgs e)
        {
            lblMinusSec2Blue.Text = (Int32.Parse(lblMinusSec2Blue.Text) + 1).ToString();
            UpdateScore();
        }

        private void btnDecSec1Blue_Click(object sender, EventArgs e)
        {
            lblSec1Blue.Text = (Int32.Parse(lblSec1Blue.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecMinusSec1Blue_Click(object sender, EventArgs e)
        {
            lblMinusSec1Blue.Text = (Int32.Parse(lblMinusSec1Blue.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecSec2Blue_Click(object sender, EventArgs e)
        {
            lblSec2Blue.Text = (Int32.Parse(lblSec2Blue.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnDecMinusSec2Blue_Click(object sender, EventArgs e)
        {
            lblMinusSec2Blue.Text = (Int32.Parse(lblMinusSec2Blue.Text) - 1).ToString();
            UpdateScore();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn tất cả các kết quả reset về 0?", "Reset", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                lblSec1Red.Text = "0";
                lblMinusSec1Red.Text = "0";
                lblSec2Red.Text = "0";
                lblMinusSec2Red.Text = "0";
                lblMinusSec1Blue.Text = "0";
                lblSec2Blue.Text = "0";
                lblMinusSec2Blue.Text = "0";
                lblSec1Blue.Text = "0";

                UpdateScore();
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnSentResult_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn gửi kết quả", "Gửi kết quả", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //tcpClients.Disconnect();
                pnlSetting.Visible = true;

                //Kết thúc chấm điểm. Đã chấm.
                Variable.ENDMATH = 0;

                //lblSec1Red.Text = "0";
                //lblMinusSec1Red.Text = "0";
                //lblSec2Red.Text = "0";
                //lblMinusSec2Red.Text = "0";
                //lblMinusSec1Blue.Text = "0";
                //lblSec2Blue.Text = "0";
                //lblMinusSec2Blue.Text = "0";
                //lblSec1Blue.Text = "0";
                //UpdateScore();
            }
            else if (dialogResult == DialogResult.No) { }
           
        }

        private void btnWinRed_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn ĐỎ thắng và kết quả điểm reset về 0?", "Đỏ thắng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                lblSec1Red.Text = "0";
                lblMinusSec1Red.Text = "0";
                lblSec2Red.Text = "0";
                lblMinusSec2Red.Text = "0";
                lblMinusSec1Blue.Text = "0";
                lblSec2Blue.Text = "0";
                lblMinusSec2Blue.Text = "0";
                lblSec1Blue.Text = "0";

                //UpdateScore();

                Variable.WIN = "RED";
                lblTotalScoreRed.Text = "0";
                lblTotalScoreBlue.Text = "0";

                rdbWinAdvantage.Checked = true;
                btnWinRed.BackColor = Color.Red;
                btnWinBlue.BackColor = Color.Silver;
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnWinBlue_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn XANH thắng và kết quả điểm reset về 0?", "Xanh thắng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                lblSec1Red.Text = "0";
                lblMinusSec1Red.Text = "0";
                lblSec2Red.Text = "0";
                lblMinusSec2Red.Text = "0";
                lblMinusSec1Blue.Text = "0";
                lblSec2Blue.Text = "0";
                lblMinusSec2Blue.Text = "0";
                lblSec1Blue.Text = "0";

                //UpdateScore();
                Variable.WIN = "BLUE";
                lblTotalScoreRed.Text = "0";
                lblTotalScoreBlue.Text = "0";

                rdbWinAdvantage.Checked = true;
                btnWinBlue.BackColor = Color.Blue;
                btnWinRed.BackColor = Color.Silver;
            }
            else if (dialogResult == DialogResult.No) { }
        }

        #endregion

        private void btnHideSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = false;

            Variable.IP = txtIpServer.Text;

            lblRefereeName.Text = txtRefereeName.Text;

            Variable.COMPUTER = Int32.Parse(nmrComputer.Text);
            lblNumberClient.Text = (Int32.Parse(nmrComputer.Text)).ToString();

            if (tcpClients.Connection())
            {
                //resetNewMath();
                //Bắt đầu chấm điểm
                Variable.ENDMATH = 1;
                tmrClient.Enabled = true;
            }
            else
            {
            }
        }


        private void pnlSetting_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtComputer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassAdmin_TextChanged(object sender, EventArgs e)
        {
            if (txtPassAdmin.Text == Variable.PASSADMIN)
            {
                pnlAdmin.Visible = true;
            }
            else
            {
                pnlAdmin.Visible = false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (tcpClients.Connection())
            {
                MessageBox.Show("Kết nối tới trọng tài chính thành công!");
                txtPassAdmin.Text = "";
                pnlAdmin.Visible = false;
            }
            else
            {
                MessageBox.Show("Kết nối tới trọng tài chính thất bại!");
            }
        }

        private void picSetting_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pnlSetting.Visible = false;
        }






    }
}

