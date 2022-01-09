using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Models
{
    public class AccountingListModel
    {
        public int Account_Sid { get; set; }

        public int UserList_UserSid { get; set; }

        public int CategoryList_CategorySid { get; set; }

        public string Category_Name { get; set; }

        public int Price_Type { get; set; }

        public int Price { get; set; }

        public string Tittle { get; set; }

        public string Description { get; set; }

        public DateTime Create_Time { get; set; }

        public bool Is_Delete { get; set; }

    }
}