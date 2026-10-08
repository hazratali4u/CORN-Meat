<%@ Page Title="CORN :: Bill of Material" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmBillOfMaterial.aspx.cs" Inherits="Forms_frmBillOfMaterial" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" />
    <script type="text/javascript" src="../AjaxLibrary/ValidateDotsAndNumbers.js"></script>
    <script language="JavaScript" type="text/javascript">
        
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Quantity is required');
                return false;
            }

            return true;
        }

        function pageLoad() {

            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
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
            <asp:UpdatePanel ID="pnlBillOfMaterial" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label id="lblSKUFinished"><span class="fa fa-caret-right rgt_cart"></span>Item Name</label>
                            <dx:ASPxComboBox runat="server" ID="ddlSKUFinished" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSKUFinished_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-2">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Unit</label>
                            <dx:ASPxComboBox runat="server" ID="drpSkuUnit" Enabled="false" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-1">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Qty</label>
                            <asp:TextBox ID="txtRecipeQty" runat="server" Enabled="false" CssClass="form-control" Width="70px"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <label><span class="fa fa-caret-right rgt_cart"></span></label>
                            <asp:CheckBox ID="chkIsReqProduciotn" runat="server" Checked="false" Text=" Production Required" Font-Bold="true"></asp:CheckBox>

                        </div>
                    </div>

                    <asp:Panel runat="server" ID="pnlDetail" DefaultButton="btnAdd">
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Raw Material Item</label>
                            </div>
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Qty</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <dx:ASPxComboBox runat="server" ID="ddlSKU" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSKU_SelectedIndexChanged" AutoPostBack="true">
                                </dx:ASPxComboBox>
                                <asp:HiddenField Value="" runat="server" ID="hfRawUnit" />
                                <asp:HiddenField Value="" runat="server" ID="hfRawUnitName" />
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtQuantity" runat="server" onkeyup="checkDec(this);"
                                    onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                                
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success"
                                    OnClick="btnAdd_Click" OnClientClick="return ValidateForm();" />
                            </div>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSKU" runat="server" Width="58%" Height="180px"
                        ScrollBars="Vertical" BorderColor="Silver" BorderStyle="Groove"
                        BorderWidth="1px">
                        <asp:GridView Width="100%" ID="gvSKU" runat="server" Class="table table-striped table-bordered table-hover table-condensed cf"
                            AutoGenerateColumns="False" OnRowDeleting="gvSKU_RowDeleting" OnRowEditing="gvSKU_RowEditing"
                            ShowHeader="true" >
                           
                            <Columns>
                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name" ReadOnly="true">
                                    <ItemStyle Width="45%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UOM_DESC" HeaderText="UOM" ReadOnly="true">
                                    <ItemStyle Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" ReadOnly="true">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="UOM_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil" CommandName="Edit" ToolTip="Edit">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="glyphicon glyphicon-trash"
                                            OnClientClick="javascript:return confirm('Are you sure you want to delete?');return false;"
                                            ToolTip="Delete">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-md-offset-4 col-md-3 ">
                            <div class="btnlist pull-right">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

