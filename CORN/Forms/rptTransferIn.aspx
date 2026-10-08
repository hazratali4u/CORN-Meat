<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTransferIn.aspx.cs" Inherits="Forms_rptTransferIn"
    MasterPageFile="~/Forms/PageMaster.master" Title="CORN :: Transfer In/Out Report" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="javascript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
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
                            <asp:RadioButtonList ID="RbTransferType" runat="server"
                                RepeatDirection="Horizontal" Width="250">
                                <asp:ListItem Selected="True" Value="Transfer In">Transfer In</asp:ListItem>
                                <asp:ListItem>Transfer Out</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Report In</label>
                            <dx:ASPxComboBox ID="DrpReportType" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpReportType_SelectedIndexChanged" SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="Unit" Value="1"></dx:ListEditItem>
                                    <%--<asp:ListItem>Carton</asp:ListItem>--%>
                                    <dx:ListEditItem Text="Value" Value="2"></dx:ListEditItem>
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox ID="DrpLocation" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="30px"></asp:ImageButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="30px"></asp:ImageButton>
                        </div>
                    </div>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        EnableViewState="False" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate" OnClientShown="calendarShown" PopupPosition="TopLeft"></cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        EnableViewState="False" PopupButtonID="ImgToDate" TargetControlID="txtToDate" OnClientShown="calendarShown" PopupPosition="TopLeft"></cc1:CalendarExtender>
                    &nbsp;&nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnViewPDF" runat="server" CssClass=" btn btn-success" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="btn btn-success" Text="View Excel"
                        OnClick="btnViewExcel_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
