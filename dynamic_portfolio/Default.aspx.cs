using System;
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
                BindProfile();
            }
        }

        private void BindProjects()
        {
            var projects = ProjectRepository.GetAll();
            rptProjects.DataSource = projects;
            rptProjects.DataBind();
        }

        private void BindProfile()
        {
            var profile = ProfileRepository.GetProfile();

            // Navigation
            litNavName.Text = profile.Name;
            litMobileNavName.Text = profile.Name;

            // Profile section
            imgProfile.ImageUrl = ResolveUrl(profile.ProfileImagePath);
            litProfileName.Text = profile.Name;
            litRole.Text = profile.Role;
            lnkResume.NavigateUrl = ResolveUrl(profile.ResumePath);
            lnkLinkedIn.NavigateUrl = profile.LinkedInUrl;
            lnkGithub.NavigateUrl = profile.GithubUrl;

            // About section
            imgAbout.ImageUrl = ResolveUrl(profile.AboutImagePath);
            litExperience.Text = profile.ExperienceYears?.Replace("\n", "<br />") ?? "";
            litEducation.Text = profile.Education;
            litAboutDescription.Text = profile.AboutDescription?.Replace("\n", "<br />") ?? "";

            // Contact section
            lnkEmail.NavigateUrl = "mailto:" + profile.Email;
            lnkEmail.Text = profile.Email;
            lnkContactLinkedIn.NavigateUrl = profile.LinkedInUrl;

            // Footer
            litFooterName.Text = profile.Name;
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
                if (!Directory.Exists(appData)) Directory.CreateDirectory(appData);
                var file = Path.Combine(appData, "messages.txt");
                File.AppendAllText(file, entry + "----" + Environment.NewLine);

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