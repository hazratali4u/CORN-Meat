<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LookUp.ascx.cs" Inherits="UserControl_LookUp" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<link href="../Styles/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="../js/jquery-3.1.1.min.js" type="text/javascript"></script>
<style type="text/css">
    .auto-style1 {
        width: 56px;
    }
    .auto-style2 {
        width: 75px;
    }
    .auto-style3 {
        width: 53px;
    }
    .auto-style4 {
        width: 140px;
    }
</style>
<div style="width: 100%; border: 2px groove #eee; margin-bottom: 4px; border-radius: 4px">
    <table style="margin: 2px; width: 730px;">
        <tr>
            <td>
                <dx:ASPxCallbackPanel ID="ClBckPnlInfoText" runat="server" ClientInstanceName="PnlInfoText"
                    OnCallback="ClBckPnlInfoText_Callback" Width="133px" Height="22px"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False">
                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:TextBox ID="txtInfo" CssClass="" ClientInstanceName="txtInfo" ClientIDMode="Static" Font-Names="Source Sans Pro" MaxLength="25" runat="server" Width="138px" Height="23px" ReadOnly="True" Font-Size="12pt" BackColor="#F9F9F9" BorderStyle="None" />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
                        <td>
                <dx:ASPxCallbackPanel ID="clbkPnlbtnNext" runat="server"
                    ClientInstanceName="PnlbtnNext" OnCallback="clbkPnlbtnNext_Callback"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False">

                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">
                            <dx:ASPxButton ID="btnNext" runat="server" Text=">"
                                CssFilePath="~/App_Themes/Office2010Silver/{0}/styles.css"
                                CssPostfix="Office2010Silver"
                                SpriteCssFilePath="~/App_Themes/Office2010Silver/{0}/sprite.css"
                                OnClick="btnNext_Click1" Height="18px" Theme="Metropolis" Width="20px" BackColor="#E08E0B" ForeColor="White">
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
        <td>
                <dx:ASPxCallbackPanel ID="clbkPnlbtnLast" runat="server"
                    ClientInstanceName="PnlbtnLast" OnCallback="clbkPnlbtnLast_Callback"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False" Width="76px">

                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

                    <PanelCollection>
                        <dx:PanelContent ID="pnlCntBtnLast" runat="server">
                            <dx:ASPxButton ID="btnLast" runat="server" Text=">>"
                                CssFilePath="~/App_Themes/Office2010Silver/{0}/styles.css"
                                CssPostfix="Office2010Silver"
                                SpriteCssFilePath="~/App_Themes/Office2010Silver/{0}/sprite.css"
                                OnClick="btnLast_Click1" Height="20px" Theme="Metropolis" Width="20px" BackColor="#E08E0B" ForeColor="White">
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td class="auto-style2">
                <asp:Label ID="lblExport" runat="server"
                    Text="Export To: " CssClass="selectedtd" />

            </td>
            <td class="auto-style3">
                <dx:ASPxRadioButton ID="rbtnPDF" AutoPostBack="true"
                    runat="server" Text="PDF" Checked="True" GroupName="grpExport"
                    OnCheckedChanged="rbtnPDF_CheckedChanged">
                </dx:ASPxRadioButton>
            </td>
            <td class="auto-style1">
                <dx:ASPxRadioButton ID="rbtnExcel"
                    AutoPostBack="true" runat="server" Text="Excel" GroupName="grpExport">
                </dx:ASPxRadioButton>
            </td>
            <td>
                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server"></dx:ASPxCheckBox>
                <dx:ASPxCheckBox ID="chkGroup" runat="server" Text="Group Data"
                    OnCheckedChanged="chkGroup_CheckedChanged1" AutoPostBack="True" style="margin-left: 41px" Width="108px">
                </dx:ASPxCheckBox>
            </td>
            <td>
                <dx:ASPxCheckBox ID="chkFilter" runat="server" Text="Filter Data"
                    OnCheckedChanged="chkFilter_CheckedChanged" AutoPostBack="True"></dx:ASPxCheckBox>
            </td>
        </tr>

    </table>
</div>
<table style="width: 100%;">

    <tr>
        <td>
            <dx:ASPxGridView ID="grdLookup" StylesPager-PageNumber-Height="520" EnableCallBacks="False" Width="100%" runat="server" SettingsCookies-Enabled="true" EnableRowsCache="true"
                CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css"
                CssPostfix="Office2003Blue" SettingsBehavior-AllowSort="true"
                OnProcessColumnAutoFilter="grdLookup_ProcessColumnAutoFilter"
                OnAfterPerformCallback="grdLookup_AfterPerformCallback"
                OnHtmlDataCellPrepared="grdLookup_HtmlDataCellPrepared"
                SettingsDetail-ExportMode="All" Theme="Office2010Black" ClientInstanceName="grid"
                OnCustomCallback="grdLookup_CustomCallback" Font-Names="Source Sans Pro" Font-Size="10pt" Font-Strikeout="False">
                <%-- <Columns>
                    <dx:GridViewDataTextColumn  Caption="#" VisibleIndex="0">
                        <DataItemTemplate>
                            <dx:ASPxButton ID="cb" runat="server">
                            </dx:ASPxButton>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>--%>
                <ClientSideEvents Init="function(s, e) {}"
                    EndCallback="function(s, e) 
                    {
                 	    PnlInfoText.PerformCallback();
                        PnlInfoText2.PerformCallback();
                 	    PnlbtnNext.PerformCallback();
                 	    PnlbtnNext1.PerformCallback();
                 	    PnlbtnLast.PerformCallback();
                 	    PnlbtnLast2.PerformCallback();
                    }"
                    RowDblClick="function(s, e) {
                        grid.PerformCallback(s.GetFocusedRowIndex());
                     }" />


                <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" />

                <SettingsDetail ExportMode="All"></SettingsDetail>

                <SettingsPager Visible="False" Mode="ShowAllRecords" PageSize="50">
                    <AllButton Text="All">
                    </AllButton>
                    <NextPageButton Text="Next &gt;">
                    </NextPageButton>
                    <PrevPageButton Text="&lt; Prev">
                    </PrevPageButton>
                </SettingsPager>


                <StylesPager>
                    <PageNumber Height="520px"></PageNumber>
                </StylesPager>


                <SettingsCustomizationWindow Width="50px" />
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="500" ShowHorizontalScrollBar="true" UseFixedTableLayout="false" />
                <SettingsCookies Enabled="True" StoreFiltering="False" StoreGroupingAndSorting="False"
                    StoreColumnsVisiblePosition="true" StoreColumnsWidth="true"></SettingsCookies>

                <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                    <LoadingPanelOnStatusBar Url="../imgMaster/ajax-loader.gif">
                    </LoadingPanelOnStatusBar>
                    <LoadingPanel Url="../imgMaster/ajax-loader.gif">
                    </LoadingPanel>
                </Images>
                <ImagesFilterControl>
                    <LoadingPanel Url="../imgMaster/ajax-loader.gif">
                    </LoadingPanel>
                </ImagesFilterControl>

                <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css"
                    CssPostfix="Office2003Blue">
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                    <LoadingPanel ImageSpacing="10px">
                    </LoadingPanel>
                </Styles>
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="grdLookup">
            </dx:ASPxGridViewExporter>
        </td>
    </tr>

</table>
<div style="width: 100%; border: 2px groove #eee; margin-top: 4px; border-radius: 4px">
    <table style="margin: 2px; width: 730px;">
        <tr>
            <td>
                <dx:ASPxCallbackPanel ID="clbckInfoText" runat="server" ClientInstanceName="PnlInfoText2"
                    OnCallback="ClBckPnlInfoText_Callback" Width="133px" Height="22px"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False">

                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:TextBox ID="txtInfo2" CssClass="MediumTextBox" ClientInstanceName="txtInfo2" ClientIDMode="Static" MaxLength="25" runat="server" Width="138px" Height="23px" ReadOnly="True" Font-Names="Source Sans Pro" Font-Size="12pt" BackColor="#F9F9F9" BorderStyle="None" />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td>
                <dx:ASPxCallbackPanel ID="clbkPnlbtnNext2" runat="server"
                    ClientInstanceName="PnlbtnNext1" OnCallback="clbkPnlbtnNext_Callback"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False">

                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>

                    <PanelCollection>
                        <dx:PanelContent ID="pnlCntNext2" runat="server">
                            <dx:ASPxButton ID="btnNext2" runat="server" Text=">"
                                CssFilePath="~/App_Themes/Office2010Silver/{0}/styles.css"
                                CssPostfix="Office2010Silver"
                                SpriteCssFilePath="~/App_Themes/Office2010Silver/{0}/sprite.css"
                                OnClick="btnNext_Click1" Theme="Metropolis" BackColor="#E08E0B" ForeColor="White">
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </td>
            <td>
                <dx:ASPxCallbackPanel ID="clbkPnlbtnLast2" runat="server"
                    ClientInstanceName="PnlbtnLast2" OnCallback="clbkPnlbtnLast_Callback"
                    SettingsLoadingPanel-Enabled="False" SettingsLoadingPanel-ShowImage="False" Width="76px">
                    <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent6" runat="server">
                            <dx:ASPxButton ID="btnLast2" runat="server" Text=">>"
                                CssFilePath="~/App_Themes/Office2010Silver/{0}/styles.css"
                                CssPostfix="Office2010Silver"
                                SpriteCssFilePath="~/App_Themes/Office2010Silver/{0}/sprite.css"
                                OnClick="btnLast_Click1" Theme="Metropolis" BackColor="#E08E0B" ForeColor="White">
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server"
                    Text="Export To: " CssClass="selectedtd" />
            </td>
            <td>
                <dx:ASPxRadioButton ID="rbtnPDF2" runat="server" Text="PDF" Checked="True" AutoPostBack="true"
                    GroupName="grpExport2" OnCheckedChanged="rbtnPDF2_CheckedChanged">
                </dx:ASPxRadioButton>
            </td>
            <td>
                <dx:ASPxRadioButton ID="rbtnExcel2"
                    AutoPostBack="true" runat="server" Text="Excel" GroupName="grpExport2">
                </dx:ASPxRadioButton>
            </td>
            <td class="auto-style4">
                <dx:ASPxCheckBox ID="chkGroup2" runat="server" Text="Group Data"
                    OnCheckedChanged="chkGroup2_CheckedChanged" AutoPostBack="True" style="margin-left: 28px" Width="108px">
                </dx:ASPxCheckBox>
            </td>
            <td>
                <dx:ASPxCheckBox ID="chkFilter2" runat="server" Text="Filter Data"
                    OnCheckedChanged="chkFilter2_CheckedChanged" AutoPostBack="True">
                </dx:ASPxCheckBox>

            </td>
        </tr>

    </table>
</div>
<asp:ObjectDataSource ID="dsGrdLookup" runat="server"></asp:ObjectDataSource>
