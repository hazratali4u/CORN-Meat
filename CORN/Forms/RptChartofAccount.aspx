<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptChartofAccount.aspx.cs" Inherits="Forms_RptChartofAccount" Title="CORN :: Chart of Account" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
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
                            <label><span class="fa fa-caret-right rgt_cart"></span>Account Category</label>
                            <dx:ASPxComboBox ID="DrpAccountCategory" runat="server" AutoPostBack="true" SelectedIndex="0"
                                CssClass="form-control" OnSelectedIndexChanged="DrpAccountCategory_SelectedIndexChanged">
                                <Items>
                                    <dx:ListEditItem Text="Balance Sheet Account" Selected="true" Value="0"></dx:ListEditItem>
                                    <dx:ListEditItem Text="Income Statment Account" Value="1"></dx:ListEditItem>
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class=" row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Account Type</label>
                            <dx:ASPxComboBox ID="DrpMainType" runat="server" AutoPostBack="true"
                                CssClass="form-control" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class=" row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Account Sub Type</label>
                            <dx:ASPxComboBox ID="DrpSubType" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="DrpSubType_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class=" row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Account Detail Type</label>
                            <dx:ASPxComboBox ID="DrpDetailType" runat="server" CssClass="form-control"
                                AutoPostBack="True">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
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
