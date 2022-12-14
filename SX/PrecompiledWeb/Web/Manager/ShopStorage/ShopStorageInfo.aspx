<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_ShopStorageInfo, App_Web_ampjtxsw" %>
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
//            if (value <= "100")
//                return Ext.String.format('<div style="color:green;font-weight:bolder;" title="预警：库存量偏小！">{0}</div>', value);
//            else if (value >= "19000")
//                return Ext.String.format('<div style="color:red;font-weight:bolder;" title="预警：库存量已达到饱和！">{0}</div>', value);
//            else
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
            //            if (!App.winAdd.hidden) {
            //                App.txtStorageName.setValue(record.data.StorageName);
            //                App.hiddenStorageID.setValue(record.data.StorageID);
            //            }
            //            else if (!App.winModify.hidden) {
            //                App.hiddenBillNo.setValue(record.data.BillNo);
            //                App.hiddenStorageID.setValue(record.data.StorageID);
            //            }
            //            else {
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            //            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            //            if (!App.winAdd.hidden) {
            //                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            //                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            //            }
            //            else if (!App.winModify.hidden) {
            //                App.hiddenBillNo.setValue(record.data.BillNo);
            //                App.hiddenStoragePlaceID.setValue(record.data.FactoryID);
            //            }
            //            else {
            App.txtStoragePlaceName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            //            }
        }

        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
            App.txtFactoryID.setValue(record.data.FacName);
            App.hiddenFactoryID.setValue(record.data.ObjID);
        }

        var QueryFactory = function () {//厂家查询
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }

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

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
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

        var AddStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    App.txtStoragePlaceName.setValue("");
                    App.hiddenStoragePlaceID.setValue("");
                    App.txtStoragePlaceName.getTrigger(0).hide();
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var AddStoragePlace = function (field, trigger, index) {//库位添加
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂家名称",
            modal: true
        })

        var ioChange = function (value) {
            return Ext.String.format(value=="I" ? "入库" : "出库");
        };

        var billTypeChange = function (value) {
            if (value == "1101")
                return Ext.String.format("车间消耗");
            else if (value == "1102")
                return Ext.String.format("车间调拨");
            else if (value == "1103")
                return Ext.String.format("调拨反向");
            else if (value == "1004")
                return Ext.String.format("退货单");
            else if (value == "1005")
                return Ext.String.format("退库单");
            else if (value == "1006")
                return Ext.String.format("调整单");
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


        var SetFXFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交放行吗？', function (btn) { commandcolumn_direct_sendfxflag(btn) });
            }
        }
        var SetFXFlag2 = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要取消放行吗？', function (btn) { commandcolumn_direct_sendfxflag2(btn) });
            }
        }
        var commandcolumn_direct_sendfxflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnFxSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        var commandcolumn_direct_sendfxflag2 = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnFxSend_Click2({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpStorage" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnStorage" runat="server" Region="North" Height="90">
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
                            <ext:ToolbarSeparator ID="tsEnd" />
                              <ext:Button runat="server" Icon="LockEdit" Text="超期放行" ID="Button3">
                                    <Listeners>
                                        <Click Handler="SetFXFlag();" />
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="tsMiddle3" />
                                <ext:Button runat="server" Icon="LockEdit" Text="取消放行" ID="Button4">
                                    <Listeners>
                                        <Click Handler="SetFXFlag2();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="ToolbarSeparator2" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Left" LabelPad="-30"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Left" LabelPad="-30"/>
                                    <ext:TextField ID="txtProductNo" runat="server" FieldLabel="批次号" Visible="false" LabelAlign="Left" />
                                    <%--<ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />--%>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Left" LabelPad="-30"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtBoxCode" runat="server" FieldLabel="框条码" LabelPad="-30" LabelAlign="Left" />
                                    <ext:TextField ID="txtFactory" runat="server" FieldLabel="供应商" LabelAlign="Left" Visible="false"/>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Left" LabelPad="-30"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryMaterial" />
                                        </Listeners>
                                    </ext:TriggerField>
                                         <ext:ComboBox ID="cbxFxfalgFlag" runat="server" FieldLabel="超期放行" LabelAlign="Left" LabelPad="-30" >
                                            <SelectedItems>
                                                <ext:ListItem Value="all">
                                                </ext:ListItem>
                                            </SelectedItems>
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
                                 <%--   ,StorageID,StoragePlaceID--%>
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="ValidDate" />
                                            <ext:ModelField Name="Num" Type="Int" />
                                            <ext:ModelField Name="PieceWeight" Type="Float" />
                                            <ext:ModelField Name="RealWeight" Type="Float" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="FacCode" />
                                            <ext:ModelField Name="SendDate" />
                                            <ext:ModelField Name="Fxflag" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <%--<Sorters>
                                    <ext:DataSorter Property="RecordDate" Direction="DESC"></ext:DataSorter>
                                </Sorters>--%>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <%--<Listeners>
                                <HeaderClick Fn="orderClick"></HeaderClick>
                            </Listeners>--%>
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1"  Format="yyyy-MM-dd" />
                                <ext:Column ID="ValidDate" runat="server" Text="保质期" DataIndex="ValidDate" Flex="1">
                                    <Renderer Fn="validDateChange" />
                                </ext:Column>
                                <ext:Column ID="PieceWeight1" runat="server" Text="单重" DataIndex="PieceWeight" Flex="1" />
                                <ext:Column ID="RealWeight" runat="server" Text="当前重量" DataIndex="RealWeight" Flex="1">
                                    <Renderer Fn="realWeightChange" />
                                </ext:Column>
                                 <ext:Column ID="FacCode" runat="server" Text="供应商" DataIndex="FacCode" Flex="1" Visible="false" />
                                  <ext:Column ID="SendDate" runat="server" Text="发货日期" DataIndex="SendDate" Flex="1" Visible="false" />
                                    <ext:Column ID="Fxflag" runat="server" Text="超期放行" DataIndex="Fxflag" Flex="1" />
                            </Columns>
                        </ColumnModel>
                       
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="排序方式:" Reorderable="false" />
                                    <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" Reorderable="false" />
                                    <ext:SplitButton ID="SplitButton1" runat="server" Text="供应商" Icon="SortAscending"
                                        OnClientClick="changeSortDirection(this, true);" SortData="={{property:'FacCode',direction:'ASC'}}" Reorderable="true">
                                        <Menu>
                                            <ext:Menu ID="Menu1" runat="server">
                                                <Items>
                                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="移除" OnClientClick="remove(this);" />
                                                </Items>
                                            </ext:Menu>
                                        </Menu>
                                    </ext:SplitButton>
                                    <ext:SplitButton ID="SplitButton2" runat="server" Text="发货日期" Icon="SortDescending"
                                        OnClientClick="changeSortDirection(this, true);" SortData="={{property:'SendDate',direction:'DESC'}}" Reorderable="true">
                                        <Menu>
                                            <ext:Menu ID="Menu2" runat="server">
                                                <Items>
                                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="移除" OnClientClick="remove(this);" />
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
                            <DirectEvents>
                                        <SelectionChange OnEvent="rowSelectMuti_SelectionChange">
                                            <ExtraParams>
                                               
                                                <ext:Parameter Name="Barcode" Value="selected[0].get('Barcode')" Mode="Raw" />
                                       
                                            </ExtraParams>
                                        </SelectionChange>
                                    </DirectEvents>
                                <%--<DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="Barcode" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>--%>
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
                                    <ext:TextField ID="txtTotalNum" runat="server" LabelAlign="Right" ReadOnly="true" />
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
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="RealName" />
                                            <ext:ModelField Name="shiftname" />
                                            <ext:ModelField Name="BoxCode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="StoreInOut" />
                                            <ext:ModelField Name="Num" Type="Int" />
                                            <ext:ModelField Name="PieceWeight" Type="Float" />
                                            <ext:ModelField Name="Weight" Type="Float" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="InaccountDuration" />
                                            <ext:ModelField Name="BillType" />
                                            <ext:ModelField Name="SourceBillNo" />
                                            <ext:ModelField Name="SourceOrderID" />
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
                                <ext:Column ID="Barcode1" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="框条码" DataIndex="BoxCode" Flex="1" />
                                <%--<ext:Column ID="StorageName1" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName1" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />--%>
                                <ext:Column ID="StoreInOut" runat="server" Text="出入库方式" DataIndex="StoreInOut" Flex="1">
                                    <Renderer Fn="ioChange" />
                                </ext:Column>
                                <ext:Column ID="Column1" runat="server" Text="班次班组" DataIndex="shiftname" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="操作人" DataIndex="RealName" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <ext:Column ID="PieceWeight" runat="server" Text="单重" DataIndex="PieceWeight" Flex="1" />
                                <ext:Column ID="Weight" runat="server" Text="重量" DataIndex="Weight" Flex="1" />
                                <ext:DateColumn ID="RecordDate1" runat="server" Text="业务发生日期" DataIndex="RecordDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <%--<ext:Column ID="BillType" runat="server" Text="单据类型" DataIndex="BillType" Flex="1">
                                    <Renderer Fn="billTypeChange" />
                                </ext:Column>--%>
                                <ext:Column ID="SourceBillNo" runat="server" Text="来源单号" DataIndex="SourceBillNo" Flex="1" />
                                <ext:Column ID="SourceOrderID" runat="server" Text="来源序号" DataIndex="SourceOrderID" Flex="1" />

                               


                            </Columns>
                        </ColumnModel>
                        <Listeners>
                            <CellDblClick Fn="gridPanelCellDblClick" />
                        </Listeners>
                        <%--<SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMutiDetail" runat="server" Mode="Multi" />
                        </SelectionModel>--%>
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
             <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
