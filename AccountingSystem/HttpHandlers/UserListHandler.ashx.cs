using AccountingSystem.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// UserListHandler 的摘要描述
    /// </summary>
    public class UserListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var deleteSid = context.Request.Form["Delete_Sid[]"].Split(',');

            if(deleteSid.Length > 0)
            {
                UserListDBMethod method = new UserListDBMethod();

                foreach (var sid in deleteSid)
                {
                    method.DeleteUser(sid);
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