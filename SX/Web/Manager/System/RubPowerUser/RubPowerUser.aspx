<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubPowerUser.aspx.cs" Inherits="Manager_System_RubPowerUser_RubPowerUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <script>
         var getDragDropText = function () {
             var buf = [];

             buf.push("<ul>");

             Ext.each(this.view.panel.getSelectionModel().getSelection(), function (record) {
                 buf.push("<li>" + record.data.Name + "</li>");
             });

             buf.push("</ul>");

             return buf.join("");
         };
    </script>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Panel ID="Panel1" runat="server" Width="650" Height="300">
            <LayoutConfig>
                <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
            </LayoutConfig>
            <Items>
                <ext:Label runat="server" ID="lblTitle"></ext:Label>
                <ext:GridPanel
                    ID="GridPanel1" 
                    runat="server" 
                    MultiSelect="true"
                    Flex="1"
                    Title="Left"
                    Margins="0 2 0 0">
                    <Store>
                        <ext:Store ID="Store1" runat="server">
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Name" />
                                        <ext:ModelField Name="Column1" />
                                        <ext:ModelField Name="Column2" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:Column ID="Column1" runat="server" Text="Record Name" Width="160" DataIndex="Name" Flex="1" />
                            <ext:Column ID="Column2" runat="server" Text="Column 1" Width="60" DataIndex="Column1" />
                            <ext:Column ID="Column3" runat="server" Text="Column 2" Width="60" DataIndex="Column2" />
                        </Columns>
                    </ColumnModel>                    
                    <View>
                        <ext:GridView ID="GridView1" runat="server">
                            <Plugins>
                                <ext:GridDragDrop ID="GridDragDrop1" runat="server" DragGroup="firstGridDDGroup" DropGroup="secondGridDDGroup"/>
                            </Plugins>
                            <Listeners>
                                <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('Name') : ' on empty view'; 
                                               Ext.net.Notification.show({title:'Drag from right to left', html:'Dropped ' + data.records[0].get('Name') + dropOn});" />
                            </Listeners>
                        </ext:GridView>
                    </View>   
                </ext:GridPanel>
                <ext:GridPanel 
                    ID="GridPanel2" 
                    runat="server"
                    MultiSelect="true"
                    Title="Right"
                    Flex="1"
                    Margins="0 0 0 3">
                    <Store>
                        <ext:Store ID="Store2" runat="server">
                            <Model>
                                <ext:Model ID="Model2" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Name" />
                                        <ext:ModelField Name="Column1" />
                                        <ext:ModelField Name="Column2" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:Column ID="Column4" runat="server" Text="Record Name" Width="160" DataIndex="Name" Flex="1" />
                            <ext:Column ID="Column5" runat="server" Text="Column 1" Width="60" DataIndex="Column1" />
                            <ext:Column ID="Column6" runat="server" Text="Column 2" Width="60" DataIndex="Column2" />
                        </Columns>
                    </ColumnModel>                   
                    <View>
                        <ext:GridView ID="GridView2" runat="server">
                            <Plugins>
                                <ext:GridDragDrop ID="GridDragDrop2" runat="server" DragGroup="secondGridDDGroup" DropGroup="firstGridDDGroup"/>
                            </Plugins>
                            <Listeners>
                                <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('Name') : ' on empty view'; 
                                               Ext.net.Notification.show({title:'Drag from left to right', html:'Dropped ' + data.records[0].get('Name') + dropOn});" />
                            </Listeners>
                        </ext:GridView>
                    </View>   
                </ext:GridPanel>
            </Items>
            <BottomBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                        <ext:Button ID="Button1" runat="server" Text="Reset both grids">
                            <Listeners>
                                <Click Handler="#{Store1}.loadData(#{Store1}.proxy.data); #{Store2}.removeAll();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>                
            </BottomBar>
        </ext:Panel> 
    </form>    
</body>
</html>
