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
        public DataTable GetCategoryList(int User_Sid)
        {

            string queryString = $@"Select 
                                        Category_Sid, Category_Name 
                                    From AccountCategoryList 
                                    Where User_List_User_Sid = @User_Sid And Is_Delete = 'false'";




            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }

        public DataTable GetAccounting(int UserSid, string AccountSid)
        {

            string queryString = $@"Select 
                                    	CategoryList_CategorySid,
                                    	Price_Type,
                                    	Price,
                                    	Tittle,
                                    	AccountList.Description
                                    From AccountList 
                                    Where Account_Sid = @AccountSid And UserList_UserSid = @UserSid And Is_Delete = 'false'";




            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserSid", UserSid),
                   new SqlParameter("@AccountSid", AccountSid)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }


        public void UpdateAccounting(AccountingListModel model)
        {

            string queryString = $@"Update AccountList 
                                    Set 
                                    	CategoryList_CategorySid = @CategorySid, 
                                    	Price_Type = @Price_Type, 
                                    	Price = @Price, 
                                    	Tittle = @Tittle, 
                                    	AccountList.Description = @Description
                                    Where Account_Sid = @Account_Sid And UserList_UserSid = @UserSid And Is_Delete = 'false'";




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


            this.ExecuteNonQuery(queryString, parameters);


        }

        public void InsterAccounting(AccountingListModel model)
        {

            string queryString = $@"Insert Into 
	                                    AccountList
	                                    	(UserList_UserSid, CategoryList_CategorySid, Price_Type, Price, Tittle,               AccountList.Description, Create_Time, Is_Delete)
	                                    Values
	                                    	(@UserList_UserSid, @CategoryList_CategorySid, @Price_Type, @Price, @Tittle, @Description, GETDATE(), 'false')";




            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserList_UserSid", model.UserList_UserSid),
                   new SqlParameter("@CategoryList_CategorySid", model.CategoryList_CategorySid),
                   new SqlParameter("@Price_Type", model.Price_Type),
                   new SqlParameter("@Price", model.Price),
                   new SqlParameter("@Tittle", model.Tittle),
                   new SqlParameter("@Description", model.Description)
                }; ;


            this.ExecuteNonQuery(queryString, parameters);
        }


        public string GetInsertAccountingSid(int UserSid)
        {

            string queryString = $@"Select Account_Sid From AccountList Where UserList_UserSid = @UserSid And Is_Delete = 'false' Order By Account_Sid Desc";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@UserSid", UserSid)
                }; ;


            string sid = this.GetScale(queryString, parameters).ToString();

            return sid;
        }
    }
}