using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingSystem.Pages
{
    public partial class LoginOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginHelper.Logout();
            Response.Redirect("Login.aspx");
        }
    }
}