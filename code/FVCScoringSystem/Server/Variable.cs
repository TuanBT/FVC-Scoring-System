﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server
{
    public static class Variable
    {
        public static string RECEIVETEXT { get; set; }
        public static string SENTTEXT { get; set; }
        public static List<ServerService> SERVERSERVICES { get; set; }
        public static List<Thread> THREADS { get; set; }
        public static int PORT = 4569;
        public static string IP = "127.0.0.1";
    }
}