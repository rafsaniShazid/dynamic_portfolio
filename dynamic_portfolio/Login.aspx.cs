using System;
using System.Web.UI;

namespace dynamic_portfolio
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text?.Trim();
            string password = txtPass.Text;

            // Demo only — in production, use a DB + salted hashing and proper auth.
            if (username == "admin" && password == "12345")
            {
                Session["Admin"] = true;
                Response.Redirect("Admin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
                lblMessage.CssClass = "error";
            }
        }
    }
}