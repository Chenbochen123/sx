﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckStand.aspx.cs" Inherits="Manager_RubberQuality_BasicInfo_CheckStand" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="Stylesheet" type="text/css" href="CheckStand.css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden runat="server" ID="HiddenNorthMaterCode" />
    <ext:Hidden runat="server" ID="HiddenMasterMaterCode" />
    <ext:Hidden runat="server" ID="HiddenStandId" />
    <ext:Hidden runat="server" ID="HiddenItemCd" />
    <ext:Hidden runat="server" ID="HiddenDetailWeightId" />
    <ext:Hidden runat="server" ID="HiddenGradeWeightId" />
    <ext:Hidden runat="server" ID="HiddenButtonCommandName" />
    <ext:Hidden runat="server" ID="HiddenMasterCopyToMaterCode" />
    <ext:Hidden runat="server" ID="HiddenEquipWeightId" />
    <ext:Hidden runat="server" ID="HiddenEquipCheckEquipCode" />
    <ext:Hidden runat="server" ID="HiddenEquipGradeWeightId" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar ID="ToolbarNorth" runat="server">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNothOperMater" Icon="StarGoldHalfGrey" Text="显示胶料树" Hidden="true">
                                <Listeners>
                                    <Click Handler="if (#{TreePanelMater}.hidden == true) { #{TreePanelMater}.show(); this.setText('隐藏胶料树'); } else { #{TreePanelMater}.hide(); this.setText('显示胶料树'); }" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthOperGrade" Icon="StarGoldHalfGrey" Text="显示扩展明细"
                                Hidden="true">
                                <Listeners>
                                    <Click Handler="if (#{TabPanelGrade}.hidden == true) { #{TabPanelGrade}.show(); this.setText('隐藏扩展明细'); } else { #{TabPanelGrade}.hide(); this.setText('显示扩展明细'); }" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonHistory" Icon="Magnifier" Text="历史查询" Hidden="true">
                                <DirectEvents>
                                    <Click OnEvent="ButtonHistory_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelNorth" Layout="ColumnLayout">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldNorthMaterName" FieldLabel="胶料名称"
                                LabelAlign="Right" Editable="false" Width="300">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="选择" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldNorthMaterName_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="分类标准" LabelAlign="Right"
                                Editable="false" />
                                 <ext:ComboBox runat="server" ID="ComboBoxRubType" FieldLabel="配方类型" Hidden="true"
                                LabelAlign="Right" Editable="false" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandVisionStat" FieldLabel="版本状态"
                                LabelAlign="Right" Editable="false" />
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:TreePanel ID="TreePanelMater" runat="server" Region="West" Width="175" AutoScroll="true"
                Hidden="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarMater">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldMater" EnableKeyEvents="true" Width="170"
                                FieldLabel="过滤" LabelAlign="Right" LabelWidth="40">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清除筛选" />
                                </Triggers>
                                <DirectEvents>
                                    <KeyUp OnEvent="TriggerFieldMater_KeyUp" Buffer="2500">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="TreePanelMater" />
                                        <ExtraParams>
                                            <ext:Parameter Name="FilterText" Value="this.getValue()" Mode="Raw" />
                                        </ExtraParams>
                                    </KeyUp>
                                    <TriggerClick OnEvent="TriggerFieldMater_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="TreePanelMater" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Fields>
                    <ext:ModelField Name="MajorTypeID">
                    </ext:ModelField>
                    <ext:ModelField Name="MinorTypeID">
                    </ext:ModelField>
                    <ext:ModelField Name="MaterCode">
                    </ext:ModelField>
                </Fields>
                <Root>
                    <ext:Node NodeID="root" Text="所有物料">
                        <CustomAttributes>
                            <ext:ConfigItem Name="MajorTypeID" Value="" Mode="Value">
                            </ext:ConfigItem>
                            <ext:ConfigItem Name="MinorTypeID" Value="" Mode="Value">
                            </ext:ConfigItem>
                            <ext:ConfigItem Name="MaterCode" Value="" Mode="Value">
                            </ext:ConfigItem>
                        </CustomAttributes>
                    </ext:Node>
                </Root>
                <View>
                    <ext:TreeView runat="server" ID="TreeViewMater" />
                </View>
                <DirectEvents>
                    <ItemClick OnEvent="TreePanelMater_ItemClick">
                        <ExtraParams>
                            <ext:Parameter Name="NodeID" Value="record.getId()" Mode="Raw" />
                            <ext:Parameter Name="MajorTypeID" Value="record.get('MajorTypeID')" Mode="Raw" />
                            <ext:Parameter Name="MinorTypeID" Value="record.get('MinorTypeID')" Mode="Raw" />
                            <ext:Parameter Name="MaterCode" Value="record.get('MaterCode')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelCenter" />
                    </ItemClick>
                </DirectEvents>
                <Listeners>
                    <BeforeLoad Fn="TreePanelMater_BeforeLoad" />
                </Listeners>
            </ext:TreePanel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" AutoScroll="true" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel ID="GridPanelMaster" runat="server" Collapsible="false" Height="200"
                        Frame="false" AutoScroll="true" Region="North">
                        <TopBar>
                            <ext:Toolbar runat="server">
                                <Items>
                                    <ext:Button ID="ButtonMasterAdd" runat="server" Text="添加" Icon="PageAdd">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterAdd_Click" />
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button ID="ButtonMasterEdit" runat="server" Text="修改/提交审核" Icon="PageEdit">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterEdit_Click" />
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button ID="ButtonMasterDelete" runat="server" Text="删除" Icon="PageDelete">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterDelete_Click">
                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelCenter" />
                                                <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除选中的质检标准么？" />
                                            </Click>
                                        </DirectEvents>
                                        <Listeners>
                                            <Click Fn="ButtonMasterDelete_BeforeClick" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="ButtonMasterKey" Text="版本启停" Icon="PageKey" Split="true">
                                        <Menu>
                                            <ext:Menu runat="server">
                                                <Items>
                                                    <ext:MenuItem Text="启用" runat="server">
                                                        <DirectEvents>
                                                            <Click OnEvent="ButtonMasterEnabled_Click">
                                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelCenter" />
                                                                <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要【启用】当前质检标准的版本么？" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:MenuItem>
                                                    <ext:MenuItem Text="停用" runat="server">
                                                        <DirectEvents>
                                                            <Click OnEvent="ButtonMasterDisabled_Click">
                                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelCenter" />
                                                                <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要【停用】当前质检标准的版本么？" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:MenuItem>
                                                    <ext:MenuItem Text="作废" runat="server">
                                                        <DirectEvents>
                                                            <Click OnEvent="ButtonMasterInvalid_Click">
                                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelCenter" />
                                                                <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要【作废】当前质检标准的版本么？" />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:MenuItem>
                                                </Items>
                                            </ext:Menu>
                                        </Menu>
                                    </ext:Button>
                                    <ext:Button ID="ButtonMasterCopy" runat="server" Text="复制" Icon="PageCopy">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterCopy_Click">
                                                <EventMask ShowMask="false" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button ID="ButtonDeleteRestore" runat="server" Text="删除复原" Icon="PageEdit" Hidden="true">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonDeleteRestore_Click" />
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button ID="ButtonMasterAudit" runat="server" Text="审核" Icon="PageGear">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterAudit_Click">
                                                <EventMask ShowMask="false" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="ButtonMasterEditRegDateTime" Text="修改生效时间" Icon="DateEdit">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonMasterEditRegDateTime_Click">
                                                <EventMask ShowMask="true" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="StoreMaster" runat="server" GroupField="StandTypeName">
                                <Model>
                                    <ext:Model ID="ModelMaster" runat="server" IDProperty="StandId">
                                        <Fields>
                                            <ext:ModelField Name="StandId" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="StandTypeName" />
                                            <ext:ModelField Name="DefineDate" />
                                            <ext:ModelField Name="StandVision" />
                                            <ext:ModelField Name="StandVisionStatExp" />
                                            <ext:ModelField Name="RegDateTime" Type="Date" />
                                            <ext:ModelField Name="QuaCompute" Type="Boolean" />
                                            <ext:ModelField Name="Choiceness" Type="Boolean" />
                                            <ext:ModelField Name="LastModifyUserName" />
                                            <ext:ModelField Name="LastModifyTime" Type="Date" />
                                            <ext:ModelField Name="LastSubmitUserName" />
                                            <ext:ModelField Name="LastSubmitTime" Type="Date" />
                                            <ext:ModelField Name="LastAuditUserName" />
                                            <ext:ModelField Name="LastAuditTime" Type="Date" />
                                            <ext:ModelField Name="LastOperateTime" Type="Date" />
                                            <ext:ModelField Name="LastAuditMemo" />
                                            <ext:ModelField Name="LLStandVision" />
                                            <ext:ModelField Name="DeleteFlag" />
                                             <ext:ModelField Name="PmtTypeName" />
                                              <ext:ModelField Name="Audit" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:Column runat="server" Width="15" Draggable="false" MenuDisabled="true" Resizable="false"
                                    Sortable="false" Selectable="false" />
                                <ext:Column ID="Column6" runat="server" Text="标准ID" DataIndex="StandId"
                                    Width="80" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterMaterName" runat="server" Text="胶料名称" DataIndex="MaterName"
                                    Width="250" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterDefineDate" runat="server" Text="定义日期" DataIndex="DefineDate"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterLLStandVision" runat="server" Text="配方版本" DataIndex="LLStandVision"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterStandVision" runat="server" Text="版本" DataIndex="StandVision"
                                    Width="45" Draggable="false" MenuDisabled="true" />
                                
                                <ext:Column ID="ColumnMasterStandVisionStatExp" runat="server" Text="版本状态" DataIndex="StandVisionStatExp"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterRegDateTime" runat="server" Text="生效日期" DataIndex="RegDateTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd" />
                                          <ext:Column ID="Column3" runat="server" Text="起始班次" DataIndex="PmtTypeName"
                                    Width="65" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterLastModifyUserName" runat="server" Text="修改人" DataIndex="LastModifyUserName"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterLastModifyTime" runat="server" Text="修改时间" DataIndex="LastModifyTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss"  Hidden="true"/>
                                <ext:Column ID="ColumnMasterLastSubmitUserName" runat="server" Text="提交人" DataIndex="LastSubmitUserName"
                                    Width="60" Draggable="false" MenuDisabled="true"   Hidden="true" />
                        
                                <ext:Column ID="ColumnMasterLastAuditMemo" runat="server" Text="审核意见" DataIndex="LastAuditMemo"
                                    Width="160" Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:Column ID="ColumnMasterLastAuditUserName" runat="server" Text="审核人" DataIndex="LastAuditUserName"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                     
                                <ext:Column ID="Column4" runat="server" Text="是否审核" DataIndex="Audit"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelMaster" Mode="Single">
                                <DirectEvents>
                                    <SelectionChange OnEvent="RowSelectionModelMaster_SelectionChange">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelDetail" />
                                        <ExtraParams>
                                            <ext:Parameter Name="StandId" Value="selected[0].get('StandId')" Mode="Raw" />
                                        </ExtraParams>
                                    </SelectionChange>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <View>
                            <ext:GridView ID="GridViewMaster" runat="server">
                                <Listeners>
                                    <Refresh Fn="setGroupStyle" />
                                </Listeners>
                                <GetRowClass Fn="SetRowClass" />
                            </ext:GridView>
                        </View>
                        <Features>
                            <ext:Grouping ID="GroupingMaster" runat="server" GroupHeaderTplString='{name} ({rows.length} 项)' />
                        </Features>
                        <BottomBar>
                            <ext:StatusBar runat="server" Height="10" />
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:Panel runat="server" ID="PanelDetail" AutoHeight="true" Region="Center" Layout="BorderLayout">
                        <Items>
                            <ext:GridPanel ID="GridPanelDetail" runat="server" Collapsible="false" Frame="false"
                                ButtonAlign="Left" AutoScroll="true" Region="Center">
                                <TopBar>
                                    <ext:Toolbar runat="server">
                                        <Items>
                                            <ext:Button runat="server" ID="ButtonDetailAdd" Icon="TableAdd" Text="添加标准明细">
                                                <DirectEvents>
                                                    <Click OnEvent="ButtonDetailAdd_Click">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelDetail" />
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="ButtonDetailEdit" Icon="TableEdit" Text="修改标准明细">
                                                <DirectEvents>
                                                    <Click OnEvent="ButtonDetailEdit_Click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button runat="server" ID="ButtonDetailDelete" Icon="TableDelete" Text="删除标准明细">
                                                <DirectEvents>
                                                    <Click OnEvent="ButtonDetailDelete_Click">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="PanelDetail" />
                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除质检标准明细信息么？" />
                                                    </Click>
                                                </DirectEvents>
                                                <Listeners>
                                                    <Click Fn="ButtonDetailDelete_BeforeClick" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Store>
                                    <ext:Store ID="StoreDetail" runat="server" ClearRemovedOnLoad="true">
                                        <Model>
                                            <ext:Model ID="ModelDetail" runat="server" IDProperty="StandId,ItemCd,WeightId">
                                                <Fields>
                                                    <ext:ModelField Name="StandId" />
                                                    <ext:ModelField Name="ItemCd" />
                                                    <ext:ModelField Name="WeightId" />
                                                    <ext:ModelField Name="ItemName" />
                                                    <ext:ModelField Name="PermMin" />
                                                    <ext:ModelField Name="IfMin" />
                                                    <ext:ModelField Name="PermMax" />
                                                    <ext:ModelField Name="IfMax" />
                                                    <ext:ModelField Name="JudgeResult" />
                                                    <ext:ModelField Name="DealNotion" />
                                                    <ext:ModelField Name="DrawMark" />
                                                    <ext:ModelField Name="Grade" />
                                                    <ext:ModelField Name="CardMark2" />
                                                    <ext:ModelField Name="QuaFrequency" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                        <Sorters>
                                            <%--<ext:DataSorter Property="WeightId" Direction="ASC" />--%>
                                        </Sorters>
                                    </ext:Store>
                                </Store>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelDetail" Mode="Single">
                                        <DirectEvents>
                                            <SelectionChange OnEvent="RowSelectionModelDetail_SelectionChange">
                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelGrade" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="StandId" Value="selected[0].get('StandId')" Mode="Raw" />
                                                    <ext:Parameter Name="ItemCd" Value="selected[0].get('ItemCd')" Mode="Raw" />
                                                    <ext:Parameter Name="WeightId" Value="selected[0].get('WeightId')" Mode="Raw" />
                                                </ExtraParams>
                                            </SelectionChange>
                                        </DirectEvents>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <ColumnModel ID="ColumnModelDetail" runat="server">
                                    <Columns>
                                        <ext:Column runat="server" Width="15" Draggable="false" MenuDisabled="true" Resizable="false"
                                            Selectable="false" />
                                        <ext:Column ID="ColumnDetailItemName" runat="server" Text="检验项目" DataIndex="ItemName"
                                            Width="150" Draggable="false" MenuDisabled="true" />
                                        <ext:Column ID="ColumnDetailPermMin" runat="server" Text="下限值" DataIndex="PermMin"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                                        <ext:CheckColumn ID="CheckColumnIfMin" runat="server" Text="容许下限" DataIndex="IfMin"  Hidden="true"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                                        <ext:Column ID="ColumnDetailPermMax" runat="server" Text="上限值" DataIndex="PermMax"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                                        <ext:CheckColumn ID="CheckColumnDetailIfMax" runat="server" Text="容许上限" DataIndex="IfMax"  Hidden="true"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                                             <ext:Column ID="Column5" runat="server" Text="是否合格" DataIndex="JudgeResult"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                       <%--                 <ext:CheckColumn ID="CheckColumnDetailJudgeResult" runat="server" Text="是否合格" DataIndex="JudgeResult"
                                            Width="95" Draggable="false" MenuDisabled="true" />--%>
                                        <ext:Column ID="ColumnDetailDealNotion" runat="server" Text="处理方式" DataIndex="DealNotion"
                                            Width="180" Draggable="false" MenuDisabled="true" />
                                 
                                        <ext:Column ID="ColumnDetailQuaFrequency" runat="server" Text="检验频率" Hidden="true" DataIndex="QuaFrequency"
                                            Width="95" Draggable="false" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:StatusBar runat="server" Height="10" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:TabPanel runat="server" ID="TabPanelGrade" Height="200" Hidden="true" Region="South">
                                <Items>
                                    <ext:Panel runat="server" ID="PanelGrade" Collapsible="false" Frame="false" AutoScroll="true"
                                        Title="等级标准">
                                        <Items>
                                            <ext:GridPanel ID="GridPanelGrade" runat="server" Collapsible="false" Frame="false"
                                                AutoScroll="true">
                                                <TopBar>
                                                    <ext:Toolbar ID="ToolbarGrade" runat="server">
                                                        <Items>
                                                            <ext:Button runat="server" ID="ButtonGradeAdd" Icon="MonitorAdd" Text="添加标准明细等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonGradeAdd_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonGradeEdit" Icon="MonitorEdit" Text="修改标准明细等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonGradeEdit_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonGradeDelete" Icon="MonitorDelete" Text="删除标准明细等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonGradeDelete_Click">
                                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelGrade" />
                                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除质检标准明细等级信息么？" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="StoreGrade" runat="server">
                                                        <Model>
                                                            <ext:Model ID="ModelGrade" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="StandId" />
                                                                    <ext:ModelField Name="ItemCd" />
                                                                    <ext:ModelField Name="WeightId" />
                                                                    <ext:ModelField Name="PermMin" />
                                                                    <ext:ModelField Name="IfMin" />
                                                                    <ext:ModelField Name="PermMax" />
                                                                    <ext:ModelField Name="IfMax" />
                                                                    <ext:ModelField Name="JudgeResult" />
                                                                    <ext:ModelField Name="DealNotion" />
                                                                    <ext:ModelField Name="DrawMark" />
                                                                    <ext:ModelField Name="Grade" />
                                                                    <ext:ModelField Name="CardMark2" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                        <Sorters>
                                                            <ext:DataSorter Property="WeightId" Direction="ASC" />
                                                        </Sorters>
                                                    </ext:Store>
                                                </Store>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelGrade" Mode="Single">
                                                        <DirectEvents>
                                                            <SelectionChange OnEvent="RowSelectionModelGrade_SelectionChange">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="StandId" Value="selected[0].get('StandId')" Mode="Raw" />
                                                                    <ext:Parameter Name="ItemCd" Value="selected[0].get('ItemCd')" Mode="Raw" />
                                                                    <ext:Parameter Name="WeightId" Value="selected[0].get('WeightId')" Mode="Raw" />
                                                                </ExtraParams>
                                                            </SelectionChange>
                                                        </DirectEvents>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel ID="ColumnModelGrade" runat="server">
                                                    <Columns>
                                                        <ext:Column ID="Column1" runat="server" Width="15" Draggable="false" Resizable="false"
                                                            Selectable="false" />
                                                        <ext:Column ID="ColumnGradePermMin" runat="server" Text="偏差下限值" DataIndex="PermMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnGradeIfMin" runat="server" Text="容许下限" DataIndex="IfMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnGradePermMax" runat="server" Text="偏差上限值" DataIndex="PermMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnGradeIfMax" runat="server" Text="容许上限" DataIndex="IfMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnGradeJudgeResult" runat="server" Text="是否合格" DataIndex="JudgeResult"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnGradeDealNotion" runat="server" Text="检测意见" DataIndex="DealNotion"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnGradeDrawMark" runat="server" Text="划胶标识" DataIndex="DrawMark"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnGradeGrade" runat="server" Text="等级" DataIndex="Grade" Width="95"
                                                            Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnGradeCardMark2" runat="server" Text="子标识" DataIndex="CardMark2"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" ID="PanelEquip" Collapsible="false" Frame="false" AutoScroll="true"
                                        Title="机台标准">
                                        <Items>
                                            <ext:GridPanel ID="GridPanelEquip" runat="server" Collapsible="false" Height="150"
                                                Frame="false" AutoScroll="true">
                                                <TopBar>
                                                    <ext:Toolbar ID="ToolbarEquip" runat="server">
                                                        <Items>
                                                            <ext:Button runat="server" ID="ButtonEquipAdd" Icon="MonitorAdd" Text="添加机台明细">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipAdd_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonEquipEdit" Icon="MonitorEdit" Text="修改机台明细">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipEdit_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonEquipDelete" Icon="MonitorDelete" Text="删除机台明细">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipDelete_Click">
                                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelEquip" />
                                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除质检标准机台明细信息么？" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="StoreEquip" runat="server">
                                                        <Model>
                                                            <ext:Model ID="ModelEquip" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="StandId" />
                                                                    <ext:ModelField Name="ItemCd" />
                                                                    <ext:ModelField Name="WeightId" />
                                                                    <ext:ModelField Name="CheckEquipCode" />
                                                                    <ext:ModelField Name="CheckEquipName" />
                                                                    <ext:ModelField Name="PermMin" />
                                                                    <ext:ModelField Name="IfMin" />
                                                                    <ext:ModelField Name="PermMax" />
                                                                    <ext:ModelField Name="IfMax" />
                                                                    <ext:ModelField Name="JudgeResult" />
                                                                    <ext:ModelField Name="DealNotion" />
                                                                    <ext:ModelField Name="DrawMark" />
                                                                    <ext:ModelField Name="Grade" />
                                                                    <ext:ModelField Name="CardMark2" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                        <Sorters>
                                                            <ext:DataSorter Property="WeightId" Direction="ASC" />
                                                        </Sorters>
                                                    </ext:Store>
                                                </Store>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelEquip" Mode="Single">
                                                        <DirectEvents>
                                                            <SelectionChange OnEvent="RowSelectionModelEquip_SelectionChange">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="StandId" Value="selected[0].get('StandId')" Mode="Raw" />
                                                                    <ext:Parameter Name="ItemCd" Value="selected[0].get('ItemCd')" Mode="Raw" />
                                                                    <ext:Parameter Name="WeightId" Value="selected[0].get('WeightId')" Mode="Raw" />
                                                                    <ext:Parameter Name="CheckEquipCode" Value="selected[0].get('CheckEquipCode')" Mode="Raw" />
                                                                </ExtraParams>
                                                            </SelectionChange>
                                                        </DirectEvents>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel ID="ColumnModelEquip" runat="server">
                                                    <Columns>
                                                        <ext:Column ID="Column2" runat="server" Width="15" Draggable="false" Resizable="false"
                                                            Selectable="false" />
                                                        <ext:Column ID="ColumnEquipCheckEquipCode" runat="server" Text="检验机台" DataIndex="CheckEquipName"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipPermMin" runat="server" Text="偏差下限值" DataIndex="PermMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipIfMin" runat="server" Text="容许下限" DataIndex="IfMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipPermMax" runat="server" Text="偏差上限值" DataIndex="PermMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipIfMax" runat="server" Text="容许上限" DataIndex="IfMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipJudegResult" runat="server" Text="是否合格" DataIndex="JudgeResult"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipDealNotion" runat="server" Text="检测意见" DataIndex="DealNotion"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipDrawMark" runat="server" Text="划胶标识" DataIndex="DrawMark"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGrade" runat="server" Text="等级" DataIndex="Grade" Width="95"
                                                            Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipCardMark2" runat="server" Text="子标识" DataIndex="CardMark2"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                            <ext:Panel runat="server" Height="10" />
                                            <ext:GridPanel ID="GridPanelEquipGrade" runat="server" Collapsible="false" Frame="false"
                                                AutoScroll="true" Height="150">
                                                <TopBar>
                                                    <ext:Toolbar ID="ToolbarEquipGrade" runat="server">
                                                        <Items>
                                                            <ext:Button runat="server" ID="ButtonEquipGradeAdd" Icon="MonitorAdd" Text="添加标准机台等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipGradeAdd_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonEquipGradeEdit" Icon="MonitorEdit" Text="修改标准机台等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipGradeEdit_Click" />
                                                                </DirectEvents>
                                                            </ext:Button>
                                                            <ext:Button runat="server" ID="ButtonEquipGradeDelete" Icon="MonitorDelete" Text="删除标准机台等级">
                                                                <DirectEvents>
                                                                    <Click OnEvent="ButtonEquipGradeDelete_Click">
                                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelEquipGrade" />
                                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除质检标准机台等级信息么？" />
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:Button>
                                                        </Items>
                                                    </ext:Toolbar>
                                                </TopBar>
                                                <Store>
                                                    <ext:Store ID="StoreEquipGrade" runat="server">
                                                        <Model>
                                                            <ext:Model ID="ModelEquipGrade" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="StandId" />
                                                                    <ext:ModelField Name="ItemCd" />
                                                                    <ext:ModelField Name="WeightId" />
                                                                    <ext:ModelField Name="CheckEquipCode" />
                                                                    <ext:ModelField Name="CheckEquipName" />
                                                                    <ext:ModelField Name="PermMin" />
                                                                    <ext:ModelField Name="IfMin" />
                                                                    <ext:ModelField Name="PermMax" />
                                                                    <ext:ModelField Name="IfMax" />
                                                                    <ext:ModelField Name="JudgeResult" />
                                                                    <ext:ModelField Name="DealNotion" />
                                                                    <ext:ModelField Name="DrawMark" />
                                                                    <ext:ModelField Name="Grade" />
                                                                    <ext:ModelField Name="CardMark2" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                        <Sorters>
                                                            <ext:DataSorter Property="WeightId" Direction="ASC" />
                                                        </Sorters>
                                                    </ext:Store>
                                                </Store>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel runat="server" ID="RowSelectionModelEquipGrade" Mode="Single">
                                                        <DirectEvents>
                                                            <SelectionChange OnEvent="RowSelectionModelEquipGrade_SelectionChange">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="StandId" Value="selected[0].get('StandId')" Mode="Raw" />
                                                                    <ext:Parameter Name="ItemCd" Value="selected[0].get('ItemCd')" Mode="Raw" />
                                                                    <ext:Parameter Name="WeightId" Value="selected[0].get('WeightId')" Mode="Raw" />
                                                                    <ext:Parameter Name="CheckEquipCode" Value="selected[0].get('CheckEquipCode')" Mode="Raw" />
                                                                </ExtraParams>
                                                            </SelectionChange>
                                                        </DirectEvents>
                                                    </ext:RowSelectionModel>
                                                </SelectionModel>
                                                <ColumnModel ID="ColumnModelEquipGrade" runat="server">
                                                    <Columns>
                                                        <ext:Column runat="server" Width="15" Draggable="false" MenuDisabled="true" Resizable="false"
                                                            Selectable="false" />
                                                        <ext:Column ID="ColumnEquipGradeCheckEquipName" runat="server" Text="检验机台" DataIndex="CheckEquipName"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradePermMin" runat="server" Text="偏差下限值" DataIndex="PermMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipGradeIfMin" runat="server" Text="容许下限" DataIndex="IfMin"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradePermMax" runat="server" Text="偏差上限值" DataIndex="PermMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipGradeIfMax" runat="server" Text="容许上限" DataIndex="IfMax"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:CheckColumn ID="CheckColumnEquipGradeJudgeResult" runat="server" Text="是否合格"
                                                            DataIndex="JudgeResult" Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradeDealNotion" runat="server" Text="检测意见" DataIndex="DealNotion"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradeDrawMark" runat="server" Text="划胶标识" DataIndex="DrawMark"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradeGrade" runat="server" Text="等级" DataIndex="Grade"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                        <ext:Column ID="ColumnEquipGradeCardMark2" runat="server" Text="子标识" DataIndex="CardMark2"
                                                            Width="95" Draggable="false" MenuDisabled="true" />
                                                    </Columns>
                                                </ColumnModel>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Window runat="server" ID="WindowMaster" Closable="false" Height="250" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelMaster" ShadowMode="Frame">
                        <Items>
                            <ext:ComboBox runat="server" ID="ComboBoxMasterStandCode" FieldLabel="分类标准" EmptyText="请选择..."
                                AllowBlank="false" />
                            <ext:TriggerField runat="server" ID="TriggerFieldMasterMaterName" FieldLabel="胶料名称"
                                EmptyText="请选择..." AllowBlank="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldMasterMaterName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelMaster" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:ComboBox runat="server" ID="ComboBoxMasterStandVisionStat" FieldLabel="版本状态"
                                EmptyText="请选择..." Editable="false" Hidden="true">
                            </ext:ComboBox>
                            <ext:NumberField runat="server" ID="TextFieldMasterLLStandVision" FieldLabel="配方版本"
                             >
                            </ext:NumberField>
                            <ext:NumberField runat="server" ID="txtbanben" FieldLabel="版本"
                             >
                            </ext:NumberField>
         

                              <ext:ComboBox runat="server" ID="RubType" FieldLabel="起始班次" EmptyText="请选择..."
                                AllowBlank="false" />
                            <ext:DateField runat="server" ID="DateFieldMasterRegDateTime" EmptyText="选择生效日期"
                                FieldLabel="生效日期" AllowBlank="false" Editable="false" Format="yyyy-MM-dd" />
                            <ext:TimeField runat="server" ID="TimeFieldMasterRegDateTime" EmptyText="选择生效时间"
                                Text="00:00:00" FieldLabel="生效时间" Format="HH:mm:ss" AllowBlank="false"  Hidden="true"  />
                            <ext:Checkbox runat="server" ID="CheckboxMasterQuaCompute" FieldLabel="参与合格率计算" Hidden="true" />
                            <ext:Checkbox runat="server" ID="CheckboxMasterChoiceness" FieldLabel="是否为精品物料" Hidden="true" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonMasterAccept}.setDisabled(!valid);#{ButtonMasterSubmit}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonMasterAccept" Icon="Disk" Text="保存" ViewStateMode="Enabled">
                        <DirectEvents>
                            <Click OnEvent="ButtonMasterAccept_Click">
                                <EventMask ShowMask="true" Msg="保存中..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonMasterSubmit" Icon="Accept" Text="提交审核" ViewStateMode="Enabled">
                        <DirectEvents>
                            <Click OnEvent="ButtonMasterSubmit_Click">
                                <EventMask ShowMask="true" Msg="提交中..." />
                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要提交审核吗(提交后不允许修改)" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonMasterCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonMasterCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowDetail" Closable="false" Height="300" Width="380"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelDetail" ShadowMode="Frame">
                        <Items>
                            <ext:ComboBox runat="server" ID="ComboBoxDetailItemCd" FieldLabel="检验项目" EmptyText="请选择..." Width="350"
                                AllowBlank="false" ForceSelection="true" />
                            <ext:NumberField runat="server" ID="NumberFieldDetailPermMin" FieldLabel="下限值" EmptyText="请填写..."
                                AllowBlank="false" DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxDetailIfMin" FieldLabel="允许下限" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldDetailPermMax" FieldLabel="上限值" EmptyText="请填写..."
                                AllowBlank="false" DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxDetailIfMax" FieldLabel="允许上限"  Hidden="true" />
                      
                            <ext:ComboBox runat="server" ID="ComboBoxDetailQuaFrequency" FieldLabel="处理方式" EmptyText="请选择..." Width="350" 
                               Editable="false" >
                             </ext:ComboBox>

                               <ext:ComboBox runat="server" ID="ComboBoxJudge" FieldLabel="是否合格" EmptyText="请选择..." 
                               Editable="false"  AllowBlank="false" >
                             </ext:ComboBox>

                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonDetailAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonDetailAccept" Icon="Accept" Text="确定" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="ButtonDetailAccept_Click">
                                <EventMask ShowMask="true" Msg="保存中..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonDetailCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonDetailCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowGrade" Closable="false" Height="300" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelGrade" ShadowMode="Frame">
                        <Items>
                            <ext:NumberField runat="server" ID="NumberFieldGradePermMin" FieldLabel="偏差下限值" AllowBlank="false"
                                EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxGradeIfMin" FieldLabel="允许下限" />
                            <ext:NumberField runat="server" ID="NumberFieldGradePermMax" FieldLabel="偏差上限值" AllowBlank="false"
                                EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxGradeIfMax" FieldLabel="允许上限" />
                            <ext:Checkbox runat="server" ID="CheckBoxGradeJudgeResult" FieldLabel="是否合格" />
                            <ext:ComboBox runat="server" ID="ComboBoxGradeDealCode" FieldLabel="处理意见" AllowBlank="false"
                                EmptyText="请选择..." Editable="false" />
                            <ext:TextField runat="server" ID="TextFieldGradeDrawMark" FieldLabel="划胶标识" MaxLength="20"
                                MaxLengthText="最长20个字符" />
                            <ext:NumberField runat="server" ID="NumberFieldGradeGrade" FieldLabel="等级" AllowBlank="false"
                                AllowDecimals="false" MinValue="1" MinText="最小值：1" MaxValue="999" MaxText="最大值：999" />
                            <ext:TextField runat="server" ID="TextFieldGradeCardMark2" FieldLabel="子标识" MaxLength="20"
                                MaxLengthText="最长20个字符" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonGradeAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonGradeAccept" Icon="Accept" Text="确定" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="ButtonGradeAccept_Click">
                                <EventMask ShowMask="true" Msg="保存中..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonGradeCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonGradeCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowMasterCopy" Closable="false" Height="450" Width="450"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelMasterCopy">
                        <Items>
                            <ext:TriggerField ID="TriggerFieldMasterCopyFromMaterName" runat="server" FieldLabel="复制源胶料"
                                Width="420" LabelWidth="90" LabelAlign="Right" ReadOnly="true" />
                            <ext:FieldSet ID="FieldSetMasterCopy" runat="server" Title="详细信息" Width="320" Collapsible="true"
                                Collapsed="true" Disabled="true">
                                <Items>
                                    <ext:ComboBox ID="ComboBoxMasterCopyStandCode" runat="server" FieldLabel="分类标准" LabelAlign="Right" />
                                    <ext:TextField ID="TextFieldMasterCopyDefineDate" runat="server" FieldLabel="定义日期"
                                        LabelAlign="Right" />
                                    <ext:NumberField ID="NumberFieldMasterCopyStandVision" runat="server" FieldLabel="版本"
                                        LabelAlign="Right" />
                                    <ext:ComboBox ID="ComboBoxMasterCopyStandVisionStat" runat="server" FieldLabel="版本状态"
                                        LabelAlign="Right" />
                                    <ext:DateField ID="DateFieldMasterCopyRegDateTime" runat="server" FieldLabel="生效时间"
                                        LabelAlign="Right" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:Checkbox ID="CheckBoxMasterCopyQuaCompute" runat="server" FieldLabel="参与合格率计算"
                                        LabelAlign="Right" Hidden="true" />
                                    <ext:Checkbox ID="CheckBoxMasterCopyChoiceness" runat="server" FieldLabel="是否为精品物料"
                                        LabelAlign="Right" />
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet runat="server" Title="复制源项目" Width="420" Height="150" AutoScroll="true">
                                <Items>
                                    <ext:CheckboxGroup ID="CheckboxGroupMasterCopy" runat="server" ColumnsNumber="3"
                                        Vertical="true" />
                                </Items>
                            </ext:FieldSet>
                            <ext:TriggerField ID="TriggerFieldMasterCopyToMaterName" runat="server" FieldLabel="复制到目的胶料"
                                EmptyText="请选择..." AllowBlank="false" Editable="false" Width="320" LabelWidth="90"
                                LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldMasterCopyToMaterName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelMasterCopy" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:ComboBox ID="ComboBoxMasterAimStandCode" runat="server" FieldLabel="分类标准" AllowBlank="false" />
                               <ext:NumberField runat="server" ID="NumberEdt" FieldLabel="配方版本"    AllowBlank="false" 
                             >
                            </ext:NumberField>
         

                              <ext:ComboBox runat="server" ID="ComboBoxShift" FieldLabel="起始班次" EmptyText="请选择..."
                                AllowBlank="false" />
                            <ext:DateField runat="server" ID="DateFieldMasterCopyToRegDateTime" EmptyText="选择生效日期"
                                FieldLabel="生效日期" AllowBlank="false" Editable="false" Format="yyyy-MM-dd" LabelWidth="90" />
                           
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonMasterCopyAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonMasterCopyAccept" Icon="Accept" Text="确定" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="ButtonMasterCopyAccept_Click">
                                <EventMask ShowMask="true" Msg="复制中..." />
                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要复制吗" />
                                <ExtraParams>
                                    <ext:Parameter Name="ItemCdList" Value="#{CheckboxGroupMasterCopy}.getValue()" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonMasterCopyCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonMasterCopyCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowEquip" Closable="false" Height="300" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelEquip" ShadowMode="Frame">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldEquipCheckEquipName" FieldLabel="检验机台"
                                Editable="false" AllowBlank="false" EmptyText="请选择...">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" HideTrigger="true" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldEquipCheckEquipName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelEquip" />
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:NumberField runat="server" ID="NumberFieldEquipPermMin" FieldLabel="偏差下限值" AllowBlank="false"
                                EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipIfMin" FieldLabel="允许下限" />
                            <ext:NumberField runat="server" ID="NumberFieldEquipPermMax" FieldLabel="偏差上限值" AllowBlank="false"
                                EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipIfMax" FieldLabel="允许上限" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipJudgeResult" FieldLabel="是否合格" />
                            <ext:ComboBox runat="server" ID="ComboBoxEquipDealCode" FieldLabel="处理意见" AllowBlank="false"
                                EmptyText="请选择..." Editable="false" />
                            <ext:TextField runat="server" ID="TextFieldEquipDrawMark" FieldLabel="划胶标识" MaxLength="20"
                                MaxLengthText="最长20个字符" />
                            <ext:NumberField runat="server" ID="NumberFieldEquipGrade" FieldLabel="等级" AllowBlank="false"
                                AllowDecimals="false" MinValue="1" MinText="最小值：1" MaxValue="999" MaxText="最大值：999" />
                            <ext:TextField runat="server" ID="TextFieldEquipCardMark2" FieldLabel="子标识" MaxLength="20"
                                MaxLengthText="最长20个字符" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonEquipAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonEquipAccept" Icon="Accept" Text="确定" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="ButtonEquipAccept_Click">
                                <EventMask ShowMask="true" Msg="保存中..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="Button4" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonEquipCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowEquipGrade" Closable="false" Height="300" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelEquipGrade" ShadowMode="Frame">
                        <Items>
                            <ext:NumberField runat="server" ID="NumberFieldEquipGradePermMin" FieldLabel="偏差下限值"
                                AllowBlank="false" EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipGradeIfMin" FieldLabel="允许下限" />
                            <ext:NumberField runat="server" ID="NumberFieldEquipGradePermMax" FieldLabel="偏差上限值"
                                AllowBlank="false" EmptyText="请填写..." DecimalPrecision="3" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipGradeIfMax" FieldLabel="允许上限" />
                            <ext:Checkbox runat="server" ID="CheckBoxEquipGradeJudgeResult" FieldLabel="是否合格" />
                            <ext:ComboBox runat="server" ID="ComboBoxEquipGradeDealCode" FieldLabel="处理意见" AllowBlank="false"
                                EmptyText="请选择..." Editable="false" />
                            <ext:TextField runat="server" ID="TextFieldEquipGradeDrawMark" FieldLabel="划胶标识"
                                MaxLength="20" MaxLengthText="最长20个字符" />
                            <ext:NumberField runat="server" ID="NumberFieldEquipGradeGrade" FieldLabel="等级" AllowBlank="false"
                                AllowDecimals="false" MinValue="1" MinText="最小值：1" MaxValue="999" MaxText="最大值：999" />
                            <ext:TextField runat="server" ID="TextFieldEquipGradeCardMark2" FieldLabel="子标识"
                                MaxLength="20" MaxLengthText="最长20个字符" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonEquipGradeAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonEquipGradeAccept" Icon="Accept" Text="确定" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="ButtonEquipGradeAccept_Click">
                                <EventMask ShowMask="true" Msg="保存中..." />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonEquipGradeCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonEquipGradeCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window ID="WindowAudit" runat="server" Closable="false" Height="300" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelAudit" ShadowMode="Frame">
                        <Items>
                            <ext:ComboBox runat="server" ID="ComboBoxAuditStandCode" FieldLabel="分类标准" EmptyText="请选择..."
                                AllowBlank="false" />
                            <ext:TriggerField runat="server" ID="TriggerFieldAuditMaterName" FieldLabel="胶料名称"
                                EmptyText="请选择..." AllowBlank="false">
                            </ext:TriggerField>
                            <ext:ComboBox runat="server" ID="ComboBoxAuditStandVisionStat" FieldLabel="版本状态"
                                EmptyText="请选择..." Editable="false" Hidden="true">
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="TextFieldAuditLLStandVision" FieldLabel="配方版本" />
                            <ext:TextField runat="server" ID="TextFieldAuditRegDateTime" FieldLabel="生效时间"  Hidden="true" />
                            <ext:Checkbox runat="server" ID="CheckboxAuditQuaCompute" FieldLabel="参与合格率计算" Hidden="true" />
                            <ext:Checkbox runat="server" ID="CheckboxAuditChoiceness" FieldLabel="是否为精品物料" Hidden="true" />
                            <ext:TextArea runat="server" ID="TextAreaAuditAuditMemo" FieldLabel="审核意见" Rows="5" Hidden="true"
                                MaxLength="250" MaxLengthText="最长250个字符" />
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonAuditAccept" Icon="Accept" Text="审核通过">
                        <DirectEvents>
                            <Click OnEvent="ButtonAuditAccept_Click">
                                <EventMask ShowMask="true" Msg="审核通过中..." />
                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要审核通过吗" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonAuditSendback" Icon="Exclamation" Text="退回修改">
                        <DirectEvents>
                            <Click OnEvent="ButtonAuditSendback_Click">
                                <EventMask ShowMask="true" Msg="退回修改中..." />
                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要退回修改吗" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonAuditCancel" Icon="Cancel" Text="取消">
                        <DirectEvents>
                            <Click OnEvent="ButtonAuditCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <ext:Window runat="server" ID="WindowSpec" Closable="false" Height="300" Width="350"
                Resizable="false" Hidden="true" Modal="false">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelSpec">
                        <Items>
                            <ext:TextField runat="server" ID="TextFieldSpecStandName" FieldLabel="标准类型" ReadOnly="true"
                                AllowBlank="false" />
                            <ext:TriggerField runat="server" ID="TriggerFieldSpecMaterName" FieldLabel="胶料名称"
                                HideTrigger="true" Editable="false" AllowBlank="false" />
                            <ext:DateField runat="server" ID="DateFieldSpecRegDateTime" FieldLabel="生效日期" Format="yyyy-MM-dd"
                                Editable="false" AllowBlank="false" />
                            <ext:TimeField runat="server" ID="TimeFieldSpecRegDateTime" FieldLabel="生效时间" Format="HH:mm:ss"
                                AllowBlank="false" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonSpecAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonSpecAccept" Icon="Accept" Text="确定">
                        <DirectEvents>
                            <Click OnEvent="ButtonSpecAccept_Click">
                                <EventMask ShowMask="true" Msg="修改中..." />
                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要修改生效时间吗" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonSpecCancel" Icon="Cancel" Text="取消">
                        <Listeners>
                            <Click Handler="#{WindowSpec}.close();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
