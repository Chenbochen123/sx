﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_Main, App_Web_a5rwyiqa" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺配方明细</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Main.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlMain" runat="server" Header="true" Region="Center" AutoHeight="true" Title="配方基本信息" AutoScroll="true">
                    <Items>
                        <ext:FormPanel ID="Panel1" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" AutoScroll="true">
                            <Items>
                                <ext:Container ID="container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtRecipeName" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方编号" ReadOnly="true" />
                                                <ext:ComboBox ID="txtRecipeMaterialCode" runat="server" LabelAlign="Right" Flex="1" FieldLabel="物料名称" SelectOnTab="true" Editable="false" />
                                                <ext:TriggerField ID="txtRecipeEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEquipmentInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox ID="txtRecipeType" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方类型" SelectOnTab="true" Editable="false" />
                                                <ext:ComboBox ID="txtRecipeState" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方状态" SelectOnTab="true" Editable="false" />
                                                              <ext:ComboBox ID="txJieDuan" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方阶段" SelectOnTab="true" Editable="false" />
                                           
                                                <ext:NumberField ID="txtLotTotalWeight" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="配方总重" IndicatorText="千克" ReadOnly="true" />
                                                <ext:NumberField ID="txtShelfLotCount" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="每架车数" IndicatorText="车" />
                                                <ext:NumberField ID="txtLotDoneTime" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="每车标准时间" IndicatorText="秒" />
                                                <ext:NumberField ID="txtOverTimeSetTime" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="超时排胶时间" IndicatorText="秒" />
                                                <ext:NumberField ID="txtOverTempSetTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="紧急排胶温度" IndicatorText="℃" />
                                                <ext:NumberField ID="txtOverTempMinTime" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="超温排胶最短时间" IndicatorText="秒" />
                                                <ext:NumberField ID="txtInPolyMaxTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="最高进胶温度" IndicatorText="℃" />
                                                <ext:NumberField ID="txtInPolyMinTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="最低进胶温度" IndicatorText="℃" />
                                                <ext:NumberField ID="txtMakeUpTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="补偿温度" IndicatorText="℃" />
                                                <ext:ComboBox ID="txtCarbonRecycleType" runat="server" LabelAlign="Right" Flex="1" FieldLabel="炭黑是否回收" SelectOnTab="true" Editable="false" />
                                                <ext:NumberField ID="txtCarbonRecycleTime" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="炭黑回收时间" IndicatorText="秒" />
                                              <ext:TextField ID="txtRecipeVersionID" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" FieldLabel="版本号" ReadOnly="true" />
                                                <ext:Checkbox ID="txtIsUseAreaTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" FieldLabel="使用三区温度" SelectOnTab="true" Editable="false" />
                                                <ext:NumberField ID="txtSideTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="侧壁温度" IndicatorText="℃" />
                                                <ext:TextField ID="txtSapVersionId" runat="server" LabelAlign="Right" Flex="1"  FieldLabel="SAP版本号"  SelectOnTab="true" Editable="false" />
                                                <ext:NumberField ID="txtRollTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="转子温度" IndicatorText="℃" />
                                                <ext:NumberField ID="txtDdoorTemp" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="卸料门温度" IndicatorText="℃" />
                                                         <ext:ComboBox ID="txtFactory" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方供应商" SelectOnTab="true" Editable="false" />
                                                <ext:TextField ID="txtB_Version" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" FieldLabel="胶料PLM版本" Editable="false" Readonly="true" />
                                                <ext:TextField ID="txtR_Version" runat="server" LabelAlign="Right" Flex="1" MaxLength="4"  FieldLabel="工艺PLM版本"  Editable="false" Readonly="true" />
                                                        <ext:ComboBox ID="ComboBoxCC" runat="server" LabelAlign="Right" Flex="1" FieldLabel="关闭除尘" SelectOnTab="true" Editable="false" />
                                                      <ext:Label ID="Label2" runat="server" LabelAlign="Right" Flex="1" Html="<br><br>" />
                                                  
                                                  
                                                    <ext:TextField ID="txtPackWeight" runat="server" LabelAlign="Right" Flex="1"  FieldLabel="小料袋重(g)"  SelectOnTab="true" Editable="false" />
                                                  <ext:ComboBox ID="txtRecipeEquipCode" runat="server" LabelAlign="Right" Flex="1" FieldLabel="机台名称" SelectOnTab="true" Editable="false" Hidden="true" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        <ext:Panel ID="Panel2" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" Title="配方审核信息">
                            <Items>
                                <ext:Container ID="container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5" ColumnsNumber="5" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtAuditFlag" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核状态" />
                                                <ext:TextField ID="txtRecipeModifyUser" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改人" />
                                                <ext:DateField ID="txtRecipeModifyTime" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField ID="txtAuditUser" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核人" />
                                                <ext:DateField ID="txtAuditDateTime" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核时间" Format="yyyy-MM-dd HH:mm:ss" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container15" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroupAuditUser" runat="server" FieldLabel="审核人设置" LabelAlign="Right" ColumnsNumber="5" Flex="1" AnchorHorizontal="true">
                                        </ext:CheckboxGroup>
                                        <ext:Hidden ID="isShowCurrentUser" runat="server" Text="0"></ext:Hidden>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
