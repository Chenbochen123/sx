<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_PlanGetMaterial, App_Web_ampjtxsw" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_edit(ObjID, {
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
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_delete(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

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

        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        var pnlListFresh2 = function () {
            if (App.hiddenNowDate.getValue() > App.hiddenAtDate.getValue()) {
                Ext.Msg.alert('操作', '获取计划日期不能小于当前日期！');
                return false;
            }
            Ext.Msg.confirm("提示", '重新获取会先删除指定日期的计划领料单，确定要获取吗？', function (btn) {
                if (btn != "yes") {
                    return;
                }
                App.direct.btnGet()
            });
            return false;
        }
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            //            if (record.get("StorageID").length < 3) {
            //                toolbar.items.getAt(0).hide();
            //                toolbar.items.getAt(1).hide();
            //                toolbar.items.getAt(2).hide();
            //            }
        };

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName2.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageID.getTrigger(0).show();
                App.txtStorageID.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
            if (!App.winAdd.hidden) {
                App.txtEquipName2.getTrigger(0).show();
                App.txtEquipName2.setValue(record.data.EquipName);
                App.hiddenEquipCodeAdd.setValue(record.data.EquipCode);
            }
            else {
                App.txtEquipID.getTrigger(0).show();
                App.txtEquipID.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName2.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
        }

        var QueryStorageID = function (field, trigger, index) {
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

        var AddStorage = function () {//库房添加
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window;
            var html = "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?UsedFlag=1&&LastStorageFlag=1&&StorageType=1' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (queryWindow.getBody()) {
                queryWindow.getBody().update(html);
            } else {
                queryWindow.html = html;
            }
            queryWindow.show();
        }

        var QueryEquipInfo = function (field, trigger, index) {
            switch(index)
            {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                //    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
        }
        }

        var QueryMaterial = function (field, trigger, index) {//物料查询
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        Ext.create("Ext.window.Window", {//库房查询带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=1&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房",
            modal: true
        })
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            hidden: true,
            width: 370,
            height: 470,
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmGetMater" runat="server" />
        <ext:Viewport ID="vpGetMater" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnGetMaterTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbGetMater">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:Button runat="server" Icon="Add" Shadow="true" Hidden="true" Text="手工重新获取" ID="Button1">
                                   <Listeners>
                                        <Click  Fn="pnlListFresh2" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:Panel ID="pnlGetMaterQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                    FieldLabel="生产开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd" OnDirectChange="txtStratShiftDate_change" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cboType" Editable="false" runat="server" FieldLabel="设备车间" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="con1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txtEquipID" runat="server" Flex="1" Editable="false" FieldLabel="机台名称"
                                                    LabelAlign="Right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEquipInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxMaterialype" runat="server" FieldLabel="统计分类" LabelAlign="Right">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all">
                                                        </ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="生胶类" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="粉料类" Value="2"></ext:ListItem>
                                                        <ext:ListItem Text="炭黑类" Value="3"></ext:ListItem>
                                                        <ext:ListItem Text="油类" Value="4"></ext:ListItem>
                                                        <ext:ListItem Text="其它类" Value="5"></ext:ListItem>
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
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="PlanDate" Type="Date" />
                                        <ext:ModelField Name="StorageID" />
                                        <ext:ModelField Name="StorageName" />
                                        <ext:ModelField Name="MaterialCode" />
                                        <ext:ModelField Name="MaterialName" />
                                        <ext:ModelField Name="TotalWeight" Type="Float" />
                                        <ext:ModelField Name="RealGetWeight" Type="Float" />
                                        <ext:ModelField Name="SourcePlace" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="Remark" />
                                         <ext:ModelField Name="EquipName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="ObjID" runat="server" Text="ID" Hidden="true" DataIndex="ObjID" />
                            <ext:DateColumn ID="PlanDate" runat="server" Text="日期" DataIndex="PlanDate" Format="yyyy-MM-dd" Flex="1" />            
                            <ext:Column ID="StorageID" runat="server" Text="库房编号" DataIndex="StorageID"  Hidden="true" />
                            <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                              <ext:Column ID="Column3" runat="server" Text="机台名称" DataIndex="EquipName" Flex="1" />
                            <ext:Column ID="MaterialCode" runat="server" Text="物料编号" DataIndex="MaterialCode"  Hidden="true" />
                            <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                            <ext:Column ID="TotalWeight" runat="server" Text="计划领料重量" DataIndex="TotalWeight" Flex="1" />
                            <ext:Column ID="RealGetWeight" runat="server" Text="实际领料重量" DataIndex="RealGetWeight" Flex="1" />
                            <ext:Column ID="Column1" runat="server" Text="产地" DataIndex="SourcePlace" Flex="1" />
                            <ext:Column ID="Column2" runat="server" Text="其他备注" DataIndex="Remark" Flex="1" />
                            <ext:Column ID="UserName" runat="server" Text="变更人" DataIndex="UserName" Flex="1" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
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
                                        <ext:ListItem Text="15" />
                                        <ext:ListItem Text="50" />
                                        <ext:ListItem Text="100" />
                                        <ext:ListItem Text="200" />
                                    </Items>
                                    <SelectedItems>
                                        <ext:ListItem Value="15" />
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

                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改计划领料重量"
                    Width="320" Height="320" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="txtObjID1" runat="server" FieldLabel="编号" LabelAlign="Left" Hidden="true" />
                                <ext:TextField ID="txtStorageName1" runat="server" FieldLabel="库房名称" ReadOnly="true" LabelAlign="Left" />
                                <ext:TextField ID="txtEquipName1" runat="server" FieldLabel="机台名称" ReadOnly="true" LabelAlign="Left" />
                                <ext:TextField ID="txtMaterName1" runat="server" FieldLabel="物料名称" ReadOnly="true" LabelAlign="Left" />
                                <ext:TextField ID="txtPlanWeight1" runat="server" FieldLabel="计划领料重量" Vtype="decimal" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
                                <ext:TextField ID="txtSourcePlace1"   runat="server" FieldLabel="产地" LabelAlign="Left" />
                                <ext:TextField ID="txtRemark1"   runat="server" FieldLabel="其他备注" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
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
                        <Show Handler="for(var i=0;i<#{vpGetMater}.items.length;i++){#{vpGetMater}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpGetMater}.items.length;i++){#{vpGetMater}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>

                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加计划领料信息" Width="320" Height="380" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                
                                <ext:TriggerField ID="txtMaterName2" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                    Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryMaterial" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TriggerField ID="txtStorageName2" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                    Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="AddStorage" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TriggerField ID="txtEquipName2" runat="server" FieldLabel="机台" LabelAlign="Left"
                                    Editable="false">
                                    <Triggers>
                                         <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryEquipInfo" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TextField ID="txtPlanWeight2" runat="server" FieldLabel="计划领料数量" Vtype="decimal" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
                                <ext:TextField ID="txtSourcePlace2"   runat="server" FieldLabel="产地" LabelAlign="Left" />
                                <ext:TextField ID="txtRemark2"   runat="server" FieldLabel="其他备注" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpGetMater}.items.length;i++){#{vpGetMater}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpGetMater}.items.length;i++){#{vpGetMater}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                
                <ext:Hidden ID="hiddenObjID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenEquipCodeAdd" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenNowDate" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenAtDate" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>

    </form>
</body>
</html>
