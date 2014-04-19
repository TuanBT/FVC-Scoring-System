using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Client
{
    public class ServerInfo
    {
        public string Math { get; set; }
        public string Weight { get; set; }
        public string Sex { get; set; }
        public string Time { get; set; }

        public ServerInfo() { }

        public ServerInfo(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jClientInfo = jObject["ServerInfo"];
            Math = (string)jClientInfo["Math"];
            Weight = (string)jClientInfo["Weight"];
            Sex = (string)jClientInfo["Sex"];
            Time = (string)jClientInfo["Time"];
        }

        public string getClientJson(ServerInfo serverInfo)
        {
            string serverJson = JsonConvert.SerializeObject(serverInfo);
            serverJson = "{\"ClientInfo\":" + serverJson + "}";
            return serverJson;
        }
    }
}
