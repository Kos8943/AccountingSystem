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
		public DataTable GetCategory(string User_Sid, string CategorySid)
		{

			string queryString = $@"Select 
										Category_Name, 
										Description 
									From AccountCategoryList 
									Where Category_Sid = @CategorySid And User_List_User_Sid = @User_Sid And Is_Delete = 'false'";


			List<SqlParameter> parameters = new List<SqlParameter>()
				{
				new SqlParameter("@CategorySid", CategorySid),
				   new SqlParameter("@User_Sid", User_Sid)
				}; ;


			var dt = this.GetDataTable(queryString, parameters);

			return dt;

		}


		public DataTable CheckCategoryRepetition(string User_Sid, string CategoryName)
		{

			string queryString = $@"Select 
										Category_Sid
									from AccountCategoryList 
									Where User_List_User_Sid = @UserSid And Category_Name = @CategoryName And Is_Delete = 'false';";


			List<SqlParameter> parameters = new List<SqlParameter>()
				{
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@UserSid", User_Sid)
				};


			var dt = this.GetDataTable(queryString, parameters);

			return dt;
		}

		public void InsertCategory(string User_Sid, string CategoryName, string Description)
		{

			string queryString = $@"insert into 
									AccountCategoryList 
										(User_List_User_Sid, Create_Time, Category_Name, AccountCategoryList.Description, Is_Delete) 
									values
										(@UserSid, GETDATE(), @CategoryName, @Description, 'false')";


			List<SqlParameter> parameters = new List<SqlParameter>()
				{
					new SqlParameter("@UserSid", User_Sid),
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@Description", Description)
				};


			this.ExecuteNonQuery(queryString, parameters);
		}


		public void UpdateCategory(string CategoryName, string Description, string CategorySid, string User_Sid )
		{

			string queryString = $@"Update AccountCategoryList 
									Set Category_Name = @CategoryName, AccountCategoryList.Description = @Description 
									Where Category_Sid = @CategorySid And User_List_User_Sid = @UserSid And Is_Delete = 'false';";


			List<SqlParameter> parameters = new List<SqlParameter>()
				{				
					new SqlParameter("@CategoryName", CategoryName),
					new SqlParameter("@Description", Description),
					new SqlParameter("@CategorySid", CategorySid),
					new SqlParameter("@UserSid", User_Sid)
				};


			this.ExecuteNonQuery(queryString, parameters);
		}
	}
}