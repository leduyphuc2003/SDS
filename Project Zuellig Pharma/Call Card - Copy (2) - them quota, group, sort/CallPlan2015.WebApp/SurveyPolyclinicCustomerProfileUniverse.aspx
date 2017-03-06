<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyPolyclinicCustomerProfileUniverse.aspx.cs" Inherits="CallPlan2015.WebApp.SurveyPolyclinicCustomerProfileUniverse" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8">
    <title></title>
</head>
<body><form id="form1" runat="server">
<%--     <div class="header" >
            <div class="title">
            <img src="../Image/aaa.png" alt="" >
            </div>
    </div>  --%>
        
    <div>
        <h3 class="text-center">
            <span class="label label-primary col-lg-6 col-lg-offset-3 text-center">Survey Polyclinic Customer Profile Universe</span>
        </h3>
        <br/>      
        <br/>         
       
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
            KeyFieldName="Id" OnRowDeleting="ASPxGridView1_RowDeleting"
            OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating" 
            OnCellEditorInitialize="grid_CellEditorInitialize" Width="100%" Theme="Aqua" Font-Size="10">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowClearFilterButton="True" >
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
             
                <dx:GridViewBandColumn Caption="STT" VisibleIndex="1">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="No" VisibleIndex="1" Width="50"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                 
                <dx:GridViewBandColumn Caption="Vùng" VisibleIndex="2">
                    <Columns>
                       <dx:GridViewDataComboBoxColumn FieldName="State" VisibleIndex="2"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Tỉnh" VisibleIndex="3">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Province" VisibleIndex="3"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Nhóm chịu trách nhiệm" VisibleIndex="4">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="AssignTEAM" VisibleIndex="4" Width="160"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="ASM/MAM Chịu trách nhiệm" VisibleIndex="5" >
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="AssignASMOrMAM" VisibleIndex="5" Width="180"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="SC Chịu trách nhiệm" VisibleIndex="6" >
                    <Columns>
                           <dx:GridViewDataTextColumn FieldName="SC" VisibleIndex="6" Width="80"></dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="AssignSC" VisibleIndex="7"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
              <dx:GridViewBandColumn Caption="Code Khoa Dược" VisibleIndex="8" >
                    <Columns>
                            <dx:GridViewDataTextColumn FieldName="CustomerCodePharmacyDept" VisibleIndex="8" Width="210"></dx:GridViewDataTextColumn>
                       </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Code Nhà Thuốc (BV/PK)" VisibleIndex="9" >
                    <Columns>
                            <dx:GridViewDataTextColumn FieldName="CustomerCodeServicePharmacy" VisibleIndex="9" Width="240"></dx:GridViewDataTextColumn>
                        </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Đã cover" VisibleIndex="10" >
                    <Columns>
                            <dx:GridViewDataTextColumn FieldName="Covered" VisibleIndex="10" Width="100"></dx:GridViewDataTextColumn>
                         </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Tên (Bệnh viện/PKDDK)" VisibleIndex="11" >
                    <Columns>
                           <dx:GridViewDataTextColumn FieldName="NamePrivateHospitalOrPolyclinic" VisibleIndex="11" Width="250"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Tên công ty chủ quản" VisibleIndex="12" >
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="12" Width="150"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Số giường bệnh" VisibleIndex="13" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="NumberOfBeds" VisibleIndex="13" Width="140"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Có đầu tư nước ngoài" VisibleIndex="14" >
                    <Columns>
                         <dx:GridViewDataComboBoxColumn FieldName="ForeignInvestment" VisibleIndex="14" Width="150"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Nguồn cung cấp hàng  </br> (BV/PK mua hàng ở đâu)" VisibleIndex="15" >
                    <Columns>
                        <dx:GridViewDataComboBoxColumn FieldName="SourceOfPurchase" VisibleIndex="15" Width="150"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Địa chỉ" VisibleIndex="16" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="16"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Phân loại" VisibleIndex="17" >
                    <Columns>
                      <dx:GridViewDataComboBoxColumn FieldName="Classification" VisibleIndex="17" Width="130"></dx:GridViewDataComboBoxColumn>
                   </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Người quyết định chính" VisibleIndex="18" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="KeyDecisionMaker" VisibleIndex="18" Width="150"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Chức vụ" VisibleIndex="19" >
                    <Columns>
                     <dx:GridViewDataComboBoxColumn FieldName="Title" VisibleIndex="19"></dx:GridViewDataComboBoxColumn>
                   </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Số điện thoại" VisibleIndex="20" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="MobilePhone" VisibleIndex="20" Width="120"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Chuyên Khoa/ Đa Khoa" VisibleIndex="21" >
                    <Columns>
                        <dx:GridViewDataComboBoxColumn FieldName="SpecialtyOrPoly" VisibleIndex="21" Width="150"></dx:GridViewDataComboBoxColumn>
                       </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Có phòng tiêm <br>vaccin được cấp phép" VisibleIndex="22" >
                    <Columns>
                        <dx:GridViewDataComboBoxColumn FieldName="VaccinationRoom" VisibleIndex="22" Width="150"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Có hợp đồng <br>bảo hiểm nhà nước" VisibleIndex="23" >
                    <Columns>
                       <dx:GridViewDataComboBoxColumn Caption="Contract With<br> Government Insurance" FieldName="ContractWithGovernmentInsurance" VisibleIndex="23" Width="180"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Số thẻ bảo hiểm nhà nước <br>được cấp" VisibleIndex="24" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="NumberOfInsuranceCard" VisibleIndex="24" Width="190"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Có hợp đồng với các<br> cty bảo hiểm tư nhân" VisibleIndex="25" >
                    <Columns>
                      <dx:GridViewDataComboBoxColumn Caption="ContractWith<br>PrivateInsurances" FieldName="ContractWithPrivateInsurances" VisibleIndex="25" Width="150"></dx:GridViewDataComboBoxColumn>
                   </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Thời hạn thanh toán<br> đang áp dụng" VisibleIndex="26" >
                    <Columns>
                       <dx:GridViewDataTextColumn FieldName="CurrentCreditTerm" VisibleIndex="26" Width="160"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Hạn mức công nợ<br> (đang áp dụng)" VisibleIndex="27" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="CurrentCreditLimit" VisibleIndex="27" Width="160"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Số bệnh nhân khám bệnh<br> trung bình/ngày" VisibleIndex="28" >
                    <Columns>
                         <dx:GridViewDataTextColumn FieldName="AveragePatientDaily" VisibleIndex="28" Width="160"></dx:GridViewDataTextColumn>
                   </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Có áp giá thầu " VisibleIndex="29" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="TenderPriceApply" VisibleIndex="29" Width="150"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Tần suất giao hàng <br> hiện tại" VisibleIndex="30" >
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Current Delivery<br> Frequency" FieldName="CurrentDeliveryFrequency" VisibleIndex="30" Width="150"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                 <dx:GridViewBandColumn Caption="Các chương trình khuyến mãi/loyalty<br> (đang áp dụng)" VisibleIndex="31" >
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Loyalty Program <br>Or Promotion" FieldName="LoyaltyProgramOrPromotion" VisibleIndex="31" Width="180"></dx:GridViewDataTextColumn>
                     </Columns>
                </dx:GridViewBandColumn>
                
                <dx:GridViewBandColumn Caption="Ghi chú" VisibleIndex="32" >
                    <Columns>
                      <dx:GridViewDataComboBoxColumn FieldName="Remark" VisibleIndex="32" Width="250"></dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:GridViewBandColumn>

                      <dx:GridViewDataTextColumn FieldName="FFMFeedback" VisibleIndex="33" Width="130"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TeamT" VisibleIndex="34"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="StateT" VisibleIndex="35"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NewOldList" VisibleIndex="36"></dx:GridViewDataTextColumn>



            <%--<dx:GridViewDataTextColumn FieldName="WorkDayStartTime" VisibleIndex="13"></dx:GridViewDataTextColumn>--%>
 

            </Columns>

            <%--<Settings ShowFilterRow="True"></Settings>--%>
            <Settings ShowFooter="True" ShowHeaderFilterButton="true" />
            <%--<SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>--%>
            <SettingsEditing Mode="Inline"  ></SettingsEditing>
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
