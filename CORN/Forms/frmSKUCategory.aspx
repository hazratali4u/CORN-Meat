<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUCategory.aspx.cs" Inherits="frmSKUCategory" Title="CORN :: Add Category" %>

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
        function ErrorMessage() {
            alert('Level 4 reached.\n You cannot create next child of the selected Parent category');
        }
        function Message(msg) {
            alert(msg);
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCategoryName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Child category name is required');
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
            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
                    <div>
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                                OnClick="btnAdd_Click">
                               <span class="fa fa-plus-circle"></span>Add
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                            <!-- POP UP MODEL-->
                                            <cc1:ModalPopupExtender ID="mPopUpCategory" runat="server" PopupControlID="pnlParameters"
                                                TargetControlID="btnAdd" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                                CancelControlID="btnClose">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="pnlParameters" runat="server" Style="display: none; width: 30%" ScrollBars="Auto" DefaultButton="btnSaveCategory">
                                                <div class="modal-dialog" style="width: 100%">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" id="btnClose" class="close" runat="server" onserverclick="btnClose_Click">
                                                                <span>&times;</span><span class="sr-only">Close</span></button>
                                                            <h1 class="modal-title" id="myModalLabel">
                                                                <span></span>Add New Category</h1>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                            <ContentTemplate>
                                                                <div class="modal-body">
                                                                    <div class="row" style="margin-top: 20px">
                                                                        <div class="col-md-12">
                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Category Type</label>
                                                                            <dx:ASPxComboBox ID="ddlType" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Parent Category</label>
                                                                            <dx:ASPxComboBox ID="drpParentCategory" runat="server" CssClass="form-control">
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Child Category Name </label>

                                                                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div style="display: none">
                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Category Code</label>
                                                                            <asp:TextBox ID="txtCategoryCode" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
                                                                        <div class="col-md-7">
                                                                        </div>
                                                                        <div class="col-md-12" align="right">
                                                                            <asp:HiddenField ID="hfStatus" runat="server" Value="Active" />
                                                                            <asp:HiddenField ID="hfcategoryId" runat="server" Value="0" />
                                                                            <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
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

                        <%--       ///////////////////////////////////////////////////////////////////////////////////////////////////////--%>
                        <div class="row center">
                            <div class="col-md-12">
                                <div class="emp-table">
                                    <asp:GridView ID="GrdCategory" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        HorizontalAlign="Center" AllowPaging="true"
                                        AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing" PageSize="8"
                                        OnPageIndexChanging="GrdCategory_PageIndexChanging" EmptyDataText="No Record exist">
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
                                            <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name" ReadOnly="true">
                                                <ItemStyle Width="53%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TYPE" HeaderText="Type" ReadOnly="true">
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Width="20px" ToolTip="Edit" CssClass="fa fa-pencil">                                            
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PARENT_SKU_HIE_ID" HeaderText="Type" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PARENT_SKU_HIE_TYPE_ID" HeaderText="PARENT_SKU_HIE_TYPE_ID" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                        </Columns>

                                        <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
                <ProgressTemplate>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif"></asp:Image>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>
