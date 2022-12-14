﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkerJob.aspx.cs" Inherits="Manager_ProducingPlan_WorkerJob" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工岗位设置</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
     <style type="text/css">
        .x-grid-body .x-grid-cell-Cost
        {
            background-color: #f1f2f4;
        }
        
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner
        {
            background-color: #e1e2e4;
        }
        
        .task .x-grid-cell-inner
        {
            padding-left: 15px;
        }
        
        .x-grid-row-summary .x-grid-cell-inner
        {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <script type="text/javascript">
        var prepareGroupToolbar = function (grid, toolbar, groupId, records) {
            // you can prepare ready toolbar
        };


        var getAdditionalData = function (data, idx, record, orig) {
            var o = Ext.grid.feature.RowBody.prototype.getAdditionalData.apply(this, arguments),
                d = data;

            Ext.apply(o, {
                rowBodyColspan: record.fields.getCount(),
                rowBody: Ext.String.format('<div style=\'padding:0 5px 5px 5px;\'>The {0} [{1}] requires light conditions of <i>{2}</i>.<br /><b>Price: {3}</b></div>', d.Common, d.Botanical, d.Light, Ext.util.Format.usMoney(d.Price)),
                rowBodyCls: ""
            });

            return o;
        };

    </script>
    
    <script type="text/javascript">

        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Save") {
                App.direct.pnlList_Add(record.data.Serial_id, record.data.Plan_date, record.data.EquipName, record.data.ShiftName, 
                    record.data.ClassName, record.data.Work_typeName,record.data.User_name, record.data.WorkShop_Name,
                    {
                    success: function () { },
                    failure: function () { }
                });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var Serial_id = record.data.Serial_id;
            App.direct.pnlList_Delete(Serial_id, {
                success: function () { },
                failure: function () { }
            });
        }


        var addEmployee = function () {
            var grid = App.pnlList,
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();

            //store.getSorters().removeAll(); // We have to remove sorting to avoid auto-sorting on insert
            grid.getView().headerCt.setSortState(); // To update columns sort UI

            store.insert(0, {
                Serial_id: '',
                Plan_date: Ext.Date.format(new Date(), 'Y-m-d'),
                EquipName: Ext.getCmp('cbxequip').getRawValue(),
                ShiftName: Ext.getCmp('cbxshift').getRawValue(),
                ClassName: Ext.getCmp('cbxclass').getRawValue(),
                Work_typeName: Ext.getCmp('cbxwork').getRawValue(),
                User_name: Ext.getCmp('cbxUser').getRawValue(),
                WorkShop_Name: Ext.getCmp('cbxworkshop').getRawValue(),
            });

            grid.editingPlugin.startEdit(0, 0);
        };
        var removeEmployee = function () {
            var grid = App.pnlList,
                sm = grid.getSelectionModel(),
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();
            store.remove(sm.getSelection());

            if (store.getCount() > 0) {
                sm.select(0);
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="FolderConnect" Text="新建" ID="btnCreatePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击新建记录" /></ToolTips>
                                    <DirectEvents><Click OnEvent="pnlList_Add"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button
                                    ID="btnRemove"
                                    runat="server"
                                    Text="取消"
                                    Icon="ControlRemove"
                                    Disabled="true">
                                    <Listeners>
                                        <Click Fn="removeEmployee" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="datetime" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="生产日期" />
                                        <ext:ComboBox ID="cbxequip" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="机台" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                         <ext:ComboBox ID="cbxshift" runat="server" FieldLabel="班次" LabelAlign="left" Width="300" 
                                                    Editable="false">
                                                </ext:ComboBox>
                                        <ext:ComboBox ID="cbxwork" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="岗位" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                         <ext:ComboBox ID="cbxclass" runat="server" FieldLabel="班组" LabelAlign="left" Width="300" 
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
                                         <ext:ComboBox ID="cbxUser" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="操作人" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                       <ext:ComboBox ID="cbxworkshop" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="车间" />
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>

                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Sorters>
                            <ext:DataSorter Property="Serial_id" />
                        </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Serial_id">
                                    <Fields>
                                        <ext:ModelField Name="Serial_id" Type="Int" />
                                        <ext:ModelField Name="Plan_date" />
                                        <ext:ModelField Name="EquipName"/>
                                        <ext:ModelField Name="ShiftName"/>
                                        <ext:ModelField Name="ClassName"/>
                                        <ext:ModelField Name="Work_typeName"/>
                                        <ext:ModelField Name="User_name"/>
                                        <ext:ModelField Name="WorkShop_Name"/>
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                    </Plugins>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Column18" runat="server" Text="主键" DataIndex="Serial_id" Hidden="true" Width="65">
                            </ext:Column>
                            
                            <ext:Column runat="server" ID="Plan_date" DataIndex="Plan_date" Text="生产日期" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column1" DataIndex="EquipName" Text="机台" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column8" DataIndex="ShiftName" Text="班次" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column9" DataIndex="ClassName" Text="班组" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column10" DataIndex="Work_typeName" Text="岗位" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column11" DataIndex="User_name" Text="操作工" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column12" DataIndex="WorkShop_Name" Text="车间" MenuDisabled="true" />

                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存" Hidden="true"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <SelectionChange Handler="App.btnRemove.setDisabled(!selected.length);" />
                    </Listeners>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
               
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_EType" runat="server">
                </ext:Hidden>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
