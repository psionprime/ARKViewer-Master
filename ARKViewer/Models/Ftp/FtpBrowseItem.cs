﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models.Ftp
{
    public class FtpBrowseItem
    {
        public bool IsFolder { get; set; } = false;
        public string FullName { get; set; } = "";
        public string Name { get; set; } = "";

    }
}
