<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptPrincipalWiseExp.aspx.cs" Inherits="Forms_RptPrincipalWiseExp" Title="CORN :: Petty Expense Summary" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
    <script language="javascript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function SelectAllAccountHead() {
            var chkBoxList = document.getElementById('<%= LstAccountHead.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllAccountHead.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }
        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllAccountHead.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstAccountHead.ClientID %>');
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
                        <div class="col-md-8">
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox ID="DrpLocation" runat="server" CssClass="form-control"
                                AutoPostBack="True">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Account Type</label>
                            <dx:ASPxComboBox ID="DrpMasterHead" runat="server" CssClass="form-control" 
                                AutoPostBack="True" OnSelectedIndexChanged="DrpMasterHead_SelectedIndexChanged">
                                <%--<Items>
                                    <dx:ListEditItem Selected="True" Value="55" Text="Administrative Expenses"></dx:ListEditItem>
                                    <dx:ListEditItem Value="56" Text="Selling Expenses"></dx:ListEditItem>
                                </Items>--%>
                            </dx:ASPxComboBox>
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>


                            <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <%-- <label><span class="fa fa-caret-right rgt_cart"></span>Account Head</label>--%>
                            <asp:CheckBox ID="ChbAllAccountHead" onclick="SelectAllAccountHead()" runat="server"
                                Text=" All Account Head" AutoPostBack="True"></asp:CheckBox>

                            <asp:Panel ID="Panel1" runat="server" Width="70%" Height="190px" ScrollBars="Vertical"
                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                <asp:CheckBoxList ID="LstAccountHead" onclick="UnCheckSelectAll()" runat="server" Font-Size="Small"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </div>
                    </div>

                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="txtFromDate" EnableViewState="False" Format="dd-MMM-yyyy" PopupPosition="TopLeft" OnClientShown="calendarShown"></cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="txtToDate" EnableViewState="False" Format="dd-MMM-yyyy" PopupPosition="TopLeft" OnClientShown="calendarShown"></cc1:CalendarExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">

                    <asp:Button ID="btnViewPDF" runat="server" OnClick="btnViewPDF_Click" Text="View PDF"
                        CssClass=" btn btn-success" />

                    <asp:Button ID="btnViewExcel" runat="server" OnClick="btnViewExcell_Click" Text="View Excel"
                        CssClass=" btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
