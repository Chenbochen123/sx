<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckStandInfo.aspx.cs" Inherits="Manager_RubberQuality_BasicInfo_CheckStandInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNothOperMater" Icon="StarGoldHalfGrey" Text="显示胶料树">
                                <Listeners>
                                    <Click Handler="if (#{TreePanelMater}.hidden == true) { #{TreePanelMater}.show(); this.setText('隐藏胶料树'); } else { #{TreePanelMater}.hide(); this.setText('显示胶料树'); }" />
                                </Listeners>
                            </ext:Button>
                            <%--   <ext:TextField ID="txtRecipeName" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方编号" ReadOnly="true" />--%>
                                            
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldNorthMaterName" FieldLabel="胶料名称"
                                LabelAlign="Right" EmptyText="请选择胶料" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查询" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="TriggerFieldNorthMaterName_TriggerClick" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenNorthMaterCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="分类标准" LabelAlign="Right"
                                EmptyText="全部">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandVisionStat" FieldLabel="版本状态"
                                LabelAlign="Right" EmptyText="全部">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cboType" runat="server" SelectOnTab="true" Editable="false"  LabelAlign="Right" FieldLabel="配方类型">
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
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
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel ID="GridPanelMaster" runat="server" Collapsible="false" Height="250"
                        Frame="false" AutoScroll="true" Region="North">
                        <Store>
                            <ext:Store ID="StoreMaster" runat="server" GroupField="StandTypeName">
                                <Model>
                                    <ext:Model ID="ModelMaster" runat="server" >
                                        <Fields>
                                            <ext:ModelField Name="StandId" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="StandTypeName" />
                                               <ext:ModelField Name="PmtTypeName" />
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
                                            <ext:ModelField Name="GroupName" />
                                               <ext:ModelField Name="ItemName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelMaster" runat="server">
                            <Columns>
                                <ext:Column ID="ColumnMasterSpace" runat="server" Width="15" Draggable="false" MenuDisabled="true"
                                    Resizable="false" Sortable="false" Selectable="false" />
                                <ext:Column ID="ColumnMasterMaterName" runat="server" Text="胶料名称" DataIndex="MaterName"
                                    Width="250" Draggable="false" MenuDisabled="true" />
                                   <ext:Column ID="Column1" runat="server" Text="配方类型" DataIndex="ItemName"
                                    Width="80" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterGroupName" runat="server" Text="等同胶料" DataIndex="GroupName"
                                    Width="250" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterDefineDate" runat="server" Text="定义日期" DataIndex="DefineDate"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterLLStandVision" runat="server" Text="玲珑版本" DataIndex="LLStandVision"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterStandVision" runat="server" Text="版本" DataIndex="StandVision"
                                    Width="45" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterStandVisionStatExp" runat="server" Text="版本状态" DataIndex="StandVisionStatExp"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterRegDateTime" runat="server" Text="生效时间" DataIndex="RegDateTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ColumnMasterLastModifyUserName" runat="server" Text="修改人" DataIndex="LastModifyUserName"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterLastModifyTime" runat="server" Text="修改时间" DataIndex="LastModifyTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ColumnMasterLastSubmitUserName" runat="server" Text="提交人" DataIndex="LastSubmitUserName"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterLastSubmitTime" runat="server" Text="提交时间" DataIndex="LastSubmitTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ColumnMasterLastAuditMemo" runat="server" Text="审核意见" DataIndex="LastAuditMemo"
                                    Width="160" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnMasterLastAuditUserName" runat="server" Text="审核人" DataIndex="LastAuditUserName"
                                    Width="60" Draggable="false" MenuDisabled="true" />
                                <ext:DateColumn ID="DateColumnMasterLastAuditTime" runat="server" Text="审核时间" DataIndex="LastAuditTime"
                                    Width="160" Draggable="false" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:CheckColumn runat="server" ID="CheckColumnMasterQuaCompute" Text="参与合格率计算" DataIndex="QuaCompute"
                                    Width="100" Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:CheckColumn runat="server" ID="CheckColumnChoiceness" Text="是否为精品物料" DataIndex="Choiceness"
                                    Width="100" Draggable="false" MenuDisabled="true" Hidden="true" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelMaster" Mode="Single">
                                <DirectEvents>
                                    <SelectionChange OnEvent="RowSelectionModelMaster_SelectionChange">
                                        <EventMask ShowMask="true" />
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
                            </ext:GridView>
                        </View>
                        <Features>
                            <ext:Grouping ID="GroupingMaster" runat="server" GroupHeaderTplString='{name} ({rows.length} 项)' />
                        </Features>
                        <BottomBar>
                            <ext:StatusBar ID="StatusBarMaster" runat="server" Height="10" />
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel ID="GridPanelDetail" runat="server" Collapsible="false" Frame="false"
                        ButtonAlign="Left" AutoScroll="true" Region="Center">
                        <Store>
                            <ext:Store ID="StoreDetail" runat="server" ClearRemovedOnLoad="true">
                                <Model>
                                    <ext:Model ID="ModelDetail" runat="server">
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
                                    <ext:DataSorter Property="WeightId" Direction="ASC" />
                                </Sorters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:Column ID="ColumnDetailSpace" runat="server" Width="15" Draggable="false" MenuDisabled="true"
                                    Resizable="false" Selectable="false" />
                                <ext:Column ID="ColumnDetailItemName" runat="server" Text="检验项目" DataIndex="ItemName"
                                    Width="150" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnDetailPermMin" runat="server" Text="下限值" DataIndex="PermMin"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:CheckColumn ID="CheckColumnIfMin" runat="server" Text="容许下限" DataIndex="IfMin"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnDetailPermMax" runat="server" Text="上限值" DataIndex="PermMax"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:CheckColumn ID="CheckColumnDetailIfMax" runat="server" Text="容许上限" DataIndex="IfMax"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:CheckColumn ID="CheckColumnDetailJudgeResult" runat="server" Text="是否合格" DataIndex="JudgeResult"
                                    Width="95" Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:Column ID="ColumnDetailDealNotion" runat="server" Text="检测意见" DataIndex="DealNotion"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                                <ext:Column ID="ColumnDetailDrawMark" runat="server" Text="划胶标识" DataIndex="DrawMark"
                                    Width="95" Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:Column ID="ColumnDetailGrade" runat="server" Text="等级" DataIndex="Grade" Width="95"
                                    Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:Column ID="ColumnDetailCardMark2" runat="server" Text="子标识" DataIndex="CardMark2"
                                    Width="95" Draggable="false" MenuDisabled="true" Hidden="true" />
                                <ext:Column ID="ColumnDetailQuaFrequency" runat="server" Text="检验频率" Hidden="true" DataIndex="QuaFrequency"
                                    Width="95" Draggable="false" MenuDisabled="true" />
                            </Columns>
                        </ColumnModel>
                        <BottomBar>
                            <ext:StatusBar ID="StatusBarDetail" runat="server" Height="10" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
