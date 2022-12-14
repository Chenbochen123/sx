<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkerProdReport.aspx.cs" Inherits="Manager_Rubber_Report_WorkerProdReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工产量统计</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var change = function (value, metadata, record, rowIndex, columnIndex, stroe) {
            debugger;
            if (value == null) { return ""; }
            value = value.toString();
            if (value == "0") { value = ""; }
            if (record.data[App.GridPanelCenter.columns[columnIndex + 1].dataIndex] == "否") {
                return '<span style="background-color: red">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex + 1].dataIndex] == "预警") {
                return '<span style="background-color: gold">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex + 1].dataIndex] == "没有标准") {
                return '<span style="background-color: #33FFFF">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex + 1].dataIndex] == "优等") {
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
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".25">
                                <Items>
                                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期" 
                                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" >
                                    </ext:DateField>
                                    <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" 
                                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" >
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".25">
                                <Items>
                                    <ext:ComboBox runat="server" ID="ComboBoxEquip" FieldLabel="机台" LabelAlign="Right"
                                        Editable="false" >
                                    </ext:ComboBox>
                                    <ext:ComboBox runat="server" ID="ComboBoxSubFac" FieldLabel="部门" LabelAlign="Right"
                                        Editable="false" >
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".25">
                                <Items>
                                    <ext:ComboBox runat="server" ID="ComboBoxWorkshop" FieldLabel="岗位" LabelAlign="Right"
                                        Editable="false" >
                                    </ext:ComboBox>
                                    <ext:ComboBox ID="cbxUser" runat="server" Disabled="false" FieldLabel="人员" LabelAlign="Right">
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
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".25" >
                                <Items>
                                      <ext:ComboBox runat="server" ID="ComboBox1" FieldLabel="机台" LabelAlign="Right"
                                        Editable="false" hidden="true" >
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
