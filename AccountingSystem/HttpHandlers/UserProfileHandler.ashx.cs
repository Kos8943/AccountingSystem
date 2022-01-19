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

            string quertStringAction = context.Request.QueryString["Action"];//取得QueryString的Action值

            LoginInfoModel sessionCurrentUser = LoginHelper.GetCurrentUserInfo();//從session取得當下使用者資訊
            UserProfileDBMethod method = new UserProfileDBMethod();

            //-----Method:GET-----

            //quertStringAction為"GetCurrentUserInfo"
            if (quertStringAction == "GetCurrentUserInfo")
            {
                //取得當下使用者的資訊
                DataTable dt = method.GetUserProfile(sessionCurrentUser);

                //用LoginInfoModel承接dt的資料
                LoginInfoModel model = new LoginInfoModel();
                model.Account = dt.Rows[0]["Account"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.User_Name = dt.Rows[0]["User_Name"].ToString();

                //將model轉成JSON字串並回傳
                string jsonString = JsonConvert.SerializeObject(model);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonString);
                return;
            }

            //---------------------

            //-----Method:POST-----

            string formDataAction = context.Request.Form["Action"];//取得FormData的Action值

            //formDataAction為"UpdateUserProfile"
            if (formDataAction == "UpdateUserProfile")
            {
                string formDataUserName = context.Request.Form["UserName"];//取得FormData的UserName值
                string formDataUserEmail = context.Request.Form["UserEmail"];//取得FormData的UserEmail值

                //修改使用者資訊並回傳成功字串
                method.UpdateUserProfile(sessionCurrentUser, formDataUserName, formDataUserEmail);
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }

            //---------------------
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