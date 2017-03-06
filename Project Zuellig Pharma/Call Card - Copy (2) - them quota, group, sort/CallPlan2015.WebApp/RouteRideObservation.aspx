<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteRideObservation.aspx.cs" Inherits="CallPlan2015.WebApp.RouteRideObservation" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link href="http://localhost:42526/Styles/Bootstrap/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="~/Styles/Bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <title></title>

</head>
<body><form id="form1" runat="server">
      
<%--    <div class="header" >
            <div class="title">
            <img src="../Image/aaa.png" alt="" >
            </div>
    </div>   --%> 

    <div>
        <h3 class="text-center">
            <span class="label label-primary col-lg-6 col-lg-offset-3 text-center">Route Ride Observation</span>
        </h3>
        <br/>      
        <br/>         
       
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
            KeyFieldName="Id" OnRowDeleting="ASPxGridView1_RowDeleting"
            OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating" 
            OnCellEditorInitialize="grid_CellEditorInitialize" Width="100%" Theme="Aqua" Font-Size="10">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowClearFilterButton="True" Width="100">
                    <EditButton Visible="True">
                    </EditButton>
<%--                    <NewButton Visible="True">
                    </NewButton>
                    <DeleteButton Visible="True">
                    </DeleteButton>--%>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                
                <%-- //Header nhieu dong   Multi-Row Headers
                <dx:GridViewBandColumn Caption="Ngay CallPlan" VisibleIndex="1">
                    <Columns>
                       <dx:GridViewDataTextColumn FieldName="DateCallplan" >
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>--%>
                <dx:GridViewDataTextColumn FieldName="No" VisibleIndex="1" Width="50">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DateCallplan" VisibleIndex="1" Width="120">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SC" VisibleIndex="2" Width="50" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerCode" VisibleIndex="3" Width="130">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="4" Width="140">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="Dist" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="Area" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="CustomerClassification" VisibleIndex="8" Width="170">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="MonthlyCallFrequency" VisibleIndex="9" Width="170">
                </dx:GridViewDataTextColumn>
                  <dx:GridViewDataComboBoxColumn FieldName="CustomerType" VisibleIndex="10" Width="140">
                </dx:GridViewDataComboBoxColumn>
                 <dx:GridViewDataTextColumn FieldName="FFM" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="ObserverName" VisibleIndex="12" Width="130">
                </dx:GridViewDataTextColumn>
                

<dx:GridViewDataTextColumn FieldName="WorkDayStartTime" VisibleIndex="13" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="WorkDayCompletedTime" VisibleIndex="14" Width="190"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TotalWorkDaytime" VisibleIndex="15" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="LunchTime" VisibleIndex="16" Width="120"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="NoonBreak" VisibleIndex="17" Width="120"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TransferTime" VisibleIndex="18" Width="120"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeEntering" VisibleIndex="19" Width="120"></dx:GridViewDataTextColumn>
<dx:GridViewDataComboBoxColumn FieldName="MeetCustomer" VisibleIndex="20" Width="120"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="MeetPharmacyOwner" VisibleIndex="21" Width="160"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="MeetPharmacyOrderingStaff" VisibleIndex="22" Width="200"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="MeetPharmacyAssisstant" VisibleIndex="23" Width="180"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn FieldName="TimeToGreet" VisibleIndex="24" Width="130"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToCheckStock" VisibleIndex="25" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToTakeOrder" VisibleIndex="26" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToNegotiate" VisibleIndex="27" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToDoSurvey" VisibleIndex="28" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToDiscussOtherServices" VisibleIndex="29" Width="210"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToDoProductDetailing" VisibleIndex="30" Width="200"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToImplementMerchandisingAction" VisibleIndex="31" Width="270"></dx:GridViewDataTextColumn>
<dx:GridViewDataComboBoxColumn FieldName="CheckInventoryToOrder" VisibleIndex="32" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="ExistingProductDetailing" VisibleIndex="33" Width="190"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="NewProductDetailing" VisibleIndex="34" Width="160"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="Promotion" VisibleIndex="35" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="Contract" VisibleIndex="36" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="MerchandisingPOPM" VisibleIndex="37" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="HandlingIssues" VisibleIndex="38" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="OtherServices" VisibleIndex="39" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn FieldName="TimeToImplementActionsNegotiatedWithCustomer" VisibleIndex="40" Width="340"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TimeToInputIntoPOES" VisibleIndex="41" Width="190"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="SpareTime" VisibleIndex="41" Width="190"></dx:GridViewDataTextColumn>

<dx:GridViewDataTextColumn FieldName="TotalStoreCallTime" VisibleIndex="42" Width="150"></dx:GridViewDataTextColumn>
<dx:GridViewDataComboBoxColumn FieldName="SystemUsedToTakeOrder" VisibleIndex="43" Width="200"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="TimingOfOrder" VisibleIndex="44" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="UpSelling" VisibleIndex="45" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="CrossSelling" VisibleIndex="46" Width="150"></dx:GridViewDataComboBoxColumn>
<dx:GridViewDataComboBoxColumn FieldName="OrderSuccessful" VisibleIndex="47" Width="150"></dx:GridViewDataComboBoxColumn>
<%--<dx:GridViewDataComboBoxColumn FieldName="DeliveryTimeExpected" VisibleIndex="48" Width="190"></dx:GridViewDataComboBoxColumn>--%>
<dx:GridViewDataTextColumn FieldName="Comments" VisibleIndex="49" Width="150"></dx:GridViewDataTextColumn>
                

                
               <%--<dx:GridViewDataComboBoxColumn FieldName="CustomerType" VisibleIndex="4" Width="150"> 
                <PropertiesComboBox EnableSynchronization="False" IncrementalFilteringMode="StartsWith" >
                    <ClientSideEvents EndCallback="OnEndCallback"/>
                </PropertiesComboBox>--%>
             <%--</dx:GridViewDataComboBoxColumn>--%> 
            </Columns>

            <%--<Settings ShowFilterRow="True"></Settings>--%>
            <Settings ShowFooter="True" ShowHeaderFilterButton="true" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" ></SettingsEditing>
            <Settings VerticalScrollableHeight="300" />
 <%--           edit mode PopupEditForm
            <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
            <SettingsPopup>
            <EditForm Width="800" />
            </SettingsPopup>--%>
        </dx:ASPxGridView>

        <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
        <dx:ASPxButton ID="btnBackHome" runat="server" Text="Back" UseSubmitBehavior="False"
                    OnClick="btnBackHome_Click" />

    </div>
    </form>
</body>
</html>
