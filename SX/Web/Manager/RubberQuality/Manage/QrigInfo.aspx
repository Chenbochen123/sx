﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QrigInfo.aspx.cs" Inherits="Manager_RubberQuality_Manage_QrigInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检验数据查询</title>
    <style type="text/css">
        .GridViewRowClass_NoStand
        {
            color: Fuchsia;
        }
        .GridViewRowClass_MoreThanMax
        {
            color: Red;
        }
        .GridViewRowClass_LessThanMin
        {
            color: Blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Hidden runat="server" ID="HiddenEquipType" />
    <ext:Hidden runat="server" ID="HiddenUserType" />
    <ext:Hidden runat="server" ID="HiddenMaterType" />
    <ext:Hidden runat="server" ID="HiddenDetailEditSeqNo" />
    <ext:Hidden runat="server" ID="HiddenDetailEditItemCd" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Window runat="server" ID="WindowQuery" Closable="false" Height="350" Width="450"
                Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelQuery">
                        <Items>
                            <ext:Panel runat="server" Layout="HBoxLayout" Border="false">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:RadioGroup runat="server" ID="RadioGroupQueryType" Vertical="false" Width="300">
                                        <Items>
                                            <ext:Radio runat="server" BoxLabel="按生产日期查询" InputValue="1" Checked="true" />
                                            <ext:Radio runat="server" BoxLabel="按检验日期查询" InputValue="2" />
                                        </Items>
                                        <DirectEvents>
                                            <Change OnEvent="RadioGroupQueryType_Change">
                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelQuery" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="QueryType" Value="newValue" Mode="Raw" />
                                                </ExtraParams>
                                            </Change>
                                        </DirectEvents>
                                    </ext:RadioGroup>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Layout="HBoxLayout" Border="false">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:FieldSet runat="server" Width="300" PaddingSpec="5 20 0 20">
                                        <Items>
                                            <ext:DateField runat="server" ID="DateFieldQuerySDate" FieldLabel="开始生产日期" AllowBlank="false"
                                                Format="yyyy-MM-dd" Editable="false" />
                                            <ext:DateField runat="server" ID="DateFieldQueryEDate" FieldLabel="截止生产日期" AllowBlank="false"
                                                Format="yyyy-MM-dd" Editable="false" />
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Layout="FormLayout">
                                <Items>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                           <ext:TriggerField runat="server" ID="TriggerFieldQueryEquipName" FieldLabel="生产机台"
                                                ColumnWidth=".5" LabelAlign="Right" Editable="false" LabelWidth="60" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <DirectEvents>
                                                    <TriggerClick OnEvent="TriggerFieldQueryEquipName_TriggerClick">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelQuery" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                                        </ExtraParams>
                                                    </TriggerClick>
                                                </DirectEvents>
                                            </ext:TriggerField>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryShiftId" FieldLabel="生产班次" ColumnWidth=".5" 
                                                LabelAlign="Right" LabelWidth="60" Editable="false" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryShiftClass" FieldLabel="生产班组" ColumnWidth=".5"
                                                LabelAlign="Right" LabelWidth="60" Editable="false" EmptyText="全部" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                         
                                            <ext:Hidden runat="server" ID="HiddenQueryEquipCode" />
                                            <ext:TextField runat="server" ID="TextFieldQueryZJSID" FieldLabel="主机手" ColumnWidth=".5"
                                                LabelAlign="Right" LabelWidth="60" MaxLength="20" EmptyText="填写编号" Hidden="true" />
                                            <ext:TriggerField runat="server" ID="TriggerFieldQueryCheckUserName" FieldLabel="检验人员"
                                                ColumnWidth=".5" LabelAlign="Right" Editable="false" LabelWidth="60" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                                </Triggers>
                                                <DirectEvents>
                                                    <TriggerClick OnEvent="TriggerFieldQueryCheckUserName_TriggerClick">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelQuery" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                                        </ExtraParams>
                                                    </TriggerClick>
                                                </DirectEvents>
                                            </ext:TriggerField>
                                            <ext:Hidden runat="server" ID="HiddenQueryCheckUserCode" />
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryCheckPlanClass" FieldLabel="检验班组" ColumnWidth=".5"
                                                LabelAlign="Right" LabelWidth="60" Editable="false" EmptyText="全部" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:TriggerField runat="server" ID="TriggerFieldQueryCheckEquipName" FieldLabel="检验机台"
                                                ColumnWidth=".5" LabelAlign="Right" Editable="false" LabelWidth="60" EmptyText="全部" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <DirectEvents>
                                                    <TriggerClick OnEvent="TriggerFieldQueryCheckEquipName_TriggerClick">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelQuery" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                                        </ExtraParams>
                                                    </TriggerClick>
                                                </DirectEvents>
                                            </ext:TriggerField>
                                            <ext:Hidden runat="server" ID="HiddenQueryCheckEquipCode" />
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                            <ext:TriggerField runat="server" ID="TriggerFieldQueryMaterName" FieldLabel="胶料名称"
                                                LabelAlign="Right" LabelWidth="60" Editable="false" Width="400" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <DirectEvents>
                                                    <TriggerClick OnEvent="TriggerFieldQueryMaterName_TriggerClick">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelQuery" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                                        </ExtraParams>
                                                    </TriggerClick>
                                                </DirectEvents>
                                            </ext:TriggerField>
                                            <ext:Hidden runat="server" ID="HiddenQueryMaterCode" />
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="Panel1" runat="server" Layout="ColumnLayout" Border="false" >
                                        <Items>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryCheckItemTypeId" FieldLabel="检验项目分类"  Hidden="true"
                                                LabelAlign="Right" LabelWidth="80" Editable="false" ColumnWidth="0.5" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryStandCode" LabelAlign="Right" FieldLabel="检验标准分类"
                                                LabelWidth="80" ColumnWidth=".5" Editable="false" EmptyText="全部" Hidden="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="cbxtype" LabelAlign="Right" FieldLabel="检验类型"
                                                LabelWidth="80" ColumnWidth=".5" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false" Hidden="true">
                                        <Items>
                                            <ext:ComboBox runat="server" ID="ComboBoxQueryItemName" LabelAlign="Right" FieldLabel="检验项目类型"
                                                LabelWidth="80" ColumnWidth=".5" EmptyText="全部">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('')" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                                        <Items>
                                            <ext:ComboBox ID="cboType" runat="server" SelectOnTab="true" Editable="false"  LabelAlign="Right" LabelWidth="60" FieldLabel="配方类型" Hidden="true">
                                            </ext:ComboBox>
                                            <ext:Hidden runat="server" ID="Hidden1" />
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonQueryAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonQueryAccept" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="ButtonQueryAccept_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowQuery" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonQueryCancel" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="ButtonQueryCancel_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowQuery" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
            <%--<ext:Window runat="server" ID="WindowEdit" Closable="false" Height="400" Width="450"
                AutoScroll="true" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelEdit">
                        <Items>
                            <ext:Hidden runat="server" ID="HiddenEditSeqNo" />
                            <ext:DateField runat="server" ID="DateFieldEditPlanDate" FieldLabel="生产日期" AllowBlank="false"
                                EmptyText="请选择..." Format="yyyy-MM-dd" Editable="false" />
                            <ext:TriggerField runat="server" ID="TriggerFieldEditEquipName" FieldLabel="生产机台"
                                Editable="false" AllowBlank="false" EmptyText="请选择...">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" HideTrigger="true" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldEditEquipName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelEdit" />
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenEditEquipCode" />
                            <ext:TriggerField runat="server" ID="TriggerFieldEditMaterName" FieldLabel="胶料名称"
                                AllowBlank="false" Editable="false" EmptyText="请选择...">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" HideTrigger="true" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldEditMaterName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelEdit" />
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenEditMaterCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxEditShiftId" FieldLabel="生产班次" Editable="false"
                                AllowBlank="false" EmptyText="请选择..." />
                            <ext:ComboBox runat="server" ID="ComboBoxEditShiftClass" FieldLabel="生产班组" Editable="false"
                                AllowBlank="false" EmptyText="请选择..." />
                            <ext:ComboBox runat="server" ID="ComboBoxEditZJSID" FieldLabel="主机手" Editable="false"
                                AllowBlank="false" EmptyText="请选择..." />
                            <ext:NumberField runat="server" ID="NumberFieldEditSerialId" FieldLabel="车次" AllowBlank="false"
                                EmptyText="请填写..." MinValue="0" MaxValue="99999" AllowDecimals="false" DecimalPrecision="0" />
                            <ext:NumberField runat="server" ID="NumberFieldEditLLSerialID" FieldLabel="玲珑车次"
                                AllowBlank="false" EmptyText="请填写..." MinValue="0" MaxValue="99999" AllowDecimals="false"
                                DecimalPrecision="0" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCheckNum" FieldLabel="检验次数" AllowBlank="false"
                                EmptyText="请填写..." MinValue="1" MaxValue="999" AllowDecimals="false" DecimalPrecision="0" />
                            <ext:ComboBox runat="server" ID="ComboBoxEditCheckPlanClass" FieldLabel="检验班组" Editable="false"
                                AllowBlank="false" EmptyText="请选择..." />
                            <ext:ComboBox runat="server" ID="ComboBoxEditShiftCheckId" FieldLabel="检验班次" Editable="false"
                                AllowBlank="false" EmptyText="请选择..." />
                            <ext:DateField runat="server" ID="DateFieldEditCheckPlanDate" FieldLabel="检验日期" AllowBlank="false"
                                EmptyText="请选择..." Editable="false" Format="yyyy-MM-dd" />
                            <ext:DateField runat="server" ID="DateFieldEditCheckDate" FieldLabel="检验时间" AllowBlank="false"
                                EmptyText="请选择..." Editable="false" Format="yyyy-MM-dd" />
                            <ext:TimeField runat="server" ID="TimeFieldEditCheckTime" FieldLabel=" " AllowBlank="false"
                                EmptyText="请填写..." Format="HH:mm:ss" />
                            <ext:TriggerField runat="server" ID="TriggerFieldEditCheckEquipName" FieldLabel="检验机台"
                                Editable="false" AllowBlank="false" EmptyText="请选择...">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" HideTrigger="true" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldEditCheckEquipName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelEdit" />
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenEditCheckEquipCode" />
                            <ext:TriggerField runat="server" ID="TriggerFieldEditCheckUserName" FieldLabel="检验人员"
                                Hidden="true">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerFieldEditCheckUserName_TriggerClick">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="FormPanelEdit" />
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenEditCheckUserCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxEditStandCode" FieldLabel="检验标准分类" Editable="false"
                                Hidden="true" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonEditAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonEditAccept" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="ButtonEditAccept_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowEdit" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonEditCancel" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="ButtonEditCancel_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowEdit" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>--%>
            <%--<ext:Window runat="server" ID="WindowDetailEdit" Closable="false" Height="300" Width="450"
                AutoScroll="true" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelDetailEdit">
                        <Items>
                            <ext:TextField runat="server" ID="TextFieldDetailEditItemName1" FieldLabel="检验项"
                                ReadOnly="true" Hidden="true" />
                            <ext:DisplayField runat="server" ID="DisplayFieldDetailEditItemName" FieldLabel="检验项" />
                            <ext:NumberField runat="server" ID="NumberFieldDetailEditItemCheck" FieldLabel="检验值"
                                AllowBlank="false" EmptyText="请填写..." DecimalPrecision="3" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonDetailEditAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonDetailEditAccept" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="ButtonDetailEditAccept_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowDetailEdit" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonDetailEditCancel" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="ButtonDetailEditCancel_Click">
                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="WindowDetailEdit" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>--%>
            <ext:Panel ID="PanelCenter" runat="server" Region="Center" AutoScroll="true" Layout="BorderLayout">
                <TopBar>
                    <ext:Toolbar runat="Server">
                        <Items>
                            <ext:Button runat="server" ID="ButtonQuery" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="ButtonQuery_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <%--<ext:Button runat="server" ID="ButtonEdit" Text="修改" Icon="PageEdit" Hidden="true">
                                <DirectEvents>
                                    <Click OnEvent="ButtonEdit_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>--%>
                            <ext:Button runat="server" ID="ButtonDelete" Text="删除" Icon="PageDelete" >
                                <DirectEvents>
                                    <Click OnEvent="ButtonDelete_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                        <Confirmation ConfirmRequest="true" Title="提示" Message="确定要删除选择的记录吗" />
                                    </Click>
                                </DirectEvents>
                                <Listeners>
                                    <Click Fn="ButtonDelete_BeforeClick" />
                                </Listeners>
                            </ext:Button>
                            <ext:TextField runat="server" ID="ts"  Hidden="true" ></ext:TextField>
                            <ext:Button runat="server" ID="ButtonExcel" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="ButtonExcel_Click" Before="ButtonExcel_BeforeClick">
                                        <ExtraParams>
                                            <ext:Parameter Name="count" Value="#{StoreMaster}.getTotalCount()" Mode="Raw" />
                                            <%--<ext:Parameter Name="fields" Value="#{StoreMaster}.model.getFields()" Mode="Raw" />--%>
                                            <%--<ext:Parameter Name="records" Value="#{StoreMaster}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />--%>
                                            <%--<ext:Parameter Name="columns" Value="#{GridPanelMaster}.columns" Mode="Raw" />--%>
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelMaster" Collapsible="false" Height="400" Region="North"
                        Frame="false" AutoScroll="true">
                        <BottomBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarMaster" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                        <Store>
                            <ext:Store runat="server" ID="StoreMaster" PageSize="50" AutoLoad="false" RemoteSort="true"
                                OnReadData="StoreMaster_ReadData">
                                <Proxy>
                                    <ext:PageProxy />
                                </Proxy>
                                <Model>
                                    <ext:Model runat="server" ID="ModelMaster" IDProperty="SeqNo">
                                        <Fields>
                                            <ext:ModelField Name="PlanDate" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="SerialId" />
                                            <ext:ModelField Name="TestType" />
                                            <ext:ModelField Name="checknum" />
                                            <ext:ModelField Name="Flag" />
                                            <ext:ModelField Name="IfLast" />
                                            <ext:ModelField Name="SeqNo" />
                                            <ext:ModelField Name="ShiftClass" />
                                            <ext:ModelField Name="CheckDate" />
                                            <ext:ModelField Name="FullCheckTime" />
                                            <ext:ModelField Name="CheckEquipName" />
                                            <ext:ModelField Name="CheckClassName" />
                                            <ext:ModelField Name="WorkerBarcode" />
                                               <ext:ModelField Name="itemname" />
                                            <ext:ModelField Name="StandCode" />
                                            <ext:ModelField Name="StandTypeName" />
                                            <ext:ModelField Name="CheckPlan_Date" />
                                            <ext:ModelField Name="CheckShiftName" />
                                            <ext:ModelField Name="ShiftId" />
                                            <ext:ModelField Name="CHECKFALG" />
                                       
                                            <ext:ModelField Name="NotQuaCompute" Type="Boolean" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:CheckColumn runat="server" DataIndex="NotQuaCompute" Text="不参与合格率计算" Width="120"  Hidden="true"/>
                                <ext:Column runat="server" DataIndex="PlanDate" Text="生产日期" Width="80" />
                                <ext:Column runat="server" DataIndex="EquipName" Text="生产机台" />
                    <%--            <ext:Column runat="server" DataIndex="ShiftName" Text="生产班次" Width="60" />--%>
                                <ext:Column runat="server" DataIndex="ShiftId" Text="生产班次" Width="60" />
                                <ext:Column runat="server" DataIndex="ShiftClass" Text="生产班组" Width="60" />
                          
                                <ext:Column runat="server" DataIndex="MaterName" Text="胶料名称" Width="160" />

                                <ext:Column runat="server" DataIndex="SerialId" Text="车次" Width="40" />
                               
                                <ext:Column runat="server" DataIndex="TestType" Text="检验类型" Width="70" />
                                <ext:Column runat="server" DataIndex="checknum" Text="检验次数" Width="60" />
                                <ext:CheckColumn runat="server" DataIndex="Flag" Text="Flag" Hidden="true" Width="40" />
                                <ext:CheckColumn runat="server" DataIndex="IfLast" Text="最后一次检验" Width="80" Hidden="true" />
                                <ext:Column runat="server" DataIndex="SeqNo" Text="SeqNo" Hidden="true" Width="100" />
                                <ext:Column runat="server" DataIndex="ShiftClass" Text="ShiftClass" Hidden="true"
                                    Width="80" />
                                <ext:Column runat="server" DataIndex="CheckPlan_Date" Text="检验日期" Width="80" />
                                <ext:Column runat="server" DataIndex="FullCheckTime" Text="检验时间" Width="150" />
                                <ext:Column runat="server" DataIndex="CheckClassName" Text="检验班组" Width="60" />
                                <ext:Column runat="server" DataIndex="WorkerBarcode" Text="检验人" Width="60" />
                                <ext:Column runat="server" DataIndex="CHECKFALG" Text="合格标志" Width="60" />
                      
                                <ext:Column runat="server" DataIndex="StandCode" Text="检验标准分类" Width="100" />
                                <ext:Column runat="server" DataIndex="CheckEquipName" Text="检验机台" Width="200" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelMaster" Mode="Single">
                                <DirectEvents>
                                    <Select OnEvent="RowSelectionModelMaster_Select">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelDetail" />
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel> 
                    <ext:GridPanel runat="server" ID="GridPanelDetail" Collapsible="false" Height="240"  Region="Center"
                        Frame="false" AutoScroll="true">
                        <TopBar>
                            <ext:Toolbar runat="server">
                                <Items>
<%--                                    <ext:Button runat="server" ID="ButtonDetailEdit" Text="修改明细" Icon="PageEdit" Hidden="true">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonDetailEdit_Click">
                                                <EventMask ShowMask="true" Target="Page" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="ButtonDetailDelete" Text="删除" Icon="PageDelete" Hidden="true">
                                        <DirectEvents>
                                            <Click OnEvent="ButtonDetailDelete_Click">
                                                <EventMask ShowMask="true" Target="Page" />
                                                <Confirmation ConfirmRequest="true" Title="提示" Message="确定要删除选择的明细记录吗" />
                                            </Click>
                                        </DirectEvents>
                                        <Listeners>
                                            <Click Fn="ButtonDetailDelete_BeforeClick" />
                                        </Listeners>
                                    </ext:Button>--%>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store runat="server" ID="StoreDetail">
                                <Model>
                                    <ext:Model runat="server">
                                        <Fields>
                                            <ext:ModelField Name="SeqNo" />
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="ItemCheck" />
                                            <ext:ModelField Name="UnitName" />
                                            <ext:ModelField Name="PermMin" />
                                            <ext:ModelField Name="PermMax" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="JudgeValue" />
                                            <ext:ModelField Name="StandCode" />
                                            <ext:ModelField Name="StandId" />
                                            <ext:ModelField Name="ItemCd" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server" ID="ColumnModelDetail">
                            <Items>
                                <ext:Column runat="server" DataIndex="SeqNo" Text="SeqNo" Hidden="true" Width="100" />
                                <ext:Column runat="server" DataIndex="ItemName" Text="检验项" Width="150" />
                                <ext:Column runat="server" DataIndex="ItemCheck" Text="检验值" Width="100" />
                                <ext:Column runat="server" DataIndex="UnitName" Text="单位" Width="60" Hidden="true" />
                                <ext:Column runat="server" DataIndex="PermMin" Text="最小值" Width="80" />
                                <ext:Column runat="server" DataIndex="PermMax" Text="最大值" Width="80" />
                                <ext:Column runat="server" DataIndex="EquipName" Text="检验机台" Hidden="true" Width="100" />
                                <ext:CheckColumn runat="server" DataIndex="JudgeValue" Text="合格" Width="50" Hidden="true" />
                                <ext:Column runat="server" DataIndex="StandCode" Text="标准分类" Width="60"  Hidden="true"/>
                                <ext:Column runat="server" DataIndex="StandId" Text="标准编号" Width="60" Hidden="true"/>
                            </Items>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelDetail" Mode="Single">
                                <DirectEvents>
                                    <Select OnEvent="RowSelectionModelDetail_Select">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="GridPanelDetail" />
                                        <ExtraParams>
                                            <ext:Parameter Name="SeqNo" Mode="Raw" Value="this.getSelection()[0].data.SeqNo" />
                                            <ext:Parameter Name="ItemCd" Mode="Raw" Value="this.getSelection()[0].data.ItemCd" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewDetail">
                                <GetRowClass Fn="GridViewDetail_GetRowClass" />
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:StatusBar runat="server" DefaultText="信息提示：紫红色字体代表未设定检验标准，红色字体代表超上标，蓝色字体代表超下标">
                            </ext:StatusBar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
