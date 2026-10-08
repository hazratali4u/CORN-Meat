<%@ Page Title="CORN :: Add Modifier" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSkuModifier.aspx.cs" Inherits="Forms_SkuModifier" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">

    <div class="main-contents">
        <div class="container employee-infomation">
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
                                    OnClick="btnAdd_Click" >
                               <span class="fa fa-plus-circle"></span>Add
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="hbutton"></asp:LinkButton>
                                <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server" Visible="false"
                                    OnClientClick="javasacript:return confirm('Are you sure you want to perform this action?'); return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                <!-- POP UP MODEL-->
                                <cc1:ModalPopupExtender ID="mPopUpLocation" runat="server" PopupControlID="pnlParameters"
                                    TargetControlID="hbutton" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                    CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlParameters" runat="server" Style="display: none;"  DefaultButton="btnSave">
                                    <div class="modal-dialog" style="width: 100%">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" class="close" runat="server" >
                                                    <span>&times;</span><span class="sr-only">Close</span></button>
                                                <h1 class="modal-title" id="myModalLabel" runat="server">
                                                    <span></span>Sku Modifier</h1>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>SKU</label>
                                                                <dx:ASPxComboBox ID="ddlHasModifier" runat="server" 
                                                                    OnSelectedIndexChanged="ddlHasModifier_SelectedIndexChanged" 
                                                                    AutoPostBack="true" CssClass="form-control"></dx:ASPxComboBox>
                                                                
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>SKU Modifier</label>
                                                                <dx:ASPxComboBox runat="server" ID="ddlIsModifier" CssClass="form-control"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlIsModifier_SelectedIndexChanged">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Stock Unit</label>
                                                                <dx:ASPxComboBox ID="ddlStockUnit" runat="server" AutoPostBack="true" 
                                                                    CssClass="form-control"></dx:ASPxComboBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Default Quantity</label>
                                                                <asp:TextBox ID="txtDefaultQty" runat="server" CssClass="form-control "></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="cbIsManadatory" Visible="false" runat="server" AutoPostBack="true"
                                                                     OnCheckedChanged="cbIsManadatory_CheckedChanged" Text=" Is Manadatory" />

                                                            </div>
                                                            <div class="col-md-8" align="right">
                                                                <asp:Button ID="btnAddGrid" runat="server" Text="Add Grid" OnClick="btnAddGrid_Click" CssClass="btn btn-success" />                                                         
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf" AutoGenerateColumns="False"                                           
                                                                    PageSize="8">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="IsModifierID">
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="StockUnitID">
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ModifierSKU_ID" HeaderText="Modifier SKU ID"></asp:BoundField>
                                                                        <asp:BoundField DataField="intStockMUnitCode" HeaderText="Stock Unit"></asp:BoundField>
                                                                        <asp:BoundField DataField="Default_Qty" HeaderText="Default Qty" ></asp:BoundField>
                                                                        <asp:BoundField DataField="IS_Manadatory" HeaderText="IS Manadatory" >
                                                                            <ItemStyle CssClass="HidePanel" />
                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="cf head"></HeaderStyle>
                                                                    <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:HiddenField ID="hfModifierCode" runat="server" Value="0" />
                                                            </div>
                                                            <div class="col-md-8" align="right">
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success" />
                                                                 <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 90px">
                &nbsp;<asp:Panel ID="Panel21" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="row center">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <asp:GridView ID="GrdModifier" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    AllowPaging="true" AutoGenerateColumns="False" OnRowEditing="GrdModifier_RowEditing" OnPageIndexChanging="GrdModifier_PageIndexChanging1"
                                    EmptyDataText="No Record exist" 
                                    PageSize="8">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%"  CssClass="HidePanel" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center"  CssClass="HidePanel" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="lngSKUModifierCode" HeaderText="Modifier Code" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_ID"  ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Has_Modifier" HeaderText="SKU" ReadOnly="true">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ModifierSKU_ID"  ReadOnly="true"> 
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />                                       
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Is_Modifier" HeaderText="SKU Modifier" ReadOnly="true">                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="intStockMUnitCode"  ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Stock_Unit" HeaderText="Stock Unit" ReadOnly="true">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Default_Qty" HeaderText="Default Qty" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="IS_Manadatory" HeaderText="IS Manadatory" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil" CommandName="Edit" ToolTip="Edit">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head"></HeaderStyle>
                                    <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

