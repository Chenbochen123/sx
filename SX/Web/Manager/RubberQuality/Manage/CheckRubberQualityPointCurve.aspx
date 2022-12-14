﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualityPointCurve.aspx.cs" Inherits="Manager_RubberQuality_Manage_CheckRubberQualityPointCurve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检验数据曲线</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/echarts.min.js"></script>
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            hidden: true,
            width: 370,
            height: 470,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=3,4,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            App.txMaterialName.getTrigger(0).show();
            App.hiddenMaterialCode.setValue(record.data.MaterialCode);
            App.txMaterialName.setValue(record.data.MaterialName);
        }
        var QueryMaterialInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterialCode.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        //var Fuzhi = function () {
        //    App.direct.ChangeFuzhi({
        //        success: function (result) { },

        //        failure: function (errorMsg) { }
        //    })
        //}
        function TipsRenderer(tip, record) {
            debugger;
            var ss = "日期：" + record.get('plandate').getFullYear()
                + "-" + (record.get('plandate').getMonth() + 1)
                + "-" + record.get('plandate').getDate() + '<br />';
            ss = ss + "检验值：" + record.get('checkvalue') + '<br />';

            tip.setTitle(ss);
        }

        function saveChart(btn) {
            var url = '../../ReportCenter/SvgToImage.aspx';
            Ext.MessageBox.confirm('提示', '确定将当前的曲线图导出为图片？', function (choice) {
                if (choice == 'yes') {
                    App.Chart1.save({
                        type: 'image/png',
                        url: url
                    });
                }
            });
        }

        var refreshChart = function () {
            debugger;
            App.direct.chartMainBindData({
                success: function (result) {
                    //alert("1" + result);
                    var arr = result.split("@");
                    //console.log(arr[0]);
                    //console.log(arr[1]);
                    //console.log(arr[2]);
                    //console.log(arr[3]);
                    //console.log(arr[4]);
                    debugger;
                    option = {
                        title: {
                            text: '检验数据分布图'
                        },
                        tooltip: {
                            padding: 10,
                            backgroundColor: '#222',
                            borderColor: '#777',
                            borderWidth: 1
                        },
                        legend: {
                            data: ['检验值', '平均值']
                        },
                        xAxis: {
                            type: 'category'
                        },
                        yAxis: {
                            min: function (value) {
                                if (value.min >= arr[3])
                                { return Math.floor(arr[3] * 0.95); }
                                else
                                { return Math.floor(value.min * 0.95); }

                            },

                            max: function (value) {
                                if (value.max >= arr[2])
                                { return Math.ceil(value.max * 1.05); }
                                else
                                { return Math.ceil(arr[2] * 1.05); }
                            }
                        },
                        //yAxis: {
                        //    min: function (value) {
                        //        return Math.floor(value.min);
                        //    },

                        //    max: function (value) {
                        //        return Math.ceil(value.max);
                        //    }
                        //},
                        series: [
                            {
                                name: '检验值',
                                type: 'scatter',
                                data: JSON.parse(arr[0]),
                                markLine: {
                                    data: [
                                        { yAxis: arr[2], name: '上限' },
                                        { yAxis: arr[3], name: '下限' },
                                        { yAxis: arr[4], name: '中线' }
                                    ]
                                }
                            },
                            {
                                name: '平均值',
                                type: 'line',
                                data: JSON.parse(arr[1]),
                            }
                        ]
                    };
                    //获取dom容器
                    debugger;
                    var myChart = echarts.init(document.getElementById('pnlLine'));
                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert("提示", errorMsg);
                    return false;
                },
                eventMask: {
                    showMask: true
                }
            });
            return false;
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
            <ext:Hidden runat="server" ID="hiddenMaterialCode" />
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <%--<DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click" Timeout="600000">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>--%>
                                <Listeners>
                                    <Click Handler="refreshChart(false); return false;" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenter}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenter}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                            <ext:Parameter Name="fieldsAVG" Value="#{ModelAvg}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="recordsAVG" Value="#{StoreAvg}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Container ID="container1" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                        <Items>
                            <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期" 
                                LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" >
                            </ext:DateField>
                            <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" 
                                LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" >
                            </ext:DateField>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container2" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                        <Items>
                            <ext:ComboBox runat="server" ID="ComboBoxCheckItem" FieldLabel="检验项目" LabelAlign="Right" 
                                Editable="false">
                               <%-- <Listeners>
                                    <Change Fn="Fuzhi"/>
                                </Listeners>--%>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxCheckType" FieldLabel="检验类型" LabelAlign="Right" 
                                Editable="false">
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container3" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                        <Items>
                            <ext:ComboBox runat="server" ID="ComboBoxEquip" FieldLabel="机台" LabelAlign="Right" MultiSelect="true" 
                                Editable="false">
                            </ext:ComboBox>
                            <ext:TriggerField runat="server" ID="txMaterialName" FieldLabel="物料名称" LabelAlign="Right" Editable="false" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="QueryMaterialInfo" />
                                    <%--<Change Fn="Fuzhi"/>--%>
                                </Listeners>
                            </ext:TriggerField>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container5" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                        <Items>
                            <ext:Button runat="server" Text="查询上下限" ID="btn_add" style="float:right">
                                <DirectEvents>
                                    <Click OnEvent="ChangeFuzhi">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container4" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                        <Items>
                            <ext:TextField ID="txtSX" runat="server" FieldLabel="上限" LabelAlign="Right" />
                            <ext:TextField ID="txtXX" runat="server" FieldLabel="下限" LabelAlign="Right" />
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout" Flex="25" Title="检验数据">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                            <Fields>
                                                <ext:ModelField Name="plandate" />
                                                <ext:ModelField Name="shiftname" />
                                                <ext:ModelField Name="equipname" />
                                                <ext:ModelField Name="kind" />
                                                <ext:ModelField Name="serialid" />
                                                <ext:ModelField Name="checkvalue" />
                                            </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                    <ext:Column ID="Column1" DataIndex="plandate" runat="server" Text="日期" Align="Center" Width="100" />
                                    <ext:Column ID="Column2" DataIndex="shiftname" runat="server" Text="班次" Align="Center" Width="150"/>
                                    <ext:Column ID="Column3" DataIndex="equipname" runat="server" Text="机台" Align="Center" Width="100" />
                                    <ext:Column ID="Column4" DataIndex="kind" runat="server" Text="类型" Align="Center" Width="150"/>
                                    <ext:Column ID="Column5" DataIndex="serialid" runat="server" Text="车次" Align="Center" Width="100" />
                                    <ext:Column ID="Column6" DataIndex="checkvalue" runat="server" Text="检验值" Align="Center" Width="150"/>
                                    
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="Panel2" Region="East" Layout="FitLayout" Flex="10" Title="平均值">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelAvg" ColumnLines="true" AnchorHeight="100">
                        <Store>
                            <ext:Store runat="server" ID="StoreAvg">
                                <Model>
                                    <ext:Model runat="server" ID="ModelAvg">
                                            <Fields>
                                                <ext:ModelField Name="plandate" />
                                                <ext:ModelField Name="checkvalue" />
                                            </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                    <ext:Column ID="Column13" DataIndex="plandate" runat="server" Text="日期" Align="Center" Width="100" />
                                    <ext:Column ID="Column14" DataIndex="checkvalue" runat="server" Text="检验值" Align="Center" Width="150"/>
                                    
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlLine" runat="server" Region="East" Layout="FitLayout"  Flex="25" >
                <%--<Content>
                    <div id="divLine" style="border:2px solid #666;width:49%;height:450px;float:left;" />
                </Content>--%>
            </ext:Panel>
            <ext:Panel ID="Panel1" runat="server" Region="East" Layout="FitLayout"  Flex="25" Title="曲线图" Hidden="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barImage">
                        <Items>
                            <ext:ToolbarSeparator ID="toolbarSeparator_start1" />
                            <ext:Button runat="server" Icon="Image" Text="导出-曲线" ID="btnExportImage" Hidden="true" >
                                <Listeners>
                                    <Click Fn="saveChart">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                            <ext:ToolbarFill ID="toolbarFill1" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Chart ID="Chart1" runat="server" StyleSpec="background:#fff;" Shadow="true"
                        StandardTheme="Category1" Animate="true">
                        <LegendConfig Position="Right" />
                        <Store>
                            <ext:Store ID="Store5" runat="server">
                                <Model>
                                    <ext:Model ID="Model5" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="plandate"  Type="Date" DateFormat="yyyy-MM-dd"/>
                                            <ext:ModelField Name="checkvalue" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <Axes>
                            <ext:NumericAxis Position="Left" Grid="true" Title="检验值" MinorTickSteps="1" />
                            <%--<ext:NumericAxis Position="Right" Grid="true" Title="平均值" />--%>
                            <ext:TimeAxis Position="Bottom" Grid="true" Title="日期" DateFormat="yyyy-MM-dd" />
                        </Axes>
                        <Series>
                            <ext:LineSeries Axes="Left" ShowMarkers="true" XField="plandate" YField="checkvalue" Title="检验值" >
                                <%--<Tips ID="Tips1" runat="server" TrackMouse="true" Width="80" Height="100">
                                    <Renderer Handler="TipsRenderer(this,storeItem); ">
                                    </Renderer>
                                </Tips>--%>
                            </ext:LineSeries>
                            <%--<ext:LineSeries Axes="Right" ShowMarkers="true" XField="plandate" YField="checkvalue" Title="平均值">
                            </ext:LineSeries>--%>
                        </Series>
                    </ext:Chart>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
