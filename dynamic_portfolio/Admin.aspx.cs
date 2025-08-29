using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using dynamic_portfolio.Models;
using dynamic_portfolio.Services;

namespace dynamic_portfolio
{
    public partial class Admin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null || !(Session["Admin"] is bool ok && ok))
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                lblStatus.Text = "You are logged in as admin.";
                BindGrid();
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            var profile = ProfileRepository.GetProfile();
            txtProfileName.Text = profile.Name;
            txtProfileRole.Text = profile.Role;
            txtProfileEmail.Text = profile.Email;
            txtLinkedInUrl.Text = profile.LinkedInUrl;
            txtGithubUrl.Text = profile.GithubUrl;
            txtExperience.Text = profile.ExperienceYears;
            txtEducation.Text = profile.Education;
            txtAboutDescription.Text = profile.AboutDescription;
        }

        private void BindGrid()
        {
            gvProjects.DataSource = ProjectRepository.GetAll();
            gvProjects.DataBind();
        }

        private string SaveUploadedFile(FileUpload fileUpload, string prefix = "file")
        {
            if (fileUpload == null || !fileUpload.HasFile) return null;

            try
            {
                var extension = Path.GetExtension(fileUpload.FileName);
                var fileName = $"{prefix}-{DateTime.Now:yyyyMMddHHmmss}{extension}";
                var assetsPath = Server.MapPath("~/assets/");
                
                if (!Directory.Exists(assetsPath))
                {
                    Directory.CreateDirectory(assetsPath);
                }

                var filePath = Path.Combine(assetsPath, fileName);
                fileUpload.SaveAs(filePath);

                return "~/assets/" + fileName;
            }
            catch
            {
                return null;
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            var currentProfile = ProfileRepository.GetProfile();
            
            // Handle file uploads
            var profileImagePath = SaveUploadedFile(fuProfileImage, "profile") ?? currentProfile.ProfileImagePath;
            var aboutImagePath = SaveUploadedFile(fuAboutImage, "about") ?? currentProfile.AboutImagePath;
            var resumePath = SaveUploadedFile(fuResume, "resume") ?? currentProfile.ResumePath;

            var profile = new Profile
            {
                Id = currentProfile.Id,
                Name = txtProfileName.Text?.Trim(),
                Role = txtProfileRole.Text?.Trim(),
                ProfileImagePath = profileImagePath,
                AboutImagePath = aboutImagePath,
                ResumePath = resumePath,
                AboutDescription = txtAboutDescription.Text?.Trim(),
                ExperienceYears = txtExperience.Text?.Trim(),
                Education = txtEducation.Text?.Trim(),
                LinkedInUrl = txtLinkedInUrl.Text?.Trim(),
                GithubUrl = txtGithubUrl.Text?.Trim(),
                Email = txtProfileEmail.Text?.Trim()
            };

            ProfileRepository.UpdateProfile(profile);
            lblStatus.Text = "Profile updated successfully!";
            lblStatus.CssClass = "success";
        }

        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProjects.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProjects.EditIndex = -1;
            BindGrid();
        }

        protected void gvProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var id = (int)gvProjects.DataKeys[e.RowIndex].Value;
            var row = gvProjects.Rows[e.RowIndex];
            var existingProject = ProjectRepository.GetById(id);

            if (existingProject == null) return;

            var fuEditImage = (FileUpload)row.FindControl("fuEditImage");
            var newImagePath = SaveUploadedFile(fuEditImage, "project") ?? existingProject.ImagePath;

            var project = new Project
            {
                Id = id,
                Title = ((TextBox)row.FindControl("txtTitle"))?.Text?.Trim(),
                ImagePath = newImagePath,
                ImageCss = ((TextBox)row.FindControl("txtImageCss"))?.Text?.Trim(),
                Alt = ((TextBox)row.FindControl("txtAlt"))?.Text?.Trim(),
                GithubUrl = ((TextBox)row.FindControl("txtGithubUrl"))?.Text?.Trim(),
                LiveUrl = ((TextBox)row.FindControl("txtLiveUrl"))?.Text?.Trim(),
                Description = ((TextBox)row.FindControl("txtDescription"))?.Text?.Trim(),
            };

            ProjectRepository.Update(project);

            gvProjects.EditIndex = -1;
            BindGrid();
            lblStatus.Text = "Project updated successfully!";
        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = (int)gvProjects.DataKeys[e.RowIndex].Value;
            ProjectRepository.Delete(id);
            BindGrid();
            lblStatus.Text = "Project deleted successfully!";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var imagePath = SaveUploadedFile(fuAddImage, "project");

            var project = new Project
            {
                Title = txtAddTitle.Text?.Trim(),
                ImagePath = imagePath,
                ImageCss = txtAddImageCss.Text?.Trim(),
                Alt = txtAddAlt.Text?.Trim(),
                GithubUrl = txtAddGithubUrl.Text?.Trim(),
                LiveUrl = txtAddLiveUrl.Text?.Trim(),
                Description = txtAddDescription.Text?.Trim()
            };

            if (!string.IsNullOrWhiteSpace(project.Title) && !string.IsNullOrWhiteSpace(project.ImagePath))
            {
                ProjectRepository.Add(project);

                txtAddTitle.Text = txtAddImageCss.Text = txtAddAlt.Text = 
                    txtAddGithubUrl.Text = txtAddLiveUrl.Text = txtAddDescription.Text = string.Empty;

                BindGrid();
                lblStatus.Text = "Project added successfully!";
            }
            else
            {
                lblStatus.Text = "Title and Image are required.";
                lblStatus.CssClass = "error";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}