using AccountingSystem.DBConnect;
using AccountingSystem.Helpers;
using AccountingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingSystem.HttpHandlers
{
    /// <summary>
    /// UserDetailHandler 的摘要描述
    /// </summary>
    public class UserDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string quertStringAction = context.Request.QueryString["Action"];
            string quertStringUserSid = context.Request.QueryString["UserSid"];
            string formDataAction = context.Request.Form["Action"];

            if(quertStringAction == "GetUserInfo")
            {             
                string jsonString = GetUserInfo(quertStringUserSid);

                if(jsonString != null)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonString);
                }
                
            }
            else if (formDataAction == "CheckAccountRepetition")
            {
                string formDataAccount = context.Request.Form["Account"];

                UserDetailDBMethod method = new UserDetailDBMethod();

                if (method.CheckAccountRepetition(formDataAccount))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("");
                }

            }
            else if(formDataAction == "Insert")
            {
                UserDetailDBMethod method = new UserDetailDBMethod();
                UserListModel model = GetFormDateUserInfo("Insert", context);

                method.InsertUser(model);

                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            else if(formDataAction == "Update")
            {
                UserDetailDBMethod method = new UserDetailDBMethod();
                UserListModel model = GetFormDateUserInfo("Update", context);

                method.UpdateUser(model);

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

        private string GetUserInfo(string UserSid)
        {

            UserDetailDBMethod method = new UserDetailDBMethod();
            DataTable dt = method.GetUserInfo(UserSid);


            if (dt.Rows.Count == 0)
                return null;

            UserListModel model = new UserListModel();

            model.UserSid = (int)dt.Rows[0]["User_Sid"];
            model.Account = dt.Rows[0]["Account"].ToString();
            model.Password = dt.Rows[0]["Password"].ToString();
            model.Email = dt.Rows[0]["Email"].ToString();
            model.User_Name = dt.Rows[0]["User_Name"].ToString();
            model.Account_Level = (int)dt.Rows[0]["Account_Level"];
            model.Create_Time = dt.Rows[0]["Create_Time"].ToString();
            model.Modify_Time = dt.Rows[0]["Modify_Time"].ToString();

            string jsonString = JsonConvert.SerializeObject(model);

            return jsonString;
        }

        private UserListModel GetFormDateUserInfo(string action, HttpContext context)
        {
            UserListModel model = new UserListModel();

            model.User_Name = context.Request.Form["UserName"];
            model.Email = context.Request.Form["Email"];
            model.Account_Level = Convert.ToInt32(context.Request.Form["AccountLevel"]);
            model.Account = context.Request.Form["Account"];

            if (action == "Insert")
            {               
                model.Create_Time = DateTime.Now.ToString();
                model.Modify_Time = DateTime.Now.ToString();
            }else if(action == "Update")
            {
                
                model.UserSid = Convert.ToInt32(context.Request.Form["UserSid"]);
                model.Modify_Time = DateTime.Now.ToString();
            }


            return model;
        }
    }
}