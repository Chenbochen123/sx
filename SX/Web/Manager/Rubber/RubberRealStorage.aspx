﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberRealStorage.aspx.cs" Inherits="Manager_Rubber_RubberRealStorage" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>车间胶料实时库存</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">

    </style>
    <script type="text/javascript">
        var change = function (value, metadata, record, rowIndex, columnIndex, stroe) {
            debugger;
            if (value == null) { return ""; }
            value = value.toString();
            if (value == "0") { value = ""; }
            if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "安全") {
                //return '<span style="background-color: red;font-weight:bold">' + value + '</span>';
                return '<span style="background-color: green">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "超上限") {
                return '<span style="background-color: red">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "低下限") {
                return '<span style="background-color: yellow">' + value + '</span>';
            }
            return value;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_stop_type" runat="server" />
    <ext:Hidden ID="hidden_stop_fault" runat="server" />
    <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <DirectEvents>
                                    <Click OnEvent="btnExportSubmit_Click" IsUpload="true">
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
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                 <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbofac" runat="server" FieldLabel="部门" LabelAlign="Right" Text="全部"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="01,07">
                                                </ext:ListItem>
                                                <ext:ListItem Text="子午" Value="01">
                                                </ext:ListItem>
                                                <ext:ListItem Text="斜交" Value="07">
                                                </ext:ListItem>
                                            </Items>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" Qtip="清空">
                                                </ext:FieldTrigger>
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Handler="this.setValue('');" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
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
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
               
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>