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
    /// AccountingDetailHandler 的摘要描述
    /// </summary>
    public class AccountingDetailHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string quertStringAction = context.Request.QueryString["Action"];
            string quertStringAccountSid = context.Request.QueryString["AccountSid"];
            int currentUserSid = LoginHelper.GetCurrentUserInfo().UserSid;

            AccountingDetailDBMethod method = new AccountingDetailDBMethod();
            AccountingListModel model = new AccountingListModel();

            if (quertStringAction == "GetAccounting")
            {
                DataTable dt = method.GetAccounting(currentUserSid, quertStringAccountSid);

                model.CategoryList_CategorySid = (int)dt.Rows[0]["CategoryList_CategorySid"];
                model.Price_Type = (int)dt.Rows[0]["Price_Type"];
                model.Price = (int)dt.Rows[0]["Price"];
                model.Tittle = dt.Rows[0]["Tittle"].ToString();
                model.Description = dt.Rows[0]["Description"].ToString();

                string jsonString = JsonConvert.SerializeObject(model);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonString);
                return;

            }


            string formDataAction = context.Request.Form["Action"];

            string formDataCategorySid = context.Request.Form["CategorySid"];
            string formDataPriceType = context.Request.Form["PriceType"];
            string formDataPrice = context.Request.Form["Price"];
            string formDataTittle = context.Request.Form["Tittle"];
            string formDataDescription = context.Request.Form["Description"];

            model.CategoryList_CategorySid = Convert.ToInt32(formDataCategorySid);
            model.Price_Type = Convert.ToInt32(formDataPriceType);
            model.Price = Convert.ToInt32(formDataPrice);
            model.Tittle = formDataTittle;
            model.Description = formDataDescription;
            model.UserList_UserSid = currentUserSid;

            if (formDataAction == "InsertAccounting")
            {
                method.InsterAccounting(model);

                string sid = method.GetInsertAccountingSid(currentUserSid);

                context.Response.ContentType = "text/plain";
                context.Response.Write(sid);
            }
            else if(formDataAction == "UpdateAccounting")
            {
                string formDataAccountSid = context.Request.Form["AccountSid"];
                model.Account_Sid = Convert.ToInt32(formDataAccountSid);

                method.UpdateAccounting(model);

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