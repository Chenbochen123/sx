<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopAdjustReport.aspx.cs" Inherits="Manager_ShopStorage_Report_ShopAdjustReport" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        }

        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
            App.txtFactoryID.setValue(record.data.FacName);
            App.hiddenFactoryID.setValue(record.data.ObjID);
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
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=1&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        var ioChange = function (value) {
            return Ext.String.format(value == "I" ? "调入" : "调出");
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
            //入库单详情record.data.BillType == "1101"
            var url = "ShopStorage/ShopAdjust.aspx?BeginTime=" + App.txtBeginTime.rawValue + "&EndTime=" + App.txtEndTime.rawValue + "&Barcode=" + record.data.Barcode;
            var tabid = "Manager_ShopStorage_ShopAdjust";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            parent.addTab(tabid, "车间调拨", url, true);
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
                                    <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                            <ext:ListItem Text="M2车间" Value="2"></ext:ListItem>
                                            <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                            <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                            <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" Editable="false" AllowBlank="false" FieldLabel="开始时间" LabelAlign="Right" />
                                    <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
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
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtEndTime" runat="server" Editable="false" AllowBlank="false" FieldLabel="结束时间" LabelAlign="Right" />
                                    <ext:ComboBox ID="cbxOutIn" runat="server" FieldLabel="调入/出" LabelAlign="Right">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                            <ext:ListItem Text="调入" Value="I"></ext:ListItem>
                                            <ext:ListItem Text="调出" Value="O"></ext:ListItem>
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
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="TotalNum" Type="Int" />
                                            <ext:ModelField Name="TotalWeight" Type="Float" />
                                            <ext:ModelField Name="AdjustType" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="StorageID" runat="server" Text="库房编号" Hidden="true" DataIndex="StorageID" />
                                <ext:Column ID="StoragePlaceID" runat="server" Text="库位编号" Hidden="true" DataIndex="StoragePlaceID" />
                                <ext:Column ID="MaterCode" runat="server" Text="物料编号" Hidden="true" DataIndex="MaterCode" />
                                <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="TotalNum" runat="server" Text="合计数量" DataIndex="TotalNum" Flex="1" />
                                <ext:Column ID="TotalWeight" runat="server" Text="合计重量" DataIndex="TotalWeight" Flex="1" />
                                <ext:Column ID="AdjustType" runat="server" Text="调入/出" DataIndex="AdjustType" Flex="1">
                                    <Renderer Fn="ioChange" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
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
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="调拨明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="StoragerID, StoragePlaceID, MaterCode, AdjustType">
                                        <Fields>
                                            <ext:ModelField Name="StoragerID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="ToStorageName" />
                                            <ext:ModelField Name="ToStoragePlaceName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="Num" Type="Int" />
                                            <ext:ModelField Name="RealWeight" Type="Float" />
                                            <ext:ModelField Name="AdjustNum" Type="Int" />
                                            <ext:ModelField Name="AdjustWeight" Type="Float" />
                                            <ext:ModelField Name="AdjustType" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="StorageID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StorageID : -1" />
                                    <ext:StoreParameter Name="StoragePlaceID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StoragePlaceID : -1" />
                                    <ext:StoreParameter Name="MaterCode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.MaterCode : -1" />
                                    <ext:StoreParameter Name="AdjustType" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.AdjustType : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:Column ID="StorageName1" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="MaterialName1" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="Barcode1" runat="server" Text="条码号" DataIndex="Barcode" Width="150" />
                                <ext:Column ID="Num2" runat="server" Text="库存数量" DataIndex="Num" Flex="1" />
                                <ext:Column ID="RealWeight1" runat="server" Text="库存重量" DataIndex="RealWeight" Flex="1" />
                                <ext:Column ID="Num1" runat="server" Text="调拨数量" DataIndex="AdjustNum" Flex="1" />
                                <ext:Column ID="Weight1" runat="server" Text="调拨重量" DataIndex="AdjustWeight" Flex="1" />
                                <ext:Column ID="AdjustType1" runat="server" Text="调入/出" DataIndex="AdjustType" Flex="1">
                                    <Renderer Fn="ioChange" />
                                </ext:Column>
                                <ext:Column ID="ToStorageName1" runat="server" Text="源库房" DataIndex="ToStorageName" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <Listeners>
                            <CellDblClick Fn="gridPanelCellDblClick" />
                        </Listeners>
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

            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
