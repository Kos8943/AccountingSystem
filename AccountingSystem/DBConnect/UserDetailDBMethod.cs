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

        public DataTable GetUserInfo(string User_Sid)
        {

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


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }

        public void InsertUser(UserListModel model)
        {

            string queryString = $@"Insert Into
                                    UserList
                                    	(Account, UserList.Password, Email, UserList.User_Name, Account_Level, Create_Time, Modify_Time, Is_Delete)
                                    Values
                                    	(@Account, '12345678', @Email, @User_Name, @Account_Level, GETDATE(), GETDATE(), 'false')";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@Email", model.Email),
                   new SqlParameter("@User_Name", model.User_Name),
                   new SqlParameter("@Account_Level", model.Account_Level)
                }; ;


            this.ExecuteNonQuery(queryString, parameters);

        }

        public void UpdateUser(UserListModel model)
        {

            string queryString = $@"Update UserList
	                                Set UserList.User_Name = @User_Name, Email = @Email, Account_Level = @Account_Level, Modify_Time = GETDATE()
	                                Where UserList.User_Sid = @User_Sid And Account = @Account And Is_Delete = 'false' ";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", model.UserSid),
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@Email", model.Email),
                   new SqlParameter("@User_Name", model.User_Name),
                   new SqlParameter("@Account_Level", model.Account_Level)
                }; ;


            this.ExecuteNonQuery(queryString, parameters);

        }

        public bool CheckAccountRepetition(string Account)
        {

            string queryString = $@"Select Account From UserList Where UserList.Account = @Account And Is_Delete = 'false' ";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", Account)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            if(dt.Rows.Count == 0)
                return true;            
            else 
                return false; 

            

        }
    }
}