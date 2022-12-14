<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StopReasonDetail.aspx.cs" Inherits="Manager_Equipment_StopManage_StopReasonDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>停机数据分析</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/echarts.min.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/ecStat.js"></script>
    <script type="text/javascript">

        var refreshChart = function () {
            // debugger;
            App.direct.chartMainBindData({
                success: function (result) {
                    var arr = result.split("@");
                    //debugger;
                    //var dataX = arr[0];
                    //var dataY = arr[1];
                    var dataX = JSON.parse(arr[0]);
                    var dataY = JSON.parse(arr[1]);
                    drawBar(dataX, dataY);
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
                                    <Listeners>
                                        <Click Handler="refreshChart(false); return false;" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                            <Items>
                                <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期"
                                    LabelAlign="Right" Editable="false" Format="yyyy-MM-dd">
                                </ext:DateField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="container2" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                            <Items>
                                <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期"
                                    LabelAlign="Right" Editable="false" Format="yyyy-MM-dd">
                                </ext:DateField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="container3" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbxfenxi" FieldLabel="停机分析类型" LabelAlign="Right"
                                    Editable="true">
                                    <Items>
                                        <ext:ListItem Text="按停机时间分析（分钟）" Value="1">
                                        </ext:ListItem>
                                        <ext:ListItem Text="按停机频率分析" Value="2">
                                        </ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="container5" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                            <Items>
                                <ext:ComboBox runat="server" ID="cbxtype" FieldLabel="停机类别" LabelAlign="Right"
                                    Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Handler="this.setValue('');" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
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


    var drawBar = function (dataX, dataY) {

        option = {
            xAxis: {
                type: 'category',
                data: [],
            },
            yAxis: {
                type: 'value'
            },
            series: [{
                data: [],
                type: 'bar',
                showBackground: true,
                backgroundStyle: {
                    color: 'rgba(220, 220, 220, 0.8)'
                }
            }]
        };

        //var yArrNew = [];
        //for (var m = 1; m < dataY.length + 1; m = m + 1) {
        //    var arr = [];
        //    arr.push(dataY[m - 1]);
        //    yArrNew.push(arr);
        //};
        var xArrNew = [];
        for (var m = 1; m < dataX.length + 1; m = m + 1) {
            var arr = [];
            arr.push(dataX[m - 1]);
            xArrNew.push(arr);
        };



        option.series[0].data = dataY;
        option.xAxis.data = xArrNew;

        debugger;
        //option.series[0].data = dataY;
        //option.xAxis.data = dataX;
        chartBar.setOption(option, true);
        divBarResize();
    }
</script>
</html>
