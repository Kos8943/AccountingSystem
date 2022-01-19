using AccountingSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingSystem
{
    public partial class NavbarAndSidebar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!LoginHelper.HasLogined())
            {
                Response.Redirect("Login.aspx");
            }
            
            if(LoginHelper.GetCurrentUserInfo().UserSid != 1)
            {
                this.adminOnly.Visible = false;
            }
            
        }
    }
}