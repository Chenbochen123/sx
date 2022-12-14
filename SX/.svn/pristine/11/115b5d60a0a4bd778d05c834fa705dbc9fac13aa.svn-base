<%@ page language="C#" autoeventwireup="true" inherits="Index, App_Web_xaf1euwx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密炼车间管控网络系统-MESNAC</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="resources/images/logotiny.ico" media="screen" />
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        body
        {
            font-size: 12px;
            font-family: "microsoft yahei" ,Verdana, Arial, Helvetica, sans-serif;
            color: #333;
            background:#b5b5b6;
            /*background: #f5f5f5;*/
        }
        #content
        {
            height: 600px;
            width: 1024px;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -304px 0 0 -515px;
            background-image:url(resources/login/images/login-b.jpg)
        }
        .cont
        {
        }
        .login
        {
            color:#FFFFFF;
            float: right;
            margin:150px 0px 0px 0px;
            width: 396px;
            height: 358px;
        }
        .message
        {
            position: absolute;
            height: 25px;
            line-height: 22px;
            width: 200px;
            background: url(resources/login/images/errmessage.gif) no-repeat;
            color: #c00;
            text-indent: 30px;
            left: 744px;
            top: 222px;
            z-index: 2;
        }
        .cp
        {
            position: absolute;
            text-align:center;
            height: 25px;
            line-height: 22px;
            font-size:16px;
            color: #FFFFFF;
            left:340px;
            top: 560px;
            z-index: 2;
        }
        table
        {
            border-collapse: collapse;
            margin: 100px 0 0 85px;
        }
        table input, table select, table img
        {
            vertical-align: middle;
        }
        th
        {
            text-align: right;
            padding-right: 5px;
            font-weight: 400;
            height: 50px;
        }
        td
        {
            padding: 0 3px;
        }
        .text
        {
            font-weight: 700;
            height: 24px;
            font-size: 14px;
            line-height: 24px;
            width: 150px;
            border: 1px solid #878787;
            background: #fff;
        }
        .wid1
        {
            width: 50px;
        }
        .btn
        {
            height: 32px;
            width: 107px;
            font-family: "microsoft yahei";
            color:#444444;
            font-size: 12px;
            font-weight: 700;
        }
        select
        {
            font-weight: 700;
            height: 24px;
            font-size: 12px;
            line-height: 24px;
            width: 150px;
            border: 1px solid #878787;
            color:#444444;
            background: #fff;
        }
        a
        {
            color: #007BBB;
            text-decoration: underline;
        }
        a:hover
        {
            text-decoration: none;
        }

        .help
        {
            float: right;
            margin: 55px 20px 0 0;
            color: #fff;
            text-decoration: none;
        }
        a.help:hover
        {
            text-decoration: underline;
        }
    </style>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            var flag = false;
            var top = window;
            while (true) {
                if (top.parent) {
                    if (top == top.parent) {
                        break;
                    }
                    top = top.parent;
                    flag = true;
                } else {
                    break;
                }
            }
            if (flag)
            {
                top.location.href = "<%= Page.ResolveUrl("~/") %>Index.aspx";
            }
        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content">
        <asp:ValidationSummary ID= "vs" runat="server" ShowMessageBox="false" ShowSummary="true" DisplayMode="SingleParagraph" CssClass="message" />
        <div class="cont">
            <div class="login">
                <table>
                    <tr>
                        <th>
                            用户名称<br />
                            UserName
                        </th>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="text" />
<%--                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="请输入用户名称!" Display="None" />--%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密 码<br />
                            PassWord
                        </th>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="text" TextMode="Password" />
      <%--                      <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="请输入密码!" Display="None" />--%>
                        </td>
                    </tr>
                    <tr id="trDbVersion" runat="server">
                        <th>
                            数据版本<br />
                            DataVersion
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDbVersion" runat="server">
                            
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td  align="right">
                            <asp:CustomValidator ID="cv" runat="server" ControlToValidate="txtUserName" ErrorMessage="用户名称或密码错误!" Display="None" />
                            <asp:Button ID="btnSubmit" runat="server" Text="登 录 / Login" CssClass="btn" 
                                onclick="btnSubmit_Click" />&nbsp;
                                <%-- <asp:Button ID="Button1" runat="server" Text="修改密码" CssClass="btn" 
                                onclick="btnChangePS_Click" />&nbsp;--%>
                      </td>
                    </tr>
                </table>
            </div>
           <%-- <div class="cp">版权所有 &copy;2002 - 2013 软控股份有限公司</div>--%>
                <div class="cp">版权所有 &copy;青岛弯弓信息技术有限公司</div>
        </div>
    </div>
    <asp:Label   id="form2" runat="server" visible="false">
        <asp:TextBox ID="TextBox1" runat="server" CssClass="text" />  
           <asp:Button ID="Button2" runat="server" Text="确定" CssClass="btn" 
                                onclick="btnChangeA_Click" />&nbsp;
                                 <asp:Button ID="Button3" runat="server" Text="取消" CssClass="btn" 
                                onclick="btnChangeC_Click" />&nbsp;
                               
        </asp:Label>

    </form>
     
</body>
</html>
