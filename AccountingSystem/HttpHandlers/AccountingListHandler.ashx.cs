using AccountingSystem.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// AccountingListHandler 的摘要描述
    /// </summary>
    public class AccountingListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var deleteSid = context.Request.Form["Delete_Sid[]"].Split(',');

            if (deleteSid.Length > 0) {

                AccountingListDBMethod method = new AccountingListDBMethod();

                foreach (var sid in deleteSid) {
                    method.DeleteAccounting(sid);
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
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