using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using dynamic_portfolio.Services;

namespace dynamic_portfolio
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
            }
        }

        private void BindProjects()
        {
            var projects = ProjectRepository.GetAll();
            rptProjects.DataSource = projects;
            rptProjects.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var name = Request.Form[txtName.UniqueID]?.Trim() ?? "";
            var email = Request.Form[txtEmail.UniqueID]?.Trim() ?? "";
            var message = Request.Form[txtMessage.UniqueID]?.Trim() ?? "";

            var entry = string.Format(
                "[{0:u}] From: {1} <{2}>{3}{4}{3}",
                DateTime.UtcNow, name, email, Environment.NewLine, message);

            try
            {
                var appData = Server.MapPath("~/App_Data");
                if (!Directory.Exists(appData)) System.IO.Directory.CreateDirectory(appData);
                var file = System.IO.Path.Combine(appData, "messages.txt");
                System.IO.File.AppendAllText(file, entry + "----" + Environment.NewLine);

                lblResult.Text = "Thanks! Your message has been sent.";
                lblResult.CssClass = "success";
                txtName.Text = txtEmail.Text = txtMessage.Text = string.Empty;
            }
            catch
            {
                lblResult.Text = "Sorry, something went wrong. Please try again later.";
                lblResult.CssClass = "error";
            }
        }
    }
}