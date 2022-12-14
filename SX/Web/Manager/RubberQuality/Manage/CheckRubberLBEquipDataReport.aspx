﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberLBEquipDataReport.aspx.cs" Inherits="Manager_RubberQuality_Manage_CheckRubberLBEquipDataReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检验日报表</title>
    <%--<link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>--%>
    <script type="text/javascript">
        var change = function (value,metadata,record,rowIndex,columnIndex,stroe) {
            debugger;
            if (value == null) { return "";}
            value = value.toString();
            if (value == "0") { value = "";}
            if(record.data[App.GridPanelCenter.columns[columnIndex+1].dataIndex] == "否")
            {
                //return '<span style="background-color: red;font-weight:bold">' + value + '</span>';
                return '<span style="background-color: red">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex+1].dataIndex] == "预警") {
                return '<span style="background-color: gold">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex+1].dataIndex] == "没有标准") {
                return '<span style="background-color: #33FFFF">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex+1].dataIndex] == "优等") {
                return '<span style="background-color: #00FF33">' + value + '</span>';
            }
            return value;
         };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click" Timeout="600000">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenter}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenter}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                     <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110" Hidden="true">
                    </ext:DateField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthWorkshop" FieldLabel="厂区" LabelAlign="Right"
                        Editable="false">
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthItemType" FieldLabel="检验类型" LabelAlign="Right"
                        Editable="false">
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                            </Columns>
                        </ColumnModel>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Label ID="planState" Html="(颜色状态说明:<span style='color:gold;padding-right:10px;font-weight: bold;'>预警</span><span style='color:red;padding-right:10px;padding-left:10px;font-weight: bold;'>不合格</span><span style='color:#33FFFF;padding-right:10px;font-weight: bold;'>无标准</span><span style='color:#00FF33;padding-right:10px;font-weight: bold;'>优等品</span>)"
                                        runat="server" ColumnWidth=".7">
                                    </ext:Label>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
