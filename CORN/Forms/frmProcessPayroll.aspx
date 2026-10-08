<%@ Page Title="CORN :: Process Payroll" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmProcessPayroll.aspx.cs" Inherits="Forms_frmProcessPayroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="javascript" type="text/javascript">
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



        function pageLoad() {
            checkAllChecked();
            $("tbody.alternateRow tr:visible").each(function (index) {
                var $this = $(this);
                if (index & 1) {
                    $this.addClass("odd");
                } else {
                    $this.removeClass("odd");
                }
            });
            $("#text").keyup(function (e) {
                $("#table1 tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#text").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#table1 tr:has(td)").show();
                    return false;
                }
                $("#table1 tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });
        }

        $(document).ready(function () {
        });
        function checkAll(CheckBox) {
            var GridVwHeaderChckbox = document.getElementById("tblEmployee");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = CheckBox.checked;
            }
        }
        function checkAllChecked() {
            var GridVwHeaderChckbox = document.getElementById("tblEmployee");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                if (GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == false) {
                    GridVwHeaderChckbox.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                    break;
                }
                else {
                    GridVwHeaderChckbox.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked = true;
                }
            }
        }
    </script>
    <div id="right_data">
        <div>
        </div>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 90px;">
                                        <strong>Location</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Department</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddDesignation" runat="server" Width="205px" AutoPostBack="True"
                                            CssClass="DropList" OnSelectedIndexChanged="ddDesignation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Month</strong>
                                    </td>
                                    <td>
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
                                        <asp:TextBox ID="txtSalaryMonth" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                        <asp:ImageButton ID="ibExpDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="30px" />
                                        <ajaxToolkit:CalendarExtender ID="ceExpDate" BehaviorID="calendar5" runat="server"
                                            Format="MMM-yyyy" PopupButtonID="ibExpDate" TargetControlID="txtSalaryMonth"
                                            OnClientShown="onCalendarShown5" OnClientHidden="onCalendarHidden5">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 914px">
                                        <asp:Label ID="ltrAdd" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:Repeater ID="rProcessSalary" runat="server" OnItemCommand="rProcessSalary_ItemCommand">
                                            <HeaderTemplate>
                                                <table width="100%" id="tblEmployee" border="0" cellspacing="0">
                                                    <tr>
                                                        <td class="tblhead" style="width: 2px">
                                                            <asp:CheckBox ID="cbAllEmployee" runat="server" onclick="checkAll(this);"></asp:CheckBox>
                                                        </td>
                                                        <td class="tblhead" style="width: 20px;">
                                                            <asp:Label ID="lblExployee" runat="server" ForeColor="White" Font-Bold="True" Text="Employee Name"
                                                                BackColor="#006699"></asp:Label>
                                                        </td>
                                                        <td class="tblhead" style="width: 20px;">
                                                            <asp:Label ID="lblDesignation" runat="server" ForeColor="White" Font-Bold="True"
                                                                Text="Designation" BackColor="#006699"></asp:Label>
                                                        </td>
                                                        <td class="tblhead" style="width: 25px;">
                                                            <asp:Label ID="lblBasicSalary" runat="server" ForeColor="White" Font-Bold="True"
                                                                Text="Basic Salary" BackColor="#006699"></asp:Label>
                                                        </td>
                                                        <td class="tblhead" style="width: 15px;">
                                                            <asp:Label ID="lblAllowances" runat="server" ForeColor="White" Font-Bold="True" Text="Allowances"
                                                                BackColor="#006699"></asp:Label>
                                                        </td>
                                                        <td class="tblhead" style="width: 30px;">
                                                            <asp:Label ID="lblDeduction" runat="server" ForeColor="White" Font-Bold="True" Text="Deductions"
                                                                BackColor="#006699"></asp:Label>
                                                        </td>
                                                        <td class="tblhead" style="width: 10px;">
                                                            <asp:Label ID="lblNetAmount" runat="server" ForeColor="White" Font-Bold="True" Text="Net Amount"
                                                                BackColor="#006699"></asp:Label>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background: #EFF3FB;">
                                                    <td>
                                                        <asp:CheckBox ID="chbSelect" runat="server" CommandArgument='<%# Eval("SALARY_ID") %>'
                                                            ToolTip="Close" Text="" onclick="checkAllChecked()"></asp:CheckBox>
                                                        <asp:HiddenField ID="hfSALARY_ID" runat="server" Value='<%# Eval("SALARY_ID")%>' />
                                                        <asp:HiddenField ID="hfCUSTOMER_ID" runat="server" Value='<%# Eval("CUSTOMER_ID")%>' />
                                                    </td>
                                                    <td style="width: 10px;">
                                                        <asp:Label ID="lblCustomer" Text='<%# Eval("CUSTOMER_NAME")%>' runat="server" Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDesignation" Text='<%# Eval("SLASH_DESC")%>' runat="server" Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBasicSalary" Text='<%# Eval("SALARY_AMOUNT")%>' runat="server"
                                                            Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAllowances" Text='<%#Eval("ALLOWANCE_AMOUNT")%>' runat="server"
                                                            Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDeduction" Text='<%# Eval("DEDUCTION_AMOUNT")%>' runat="server"
                                                            Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNetAmount" Text=' <%#Eval("Net_Amount")%>' runat="server" Style="text-align: right"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="56%">
                                                    <asp:LinkButton ID="linkbtnprev" runat="server" Enabled="False" OnClick="linkbtnprev_Click"
                                                        CausesValidation="false" Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                                    &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" Enabled="False" OnClick="linkbtnnext_Click"
                                                        CausesValidation="false" Visible="false" Style="color: #6C5A10;">Next</asp:LinkButton>
                                                </td>
                                                <td align="right" width="44%">
                                                    <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button AccessKey="S" ID="btnSave" autopostback="true" TabIndex="101" runat="server"
                                            Width="90px" Font-Size="8pt" Text="Save" CssClass="Button" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
