﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairStandards.aspx.cs" Inherits="Manager_Equipment_Repair_RepairStandards" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>维修标准定义</title>
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
                if (parseInt((new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 0) {

                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="最后维修日期已超期，请联系处理！">{0}</div>', value);
                }
                else {
                    return Ext.String.format('<div style="color:black;font-weight:bolder;" title="">{0}</div>', value);
                }
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
                App.direct.pnlList_Edit(record.data.Mp_StandId, {
                    success: function () { },
                    failure: function () { }
                });
            }
            else if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Add") {
                App.direct.pnlList_Add(record.data.Mp_StandId, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var Mp_StandId = record.data.Mp_StandId;
            App.direct.pnlList_Delete(Mp_StandId, {
                success: function () { },
                failure: function () { }
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
                                <ext:Button runat="server" Icon="FolderAdd" Text="复制" ID="btnCopy">
                                    <ToolTips><ext:ToolTip ID="ToolTip4" runat="server" Html="点击进行复制" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnCopy_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="FolderConnect" Text="生成维修计划" ID="btnCreatePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击生成计划" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnCreatePlan_Click"/></DirectEvents>
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
                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Mp_StandId">
                                    <Fields>
                                        <ext:ModelField Name="Mp_StandId" />
                                        <ext:ModelField Name="ImportName" />
                                        <ext:ModelField Name="TypeName" />
                                        <ext:ModelField Name="workshop" />
                                        <ext:ModelField Name="Mp_name" />
                                        <ext:ModelField Name="Equip_name" />
                                        <ext:ModelField Name="Mp_stand" />
                                        <ext:ModelField Name="Mp_period" />
                                        <ext:ModelField Name="Mp_date" />
                                        <ext:ModelField Name="In_date" />
                                        <ext:ModelField Name="Next_date" />
                                        <ext:ModelField Name="USER_NAME" />
                                        <ext:ModelField Name="Mem_note" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Mp_StandId" runat="server" Text="标准号" DataIndex="Mp_StandId" Hidden="false" Width="65"/>
                            <ext:Column ID="clImportName" runat="server" Text="重要程度" DataIndex="ImportName" Width="65"/>
                            <ext:Column ID="clTypeName" runat="server" Text="维修类型" DataIndex="TypeName" Width="65"/>
                            <ext:Column ID="clworkshop" runat="server" Text="厂房" DataIndex="workshop" Width="65"/>
                            <ext:Column ID="clMp_name" runat="server" Text="维修部件" DataIndex="Mp_name" Width="100"/>
                            <ext:Column ID="Column1" runat="server" Text="机台" DataIndex="Equip_name" Width="50" />
                            <ext:Column ID="Column20" runat="server" Text="维修项目" DataIndex="Mp_stand" Width="180"/>
                            <ext:Column ID="Column6" runat="server" Text="维修周期" DataIndex="Mp_period" Width="65"/>
                            <ext:Column ID="Column7" runat="server" Text="标准启用日期" DataIndex="Mp_date" Width="85"/>
                            <ext:Column ID="Column12" runat="server" Text="最后维修日期" DataIndex="In_date" Width="85"/>
                            <ext:Column ID="Column21" runat="server" Text="下次维修日期" DataIndex="Next_date" Width="85">
                                <Renderer Fn="validDateChange" />
                            </ext:Column>
                            <ext:Column ID="Column10" runat="server" Text="录入人" DataIndex="USER_NAME" Width="80"/>
                            <ext:Column ID="Column2" runat="server" Text="备注" DataIndex="Mem_note" Width="120"/>
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
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="维修标准定义维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
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
                                <ext:FieldSet runat="server" Title="维修标准信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" Padding="5" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxImport" runat="server" FieldLabel="重要程度"   AnchorHorizontal="100%" Width="200"  Editable="false"/>
                                                <ext:ComboBox ID="cbxType" runat="server"  FieldLabel="维护类型" LabelAlign="Right"  AnchorHorizontal="100%" Width="200"  Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxEquipClass" runat="server" FieldLabel="设备类别"  AnchorHorizontal="100%" Width="200" LabelAlign="Right" LabelWidth="75" DisplayField="Equip_classname" ValueField="Equip_class" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquipClass">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model5">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Equip_class" />
                                                                        <ext:ModelField Name="Equip_classname" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxEquipClass_SelectChanged" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cbxEquip" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="机台" LabelAlign="Right" LabelWidth="75" DisplayField="Equip_name" ValueField="Equip_code" Editable="false" AllowBlank="false">
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
                                            </Items>
                                        </ext:FieldContainer>
                                                
                                        <ext:FieldContainer ID="FieldContainer12"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxEquipType" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="部件名称" LabelAlign="Right" LabelWidth="75" DisplayField="Mp_name" ValueField="Mp_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquipType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model2">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Mp_code" />
                                                                        <ext:ModelField Name="Mp_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    </ext:ComboBox>
                                                <ext:TextField ID="txtWorkShop" runat="server" Width="200" Enabled="false"  FieldLabel="厂房"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtMp_period" runat="server" Width="200" Enabled="true"  FieldLabel="维修周期" AllowBlank="false" InputType="Number"/>
                                                <ext:DateField ID="txtStartDate" runat="server" FieldLabel="启用日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="维修信息">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="维修项目">
                                            <Items>
                                                <ext:TextField ID="txtMp_stand" runat="server" Width="280"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer11" runat="server" FieldLabel="备注">
                                            <Items>
                                                <ext:TextField ID="txtMem_note" runat="server" Width="280"/>
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
            <ext:Window ID="winCopy" runat="server" Icon="MonitorAdd" Closable="false" Title="标准复制" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:FieldSet runat="server" Title="原机台" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer6"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxEquipCopy" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="机台" LabelAlign="Right" LabelWidth="75" DisplayField="Equip_name" ValueField="Equip_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquipCopy">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model4">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Equip_code" />
                                                                        <ext:ModelField Name="Equip_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxEquipCopy_SelectChanged" />
                                                    </DirectEvents>
                                                    </ext:ComboBox>
                                                <ext:ComboBox ID="cbxEquipTypeCopy" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="部件" LabelAlign="Right" LabelWidth="75" DisplayField="Mp_name" ValueField="Mp_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquipTypeCopy">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model6">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Mp_code" />
                                                                        <ext:ModelField Name="Mp_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxEquipTypeCopy_SelectChanged" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet2" runat="server" Title="目标机台" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                            <ext:ComboBox ID="cbxEquipCopyTo" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="机台" LabelAlign="Right" LabelWidth="75" DisplayField="Equip_name" ValueField="Equip_code" Editable="false" AllowBlank="false">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeEquipCopyTo">
                                                        <Model>
                                                            <ext:Model runat="server" ID="Model8">
                                                                <Fields>
                                                                    <ext:ModelField Name="Equip_code" />
                                                                    <ext:ModelField Name="Equip_name" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                </ext:ComboBox>
                                            <ext:ComboBox ID="cbxEquipTypeCopyTo" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="部件" LabelAlign="Right" LabelWidth="75" DisplayField="Mp_name" ValueField="Mp_code" Editable="false" AllowBlank="false" Selectable="false">
                                                <Store>
                                                    <ext:Store runat="server" ID="storeEquipTypeCopyTo">
                                                        <Model>
                                                            <ext:Model runat="server" ID="Model7">
                                                                <Fields>
                                                                    <ext:ModelField Name="Mp_code" />
                                                                    <ext:ModelField Name="Mp_name" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                            </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSaveCopy}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:Hidden runat="server" ID="Hidden2" Text="Add" />
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSaveCopy" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSaveCopy_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancelCopy" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancelCopy_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            
                <ext:Window ID="winPlanSave" runat="server" Icon="MonitorAdd" Closable="false" Title="维修计划单" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlPlanAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hidePlanObjID" />
                                <ext:FieldSet runat="server" Title="计划信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer20"  runat="server" Layout="HBoxLayout" Padding="5" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxPlanImport" runat="server" FieldLabel="重要程度"  AllowBlank="false"  AnchorHorizontal="100%" Width="200"  Editable="false"/>
                                                <ext:ComboBox ID="cbxPlanType" runat="server"  FieldLabel="维护类型" AllowBlank="false" LabelAlign="Right"  AnchorHorizontal="100%" Width="200"  Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer19"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxPlanEquipClass" runat="server" FieldLabel="设备类别"  AnchorHorizontal="100%" Width="200" LabelWidth="75" DisplayField="Equip_classname" ValueField="Equip_class" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storePlanEquipClass">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model11">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Equip_class" />
                                                                        <ext:ModelField Name="Equip_classname" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxPlanEquipClass_SelectChanged" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cbxPlanEquip" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="机台" LabelAlign="Right" LabelWidth="75" DisplayField="Equip_name" ValueField="Equip_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storePlanEquip">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model10">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Equip_code" />
                                                                        <ext:ModelField Name="Equip_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxPlanEquip_SelectChanged" />
                                                    </DirectEvents>
                                                    </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                                
                                        <ext:FieldContainer ID="FieldContainer18"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxPlanEquipType" runat="server" Disabled="false" Width="200" AnchorHorizontal="100%" FieldLabel="部件名称" LabelWidth="75" DisplayField="Mp_name" ValueField="Mp_code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storePlanEquipType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model9">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Mp_code" />
                                                                        <ext:ModelField Name="Mp_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    </ext:ComboBox>
                                                <ext:TextField ID="txtMp_planday" runat="server" Width="200" Enabled="true" LabelAlign="Right"  FieldLabel="计划维修工时" AllowBlank="false" InputType="Number"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer17"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtMp_plandate" runat="server" FieldLabel="计划维修日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                                <ext:ComboBox ID="cbxMaintainers" runat="server" Disabled="false" Width="200" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="维修人员"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer8"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtRepairuser" runat="server" Width="200" Enabled="true"  FieldLabel="维修参与人"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet10" runat="server" Title="维修内容">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer16" runat="server">
                                            <Items>
                                                <ext:TextField ID="txtMp_info" runat="server" Width="450" FieldLabel="维修内容"/>
                                                <ext:DateField ID="txtMp_realend" runat="server" FieldLabel="维修结束日期" Editable="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer15" runat="server">
                                            <Items>
                                                <ext:TextField ID="txtMp_realday" runat="server" Width="200" Enabled="true"  FieldLabel="实际维修工时" InputType="Number"/>
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
                                        <ext:FieldContainer ID="FieldContainer10" runat="server">
                                            <Items>
                                                <ext:TextField ID="txtMp_result" runat="server" Width="450" Enabled="true"  FieldLabel="维修结论"/>
                                                <ext:TextField ID="txtMp_memo" runat="server" Width="450" FieldLabel="备件使用情况"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnPlanSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnPlanSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnPlanSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnPlanCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnPlanCancel_Click"/>
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
