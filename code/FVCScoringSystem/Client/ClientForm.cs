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
            tcpClients.Connection();
            tmrClient.Enabled = true;
        }

        private void tmrClient_Tick(object sender, EventArgs e)
        {
            Variable.SENTTEXT = DateTime.Now.Second.ToString();
            tcpClients.Sent(Variable.SENTTEXT);

            lblClock.Text = Variable.RECEIVETEXT;
        }

        public void setJsonString()
        {
            //{"ClientInfo":{"WinForm":"TuyetDoi","Win":"Red","ScoreBlue":13.5,"Referee":"Nguyễn Hoàng Tùng","Weight":"51-54kg","Sec":2,"MinusBlue":1,"Sex":"Nam","ScoreRed":12.5,"Math":"T15","Computer":"M1","MinusRed":3}}
            string win = "Red";
            string winForm = "Score";
            
            ClientInfo clientInfo = new ClientInfo
            {
                Computer = lblNumberClient.Text,
                Math = Int32.Parse (lblNumberMatch.Text),
                MinusBlueSec1 = Int32.Parse(lblMinusSec1Blue.Text),
                MinusBlueSec2 = Int32.Parse(lblMinusSec2Blue.Text),
                MinusRedSec1 = Int32.Parse(lblMinusSec1Red.Text),
                MinusRedSec2 = Int32.Parse(lblMinusSec2Red.Text),
                ScoreBlueSec1 = Int32.Parse(lblSec1Blue.Text),
                ScoreBlueSec2 = Int32.Parse(lblSec2Blue.Text),
                ScoreRedSec1 = Int32.Parse(lblSec1Red.Text),
                ScoreRedSec2 = Int32.Parse(lblSec2Red.Text),
                Sex = lblSex.Text,
                Weight = lblWeight.Text,
                Win = win,
                WinForm = winForm,
                Referee = lblRefereeName.Text
            };

            string json = "";
        }
    }
}

