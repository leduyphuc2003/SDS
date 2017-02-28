<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CallPlan2015.WebApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <%--<form id="form1" runat="server">
        <br/>
        <br/>
        <div class="container">
            <div class="row">
                <div class="col-md-1">UserName:</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" />
                </div>
            </div>
            <br/>
            <div class="row">
                <div class="col-md-1">Password:</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password"/>
                </div>
            </div>
            <br/>
            <div style="color:red" runat="server" id="errorMessage" Visible="False">Incorrect user name or password! Please try again!</div>
            <div class="row">
                <div class="col-md-4" align="right">
                    <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-primary" OnClick="btnLogin_OnClick" Text=" Login "/>
                </div>
            </div>
        </div>
    </form>--%>
    <div class="container">

        <form class="form-signin" runat="server">
            <h2 class="form-signin-heading">Please sign in</h2>
            <label for="inputEmail" class="sr-only">Email address</label>
            <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User name" runat="server" />
            <label for="inputPassword" class="sr-only">Password</label>
            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" runat="server" TextMode="Password" />
            <div style="color: red" runat="server" id="errorMessage" visible="False">Incorrect user name or password! Please try again!</div>
            <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnLogin_OnClick" Text=" Login "/>
        </form>

    </div>
    <!-- /container -->
</body>
</html>
