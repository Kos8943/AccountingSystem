using AccountingSystem.DBConnect;
using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingSystem.Pages
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int userSid = LoginHelper.GetCurrentUserInfo().UserSid;

                AccountingDetailDBMethod method = new AccountingDetailDBMethod();

                DataTable dt = method.GetCategoryList(userSid);

                this.CategoryDropDownList.DataSource = dt;
                this.CategoryDropDownList.DataTextField = "Category_Name";
                this.CategoryDropDownList.DataValueField = "Category_Sid";
                this.CategoryDropDownList.DataBind();
            }
        }
    }
}