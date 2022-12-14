<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Manager_Technology_Comparison_Main" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询</title>
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

</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlMain1" runat="server" Header="true" Region="West" Flex="1" AutoHeight="true" Title="配方基本信息" AutoScroll="true">
                    <Items>
                        <ext:FormPanel ID="Panel1" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" AutoScroll="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtRecipeName1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方编号" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeMaterialCode1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="物料名称" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeEquipCode1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="机台名称" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeType1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方类型" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeState1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方状态" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeVersionID1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="版本号" ReadOnly="true" />
                                                <ext:TextField ID="txtLotTotalWeight1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方总重" IndicatorText="千克" ReadOnly="true" />
                                                <ext:TextField ID="txtShelfLotCount1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="每架车数" IndicatorText="车" ReadOnly="true" />
                                                <ext:TextField ID="txtLotDoneTime1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="每车标准时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTimeSetTime1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="超时排胶时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTempSetTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="紧急排胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTempMinTime1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="超温排胶最短时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtInPolyMaxTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="最高进胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtInPolyMinTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="最低进胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtMakeUpTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="补偿温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtCarbonRecycleType1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="炭黑是否回收" ReadOnly="true" />
                                                <ext:TextField ID="txtCarbonRecycleTime1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="炭黑回收时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtIsUseAreaTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="使用三区温度" ReadOnly="true" />
                                                <ext:TextField ID="txtSideTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="侧壁温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtRollTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="转子温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtDdoorTemp1" runat="server" LabelAlign="Right" Flex="1" FieldLabel="卸料门温度" IndicatorText="℃" ReadOnly="true" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        <ext:Panel ID="Panel2" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" Title="配方审核信息">
                            <Items>
                                <ext:Container ID="container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtRecipeModifyUser1" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改人" />
                                                <ext:TextField ID="txtRecipeModifyTime1" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField ID="txtAuditUser1" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核人" />
                                                <ext:TextField ID="txtAuditDateTime1" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField ID="txtAuditFlag1" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核状态" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlMain2" runat="server" Header="true" Region="Center" Flex="1" AutoHeight="true" Title="配方基本信息" AutoScroll="true">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" AutoScroll="true">
                            <Items>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup3" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtRecipeName2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方编号" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeMaterialCode2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="物料名称" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeEquipCode2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="机台名称" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeType2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方类型" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeState2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方状态" ReadOnly="true" />
                                                <ext:TextField ID="txtRecipeVersionID2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="版本号" ReadOnly="true" />
                                                <ext:TextField ID="txtLotTotalWeight2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方总重" IndicatorText="千克" ReadOnly="true" />
                                                <ext:TextField ID="txtShelfLotCount2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="每架车数" IndicatorText="车" ReadOnly="true" />
                                                <ext:TextField ID="txtLotDoneTime2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="每车标准时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTimeSetTime2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="超时排胶时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTempSetTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="紧急排胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtOverTempMinTime2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="超温排胶最短时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtInPolyMaxTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="最高进胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtInPolyMinTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="最低进胶温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtMakeUpTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="补偿温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtCarbonRecycleType2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="炭黑是否回收" ReadOnly="true" />
                                                <ext:TextField ID="txtCarbonRecycleTime2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="炭黑回收时间" IndicatorText="秒" ReadOnly="true" />
                                                <ext:TextField ID="txtIsUseAreaTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="使用三区温度" ReadOnly="true" />
                                                <ext:TextField ID="txtSideTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="侧壁温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtRollTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="转子温度" IndicatorText="℃" ReadOnly="true" />
                                                <ext:TextField ID="txtDdoorTemp2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="卸料门温度" IndicatorText="℃" ReadOnly="true" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        <ext:Panel ID="Panel3" runat="server" Header="true" Layout="AnchorLayout" AutoHeight="true" Title="配方审核信息">
                            <Items>
                                <ext:Container ID="container4" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup4" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="txtRecipeModifyUser2" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改人" />
                                                <ext:TextField ID="txtRecipeModifyTime2" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="修改时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField ID="txtAuditUser2" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核人" />
                                                <ext:TextField ID="txtAuditDateTime2" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField ID="txtAuditFlag2" runat="server" ReadOnly="true" LabelAlign="Right" Flex="1" FieldLabel="审核状态" />
                                            </Items>
                                        </ext:CheckboxGroup>
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
