<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptEmployeeAttendance.aspx.cs" Inherits="Forms_RptCustomerWiseSale" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function onCalendarShown3() {
            var cal = $find("calendar3");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
            }
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function onCalendarHidden3() {
            var cal = $find("calendar3");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
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
                            <dx:ASPxComboBox ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"
                                AutoPostBack="true" runat="server"
                                CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Department</label>
                            <dx:ASPxComboBox ID="drpDepartment" runat="server" CssClass="form-control"
                                 AutoPostBack="true" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AppendDataBoundItems="true">
                            </dx:ASPxComboBox>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Employee</label>
                            <dx:ASPxComboBox ID="ddlEmployee" runat="server" CssClass="form-control"
                                AutoPostBack="True">
                            </dx:ASPxComboBox>
                            <asp:HiddenField ID="hfAttendanceID" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Month</label>
                            <asp:TextBox ID="txtFromMonth"  runat="server"
                                Enabled="true" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:ImageButton ID="ibtnStartMonth" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                        </div>
                    </div>

                    </div>
                         <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                    <cc1:CalendarExtender ID="CEStartMonth" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3"
                        OnClientHidden="onCalendarHidden3" Format="MMM-yyyy" PopupButtonID="ibtnStartMonth" PopupPosition="TopLeft"
                        TargetControlID="txtFromMonth"></cc1:CalendarExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnViewPDF" runat="server" CssClass=" btn btn-success" OnClick="btnViewPDF_Click" Text="View PDF" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="btn btn-success" Text="View Excel" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>

