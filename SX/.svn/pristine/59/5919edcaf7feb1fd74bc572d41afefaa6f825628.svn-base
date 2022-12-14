<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mixing.aspx.cs" Inherits="Manager_Technology_Comparison_Mixing" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询-混炼</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("isDifference") == "1") {
                return "x-grid-row-deleted";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:GridPanel ID="gridPanel1" runat="server" Frame="true" Region="West" Flex="1" Title="混炼信息（1）">
                    <Store>
                        <ext:Store ID="gridPanel1Store" runat="server">
                            <Model>
                                <ext:Model ID="model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="LogObjID" />
                                        <ext:ModelField Name="RecipeObjID" />
                                        <ext:ModelField Name="RecipeEquipCode" />
                                        <ext:ModelField Name="RecipeMaterialCode" />
                                        <ext:ModelField Name="RecipeVersionID" />
                                        <ext:ModelField Name="MixingStep" />
                                        <ext:ModelField Name="TermCode" />
                                        <ext:ModelField Name="TermName" />
                                        <ext:ModelField Name="MixingTime" />
                                        <ext:ModelField Name="MixingTemp" />
                                        <ext:ModelField Name="MixingEnergy" />
                                        <ext:ModelField Name="MixingPower" />
                                        <ext:ModelField Name="MixingPress" />
                                        <ext:ModelField Name="MixingSpeed" />
                                        <ext:ModelField Name="ActionCode" />
                                        <ext:ModelField Name="ActionName" />
                                        <ext:ModelField Name="isDifference" />
                                        <ext:ModelField Name="LogRecordTime" />
                                        <ext:ModelField Name="time_diff" />
                                                <ext:ModelField Name="temp_diff" />
                                                <ext:ModelField Name="ener_diff" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel1" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNum1" runat="server" Text="步骤" Width="50" Align="Center" />
                            <ext:Column ID="TermCode1" DataIndex="TermName" runat="server" Text="条件设定" Flex="1" Sortable="false">
                            </ext:Column>
                            <ext:Column ID="MixingTime1" DataIndex="MixingTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                               <ext:Column ID="Column1" DataIndex="time_diff" runat="server" DecimalPrecision="0" Text="时间公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingTemp1" DataIndex="MixingTemp" runat="server" DecimalPrecision="0" Text="温度" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                               <ext:Column ID="Column2" DataIndex="temp_diff" runat="server" DecimalPrecision="0" Text="温度公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingEnergy1" DataIndex="MixingEnergy" runat="server" DecimalPrecision="0" Text="能量" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                                <ext:Column ID="Column3" DataIndex="ener_diff" runat="server" DecimalPrecision="0" Text="能量公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingPower1" DataIndex="MixingPower" runat="server" DecimalPrecision="0" Text="功率" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="ActionCode1" DataIndex="ActionName" runat="server" Text="动作" Width="160" Sortable="false">
                            </ext:Column>
                            <ext:Column ID="MixingSpeed1" DataIndex="MixingSpeed" runat="server" Text="转速" DecimalPrecision="0" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingPress1" DataIndex="MixingPress" runat="server" Text="压力" DecimalPrecision="0" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                </ext:GridPanel>
                <ext:GridPanel ID="gridPanel2" runat="server" Frame="true" Region="Center" Flex="1" Title="混炼信息（2）">
                    <Store>
                        <ext:Store ID="gridPanel2Store" runat="server">
                            <Model>
                                <ext:Model ID="model2" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="LogObjID" />
                                        <ext:ModelField Name="RecipeObjID" />
                                        <ext:ModelField Name="RecipeEquipCode" />
                                        <ext:ModelField Name="RecipeMaterialCode" />
                                        <ext:ModelField Name="RecipeVersionID" />
                                        <ext:ModelField Name="MixingStep" />
                                        <ext:ModelField Name="TermCode" />
                                        <ext:ModelField Name="TermName" />
                                        <ext:ModelField Name="MixingTime" />
                                        <ext:ModelField Name="MixingTemp" />
                                        <ext:ModelField Name="MixingEnergy" />
                                        <ext:ModelField Name="MixingPower" />
                                        <ext:ModelField Name="MixingPress" />
                                        <ext:ModelField Name="MixingSpeed" />
                                        <ext:ModelField Name="ActionCode" />
                                        <ext:ModelField Name="ActionName" />
                                        <ext:ModelField Name="isDifference" />
                                        <ext:ModelField Name="LogRecordTime" />
                                      <ext:ModelField Name="time_diff" />
                                                <ext:ModelField Name="temp_diff" />
                                                <ext:ModelField Name="ener_diff" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel2" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNum2" runat="server" Text="步骤" Width="50" Align="Center" />
                            <ext:Column ID="TermCode2" DataIndex="TermName" runat="server" Text="条件设定" Flex="1" Sortable="false">
                            </ext:Column>
                            <ext:Column ID="MixingTime2" DataIndex="MixingTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                               <ext:Column ID="Column4" DataIndex="time_diff" runat="server" DecimalPrecision="0" Text="时间公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingTemp2" DataIndex="MixingTemp" runat="server" DecimalPrecision="0" Text="温度" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                                    <ext:Column ID="Column5" DataIndex="temp_diff" runat="server" DecimalPrecision="0" Text="温度公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingEnergy2" DataIndex="MixingEnergy" runat="server" DecimalPrecision="0" Text="能量" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                                 <ext:Column ID="Column6" DataIndex="ener_diff" runat="server" DecimalPrecision="0" Text="能量公差" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingPower2" DataIndex="MixingPower" runat="server" DecimalPrecision="0" Text="功率" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="ActionCode2" DataIndex="ActionName" runat="server" Text="动作" Width="160" Sortable="false">
                            </ext:Column>
                            <ext:Column ID="MixingSpeed2" DataIndex="MixingSpeed" runat="server" Text="转速" DecimalPrecision="0" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                            <ext:Column ID="MixingPress2" DataIndex="MixingPress" runat="server" Text="压力" DecimalPrecision="0" Align="Center" Width="50" Sortable="false">
                                  <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ID="gvRows2" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>

</body>
</html>
