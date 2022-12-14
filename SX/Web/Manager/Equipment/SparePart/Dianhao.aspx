<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dianhao.aspx.cs" Inherits="Manager_Equipment_SparePart_Dianhao" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机台电耗日报</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/echarts.min.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/ecStat.js"></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #CCFF66 !important;
        }
    </style>
     <script type="text/javascript">

                 var refreshChart = function () {
                     // debugger;
                     App.direct.chartMainBindData({
                         success: function (result) {
                             var arr = result.split("@");
                            // debugger;
                             var datajiaban = JSON.parse(arr[0]);
                             var datayiban = JSON.parse(arr[1]);
                             var databingban = JSON.parse(arr[2]);
                             var datajizhun = JSON.parse(arr[3]);
                             drawBar(datajiaban, datayiban, databingban, datajizhun);
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

                 var divBarResize = function () {
                     var width = App.pnlBar.getWidth() - 10;
                     var height = App.pnlBar.getHeight();
                     $('#divBar').height(height);
                     $('#divBar').width(width);
                     resizechartbar();
                 }

                 var resizechartbar = function () {
                     chartBar.resize();
                 }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display: none" />--%>
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
                                <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                    <Listeners>
                                        <Click Handler="refreshChart(false); return false;" />
                                    </Listeners>
                                </ext:Button>
                                <%--<ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>--%>
                                <ext:ToolbarSeparator />
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
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                       <ext:DateField ID="daDateTime" runat="server" FieldLabel="开始日期" Width="300" Editable="false" AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                       <ext:DateField ID="daDateTimeEnd" runat="server" FieldLabel="结束日期" Width="300" Editable="false" AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                       <ext:ComBobox ID="cbxequip" runat="server" FieldLabel="机台" Width="300" Editable="false" AllowBlank="false" />
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" ID="PanelCenter" Region="North" Layout="FitLayout" Flex="1" Title="检验数据">
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
                <ext:Panel ID="Panel5" runat="server" Region="Center" Flex="1" Layout="FitLayout">
                    <Items>
                        <ext:Panel ID="pnlBar" runat="server" Header="false">
                            <Content>
                                <div id="divBar" style="width: 200px; height: 260px;" />
                            </Content>
                            <Listeners>
                                <Resize Handler="this.on('resize', divBarResize);"></Resize>
                            </Listeners>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
<script type="text/javascript">
    var chartBar = echarts.init(document.getElementById('divBar'), 'light');

    var drawBar = function (datajiaban, datayiban, databingban, datajizhun) {

        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                data: ['甲班', '乙班', '丙班', '基准']
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['1', '2', '3', '4', '5', '6', '7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','27','28','29','30','31']
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ,
            {
                type: 'value',
                position: 'right'
            }
            ],
            series: [
                {
                    name: '甲班',
                    type: 'bar',
                    stack: '1',
                    data: [],
                    markLine: {
                        data: [
                            { yAxis: datajizhun[0] }
                        ],
                        lineStyle: {
                            normal: {
                                type: 'solid',
                                color: '#ff0000',
                            },
                        },
                    }
                },
                {
                    name: '乙班',
                    type: 'bar',
                    stack: '2',
                    data: []
                },
                {
                    name: '丙班',
                    type: 'bar',
                    stack: '3',
                    data: []
                }
                
            ]
        };


        //var yArrNew = [];
        //for (var m = 1; m < dataheji.length + 1; m = m + 1) {
        //    var arr = [];
        //    arr.push(dataheji[m - 1]);
        //    yArrNew.push(arr);
        //}
      

        //var yArr = [];

        //for (var m = 1; m < dataheji.length + 1; m = m + 1) {
        //    var arr = [];
        //    arr.push(m.toFixed(2));
        //    arr.push(dataheji[m - 1]);
        //    yArr.push(arr);
        //}datajiaban, datayiban, databingban, datajizhun

        option.series[0].data = datajiaban;
        option.series[1].data = datayiban;
        option.series[2].data = databingban;
       // option.series[3].data = datajizhun;
       

        debugger;
        chartBar.setOption(option, true);
        divBarResize();
    }
</script>
</html>
