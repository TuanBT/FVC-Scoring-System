using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FVC_Stopwatch
{
    class ClockInfo
    {
        public string State { get; set; } //Trạng thái của đồng hồ. Standing, Running, Pausing, Stopping
        public string Type { get; set; }//Các kiểu Type: H1, GL, H2
        public string TimeState { get; set; }//2 trạng thái thời gian Decrease và Increase
        public int M { get; set; } //Minute
        public int S { get; set; } //Second

        public ClockInfo() { }

        public ClockInfo(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jClientInfo = jObject["ClockInfo"];
            State = (string)jClientInfo["State"];
            Type = (string)jClientInfo["Type"];
            TimeState = (string)jClientInfo["TimeState"];
            M = (int)jClientInfo["M"];
            S = (int)jClientInfo["S"];
        }

        public string getClockJson(ClockInfo clock)
        {
            string clockJson = JsonConvert.SerializeObject(clock);
            clockJson = "{\"ClockInfo\":" + clockJson + "}";
            return clockJson;
        }
    }
}
