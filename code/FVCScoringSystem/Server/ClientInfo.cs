using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class ClientInfo
    {
        public int Computer { get; set; }
        public int Math { get; set; }
        public string Weight { get; set; }
        public string Sex { get; set; }
        public int ScoreRedSec1 { get; set; }
        public int ScoreRedSec2 { get; set; }
        public int MinusRedSec1 { get; set; }
        public int MinusRedSec2 { get; set; }
        public int ScoreBlueSec1 { get; set; }
        public int ScoreBlueSec2 { get; set; }
        public int MinusBlueSec1 { get; set; }
        public int MinusBlueSec2 { get; set; }
        public int ScoreRed { get; set; }
        public int ScoreBlue { get; set; }
        public string Win { get; set; }
        public string WinForm { get; set; }
        public string Referee { get; set; }
        public int EndMath { get; set; } //1 la dang cham, 0 la xong roi

        public ClientInfo() { }

        public ClientInfo(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jClientInfo = jObject["ClientInfo"];
            Computer = (int)jClientInfo["Computer"];
            Math = (int)jClientInfo["Math"];
            Weight = (string)jClientInfo["Weight"];
            Sex = (string)jClientInfo["Sex"];
            ScoreRedSec1 = (int)jClientInfo["ScoreRedSec1"];
            ScoreRedSec2 = (int)jClientInfo["ScoreRedSec2"];
            MinusRedSec1 = (int)jClientInfo["MinusRedSec1"];
            MinusRedSec2 = (int)jClientInfo["MinusRedSec2"];
            ScoreBlueSec1 = (int)jClientInfo["ScoreBlueSec1"];
            ScoreBlueSec2 = (int)jClientInfo["ScoreBlueSec2"];
            MinusBlueSec1 = (int)jClientInfo["MinusBlueSec1"];
            MinusBlueSec2 = (int)jClientInfo["MinusBlueSec2"];
            ScoreRed = (int)jClientInfo["ScoreRed"];
            ScoreBlue = (int)jClientInfo["ScoreBlue"];
            Win = (string)jClientInfo["Win"];
            WinForm = (string)jClientInfo["WinForm"];
            Referee = (string)jClientInfo["Referee"];
            EndMath = (int)jClientInfo["EndMath"];
        }

        public string getClientJson(ClientInfo clientInfo)
        {
            string clientJson = JsonConvert.SerializeObject(clientInfo);
            clientJson = "{\"ClientInfo\":" + clientJson + "}";
            return clientJson;
        }
    }
}