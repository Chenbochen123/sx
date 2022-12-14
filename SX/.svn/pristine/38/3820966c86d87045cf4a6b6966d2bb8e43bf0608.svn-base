<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_Report_CompoundOutput, App_Web_wycfotos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .indoor-g
        {
            color: Blue;
        }
    </style>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        function TimeCom(dateValue) {
            var newCom = new Date(dateValue);
            this.year = newCom.getYear();
            this.month = newCom.getMonth() + 1;
            this.day = newCom.getDate();
            this.hour = newCom.getHours();
            this.minute = newCom.getMinutes();
            this.second = newCom.getSeconds();
            this.msecond = newCom.getMilliseconds();
            this.week = newCom.getDay();
        }
        var getRowClass = function () { 
            
        }

        var pnlListFresh = function () {
            //判断开始日期是否大于结束日期
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            //判断日期之间间隔是否大于30天
            if ((App.txtEndTime.getValue().getTime() - App.txtBeginTime.getValue().getTime()) / 3600000 / 24 > 30) {
                Ext.Msg.alert('操作', '开始时间和结束时间间隔不能大于30天！');
                return false;
            }
            App.store.reload();
            App.storeDetail.reload();
            return false;
        }
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            var name = App.hldname.value;
            if (name == "txtStorageName") {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "txtToStorageName") {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            } 

        }
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            var name = App.hldname.value;

            if (name == "txtStoragePlaceName") {
                App.txtStoragePlaceName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "txtToStoragePlaceName") {
                App.txtToStoragePlaceName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            } 
        }
        var QueryEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.store.reload();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            App.txtEquipName.setValue(record.data.EquipName);
            App.txtEquipName.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            //App.pageToolBar.doRefresh();
        }
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            //App.pageToolBar.doRefresh();
        }
        var QueryStorage = function (field, trigger, index) {//库房添加
            App.hldname.setValue(field.name);
            switch (index) {
                case 0:
                    var name = App.hldname.value;
                    if (name == "txtStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtStoragePlaceName.setValue("");
                        App.txtStoragePlaceName.getTrigger(0).hide();
                        App.hiddenStorageID.setValue("");
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "txtToStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtToStorageName.setValue("");
                        App.hiddenToStorageID.setValue("");
                        App.hiddenToStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }


        var QueryStoragePlace = function (field, trigger, index) {//库位添加
            App.hldname.setValue(field.name);
            var url = "../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }
            switch (index) {
                case 0:

                    if (field.name == "txtStoragePlaceName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");

                    } else if (field.name == "txtToStoragePlaceName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenToStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:

                    App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
                    break;
            }
        }
        Ext.create("Ext.window.Window", {//机台带窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台名称",
            modal: true
        })
        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?minMajorTypeID=2' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })
        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=all&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        Ext.create("Ext.window.Window", {//库位带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window",
            height: 460,
            hidden: true,
            width: 360,
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库位",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmReport" runat="server" />
    <ext:Viewport ID="vpReport" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnReportTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbReport">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlReportByClass" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:Checkbox ID="cbx1" runat="server"  BoxLabelAlign="After" Checked="true" BoxLabel="生产类" />
                                            <ext:Checkbox ID="cbx2" runat="server" BoxLabelAlign="After" BoxLabel="调拨类" />
                                            <ext:Checkbox ID="cbx3" runat="server"  BoxLabelAlign="After" BoxLabel="发货" />
                                            <ext:Checkbox ID="cbx4" runat="server"  BoxLabelAlign="After" BoxLabel="消耗类" />
                                            <ext:Checkbox ID="cbx5" runat="server" BoxLabelAlign="After" BoxLabel="胶料退回" />
                                            <ext:Checkbox ID="cbx6" runat="server"  BoxLabelAlign="After" BoxLabel="返回胶" />
                                            <ext:Checkbox ID="cbx7" runat="server"  BoxLabelAlign="After" BoxLabel="废品出入库" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                            <ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="ctnBeginTime" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right">
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
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" Editable="false"
                                                LabelAlign="Right" />
                                            <ext:ComboBox ID="cbxShiftClass" runat="server" FieldLabel="班组" LabelAlign="Right">
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="甲组" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="乙组" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="丙组" Value="3">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnEndTime" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right"
                                                Flex="1" Editable="false" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Editable="false"
                                                LabelAlign="Right" />
                                              <ext:ComboBox ID="cbxShift" runat="server"  FieldLabel="班次"
                                                LabelAlign="Right">
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="0">
                                                    </ext:ListItem>
                                                     <ext:ListItem Text="中班" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="夜班" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="早班" Value="3">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnEquipName" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStoragePlace" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryMaterial" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnMaterName" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:TriggerField ID="txtEquipName" runat="server" FieldLabel="机台" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:ComboBox ID="cbxStock" runat="server" FieldLabel="是否出库"
                                                LabelAlign="Right" >
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="0" AutoDataBind="true">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="是" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="否" Value="0">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                           <ext:TriggerField ID="txtToStorageName" runat="server" FieldLabel="发往库房" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:TriggerField ID="txtToStoragePlaceName" runat="server" FieldLabel="发往库位" LabelAlign="Right"
                                                Flex="1" Editable="false" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStoragePlace" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>

            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" AutoLoad="false">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="类型" />
                                    <ext:ModelField Name="库房" />
                                    <ext:ModelField Name="库位" />
                                    <ext:ModelField Name="机台" />
                                    <ext:ModelField Name="物料" />
                                    <ext:ModelField Name="发往库房" />
                                    <ext:ModelField Name="发往库位" />
                                    <ext:ModelField Name="操作人" />
                                    <ext:ModelField Name="重量" />
                                    <ext:ModelField Name="车数" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="col1" runat="server" Text="类型" DataIndex="类型" Flex="1" />
                        <ext:Column ID="col2" runat="server" Text="库房" DataIndex="库房" Flex="1" />
                        <ext:Column ID="col3" runat="server" Text="库位" DataIndex="库位" Flex="1" />
                        <ext:Column ID="col4" runat="server" Text="机台" DataIndex="机台" Flex="1" />
                        <ext:Column ID="col5" runat="server" Text="物料" DataIndex="物料" Flex="1" />
                        <ext:Column ID="col6" runat="server" Text="发往库房" DataIndex="发往库房" Flex="1" />
                        <ext:Column ID="Column1" runat="server" Text="发往库位" DataIndex="发往库位" Flex="1" />
                        <ext:Column ID="Column2" runat="server" Text="操作人" DataIndex="操作人" Flex="1" />
                        <ext:Column ID="Column3" runat="server" Text="重量" DataIndex="重量" Flex="1" />
                        <ext:Column ID="Column4" runat="server" Text="车数" DataIndex="车数" Flex="1" />
                    </Columns>
                </ColumnModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager"   runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="汇总数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true"  Split="true">
                <Items>
                        <ext:GridPanel ID="pnlDetailList"  Cls="x-grid-custom" runat="server">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" AutoLoad="false">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridTotalPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="物料" />
                                            <ext:ModelField Name="人员" />
                                            <ext:ModelField Name="类型" />
                                            <ext:ModelField Name="重量" />
                                            <ext:ModelField Name="车数" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />           
                                <ext:Column ID="co2" runat="server" Text="类型" DataIndex="类型" Flex="1" />
                                <ext:Column ID="co3" runat="server" Text="物料" DataIndex="物料" Flex="1" />
                                <ext:Column ID="co4" runat="server" Text="人员" DataIndex="人员" Flex="1" />
                                <ext:Column ID="co5" runat="server" Text="重量" DataIndex="重量" Flex="1" />
                                <ext:Column ID="co6" runat="server" Text="车数" DataIndex="车数" Flex="1" />
                             </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:Hidden ID="hiddenEquipCode" runat="server" />
    <ext:Hidden ID="hiddenMaterCode" runat="server" />
    <ext:Hidden ID="hiddenStorageID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenStoragePlaceID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenToStorageID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenToStoragePlaceID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hldname" runat="server">
    </ext:Hidden>
    </form>
</body>
</html>
