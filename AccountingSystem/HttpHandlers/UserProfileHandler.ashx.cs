using AccountingSystem.DBConnect;
using AccountingSystem.Helpers;
using AccountingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// UserProfileHandler 的摘要描述
    /// </summary>
    public class UserProfileHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            string quertStringAction = context.Request.QueryString["Action"];

            LoginInfoModel sessionCurrentUser = LoginHelper.GetCurrentUserInfo();
            UserProfileDBMethod method = new UserProfileDBMethod();

            if (quertStringAction == "GetCurrentUserInfo")
            {
                DataTable dt = method.GetUserProfile(sessionCurrentUser);

                LoginInfoModel model = new LoginInfoModel();
                model.Account = dt.Rows[0]["Account"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.User_Name = dt.Rows[0]["User_Name"].ToString();

                string jsonString = JsonConvert.SerializeObject(model);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonString);
                return;
            }

            string formDataAction = context.Request.Form["Action"];

            if(formDataAction == "UpdateUserProfile")
            {
                string formDataUserName = context.Request.Form["UserName"];
                string formDataUserEmail = context.Request.Form["UserEmail"];

                method.UpdateUserProfile(sessionCurrentUser, formDataUserName, formDataUserEmail);

                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            //context.Response.ContentType = "text/plain";
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