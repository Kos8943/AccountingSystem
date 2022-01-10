using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class UserListDBMethod : DBHelper
    {
        public DataTable GetUserList()
        {

            string queryString = $@"Select 
                                    	User_Sid, 
                                    	Account, 
                                    	User_Name, 
                                    	Email, 
                                    	Account_Level, 
                                    	replace(convert(varchar(16), Create_Time, 120), '-', '/') As Create_Time 
                                    from UserList Where Is_Delete = 'false';";

            List<SqlParameter> parameters = new List<SqlParameter>();

            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }


        public void DeleteUser(string User_Sid)
        {

            string queryString = $@"Update UserList Set Is_Delete = 'true' Where User_Sid = @User_Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>() 
            {
                new SqlParameter("@User_Sid", User_Sid)
            };

            this.ExecuteNonQuery(queryString, parameters);

        }
    }
}