<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GovHosDateChoose.aspx.cs" Inherits="CallPlan2015.WebApp.GovHosDateChoose" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <html>
   <head>
      <meta charset="utf-8">
      <title>jQuery UI Datepicker functionality</title>
       
<%--       <link href="~/Styles/jquery-ui.css" rel="stylesheet">
       <script src="/Styles/jquery-1.10.2.js"></script>
       <script src="/Styles/jquery-ui.js"></script>--%>

      <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
      <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
      <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
       

      <!-- Javascript -->
      <script>
          $(function () {
              $("#<%= TextBox1.ClientID %>").datepicker({
                  showWeek: true,
                  yearSuffix: "-CE",
                  showAnim: "slide"
              });
          });</script>
   </head>
   <body>
      <!-- HTML -->
      <%--<p>Enter Date: <input type="text" id="datepicker-11" name="pickdate"></p>--%>
       <p>Enter Date:</p>
       <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       <asp:Button ID="Button1" runat="server" Text="OK" OnClick="btnClick"/>
       &nbsp;
       <asp:Label ID="lblDate" runat="server" Text="" ForeColor="red"></asp:Label>
</asp:Content>
