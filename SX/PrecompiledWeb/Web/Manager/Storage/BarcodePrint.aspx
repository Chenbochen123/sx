﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_BarcodePrint, App_Web_p5ht2o2r" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False" 
             OnStartReport="WebReport1_StartReport" Width="100%" Zoom="1" 
            Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" PdfEmbeddingFonts="True" Layers="False" />
    </div>
    </form>
</body>
</html>
