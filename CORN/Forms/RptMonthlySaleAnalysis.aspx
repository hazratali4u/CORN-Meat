<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptMonthlySaleAnalysis.aspx.cs" Inherits="Forms_RptMonthlySaleAnalysis"
    Title="CORN :: Monthly Sale Report" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script language="javascript" type="text/javascript">
        function onCalendarShown(sender) {

            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }


        function onCalendarShown2() {
            var cal = $find("calendar2");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call2);
                    }
                }
            }
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function onCalendarHidden2() {
            var cal = $find("calendar2");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call2);
                    }
                }
            }
        }

        function call2(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar2");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }


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

        function call3(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar3");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }


        function onCalendarShown4() {
            var cal = $find("calendar4");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call4);
                    }
                }
            }
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function onCalendarHidden4() {
            var cal = $find("calendar4");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call4);
                    }
                }
            }
        }

        function call4(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar4");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }



        function onCalendarShown5() {
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
            sender._popupBehavior._element.style.zIndex = 10005;
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnl_rpt" runat="server">
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="210px" RepeatDirection="Horizontal" Visible="false">
                                    <asp:ListItem Selected="True">Trade Price</asp:ListItem>
                                    <asp:ListItem>Purchase Price</asp:ListItem>
                                </asp:RadioButtonList>
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Report Type</label>
                                <dx:ASPxComboBox ID="DrpReportType" runat="server" CssClass="form-control" SelectedIndex="0">
                                    <Items>
                                    <dx:ListEditItem Text="Gross Sale" Selected="true"/>
                                    <dx:ListEditItem Text="GST" />        
                                    <dx:ListEditItem Text="Discount" />  
                                    <dx:ListEditItem Text="Credit Card" />
                                    <dx:ListEditItem Text="Cash Sales" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </div>
                            </div>
                            <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Value Type</label>
                                <dx:ASPxComboBox ID="DrpUnitType" runat="server" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="DrpUnitType_SelectedIndexChanged" 
                                    SelectedIndex="0">
                                    <Items>
                                    <dx:ListEditItem Value="0" Text="Year Wise" />
                                    <dx:ListEditItem Value="1" Text="Month Wise" />
                                    <dx:ListEditItem Value="2" Text="Date Wise" />
                                        </Items>
                                </dx:ASPxComboBox>
                            </div>
                                </div>
                            <div class="row">
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lbltoLocation" runat="server" Text="Location" Font-Bold="true"></asp:Label>
                                <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                                </dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row" id="divYear" runat="server">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>From Year</label>
                                <asp:TextBox ID="txtStartYear"  runat="server"
                                    Enabled="false" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                 </div>
                            <div class="col-md-1"  style="margin-top:27px">
                                <asp:ImageButton ID="ibtnStartYear" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>To Year</label>
                                <asp:TextBox ID="txtEndYear" onkeyup="BlockEndDateKeyPress()" runat="server"
                                    Enabled="false" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ibtnEndYear" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                            <div class="col-md-4">
                                <sapn  class="fa fa-caret-right rgt_cart" runat="server" Visible="false"></sapn>
                                <asp:Label ID="lblMonth" runat="server" Text="Month" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtMonth" onkeyup="BlockStartDateKeyPress()" Visible="False" runat="server"
                                    Enabled="false" CssClass=" form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="30px" Visible="False" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                        </div>

                        <div class="row" id="divDate" runat="server">
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass=" form-control" MaxLength="11"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ibtnStartDate" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lblEndDate" runat="server" Text="To Date" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass=" form-control" MaxLength="11"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ibtnEndDate" runat="server" Width="30px" Enabled="true" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                        </div>

                        <div class="row" id="divMonth" runat="server">
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lblFromMonth" runat="server" Text="From Month" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtFromMonth" onkeyup="BlockStartDateKeyPress()" runat="server"
                                    Enabled="true" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ibtnStartMonth" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                            <div class="col-md-4">
                                <sapn class="fa fa-caret-right rgt_cart"></sapn>
                                <asp:Label ID="lblToMonth" runat="server" Text="To Month" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtToMonth" onkeyup="BlockStartDateKeyPress()" runat="server"
                                    Enabled="true" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            <div class="col-md-1" style="margin-top:27px">
                                <asp:ImageButton ID="ibtnEndMonth" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            </div>
                        </div>


                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                        <cc1:CalendarExtender ID="CEStartYear" runat="server" BehaviorID="calendar1" OnClientShown="onCalendarShown"
                            OnClientHidden="onCalendarHidden" Format="yyyy" PopupButtonID="ibtnStartYear" PopupPosition="TopLeft"
                            TargetControlID="txtStartYear">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEEndYear" runat="server" BehaviorID="calendar2" OnClientShown="onCalendarShown2"
                            OnClientHidden="onCalendarHidden2" Format="yyyy" PopupButtonID="ibtnEndYear" PopupPosition="TopLeft"
                            TargetControlID="txtEndYear">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEStartMonth" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3"
                            OnClientHidden="onCalendarHidden3" Format="MMM-yyyy" PopupButtonID="ibtnStartMonth" PopupPosition="TopLeft"
                            TargetControlID="txtFromMonth">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CESEndMonth" runat="server" BehaviorID="calendar4" OnClientShown="onCalendarShown4"
                            OnClientHidden="onCalendarHidden4" Format="MMM-yyyy" PopupButtonID="ibtnEndMonth" PopupPosition="TopLeft"
                            TargetControlID="txtToMonth">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="calendar5"
                            OnClientShown="onCalendarShown5" OnClientHidden="onCalendarHidden5" Format="MMM" PopupPosition="TopLeft"
                            PopupButtonID="ImageButton1" TargetControlID="txtMonth">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                            Format="dd-MMM-yyyy"  PopupPosition="TopLeft"
                            PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                            Format="dd-MMM-yyyy"  PopupPosition="TopLeft"
                            PopupButtonID="ibtnEndDate" TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                        <caption>
                            &nbsp;&nbsp;&nbsp;
                        </caption>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            </div>

        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                <asp:Button ID="btnViewPDF" runat="server" OnClick="btnViewPDF_Click" Text="View PDF"  CssClass=" btn btn-success" />
                <asp:Button ID="btnViewExcel" runat="server" OnClick="btnViewExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
            </div>
        </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <span class="fa fa-caret-right rgt_cart" runat="server" visible="false"></span>
                    <asp:Label ID="Label2" runat="server" Text="Location Type" Font-Bold="true" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddDistributorType" runat="server" CssClass="form-control"
                Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged">
            </asp:DropDownList>
                </div>
            </div>
                

            

        </div>
</asp:Content>
