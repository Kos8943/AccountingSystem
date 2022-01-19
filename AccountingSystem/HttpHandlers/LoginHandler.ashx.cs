using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// LoginHandler 的摘要描述
    /// </summary>
    public class LoginHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string account = context.Request.Form["account"];//取得FormData的account值
            string password = context.Request.Form["password"];//取得FormData的password值

            //以account、password為參數，使用LoginHelper.TryLogin嘗試登入，登入成功則回傳true
            if (LoginHelper.TryLogin(account, password)) {
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