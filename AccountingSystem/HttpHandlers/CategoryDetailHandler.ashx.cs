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
            string quertStringAction = context.Request.QueryString["Action"];//取得QueryString的Action值
            string quertStringCategorySid = context.Request.QueryString["CategorySid"];//取得QueryString的CategorySid值
            string sessionUserSid = LoginHelper.GetCurrentUserInfo().UserSid.ToString();//取得當下使用者的UserSid

            CategoryDetailDBMethod method = new CategoryDetailDBMethod();//建立CategoryDetailDBMethod實例
            CategoryModel model = new CategoryModel();//建立CategoryModel實例


            //-----Method:GET-----
            //quertStringAction為"GetCategory"
            if (quertStringAction == "GetCategory")
            {
                //從資料庫讀取單筆記帳分類
                DataTable dt = method.GetCategory(sessionUserSid, quertStringCategorySid);

                //dt的長度大於0
                if (dt.Rows.Count > 0)
                {
                    //將dt的資料用model來承接
                    model.Category_Name = dt.Rows[0]["Category_Name"].ToString();
                    model.Description = dt.Rows[0]["Description"].ToString();

                    //將model轉換成JSON字串
                    string jsonString = JsonConvert.SerializeObject(model);

                    //回傳jsonString
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonString);
                    return;
                }
                
            }
            //---------------------

            //-----Method:POST-----
            string formDataAction = context.Request.Form["Action"];//取得FormData的Action值
            string formDataCategoryName = context.Request.Form["CategoryName"];//取得FormData的CategoryName值
            string formDataDescription = context.Request.Form["Description"];//取得FormData的Description值


            //檢查formDataAction值
            //檢查分類是否重複 : CheckCategoryRepetition
            //新增分類 : InsertCategory
            //修改分類 : UpdateCategory
            if (formDataAction == "CheckCategoryRepetition")
            {
                //以sessionUserSid、formDataCategoryName為參數，查詢資料庫
                DataTable checkCategoryDataTable = method.CheckCategoryRepetition(sessionUserSid, formDataCategoryName);

                //checkCategoryDataTable的長度為0，回傳成功字串，否則回傳空字串
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
                //以sessionUserSid、formDataCategoryName、formDataDescription為參數，新增分類至資料庫
                method.InsertCategory(sessionUserSid, formDataCategoryName, formDataDescription);

                //取得新增分類的Sid
                DataTable checkCategoryDataTable = method.CheckCategoryRepetition(sessionUserSid, formDataCategoryName);
                string newCategorySid = checkCategoryDataTable.Rows[0]["Category_Sid"].ToString();

                //回傳新增分類的Sid
                context.Response.ContentType = "text/plain";
                context.Response.Write(newCategorySid);
            }
            else if(formDataAction == "UpdateCategory")
            {

                string formDataCategorySid = context.Request.Form["CategorySid"];//取得FormData的CategorySid值

                //以sessionUserSid、formDataCategoryName、formDataDescription、formDataCategorySid為參數，更新分類至資料庫
                method.UpdateCategory(formDataCategoryName, formDataDescription, formDataCategorySid, sessionUserSid);

                //回傳成功字串
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