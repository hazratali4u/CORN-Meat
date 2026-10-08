<%@ Page Title="CORN :: Item Sales Contribution" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="rptItemContribution.aspx.cs" Inherits="Forms_rptItemContribution" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }        function ValidateForm() {

            return true;
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
                                       <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Report Type" Width="78px"
                                        Visible="False"></asp:Label>
                                      <asp:DropDownList ID="DrpReportType" runat="server" Width="200px" CssClass="DropList"
                                        Visible="False">
                                        <asp:ListItem>Sales</asp:ListItem>
                                    </asp:DropDownList>
                                   </div>
                                </div>
                                <div class="row">
                                   <div class="col-md-4">
                                       <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Value Type" Width="78px"
                                        Visible="False"></asp:Label>
                                    <asp:DropDownList ID="DrpUnitType" runat="server" Width="200px" CssClass="DropList"
                                        Visible="False">
                                        <asp:ListItem>Pieces</asp:ListItem>
                                        <asp:ListItem>Value</asp:ListItem>
                                    </asp:DropDownList>
                                   </div>
                                </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                                            </dx:ASPxComboBox>
                                          </div>
                                    </div>
                                <div class="row">
                                        <div class="col-md-4">
                                             <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" MaxLength="10"
                                                onkeyup="BlockStartDateKeyPress()"></asp:TextBox>
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
                                        TargetControlID="txtStartDate" OnClientShown="calendarShown" PopupPosition="TopLeft">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                        TargetControlID="txtEndDate" PopupPosition="TopLeft" OnClientShown="calendarShown">
                                    </cc1:CalendarExtender>
                            </asp:Panel>
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
