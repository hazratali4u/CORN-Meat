<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeBook.aspx.cs" Inherits="Forms_frmChequeBook" Title="SAMS :: Cheque Book" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../js/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
        function Change(obj, evt) {
            if (evt.type == "focus") {
                obj.style.border = "1px solid black";
            }
            else if (evt.type == "blur") {
                obj.style.border = "0px";
            }
        }
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function ErrorMessage() {
            alert("Can't inactive as its cheques are in use");
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Branch code is required');
                return false;
            }
            str = document.getElementById('<%=txtPrefix.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque no. prefix is required');
                return false;
            }
            str = document.getElementById('<%=txtCheckfrom.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque From Range is required');
                return false;
            }
            str = document.getElementById('<%=txtcheckto.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Cheque To Range is required');
                return false;
            }
            str = document.getElementById('<%=txtIssueDate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Issue date is required');
                return false;
            }
            return true;
        }

    </script>
    <div class="main-contents">
        <div class="col-md-12">
        </div>
        <div class="col-md-12">
            &nbsp;
        </div>
        <asp:Panel ID="pnlMainDivision" runat="server" DefaultButton="btnsearch">
            <div class="container employee-infomation">
                <div class="row top">
                    <div class="col-md-4">
                        <div class="search">
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"
                                TabIndex="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-left: -60px;">
                        <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" runat="server" Text="Search"
                            CssClass="btn btn-success"><i class="fa fa-search"  style="font-size:20px;"></i></asp:LinkButton>
                    </div>
                    <asp:LinkButton ID="btndummy2" runat="server" UseSubmitBehavior="false" />
                    <div class="col-md-offset-5 col-md-3 ">
                        <div class="btnlist pull-right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAddDivision" OnClick="btnAddDivision_Click"
                                        Text="Add">
                                <span class="fa fa-plus-circle"></span>Add</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-success" runat="server" Text="Active" ID="btnActive"
                                        OnClick="btnActive_CLICK" OnClientClick="javasacript:return confirm('Are you sure you want to perform this action?'); return false;"><span class="fa fa-check"></span>Active</asp:LinkButton>
                                    <!-- POP UP MODEL-->
                                    <cc1:ModalPopupExtender ID="mPopupDivision" runat="server" DropShadow="False" PopupControlID="pnlDivision"
                                        TargetControlID="hbtn" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                        CancelControlID="btnCloseDivision">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDivision" runat="server" Style="display: none; margin-left: -40.5px !IMPORTANT; width: 36%">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 100%">
                                                <div class="modal-header">
                                                    <button type="button" id="btnCloseDivision" runat="server" class="close">
                                                        <span>&times;</span><span class="sr-only">Close</span></button>
                                                    <h1 class="modal-title" id="H2">
                                                        <span class="fa fa-1x  fa-globe"></span>Add New Cheque Book</h1>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Bank Account</label>
                                                            <dx:ASPxComboBox ID="drpAccountno" runat="server" CssClass="form-control">
                                                            </dx:ASPxComboBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Branch Code</label>
                                                            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Branch No.</label>
                                                            <asp:TextBox ID="txtBranchNo" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>CP Number</label>
                                                            <asp:TextBox ID="txtCPNumber" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Account Title</label>
                                                            <asp:TextBox ID="txtAccountTitle" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Prefix</label>
                                                            <asp:TextBox ID="txtPrefix" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                                TargetControlID="txtPrefix" ValidChars="QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklxcvbnm" >
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Cheque No. From</label>
                                                            <asp:TextBox ID="txtCheckfrom" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredtxtFaxNumber" runat="server" FilterType="Custom"
                                                                TargetControlID="txtCheckfrom" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Cheque No. To</label>
                                                            <asp:TextBox ID="txtcheckto" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                                TargetControlID="txtcheckto" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>IBAN</label>
                                                            <asp:TextBox ID="txtIBAN" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Issue Date</label>
                                                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                Width="30px" Style="margin-top: 24px;" />
                                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                                TargetControlID="txtIssueDate" OnClientShown="calendarShown">
                                                            </cc1:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Remarks</label>
                                                            <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button ID="btnSaveCountry" runat="server" CssClass="btn btn-success" onblur="Change(this, event)"
                                                        OnClick="btnSaveCountry_CLICK" onfocus="Change(this, event)" Text="Save" />
                                                    <asp:Button ID="btnCancelCountry" OnClick="btnCancelCountry_Click" runat="server" CssClass="btn btn-danger" onblur="Change(this, event)"
                                                        onfocus="Change(this, event)" Text="Cancel" />
                                                    <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="A1"
                                                        runat="server">dummy</a>
                                                    <asp:HiddenField runat="server" ID="hfStatus" Value="0" />
                                                    <div class="row" style="visibility: hidden">
                                                        <div class="col-md-12">
                                                            <input type="checkbox" id="chkIsActive" visible="true" runat="server" checked="checked" />
                                                            Is Active
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row center">
                            <div class="col-md-12">
                                <div class="emp-table">
                                    <asp:GridView ID="Grid_Country" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        OnRowEditing="Grid_Country_RowEditing" OnPageIndexChanging="Grid_Country_PageIndexChanging" PageSize="8" AllowPaging="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CHECK_BOOK_ID" ReadOnly="true" HeaderText="CHECK_BOOK_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BANK_ACCOUNT_ID" ReadOnly="true" HeaderText="BANK_ACCOUNT_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ACCOUNT_DETAIL" ReadOnly="true" HeaderText="Branch Code">
                                                <HeaderStyle Width="25%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHECK_FROM" ReadOnly="true" HeaderText="Cheque From">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHECK_TO" ReadOnly="true" HeaderText="Cheque To">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TIME_STAMP" ReadOnly="true" HeaderText="TIME_STAMP">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" ReadOnly="true" HeaderText="Remarks"></asp:BoundField>
                                            <asp:BoundField DataField="IS_ACTIVE" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="BRANCH_CODE" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BRANCH_NO" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IBAN" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CP_NUMBER" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ACCOUNT_TITLE" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STATUS" ReadOnly="true" HeaderText="Status">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                        CssClass="fa fa-pencil"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
