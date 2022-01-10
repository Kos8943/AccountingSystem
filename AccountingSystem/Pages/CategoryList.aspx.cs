using AccountingSystem.DBConnect;
using AccountingSystem.Helpers;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingSystem.Pages
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CategoryDBMethod method = new CategoryDBMethod();
                LoginInfoModel sessionInfo = LoginHelper.GetCurrentUserInfo();

                DataTable dt = method.GetCategoryList(sessionInfo.UserSid);
                this.CategoryRepeater.DataSource = dt;
                this.CategoryRepeater.DataBind();


            }
        }
    }
}