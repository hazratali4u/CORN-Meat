<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerFeedback.aspx.cs" Inherits="Forms_frmCustomerFeedback" Title="CORN :: Customer Feedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">

    <script language="JavaScript" type="text/javascript">
        function ShowPopup() {
            $('#myModal').modal('show');
        }

        function ValidateOtherMedium() {
            var chkBoxP = document.getElementById('<%= Hear6.ClientID %>');
            if (chkBoxP.checked == true) {
                Error("Hello");
                document.getElementById('<%= txtOther.ClientID %>').disabled = false;
                document.getElementById('<%= txtOther.ClientID %>').readOnly = false;
                document.getElementById('<%= txtOther.ClientID %>').value = "";
            }
            else {
                document.getElementById('<%= txtOther.ClientID %>').disabled = true;
                document.getElementById('<%= txtOther.ClientID %>').readOnly = true;
                document.getElementById('<%= txtOther.ClientID %>').value = "";
            }
        }
        var previousIdService;
        var previousIdFood;
        var previousIdEnvironment;
        var previousIdHear;

        function toggle(chkBox, ListName) {
            var chkHear = document.getElementById('<%= Hear6.ClientID %>');
            if (ListName == 'Service') {
                if (chkBox.checked) {
                    if (previousIdService) {
                        document.getElementById(previousIdService).checked = false;
                    }
                    previousIdService = chkBox.getAttribute('id');
                }
            }
            if (ListName == 'Food') {
                if (chkBox.checked) {
                    if (previousIdFood) {
                        document.getElementById(previousIdFood).checked = false;
                    }
                    previousIdFood = chkBox.getAttribute('id');
                }
            }
            if (ListName == 'Environment') {
                if (chkBox.checked) {
                    if (previousIdEnvironment) {
                        document.getElementById(previousIdEnvironment).checked = false;
                    }
                    previousIdEnvironment = chkBox.getAttribute('id');
                }
            }
            if (ListName == 'Overall') {
                if (chkBox.checked) {
                    if (previousIdOverall) {
                        document.getElementById(previousIdOverall).checked = false;
                    }
                    previousIdOverall = chkBox.getAttribute('id');
                }
            }
            if (ListName == 'Hear') {
                if (chkBox.checked) {
                    if (previousIdHear) {
                        document.getElementById(previousIdHear).checked = false;
                    }
                    previousIdHear = chkBox.getAttribute('id');
                }
                if (chkBox == chkHear) {
                    ValidateOtherMedium();
                }
                else {
                    document.getElementById('<%= txtOther.ClientID %>').disabled = true;
                    document.getElementById('<%= txtOther.ClientID %>').readOnly = true;
                    document.getElementById('<%= txtOther.ClientID %>').value = "";
                }
            }
        }

    </script>
    <style>
        .ListOrientation {
            text-orientation: sideways-left;
        }

            .ListOrientation input[type=checkbox] {
                margin-left: 10px;
            }
    </style>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel3" runat="server" BackColor="White" DefaultButton="btnSave">
                        <div class="row">
                            <div class="col-md-7" style="text-align: center;">
                                <label for="ctl00_ctl00_mainCopy_cphPage_fuPic" id="lbl"
                                    runat="server" style="cursor: pointer;">
                                    <img src="../images/watch.png" id="pic" name="pic" width="100" height="100" />
                                </label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7" style="text-align: center;">
                                <asp:Label ID="lblTitle" Text="WE'RE INTERESTED IN YOUR COMMENTS" runat="server" Width="312px" ForeColor="Red" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog modal-sm">
                                    <!-- Modal content-->
                                    <div class="modal-content" style="border-radius: 73px;">
                                        <div class="modal-body" style="text-align:center;color:darkslateblue">
                                            <h1><i>Thank You !!!</i></h1>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <di v class="row">
                            <div class="col-md-7">
                                <div class="col-md-12">
                                    <table width="100%">
                                        <tr>
                                            <td></td>
                                            <td align="center">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Excellent</label></td>
                                            <td align="center">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Very good</label></td>
                                            <td align="center">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Good</label></td>
                                            <td align="center">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Fair</label></td>
                                            <td align="center">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Poor</label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Service</label></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Service5" onClick="toggle(this,'Service');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Service4" onClick="toggle(this,'Service');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Service3" onClick="toggle(this,'Service');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Service2" onClick="toggle(this,'Service');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Service1" onClick="toggle(this,'Service');" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Food</label></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Food5" onClick="toggle(this,'Food');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Food4" onClick="toggle(this,'Food');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Food3" onClick="toggle(this,'Food');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Food2" onClick="toggle(this,'Food');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Food1" onClick="toggle(this,'Food');" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Environment</label></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Environment5" onClick="toggle(this,'Environment');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Environment4" onClick="toggle(this,'Environment');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Environment3" onClick="toggle(this,'Environment');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Environment2" onClick="toggle(this,'Environment');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Environment1" onClick="toggle(this,'Environment');" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <label><span class="fa fa-caret-right rgt_cart"></span>Overall Experience</label></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Overall5" onClick="toggle(this,'Overall');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Overall4" onClick="toggle(this,'Overall');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Overall3" onClick="toggle(this,'Overall');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Overall2" onClick="toggle(this,'Overall');" /></td>
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="Overall1" onClick="toggle(this,'Overall');" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </di>
                        <div class="row" style="margin-top: 15px">
                            <div class="col-md-7">
                                <div class="col-md-12" style="margin-top: 10px">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>We welcome your comments or any suggestions:</label>
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="col-md-12">
                                    <label id="lbltoLocation"><span class="fa fa-caret-right rgt_cart"></span>How did you hear about us?</label>
                                    <br />
                                    <asp:CheckBox runat="server" ID="Hear1" Text="I've eaten here before" onClick="toggle(this,'Hear');" />
                                    <asp:CheckBox runat="server" ID="Hear2" Text="Advertisement" onClick="toggle(this,'Hear');" Style="margin-left: 10%" />
                                    <br />
                                    <asp:CheckBox runat="server" ID="Hear3" Text="Refered by a friend" onClick="toggle(this,'Hear');" />
                                    <asp:CheckBox runat="server" ID="Hear4" Text="Internet/website" onClick="toggle(this,'Hear');" Style="margin-left: 13%" />
                                    <br />
                                    <asp:CheckBox runat="server" ID="Hear5" Text="Just driving/walking by" onClick="toggle(this,'Hear');" />
                                    <asp:CheckBox runat="server" ID="Hear6" Text="Others" onClick="toggle(this,'Hear');" Style="margin-left: 8%" />
                                    <asp:TextBox ID="txtOther" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="col-md-12" style="margin-top: 10px">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>If you would like to be on our mailing list, Please share your detail:</label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px">
                            <div class="col-md-7">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Name</label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Contact No</label>
                                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Email Address</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-offset-1 col-md-6 ">
                                <div class="btnlist pull-right">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" TabIndex="3" />
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" TabIndex="4" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="hiddenPassword" runat="server" Visible="False" Width="97px"></asp:TextBox>
        </div>
    </div>
</asp:Content>
