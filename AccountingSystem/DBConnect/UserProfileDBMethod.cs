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
        /// <summary>以LoginInfoModel為參數，取得當下使用者帳號資訊</summary>
        public DataTable GetUserProfile(LoginInfoModel model)
        {
            //宣告資料庫修改字串
            string queryString = $@"Select  
                                    	User_Sid, 
                                    	Account, 
                                    	(User_Name), 
                                    	Email 
                                    from UserList Where User_Sid = @UserSid AND Account = @Account AND Is_Delete = 'false'";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@UserSid", model.UserSid)

                }; ;

            //使用DBHelper的GetDataTable取得資料列表
            DataTable dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;

        }

        /// <summary>model、userName、email為參數，修改當下使用者帳號資訊</summary>
        public void UpdateUserProfile(LoginInfoModel model, string userName, string email)
        {
            //宣告資料庫修改字串
            string queryString = $@"Update UserList 
	                                    Set User_Name = @User_Name, Email = @Email 
	                                    Where Account = @Account AND User_Sid = @UserSid AND Is_Delete = 'false'";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Name", userName),
                   new SqlParameter("@Email", email),
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@UserSid", model.UserSid)

                }; ;

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);

        }
    }
}