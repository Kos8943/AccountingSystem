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
        public DataTable GetAccountingList(int User_Sid)
        {                                                

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


           

            List<SqlParameter> parameters = new List<SqlParameter>() 
                {
                   new SqlParameter("@User_Sid", User_Sid)
                }; ;


            var dt = this.GetDataTable(queryString, parameters);

            return dt;

        }


        public void DeleteAccounting(string Account_Sid) 
        {

            string queryString = $@"Update AccountList Set Is_Delete = 'true' Where Account_Sid = @User_Sid ";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@User_Sid", Account_Sid)
                }; ;

            this.ExecuteNonQuery(queryString, parameters);
        }
    }
}