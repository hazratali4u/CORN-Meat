<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAccountHead.aspx.cs" Inherits="Forms_frmAccountHead" Title="CORN: Chart of Account" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnAccountType.ClientID%>').disabled = true;
            document.getElementById('<%=btnAccountSubType.ClientID%>').disabled = true;
            document.getElementById('<%=btnAccountDetailType.ClientID%>').disabled = true;
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnAccountType.ClientID%>').disabled = false;
            document.getElementById('<%=btnAccountSubType.ClientID%>').disabled = false;
            document.getElementById('<%=btnAccountDetailType.ClientID%>').disabled = false;
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
        }

    </script>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="400px"
        Width="65%">
        <cc1:TabPanel ID="TabPanel1" runat="server" CssClass="accordion__content">
            <HeaderTemplate>
                Main Type
            </HeaderTemplate>
            <ContentTemplate>
                <div class="main-contents">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-5">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Account Category</label>
                                    <dx:ASPxComboBox ID="DrpAccountCategory" runat="server" CssClass="form-control"
                                        AutoPostBack="True" OnSelectedIndexChanged="DrpAccountCategory_SelectedIndexChanged" SelectedIndex="0">
                                        <Items>
                                            <dx:ListEditItem Value="B" Text="Balance Sheet Account"></dx:ListEditItem>
                                            <dx:ListEditItem Value="I" Text="Income Statment Account"></dx:ListEditItem>

                                        </Items>
                                    </dx:ASPxComboBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Main Account Type</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAtypeCode" runat="server" CssClass="form-control" MaxLength="2"
                                        ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAtypeName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnAccountType" OnClick="btnAccountType_Click" runat="server" Text="New Account Type"
                                        CssClass="btn btn-success" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMain" runat="server" ScrollBars="Vertical" Width="100%" Height="170">
                                        <asp:GridView ID="GrdMainType" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                            OnRowDeleting="GrdMainType_RowDeleting" AutoGenerateColumns="False"
                                            OnRowEditing="GrdMainType_RowEditing"   style="height:50px;">
                                            <Columns>
                                                <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Account_Code" HeaderText="Account Code" ReadOnly="true"></asp:BoundField>
                                                <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true"></asp:BoundField>
                                                <asp:BoundField DataField="ACCOUNT_CATEGORY" HeaderText="ACCOUNT_CATEGORY" ReadOnly="true">
                                                    <FooterStyle CssClass="HidePanel" />
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                            CssClass="fa fa-pencil"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" HorizontalAlign="Center" runat="server" CommandName="Delete"
                                                            class="fa fa-trash-o"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="cf head" />
                                        </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" CssClass="accordion__content">
            <HeaderTemplate>
                Sub Type
            </HeaderTemplate>
            <ContentTemplate>
                <div class="main-contents">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-5">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Main Account Type</label>
                                    <dx:ASPxComboBox ID="ddAccountType1" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="ddAccountType1_SelectedIndexChanged" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Sub Account Type</label>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtASubTypeCode" runat="server" CssClass="form-control"
                                        ReadOnly="True" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSubTypeName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnAccountSubType" OnClick="btnAccountSubType_Click" runat="server"
                                        Text="New Sub Type" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSub" runat="server" ScrollBars="Vertical" Width="100%" Height="170">
                                        <asp:GridView ID="GrdSubType" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                            OnRowEditing="GrdSubType_RowEditing" AutoGenerateColumns="False"
                                            OnRowDeleting="GrdSubType_RowDeleting">
                                            <Columns>
                                                <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Account_Code" HeaderText="Account Code" ReadOnly="true"></asp:BoundField>
                                                <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true"></asp:BoundField>
                                                <asp:BoundField DataField="ACCOUNT_CATEGORY" HeaderText="ACCOUNT_CATEGORY" ReadOnly="true">
                                                    <FooterStyle CssClass="HidePanel" />
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                            CssClass="fa fa-pencil"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" HorizontalAlign="Center" runat="server" CommandName="Delete"
                                                            class="fa fa-trash-o"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="cf head" />
                                        </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" CssClass="accordion__content">
            <HeaderTemplate>
                Detail Type
            </HeaderTemplate>
            <ContentTemplate>
                <div class="main-contents">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-5">

                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Main Account Type</label>
                                    <dx:ASPxComboBox ID="ddAccountType2" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddAccountType2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                                <div class="col-md-5">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Sub Account Type</label>
                                    <dx:ASPxComboBox ID="ddAccountSubType1" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="ddAccountSubType1_SelectedIndexChanged" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-4">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Detail Account Type</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtADetailTypeCode" runat="server" CssClass="form-control" ReadOnly="True"
                                        MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDetailTypeName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnAccountDetailType" OnClick="btnAccountDetailType_Click" runat="server"
                                        Text="New Detail Type" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <asp:Panel ID="Panel4" runat="server" Height="150px" ScrollBars="Vertical" Width="100%">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GrdDetailType" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                OnRowEditing="GrdDetailType_RowEditing" AutoGenerateColumns="False"
                                                HorizontalAlign="Center" OnRowDeleting="GrdDetailType_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Account_Code" HeaderText="Account Code" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="ACCOUNT_CATEGORY" HeaderText="ACCOUNT_CATEGORY" ReadOnly="true">
                                                        <FooterStyle CssClass="HidePanel" />
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                                CssClass="fa fa-pencil"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" HorizontalAlign="Center" runat="server" CommandName="Delete"
                                                                class="fa fa-trash-o"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="cf head" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server">
            <HeaderTemplate>
                Account Head
            </HeaderTemplate>
            <ContentTemplate>
                <div class="main-contents">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-5">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Main Account Type</label>
                                    <dx:ASPxComboBox ID="ddAccountType3" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddAccountType3_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Sub Account Type</label>
                                    <dx:ASPxComboBox ID="ddAccountSubType2" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddAccountSubType2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Detail Account Type</label>
                                    <dx:ASPxComboBox ID="drpAccountTypeDetail" runat="server" CssClass="form-control"
                                        OnSelectedIndexChanged="drpAccountTypeDetail_SelectedIndexChanged" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Account Head</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAccountCode" runat="server" CssClass="form-control" ReadOnly="True"
                                        MaxLength="4"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAccountHead" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"
                                        Text="New" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <asp:Panel ID="Panel12" runat="server" Height="150px" ScrollBars="Vertical" Width="100%">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridAccountHead" runat="server" Width="100%" OnRowEditing="GridAccountHead_RowEditing"
                                                AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                OnRowDeleting="GridAccountHead_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Account_Code" HeaderText="Account Code" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ReadOnly="true"></asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                                CssClass="fa fa-pencil"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" HorizontalAlign="Center" runat="server" CommandName="Delete"
                                                                class="fa fa-trash-o"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="cf head"></HeaderStyle>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
