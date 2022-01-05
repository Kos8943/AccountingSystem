using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Models
{
    public class IndexModel
    {
        public string FirstDate { get; set; }

        public string LastDate { get; set; }

        public string TotalAccounting { get; set; }

        public string TotalUser { get; set; }
    }
}