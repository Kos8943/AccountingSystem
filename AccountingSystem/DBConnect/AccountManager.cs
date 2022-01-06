using AccountingSystem.Helpers;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class AccountManager : DBHelper
    {
        public UserListModel GetAccount(string Account)
        {

            string queryString =
                $@" Select 
	                    UserList.User_Sid, 
	                    Account, 
	                    UserList.Password, 
	                    Account_Level, 
	                    UserList.User_Name 
                    From UserList
                    Where Account = @Account And Is_Delete = 'false'
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Account", Account)
                };

            var dt = this.GetDataTable(queryString, parameters);

            UserListModel model = new UserListModel();

            if (dt.Rows.Count > 0)
            {

                if (!Convert.IsDBNull(dt.Rows[0]["User_Sid"]))
                {
                    model.UserSid = (int)dt.Rows[0]["User_Sid"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Account"]))
                {
                    model.Account = (string)dt.Rows[0]["Account"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Password"]))
                {
                    model.Password = (string)dt.Rows[0]["Password"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Account_Level"]))
                {
                    model.Account_Level = (int)dt.Rows[0]["Account_Level"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["User_Name"]))
                {
                    model.User_Name = (string)dt.Rows[0]["User_Name"];
                }
            }
            return model;


        }
    }
}