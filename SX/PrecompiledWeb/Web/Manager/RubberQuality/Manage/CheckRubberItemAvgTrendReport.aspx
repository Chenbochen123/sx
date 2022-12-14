<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_CheckRubberItemAvgTrendReport, App_Web_bsjgrvuf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>质检项目趋势图</title>
    <script language="javascript" type="text/javascript">
        Ext.create("Ext.window.Window", {//胶料查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料",
            modal: true
        });

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//胶料返回值处理
            var materCode = record.data.MaterialCode;
            var materName = record.data.MaterialName;
            App.HiddenNorthMaterCode.setValue(materCode);
            App.TriggerFieldNorthMaterName.setValue(materName);
        };

        var TriggerFieldNorthMaterName_TriggerClick = function (item, trigger, index, tag, e) {
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        };

        var saveChart = function (btn) {
            var url = '../../ReportCenter/SvgToImage.aspx';
            Ext.MessageBox.confirm('提示', '确定将当前的曲线图导出为图片？', function (choice) {
                if (choice == 'yes') {
                    App.ChartCenter.save({
                        type: 'image/png',
                        url: url
                    });
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Text="导出表格">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelEast}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreEast}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExportImage" Text="导出图片">
                                <Listeners>
                                    <Click Fn="saveChart" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Layout="ColumnLayout" Width="800">
                        <Items>
                            <ext:DateField runat="server" ID="DateFieldNorthBeginDate" LabelAlign="Right" FieldLabel="开始日期"
                                Editable="false" Format="yyyy-MM-dd" AllowBlank="false" EmptyText="请选择" LabelWidth="80"
                                InputWidth="110" ColumnWidth="0.25">
                            </ext:DateField>
                            <ext:DateField runat="server" ID="DateFieldNorthEndDate" LabelAlign="Right" FieldLabel="结束日期"
                                Editable="false" Format="yyyy-MM-dd" AllowBlank="false" EmptyText="请选择" LabelWidth="80"
                                InputWidth="110" ColumnWidth="0.25">
                            </ext:DateField>
                            <ext:TriggerField runat="server" ID="TriggerFieldNorthMaterName" LabelAlign="Right"
                                FieldLabel="胶号" Editable="false" AllowBlank="false" EmptyText="请选择" LabelWidth="80"
                                InputWidth="110" ColumnWidth="0.25">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="TriggerFieldNorthMaterName_TriggerClick" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenNorthMaterCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthWorkShop" LabelAlign="Right" FieldLabel="车间"
                                Editable="false" EmptyText="全部" LabelWidth="80" InputWidth="110" MatchFieldWidth="false"
                                ColumnWidth="0.25">
                                <ListConfig Width="110" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthZJS" LabelAlign="Right" FieldLabel="主机手"
                                Editable="false" EmptyText="全部" LabelWidth="80" InputWidth="110" MatchFieldWidth="false"
                                ColumnWidth="0.25">
                                <ListConfig Width="110" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckItem" LabelAlign="Right" FieldLabel="项目"
                                Editable="false" EmptyText="请选择" LabelWidth="80" InputWidth="110" MatchFieldWidth="false"
                                ColumnWidth="0.25">
                                <ListConfig Width="110" />
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStatisticType" LabelAlign="Right" FieldLabel="统计分类"
                                Editable="false" EmptyText="请选择" LabelWidth="80" InputWidth="110" MatchFieldWidth="false"
                                ColumnWidth="0.25">
                                <ListConfig Width="110" />
                                <Items>
                                    <ext:ListItem Mode="Value" Value="1" Text="机台对比" />
                                    <ext:ListItem Mode="Value" Value="2" Text="车间对比" />
                                    <ext:ListItem Mode="Value" Value="3" Text="主机手对比" />
                                </Items>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:Panel runat="server" ID="PanelCenterInfo" Layout="BorderLayout">
                        <Items>
                            <ext:Chart runat="server" ID="ChartCenter" Region="Center">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenter">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenter" />
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:Chart>
                            <ext:GridPanel runat="server" ID="GridPanelEast" Region="East" Width="300" Collapsible="true" Title="数据列表">
                                <Store>
                                    <ext:Store runat="server" ID="StoreEast">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelEast" />
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
