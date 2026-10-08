<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptItemList.aspx.cs" Inherits="Forms_rptItemList" Title="CORN :: Item List" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div class="main-contents" style="height:250px;">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnl_rpt" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Item Type</label>
                                <dx:ASPxRadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                    <Items>
                                        <dx:ListEditItem Value="0" Text="All" Selected="true"/>
                                        <dx:ListEditItem Value="1" Text="Finished"/>
                                        <dx:ListEditItem Value="2" Text="Raw Material" />
                                        <dx:ListEditItem Value="3" Text="Packing Material" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Catagory</label>
                                <dx:ASPxComboBox ID="DrpCatagory" runat="server" CssClass="form-control">
                                </dx:ASPxComboBox>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-3 ">
                <div class="btnlist pull-right">
                <asp:Button ID="btnViewPDF" runat="server" OnClick="btnViewPDF_Click" Text="View PDF" CssClass=" btn btn-success" />
                <asp:Button ID="btnViewExcel" runat="server" OnClick="btnViewExcel_Click" Text="View Excel" CssClass=" btn btn-success" />
            </div>
                </div>
        </div>

    </div>
</asp:Content>
