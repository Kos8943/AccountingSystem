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
            //-----Method:GET-----

            string quertStringAction = context.Request.QueryString["Action"];//取得QueryString的Action值
            string quertStringUserSid = context.Request.QueryString["UserSid"];//取得QueryString的Action值


            if (quertStringAction == "GetUserInfo")
            {             
                string jsonString = GetUserInfo(quertStringUserSid);

                if(jsonString != null)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonString);
                }
                return;
            }

            //---------------------


            //-----Method:POST-----
            string formDataAction = context.Request.Form["Action"];//取得FormData的Action值

            //判斷formDataAction
            //檢查帳號是否重複 : "CheckAccountRepetition"
            //新增帳號 : "Insert"
            //修改帳號 : "Update"
            if (formDataAction == "CheckAccountRepetition")
            {
                string formDataAccount = context.Request.Form["Account"];//取得FormData的Account值

                UserDetailDBMethod method = new UserDetailDBMethod();

                //CheckAccountRepetition回傳true則無帳號重複，false則帳號重複
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
                UserListModel model = GetFormDateUserInfo("Insert", context);//取得FormData的值

                //新增使用者資訊並回傳成功字串
                method.InsertUser(model);
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            else if(formDataAction == "Update")
            {
                UserDetailDBMethod method = new UserDetailDBMethod();
                UserListModel model = GetFormDateUserInfo("Update", context);//取得FormData的值

                //修改使用者資訊並回傳成功字串
                method.UpdateUser(model);
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

        /// <summary>以UserSid為參數，取得使用者資訊</summary>
        private string GetUserInfo(string UserSid)
        {
            //取得帳號資訊
            UserDetailDBMethod method = new UserDetailDBMethod();
            DataTable dt = method.GetUserInfo(UserSid);

            //dt長度為0則回傳空值
            if (dt.Rows.Count == 0)
                return null;

            //用model承接dt的資料
            UserListModel model = new UserListModel();

            model.UserSid = (int)dt.Rows[0]["User_Sid"];
            model.Account = dt.Rows[0]["Account"].ToString();
            model.Password = dt.Rows[0]["Password"].ToString();
            model.Email = dt.Rows[0]["Email"].ToString();
            model.User_Name = dt.Rows[0]["User_Name"].ToString();
            model.Account_Level = (int)dt.Rows[0]["Account_Level"];
            model.Create_Time = dt.Rows[0]["Create_Time"].ToString();
            model.Modify_Time = dt.Rows[0]["Modify_Time"].ToString();

            //將model轉成JSON字串並回傳
            string jsonString = JsonConvert.SerializeObject(model);
            return jsonString;
        }

        /// <summary>取得FormData的值，並用UserListModel承接後回傳</summary>
        private UserListModel GetFormDateUserInfo(string action, HttpContext context)
        {
            //使用UserListModel來承接FormData的值
            UserListModel model = new UserListModel();

            model.User_Name = context.Request.Form["UserName"];
            model.Email = context.Request.Form["Email"];
            model.Account_Level = Convert.ToInt32(context.Request.Form["AccountLevel"]);
            model.Account = context.Request.Form["Account"];

            //檢查action
            //新增使用者 : "Insert"
            //修改使用者 : "Update"
            if (action == "Insert")
            {               
                model.Create_Time = DateTime.Now.ToString();//將建立時間設為當下
                model.Modify_Time = DateTime.Now.ToString();//將修改時間設為當下
            }
            else if(action == "Update")
            {            
                model.UserSid = Convert.ToInt32(context.Request.Form["UserSid"]);//取得FormData的UserSid值
                model.Modify_Time = DateTime.Now.ToString();//將修改時間設為當下
            }

            //回傳model
            return model;
        }
    }
}