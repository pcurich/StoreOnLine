using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnLine.Areas.Report.Models
{
    public class Day
    {
        public int Number { get; set; }
        public string AbbNameDay { get; set; }
        public string Activity { get; set; }
    }
}

public enum Activity
{
    FL_D,
    FL_N,
    DES,
    DT_D,
    DT_N,
    ROT,
    F,
    P,
    S,
    DM,
    VAC,
    RN
}