﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubInJar.aspx.cs" Inherits="Manager_ShopStorage_RubInJar" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>料仓投料</title>
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
            if (record.get("AuditFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
        }

        var commandcolumn_direct_sendchkflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SetChkFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交审核吗？', function (btn) { commandcolumn_direct_sendchkflag(btn) });
            }
        }
        var AddStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenNoStorageID.setValue("");
                    App.hiddenStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        var QueryEquipmentInfo = function (field, trigger, index) { //机台添加
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
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var JarID = record.data.JarID;
            App.direct.commandcolumn_direct_edit(JarID, {
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
            var JarID = record.data.JarID;
            App.direct.commandcolumn_direct_delete(JarID, {
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

        var clearFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "已清仓" : "未清仓");
        };

        var auditFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "已审核" : "未审核");
        };

        var QueryUser = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
        }

        var QueryEquipmentInfo1 = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=0&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true

        })

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            if (!App.winAdd.hidden) {
                App.txtUserName1.setValue(record.data.UserName);
                App.hiddenUserID.setValue(record.data.HRCode);
            }
            else if (!App.winModify.hidden) {
                App.txtUserName2.setValue(record.data.UserName);
                App.hiddenUserID.setValue(record.data.HRCode);
            }
        }
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName1.getTrigger(0).show();
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenNoStorageID.setValue(record.data.StorageID);

            }
            else {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.pageToolBar.doRefresh();
            }
        }

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台返回信息处理
            if (!App.winAdd.hidden) {
                App.txtEquipName1.getTrigger(0).show();
                App.txtEquipName1.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            }
            else if (!App.winModify.hidden) {
                App.txtEquipName2.getTrigger(0).show();
                App.txtEquipName2.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            }
            else {
                App.txtEquip.getTrigger(0).show();
                App.txtEquip.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
                App.pageToolBar.doRefresh();
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtMaterialName1.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else if (!App.winModify.hidden) {
                App.txtMaterialName2.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else {
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
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
      <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmMminjar" runat="server" />
    <ext:Viewport ID="vpMminjar" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMminjarTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMminjar">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd"  >
                                <DirectEvents>
                                    <Click OnEvent="btnAdd_Click" />
                                </DirectEvents>
                            </ext:Button>
                          
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="LockEdit" Text="审核" ID="Button1"  Hidden="true">
                                <Listeners>
                                    <Click Handler="SetChkFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                              <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
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
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="入仓开始时间" LabelAlign="Right" />
                                            
                                                <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="原材料名" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                   <%--<ext:TextField ID="txtBarcode" runat="server" FieldLabel="物料名称" LabelAlign="Right" AllowBlank="false" IndicatorCls="red-text" />--%>
                                  
                                           
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="入仓结束时间" LabelAlign="Right" />
                                            <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipmentInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right">
                                                <SelectedItems>
                                                    <ext:ListItem Value="all">
                                                    </ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="早" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="中" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="夜" Value="3">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                             <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right"
                                                Editable="false" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="AddStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                           </Items>
                                    </ext:Container>
                                </Items>
                                <%--<Listeners>
                                    <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                </Listeners>--%>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server" IDProperty="JarID">
                                <Fields>
                                    <ext:ModelField Name="JarID" />
                                    <ext:ModelField Name="JarNum" />
                                    <ext:ModelField Name="Materbarcode" />
                                    <ext:ModelField Name="StockDate" Type="Date" />
                                    <ext:ModelField Name="ShiftDate" Type="Date" />
                                    <ext:ModelField Name="EquipCode" />
                                    <ext:ModelField Name="EquipName" />
                                    <ext:ModelField Name="InTime" Type="Date" />
                                    <ext:ModelField Name="ShiftID" />
                                    <ext:ModelField Name="MaterCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="RealNum" />
                                    <ext:ModelField Name="RealWeight" />
                                    <ext:ModelField Name="User_Name" />
                           
                                    <ext:ModelField Name="InTime" Type="Date" />
                                    <ext:ModelField Name="ClearFlag" />
                                    <ext:ModelField Name="InaccountFlag" />
                                    <ext:ModelField Name="AuditFlag" />
                                      <ext:ModelField Name="Usedweigh" />
                                  
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="JarID" runat="server" Text="ID" Hidden="true" DataIndex="JarID" Flex="1" />
                        <ext:Column ID="Materbarcode" runat="server" Text="批次号" DataIndex="Materbarcode" />
                        <ext:Column ID="JarNum" runat="server" Text="料仓号" DataIndex="JarNum" Width="45" />
                        <ext:DateColumn ID="StockDate" runat="server" Text="入仓日期" DataIndex="StockDate" Format="yyyy-MM-dd"
                            Flex="1" />
                
                        <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                        <ext:Column ID="ShiftID" runat="server" Text="班次" DataIndex="ShiftID" Flex="1">
                            <Renderer Fn="shiftIDChange" />
                        </ext:Column>
                        <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName"
                            Flex="1" />
                        <ext:Column ID="RealNum" runat="server" Text="投入数量" DataIndex="RealNum" Flex="1" />
                        <ext:Column ID="RealWeight" runat="server" Text="投入重量" DataIndex="RealWeight" Flex="1" />
                          <ext:Column ID="Column1" runat="server" Text="已用重量" DataIndex="Usedweigh" Flex="1"  Hidden="true"/>
                        <ext:Column ID="handLename" runat="server" Text="经办人" DataIndex="User_Name" Flex="1" />
                        <ext:DateColumn ID="InTime" runat="server" Text="投料时间" DataIndex="InTime" Format="yyyy-MM-dd HH:mm:ss"
                            Flex="1" />
                <%--        <ext:Column ID="ClearFlag" runat="server" Text="清仓标志" DataIndex="ClearFlag" Flex="1">
                            <Renderer Fn="clearFlagChange" />
                        </ext:Column>
                        <ext:Column ID="AuditFlag" runat="server" Text="审核标志" DataIndex="AuditFlag" Flex="1">
                            <Renderer Fn="auditFlagChange" />
                        </ext:Column>--%>
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
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
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
                                    <ext:ListItem Value="50" />
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
            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改投料信息"
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
                            <ext:TextField ID="txtJarID2" runat="server" FieldLabel="投料编号" Hidden="true" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="物料条码" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:DateField ID="txtStockDate2" runat="server" FieldLabel="入仓日期" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:TextField ID="txtMaterName2" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:ComboBox ID="cbxShiftID2" runat="server" FieldLabel="班次" LabelAlign="Left">
                                <SelectedItems>
                                    <ext:ListItem Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="早" Value="1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="中" Value="2">
                                    </ext:ListItem>
                                    <ext:ListItem Text="夜" Value="3">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="txtEquipName2" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="QueryEquipmentInfo1" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtRealNum2" runat="server" Vtype="integer" FieldLabel="投入数量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtRealWeight2" runat="server" Vtype="decimal" FieldLabel="投入重量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TriggerField ID="txtUserName2" runat="server" FieldLabel="经办人" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="QueryUser" />
                                </Listeners>
                            </ext:TriggerField>
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
                    <Show Handler="for(var i=0;i<#{vpMminjar}.items.length;i++){#{vpMminjar}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpMminjar}.items.length;i++){#{vpMminjar}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加投料信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:ComboBox ID="cbxShiftID1" runat="server" FieldLabel="班次" LabelAlign="Left">
                                <SelectedItems>
                                    <ext:ListItem Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="早" Value="1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="中" Value="2">
                                    </ext:ListItem>
                                    <ext:ListItem Text="夜" Value="3">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="txtStorageName1" runat="server" FieldLabel="库房名称" LabelAlign="Left" Hidden="true"
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
                            <ext:TriggerField ID="txtEquipName1" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="QueryEquipmentInfo1" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="txtMaterialName1" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="AddMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                             <ext:ComboBox ID="Ctype" runat="server" FieldLabel="罐子种类" LabelAlign="Left">
                                <SelectedItems>
                                    <ext:ListItem Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                <ext:ListItem Text="" Value="">
                                    </ext:ListItem>
                                    <ext:ListItem Text="炭黑" Value="炭黑">
                                    </ext:ListItem>
                                    <ext:ListItem Text="油" Value="油">
                                    </ext:ListItem>
                                    <ext:ListItem Text="小料" Value="小料">
                                    </ext:ListItem>
                                    
                                </Items>
                            </ext:ComboBox>
                            <ext:NumberField ID="TNum" runat="server" FieldLabel="罐号" AllowBlank="false"
                                IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
                            <ext:TextField ID="txtMaterBarcode1" runat="server" FieldLabel="物料条码" AllowBlank="false"
                                IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" />
                            <ext:TextField ID="txtRealNum1" runat="server" Vtype="integer" FieldLabel="投入数量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtRealWeight1" runat="server" Vtype="decimal" FieldLabel="投入重量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TriggerField ID="txtUserName1" runat="server" FieldLabel="经办人" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="QueryUser" />
                                </Listeners>
                            </ext:TriggerField>
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
                    <Show Handler="for(var i=0;i<#{vpMminjar}.items.length;i++){#{vpMminjar}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpMminjar}.items.length;i++){#{vpMminjar}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Hidden ID="hiddenUserID" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenNoStorageID" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenHigherLevel" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
