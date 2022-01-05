using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AccountingSystem.DBConnect;
using AccountingSystem.Models;
using Newtonsoft.Json;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// IndexHandler 的摘要描述
    /// </summary>
    public class IndexHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            IndexModel model = new IndexModel();
            IndexDBMethod method = new IndexDBMethod();

            DataTable dtAccountingInfo = method.GetAccountingInfo();
            DataTable dtTotalUser = method.GetTotalUser();

            model.FirstDate = dtAccountingInfo.Rows[0]["FirstDate"].ToString();
            model.LastDate = dtAccountingInfo.Rows[0]["LastDate"].ToString();
            model.TotalAccounting = dtAccountingInfo.Rows[0]["TotalAccounting"].ToString();
            model.TotalUser = dtTotalUser.Rows[0]["TotalUser"].ToString();

            string returnJsonString = JsonConvert.SerializeObject(model);
            

            context.Response.ContentType = "application/json";
            context.Response.Write(returnJsonString);
            //context.Response.Write("Hello World");
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