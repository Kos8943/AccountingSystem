using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class AccountingListDBMethod : DBHelper
    {

        /// <summary>以UserSid為參數，取得當下使用者的所有記帳紀錄</summary>
        public DataTable GetAccountingList(int User_Sid)
        {

            //宣告資料庫查尋字串，以User_Sid為查詢條件，JOIN進AccountCategoryList，以倒敘Create_Time列出
            string queryString = $@"select 
                                	Account_Sid, 
                                	convert(varchar, AccountList.Create_Time, 111) AS Create_Time, 
                                	Category_Name, 
                                	Price_Type, 
                                	Price, Tittle, 
                                AccountCategoryList.Description from AccountList 
                                Join AccountCategoryList On AccountList.CategoryList_CategorySid = AccountCategoryList.Category_Sid 
                                Where AccountList.UserList_UserSid = @User_Sid And AccountList.Is_Delete = 'false' And AccountCategoryList.Is_Delete = 'false' 
                                Order By Create_Time Desc";



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


        /// <summary>以Account_Sid為參數，刪除單筆記帳紀錄</summary>
        public void DeleteAccounting(string Account_Sid) 
        {

            //宣告資料庫修改字串，以Account_Sid為修改條件將Is_Delete改成'true'
            string queryString = $@"Update AccountList Set Is_Delete = 'true' Where Account_Sid = @User_Sid ";

            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", Account_Sid)
                }; ;

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);
        }
    }
}