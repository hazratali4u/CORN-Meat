<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmLedgerViewData.aspx.cs" Inherits="Forms_frmLedgerViewData" Title="CORN :: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }

            headerCheckBox.checked = checked;
        }

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <div class="row">
                <div class="col-md-3">
                    <span class=" fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="lbltoLocation" runat="server" Text="Location" Font-Bold="true" />
                    <asp:DropDownList ID="drpDistributor" runat="server" CssClass=" form-control"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Type</label>
                    <asp:DropDownList runat="server" ID="DrpVoucherType" AutoPostBack="True"
                        OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <span id="spanlblSubType" runat="server" class="fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="lblSubType" runat="server" Text="Voucher Sub Type" Font-Bold="true" />
                    <asp:DropDownList runat="server" ID="DrpPrincipal" Visible="false" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="DrpVoucherSubType" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <span id="span1" runat="server" class="fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="Label2" runat="server" Text="User Name" Font-Bold="true" />
                    <asp:DropDownList runat="server" ID="DrpUser" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <span id="spanlblChequeNo" runat="server" class="fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="lblChequeNo" runat="server" Text="From Date" Font-Bold="true" />
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" MaxLength="10" onkeyup="BlockStartDateKeyPress()"></asp:TextBox>
                </div>
                <div class="col-md-1" style="margin-top: 22px">
                    <asp:ImageButton ID="ibtnStartDate" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                        TargetControlID="txtStartDate" OnClientShown="calendarShown">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-md-2">
                    <span id="span2" runat="server" class="fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="Label3" runat="server" Text="To Date" Font-Bold="true" />
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" MaxLength="10" onkeyup="BlockEndDateKeyPress()"></asp:TextBox>
                </div>
                <div class="col-md-1" style="margin-top: 22px">
                    <asp:ImageButton ID="ibnEndDate" runat="server" Width="30px" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                        TargetControlID="txtEndDate" OnClientShown="calendarShown">
                    </cc1:CalendarExtender>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <span id="span3" runat="server" class="fa fa-caret-right rgt_cart" visible="false"></span>
                    <asp:Label ID="Label4" runat="server" Visible="false" Text="Status " />
                    <asp:RadioButtonList ID="RbdList" runat="server" Width="150px" Visible="false" AutoPostBack="True"
                        OnSelectedIndexChanged="RbdList_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="FALSE">Un Post Voucher</asp:ListItem>
                        <asp:ListItem Value="TRUE">Post Voucher</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-md-offset-1 col-md-6 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnView" OnClick="btnView_Click" runat="server" Text="View" CssClass="btn btn-success" />
                    <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Cancel Voucher" CssClass="btn btn-danger" />
                    <asp:Button ID="btnPrintVoucher" OnClick="btnPrintVoucher_Click" runat="server" Text="Print Voucher" CssClass="btn btn-success" />
                </div>
            </div>
        </div>

        <div class="row center">
            <div class="col-md-12">
                <div class="emp-table">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto" Width="70%" Style="margin-top: 10px; margin-left:10px">
                        <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" Width="752px"
                            CssClass="able table-striped table-bordered table-hover table-condensed cf"
                            OnRowEditing="GrdLedger_RowEditing" OnRowDeleting="GrdLedger_RowDeleting">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="ChbSelectHead" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChbSelect" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Right"/>
                                </asp:TemplateField>

                                <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VOUCHER_TYPE_ID" HeaderText="VOUCHER_TYPE_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VOUCHER_DATE" HeaderText="VOUCHER_DATE" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="CHEQUE_NO" HeaderText="CHEQUE_NO" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="fa fa-print" CommandName="Edit" ToolTip="Print">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Edit" CssClass="fa fa-pencil" OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;">                                          
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                            <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>

    </div>
</asp:Content>