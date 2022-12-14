<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_Report_RubberSplitReport, App_Web_4coxxdow" %>
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

        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
            App.txtFactoryID.setValue(record.data.FacName);
            App.hiddenFactoryID.setValue(record.data.ObjID);
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtOperPerson.getTrigger(0).show();
            App.txtOperPerson.setValue(record.data.UserName);
            App.hiddenOperPerson.setValue(record.data.WorkBarcode);
            App.pageToolBar.doRefresh();
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
        var AddStoragePlace = function (field, trigger, index) {//库位添加
            var url = "../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
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

        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenOperPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
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
            html: "<iframe src='../../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂家名称",
            modal: true
        })

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
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Left" />
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Left" OnDirectChange="Search_Change">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                            <ext:ListItem Text="M2车间" Value="2"></ext:ListItem>
                                            <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                            <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                            <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Left" />
                                    <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryMaterial" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Left" OnDirectChange="Search_Change">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                            <ext:ListItem Text="早" Value="1"></ext:ListItem>
                                            <ext:ListItem Text="中" Value="2"></ext:ListItem>
                                            <ext:ListItem Text="夜" Value="3"></ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Left" />
                                    <ext:TriggerField ID="txtOperPerson" runat="server" FieldLabel="拆分人" LabelAlign="Left" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryUser" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtshelf" runat="server" FieldLabel="车架号" LabelAlign="Left" />
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
                            <ext:Store ID="store" runat="server" PageSize="10" AutoLoad="false">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" >
                                        <Fields>
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="OperDate" />
                                            <ext:ModelField Name="ShiftID" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ShiftClassID" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="Num" Type="Int" />
                                            <ext:ModelField Name="TotalWeight" Type="Float" />
                                            <ext:ModelField Name="OperPerson" />
                                            <ext:ModelField Name="UserName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Hidden="true" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="OperDate" runat="server" Text="操作日期" DataIndex="OperDate" Flex="1" />
                                <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Flex="1" />
                                <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Flex="1" />
                                <ext:Column ID="Num1" runat="server" Text="架子数" DataIndex="Num" Flex="1" />
                                <ext:Column ID="TotalWeight" runat="server" Text="扒胶重量" DataIndex="TotalWeight" Flex="1" />
                                <ext:Column ID="UserName" runat="server" Text="扒胶人" DataIndex="UserName" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
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
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="扒胶明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="Barcode, BarcodeSplit">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="BarcodeSplit" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="Weight" Type="Float" />
                                            <ext:ModelField Name="OperTime" Type="Date" />
                                            <ext:ModelField Name="PrintTime" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="LLShelfID" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="Barcode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.Barcode : -1" />
                                    <ext:StoreParameter Name="StorageID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StorageID : -1" />
                                    <ext:StoreParameter Name="StoragePlaceID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StoragePlaceID : -1" />
                                    <ext:StoreParameter Name="OperPerson" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.OperPerson : -1" />
                                    <ext:StoreParameter Name="OperDate" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.OperDate : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:Column ID="Barcode1" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="BarcodeSplit" runat="server" Text="拆分条码号" DataIndex="BarcodeSplit" Flex="1" />
                                <ext:Column ID="MaterialName1" runat="server" Text="物料" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="Weight1" runat="server" Text="重量" DataIndex="Weight" Flex="1" />
                                <ext:DateColumn ID="OperTime" runat="server" Text="操作日期" DataIndex="OperTime" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="PrintTime" runat="server" Text="打印日期" DataIndex="PrintTime" Flex="1" />
                                <ext:Column ID="UserName1" runat="server" Text="打印人" DataIndex="UserName" Flex="1" />
                                <ext:Column ID="Column1" runat="server" Text="架子号" DataIndex="LLShelfID" Flex="1" />
                            </Columns>
                        </ColumnModel>
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
            <ext:Hidden ID="hiddenOperPerson" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>