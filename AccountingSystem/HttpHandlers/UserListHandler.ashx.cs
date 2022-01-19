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
            //將FormData的Delete_Sid值用Split切割成陣列
            var deleteSid = context.Request.Form["Delete_Sid[]"].Split(',');

            //deleteSid長度大於0
            if (deleteSid.Length > 0)
            {
                UserListDBMethod method = new UserListDBMethod();

                //使用foreach逐筆刪除使用者帳號
                foreach (var sid in deleteSid)
                {
                    method.DeleteUser(sid);
                }

                //回傳成功字串
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