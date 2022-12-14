<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarCodeLousao.aspx.cs"
    Inherits="Manager_ProducingPlan_BarcodeScan_BarCodeLousao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>条码漏扫统计</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            //App.Store1.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_equip_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_equip.setValue("Equip");
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
            var type = App.hidden_select_equip.getValue();
            App.txtEquipCode.getTrigger(0).show();
            if (type == "Equip") {
                App.txtEquipCode.setValue(record.data.EquipName);
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
            }
        }
        Ext.create("Ext.window.Window", {//机台信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 480,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display: none" />
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>
        <ext:Hidden ID="hidden_select_equip" runat="server">
        </ext:Hidden>
        <ext:Hidden ID="hidden_select_equip_code" runat="server">
        </ext:Hidden>
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="ctl320">
                            <Items>
                                <ext:ToolbarSeparator ID="ctl347" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh">
                                        </Click>
                                    </Listeners>
                                    <ToolTips>
                                        <ext:ToolTip runat="server" Html="查询" ID="ToolTip2" />
                                    </ToolTips>
                                </ext:Button>

                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtbegindate" runat="server" Editable="false" Vtype="daterange"
                                                    FieldLabel="开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                            <ext:TriggerField ID="txtEquipCode" runat="server" Flex="1" Editable="false" FieldLabel="机台"
                                                LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtenddate" runat="server" Editable="false" Vtype="daterange"
                                                    FieldLabel="结束日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxclass" runat="server" FieldLabel="班组" LabelAlign="Right" Width="300"
                                                    Editable="false">
                                                    <Items>
                                                        <ext:ListItem Text="甲" Value="1">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="乙" Value="2">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="丙" Value="3">
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
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" AnimCollapse="true">
                    <Store>
                        <ext:Store ID="Store2" runat="server" IgnoreExtraFields="false" PageSize="50">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="plan_Date" />
                                        <ext:ModelField Name="totalnum" Type="Int" />
                                        <ext:ModelField Name="ScanNum" Type="Int" />
                                        <ext:ModelField Name="EquipName" Type="String" />
                                        <ext:ModelField Name="shift_ClassName" Type="String" />
                                        <ext:ModelField Name="ShiftName" Type="String" />
                                        <ext:ModelField Name="lousaonum" Type="String" />
                                        <ext:ModelField Name="lousaolv" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:Column ID="Column3" runat="server" Text="日期" Flex="1" DataIndex="plan_Date">
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="机台" Flex="1" DataIndex="EquipName">
                            </ext:Column>
                            <ext:Column ID="Column5" runat="server" Text="班组" Flex="1" DataIndex="shift_ClassName">
                            </ext:Column>
                            <ext:Column ID="Column2" runat="server" Text="班次" Flex="1" DataIndex="ShiftName">
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="总数量" Flex="1" DataIndex="totalnum">
                            </ext:Column>
                            <ext:Column ID="Column10" runat="server" Text="已扫描数" Flex="1" DataIndex="ScanNum">
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="漏扫数" Flex="1" DataIndex="lousaonum">
                            </ext:Column>
                            <ext:Column ID="Column6" runat="server" Text="漏扫率%" Flex="1" DataIndex="lousaolv">
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Hidden ID="hidden_equip_type_name" runat="server" />
                <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
                </ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
