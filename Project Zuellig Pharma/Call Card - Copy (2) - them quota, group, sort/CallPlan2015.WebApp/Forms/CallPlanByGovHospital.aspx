<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CallPlanByGovHospital.aspx.cs" Inherits="CallPlan2015.WebApp.Forms.CallPlanByGovHospital" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="CallPlan2015.Common" %>
<%@ Import Namespace="CallPlan2015.DataModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2 align="center" style="color: #008B8B">DAILY GOVERNMENT HOSPITAL CALL PLAN</h2>
	<div class="row">
		<div class="col-md-5"></div>
		<div class="col-md-7">
			<div class="form-inline">
				<div class="form-group">
					<label>Date: </label>
					<%--<label><%= DateTime.Now.ToShortDateString() %></label>--%>
                    <label><%= Session[Constants.SESSION_GOV_HOS_DATE_SHOW].ToString() %></label>
				</div>
				<%--<div class="form-group">
					<label>Year: </label>
					<label><%= DateTime.Now.Year %></label>
				</div>--%>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col-md-4">
			<div class="form-inline">
				<div class="form-group">
					<label>SC Code: </label>
					<label runat="server" ID="lblScCode"></label>
				</div>
			</div>
		</div>
		<div class="col-md-4">
			<div class="form-inline">
				<div class="form-group">
					<label>SC Name: </label>
					<label runat="server" ID="lblScName"></label>
				</div>
			</div>
		</div>
		<div class="col-md-4">
			<div class="form-inline">
				<div class="form-group">
					<label>BU: </label>
					<label runat="server" ID="lblBU"></label>
				</div>
			</div>
		</div>
	</div>
	<div class="row">
		        <%-- them --%>
	    <div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>Days gone by: </label>
					<label runat="server" ID="lblDaysGoneBy"></label>
                    <br/>
                    <label>Days left in month: </label>
					<label runat="server" ID="lblLeftWD"></label>
                    <br/>
                    <label>Total Days in month: </label>
					<label runat="server" ID="lblTotalDaysInMonth"></label>
				</div>
			</div>
		</div>
		<div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>Monthly Target: </label>
					<label runat="server" ID="lblTargetMTD"></label>
				</div>
			</div>
		</div>
		<div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>Sales MTD: </label>
					<label runat="server" ID="lblSalesMTD"></label>
				</div>
			</div>
		</div>
        
        <div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>% By SC: </label>
					<label runat="server" ID="lblBySC"></label>
				</div>
			</div>
		</div>

		<div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>Month to go: </label>
					<label runat="server" ID="lblMonthToGo"></label>
				</div>
			</div>
		</div>
		<div class="col-md-2">
			<div class="form-inline">
				<div class="form-group">
					<label>Target today: </label>
					<label runat="server" ID="lblTargetToday"></label>
                                        <%-- them --%>
                    <br/>
                    <label>% of month gone by: </label>
                    <label runat="server" ID="lblOfMonthGoneBy"></label>
				</div>
			</div>
		</div>
	</div>
	<div class="space"></div>
	<div class="row">
		<div class="col-md-12">
			<table class="list-table">
				<tr class="list-title-bg">
				    <td align="center">Checked</td>
					<td align="center">No.</td>
					<td align="center">CUSTOMER CODE</td>
					<td align="center">CUSTOMER NAME</td>
					<td align="center">ADDRESS</td>
					<td align="center">DISTRICT</td>
					<td align="center">CLASS</td>
					<td align="center" style="width: 50px">AVE. 6 LAST MONTH</td>
					<td align="center" style="width: 50px">MTD ALL SOURCES</td>
					<td align="center" style="width: 50px">MTD SC SOURCES</td>
					<%--<td align="center" style="width: 50px">% SALES BY SC</td>--%>
                    <%-- them --%>
                    <td align="center" style="width: 50px">% MTD vs Ave Last 6 M</td>
				</tr>
				<asp:Repeater runat="server" ID="rCustomer">
					<ItemTemplate>
						<%--<tr class="<%# GenerateClass(((CallPlanData)Container.DataItem).Class, Container.ItemIndex % 2 + 1) %>"--%>
                        <tr class="<%# GenerateClass(((CallPlanData)Container.DataItem), Container.ItemIndex % 2 + 1) %>"
                        onclick="LinkToPharmacyCustomer('<%# ((CallPlanData)Container.DataItem).CustomerCode %>')">
                            
                            <%--icon checked--%>
                            <td align="center" style="<%# ChangeIcon(((CallPlanData)Container.DataItem))%>"><i class="fa fa-check-circle" aria-hidden="true"></i></td>

							<td align="left"><%# Container.ItemIndex + 1 %></td>
							<td align="left"><%# ((CallPlanData)Container.DataItem).CustomerCode %></td>
							<td align="left"><%# ((CallPlanData)Container.DataItem).CustomerName %></td>
							<td align="left"><%# ((CallPlanData)Container.DataItem).Address %></td>
							<td align="left"><%# ((CallPlanData)Container.DataItem).District %></td>
							<td align="left"><%# ((CallPlanData)Container.DataItem).Class %></td>
							<td align="right"><%# ((CallPlanData)Container.DataItem).Ave6LastMonth.ToString("n0") %></td>
							<td align="right"><%# ((CallPlanData)Container.DataItem).MtdAllSources.ToString("n0") %></td>
							<td align="right"><%# ((CallPlanData)Container.DataItem).MtdScSources.ToString("n0") %></td>
							<%--<td align="right"><%# ((CallPlanData)Container.DataItem).PercentSaleBySc.ToString("n0") %></td>--%>
						    <%-- them --%>
                            <td align="right"><%# ((((CallPlanData)Container.DataItem).MtdAllSources/((CallPlanData)Container.DataItem).Ave6LastMonth)*100).ToString("n1")  %></td>
						
                        </tr>
					</ItemTemplate>
				</asp:Repeater>
                <tr>
					<td style="border:none;"></td>
					<td style="border:none;"></td>
					<td style="border:none;"></td>
					<td style="border:none;"></td>
					<td style="border:none;"></td>
					<td></td>
                    <td style="text-align: right; font-weight: bold"><asp:Literal runat="server" ID="ltSumAve6LastMonth"/></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
			</table>
		</div>
	</div>

    <script type="text/javascript">
        function LinkToPharmacyCustomer(customerCode) {
            //var newwindow = window.open("http://localhost/CallPlan2015/CallPlan2015.WebApp/Forms/PharmacyCustomer.aspx", 'Pharmacy by customer', 'height=600,width=800');
            //if (window.focus) {
            //    newwindow.focus();
            //}

            //return false;
            //khi click vao cust se open ra link dan den trang callcard PhaymacyCustomer
            window.location = "CallCardByGovHospital.aspx" + "?cc=" + customerCode;
        }
    </script>
</asp:Content>
