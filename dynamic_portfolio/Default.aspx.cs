using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

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
            var projects = new List<Project>
            {
                new Project
                {
                    Title = "MP3 Player",
                    ImagePath = "~/assets/project-1.png",
                    ImageCss = "project-img project-img1",
                    Alt = "project-1",
                    GithubUrl = "https://github.com/rafsaniShazid/MP3_player",
                    LiveUrl = "https://drive.google.com/file/d/1jpmlTABruHj-Je9OEF88NuiQo1Ffq8Es/view?usp=drivesdk"
                },
                new Project
                {
                    Title = "Portfolio Website",
                    ImagePath = "~/assets/project-2.png",
                    ImageCss = "project-img",
                    Alt = "project-2",
                    GithubUrl = "https://github.com/rafsaniShazid/MP3_player",
                    Description = "This website is the project itself"
                },
                new Project
                {
                    Title = "Snake Game",
                    ImagePath = "~/assets/project-3.png",
                    ImageCss = "project-img project-img3",
                    Alt = "project-3",
                    GithubUrl = "https://github.com/rafsaniShazid/Snake_game.",
                    LiveUrl = "https://drive.google.com/file/d/1k1AM-JSObbuRZOK_7zHOOE0GQHrapf0p/view?usp=drivesdk"
                }
            };

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
                if (!Directory.Exists(appData))
                {
                    Directory.CreateDirectory(appData);
                }
                var file = Path.Combine(appData, "messages.txt");
                File.AppendAllText(file, entry + "----" + Environment.NewLine);

                lblResult.Text = "Thanks! Your message has been sent.";
                lblResult.CssClass = "success";
                txtName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtMessage.Text = string.Empty;
            }
            catch
            {
                lblResult.Text = "Sorry, something went wrong. Please try again later.";
                lblResult.CssClass = "error";
            }
        }

        private sealed class Project
        {
            public string Title { get; set; }
            public string ImagePath { get; set; }
            public string ImageCss { get; set; }
            public string Alt { get; set; }
            public string GithubUrl { get; set; }
            public string LiveUrl { get; set; }
            public string Description { get; set; }
        }
    }
}