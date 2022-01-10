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
        public DataTable GetCategoryList(int User_Sid)
        {

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
											Where User_List_User_Sid = @User_Sid And AccountCategoryList.Is_Delete = 'false' And AccountList.Is_Delete = 'false' Or AccountList.Is_Delete Is Null
											Group By Category_Sid, User_List_User_Sid, AccountCategoryList.Create_Time, Category_Name, AccountCategoryList.Description, AccountCategoryList.Is_Delete
										) a Where a.Is_Delete = 'false' 
									Order By a.Create_Time Desc ;";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }

		public DataTable GetCategoryAccountingRecord(string CategorySid)
		{

			string queryString = $@"Select 
										AccountCategoryList.Category_Name 
									from AccountList 
									Join AccountCategoryList On AccountList.CategoryList_CategorySid = AccountCategoryList.Category_Sid 
									Where CategoryList_CategorySid = @CategorySid";
									
			List<SqlParameter> parameters = new List<SqlParameter>()
				{
				   new SqlParameter("@CategorySid", CategorySid)
				}; ;


			var dt = this.GetDataTable(queryString, parameters);

			return dt;

		}

		public void DeleteCategory(string CategorySid)
		{

			string queryString = $@"Update AccountCategoryList Set Is_Delete = 'true' Where Category_Sid = @CategorySid ";

			List<SqlParameter> parameters = new List<SqlParameter>()
				{
				   new SqlParameter("@CategorySid", CategorySid)
				}; ;


			this.ExecuteNonQuery(queryString, parameters);
		}
	}
}