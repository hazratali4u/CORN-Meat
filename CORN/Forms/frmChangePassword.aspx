<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChangePassword.aspx.cs" Inherits="Forms_fmChangePassword" Title="CORN :: Change Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            addBubbleMouseovers("CurrentPassword");
            addBubbleMouseovers("NewPassword");
            addBubbleMouseovers("ConfirmPassword");
        }

        //--indicates the mouse is currently over a div
        var onDiv = false;
        //--indicates the mouse is currently over a link
        var onLink = false;
        //--indicates that the bubble currently exists
        var bubbleExists = false;
        //--this is the ID of the timeout that will close the window if the user mouseouts the link
        var timeoutID;
        var ClassName;
        var DivText;

        function addBubbleMouseovers(mouseoverClass) {
            $("." + mouseoverClass).mouseover(function (event) {
                if (onDiv || onLink) {
                    return false;
                }
                ClassName = mouseoverClass;
                onLink = true;
                showBubble.call(this, event);
            });

            $("." + mouseoverClass).mouseout(function () {
                onLink = false;
                timeoutID = setTimeout(hideBubble, 150);
            });
        }

        function hideBubble() {
            clearTimeout(timeoutID);
            //--if the mouse isn't on the div then hide the bubble
            if (bubbleExists && !onDiv) {
                $("#bubbleID").remove();
                bubbleExists = false;
            }
        }

        function showBubble(event) {
            if (bubbleExists) {
                hideBubble();
            }
            if (ClassName == 'CurrentPassword') {
                DivText = '<u><b>Current Password</b></u><br/>'
            + 'The password you use for login to CORN';
            }
            else if (ClassName == 'NewPassword') {
                DivText = '<u><b>New Password</b></u><br/>'
            + 'Max length:50<br>';
            }
            else if (ClassName == 'ConfirmPassword') {
                DivText = '<u><b>Confirm Password</b></u><br/>'
           + 'Same as new password';
            }

            var tPosX = event.pageX - 175;
            var tPosY = event.pageY + 2;
            $('<div ID="bubbleID"'
        + 'style="top:' + tPosY + 'px;'
        + 'left:' + tPosX + 'px;'
        + 'position: absolute;'
        + 'display: inline;'
        + 'border: 2px;'
        + 'width: 170px;'
        + 'height: 65px;'
        + 'padding:5px;'
        + 'color:White;'
        + 'background-color: #007395;">'
        + DivText
        + '</div>').mouseover(keepBubbleOpen).mouseout(letBubbleClose).appendTo('body');

            bubbleExists = true;
        }

        function keepBubbleOpen() {
            onDiv = true;
        }

        function letBubbleClose() {
            onDiv = false;

            hideBubble();
        }

        function ValidatePassword() {

            var strCurrentPassword;
            var strNewPassword;
            var strChangePassword;

            strCurrentPassword = document.getElementById('<%=txtCurrentPassword.ClientID%>').value;
            if (strCurrentPassword == null || strCurrentPassword.length == 0) {
                alert('Please enter current password ');
                document.getElementById('<%=txtCurrentPassword.ClientID%>').focus();
                return false;
            }
            strNewPassword = document.getElementById('<%=txtNewPassword.ClientID%>').value;
            if (strNewPassword == null || strNewPassword.length == 0) {
                alert('Please enter new password ');
                document.getElementById('<%=txtNewPassword.ClientID%>').focus();
                return false;
            }
            strChangePassword = document.getElementById('<%=txtConfirmNewPassword.ClientID%>').value;
            if (strChangePassword == null || strChangePassword.length == 0) {
                alert('Please enter confirm new password ');
                document.getElementById('<%=txtConfirmNewPassword.ClientID%>').focus();
                return false;
            }
            if (strNewPassword == strChangePassword) {
                return true;
            }
            else {
                alert('New Password does not match');
                return false;
            }

            return true;
        }

    </script>

    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSave">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Current Password</label>
                                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="margin-top: 35px; margin-left: -22px">
                                <a class="CurrentPassword" href="javascript:void(0)">
                                    <img height="15" alt="" src="../App_Themes/Granite/Images/help.jpg" /></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label id="lbltoLocation"><span class="fa fa-caret-right rgt_cart"></span>New Password</label>
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="margin-top: 35px; margin-left: -22px">
                                <a class="NewPassword" href="javascript:void(0)">
                                    <img height="15" alt="" src="../App_Themes/Granite/Images/help.jpg" /></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Confirm New Password</label>
                                <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password" TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="margin-top: 35px; margin-left: -22px">
                                <a class="ConfirmPassword" href="javascript:void(0)">
                                    <img height="15" alt="" src="../App_Themes/Granite/Images/help.jpg" /></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-offset-1 col-md-3 ">
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
