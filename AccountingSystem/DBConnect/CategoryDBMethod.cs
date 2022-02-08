using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class CategoryDBMethod : DBHelper
    {
		/// <summary>以UserSid為參數，取得當下使用者的所有記帳分類</summary>
		public DataTable GetCategoryList(int User_Sid)
        {
			//宣告資料庫查尋字串
			string queryString = $@"Select 
										a.Category_Sid,
										a.User_List_User_Sid,
										a.Create_Time,
										a.Category_Name,
										a.Description,
										a.Has_Accounting from 
											(
												Select
												Category_Sid, 
												User_List_User_Sid,
												convert(varchar, AccountCategoryList.Create_Time, 111) AS Create_Time, 
												Category_Name, 
												AccountCategoryList.Description, 
												Count(Account_Sid) As Has_Accounting,
												AccountCategoryList.Is_Delete
												from AccountCategoryList 
												Left Join AccountList On Category_Sid = AccountList.CategoryList_CategorySid 
												Where AccountCategoryList.Is_Delete = 'false' And AccountList.Is_Delete = 'false' Or							AccountList.Is_Delete Is Null
												Group By Category_Sid, User_List_User_Sid, AccountCategoryList.Create_Time,	Category_Name,						AccountCategoryList.Description, AccountCategoryList.Is_Delete
											) a Where User_List_User_Sid = @User_Sid And a.Is_Delete = 'false' 
										Order By a.Create_Time Desc ";

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

		/// <summary>以CategorySid為參數，取得該筆記帳分類記帳紀錄</summary>
		public DataTable GetCategoryAccountingRecord(string CategorySid)
		{

			//宣告資料庫查尋字串，以CategoryList_CategorySid為查詢條件，並JOIN進AccountCategoryList
			string queryString = $@"Select 
										AccountCategoryList.Category_Name 
									from AccountList 
									Join AccountCategoryList On AccountList.CategoryList_CategorySid = AccountCategoryList.Category_Sid 
									Where CategoryList_CategorySid = @CategorySid";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
				   new SqlParameter("@CategorySid", CategorySid)
				}; ;

			//使用DBHelper的GetDataTable取得資料列表
			var dt = this.GetDataTable(queryString, parameters);

			//回傳資料列表
			return dt;

		}

		/// <summary>以CategorySid為參數，刪除記帳分類</summary>
		public void DeleteCategory(string CategorySid)
		{
			//宣告資料庫修改字串，以Category_Sid為修改條件，將Is_Delete改成'true'
			string queryString = $@"Update AccountCategoryList Set Is_Delete = 'true' Where Category_Sid = @CategorySid ";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
				   new SqlParameter("@CategorySid", CategorySid)
				}; ;

			//使用DBHelper的ExecuteNonQuery更新資料列表
			this.ExecuteNonQuery(queryString, parameters);
		}
	}
}