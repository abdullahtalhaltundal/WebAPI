﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace talha07.ViewModel
{
    public class KayitModel
    {

        public string kayitId { get; set; }
        public string kayitOdevId { get; set; }
        public string kayitOgrId { get; set; }
        public OgrenciModel ogrBilgi { get; set; }
        public OdevModel odevBilgi { get; set; }

    }
}