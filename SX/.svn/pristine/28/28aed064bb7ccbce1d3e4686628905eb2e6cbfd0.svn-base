<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_RubberStorageInfo, App_Web_dobmrtlx" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .total-field{
            background-color : #00ff00;                        
            padding          : 0px 1px 1px 1px;
            margin-right     : 1px;   
        }
         
        .total-field .x-form-display-field{            
            border           : solid 1px silver;
            font-weight      : bold !important;                       
            font-size        : 11px;
            font-family      : tahoma, arial, verdana, sans-serif;
            color            : #000000;  
            padding          : 0px 0px 0px 5px;
            height: 22px;
        } 
    </style>
    <script type="text/javascript">
        //库存预警提示
        var realWeightChange = function (value) {
            if (value <= "100")
                return Ext.String.format('<div style="color:green;font-weight:bolder;" title="预警：库存量偏小！">{0}</div>', value);
            else if (value >= "19000")
                return Ext.String.format('<div style="color:red;font-weight:bolder;" title="预警：库存量已达到饱和！">{0}</div>', value);
            else
                return Ext.String.format('<div style="color:black;font-weight:normal;">{0}</div>', value);
        }

        //有效期报警
        var validDateChange = function (value) {
            if (value != null && value != "") {
                if (parseInt(Math.abs(new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 30)
                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="预警：即将到达保质期，请尽快使用！">{0}</div>', value);
                else
                    return Ext.String.format('<div style="color:black;font-weight:normal;">{0}</div>', value);
            }
        }

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项格式为正整数"
        });

        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var startTrack = function () {
            this.checkboxes = [];
            var cb;
            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            App.pageToolBar.doRefresh();
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            App.txtStoragePlaceName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            App.pageToolBar.doRefresh();
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
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            App.txtEquipCode.setValue(record.data.EquipName);
            App.txtEquipCode.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            App.pageToolBar.doRefresh();
        }

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })
        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台名称",
            modal: true
        })

        var QueryStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    App.txtStoragePlaceName.setValue("");
                    App.hiddenStoragePlaceID.setValue("");
                    App.txtStoragePlaceName.getTrigger(0).hide();
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var QueryStoragePlace = function (field, trigger, index) {//库位添加
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }

            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStoragePlaceID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=2&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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

        var stockFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "已出库" : "未出库");
        };

        var OperTypeChange = function (value) {
            if (value == "001")
                return Ext.String.format("生产");
            else if (value == "002")
                return Ext.String.format("调拨");
            else if (value == "003")
                return Ext.String.format("外发");
            else if (value == "004")
                return Ext.String.format("外供");
            else if (value == "005")
                return Ext.String.format("外送");
            else if (value == "006")
                return Ext.String.format("消耗");
            else if (value == "007")
                return Ext.String.format("退回");
            else if (value == "008")
                return Ext.String.format("返回");
            else if (value == "009")
                return Ext.String.format("废品");
            else if (value == "010")
                return Ext.String.format("架子拆分");
            else if (value == "011")
                return Ext.String.format("盘点");
        };

        var gridPanelCellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            //alert(record.data.BillType);
            if (record.data.BillType == "1001") {//入库单详情
                var url = "Storage/MaterialStoreIn.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialStoreIn";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "入库单查询", url, true);
            }
            if (record.data.BillType == "1002") {//出库单详情
                var url = "Storage/MaterialStoreOut.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialStoreOut";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "出库单查询", url, true);
            }
            if (record.data.BillType == "1003") {//调拨单详情
                var url = "Storage/MaterialStorageAdjust.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialStorageAdjust";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "调拨单查询", url, true);
            }
            if (record.data.BillType == "1004") {//退货单详情
                var url = "Storage/MaterialReturn.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialReturn";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "退货单查询", url, true);
            }
            if (record.data.BillType == "1005") {//退库单详情
                var url = "Storage/MaterialReturnIn.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialReturnIn";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "退货单查询", url, true);
            }
            if (record.data.BillType == "1006") {//调整单详情
                var url = "Storage/MaterialStorageAdjusting.aspx?BillNo=" + record.data.SourceBillNo + "&Barcode=" + record.data.Barcode + "&OrderID=" + record.data.SourceOrderID;
                var tabid = "Manager_Storage_MaterialStorageAdjusting";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "退货单查询", url, true);
            }
        }

        var doSort = function () {
            App.store.sort(getSorters());
        };

        var getSorters = function () {
            var sorters = [];

            Ext.each(App.Toolbar1.query('button'), function (button) {
                sorters.push(button.sortData);
            }, this);

            return sorters;
        };

        var changeSortDirection = function (button, changeDirection) {
            var sortData = button.sortData,
                iconCls = button.iconCls;

            if (sortData) {
                if (changeDirection !== false) {
                    button.sortData.direction = Ext.String.toggle(button.sortData.direction, "ASC", "DESC");
                    button.setIconCls(Ext.String.toggle(iconCls, "icon-sortascending", "icon-sortdescending"));
                }
                App.store.clearFilter();
                doSort();
            }
        };

        var createSorterButtonConfig = function (config) {
            config = config || {};

            Ext.applyIf(config, {
                listeners: {
                    click: function (button, e) {
                        changeSortDirection(button, true);
                    }
                },
                iconCls: config.sortData.direction.toLowerCase() == "asc" ? "icon-sortascending" : "icon-sortdescending",
                reorderable: true,
                xtype: 'splitbutton',
                menu: {
                    xtype: "menu",
                    items: [
                        {
                            text: "移除",
                            handler: remove
                        }
                    ]
                }
            });

            return config;
        };

        var createItem = function (data) {
            var header = data.header,
                headerCt = header.ownerCt,
                reorderer = headerCt.reorderer;

            if (reorderer) {
                reorderer.dropZone.invalidateDrop();
            }

            return createSorterButtonConfig({
                text: header.text,
                sortData: {
                    property: header.dataIndex,
                    direction: "ASC"
                }
            });
        };

        var canDrop = function (dragSource, event, data) {
            var sorters = getSorters(),
                header = data.header,
                length = sorters.length,
                entryIndex = this.calculateEntryIndex(event),
                targetItem = this.toolbar.getComponent(entryIndex),
                i;

            if (!header.dataIndex || (targetItem && targetItem.reorderable === false)) {
                return false;
            }

            for (i = 0; i < length; i++) {
                if (sorters[i].property == header.dataIndex) {
                    return false;
                }
            }

            return true;
        };

        var remove = function (menu) {
            App.Toolbar1.remove(menu.up('button'));
            App.store.clearFilter();
            doSort();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpStorage" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnStorage" runat="server" Region="North" Height="120">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbStorage">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                              <ext:Button runat="server" ID="ButtonFX" Icon="PageEdit" Text="放行">
                                <DirectEvents>
                                    <Click OnEvent="ButtonFX_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                   
                                </DirectEvents>
                                 
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonQXFX" Icon="PageDelete" Text="取消放行">
                                <DirectEvents>
                                    <Click OnEvent="ButtonQXFX_Click">
                                        <EventMask ShowMask="true" />
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
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Left" LabelPad="-40"/>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="Search_Change" LabelAlign="Left" LabelPad="-40"/>
                                    <ext:ComboBox ID="cbxShiftClass" runat="server" OnDirectChange="Search_Change" FieldLabel="班组" LabelAlign="Left" LabelPad="-40">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                            <ext:ListItem Text="甲组" Value="1"></ext:ListItem>
                                            <ext:ListItem Text="乙组" Value="2"></ext:ListItem>
                                            <ext:ListItem Text="丙组" Value="3"></ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Left" Flex="1" Editable="false" LabelPad="-40">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="Search_Change" LabelAlign="Left" LabelPad="-40"/>
                                    <ext:ComboBox ID="cbxShift" runat="server" OnDirectChange="Search_Change" FieldLabel="班次" LabelAlign="Left" LabelPad="-40">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                            <ext:ListItem Text="早班" Value="3"></ext:ListItem>
                                            <ext:ListItem Text="中班" Value="1"></ext:ListItem>
                                            <ext:ListItem Text="夜班" Value="2"></ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Left" Flex="1" Editable="false" LabelPad="-40">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Left" Editable="false" LabelPad="-40">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryMaterial" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:Checkbox ID="cbxScreen" runat="server" BoxLabelAlign="After" OnDirectCheck="Search_Change" BoxLabel="显示库存为0的数据" Checked="false" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtEquipCode" runat="server" FieldLabel="机台" LabelAlign="Left" Editable="false" LabelPad="-40">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryEquipInfo" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:ComboBox ID="cbxStock" OnDirectChange="Search_Change" runat="server" FieldLabel="是否出库" LabelAlign="Left" LabelPad="-40">
                                        <%--<SelectedItems>
                                            <ext:ListItem Value="0"></ext:ListItem>
                                        </SelectedItems>--%>
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                            </ext:ListItem>
                                            <ext:ListItem Text="是" Value="1">
                                            </ext:ListItem>
                                            <ext:ListItem Text="否" Value="0">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>

            <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlList" runat="server">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode,StorageID,StoragePlaceID">
                                        <Fields>
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="ShelfBarcode" />
                                            <ext:ModelField Name="BarcodeStart" />
                                            <ext:ModelField Name="BarcodeEnd" />
                                            <ext:ModelField Name="Checi" />
                                            <ext:ModelField Name="ShelfNum" />
                                            <ext:ModelField Name="RealNum" />
                                            <ext:ModelField Name="Mem_Note" />
                                            <ext:ModelField Name="PlanDate" Type="Date" />
                                            <ext:ModelField Name="ShiftID" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ShiftClassID" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="EquipCode" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ValidDate" Type="Date" />
                                            <ext:ModelField Name="ProductWeight" Type="Float" />
                                            <ext:ModelField Name="StockFlag" />
                                            <ext:ModelField Name="CheckFlag" />
                                            <ext:ModelField Name="TecDealFlag" />
                                            <ext:ModelField Name="TecDealIdea" />
                                            <ext:ModelField Name="ConsumeWeight" Type="Float" />
                                            <ext:ModelField Name="RealWeight" Type="Float" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="OperPerson" />
                                             <ext:ModelField Name="fxflag" />
                                              <ext:ModelField Name="ShiftBarcode" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="Checi" runat="server" Text="车次" DataIndex="Checi" Flex="1" />
                                <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="PlanDate" runat="server" Text="生产日期" DataIndex="PlanDate" Flex="1" Format="yyyy-MM-dd" />
                                <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Flex="1" />
                                <ext:DateColumn ID="ValidDate" runat="server" Text="保质期" DataIndex="ValidDate" Flex="1" Format="yyyy-MM-dd" />
                                <ext:Column ID="Column1" runat="server" Text="原始数量" DataIndex="ShelfNum" Flex="1" />
                       <%--         <ext:Column ID="Column2" runat="server" Text="剩余数量" DataIndex="RealNum" Flex="1" />--%>
                                <ext:Column ID="RealWeight" runat="server" Text="实际重量" DataIndex="RealWeight" Flex="1" />
                                <ext:Column ID="StockFlag" runat="server" Text="出库状态" DataIndex="StockFlag" Flex="1">
                                    
                                    <Renderer Fn="stockFlagChange" />
                                </ext:Column>
                                  <ext:Column ID="Column4" runat="server" Text="RFID" DataIndex="ShiftBarcode" Flex="1"/>
                                   <ext:Column ID="Column2" runat="server" Text="放行标志" DataIndex="fxflag" Flex="1" />
                             
                            </Columns>
                        </ColumnModel>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="排序方式:" Reorderable="false" />
                                    <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" Reorderable="false" />
                                    <ext:SplitButton ID="SplitButton1" runat="server" Text="机台" Icon="SortAscending"
                                        OnClientClick="changeSortDirection(this, true);" SortData="={{property:'EquipName',direction:'ASC'}}" Reorderable="true">
                                        <Menu>
                                            <ext:Menu ID="Menu1" runat="server">
                                                <Items>
                                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="移除" OnClientClick="remove(this);" />
                                                </Items>
                                            </ext:Menu>
                                        </Menu>
                                    </ext:SplitButton>
                                    <ext:SplitButton ID="SplitButton2" runat="server" Text="物料名称" Icon="SortAscending"
                                        OnClientClick="changeSortDirection(this, true);" SortData="={{property:'MaterialName',direction:'DESC'}}" Reorderable="true">
                                        <Menu>
                                            <ext:Menu ID="Menu2" runat="server">
                                                <Items>
                                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="移除" OnClientClick="remove(this);" />
                                                </Items>
                                            </ext:Menu>
                                        </Menu>
                                    </ext:SplitButton>
                                    <ext:SplitButton ID="SplitButton3" runat="server" Text="保质期" Icon="SortDescending"
                                        OnClientClick="changeSortDirection(this, true);" SortData="={{property:'ValidDate',direction:'DESC'}}" Reorderable="true">
                                        <Menu>
                                            <ext:Menu ID="Menu3" runat="server">
                                                <Items>
                                                    <ext:MenuItem ID="MenuItem3" runat="server" Text="移除" OnClientClick="remove(this);" />
                                                </Items>
                                            </ext:Menu>
                                        </Menu>
                                    </ext:SplitButton>
                                </Items>
                                <Plugins>
                                    <ext:BoxReorderer ID="BoxReorderer1" runat="server">
                                        <Listeners>
                                            <Drop Handler="changeSortDirection(dragCmp, false);" />
                                        </Listeners>
                                    </ext:BoxReorderer>
                                    <ext:ToolbarDroppable ID="ToolbarDroppable1" runat="server">
                                        <CreateItem Fn="createItem" />
                                        <CanDrop Fn="canDrop" />
                                        <Listeners>
                                            <Drop Fn="doSort" />
                                        </Listeners>
                                    </ext:ToolbarDroppable>
                                </Plugins>
                            </ext:Toolbar>
                        </TopBar>
                        <Listeners>
                            <AfterLayout Handler="#{ToolbarDroppable1}.addDDGroup(this.child('headercontainer').reorderer.dragZone.ddGroup); doSort();" Single="true" />
                        </Listeners>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                <Listeners>
                                    <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                </Listeners>
                            </ext:RowSelectionModel>
                            
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Items>
                                    <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="10" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                              <%-- <ext:TextField ID="txtTotalNum" runat="server" LabelAlign="Right" ReadOnly="true" />--%>
                                    <ext:TextField ID="txtTotalWeight" runat="server" LabelAlign="Right" ReadOnly="true" />
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="库存明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <%-- <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDetail" />
                                </Proxy>--%>
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="Barcode, OrderID">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="Banci" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="Num" />
                                            <ext:ModelField Name="Weight" Type="Float" />
                                            <ext:ModelField Name="OperType" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="Barcode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.Barcode : -1" />
                                    <ext:StoreParameter Name="StorageID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StorageID : -1" />
                                    <ext:StoreParameter Name="StoragePlaceID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StoragePlaceID : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                
                                <ext:DateColumn ID="RecordDate1" runat="server" Text="执行日期" DataIndex="RecordDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ShiftName1" runat="server" Text="班次班组" DataIndex="Banci" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <ext:Column ID="Num" runat="server" Text="数量" DataIndex="Num" Flex="1" />
                                <ext:Column ID="Weight1" runat="server" Text="重量" DataIndex="Weight" Flex="1" />
                                <ext:Column ID="StorageName1" runat="server" Text="发往库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName1" runat="server" Text="发往名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="OperType1" runat="server" Text="操作类型" DataIndex="OperType" Flex="1">
                                    <Renderer Fn="OperTypeChange" />
                                </ext:Column>
                             </Columns>
                        </ColumnModel>
                        <%--<Listeners>
                            <CellDblClick Fn="gridPanelCellDblClick" />
                        </Listeners>--%>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="10" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{pnlDetailList}.store.pageSize = parseInt(this.getValue(), 10); #{PagingToolbar1}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>

            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOrderColumn" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>

             <ext:Hidden ID="FBarcode" runat="server"></ext:Hidden>
            <ext:Hidden ID="FStoragePlaceID" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
