﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstEquip.aspx.cs" Inherits="Manager_ProducingPlan_FirstEquip" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首选机台设置</title>
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
                App.direct.pnlList_Add(record.data.serialid, record.data.mater_name, record.data.equip_name, record.data.sub_fac, record.data.mater_Code, record.data.equip_Code,
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
            var serialid = record.data.serialid;
            App.direct.pnlList_Delete(serialid, {
                success: function () { },
                failure: function () { }
            });
        }


        var addEmployee = function () {
            var grid = App.pnlList,
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();

            grid.getView().headerCt.setSortState(); // To update columns sort UI

            store.insert(0, {
                serialid: '',
                mater_name: Ext.getCmp('cbxmater').getRawValue(),
                equip_name: Ext.getCmp('cbxequip').getRawValue(),
                sub_fac: '',
                mater_Code: '',
                equip_Code: '',
            });

            //App.direct.pnlList_Add(record.data.serialid, record.data.mater_name, record.data.equip_name, record.data.sub_fac, record.data.mater_Code, record.data.equip_Code,
            //          {
            //              success: function () { },
            //              failure: function () { }
            //          });

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
                                <%--<ext:Button runat="server" Text="新建" Icon="Add" ID="btnAdd">
                                    <Listeners>
                                        <Click Fn="addEmployee" />
                                    </Listeners>
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
                                </ext:Button>--%>
                                <ext:Button runat="server" Icon="FolderConnect" Text="新建" ID="btnCreatePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击新建记录" /></ToolTips>
                                    <DirectEvents><Click OnEvent="pnlList_Add"/></DirectEvents>
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxmater" runat="server" Disabled="false" Width="400" AnchorHorizontal="100%" FieldLabel="物料" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                       <ext:ComboBox ID="cbxequip" runat="server" Disabled="false" Width="400" AnchorHorizontal="100%" FieldLabel="机台" />
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
                            <ext:DataSorter Property="serialid" />
                        </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="serialid">
                                    <Fields>
                                        <ext:ModelField Name="serialid" Type="Int" />
                                        <ext:ModelField Name="mater_name"/>
                                        <ext:ModelField Name="equip_name"/>
                                        <ext:ModelField Name="sub_fac"/>
                                        <ext:ModelField Name="mater_Code"/>
                                        <ext:ModelField Name="equip_Code"/>
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
                            <ext:Column ID="Column18" runat="server" Text="主键" DataIndex="serialid" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:Column runat="server" ID="mater_name" DataIndex="mater_name" Text="物料名称" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column1" DataIndex="equip_name" Text="首选机台" MenuDisabled="true" />
                           
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
