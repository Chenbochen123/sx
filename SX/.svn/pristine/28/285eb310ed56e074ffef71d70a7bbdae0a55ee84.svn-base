<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_canAuditUser, App_Web_zqfhdfip" %>



<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>审核人选定</title>
 

    <!--特殊-->
    <script type="text/javascript">

        var BtnModifySave_Click = function () {
            var AuditUser = "";
            
            for (var i = 0; i < App.CheckboxGroupAuditUser.items.items.length; i++) {
                var item = App.CheckboxGroupAuditUser.items.items[i];
                if (item && item.checked) {
                    AuditUser = AuditUser + item.inputValue + "|";
                }
            }
            parent.App.Manager_BasicInfo_CommonPage_canAuditUser_Window.close();
            parent.requestcanAuditUser(AuditUser);

        }
        var BtnCancel_Click = function () {
            parent.App.Manager_BasicInfo_CommonPage_canAuditUser_Window.close();
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server"  />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
              <ext:Panel ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" 
                    Width="400" Height="300" Resizable="false"  Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:CheckboxGroup ID="CheckboxGroupAuditUser" runat="server"  LabelAlign="Right"  ColumnsNumber="4" Flex="1" AnchorHorizontal="true">
                                        </ext:CheckboxGroup>
                                        <ext:Hidden ID="Hidden1" runat="server" Text="1"></ext:Hidden>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <Listeners>
                              <Click Fn="BtnModifySave_Click" />
                           </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                           <Listeners>
                                <Click Fn="BtnCancel_Click"/>
                               
                            </Listeners>
                        </ext:Button>
                    </Buttons>
          
               </ext:Panel>
            
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
