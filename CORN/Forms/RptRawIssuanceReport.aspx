<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptRawIssuanceReport.aspx.cs" Inherits="Forms_RptRawIssuanceReport" Title="CORN :: Raw Material Issuance Report" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function CheckBoxListSelectSection() {
            var chkBoxList = document.getElementById('<%= chblSection.ClientID %>');
            var chkBox = document.getElementById('<%= chbAllSections.ClientID %>');
            var chkBoxCount;
            var i;
            if (chkBox.checked == true) {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            } else {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {

                    chkBoxCount[i].checked = false;
                }
            }
        }
        function CheckBoxListSelectItem() {
            var chkBoxList = document.getElementById('<%= chblItem.ClientID %>');
            var chkBox = document.getElementById('<%= chbAllItems.ClientID %>');
            var chkBoxCount;
            var i;
            if (chkBox.checked == true) {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            } else {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {

                    chkBoxCount[i].checked = false;
                }
            }
        }
        function UnCheckSelectAllSection() {
            var chkBox = document.getElementById('<%= chbAllSections.ClientID %>');
            var chkBoxList = document.getElementById('<%= chblSection.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
        function UnCheckSelectAllItem() {
            var chkBox = document.getElementById('<%= chbAllItems.ClientID %>');
            var chkBoxList = document.getElementById('<%= chblItem.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                    <asp:Label ID="lbltoLocation" runat="server" Text="Location" Font-Bold="true"></asp:Label>
                                    <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                                    <asp:TextBox ID="txtStartDate" runat="server"
                                        CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-1" style="margin-top: 27px">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                                    <asp:TextBox ID="txtEndDate" runat="server"
                                        CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-1" style="margin-top: 27px">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" />
                                </div>
                            </div>
                            <div class="row" style="height: 110px">
                            </div>
                            <div class="row">
                                <div class="col-md-offset-1 col-md-12 ">
                                    <div class="btnlist pull-right">
                                        <asp:Button ID="btnViewPDF" runat="server" OnClick="btnViewPDF_Click" Text="View PDF" CssClass=" btn btn-success" />
                                        <asp:Button ID="btnViewExcel" Visible="false" runat="server" OnClick="btnViewExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
                                    </div>
                                </div>
                            </div>
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                PopupPosition="BottomLeft" TargetControlID="txtStartDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                TargetControlID="txtEndDate" PopupPosition="TopLeft" OnClientShown="calendarShown"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-4">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chbAllSections" onclick="CheckBoxListSelectSection()" AutoPostBack="true" OnCheckedChanged="chbAllSections_CheckedChanged" CssClass="SingleCheckbox1" runat="server" Width="100%"
                                        Font-Size="10pt" Text="Section"></asp:CheckBox>
                                    <asp:Panel ID="Panel3" runat="server" Width="255px" Height="350px" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                        <asp:CheckBoxList ID="chblSection" AutoPostBack="true" OnSelectedIndexChanged="chblSection_SelectedIndexChanged" CssClass="SingleCheckbox1" onclick="UnCheckSelectAllSection()" margin-left="15px" runat="server" Width="236px" Font-Size="14px">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-4">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chbAllItems" onclick="CheckBoxListSelectItem()" CssClass="SingleCheckbox1" runat="server" Width="100%"
                                        Font-Size="10pt" Text="Item"></asp:CheckBox>
                                    <asp:Panel ID="Panel1" runat="server" Width="255px" Height="350px" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                        <asp:CheckBoxList ID="chblItem" CssClass="SingleCheckbox1" onclick="UnCheckSelectAllItem()" margin-left="15px" runat="server" Width="236px" Font-Size="14px">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnViewPDF" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="row">
        </div>
    </div>
</asp:Content>
