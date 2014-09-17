using System;
using System.Collections.Generic;

namespace StoreOnLine.Models
{
    public class ProgressBarView : IProgressBar
    {
        public IDictionary<string, string> ClassNameParent =
            new Dictionary<string, string>
            {
                { "progress", "progress " }, { "striped", "progress progress-striped " }
            };

        public IDictionary<string, string> ClassNameChild =
            new Dictionary<string, string>
            {
                { "danger" , "progress-bar progress-bar-danger" },
                { "success","progress-bar progress-bar-success"},
                { "info"   , "progress-bar progress-bar-info"},
                { "warning","progress-bar progress-bar-warning"}
            };

        public String Active = "active";
        public int Size { get; set; }


        public string GetProgressBar(bool isstriped)
        {
            if (isstriped)
            {
                return ClassNameParent["striped"];
            }
            return ClassNameParent["progress"];
        }

        public string Getdanger()
        {
            return ClassNameChild["danger"];
        }

        public string GetSuccess()
        {
            return ClassNameChild["success"];
        }

        public string GetInfo()
        {
            return ClassNameChild["info"];
        }

        public string GetWarning()
        {
            return ClassNameChild["warning"];
        }
    }
}