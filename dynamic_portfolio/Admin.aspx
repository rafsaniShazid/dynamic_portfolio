<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="dynamic_portfolio.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin</title>
    <link href="Content/style.css" rel="stylesheet" />
    <style>
        .admin-wrap { max-width: 1040px; margin: 40px auto; }
        .grid { width: 100%; border-collapse: collapse; }
        .grid th, .grid td { padding: 8px 10px; border-bottom: 1px solid #e5e7eb; vertical-align: top; }
        .grid th { text-align: left; background: #f3f4f6; }
        .grid .input { width: 100%; box-sizing: border-box; padding: 8px 10px; }
        .admin-actions { display: flex; gap: 10px; margin: 14px 0; }
        .admin-card { background: #fff; border-radius: 10px; box-shadow: 0 6px 16px rgba(0,0,0,.06); padding: 16px; margin-top: 16px; }
        .admin-card h3 { margin: 0 0 10px; }
        .admin-card .row { display: grid; grid-template-columns: repeat(2, 1fr); gap: 12px; }
        .admin-card .row .col-2 { grid-column: 1 / -1; }
        .btn { display:inline-block; padding: 10px 14px; border-radius: 8px; cursor:pointer; text-decoration:none; border: none; }
        .btn.btn-color-1 { background:#4f46e5; color:#fff; }
        .btn.btn-color-2 { background:#e5e7eb; color:#111827; }
        .file-upload { padding: 8px 10px; border: 1px solid #d1d5db; border-radius: 6px; }
        .profile-section { margin-top: 32px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="admin-wrap">
            <h1 class="title">Admin Dashboard</h1>
            <asp:Label ID="lblStatus" runat="server" />
            <div class="admin-actions">
                <asp:Button ID="btnLogout" runat="server" Text="Log out" CssClass="btn btn-color-1" OnClick="btnLogout_Click" />
                <a class="btn btn-color-2" href="Default.aspx">Back to site</a>
            </div>

            <!-- Profile & About Section -->
            <div class="profile-section">
                <h2 class="title" style="font-size:26px;">Profile & About</h2>
                <div class="admin-card">
                    <h3>Update Profile Information</h3>
                    <div class="row">
                        <div><asp:TextBox ID="txtProfileName" runat="server" CssClass="input" placeholder="Your Name" /></div>
                        <div><asp:TextBox ID="txtProfileRole" runat="server" CssClass="input" placeholder="Your Role (e.g., Android Developer)" /></div>
                        <div><asp:TextBox ID="txtProfileEmail" runat="server" CssClass="input" placeholder="Email Address" /></div>
                        <div><asp:TextBox ID="txtLinkedInUrl" runat="server" CssClass="input" placeholder="LinkedIn URL" /></div>
                        <div><asp:TextBox ID="txtGithubUrl" runat="server" CssClass="input" placeholder="GitHub URL" /></div>
                        <div class="col-2">
                            <label>Profile Picture:</label><br />
                            <asp:FileUpload ID="fuProfileImage" runat="server" CssClass="file-upload" accept="image/*" />
                        </div>
                        <div class="col-2">
                            <label>About Page Picture:</label><br />
                            <asp:FileUpload ID="fuAboutImage" runat="server" CssClass="file-upload" accept="image/*" />
                        </div>
                        <div class="col-2">
                            <label>Resume/CV File:</label><br />
                            <asp:FileUpload ID="fuResume" runat="server" CssClass="file-upload" accept=".pdf,.doc,.docx" />
                        </div>
                        <div class="col-2">
                            <label>Experience Years:</label><br />
                            <asp:TextBox ID="txtExperience" runat="server" CssClass="input" TextMode="MultiLine" Rows="3" placeholder="e.g., 2+ years in Development&#10;1+ year in SEO" />
                        </div>
                        <div class="col-2">
                            <label>Education:</label><br />
                            <asp:TextBox ID="txtEducation" runat="server" CssClass="input" placeholder="Your Education Background" />
                        </div>
                        <div class="col-2">
                            <label>About Description:</label><br />
                            <asp:TextBox ID="txtAboutDescription" runat="server" CssClass="input" TextMode="MultiLine" Rows="5" placeholder="Write about yourself..." />
                        </div>
                    </div>
                    <div class="admin-actions">
                        <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-color-1" OnClick="btnUpdateProfile_Click" />
                    </div>
                </div>
            </div>

            <h2 class="title" style="font-size:26px;margin-top:20px;">Projects</h2>
            <asp:GridView ID="gvProjects" runat="server" AutoGenerateColumns="False" CssClass="grid"
                DataKeyNames="Id"
                OnRowEditing="gvProjects_RowEditing"
                OnRowCancelingEdit="gvProjects_RowCancelingEdit"
                OnRowUpdating="gvProjects_RowUpdating"
                OnRowDeleting="gvProjects_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate><%# Eval("Title") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtTitle" runat="server" CssClass="input" Text='<%# Bind("Title") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <img src="<%# ResolveUrl((string)Eval("ImagePath")) %>" alt="" style="width:60px;height:40px;object-fit:cover;" />
                            <br /><small><%# Eval("ImagePath") %></small>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="fuEditImage" runat="server" CssClass="file-upload" />
                            <br /><small>Current: <%# Eval("ImagePath") %></small>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ImageCss">
                        <ItemTemplate><%# Eval("ImageCss") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtImageCss" runat="server" CssClass="input" Text='<%# Bind("ImageCss") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alt">
                        <ItemTemplate><%# Eval("Alt") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtAlt" runat="server" CssClass="input" Text='<%# Bind("Alt") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GithubUrl">
                        <ItemTemplate><a href="<%# Eval("GithubUrl") %>" target="_blank">Link</a></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtGithubUrl" runat="server" CssClass="input" Text='<%# Bind("GithubUrl") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LiveUrl">
                        <ItemTemplate><%# string.IsNullOrEmpty((string)Eval("LiveUrl")) ? "-" : "<a target=\"_blank\" href=\"" + Eval("LiveUrl") + "\">Link</a>" %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtLiveUrl" runat="server" CssClass="input" Text='<%# Bind("LiveUrl") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate><%# Eval("Description") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtDescription" runat="server" CssClass="input" TextMode="MultiLine" Rows="3" Text='<%# Bind("Description") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>

            <div class="admin-card">
                <h3>Add new project</h3>
                <div class="row">
                    <div><asp:TextBox ID="txtAddTitle" runat="server" CssClass="input" placeholder="Title" /></div>
                    <div><asp:TextBox ID="txtAddImageCss" runat="server" CssClass="input" placeholder="Image CSS (e.g., 'project-img')" /></div>
                    <div class="col-2">
                        <label>Select Image:</label><br />
                        <asp:FileUpload ID="fuAddImage" runat="server" CssClass="file-upload" accept="image/*" />
                    </div>
                    <div><asp:TextBox ID="txtAddAlt" runat="server" CssClass="input" placeholder="Alt text" /></div>
                    <div><asp:TextBox ID="txtAddGithubUrl" runat="server" CssClass="input" placeholder="Github URL" /></div>
                    <div class="col-2"><asp:TextBox ID="txtAddLiveUrl" runat="server" CssClass="input" placeholder="Live URL (optional)" /></div>
                    <div class="col-2"><asp:TextBox ID="txtAddDescription" runat="server" CssClass="input" TextMode="MultiLine" Rows="3" placeholder="Description (optional)" /></div>
                </div>
                <div class="admin-actions">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Project" CssClass="btn btn-color-1" OnClick="btnAdd_Click" />
                </div>
            </div>
        </section>
    </form>
</body>
</html>