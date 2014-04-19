using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public static class Variable
    {
        public static string RECEIVETEXT { get; set; }
        public static string SENTTEXT { get; set; }
        public static string LASTSCORE { get; set; }
        public static int SEC { get; set; }
        public static int ENDMATH { get; set; }
        public static string WIN { get; set; }
        public static int PORT = 4569;
        public static string IP = "127.0.0.1";
    }
}
