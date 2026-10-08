<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Forms/PageMaster.master" CodeFile="frmVoucherEditing.aspx.cs"
    Inherits="Forms_frmVoucherEditing" Title="CORN :: Voucher Editing" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <style>
        fieldset {
            margin: 0px 0px;
            border: 1px solid Silver;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

            fieldset p {
                border: 1px solid #fff;
            }

            fieldset legend {
                position: relative;
                top: -1em;
                font-weight: bold;
                display: block;
                text-align: right;
                padding: 0px 0px;
            }

                fieldset legend a {
                    color: #006699;
                    text-decoration: underline;
                }

                    fieldset legend a:hover {
                        color: #3399cc;
                    }

            fieldset form div.right {
                text-align: right;
            }
    </style>
    <script type="text/javascript" src="../AjaxLibrary/jquery-1.6.1.min.js"></script>
    <script language="JavaScript" type="text/javascript">

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

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtVoucherDate.ClientID %>").value;
            if (str == null || str.length < 9) {
                alert('Must Enter Voucher Date');
                return false;
            }
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }
            else if (parseFloat(Debit) > 0 && parseFloat(Credit) > 0) {
                alert('Enter only Debit or Credit');
                return false;
            }

            return true;
        }
    </script>

    <div class="main-contents">

        <div class="container employee-infomation">
            <div>
                <h3>Voucher Editing
                </h3>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                    <asp:DropDownList ID="drpDistributor" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Date</label>
                    <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher No</label>
                    <asp:TextBox ID="lblVoucherNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Type</label>
                    <asp:DropDownList ID="DrpVoucherType" runat="server" Enabled="False" CssClass="form-control">
                        <asp:ListItem Value="14">Cash Voucher</asp:ListItem>
                        <asp:ListItem Value="15">Bank Voucher</asp:ListItem>
                        <asp:ListItem Value="16">Journal Voucher</asp:ListItem>
                        <asp:ListItem Value="812">Opening Balance Voucher</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <span id="SpanVsubtype" runat="server" class="fa fa-caret-right rgt_cart"></span>
                    <asp:Label ID="lblVouchetSubType" runat="server" Text="Voucher Sub Type" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="DrpVoucherSubType" runat="server" Enabled="False" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <span id="spanPaymentMode" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblPyamentMode" runat="server" Text="Payment Mode" Font-Bold="true"></asp:Label>
                            <asp:DropDownList ID="DrpPaymentMode" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <span id="spanDdlBank" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblBank" runat="server" Text="Bank/Cash Head" Font-Bold="true"></asp:Label>
                            <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <span id="spanPayees" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblPayees" runat="server" Text="Payee's Name" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtpayeesName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <span id="spanRemarks" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblRemarks" runat="server" Text="Narration" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span id="spanlblChequeNo" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span id="spanlblSlipNo" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblSlipNo" runat="server" Text="Slip No" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtSlipNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <span id="spanlblDueDate" runat="server" class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblDueDate" runat="server" Text="Due Date" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" onkeyup="BlockEndDateKeyPress()" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <asp:TextBox ID="txtTotalDebit" runat="server" CssClass="HidePanel" Width="100px"></asp:TextBox>
                    <asp:TextBox ID="txtTotalCredit" runat="server" CssClass="HidePanel" Width="73px"></asp:TextBox>
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
                                    <dx:ASPxComboBox ID="ddlAccountHead" runat="server" CssClass="form-control"  TextFormatString="{0} ({1})" IncrementalFilteringMode="Contains">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="ACCOUNT_NAME" Caption="Account Name" Width="60px" />
                                            <dx:ListBoxColumn FieldName="ACCOUNT_CODE" Caption="Account Code" Width="40px" />
                                        </Columns>

                                    </dx:ASPxComboBox>
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
                            <asp:HiddenField ID="RowId" runat="server" Value="0" />
                            <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing"
                                CssClass="able table-striped table-bordered table-hover table-condensed cf" ShowFooter="true" Width="68%">
                                <Columns>
                                    <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id" ReadOnly="true">
                                        <FooterStyle CssClass="HidePanel" />
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                            Width="230px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Account_Name" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel" />
                                        <ItemStyle CssClass="HidePanel" />
                                        <FooterStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REMARKS" HeaderText="Account Description" ReadOnly="true">
                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                            Width="140px" />
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
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                TargetControlID="txtCreditAmount" ValidChars="-.0123456789">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                TargetControlID="txtDebitAmount" ValidChars="-0123456789.">
                            </cc1:FilteredTextBoxExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                    <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save" CssClass=" btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
