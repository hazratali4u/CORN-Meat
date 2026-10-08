<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmShiftOpening.aspx.cs" Inherits="Forms_frmShiftOpening" Title="CORN :: Shift Opening" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
        function ErrorMessages() {
            alert('Shift is closed.\n You can enter shift opening after day close');
        }
        function ValidatePassword() {

            var str;
            str = document.getElementById('<%=ddDistributorId.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select location');
                return false;
            }
            str = document.getElementById('<%=ddUser.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select cashier');
                return false;
            }
            strCurrentPassword = document.getElementById('<%=txtAmount.ClientID%>').value;
            if (strCurrentPassword == null || strCurrentPassword.length == 0) {
                alert('Amount is required');
                return false;
            }

            return true;
        }

    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Width="312px" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Base Location</label>
                                    
                                    <dx:ASPxComboBox ID="ddDistributorId" runat="server" CssClass="form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged"></dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>User</label>
                                   
                                    
                                    <dx:ASPxComboBox ID="ddUser" runat="server" CssClass="form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddUser_SelectedIndexChanged"></dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label id="lbltoLocation"><span class="fa fa-caret-right rgt_cart"></span>Amount</label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                        ValidChars="0123456789." TargetControlID="txtAmount">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label id="lblremarks"><span class="fa fa-caret-right rgt_cart"></span>Remarks</label>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-offset-1 col-md-6 ">
                                    <div class="btnlist pull-right">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="emp-table">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdChannelData" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                            AllowPaging="true" PageSize="7" OnPageIndexChanging="grdChannelData_PageIndexChanging"
                                            AutoGenerateColumns="False" OnRowEditing="grdChannelData_RowEditing" OnRowDeleting="grdChannelData_RowDeleting">
                                            <Columns>
                                                 <asp:BoundField DataField="OPENIIN_ID" HeaderText="OPENIIN_ID" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_ID" HeaderText="NOTE_ID" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="USER_NAME" HeaderText="Cashier" ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ReadOnly="true">
                                                    <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" class="fa fa-pencil">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');return false;"
                                                            ToolTip="Delete">            
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="hiddenPassword" runat="server" Visible="False" Width="97px"></asp:TextBox>
        </div>
    </div>
</asp:Content>
