<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanSumDayMater_PlanSumDayMater, App_Web_y0f3nddi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生产计划领料单</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.Store1.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }


        var SelectStoreCodeID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_store_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_store.setValue("Store");
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {  //库房返回值处理
            var type = $("#hidden_select_store").val();
            App.txtStoreCode.getTrigger(0).show();
            if (type == "Store") {

                App.txtStoreCode.setValue(record.data.StorageName);
                App.hidden_select_store_code.setValue(record.data.StorageID);
            }
        }
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择仓库",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Hidden ID="hidden_select_store_code" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_store" runat="server">
    </ext:Hidden>
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
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
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="导出" ID="ctl350" />
                                </ToolTips>
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
                                            <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="生产开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboType" Editable="false" runat="server" FieldLabel="设备车间" LabelAlign="Right">
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="con1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtStoreCode" runat="server" Flex="1" Editable="false" FieldLabel="仓库名称"
                                                LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectStoreCodeID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center" Layout="FitLayout">
                <Store>
                    <ext:Store ID="Store1" runat="server">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model2" runat="server">
                                <Fields>
                                    <ext:ModelField Name="物料名称" Type="String" />
                                    <ext:ModelField Name="库房" Type="String" />
                                    <ext:ModelField Name="需求重量" Type="Float" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel1" runat="server">
                    <Columns>
                        <ext:Column ID="Column1" runat="server" Text="物料名称" Width="150" DataIndex="物料名称">
                        </ext:Column>
                        <ext:Column ID="Column3" runat="server" Text="库房" Width="150" DataIndex="库房">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="需求重量" Width="60" DataIndex="需求重量">
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server" Hidden="true">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
