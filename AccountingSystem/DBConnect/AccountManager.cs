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
        /// <summary>以Account為參數，取得使用者的帳號資訊</summary>
        public UserListModel GetAccount(string Account)
        {
            //宣告資料庫查尋字串，以Account為查詢條件，查詢帳號資訊
            string queryString =
                $@" Select 
	                    UserList.User_Sid, 
	                    Account, 
	                    UserList.Password, 
	                    Account_Level, 
	                    UserList.User_Name,
                        Email
                    From UserList
                    Where Account = @Account And Is_Delete = 'false'
                ";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
               new SqlParameter("@Account", Account)
            };

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            UserListModel model = new UserListModel();

            //檢查是否有取得資料
            if (dt.Rows.Count > 0)
            {
                //檢查所需欄位是否為空值，值果為否則將資料放進model的相應欄位
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

                if (!Convert.IsDBNull(dt.Rows[0]["Email"]))
                {
                    model.Email = (string)dt.Rows[0]["Email"];
                }
            }

            //回傳使用者資訊
            return model;


        }
    }
}