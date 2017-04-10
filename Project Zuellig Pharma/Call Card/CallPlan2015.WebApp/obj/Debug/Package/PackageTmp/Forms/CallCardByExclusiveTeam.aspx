<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CallCardByExclusiveTeam.aspx.cs" Inherits="CallPlan2015.WebApp.Forms.CallCardByExclusiveTeam" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="CallPlan2015.Common" %>
<%@ Import Namespace="CallPlan2015.DataModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center" style="color: #008B8B">Exclusive CALL CARD<span style="color: green"></span></h2>
    <div class="row">
        <div class="col-md-5 col-md-offset-1"></div>
        <div class="col-md-5">
            <div class="form-inline">
                <div class="form-group">
                    <label>Date: </label>
                    <%--<label><%= DateTime.Now.ToShortDateString() %></label>--%>
                    <label><%= Session[Constants.SESSION_EXCLUSIVE_DATE_SHOW].ToString() %></label>
                </div>
                <%--<div class="form-group">
					<label>Year: </label>
					<label><%= DateTime.Now.Year %></label>
				</div>--%>
            </div>
        </div>
    </div>
    <%--<div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Customer code: </label>
					<label runat="server" ID="lblCustCode"></label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Customer name: </label>
					<label runat="server" ID="lblCustName"></label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Address: </label>
					<label runat="server" ID="lblCustAddress"></label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Telephone: </label>
					<label runat="server" ID="lblTelephone"></label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Contact person: </label>
					<label runat="server" ID="lblContactPerson"></label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Payment team: </label>
					<label runat="server" ID="lblTerm"></label>
				</div>
			</div>
		</div>
	</div>
    <div class="row">
		<div class="col-md-5 col-md-offset-1">
			<div class="form-inline">
				<div class="form-group">
					<label>Sc code: </label>
					<label runat="server" ID="lblScCode"></label>
				</div>
			</div>
		</div>
        <div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label>Sale consultant name: </label>
					<label runat="server" ID="lblScName"></label>
				</div>
			</div>
		</div>
	</div>--%>

    <%-- them moi --%>
    <div class="row">
        <%-- Cum 1 --%>
        <div class="col-md-3 " style="float: left">
            <div class="form-inline">
                <div class="form-group">
                    <label>Days gone by: </label>
                    <label runat="server" id="Label1"><%=Session[Constants.SESSION_DAYS_GONE_BY] %></label>
                    <br />
                    <label>Days left in month: </label>
                    <label runat="server" id="Label2"><%=Session[Constants.SESSION_Days_Left_In_Month] %></label>
                    <br />
                    <label>Total Days in month: </label>
                    <label runat="server" id="Label3"><%=(Session[Constants.SESSION_Total_Days_In_Month])%></label>
                    <br />
                    <label>% Of Month Gone By: </label>
                    <label runat="server" id="Label4"><%=Session[Constants.SESSION_OF_Month_Gone_By]%> %</label>
                </div>
            </div>
        </div>
        <%-- Cum 2 --%>
        <div class="col-md-3" style="float: left">
            <div class="form-inline">
                <div class="form-group">
                    <label>Customer code: </label>
                    <label runat="server" id="lblCustCode"></label>
                    <br />
                    <label>Customer name: </label>
                    <label runat="server" id="lblCustName"></label>
                    <br />
                    <label>Address: </label>
                    <label runat="server" id="lblCustAddress"></label>
                    <br />
                    <label>Telephone: </label>
                    <label runat="server" id="lblTelephone"></label>
                </div>
            </div>
        </div>
        <%-- Cum 3 --%>
        <div class="col-md-3" style="float: left; width: 500px">
            <div class="form-inline">
                <div class="form-group">
                    <label>Contact person: </label>
                    <label runat="server" id="lblContactPerson"></label>
                    <br />
                    <label>Payment team: </label>
                    <label runat="server" id="lblTerm"></label>
                    <br />
                    <label>Sc code: </label>
                    <label runat="server" id="lblScCode"></label>
                    <br />
                    <label>Sale consultant name: </label>
                    <label runat="server" id="lblScName"></label>
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
                    <td colspan="4" style="font-weight: bold">Data by value</td>
                    <td colspan="4" style="font-weight: bold">Data by quantity</td>
                </tr>
                <tr class="list-title-bg">
                    <td align="center" style="width: 50px">AVE6 LAST MONTH</td>
                    <td align="center" style="width: 50px">MTD ALL SOURCE</td>
                    <td align="center" style="width: 50px">MTD SC SOURCE</td>
                    <%-- them --%>
                    <td align="center" style="width: 50px; width: 80px">% MTD vs Ave Last 6M</td>

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
                            <td align="left"><%# ((PharmacyCustomerData)Container.DataItem).PrnCode %></td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblProCode" Text="<%#((PharmacyCustomerData)Container.DataItem).ProCode%>" /></td>
                            <td align="left"><%# ((PharmacyCustomerData)Container.DataItem).Description %></td>
                            <td align="right"><%# ((PharmacyCustomerData)Container.DataItem).Ave6LastMonthValue.ToString("n0") %></td>
                            <td align="right"><%# ((PharmacyCustomerData)Container.DataItem).MtdAllSourcesValue.ToString("n0") %></td>
                            <td align="right"><%# ((PharmacyCustomerData)Container.DataItem).MtdScSourcesValue.ToString("n0") %></td>

                            <%-- them --%>
                            <td class="<%# DoiMau(((PharmacyCustomerData)Container.DataItem), Container.ItemIndex % 2 + 1) %>"
                                align="right"><%# (((((PharmacyCustomerData)Container.DataItem).MtdAllSourcesValue/((PharmacyCustomerData)Container.DataItem).Ave6LastMonthValue))*100).ToString("n0") +" %" %></td>

                            <td align="right">
                                <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ? ((PharmacyCustomerData)Container.DataItem).Ave6LastMonthQuantity.ToString("n0") : string.Empty %>
                            </td>
                            <td align="right">
                                <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ? ((PharmacyCustomerData)Container.DataItem).MtdAllSourcesQuantity.ToString("n0"): string.Empty %>
                            </td>
                            <td align="right">
                                <%# ((((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                 && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false))
                                 ?((PharmacyCustomerData)Container.DataItem).MtdScSourcesQuantity.ToString("n0") : string.Empty %>
                            </td>
                            <td align="right">
                                <asp:TextBox runat="server" ID="txtStock" CssClass="callplan-text" Style="text-align: left"
                                    Text="<%#((PharmacyCustomerData)Container.DataItem).CheckStock %>"
                                    Visible='<%# (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Others") == false)
                                              && (((PharmacyCustomerData)Container.DataItem).PrnCode.Contains("Sum") == false) %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr <%= string.IsNullOrEmpty(Request["detail"]) ? "" : " style='visibility: hidden'" %>>
                    <td colspan="10"></td>
                </tr>
                <tr <%= string.IsNullOrEmpty(Request["detail"]) ? "" : " style='visibility: hidden'" %>>
                    <td colspan="3" style="font-weight: bold">Grand total</td>
                    <td style="text-align: right; font-weight: bold">
                        <asp:Literal runat="server" ID="ltSumAve6LastMonth" /></td>
                    <td style="text-align: right; font-weight: bold">
                        <asp:Literal runat="server" ID="ltSumMtdAllSources" /></td>
                    <td style="text-align: right; font-weight: bold">
                        <asp:Literal runat="server" ID="ltSumMtdScSources" /></td>
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
                OnClick="btnSubmitStock_OnClick" Style="background-color: #094658" />
        </div>
    </div>

    <script type="text/javascript">
        function ShowPharmacyDetail(customerCode) {
            //neu co chu Others
            if (customerCode.indexOf('Others') >= 0) {
                var newwindow = window.open("CallCardByExclusiveTeam.aspx" + "?detail=" + customerCode.replace('Others ', ''), 'Call Card By Exclusive Team', 'height=600,width=800');
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