<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptTrialBalance.aspx.cs" Inherits="Forms_RptTrialBalance" Title="CORN :: Trial Balance" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        $('#btnClose').click(function () {
            document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
        });
        function ShowListFrom() {
            var strFromCode = document.getElementById('<%= txtFromAccount.ClientID %>').value;
            if (strFromCode.length == 0) {
                document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= LstAccountHead.ClientID %>").focus();
            }
        }
        function ShowListTo() {
            var strFromCode = document.getElementById('<%= txttoAccount.ClientID %>').value;

            if (strFromCode.length == 0) {
                document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= LstAccountHead.ClientID %>").focus();
            }
        }
        function SelectCode(e) {
            if (e.keyCode == 13) {
                var StrValue = document.getElementById("<%= txtFromAccount.ClientID %>").value;
                if (StrValue.length == 0) {
                    var str = document.getElementById("<%= LstAccountHead.ClientID %>").value;
                    document.getElementById("<%= txtFromAccount.ClientID %>").value = str;
                    document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
                    document.getElementById("<%= txttoAccount.ClientID %>").focus();
                }
                else {
                    var str = document.getElementById("<%= LstAccountHead.ClientID %>").value;
                    document.getElementById("<%= txttoAccount.ClientID %>").value = str;
                    document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
                    document.getElementById("<%= txttoAccount.ClientID %>").focus();
                }
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:CheckBox ID="ChbSelectAll" runat="server" Height="27px" Text="All Accounts"
                                Width="100px" AutoPostBack="True" Checked="True" OnCheckedChanged="ChbSelectAll_CheckedChanged" />
                        </div>
                    </div>
                    <div class="row" style="height:20px;">

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">

                            <label><span class="fa fa-caret-right rgt_cart"></span>Level</label>
                            <dx:ASPxComboBox ID="DrpLevel" runat="server" CssClass="form-control">
                                <Items>
                                    <dx:ListEditItem Value="4" Text="4" Selected="true"></dx:ListEditItem>
                                    <dx:ListEditItem Value="3" Text="3"></dx:ListEditItem>
                                    <dx:ListEditItem Value="2" Text="2"></dx:ListEditItem>
                                    <dx:ListEditItem Value="1" Text="1"></dx:ListEditItem>
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Main Account</label>
                            <dx:ASPxComboBox ID="DrpMainAccount" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Account Head</label>
                            <asp:TextBox ID="txtFromAccount" runat="server" CssClass="form-control" onBlur="ShowListFrom()">0000000000</asp:TextBox>

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>To Account Head</label>
                            <asp:TextBox ID="txttoAccount" runat="server" CssClass="form-control" onBlur="ShowListTo()">9999999999</asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                            &nbsp;<asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                            &nbsp;<asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                        </div>
                    </div>

                    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                        TargetControlID="txtStartDate"></cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                        TargetControlID="txtEndDate"></cc1:CalendarExtender>
                    <div class="HidePanel" id="Panel3" runat="server">

                        <div class="col-md-4" style="left: 50%; position: absolute; top: 250px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <strong>Account Head List</strong>

                                    <asp:Button ID="btnClose" runat="server"
                                        Text="X" CssClass="btn btn-danger" Style="margin-left: 220px; height: 31px" />

                                </div>
                                <div class="panel-body">
                                    <div class="module_contents">

                                        <div class="col-md-12">
                                            <asp:ListBox ID="LstAccountHead" runat="server" CssClass="select" Height="232px"
                                                onkeyup="SelectCode(event)" RepeatLayout="Flow"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
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