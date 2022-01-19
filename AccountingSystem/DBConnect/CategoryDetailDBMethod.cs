using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class CategoryDetailDBMethod : DBHelper
    {
		/// <summary>以User_Sid、CategorySid為參數，取得單筆記帳分類</summary>
		public DataTable GetCategory(string User_Sid, string CategorySid)
		{

			//宣告資料庫查詢字串，以CategorySid、User_List_User_Sid為查詢條件
			string queryString = $@"Select 
										Category_Name, 
										Description 
									From AccountCategoryList 
									Where Category_Sid = @CategorySid And User_List_User_Sid = @User_Sid And Is_Delete = 'false'";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
					new SqlParameter("@CategorySid", CategorySid),
				    new SqlParameter("@User_Sid", User_Sid)
				}; ;

			//使用DBHelper的GetDataTable取得資料列表
			var dt = this.GetDataTable(queryString, parameters);

			//回傳資料列表
			return dt;

		}

		/// <summary>以User_Sid、CategoryName為參數，取得單筆記帳分類，用來確認記帳分類是否重複</summary>
		public DataTable CheckCategoryRepetition(string User_Sid, string CategoryName)
		{
			//宣告資料庫查詢字串，以User_List_User_Sid、Category_Name為查詢條件
			string queryString = $@"Select 
										Category_Sid
									from AccountCategoryList 
									Where User_List_User_Sid = @UserSid And Category_Name = @CategoryName And Is_Delete = 'false';";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@UserSid", User_Sid)
				};

			//使用DBHelper的GetDataTable取得資料列表
			var dt = this.GetDataTable(queryString, parameters);

			//回傳資料列表
			return dt;
		}

		/// <summary>以User_Sid、CategoryName、Description為參數，新增記帳分類</summary>
		public void InsertCategory(string User_Sid, string CategoryName, string Description)
		{
			//宣告資料庫查詢字串
			string queryString = $@"insert into 
									AccountCategoryList 
										(User_List_User_Sid, Create_Time, Category_Name, AccountCategoryList.Description, Is_Delete) 
									values
										(@UserSid, GETDATE(), @CategoryName, @Description, 'false')";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
					new SqlParameter("@UserSid", User_Sid),
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@Description", Description)
				};

			//使用DBHelper的ExecuteNonQuery更新資料列表
			this.ExecuteNonQuery(queryString, parameters);
		}

		/// <summary>以User_Sid、CategoryName、Description、CategorySid為參數，修改記帳分類</summary>
		public void UpdateCategory(string CategoryName, string Description, string CategorySid, string User_Sid )
		{
			//宣告資料庫修改字串
			string queryString = $@"Update AccountCategoryList 
									Set Category_Name = @CategoryName, AccountCategoryList.Description = @Description 
									Where Category_Sid = @CategorySid And User_List_User_Sid = @UserSid And Is_Delete = 'false';";

			//使用List<SqlParameter>承接參數化查詢
			List<SqlParameter> parameters = new List<SqlParameter>()
				{				
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@Description", Description),
					new SqlParameter("@CategorySid", CategorySid),
					new SqlParameter("@UserSid", User_Sid)
				};

			//使用DBHelper的ExecuteNonQuery更新資料列表
			this.ExecuteNonQuery(queryString, parameters);
		}
	}
}