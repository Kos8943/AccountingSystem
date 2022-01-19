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
    public class AccountingDetailDBMethod : DBHelper
    {

        /// <summary>取得當下使用者的記帳分類列表</summary>
        public DataTable GetCategoryList(int User_Sid)
        {

            //從AccountCategoryList資料表取出Category_Sid、Category_Name資料列表，
            //並以User_List_User_Sid、Is_Delete為收尋條件
            string queryString = $@"Select 
                                        Category_Sid, Category_Name 
                                    From AccountCategoryList 
                                    Where User_List_User_Sid = @User_Sid And Is_Delete = 'false'";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;

        }

        /// <summary>以AccountSid、UserList_UserSid為收尋條件，取得一筆記帳紀錄</summary>
        public DataTable GetAccounting(int UserSid, string AccountSid)
        {

            //宣告資料庫查尋字串
            string queryString = $@"Select 
                                    	CategoryList_CategorySid,
                                    	Price_Type,
                                    	Price,
                                    	Tittle,
                                    	AccountList.Description
                                    From AccountList 
                                    Where Account_Sid = @AccountSid And UserList_UserSid = @UserSid And Is_Delete = 'false'";


            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserSid", UserSid),
                   new SqlParameter("@AccountSid", AccountSid)
                }; ;

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;

        }

        /// <summary>使用AccountingListModel為參數，以Account_Sid、UserList_UserSid為條件，更新單筆記帳列表</summary>
        public void UpdateAccounting(AccountingListModel model)
        {

            //宣告資料庫更新字串
            string queryString = $@"Update AccountList 
                                    Set 
                                    	CategoryList_CategorySid = @CategorySid, 
                                    	Price_Type = @Price_Type, 
                                    	Price = @Price, 
                                    	Tittle = @Tittle, 
                                    	AccountList.Description = @Description
                                    Where Account_Sid = @Account_Sid And UserList_UserSid = @UserSid And Is_Delete = 'false'";


            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@CategorySid", model.CategoryList_CategorySid),
                   new SqlParameter("@Price_Type", model.Price_Type),
                   new SqlParameter("@Price", model.Price),
                   new SqlParameter("@Tittle", model.Tittle),
                   new SqlParameter("@Description", model.Description),
                   new SqlParameter("@Account_Sid", model.Account_Sid),
                   new SqlParameter("@UserSid", model.UserList_UserSid)
                }; ;

            //使用DBHelper的ExecuteNonQuery更新資料列表
            this.ExecuteNonQuery(queryString, parameters);

        }

        /// <summary>使用AccountingListModel為參數，新增單筆記帳列表</summary>
        public void InsterAccounting(AccountingListModel model)
        {

            //宣告資料庫新增字串
            string queryString = $@"Insert Into 
	                                    AccountList
	                                    	(UserList_UserSid, CategoryList_CategorySid, Price_Type, Price, Tittle,               AccountList.Description, Create_Time, Is_Delete)
	                                    Values
	                                    	(@UserList_UserSid, @CategoryList_CategorySid, @Price_Type, @Price, @Tittle, @Description, GETDATE(), 'false')";



            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserList_UserSid", model.UserList_UserSid),
                   new SqlParameter("@CategoryList_CategorySid", model.CategoryList_CategorySid),
                   new SqlParameter("@Price_Type", model.Price_Type),
                   new SqlParameter("@Price", model.Price),
                   new SqlParameter("@Tittle", model.Tittle),
                   new SqlParameter("@Description", model.Description)
                }; ;


            //使用DBHelper的ExecuteNonQuery新增資料列表
            this.ExecuteNonQuery(queryString, parameters);
        }


        /// <summary>以UserSid為參數，取得最新一筆新增記帳的Account_Sid</summary>
        public string GetInsertAccountingSid(int UserSid)
        {
            //宣告資料庫查尋字串
            string queryString = $@"Select Account_Sid From AccountList Where UserList_UserSid = @UserSid And Is_Delete = 'false' Order By Account_Sid Desc";


            //使用List<SqlParameter>承接參數化查詢
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserSid", UserSid)
                }; ;


            //使用DBHelper的GetScale取得第一筆資料列表
            string sid = this.GetScale(queryString, parameters).ToString();

            //回傳Account_Sid
            return sid;
        }
    }
}