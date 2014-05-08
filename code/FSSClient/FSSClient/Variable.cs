using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public static class Variable
    {
        public static string RECEIVETEXT = "-";
        public static string SENTTEXT { get; set; }
        public static string LASTSCORE { get; set; }
        public static int SEC { get; set; }
        public static int ENDMATH { get; set; } //0 chấm xong, 1 đang chấm, -1 chưa chấm
        public static string WIN { get; set; }
        public static int COMPUTER { get; set; }
        public static int PORT = 4569;
        public static string IP = "127.0.0.1";
        public static string PASSADMIN = "1";
    }
}
