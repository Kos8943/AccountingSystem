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
        /// <summary>取得資料庫的總資料筆數、第一筆記帳時間、最後一筆記帳時間</summary>
        public DataTable GetAccountingInfo()
        {
            //宣告資料庫查詢字串
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

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;


        }

        /// <summary>取得資料庫的使用者帳號數量</summary>
        public DataTable GetTotalUser()
        {
            //宣告資料庫查詢字串
            string queryString = $@"Select Count(User_Sid) As TotalUser From UserList Where Is_Delete = 'false'";

            List<SqlParameter> parameters = new List<SqlParameter>() { };

            //使用DBHelper的GetDataTable取得資料列表
            var dt = this.GetDataTable(queryString, parameters);

            //回傳資料列表
            return dt;
        }
    }
}