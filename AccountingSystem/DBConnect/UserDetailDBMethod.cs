using AccountingSystem.Helpers;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class UserDetailDBMethod : DBHelper
    {
        /// <summary>以User_Sid為參數，取得一筆使用者資訊</summary>
        public DataTable GetUserInfo(string User_Sid)
        {
            //宣告資料庫查詢字串，以UserList.User_Sid為查詢條件
            string queryString = $@"Select
                                       UserList.User_Sid, 
                                       Account, UserList.Password, 
                                       Email, UserList.User_Name, 
                                       Account_Level, 
                                       convert(varchar, UserList.Create_Time, 120) AS Create_Time, 
                                       convert(varchar, UserList.Modify_Time, 120) AS Modify_Time, 
                                       Is_Delete 
                                    From UserList 
                                    Where UserList.User_Sid = @User_Sid And Is_Delete = 'false'";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;

        }

        /// <summary>以UserListModel為參數，新增使用者帳號</summary>
        public void InsertUser(UserListModel model)
        {
            //宣告資料庫新增字串
            string queryString = $@"Insert Into
                                    UserList
                                    	(Account, UserList.Password, Email, UserList.User_Name, Account_Level, Create_Time, Modify_Time, Is_Delete)
                                    Values
                                    	(@Account, '12345678', @Email, @User_Name, @Account_Level, GETDATE(), GETDATE(), 'false')";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@Email", model.Email),
                   new SqlParameter("@User_Name", model.User_Name),
                   new SqlParameter("@Account_Level", model.Account_Level)
                }; ;

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);

        }

        /// <summary>以UserListModel為參數，修改使用者帳號</summary>
        public void UpdateUser(UserListModel model)
        {
            //宣告資料庫修改字串
            string queryString = $@"Update UserList
	                                Set UserList.User_Name = @User_Name, Email = @Email, Account_Level = @Account_Level, Modify_Time = GETDATE()
	                                Where UserList.User_Sid = @User_Sid And Account = @Account And Is_Delete = 'false' ";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", model.UserSid),
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@Email", model.Email),
                   new SqlParameter("@User_Name", model.User_Name),
                   new SqlParameter("@Account_Level", model.Account_Level)
                }; ;

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);

        }

        /// <summary>以Account為參數，檢查使用者帳號是否重複</summary>
        public bool CheckAccountRepetition(string Account)
        {
            //宣告資料庫修改字串
            string queryString = $@"Select Account From UserList Where UserList.Account = @Account And Is_Delete = 'false' ";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", Account)
                }; ;

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);


            //dt.Rows.Count長度等於0回傳true，否則回傳false
            if (dt.Rows.Count == 0)
                return true;            
            else 
                return false; 

            

        }
    }
}