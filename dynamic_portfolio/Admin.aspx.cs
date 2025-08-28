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
            }
        }

        private void BindGrid()
        {
            gvProjects.DataSource = ProjectRepository.GetAll();
            gvProjects.DataBind();
        }

        private string SaveUploadedFile(FileUpload fileUpload)
        {
            if (fileUpload == null || !fileUpload.HasFile) return null;

            try
            {
                // Create unique filename
                var extension = Path.GetExtension(fileUpload.FileName);
                var fileName = $"project-{DateTime.Now:yyyyMMddHHmmss}{extension}";
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

            // Handle image upload for edit
            var fuEditImage = (FileUpload)row.FindControl("fuEditImage");
            var newImagePath = SaveUploadedFile(fuEditImage);

            var project = new Project
            {
                Id = id,
                Title = ((TextBox)row.FindControl("txtTitle"))?.Text?.Trim(),
                ImagePath = newImagePath ?? existingProject.ImagePath, // Keep existing if no new image
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
            var imagePath = SaveUploadedFile(fuAddImage);

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

                    // Clear form
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