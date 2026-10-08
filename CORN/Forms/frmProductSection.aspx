<%@ Page Title="CORN :: Add Item Section" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmProductSection.aspx.cs" Inherits="Forms_frmProductSection" %>

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

            str = document.getElementById('<%=txtSectionName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Section name is required');
                return false;
            }
            str = document.getElementById('<%=txtPrinterName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Printer name is required');
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
            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 100px">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif"/>

                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
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
                                                            <span></span>Add New Item Section</h1>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <div class="modal-body">

                                                                <div class="row">
                                                                    <div class="col-md-2" style="display: none">
                                                                        <caption>
                                                                            <label>
                                                                                <span class="fa fa-caret-right rgt_cart"></span>Section Code
                                                                            </label>
                                                                            <asp:TextBox ID="txtCodeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </caption>
                                                                    </div>

                                                                    <div class="col-md-10">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Section Name</label>
                                                                        <asp:TextBox ID="txtSectionName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-10">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Printer Name</label>
                                                                        <asp:TextBox ID="txtPrinterName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2" style="margin-top: 35px">
                                                                        <asp:CheckBox ID="chkIsPrint" runat="server" Checked="True" Font-Bold="true"></asp:CheckBox>
                                                                        <label>Is Print</label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                    </div>
                                                                    <div class="col-md-5" align="right">
                                                                        <asp:HiddenField ID="hfStatus" runat="server" Value="Active" />
                                                                        <asp:HiddenField ID="hfSectionId" runat="server" Value="0" />
                                                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
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

                </ContentTemplate>
            </asp:UpdatePanel>

            
            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanelDetail" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="GrdSection" AllowPaging="true" runat="server"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    AutoGenerateColumns="False" OnPageIndexChanging="GrdSection_PageIndexChanging" PageSize="8"
                                    OnRowEditing="GrdSection_RowEditing" EmptyDataText="No Record exist">
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
                                        <asp:BoundField DataField="SECTION_ID" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="SECTION_NAME" HeaderText="Section Name" ReadOnly="true">
                                            <ItemStyle Width="35%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRINTER_NAME" HeaderText="Printer Name" ReadOnly="true">
                                            <ItemStyle Width="40%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_PRINT" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head" HorizontalAlign="Center"></HeaderStyle>
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

