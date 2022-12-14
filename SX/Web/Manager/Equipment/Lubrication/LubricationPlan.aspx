﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LubricationPlan.aspx.cs" Inherits="Manager_Equipment_Lubrication_LubricationPlan" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备润滑计划</title>
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

        var onGroupCommand = function (column, command, group) {
            if (command === 'SelectGroup') {
                column.grid.getSelectionModel().select(group.children, true);
                return;
            }

            Ext.Msg.alert(command, 'Group name: ' + group.name + '<br/>Count - ' + group.children.length);
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


        //下次维修日期报警
        var validDateChange = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (value != null && value != "") {
                if (parseInt((new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 0)
                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="最后维修日期已超期，请联系处理！">{0}</div>', value);
            }
        }

    </script>
    <script type="text/javascript">
        function deleteuser() {
            //            var grid = #{GridPanel1};
            //            sm = grid.getSelectionModel().getSelected();
            //            alert("删除物料：" + sm.data.Name.toString());
            //Ext.net.DirectMethods.Deletestudent(record.data.stuid.toString());  
        }  
    </script>
    <style>
        /* style rows on mouseover */
        .x-grid-row-over .x-grid-cell-inner
        {
            font-weight: bold;
        }
    </style>
    
    <script type="text/javascript">
        //树形结构点击刷新右侧方法
        //点击tree产生配方相信信息 绑定到Gridpanel
        var menuItemClick = function (view, rcd, item, idx, event, eOpts) {
            var s = rcd.get('qtip');
            App.direct.LoadGridData(s, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
        };


        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.Lube_planid, {
                    success: function () { },
                    failure: function () { }
                });
            }
            else if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Add") {
                App.direct.pnlList_Add(record.data.Lube_planid, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var Lube_planid = record.data.Lube_planid;
            App.direct.pnlList_Delete(Lube_planid, {
                success: function () { },
                failure: function () { }
            });
        }


        var PlanCancelClick = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                Ext.Msg.alert('提示', '您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要取消该计划吗？', function (btn) { commandcolumn_direct_plancancel(btn) });
            }
        }

        var commandcolumn_direct_plancancel = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelPlan_Click({
                success: function (result) {
                    if (result == "true") {
                        Ext.Msg.alert('提示', "取消计划单成功！");
                    }
                    else {
                        Ext.Msg.alert('提示', result);
                    }
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                },
                eventMask: {
                    showMask: true
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="180" Split="true" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeEquip" runat="server" Title="机台分组信息" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
                        <Store>
                            <ext:TreeStore ID="treeDeptStore" runat="server">
                                <Proxy>
                                    <ext:PageProxy>
                                        <RequestConfig Method="GET" Type="Load" />
                                    </ext:PageProxy>
                                </Proxy>
                                <Root>
                                    <ext:Node NodeID="root" Expanded="true" />
                                </Root>
                            </ext:TreeStore>
                        </Store>
                        <Listeners>
                            <ItemClick Fn="menuItemClick">
                            </ItemClick>
                        </Listeners>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行添加" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnAdd_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageSave" Text="计划完成" ID="btnCompletePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip4" runat="server" Html="点击进行完成" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnCompletePlan_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Cancel" Text="计划取消" ID="btnCancelPlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击进行取消" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="PlanCancelClick();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="RecordRed" Text="生成检修记录" ID="btnRecord">
                                    <ToolTips><ext:ToolTip ID="ToolTip6" runat="server" Html="点击生成记录" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnRecord_Click"/></DirectEvents>
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
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="计划开始时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout" FieldLabel="计划结束时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:ComboBox ID="cbxEquipQuery" runat="server" FieldLabel="机台"  LabelAlign="Right" LabelWidth="75" Width="200" DisplayField="Equip_name" ValueField="Equip_code" Editable="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storeEquipQuery">
                                                    <Model>
                                                        <ext:Model runat="server" ID="mEquip">
                                                            <Fields>
                                                                <ext:ModelField Name="Equip_code" />
                                                                <ext:ModelField Name="Equip_name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                            <DirectEvents>
                                                <Change OnEvent="cbxEquipQuery_SelectChanged" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxLube_nameQuery" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="润滑部位" LabelAlign="Right" LabelWidth="75" DisplayField="Lube_name" ValueField="Lube_name">
                                            <Store>
                                                <ext:Store runat="server" ID="storeLube_nameQuery">
                                                    <Model>
                                                        <ext:Model runat="server" ID="Model6">
                                                            <Fields>
                                                                <ext:ModelField Name="Lube_name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".25" Border="true">
                                    <Items>
                                        <ext:ComboBox ID="cbxlubeTypeQuery" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="油品名称"  LabelWidth="75" DisplayField="Oil_name" ValueField="Oil_code" Editable="false" AllowBlank="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storelubeTypeQuery">
                                                    <Model>
                                                        <ext:Model runat="server" ID="Model4">
                                                            <Fields>
                                                                <ext:ModelField Name="Oil_code" />
                                                                <ext:ModelField Name="Oil_name" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                            </ext:ComboBox>
                                        <ext:ComboBox ID="cbxPlanState" runat="server" FieldLabel="计划状态" LabelAlign="Right" Width="200" AnchorHorizontal="100%" LabelWidth="75"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Text="下达" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="完成" Value="2">
                                                </ext:ListItem>
                                                <ext:ListItem Text="取消" Value="3">
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
                                                
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>

                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Lube_planid">
                                    <Fields>
                                        <ext:ModelField Name="Lube_planid" />
                                        <ext:ModelField Name="Equip_classname" />
                                        <ext:ModelField Name="Equip_name" />
                                        <ext:ModelField Name="LubeName" />
                                        <ext:ModelField Name="Oil_name" />
                                        <ext:ModelField Name="Lube_plandate" />
                                        <ext:ModelField Name="Lube_planday" />
                                        <ext:ModelField Name="Lube_realdate" />
                                        <ext:ModelField Name="Lube_realday" />
                                        <ext:ModelField Name="Plan_state" />
                                        <ext:ModelField Name="Lube_result" />
                                        <ext:ModelField Name="USER_NAME" />
                                        <ext:ModelField Name="workshop" />
                                        <ext:ModelField Name="Lube_planid" />
                                        <ext:ModelField Name="lube_standId" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Column6" runat="server" Text="Lube_planid" DataIndex="Lube_planid" Width="65" Hidden="true" />
                            <ext:Column ID="Column3" runat="server" Text="设备分类" DataIndex="Equip_classname" Width="65"/>
                            <ext:Column ID="Column7" runat="server" Text="机台名称" DataIndex="Equip_name" Width="65"/>
                            <ext:Column ID="Column12" runat="server" Text="润滑部件" DataIndex="LubeName" Width="120"/>
                            <ext:Column ID="clOil_name" runat="server" Text="油品名称" DataIndex="Oil_name" Width="120"/>
                            <ext:Column ID="Column11" runat="server" Text="计划状态" DataIndex="Plan_state" Width="65" />
                            <ext:Column ID="Column4" runat="server" Text="计划润滑日期" DataIndex="Lube_plandate" Width="85"/>
                            <ext:Column ID="Column5" runat="server" Text="计划润滑工时" DataIndex="Lube_planday" Width="85" />
                            <ext:Column ID="Column8" runat="server" Text="实际润滑日期" DataIndex="Lube_realdate" Width="85"/>
                            <ext:Column ID="Column9" runat="server" Text="实际润滑工时" DataIndex="Lube_realday" Width="85" />
                            <ext:Column ID="Column20" runat="server" Text="润滑结论" DataIndex="Lube_result" Width="180"/>
                            <ext:Column ID="Column13" runat="server" Text="润滑人员" DataIndex="USER_NAME" Width="80"/>
                            <ext:Column ID="Column14" runat="server" Text="厂房" DataIndex="workshop" Width="80"/>
                            <ext:Column ID="Column1" runat="server" Text="计划编号" DataIndex="Lube_planid" Hidden="false" Width="100"/>
                            <ext:Column ID="Mp_StandId" runat="server" Text="标准号" DataIndex="lube_standId" Hidden="false" Width="65"/>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                    <%--<ext:GridCommand Icon="Add" CommandName="Add" Text="报修"/>--%>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
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
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="润滑计划单" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID" />
                                <ext:FieldSet runat="server" Title="润滑计划信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxEquip" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="机台" LabelWidth="75" DisplayField="Equip_name" ValueField="Equip_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquip">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model1">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Equip_code" />
                                                                        <ext:ModelField Name="Equip_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxEquip_SelectChanged" />
                                                    </DirectEvents>
                                                    </ext:ComboBox>
                                                <ext:ComboBox ID="cbxLube_name" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="润滑部件" LabelAlign="Right" LabelWidth="75" DisplayField="Lube_name" ValueField="Lube_name" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeLube_name">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model2">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Lube_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                                
                                        <ext:FieldContainer ID="FieldContainer12"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxlubeType" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="油品名称" LabelWidth="75" DisplayField="Oil_name" ValueField="Oil_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storelubeType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model5">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Oil_code" />
                                                                        <ext:ModelField Name="Oil_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    </ext:ComboBox>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtMp_plandate" runat="server" FieldLabel="计划润滑日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                                <ext:TextField ID="txtMp_planday" runat="server" Width="200" LabelAlign="Right" Enabled="true"  FieldLabel="计划润滑工时" InputType="Number"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer11" runat="server">
                                            <Items>
                                                <ext:ComboBox ID="cbxPlanStateAdd" runat="server" FieldLabel="计划状态"  Editable="false">
                                                    <Items>
                                                        <ext:ListItem Text="下达" Value="1">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="完成" Value="2">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="取消" Value="3">
                                                        </ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:Hidden runat="server" ID="hideMode" Text="Add" />
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
                <ext:Window ID="winComplete" runat="server" Icon="MonitorAdd" Closable="false" Title="计划完成情况" Width="550" Height="400" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjIDComplete" />
                                <ext:FieldSet runat="server" Title="计划信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer14"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtMp_realendCom" runat="server" FieldLabel="实际润滑日期" Editable="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200" AllowBlank="false"/>
                                                <ext:TextField ID="txtMp_realdayCom" runat="server" Width="200" Enabled="true" LabelAlign="Right" FieldLabel="实际润滑工时" InputType="Number" AllowBlank="false"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer15"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxMaintainersCom" runat="server" Disabled="false" Width="200"  AnchorHorizontal="100%" FieldLabel="维修人员" AllowBlank="false"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet2" runat="server">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer17" runat="server">
                                            <Items>
                                                <ext:TextField ID="txtMp_resultCom" runat="server" Width="450" Enabled="true"  FieldLabel="润滑结果"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnComplete}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnComplete" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnComplete_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancelComplete" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancelComplete_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
