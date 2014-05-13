using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Client;

namespace FSSClient
{
    public partial class Form1 : Form
    {
        TcpClients tcpClients = new TcpClients();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pnlMain.Left = (this.Width - pnlMain.Width) / 2;
            //pnlMain.Top = (this.Height - pnlMain.Height) / 2;
            pnlSetting.Left = 0;
            pnlSetting.Top = 0;
            pnlSetting.Width = pnlMain.Width;
            pnlSetting.Height = pnlMain.Height;
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
            resetColorKeydown();
            ServerInfo serverInfo;
            try
            {
                serverInfo = new ServerInfo(serverJson);
            }
            catch (Exception)
            {
                return;
            }

            //Máy con đang được cấp phép chấm điểm trận
            if (serverInfo.Sec != -1)
            {
                btnHideSetting.Enabled = true;
                btnHideSetting.BackColor = Color.Green;

                lblClock.Text = serverInfo.Time;
                lblWeight.Text = serverInfo.Weight;
                lblSex.Text = serverInfo.Sex;
                lblNumberMatch.Text = serverInfo.Math.ToString();
                if (serverInfo.State == "Standing")
                {
                    lblClock.BackColor = Color.LightGray;
                }
                else if (serverInfo.State == "Running")
                {
                    lblClock.BackColor = Color.White;
                }
                else if (serverInfo.State == "Pausing")
                {
                    lblClock.BackColor = Color.Yellow;
                }
                else if (serverInfo.State == "Stopping")
                {
                    lblClock.BackColor = Color.Red;
                }
                this.Text = "Giám định: " + Variable.COMPUTER + "        Trận số: " + serverInfo.Math.ToString();

                if (serverInfo.Sec == 1)
                {
                    lblSec.Text = "Hiệp 1";

                    lblSec1.ForeColor = Color.Green;
                    lblTotalSec1Red.BackColor = Color.White;
                    lblMinusSec1Red.BackColor = Color.White;
                    lblSec1.BackColor = Color.White;
                    lblSec1Red.BackColor = Color.White;
                    lblSec1Blue.BackColor = Color.White;
                    lblMinusSec1Blue.BackColor = Color.White;
                    lblTotalSec1Blue.BackColor = Color.White;

                    EndSec2();
                }
                else if (serverInfo.Sec == 2)
                {
                    lblSec.Text = "Hiệp 2";

                    lblSec2.ForeColor = Color.Green;
                    lblTotalSec2Red.BackColor = Color.White;
                    lblMinusSec2Red.BackColor = Color.White;
                    lblSec2.BackColor = Color.White;
                    lblSec2Red.BackColor = Color.White;
                    lblSec2Blue.BackColor = Color.White;
                    lblMinusSec2Blue.BackColor = Color.White;
                    lblTotalSec2Blue.BackColor = Color.White;

                    EndSec1();
                }
                else if (serverInfo.Sec == 0)
                {
                    lblSec.Text = "Giải lao";
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
            lblSec1.BackColor = Color.Silver;
            lblSec1.ForeColor = Color.Gray;
            lblSec1Blue.BackColor = Color.Silver;
            lblMinusSec1Blue.BackColor = Color.Silver;
            lblTotalSec1Blue.BackColor = Color.Silver;
        }
        public void EndSec2()
        {
            lblTotalSec2Blue.BackColor = Color.Silver;
            lblMinusSec2Blue.BackColor = Color.Silver;
            lblSec2Blue.BackColor = Color.Silver;
            lblSec2.BackColor = Color.Silver;
            lblSec2.ForeColor = Color.Gray;
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
            lblTotalSec1Red.Text = "0";
            lblTotalSec2Red.Text = "0";
            lblMinusSec1Blue.Text = "0";
            lblSec2Blue.Text = "0";
            lblMinusSec2Blue.Text = "0";
            lblSec1Blue.Text = "0";
            lblTotalSec1Blue.Text = "0";
            lblTotalSec2Blue.Text = "0";
            lblTotalScoreRed.Text = "0";
            lblTotalScoreBlue.Text = "0";
            btnWinRed.BackColor = Color.Silver;
            btnWinBlue.BackColor = Color.Silver;
            rdbWinPoint.Checked = true;

            //Variable.ENDMATH = 1;
            Variable.WIN = "";

            //UpdateScore(); 
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

        public void resetColorKeydown()
        {
            ServerInfo serverInfo;
            try
            {
                serverInfo = new ServerInfo(Variable.RECEIVETEXT);
            }
            catch (Exception)
            {
                return;
            }
            if (serverInfo.Sec == 1)
            {
                lblSec1Red.BackColor = Color.White;
                lblSec2Red.BackColor = Color.White;
            }
            if (serverInfo.Sec == 2)
            {
                lblSec1Blue.BackColor = Color.White;
                lblSec2Blue.BackColor = Color.White;
            }
        }

        public void UpdateScore()
        {
            btnSentResult.Focus();
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

        private void txtPassAdmin_TextChanged(object sender, EventArgs e)
        {
            if (txtPassAdmin.Text == Variable.PASSADMIN)
            {
                btnConnect.Focus();
                txtIpServer.Text = Variable.IP;
                txtPortServer.Text = Variable.PORT.ToString();
                pnlAdmin.Visible = true;
            }
            else
            {
                pnlAdmin.Visible = false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Variable.IP = txtIpServer.Text;
            try
            {
                Variable.PORT = Convert.ToInt32(txtPortServer.Text);
            }
            catch (Exception)
            {
            }

            if (tcpClients.Connection())
            {
                MessageBox.Show("Kết nối tới trọng tài chính thành công!");
                this.Text = "Giám định " + nmrComputer.Value;
                Variable.COMPUTER = Convert.ToInt32(nmrComputer.Value);
                txtPassAdmin.Text = "";
                pnlAdmin.Visible = false;
                tmrClient.Enabled = true;
                btnHideSetting.Focus();
            }
            else
            {
                MessageBox.Show("Kết nối tới trọng tài chính thất bại!");
            }
        }

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
                btnSentResult.Focus();
                Variable.ENDMATH = 1;
                tmrClient.Enabled = true;
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

        private void btnWinRed_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn ĐỎ thắng và kết quả điểm reset về 0?", "Đỏ thắng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                resetNewMath();

                Variable.WIN = "RED";

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
                resetNewMath();

                Variable.WIN = "BLUE";

                rdbWinAdvantage.Checked = true;
                btnWinBlue.BackColor = Color.Blue;
                btnWinRed.BackColor = Color.Silver;
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn tất cả các kết quả reset về 0?", "Reset", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                resetNewMath();
            }
            else if (dialogResult == DialogResult.No) { }
        }

        private void btnSentResult_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn thực sự muốn gửi kết quả", "Gửi kết quả", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                pnlSetting.Visible = true;
                //Kết thúc chấm điểm. Đã chấm.
                Variable.ENDMATH = 0;
            }
            else if (dialogResult == DialogResult.No) { }
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
            Variable.LASTSCORE = "BLUE";
            UpdateScore();
        }

        private void btnIncMinusSec2Red_Click(object sender, EventArgs e)
        {
            lblMinusSec2Red.Text = (Int32.Parse(lblMinusSec2Red.Text) + 1).ToString();
            Variable.LASTSCORE = "BLUE";
            UpdateScore();
        }

        private void btnDecSec1Red_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblSec1Red.Text) - 1;
            if (number < 0)
                lblSec1Red.Text = "0";
            else
                lblSec1Red.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecSec2Red_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblSec2Red.Text) - 1;
            if (number < 0)
                lblSec2Red.Text = "0";
            else
                lblSec2Red.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecMinusSec1Red_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblMinusSec1Red.Text) - 1;
            if (number < 0)
                lblMinusSec1Red.Text = "0";
            else
                lblMinusSec1Red.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecMinusSec2Red_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblMinusSec2Red.Text) - 1;
            if (number < 0)
                lblMinusSec2Red.Text = "0";
            else
                lblMinusSec2Red.Text = number.ToString();
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
            Variable.LASTSCORE = "RED";
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
            Variable.LASTSCORE = "RED";
            UpdateScore();
        }

        private void btnDecSec1Blue_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblSec1Blue.Text) - 1;
            if (number < 0)
                lblSec1Blue.Text = "0";
            else
                lblSec1Blue.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecMinusSec1Blue_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblMinusSec1Blue.Text) - 1;

            if (number < 0)
                lblMinusSec1Blue.Text = "0";
            else
                lblMinusSec1Blue.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecSec2Blue_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblSec2Blue.Text) - 1;
            if (number < 0)
                lblSec2Blue.Text = "0";
            else
                lblSec2Blue.Text = number.ToString();
            UpdateScore();
        }

        private void btnDecMinusSec2Blue_Click(object sender, EventArgs e)
        {
            int number = Int32.Parse(lblMinusSec2Blue.Text) - 1;
            if (number < 0)
                lblMinusSec2Blue.Text = "0";
            else
                lblMinusSec2Blue.Text = number.ToString();
            UpdateScore();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ServerInfo serverInfo;
            try
            {
                serverInfo = new ServerInfo(Variable.RECEIVETEXT);
            }
            catch (Exception)
            {
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                if (serverInfo.Sec == 1)
                {
                    int score = Convert.ToInt32(lblSec1Red.Text);
                    score++;
                    lblSec1Red.Text = score.ToString();
                    lblSec1Red.BackColor = Color.LightCoral;
                    UpdateScore();
                }
                if (serverInfo.Sec == 2)
                {
                    int score = Convert.ToInt32(lblSec2Red.Text);
                    score++;
                    lblSec2Red.Text = score.ToString();
                    lblSec2Red.BackColor = Color.LightCoral;
                    UpdateScore();
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (serverInfo.Sec == 1)
                {
                    int score = Convert.ToInt32(lblSec1Blue.Text);
                    score++;
                    lblSec1Blue.Text = score.ToString();
                    lblSec1Blue.BackColor = Color.LightBlue;
                    UpdateScore();
                }
                if (serverInfo.Sec == 2)
                {
                    int score = Convert.ToInt32(lblSec2Blue.Text);
                    score++;
                    lblSec2Blue.Text = score.ToString();
                    lblSec2Blue.BackColor = Color.LightBlue;
                    UpdateScore();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thực sự muốn tắt phần mềm?", "Tắt phần mềm!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void nmrComputer_ValueChanged(object sender, EventArgs e)
        {
            btnConnect.Focus();
        }

    }
}
