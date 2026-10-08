<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRoleManagement.aspx.cs" Inherits="frmRoleManagement" Title="CORN :: Role Management" %>

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
       </script>

   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btndummy">
                <div class="main-contents">
                    <div class="container role_management">
                        <div class="row top">
                            <div class="col-md-4">
                                <label>
                                    <span class="fa fa-caret-right rgt_cart"></span>Role Description</label>
                               
                                <dx:ASPxComboBox  ID="ddRole" runat="server" CssClass="form-control" 
                                    OnSelectedIndexChanged="ddRole_SelectedIndexChanged"
                                    AutoPostBack="True"></dx:ASPxComboBox>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" CssClass="form-control"
                                    Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Button class="btn btn-success" OnClick="btnNew_Click" ID="btnNew" runat="server" TabIndex="1" Text="New"></asp:Button>
                            </div>
                            <div class="col-md-4">
                                 <asp:Literal ID="lblmsg" runat="server" Visible="False" ></asp:Literal>    
                                </div>
                        </div>
                        <div class="row center">
                            <div class="col-md-3">
                                <label>
                                    <span class="fa fa-caret-right rgt_cart"></span>Module First Layer</label>
                                
                                <dx:ASPxComboBox  ID="DrpModule1stLayer" runat="server" CssClass="form-control" 
                                    OnSelectedIndexChanged="DrpModule1stLayer_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="2"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    <span class="fa fa-caret-right rgt_cart"></span>Module Second Layer</label>
                                
                                <dx:ASPxComboBox  ID="DrpModule2ndLayer" runat="server" CssClass="form-control" 
                                    OnSelectedIndexChanged="DrpModule2ndLayer_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="3"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    <span class="fa fa-caret-right rgt_cart"></span>Module Third Layer</label>
                                
                                <dx:ASPxComboBox  ID="DrpModule3rdLayer" runat="server" CssClass="form-control" 
                                    OnSelectedIndexChanged="DrpModule3rdLayer_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="4">

                                </dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row bottom1">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>Module Assignment</label>
                        </div>
                        <div class="row bottom">
                            <div class="module_contents">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstUnAssignModule" runat="server" CssClass="select" TabIndex="5" ></asp:ListBox>
                                </div>
                                <div class="col-md-2">
                                    <div class="navs navslist" style="padding-top:60px; padding-left:50px">
                                        <div class="nxt arrow">
                                            <a href="#"><span>
                                                <asp:LinkButton ID="btnAssign" runat="server" OnClick="btnAssign_Click" CssClass="fa fa-angle-right" TabIndex="6"/>
                                            </span></a>
                                        </div>
                                        <div class="nxt arrow">
                                            <span>
                                                <asp:LinkButton ID="btnAssignAllItem" OnClick="btnAssignAllItem_Click"
                                                    CssClass="fa  fa-angle-double-right" runat="server" /></span>
                                        </div>
                                        <div class="nxt arrow">
                                            <span>
                                                <asp:LinkButton ID="btnUnAssignAllItem" CssClass="fa  fa-angle-double-left"
                                                    OnClick="btnUnAssignAllItem_Click" runat="server" /></span>
                                        </div>
                                        <div class="nxt arrow">
                                            <a href="#"><span>
                                                <asp:LinkButton ID="btnUnAssign" runat="server" OnClick="btnUnAssign_Click" CssClass="fa fa-angle-left" TabIndex="7"/>
                                            </span></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstAssignModule" CssClass="select" runat="server" TabIndex="8"></asp:ListBox>
                                </div>
                            </div>
                            <div class="btnlist pull-right">
                                <asp:Button class="btn btn-success" OnClick="btnReport_Click" Text="View Report"
                                    runat="server" ID="btnReport" TabIndex="9"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:LinkButton ID="btndummy" runat="server" UseSubmitBehavior="false" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
