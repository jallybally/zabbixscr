﻿using System.Collections.Generic;
using System;

namespace zabbixscr.TJ
{
    struct DataTJ
    {
        public static string RootDirTJ = @"C:\LOG1C";
        public static string CurrentDate = String.Empty;
        public static string Path = String.Empty;
        public static string Error = String.Empty;
        public static string MessageNumber = @"C:\LOG1C\Message.txt";
        public static int IndexMessage = 0;
        public static int CurrentIndexMessage = 0;
        public static bool Chek = true;
        //
        public static List<string> TJ = new List<string>();
    }
}