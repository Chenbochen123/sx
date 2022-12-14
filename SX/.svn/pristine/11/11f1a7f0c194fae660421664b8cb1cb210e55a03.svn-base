<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Manager_Authentication_Login" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <title>密炼车间管控网络系统 - 登录</title>
    <script type="text/javascript">
        function RequestException(response, Msg, sender) {
            if (response.timedout) {
                Ext.Msg.alert('请求超时', '系统登录请求超时，请及时联系管理人员！');
            } else {
                Ext.Msg.alert('执行错误', Msg);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Window ID="Window1" runat="server" Closable="false" Resizable="false" Height="150"
            Icon="Lock" Title="用户登录" Draggable="false" Width="350" Modal="true" BodyPadding="5"
            DefaultButton="Button1"
            Layout="Form">
            <Items>
                <ext:TextField ID="txtUsername" runat="server" FieldLabel="用户名称" AllowBlank="false"
                    BlankText="请输入用户名称。" />
                <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="用户密码"
                    AllowBlank="false" BlankText="请输入密码。" />
                <ext:ComboBox ID="ddlDbVersion" runat="server" FieldLabel="数据版本" Editable="false" />
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="登录" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="Button1_Click" Before="return (#{txtUsername}.isValid() && #{txtPassword}.isValid());"
                            Failure="RequestException">
                            <EventMask ShowMask="true" Msg="正在验证..." MinDelay="0" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>
