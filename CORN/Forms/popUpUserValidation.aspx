<div class="container" id="UserValidation" style="max-width: 400px; display: none;
    z-index: 5000; top: 30%; left: 35%; position: absolute; background-color: #c6c6c6; border: 2px solid #000;    padding: 10px;">
    <div class="row">
        <div class="col-md-12">
            <div class="text">
                User Authentication</div>
            <div class="col-md-12" style="margin-top: 5px; margin-left: 60px;">
                <div class="col-md-8">
                    <label>
                        User ID
                    </label>
                    <input type="text" id="txtUserID" autocorrect="off" runat="server" class=" form-control" autocomplete="off" value="" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12" style="margin-top: 5px;  margin-left: 60px;">
                <div class="col-md-8">
                    <label>
                        User Password
                    </label>
              
                     <input type="password" id="txtUserPass" runat="server" class=" form-control" autocomplete="off" value=""/>
                </div>
            </div>
            
        </div>
    </div>

     <div class="row">
        <div class="col-md-12">
          
            <div class="col-md-12">
                
                <div class="col-md-6" style="margin-top: 5px;  margin-left: 40px;">
                    <div class="col-md-5 btn-mar">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; font-size: 15px;
                            color: #FFF; background-color: #3c8d75;" id="Button1" onclick="UserValidationInDataBase();">
                            Proceed</button>
                    </div>
                    <div class="col-md-5 btn-mar" style="margin-left: 10px;">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left :45px; font-size: 15px;
                            color: #FFF; background-color: #f94d72;" id="Button2" onclick="CancelUserValidation();">
                            Cancel</button>
                    </div>
                    <div class="col-md-5 btn-mar" style="margin-top: 15px;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>

