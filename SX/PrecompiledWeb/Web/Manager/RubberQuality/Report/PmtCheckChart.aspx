<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Report_PmtCheckChart, App_Web_liweduvv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="../../../resources/js/highcharts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../../../resources/js/highcharts/highcharts.js"></script>
    <script type="text/javascript" src="../../../resources/js/highcharts/modules/exporting.js"></script>
    <script type="text/javascript">
        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }
        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
        }
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.WorkBarcode);
            App.pageToolBar.doRefresh();
        }
        Ext.create("Ext.window.Window", {//人员信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })
        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMakerPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }
        function TipsRenderer(tip, record) {
            var ss = "T1：" + record.get('T1') + '<br />';
            ss = ss + "T2：" + record.get('T2') + '<br />';
            tip.setTitle(ss);
        }
        function TipsRenderer(tip, record) {
            var ss = "时间：" + record.get('天数') + '<br />';
            ss = ss + "中值：" + record.get('中值') + '<br />';
            ss = ss + "上限：" + record.get('上限') + '<br />';
            ss = ss + "下限：" + record.get('下限') + '<br />';
            <%=TipsRenderer %>
            tip.setTitle(ss);
        }
         function saveChart (btn) {
             var url = '../../ReportCenter/SvgToImage.aspx';
            Ext.MessageBox.confirm('提示', '确定将当前的曲线图导出为图片？', function (choice) {
        if (choice == 'yes') {
            App.Chart2.save({
                type: 'image/png',
                url: url
            });
        }
    });
        }
    </script>
    <script type="text/javascript">
        var chart;
        function newchart()
        {
            chart = new Highcharts.Chart({
            chart: {
                renderTo:'cont',
                type: 'line',
                shadow:'true',
                marginRight: 130,
                marginBottom: 25,
                width:<%=width %>,
                height:700
            },
            title: {
                text: '质检数据统计',
                x: -20 //center
            },
            subtitle: {
                text: '',
                x: -20
            },
            xAxis: {
                categories: [<%=days %>],
                labels:{rotation:-10,align:'right'}
            },
            yAxis: {
                title: {
                    text: ''
                },
                tickInterval: <%=tickInterval %>,
                min:<%=ymin %>,
                max:<%=ymax %>,
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function() {
                        return '<b>'+ this.series.name +'</b><br/>'+
                        this.x +': '+ this.y;               
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
                }
                
                <%=series %>
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMmShopout">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" AutoPostBack="true" OnClick="btnSearch_Click" Shadow="true" Icon="Find" Text="查询" ID="btnSearch">
                                <DirectEvents>
                                    <Click>
                                        <EventMask ShowMask="true" Msg="查询中.." MinDelay="50">
                                        </EventMask>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server" AllowBlank="false" FieldLabel="开始时间" LabelAlign="Right" Editable="false" />
                                            <ext:DateField ID="txtEndTime" runat="server" AllowBlank="false" FieldLabel="结束时间" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtMaterName" runat="server" AllowBlank="false" FieldLabel="胶号" LabelAlign="Right" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                 <Listeners>
                                                    <TriggerClick Fn="QueryMaterial" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:ComboBox ID="cbxXiang" AllowBlank="false" runat="server" FieldLabel="项目" LabelAlign="Right" Editable="false">
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                           <ext:ComboBox ID="cbxWorkCode" EmptyText="全部" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                            <ext:TriggerField ID="txtUserName" runat="server" FieldLabel="主机手" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                 <Listeners>
                                                        <TriggerClick Fn="QueryUser" />
                                                    </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxType" runat="server" EmptyText="车间对比" FieldLabel="统计分类" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="车间对比" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="机台对比" Value="1">
                                                    </ext:ListItem>
                                                     <ext:ListItem Text="主机手对比" Value="2">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Container Region="Center" OverflowX="Scroll" OverflowY="Scroll" runat="server">
                <Content>
                    <div id="cont" style="width: 100%; overflow: scroll;">
                        
                    </div>
                </Content>
            </ext:Container>
               
            <%--<ext:Chart ID="Chart2"  Region="Center"  Frame="true" Shadow="true" runat="server" StyleSpec="background:#fff;font-size:9pt"  Animate="true">
                <LegendConfig Position="Right" />
                <Store>
                    <ext:Store ID="Store2" runat="server">
                        <Model>
                            <ext:Model ID="Model2" runat="server">
                                <Fields>
                                     <ext:ModelField Name="天数" />
                                     <ext:ModelField Name="中值" />
                                     <ext:ModelField Name="上限"/>
                                     <ext:ModelField Name="下限"/>
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <Axes>
                    <ext:NumericAxis Position="Left" Grid="true" />
                    <ext:NumericAxis Position="Bottom" Grid="true" Minimum="0" MinorTickSteps="1" MajorTickSteps="1" />
                </Axes>
                <Series>
                    <ext:LineSeries Axis="Left" XField="天数" YField="中值" AutoDataBind="false">
                        <Tips runat="server" TrackMouse="true" Width="140" Height="180">
                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                            </Renderer>
                        </Tips>
                    </ext:LineSeries>
                        <ext:LineSeries  Axis="Left" XField="天数" YField="上限" AutoDataBind="false">
                        <Tips runat="server" TrackMouse="true" Width="140" Height="180">
                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                            </Renderer>
                        </Tips>
                    </ext:LineSeries>
                    <ext:LineSeries Axis="Left" XField="天数" YField="下限" AutoDataBind="false">
                        <Tips runat="server" TrackMouse="true" Width="140" Height="180">
                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                            </Renderer>
                        </Tips>
                     </ext:LineSeries>
                </Series>
            </ext:Chart>--%>
        </Items>
    </ext:Viewport>
  
    <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
    <ext:Hidden ID="hiddenMaterCode" runat="server" />
    </form>
    <script type="text/javascript">
        newchart();
    </script>
</body>
</html>
