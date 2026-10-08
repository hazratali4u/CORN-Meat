<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSkipCheque.aspx.cs" Inherits="Forms_frmSkipCheque" Title="CORN :: Cancel Cheque" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
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

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=drpBankAccount.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select bank account');
                return false;
            }
            str = document.getElementById('<%=drpCheque.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select cheque no.');
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
                                        OnClientClick="javasacript:return confirm('Are you sure you want to perform this action?'); return false;"
                                        OnClick="btnActive_CLICK"><span class="fa fa-check"></span>Active</asp:LinkButton>
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
                                                        <span class="fa fa-1x  fa-globe"></span>Cancel Cheque</h1>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Bank Account</label>
                                                            <dx:ASPxComboBox ID="drpBankAccount" OnSelectedIndexChanged="drpBankAccount_SelectedIndexChanged" 
                                                                AutoPostBack="true" runat="server" CssClass="form-control">
                                                            </dx:ASPxComboBox>
                                                           
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Cheque No.</label>
                                                            <dx:ASPxComboBox ID="drpCheque" runat="server" CssClass="form-control">
                                                            </dx:ASPxComboBox>
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
                                            <asp:BoundField DataField="CHEQUE_ID" ReadOnly="true" HeaderText="CHEQUE_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHEQUE_NO" ReadOnly="true" HeaderText="Cheque No">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BANK_ACCOUNT_ID" ReadOnly="true" HeaderText="BANK_ACCOUNT_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" ReadOnly="true" HeaderText="Remarks"></asp:BoundField>
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
