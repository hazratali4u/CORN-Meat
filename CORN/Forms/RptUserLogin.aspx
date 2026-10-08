<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptUserLogin.aspx.cs" Inherits="Forms_RptUserLogin" Title="CORN :: User Login History" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
       
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnl_rpt" runat="server">
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lblUser" runat="server" Text="User" Font-Bold="true"></asp:Label>
                                <dx:ASPxComboBox ID="ddlUser" runat="server" CssClass="form-control">
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
                                <asp:ImageButton ID="ibtnStartDate" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                                <asp:TextBox ID="txtEndDate" runat="server" onkeyup="BlockEndDateKeyPress()"
                                    CssClass="form-control" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="margin-top: 27px">
                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                        </div>

                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                        <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                            PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy" PopupPosition="BottomLeft" OnClientShown="calendarShown">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy" PopupPosition="TopLeft" OnClientShown="calendarShown">
                        </cc1:CalendarExtender>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
                <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                <asp:Button ID="btnPDF" runat="server" OnClick="btnPDF_Click" Text="View PDF" CssClass=" btn btn-success" />
                <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
            </div>
        </div>
                    </div>
    </div>
</asp:Content>
