using AccountingSystem.DBConnect;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSystem.Helpers
{
    public class LoginHelper
    {
        private const string _sessionKey = "IsLogined";

        /// <summary> 檢查是否有登入 </summary>
        /// <returns></returns>
        public static bool HasLogined()
        {
            var val = HttpContext.Current.Session[_sessionKey] as LoginInfoModel;

            if (val != null)
                return true;
            else
                return false;
        }





        /// <summary> 嘗試登入 </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>

        public static bool TryLogin(string account, string pwd)
        {
            if (LoginHelper.HasLogined())
                return true;

            // READ DB And check account / pwd
            AccountManager manager = new AccountManager();
            var model = manager.GetAccount(account);

            if (model == null ||
                string.Compare(pwd, model.Password, false) != 0)
                return false;


            // Keep status
            HttpContext.Current.Session[_sessionKey] = new LoginInfoModel()
            {
                UserSid = model.UserSid,
                User_Name = model.User_Name,
                Account_Level = model.Account_Level
            };

            return true;
        }



        /// <summary> 登出目前使用者，如果還沒登入就不執行 </summary>
        public static void Logout()
        {
            if (!LoginHelper.HasLogined())
                return;

            HttpContext.Current.Session.Remove(_sessionKey);
        }



        /// <summary> 取得已登入者的資訊，如果還沒登入回傳 NULL </summary>
        /// <returns></returns>
        /// 
        public static LoginInfoModel GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogined())
                return null;

            return HttpContext.Current.Session[_sessionKey] as LoginInfoModel;
        }
    }
}