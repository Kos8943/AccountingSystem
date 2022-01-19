using AccountingSystem.DBConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// CategoryListHandler 的摘要描述
    /// </summary>
    public class CategoryListHandler : IHttpHandler
    {

        private class Result
        {
            public string result { get; set; }

            public string message { get; set; }
        }
        public void ProcessRequest(HttpContext context)
        {
            //將FormData的Delete_Sid值用Split切割成陣列
            var deleteSid = context.Request.Form["Delete_Sid[]"].Split(',');

            //deleteSid長度大於0
            if (deleteSid.Length > 0)
            {

                CategoryDBMethod method = new CategoryDBMethod();//建立CategoryDBMethod實例


                foreach (var sid in deleteSid)
                {
                    //以CategorySid為參數查詢資料庫
                    DataTable dt = method.GetCategoryAccountingRecord(sid);

                    //dt長度大於0，則判定為有相同分類存在
                    if (dt.Rows.Count > 0) 
                    {
                        //建立Result實例，並將結果及錯誤訊息回傳
                        Result result = new Result()
                        {
                            result = "Fail",
                            message = $"請刪除{dt.Rows[0]["Category_Name"]}的記帳紀錄"
                        };

                        //將result轉成JSON字串並回傳
                        string jsonString = JsonConvert.SerializeObject(result);
                        context.Response.ContentType = "application/json";
                        context.Response.Write(jsonString);                     
                        return;
                    }                      
                }

                //使用foreach逐筆刪除分類
                foreach (var sid in deleteSid)
                {
                    method.DeleteCategory(sid);
                }

                //建立Result實例，並將結果回傳
                Result successResult = new Result()
                {
                    result = "success"
                };

                //將result轉成JSON字串並回傳
                string successJsonString = JsonConvert.SerializeObject(successResult);
                context.Response.ContentType = "application/json";
                context.Response.Write(successJsonString);
            }

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}