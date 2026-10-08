<%@ Page Language="C#" Title="CORN :: Dashboard" MasterPageFile="~/Forms/PageMaster.master" 
     AutoEventWireup="true" CodeFile="frmDashBoard2.aspx.cs" Inherits="Forms_frmDashBoard2" %>

<%@ Register assembly="DevExpress.Dashboard.v16.1.Web, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
     namespace="DevExpress.DashboardWeb" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">

<%--<asp:UpdatePanel runat="server" id="UpdatePanel1" UpdateMode="Always">
  <contenttemplate>    --%>
        
         <dx:ASPxDashboardViewer ID="dshViewer" runat="server" 
                                onDashboardLoading="dshViewer_DashboardLoading" 
                                OnDataLoading="dshViewer_DataLoading"
                                Width="100%" Height="800px">
        </dx:ASPxDashboardViewer>
    
<%--    </contenttemplate>
</asp:UpdatePanel>--%>

</asp:Content>