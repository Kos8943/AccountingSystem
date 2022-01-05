using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccountingSystem.DBConnect
{
    public class IndexDBMethod : DBHelper
    {
        public DataTable GetAccountingInfo()
        {
            //使用的SQL語法
            string queryString = $@"Select 
                                        Count(a.Account_Sid) As TotalAccounting, 
                                        convert(varchar, a.FirstDate, 120) As FirstDate,
                                        convert(varchar, a.LastDate, 120) As LastDate From 
                                    (Select AccountList.Account_Sid,
                                    FIRST_VALUE(Create_Time) OVER(
                                            ORDER BY Create_Time 
                                        ) As FirstDate,
                                    Last_VALUE(Create_Time) OVER(
                                            ORDER BY Create_Time RANGE BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING
                                        ) As LastDate
                                        From AccountList Where Is_Delete = 'false' 
                                        Group By Create_Time, AccountList.Account_Sid) a
                                        Group By a.FirstDate, a.LastDate";

            List<SqlParameter> parameters = new List<SqlParameter>() { };


            var dt = this.GetDataTable(queryString, parameters);

            return dt;


        }

        public DataTable GetTotalUser()
        {

            string queryString = $@"Select Count(User_Sid) As TotalUser From UserList Where Is_Delete = 'false'";

            List<SqlParameter> parameters = new List<SqlParameter>() { };


            var dt = this.GetDataTable(queryString, parameters);

            return dt;
        }
    }
}