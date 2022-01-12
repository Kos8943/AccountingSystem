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
    /// CategoryDetailHandler 的摘要描述
    /// </summary>
    public class CategoryDetailHandler : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string quertStringAction = context.Request.QueryString["Action"];
            string quertStringCategorySid = context.Request.QueryString["CategorySid"];
            

            string sessionUserSid = LoginHelper.GetCurrentUserInfo().UserSid.ToString();

            CategoryDetailDBMethod method = new CategoryDetailDBMethod();
            CategoryModel model = new CategoryModel();

            if (quertStringAction == "GetCategory")
            {
                DataTable dt = method.GetCategory(sessionUserSid, quertStringCategorySid);

                if (dt.Rows.Count > 0)
                {
                    model.Category_Name = dt.Rows[0]["Category_Name"].ToString();
                    model.Description = dt.Rows[0]["Description"].ToString();

                    string jsonString = JsonConvert.SerializeObject(model);

                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonString);
                    return;
                }
                
            }

            string formDataAction = context.Request.Form["Action"];
            string formDataCategoryName = context.Request.Form["CategoryName"];
            string formDataDescription = context.Request.Form["Description"];

            

            if (formDataAction == "CheckCategoryRepetition")
            {
                DataTable checkCategoryDataTable = method.CheckCategoryRepetition(sessionUserSid, formDataCategoryName);
                if (checkCategoryDataTable.Rows.Count == 0)
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
            else if(formDataAction == "InsertCategory")
            {
                
                context.Response.ContentType = "text/plain";
                method.InsertCategory(sessionUserSid, formDataCategoryName, formDataDescription);

                DataTable checkCategoryDataTable = method.CheckCategoryRepetition(sessionUserSid, formDataCategoryName);
                string newCategorySid = checkCategoryDataTable.Rows[0]["Category_Sid"].ToString();

                context.Response.Write(newCategorySid);
            }
            else if(formDataAction == "UpdateCategory")
            {
                string formDataCategorySid = context.Request.Form["CategorySid"];
                method.UpdateCategory(formDataCategoryName, formDataDescription, formDataCategorySid, sessionUserSid);

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