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
            IndexModel model = new IndexModel();//建立IndexModel實例
            IndexDBMethod method = new IndexDBMethod();//建立IndexDBMethod實例

            DataTable dtAccountingInfo = method.GetAccountingInfo();//取得記帳資訊
            DataTable dtTotalUser = method.GetTotalUser();//取得所有使用者數量

            //將相應資料用model承接
            model.FirstDate = dtAccountingInfo.Rows[0]["FirstDate"].ToString();
            model.LastDate = dtAccountingInfo.Rows[0]["LastDate"].ToString();
            model.TotalAccounting = dtAccountingInfo.Rows[0]["TotalAccounting"].ToString();
            model.TotalUser = dtTotalUser.Rows[0]["TotalUser"].ToString();

            //將model轉成JSON字串並回傳
            string returnJsonString = JsonConvert.SerializeObject(model);
            context.Response.ContentType = "application/json";
            context.Response.Write(returnJsonString);
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