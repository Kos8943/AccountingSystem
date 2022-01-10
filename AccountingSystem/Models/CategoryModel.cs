using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Models
{
    public class CategoryModel
    {
        public int Account_Sid { get; set; }

        public int User_List_User_Sid { get; set; }

        public DateTime Create_Time { get; set; }

        public string Category_Name { get; set; }

        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        public int Has_Accounting { get; set; }

    }
}