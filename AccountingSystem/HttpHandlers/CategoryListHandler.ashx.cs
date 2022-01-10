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
            var deleteSid = context.Request.Form["Delete_Sid[]"].Split(',');

            if(deleteSid.Length > 0)
            {
                CategoryDBMethod method = new CategoryDBMethod();

                foreach (var sid in deleteSid)
                {
                    DataTable dt = method.GetCategoryAccountingRecord(sid);

                    if (dt.Rows.Count > 0) 
                    {
                        Result result = new Result()
                        {
                            result = "Fail",
                            message = $"請刪除{dt.Rows[0]["Category_Name"]}的記帳紀錄"
                        };

                        string jsonString = JsonConvert.SerializeObject(result);
                        context.Response.ContentType = "application/json";
                        context.Response.Write(jsonString);                     
                        return;
                    }                      
                }

                foreach (var sid in deleteSid)
                {
                    method.DeleteCategory(sid);
                }

                Result successResult = new Result()
                {
                    result = "success"
                };

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