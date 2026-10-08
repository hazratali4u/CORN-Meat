<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVoucherEntry.aspx.cs" Inherits="Forms_frmVoucherEntry" Title="CORN :: Voucher Entry" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">

    <script language="JavaScript" type="text/javascript">

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function ErrorMessage(msg) {
            document.getElementById('<%=btnDone.ClientID%>').value = msg;
        }


        function ImportVisibility(ddlVoucherType) {
            if (ddlVoucherType.value == '812') {
                document.getElementById('<%=divImport.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=divImport.ClientID%>').disabled = true;
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

        }

        function AccountHeadSelected(source, eventArgs) {
            document.getElementById("<%=txtAccountDes.ClientID %>").focus();
        }

        function ValidateForm() {
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }


            if (Debit > 0 && Credit > 0) {
                alert('Only Debit or Credit can enter');
                return false;
            }
            return true;
        }

    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <div style="z-index: 5000; left: 700px; width: 100px; position: absolute; top: 10%; height: 100px">
                &nbsp;<asp:Panel ID="Panel1" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="uPImageButton" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                            Wait....
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row" id="divImport" runat="server" visible="false" style="z-index: 5000; left: 73%; position: absolute; top: 68%;">
                        <div class="col-md-12">
                            <div class="btnlist pull-left">
                                <asp:Button ID="btnImport" runat="server" Text="Select File" CssClass=" btn btn-success" />
                                <asp:Button ID="btnUpload" runat="server" Text="Import" OnClick="btnImport_Click" CssClass=" btn btn-success" />
                            </div>
                            <asp:FileUpload ID="FileUpload1" accept=".xls,.xlsx" runat="server" CssClass="hidden" />

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox runat="server" ID="drpDistributor" CssClass="form-control">
                            </dx:ASPxComboBox>

                        </div>
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Type</label>
                            <dx:ASPxComboBox runat="server" ID="DrpVoucherType" AutoPostBack="True"
                                OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-3" id="SubType" runat="server">
                            <span id="spanlblSubType" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblSubType" runat="server" Text="Voucher Sub Type" Font-Bold="true" />
                            <dx:ASPxComboBox runat="server" ID="DrpVoucherSubType" AutoPostBack="True"  NullText="Plz Select Voucher Sub Type"
                                OnSelectedIndexChanged="DrpVoucherSubType_SelectedIndexChanged" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-2">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transaction Date</label>
                            <asp:TextBox ID="txtTransactionDate" runat="server" onkeyup="BlockEndDateKeyPress()" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 30px">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2"
                                TargetControlID="txtTransactionDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <span id="spanlblPaymentMode" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" Font-Bold="true" />
                            <dx:ASPxComboBox runat="server" ID="DrpPaymentMode" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-3">
                            <span id="spanlblBankHead" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblBankHead" runat="server" Text="Bank/Cash Head" Font-Bold="true" />
                            <dx:ASPxComboBox runat="server" ID="drpBanks" OnSelectedIndexChanged="drpBanks_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-2">
                            <span id="spanlblChequeNo" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No" Font-Bold="true" />
                            <dx:ASPxComboBox runat="server" ID="drpChequeNo" CssClass="form-control" Enabled="false">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-1">
                            <span id="spanlblSlipNo" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblSlipNo" runat="server" Text="Slip No" Font-Bold="true" />
                            <asp:TextBox ID="txtSlipNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <span id="spanlblDueDate" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblDueDate" runat="server" Text="Due Date" Font-Bold="true" />
                            <asp:TextBox ID="txtDueDate" runat="server" onkeyup="BlockEndDateKeyPress()" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px">
                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                Width="30px" />
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                TargetControlID="txtDueDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label id="lblpayeesName" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Payee's Name</label>
                            <asp:TextBox ID="txtpayeesName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label id="lblRemarks" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Narration</label>
                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div>
                        <asp:TextBox ID="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox>
                        <asp:TextBox ID="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row center">
            <div class="col-md-12">
                <div class="emp-table">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3">
                                    <span class="fa fa-caret-right rgt_cart"></span>
                                    <asp:Label ID="lblskuCode" runat="server" Font-Bold="True" Text="Account Description"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <span class="fa fa-caret-right rgt_cart"></span>
                                    <asp:Label ID="lblskuname" runat="server" Font-Bold="True" Text="Remarks"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <span class="fa fa-caret-right rgt_cart"></span>
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Debit"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <span class="fa fa-caret-right rgt_cart"></span>
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Credit"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <dx:ASPxComboBox ID="ddlAccountHead" runat="server" CssClass="form-control"></dx:ASPxComboBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAccountDes" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Add New" CssClass="btn btn-success" />
                                </div>
                            </div>

                            <asp:GridView ID="GrdOrder" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                AutoGenerateColumns="False" OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting"
                                OnRowEditing="GrdOrder_RowEditing" Width="68%" ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id" ReadOnly="true">
                                        <FooterStyle CssClass="HidePanel" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="295px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Account_Name" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                        <FooterStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REMARKS" HeaderText="Account Description" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                            Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="debit" DataFormatString="{0:N}" HeaderText="Debit" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                            Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="credit" DataFormatString="{0:N}" HeaderText="Credit" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                            Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Principal_id" HeaderText="Principal" ReadOnly="true">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                CssClass=" fa fa-trash-o"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tblhead" />
                            </asp:GridView>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCreditAmount"
                                FilterType="Custom" ValidChars="-.0123456789"></cc1:FilteredTextBoxExtender>
                            <div style="z-index: 101; left: 706px; width: 100px; position: absolute; top: 364px; height: 100px">
                                <asp:Panel ID="Panel21" runat="server">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </asp:Panel>
                            </div>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebitAmount"
                                FilterType="Custom" ValidChars="-0123456789."></cc1:FilteredTextBoxExtender>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 ">
                <asp:UpdatePanel ID="abc" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-4 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save" CssClass=" btn btn-success" />
                    <asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click" Text="Cancel" CssClass=" btn btn-danger" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>