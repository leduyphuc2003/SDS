<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PharmacyCustomer.aspx.cs" Inherits="CallPlan2015.WebApp.Forms.PharmacyCustomer" %>
<%@ Import Namespace="CallPlan2015.Common" %>
<%@ Import Namespace="CallPlan2015.DataModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center">CALL CARD PER <span style="color: green">CUSTOMER</span></h2>
	<div class="row">
		<div class="col-md-5 col-md-offset-1">Report on Mini POES</div>
		<div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Date: </label>
					<label><%= DateTime.Now.ToShortDateString() %></label>
				</div>
				<div class="form-group">
					<label>Year: </label>
					<label><%= DateTime.Now.Year %></label>
				</div>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Customer code: </label>
					<label>Z0001</label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Customer name: </label>
					<label>Tran Van A</label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Address: </label>
					<label>Tp. Ho Chi Minh</label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Telephone: </label>
					<label>0909090909</label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Contact person: </label>
					<label>Le Duy Phuc</label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Payment team: </label>
					<label>IC Dota</label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Sc code: </label>
					<label>Z0001</label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Sale consultant name: </label>
					<label>Tran Van A</label>
				</div>
			</div>
		</div>
	</div>
	<div class="space"></div>
    <div class="row">
		<div class="col-md-12">
			<table class="list-table">
			    <tr class="list-title-bg">
					<td align="center" rowspan="2">PRN CODE</td>
					<td align="center" rowspan="2">PRO CODE</td>
					<td align="center" rowspan="2">DESCRIPTION</td>
					<td colspan="3" style="font-weight: bold">Data by value</td>
					<td colspan="4" style="font-weight: bold">Data by quantity</td>
				</tr>
				<tr class="list-title-bg">
					<td align="center" style="width: 50px">AVE6 LAST MONTH</td>
					<td align="center" style="width: 50px">MTD ALL SOURCE</td>
					<td align="center" style="width: 50px">MTD SC SOURCE</td>
					<td align="center" style="width: 50px">AVE6 LAST MONTH</td>
					<td align="center" style="width: 50px">MTD ALL SOURCE</td>
					<td align="center" style="width: 50px">MTD SC SOURCE</td>
					<td align="center" style="width: 50px">CHECK STOCK</td>
				</tr>
				<asp:Repeater runat="server" ID="rPharmacyCustomer">
					<ItemTemplate>
						<tr <%# ((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others")
                                ? "class='list-item-bg3'" : ((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum")
                                ? "class='list-item-bg4'" : "class='list-item-bg-no-cursor" + (Container.ItemIndex % 2 + 1).ToString() + "'" %>
                            onclick="ShowPharmacyDetail('<%# ((PharmacyCustomerData)Container.DataItem).PrnCode %>')">
							<td align="left"><%# Eval(Constants.PHARMACY_CUSTOMER_PRN_CODE) %></td>
						    <td align="left"><asp:Label runat="server" ID="lblProCode" Text="<%# Eval(Constants.PHARMACY_CUSTOMER_PRO_CODE) %>"/></td>
							<td align="left"><%# Eval(Constants.PHARMACY_CUSTOMER_DESCRIPTION) %></td>
							<td align="right"><%# Eval(Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_VALUE) %></td>
							<td align="right"><%# Eval(Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_VALUE) %></td>
							<td align="right"><%# Eval(Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_VALUE) %></td>
							<td align="right">
							    <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ? Eval(Constants.PHARMACY_CUSTOMER_AVE6_LAST_MONTH_QUANTITY) : string.Empty %>
							</td>
							<td align="right">
							    <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ? Eval(Constants.PHARMACY_CUSTOMER_MTD_ALL_SOURCE_QUANTITY) : string.Empty %>
							</td>
                            <td align="right">
							    <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ? Eval(Constants.PHARMACY_CUSTOMER_MTD_SC_SOURCE_QUANTITY) : string.Empty %>
							</td>
							<td align="right">
							    <asp:TextBox runat="server" ID="txtStock" CssClass="callplan-text"
                                    Text="<%# Eval(Constants.PHARMACY_CUSTOMER_CHECK_STOCK) %>"
                                    Visible='<%# (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                              && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false) %>'/>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
                <tr <%= string.IsNullOrEmpty(Request["detail"]) ? "" : " style='visibility: hidden'" %>>
                    <td colspan="10"></td>
                </tr>
                <tr <%= string.IsNullOrEmpty(Request["detail"]) ? "" : " style='visibility: hidden'" %>>
                    <td colspan="3" style="font-weight: bold">Grand total</td>
                    <td style="text-align: right; font-weight: bold"><asp:Literal runat="server" ID="ltSumAve6LastMonth"/></td>
                    <td style="text-align: right; font-weight: bold"><asp:Literal runat="server" ID="ltSumMtdAllSources"/></td>
                    <td style="text-align: right; font-weight: bold"><asp:Literal runat="server" ID="ltSumMtdScSources"/></td>
                    <td colspan="4"></td>
                </tr>
			</table>
		</div>
	</div>
    <div class="space"></div>
    <div class="row">
        <div class="col-md-12" align="right">
            <asp:Button runat="server" ID="btnSubmitStock"
                Text="Submit Stock" CssClass="btn btn-primary"
                OnClick="btnSubmitStock_OnClick"/>
        </div>
    </div>
    
    <script type="text/javascript">
        function ShowPharmacyDetail(customerCode) {
            if (customerCode.indexOf('Others') >= 0) {
                var newwindow = window.open("PharmacyCustomer.aspx" + "?detail=" + customerCode.replace('Others ', ''), 'Pharmacy by customer', 'height=600,width=800');
                if (window.focus) {
                    newwindow.focus();
                }
            }
            return false;
        }

        $(document).ready(function () {
            //called when key is pressed in textbox
            $('input[type=text]').keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
                return true;
            });
        });
    </script>
</asp:Content>
