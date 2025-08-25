<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dynamic_portfolio.Login" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section id="login" style="max-width:420px;margin:60px auto;">
            <h1 class="title">Admin Login</h1>
            <div class="about-containers">
                <div class="details-container" style="width:100%;">
                    <div class="btn-container" style="display:block;">
                        <asp:TextBox ID="txtUser" runat="server" CssClass="input" placeholder="Username" />
                        <asp:RequiredFieldValidator ID="rfvUser" runat="server"
                            ControlToValidate="txtUser" ErrorMessage="Username is required."
                            Display="Dynamic" CssClass="validator" />

                        <asp:TextBox ID="txtPass" runat="server" CssClass="input" TextMode="Password" placeholder="Password" />
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server"
                            ControlToValidate="txtPass" ErrorMessage="Password is required."
                            Display="Dynamic" CssClass="validator" />

                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-color-1"
                            OnClick="btnLogin_Click" />
                        <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>