<%@ Page Title="CORN :: Add Tables" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTableDefination.aspx.cs" Inherits="Forms_frmTableDefination" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=ddlDistributor.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Location is not selected.');
                document.getElementById('<%=txtTableNo.ClientID%>').focus();
                return false;
            }
            str = document.getElementById('<%=txtTableNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Table name is required');
                document.getElementById('<%=txtTableNo.ClientID%>').focus();
                return false;
            }
            str = document.getElementById('<%=txtDescription.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Description is required');
                document.getElementById('<%=txtDescription.ClientID%>').focus();
                return false;
            }
            return true;
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:HiddenField ID="hfStatus" runat="server" />
                    <asp:HiddenField ID="hf_tbldefID" runat="server" />
                    <div class="row top">
                        <asp:Panel ID="Panel4" runat="server" DefaultButton="btnsearch">
                            <div class="col-md-4">
                                <div class="search">
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"
                                        TabIndex="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="margin-left: -60px;">
                                <asp:LinkButton ID="btnsearch" OnClick="btnFilter_Click" runat="server" Text="Search"
                                    CssClass="btn btn-success"><i class="fa fa-search"  style="font-size:20px;"></i></asp:LinkButton>
                            </div>
                            <asp:LinkButton ID="btndummy" runat="server" UseSubmitBehavior="false" />
                        </asp:Panel>
                        <div class="col-md-offset-5 col-md-3 ">
                            <div class="btnlist pull-right">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                            OnClick="btnAdd_Click">
                                           <span class="fa fa-plus-circle"></span>Add
                                        </asp:LinkButton>
                                        <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server"
                                            OnClientClick="javasacript:return confirm('Are you sure you want to perform this action?'); return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                        <!-- POP UP MODEL-->
                                        <cc1:ModalPopupExtender ID="mPopUpSection" runat="server" PopupControlID="pnlParameters"
                                            TargetControlID="btnAdd" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                            CancelControlID="btnClose">
                                        </cc1:ModalPopupExtender>
                                        <asp:Panel ID="pnlParameters" runat="server" Style="display: none;" ScrollBars="Auto" DefaultButton="btnSave">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" id="btnClose" class="close" runat="server" onserverclick="btnClose_Click">
                                                            <span>&times;</span><span class="sr-only">Close</span></button>
                                                        <h1 class="modal-title" id="myModalLabel">
                                                            <span></span>Add New Table</h1>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                                                        <dx:ASPxComboBox runat="server" ID="ddlDistributor" CssClass="form-control">
                                                                        </dx:ASPxComboBox>
                                                                        
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Table Name</label>
                                                                        <asp:TextBox ID="txtTableNo" runat="server" CssClass="form-control "></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Description</label>
                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control "></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-offset-8 col-md-4 ">
                                                                        <div class="btnlist pull-right">
                                                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanelDetail" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdTable" AllowPaging="true" runat="server"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    OnRowEditing="GrdTable_RowEditing" OnPageIndexChanging="GrdTable_PageIndexChanging" PageSize="8"
                                    AutoGenerateColumns="False" EmptyDataText="No Record exist">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>

                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TableDefination_ID" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location" ReadOnly="true">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TableDefination_No" HeaderText="Table Name" ReadOnly="true">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TableDefination_Description" HeaderText="Description" ReadOnly="true">
                                            <ItemStyle Width="27%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>

                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit" Width="5px">
                                                </asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:BoundField DataField="distributorId" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>

                                    </Columns>
                                    <HeaderStyle CssClass="cf head"></HeaderStyle>
                                    <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                </asp:GridView>



                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
