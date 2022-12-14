<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostRegister.aspx.cs" Inherits="Manager_Authentication_HostRegister" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密炼车间管控网络系统服务器授权</title>
    <link rel="Stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/") %>tpl/login.css" />
    <script language="javascript" type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="t">
        <div class="h">
            <div class="logo">
                <img src="../../tpl/login/user/images/login_logo.png" /></div>
                <a target="_blank" class="help">请联系供应商提供注册帮助</a>
        </div>
    </div>
    <div class="c">
        <div class="box" style="right: 60px; top: 150px;" id="box">
            <!-- 登录到切换工具栏开始 -->
            <ul class="tab" id="tab">
                <!--li class="current">收信</li-->
                <li id='inbox'>输入服务器授权码</li>
                <li class="dragbar" id="dragbar"></li>
            </ul>
            <!-- 登录到切换工具栏结束 -->
            <!-- 错误信息提示框开始 -->
            <div class="msg" id="msg"><asp:ValidationSummary ID="vs" runat="server" ShowMessageBox="false" ShowSummary="true" style="color:White;" /></div>
            <!-- 错误信息提示框结束 -->
            <div class="boxc">
                <h3>&nbsp;<span id="where"></span></h3>
                <div class="text_item">
                    <asp:TextBox ID="txtRegCode" runat="server" CssClass="text" AutoCompleteType="Disabled" _placeholder="注册码" onfocus="this.className='text_f'"
                        onblur="this.className='text'" /> 
                    <asp:RequiredFieldValidator ID="rfvRegCode" runat="server" ControlToValidate="txtRegCode" Display="None" ErrorMessage="请输入厂家提供的注册码!" />
                    <div class="pop" style="display: none;" id="pop">
                    </div>
                </div>
                <div class="btnb">
                    <!-- 登录按钮 -->
                    <asp:Button runat="server" Id="btnSubmit" CssClass="btn" Text="注  册" style="float: right" />
                    <div style="clear: both">
                    </div>
                </div>
            </div>
        </div>
        <div class="f" id="f" style="display: none;">
        </div>
        <div class="login_drag" id="drag_target">
        </div>
    </div>
    <div class="b">
        <!-- 版权信息开始 -->
        MES 5.0 &copy;2002-2013 mesnac.com &nbsp;&nbsp;&nbsp;&nbsp;中国第一大轮胎工业MES整体解决方案提供商&nbsp;&nbsp;&nbsp;&nbsp;
        <!-- 版权信息结束 -->
    </div>
    </form>
</body>
</html>
