using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server
{
    public static class Variable
    {
        public static string RECEIVETEXT { get; set; }
        public static string RECEIVETEXT1 { get; set; }
        public static string RECEIVETEXT2 { get; set; }
        public static string RECEIVETEXT3 { get; set; }
        public static string RECEIVETEXTCLOCK { get; set; }
        public static string SENTTEXT = "-";
        public static List<ServerService> SERVERSERVICES { get; set; }
        public static List<Thread> THREADS { get; set; }
        public static string WIN { get; set; }
        public static int SEC = 1; //4 kiểu: 1 sec 1, 2 sec 2, -1 không chấm, 0 sec giải lao
        public static string STATE = "Standing"; //Trạng thái của đồng hồ. Standing, Running, Pausing, Stopping
        public static int PORT = 4569;
        public static string IP = "192.168.0.100";
    }
}
