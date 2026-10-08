<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSalePersonDSR.aspx.cs" Inherits="Forms_frmSalePersonDSR" Title="CORN :: Daily Sales Report" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function ReportTypeChange(SelectedValue)
        {
            document.getElementById('ctl00_ctl00_mainCopy_cphPage_hfReportType').value = SelectedValue;
            if(SelectedValue == '4')
            {
                document.getElementById('divCashier').style.display = "block";
            }
            else
            {
                document.getElementById('divCashier').style.display = "none";
            }
        }

        function pageLoad()
        {
            if (document.getElementById('ctl00_ctl00_mainCopy_cphPage_hfReportType').value == '4') {
                document.getElementById('divCashier').style.display = "block";
            }
            else {
                document.getElementById('divCashier').style.display = "none";
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <sapn class="fa fa-caret-right rgt_cart"></sapn>
                            <asp:Label ID="Label1" runat="server" Text="DSR Wise" Font-Bold="true"></asp:Label>
                            <dx:ASPxComboBox ID="RbReportType" runat="server" CssClass="form-control" ClientSideEvents-SelectedIndexChanged="function(s, e) { ReportTypeChange(s.GetSelectedItem().value); }">
                                <Items>
                                    <dx:ListEditItem Value="1" Selected="True" Text="Location" />
                                    <dx:ListEditItem Value="2" Text="Date" />
                                    <dx:ListEditItem Value="4" Text="Cashier" />
                                </Items>
                            </dx:ASPxComboBox>
                            <asp:HiddenField runat="server" ID="hfReportType"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <sapn class="fa fa-caret-right rgt_cart"></sapn>
                            <asp:Label ID="lbltoLocation" runat="server" Text="Location" Font-Bold="true"></asp:Label>
                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row" id="divCashier" style="display:none;">
                        <div class="col-md-4">
                            <sapn class="fa fa-caret-right rgt_cart"></sapn>
                            <asp:Label ID="lblCashier" runat="server" Text="Cashier" Font-Bold="true"></asp:Label>
                            <dx:ASPxComboBox ID="ddlCashier" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                            <asp:TextBox ID="txtStartDate" runat="server" onkeyup="BlockStartDateKeyPress()"
                                CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                            <asp:TextBox ID="txtEndDate" runat="server" onkeyup="BlockEndDateKeyPress()"
                                CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                        </div>
                    </div>

                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                        PopupPosition="TopRight" TargetControlID="txtStartDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                        TargetControlID="txtEndDate" PopupPosition="TopRight" OnClientShown="calendarShown"></cc1:CalendarExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnViewPDF" runat="server" OnClick="btnViewPDF_Click" Text="View PDF" CssClass=" btn btn-success" />
                    <asp:Button ID="btnViewExcel" runat="server" OnClick="btnViewExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
