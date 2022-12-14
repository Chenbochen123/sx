<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_RubberConsume, App_Web_dobmrtlx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ShopoutID = record.data.Id;
            App.direct.commandcolumn_direct_edit(ShopoutID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ShopoutID = record.data.Id;
            App.direct.commandcolumn_direct_delete(ShopoutID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        var shiftIDChange = function (value) {
            if (value == "1")
                return Ext.String.format("早");
            if (value == "2")
                return Ext.String.format("中");
            if (value == "3")
                return Ext.String.format("夜");
        };

        var shiftClassIDChange = function (value) {
            if (value == "1")
                return Ext.String.format("甲");
            if (value == "2")
                return Ext.String.format("乙");
            if (value == "3")
                return Ext.String.format("丙");
        };

        var handFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "是" : "否");
        };

        var QueryUser = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
        }

        var QueryEquipmentInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }

        var AddEquipmentInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
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

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var AddStorage = function (field, trigger, index) {//库房添加
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
        }

        var AddStoragePlace = function (field, trigger, index) {//库位添加
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }

            App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
        }

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=1&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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

        Ext.create("Ext.window.Window", {//人员信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })

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

        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName1.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            App.txtStorageName1.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            if (!App.winAdd.hidden) {
                App.txtUserName1.setValue(record.data.UserName);
                App.hiddenUserID.setValue(record.data.WorkBarcode);
            }
            else if (!App.winModify.hidden) {
                App.txtUserName2.setValue(record.data.UserName);
                App.hiddenUserID.setValue(record.data.WorkBarcode);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
           
                App.txtEquip.getTrigger(0).show();
                App.txtEquip.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
           
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            
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
            integerText: "此填入项格式为正整数",
            decimal: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d.\d]+$/.test(val)) {
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
            decimalText: "此填入项格式为浮点数"
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMmShopout" runat="server" />
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:Viewport ID="vpMmShopout" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMmShopout">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
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
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" Editable="false" />
                                            <ext:ComboBox ID="cboMaterType" runat="server" FieldLabel="生产物料" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="母炼胶" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="终炼胶" Value="5">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="返回胶" Value="6">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="其他" Value="05">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" Editable="false" />
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
                                    <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipmentInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="all"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                    <ext:ListItem Text="早" Value="1"></ext:ListItem>
                                                    <ext:ListItem Text="中" Value="2"></ext:ListItem>
                                                    <ext:ListItem Text="夜" Value="3"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <Items>
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
                                             <ext:ComboBox ID="ComboBox1" runat="server" FieldLabel="消耗物料" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="母炼胶" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="终炼胶" Value="5">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="返回胶" Value="6">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="全部" Value="">
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
                </Items>
            </ext:Panel>

            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15"> 
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="Id" />
                                    <ext:ModelField Name="PlanDate" Type="Date" />
                                    <ext:ModelField Name="CostCode" />
                                    <ext:ModelField Name="CostMaterName" />
                                    <ext:ModelField Name="MaterCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="EquipCode" />
                                    <ext:ModelField Name="EquipName" />
                                    <ext:ModelField Name="ShiftID" />
                                    <ext:ModelField Name="ShiftClassID" />
                                    <ext:ModelField Name="ConsumeQty" />
                                    <ext:ModelField Name="BalanceQty" />
                                    <ext:ModelField Name="ConsQty" />
                                    <ext:ModelField Name="SurPlus" />
                                    <ext:ModelField Name="HandFlag" />
                                    <ext:ModelField Name="ConsRate" />
                                    <ext:ModelField Name="Mater_Kind" />
                                    <ext:ModelField Name="MinorTypeName" />
                                    <ext:ModelField Name="RecordDate" Type="Date" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="ShopOutid" runat="server" Text="ShopOutid" Hidden="true" DataIndex="Id" Flex="1" />
                        <ext:DateColumn ID="PlanDate" runat="server" Text="生产日期" DataIndex="PlanDate" Format="yyyy-MM-dd" Flex="1" />
                        <ext:Column ID="MinorTypeName" runat="server" Text="物料分类" DataIndex="MinorTypeName" Flex="1" />
                        <ext:Column ID="EquipName" runat="server" Text="生产机台" DataIndex="EquipName" Flex="1" />
                        <ext:Column ID="CostMaterName" runat="server" Text="生产物料" DataIndex="CostMaterName" Flex="1" />
                        <ext:Column ID="MaterialName" runat="server" Text="消耗物料" DataIndex="MaterialName" Flex="1" />
                        <ext:Column ID="ConsumeQty" runat="server" Text="设定重量" DataIndex="ConsumeQty" Flex="1" />
                        <ext:Column ID="ConsQty" runat="server" Text="消耗重量" DataIndex="ConsQty" Flex="1" />
                         <ext:Column ID="ShiftID" runat="server" Text="班次" DataIndex="ShiftID" Flex="1" >
                            <Renderer Fn="shiftIDChange" />
                        </ext:Column>
                        <ext:Column ID="ShiftClassID" runat="server" Text="班组" DataIndex="ShiftClassID" Flex="1" >
                            <Renderer Fn="shiftClassIDChange" />
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
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

            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改消耗信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="ShopOutid2" runat="server" FieldLabel="编号" Hidden="true" LabelAlign="Left" ReadOnly="true" />
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="物料条码" LabelAlign="Left" ReadOnly="true" />
                            <ext:TextField ID="txtMaterName2" runat="server" FieldLabel="消耗物料" LabelAlign="Left" ReadOnly="true" />
                            <ext:ComboBox ID="cbxShiftID2" runat="server" FieldLabel="班次" LabelAlign="Left" ReadOnly="true">
                                <SelectedItems>
                                    <ext:ListItem Value="1"></ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="早" Value="1"></ext:ListItem>
                                    <ext:ListItem Text="中" Value="2"></ext:ListItem>
                                    <ext:ListItem Text="夜" Value="3"></ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cbxShiftClassID2" runat="server" FieldLabel="班组" LabelAlign="Left" ReadOnly="true">
                                <SelectedItems>
                                    <ext:ListItem Value="1"></ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="甲" Value="1"></ext:ListItem>
                                    <ext:ListItem Text="乙" Value="2"></ext:ListItem>
                                    <ext:ListItem Text="丙" Value="3"></ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="txtEquipName2" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Left" Editable="false" ReadOnly="true">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="AddEquipmentInfo" />
                                </Listeners>
                            </ext:TriggerField>                            
                            
                            <ext:TextField ID="txtRealWeight2" runat="server" Vtype="decimal" FieldLabel="投入重量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnModify_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
           
            <ext:Hidden ID="hiddenUserID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenNoStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenHigherLevel" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
