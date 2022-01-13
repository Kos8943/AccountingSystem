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
    public class UserProfileDBMethod : DBHelper
    {
        public DataTable GetUserProfile(LoginInfoModel model)
        {

            string queryString = $@"Select  
                                    	User_Sid, 
                                    	Account, 
                                    	(User_Name), 
                                    	Email 
                                    from UserList Where User_Sid = @UserSid AND Account = @Account AND Is_Delete = 'false'";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@UserSid", model.UserSid)

                }; ;


            DataTable dt = this.GetDataTable(queryString, parameters);

            return dt;

        }

        public void UpdateUserProfile(LoginInfoModel model, string userName, string email)
        {

            string queryString = $@"Update UserList 
	                                    Set User_Name = @User_Name, Email = @Email 
	                                    Where Account = @Account AND User_Sid = @UserSid AND Is_Delete = 'false'";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Name", userName),
                   new SqlParameter("@Email", email),
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@UserSid", model.UserSid)

                }; ;


            this.ExecuteNonQuery(queryString, parameters);

        }
    }
}