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
        /// <summary>使用者帳號列表</summary>
        public DataTable GetUserList()
        {
            //宣告資料庫修改字串
            string queryString = $@"Select 
                                    	User_Sid, 
                                    	Account, 
                                    	User_Name, 
                                    	Email, 
                                    	Account_Level, 
                                    	replace(convert(varchar(16), Create_Time, 120), '-', '/') As Create_Time 
                                    from UserList Where Is_Delete = 'false';";

            List<SqlParameter> parameters = new List<SqlParameter>();

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;

        }

        /// <summary>以User_Sid為參數，刪除單筆使用者帳號</summary>
        public void DeleteUser(string User_Sid)
        {
            //宣告資料庫修改字串
            string queryString = $@"Update UserList Set Is_Delete = 'true' Where User_Sid = @User_Sid;";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>() 
            {
                new SqlParameter("@User_Sid", User_Sid)
            };

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);

        }
    }
}