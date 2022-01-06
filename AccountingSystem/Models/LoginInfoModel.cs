using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Models
{
    public class LoginInfoModel
    {
        public int UserSid { get; set;}

        public string User_Name { get; set; }

        public int Account_Level { get; set; }
    }
}