using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Models
{
    public class UserListModel
    {
        public int UserSid { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string User_Name { get; set; }

        public int Account_Level { get; set; }

        public string Create_Time { get; set; }

        public string Modify_Time { get; set; }

        public bool Is_Delete { get; set; }


    }
}