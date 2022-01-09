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
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                AccountingListDBMethod dBMethod = new AccountingListDBMethod();
                LoginInfoModel sessionInfo = LoginHelper.GetCurrentUserInfo();
                DataTable dt = dBMethod.GetAccountingList(sessionInfo.UserSid);
                this.AccountingListRepeater.DataSource = dt;
                this.AccountingListRepeater.DataBind();


                if (dt.Rows.Count > 0) 
                {
                    int subTotal = 0;
                    int thisYear = DateTime.Now.Year;
                    int thisMonth = DateTime.Now.Month;

                    foreach (DataRow model in dt.Rows)
                    {
                        string accountYear = model["Create_Time"].ToString().Split('/')[0];
                        string accountMonth = model["Create_Time"].ToString().Split('/')[1];

                        if (Convert.ToInt32(accountYear) == thisYear && Convert.ToInt32(accountMonth) == thisMonth)
                        {

                            if (model["Price_Type"].ToString() == "0") 
                            {
                                subTotal += (int)model["Price"];
                            }
                            else
                            {
                                subTotal -= (int)model["Price"];
                            }
                            
                        }
                    }

                    this.subTotal.Text = "小計" + subTotal.ToString() + "元";
                }
                
            }
        }
    }
}