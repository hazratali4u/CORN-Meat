<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmExpenseEntry.aspx.cs" Inherits="frmExpenseEntry" Title="CORN :: Petty Expense" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            } else if (str == "0") {
                alert('Amount should be greater than zero');
                return false;
            }
        }

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;

            return true;
        }
    </script>

    <div class="main-contents">
        <div class="container employee-infomation">
            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 83px">
                &nbsp;<asp:Panel ID="Panel21" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                            Wait Update
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddNew">

                        <div class="row">
                            <div class="col-md-3">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Voucher No</label>
                                <dx:ASPxComboBox runat="server" ID="DrpVoucherNo" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="DrpVoucherNo_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                <dx:ASPxComboBox runat="server" ID="drpDistributor" CssClass="form-control"
                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </div>
                            <div class="col-md-3">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Expense Head</label>
                                <dx:ASPxComboBox runat="server" ID="DrpAccountHead" CssClass="form-control">
                                </dx:ASPxComboBox>
                            </div>

                            <asp:Label ID="Label4" Visible="false" runat="server" Text="Closing Bal" />
                            <asp:TextBox ID="txtClosinBal" runat="server" ReadOnly="true" Visible="false" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Remarks</label>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Amount</label>
                                <asp:TextBox ID="txtAmount" runat="server" onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="display: none">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Slip No</label>
                                <asp:TextBox ID="txtslipNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="padding-top: 25px;">
                                <asp:Button AccessKey="S" ID="btnAddNew" OnClick="btnAddNew_Click" runat="server"
                                    Text="Add" CssClass="btn btn-success" />
                            </div>
                        </div>

                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <strong>
                <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row center">
                    <div class="col-md-9">
                        <div class="emp-table">

                            <asp:Panel ID="Panel12" runat="server" Height="180px" ScrollBars="Vertical" Width="100%"
                                BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px">
                                <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing" ShowFooter="true" OnRowDataBound="GrdOrder_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id" ReadOnly="true">
                                            <FooterStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Account_Name" HeaderText="Account Head" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="debit" DataFormatString="{0:0.00000}" FooterText="Total" HeaderText="Amount" ReadOnly="true">

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCOUNT_PARENT_ID" HeaderText="ACCOUNT_PARENT_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    class="fa fa-trash-o"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head" />
                                </asp:GridView>
                            </asp:Panel>
                            <div class="row" style="padding-top: 5px;">
                                <div class="col-md-offset-10 col-md-2">
                                    <div class="btnlist pull-right">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save"
                                            CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="HidePanel">
                        <label><span class="fa fa-caret-right rgt_cart"></span>Total Amount</label>
                        <asp:TextBox ID="txtMainCash" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
