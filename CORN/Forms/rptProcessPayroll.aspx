<%@ Page Title="CORN :: Payroll Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptProcessPayroll.aspx.cs" Inherits="Forms_rptProcessPayroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="javascript" type="text/javascript">
        function calendarShown(sender, args) {
            
        }

        function onCalendarShown5(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;

            var cal = $find("calendar5");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call5);
                    }
                }
            }
        }

        function onCalendarHidden5() {
            var cal = $find("calendar5");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call5);
                    }
                }
            }
        }

        function call5(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar5");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

    </script>
        <div class="main-contents">
                <div class="container employee-infomation">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                            <div class="col-md-4">
                        <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                        <asp:DropDownList ID="DrpDistributor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                                </div>
                            <div class="row">
                            <div class="col-md-4">
                        <label><span class="fa fa-caret-right rgt_cart"></span>Department</label>
                        <asp:DropDownList ID="ddDesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddDesignation_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                                </div>
                            <div class="row">
                            <div class="col-md-4">
                        <label><span class="fa fa-caret-right rgt_cart"></span>Employee Name</label>
                        <asp:DropDownList ID="DrpEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                                </div>
                            <div class="row">
                                <div class="col-md-4">
                                 <label><span class="fa fa-caret-right rgt_cart"></span>Month</label>
                                    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
                            <asp:TextBox ID="txtSalaryMonth" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1" style="margin-top:34px">
                                    <asp:ImageButton ID="ibExpDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"/>
                                        <ajaxToolkit:CalendarExtender ID="ceExpDate" BehaviorID="calendar5" runat="server"
                                            Format="MMM-yyyy" PopupButtonID="ibExpDate" TargetControlID="txtSalaryMonth"
                                            OnClientShown="onCalendarShown5" OnClientHidden="onCalendarHidden5" PopupPosition="TopRight">
                                        </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                              <div class="col-md-2">
                               </div>
                            <div class="col-md-2" align="right">
                              <asp:Button ID="btnShow" OnClick="btnShow_Click1" runat="server" Text="Print PDF" AccessKey="S" autopostback="true" CssClass="btn btn-success" />                          
                                      </div>
                              </div>                                             
                        </ContentTemplate>
                    </asp:UpdatePanel>
        </div>
            </div>
</asp:Content>
