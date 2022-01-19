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
            string quertStringAction = context.Request.QueryString["Action"];//取得QueryString的Action值
            string quertStringAccountSid = context.Request.QueryString["AccountSid"];//取得QueryString的AccountSid值
            int currentUserSid = LoginHelper.GetCurrentUserInfo().UserSid;//取得當下使用者的UserSid

            
            AccountingDetailDBMethod method = new AccountingDetailDBMethod();//建立AccountingDetailDBMethod實例
            AccountingListModel model = new AccountingListModel();//建立AccountingListModel實例


            //-----Method:GET-----
            //如果quertStringAction為"GetAccounting"
            if (quertStringAction == "GetAccounting")
            {
                //以currentUserSid、quertStringAccountSid，使用GetAccounting取得單筆記帳紀錄
                DataTable dt = method.GetAccounting(currentUserSid, quertStringAccountSid);

                //將dt的資料用model來承接
                model.CategoryList_CategorySid = (int)dt.Rows[0]["CategoryList_CategorySid"];
                model.Price_Type = (int)dt.Rows[0]["Price_Type"];
                model.Price = (int)dt.Rows[0]["Price"];
                model.Tittle = dt.Rows[0]["Tittle"].ToString();
                model.Description = dt.Rows[0]["Description"].ToString();

                //將model轉成JSON字串
                string jsonString = JsonConvert.SerializeObject(model);

                //回傳jsonString
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonString);
                return;

            }

            //---------------------


            //-----Method:POST-----

            string formDataAction = context.Request.Form["Action"];//取得FormData的Action值
            string formDataCategorySid = context.Request.Form["CategorySid"];//取得FormData的CategorySid值
            string formDataPriceType = context.Request.Form["PriceType"];//取得FormData的PriceType值
            string formDataPrice = context.Request.Form["Price"];//取得FormData的Price值
            string formDataTittle = context.Request.Form["Tittle"];//取得FormData的Tittle值
            string formDataDescription = context.Request.Form["Description"];//取得FormData的Description值


            //將FormData的值，用model承接
            model.CategoryList_CategorySid = Convert.ToInt32(formDataCategorySid);
            model.Price_Type = Convert.ToInt32(formDataPriceType);
            model.Price = Convert.ToInt32(formDataPrice);
            model.Tittle = formDataTittle;
            model.Description = formDataDescription;
            model.UserList_UserSid = currentUserSid;

            //檢查formDataAction
            if (formDataAction == "InsertAccounting")
            {
                //新增記帳紀錄至資料庫
                method.InsterAccounting(model);

                //取得新增的記帳紀錄的AccountingSid
                string sid = method.GetInsertAccountingSid(currentUserSid);

                //回傳新增的記帳紀錄的AccountingSid
                context.Response.ContentType = "text/plain";
                context.Response.Write(sid);
            }
            else if(formDataAction == "UpdateAccounting")
            {

                string formDataAccountSid = context.Request.Form["AccountSid"];//取得FormData的AccountSid值

                //將formDataAccountSid加進model.Account_Sid
                model.Account_Sid = Convert.ToInt32(formDataAccountSid);

                //更新記帳紀錄
                method.UpdateAccounting(model);

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