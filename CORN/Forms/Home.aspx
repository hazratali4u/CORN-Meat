<%@ Page Title="CORN :: Home" Language="C#" MasterPageFile="~/Forms/Test.master"
    AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Forms_Home" %>

<%@ PreviousPageType VirtualPath="~/Login.aspx" %>


<asp:Content ID="cPage" ContentPlaceHolderID="mainCopy" runat="Server">
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
</asp:Content>
