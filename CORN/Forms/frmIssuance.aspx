<%@ Page Title="CORN :: Stock Issuance" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmIssuance.aspx.cs" Inherits="Forms_frmIssuance" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">


    <script language="JavaScript" type="text/javascript">


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            return true;
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

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hfId" />
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transaction Type</label>

                            <dx:ASPxComboBox ID="DrpDocumentType" runat="server" CssClass="form-control"
                                AutoPostBack="true" SelectedIndex="0"
                                OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                <Items>
                                    <dx:ListEditItem Value="13" Text="Issuance" />
                                    <dx:ListEditItem Value="14" Text="Return" />
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">

                            <label><span class="fa fa-caret-right rgt_cart"></span>Document No</label>
                            

                            <dx:ASPxComboBox ID="drpDocumentNo" runat="server" CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">

                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>

                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </dx:ASPxComboBox>

                        </div>
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Section</label>
                            
                            <dx:ASPxComboBox ID="drpSection" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Remarks</label>
                            <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="form-control"
                                MaxLength="250"></asp:TextBox>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <span class="fa fa-caret-right rgt_cart"></span>

                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Item Description"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="fa fa-caret-right rgt_cart"></span>

                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Quantity"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">

                                        <dx:ASPxComboBox ID="ddlSkus" runat="server" CssClass="form-control">
                                        </dx:ASPxComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                            onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server"
                                            Text="Add" CssClass="btn btn-success" />
                                    </div>
                                </div>
                                <asp:Panel ID="Panel2" runat="server" Width="615px" Height="160px" ScrollBars="Vertical"
                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                        ShowHeader="False" Width="100%" OnRowDataBound="GrdPurchase_RowDataBound" ShowFooter="true">

                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_CODE" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FREE_SKU" DataFormatString="{0:N}" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM_ID" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        CssClass=" fa fa-trash-o"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>


                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4" style="margin-top: 15px">
                    <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server"
                        Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                        CssClass="btn btn-success" />
                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px"
                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="btn btn-success" />
                </div>
                <div class="col-md-2" style="display: none">
                    <strong>Total Quantity</strong>
                    <asp:TextBox ID="txtTotalQuantity" runat="server" CssClass="form-control"
                        Enabled="true"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

