<%@ Page Language="C#" Title="CORN :: Table Wise Sales Report" AutoEventWireup="true"
    MasterPageFile="~/Forms/PageMaster.master"
    CodeFile="RptTableWiseSale.aspx.cs" Inherits="Forms_RptTableWiseSale" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
    <script language="JavaScript" type="text/javascript">

      
        
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }


        function SelectAll() {
            var chkBoxList = document.getElementById('<%= LstTables.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAll.ClientID %>');
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
            var chkBox = document.getElementById('<%= ChbAll.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstTables.ClientID %>');
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
                        <div class="col-md-4">

                            <label><span class="fa fa-caret-right rgt_cart"></span>Report Type</label>
                            <dx:ASPxComboBox ID="RbReportType" runat="server" CssClass="form-control"
                                OnSelectedIndexChanged="RbReportType_SelectedIndexChanged" AutoPostBack="true" SelectedIndex="0">
                                <Items>

                                <dx:ListEditItem Value="1" Selected="True" Text="Summary"></dx:ListEditItem>
                                <dx:ListEditItem Value="2" Text="Detail"></dx:ListEditItem>
                                    
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">

                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div runat="server" id="dvTables" visible="false">

                        <div class="row">
                            <div class="col-md-4">
                                <div class="SingleCheckbox">
                                    <asp:CheckBox ID="ChbAll" runat="server" onclick="SelectAll()" />
                                    <asp:Label ID="Label3" AssociatedControlID="ChbAll" runat="server"
                                        Text="All Tables" CssClass="CheckBoxLabel"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">

                                <div class="SingleCheckbox">
                                    <asp:Panel ID="pnlTables" runat="server" Height="140px" ScrollBars="Vertical" BorderColor="Silver"
                                        BorderWidth="1px" BorderStyle="Groove">

                                        <asp:CheckBoxList ID="LstTables" onclick="UnCheckSelectAll()" runat="server">
                                        </asp:CheckBoxList>

                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>

                            <asp:TextBox ID="txtStartDate" runat="server"
                                CssClass="form-control"></asp:TextBox>

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

                    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                        TargetControlID="txtStartDate" OnClientShown="calendarShown" PopupPosition="TopLeft">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                        TargetControlID="txtEndDate" OnClientShown="calendarShown" PopupPosition="TopLeft">
                    </cc1:CalendarExtender>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="BtnViewPdf" runat="server" OnClick="BtnViewPdf_Click" Text="View PDF" CssClass=" btn btn-success" />
                    <asp:Button ID="btnViewExcel" runat="server" OnClick="btnViewExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
