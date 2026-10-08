<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDayCloseStatus.aspx.cs" Inherits="Forms_frmDayCloseStatus" Title="CORN :: Data Update Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            if (document.getElementById('<%=btnDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnDayClose.ClientID%>').disabled = true;
            }
            if (document.getElementById('<%=btnReverseDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnReverseDayClose.ClientID%>').disabled = true;
            }
        }

        function endRequest(sender, e) {
            if (document.getElementById('<%=btnDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnDayClose.ClientID%>').disabled = false;
            }
             if (document.getElementById('<%=btnReverseDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnReverseDayClose.ClientID%>').disabled = false;
            }
        }

        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server"  ImageUrl="~/OrderPOS/images/wheel.gif"
                        />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-4">
                        <asp:RadioButtonList ID="rblDistributorTypes" runat="server" Visible="False" Width="450px"
                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDistributorTypes_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">In Active</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <asp:GridView ID="Grid_Hierarchy" runat="server" Width="100%"
                     CssClass="table table-striped table-bordered table-hover table-condensed cf"
                    AutoGenerateColumns="False" EmptyDataText="No Record exist">
                  
                    <Columns>
                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location Name" ReadOnly="true">
                          
                        </asp:BoundField>
                        <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person" ReadOnly="true">
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number" ReadOnly="true">
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="CLOSING_DATE" HeaderText="Last Day Close" ReadOnly="true">
                            <ItemStyle ForeColor="Red" />
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="TIME_STAMP" HeaderText="Date/Time of Closing" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}" ReadOnly="true">
                          
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="cf head" />
                </asp:GridView>

                <div class="row">
                    <div class="col-md-offset-0 col-md-4 ">
                        <div class="btnlist pull-left">
                            <asp:Button ID="btnDayClose" runat="server" Text="Day Close" Visible="False" CssClass="btn btn-success"
                                OnClick="btnDayClose_Click" />
                            <asp:Button ID="btnReverseDayClose" runat="server" Text="Reverse Day Close" Visible="False" CssClass="btn btn-danger"
                                OnClick="btnReverseDayClose_Click" OnClientClick="javascript:return confirm('Are you sure you want to Reverse Day Close?');return false;" />
                            <asp:Button ID="btnSync" runat="server" Text="Sync" Visible="False" CssClass="btn btn-success"
                                OnClick="btnSync_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
