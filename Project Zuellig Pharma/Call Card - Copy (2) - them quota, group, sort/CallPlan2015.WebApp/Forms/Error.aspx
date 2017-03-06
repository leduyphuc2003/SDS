<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CallPlan2015.WebApp.Forms.Error" %>
<%@ Import Namespace="CallPlan2015.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="color: red">
        <%= Session[Constants.SESSION_ERROR] %>
    </div>
</asp:Content>
