<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryBasUsers.aspx.cs" Inherits="Manager_BasicInfo_CommonPage_QueryBasUsers" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var btnOK_Click = function () {
            App.direct.getWorkBarcode({
                success: function (result) {
                    parent.Manager_BasicInfo_CommonPage_QueryBasUsers_Request(result);
                    parent.App.Manager_BasicInfo_CommonPage_QueryBasUsers_Window.close();
                },
                failture: function () {
                    parent.App.Manager_BasicInfo_CommonPage_QueryBasUsers_Window.close();
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:GridPanel ID="gridPanel" runat="server" Region="Center" MultiSelect="true">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="RealName" />
                                        <ext:ModelField Name="HRCode" />
                                        <ext:ModelField Name="WorkBarcode" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:Column ID="HRCode" DataIndex="HRCode" runat="server" Text="用户编号" Width="60" />
                            <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Width="120" />
                            <ext:Column ID="RealName" DataIndex="RealName" runat="server" Text="用户姓名" Flex="1" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" />
                    </SelectionModel>
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Accept" Text="确定" ID="btnOK">
                                    <Listeners>
                                        <Click Fn="btnOK_Click"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:Button runat="server" Icon="Cancel" Text="取消" ID="btnCancel">
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
