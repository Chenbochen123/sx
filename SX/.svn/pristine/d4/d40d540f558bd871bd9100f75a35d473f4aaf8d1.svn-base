<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Weight.aspx.cs" Inherits="Manager_Technology_Comparison_Weight" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询-称量</title>
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
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" TitleAlign="Center" >
            <Items>
                <ext:Panel ID="pnlWeight" runat="server" Region="Center" Header="false" Layout="AccordionLayout">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:Panel ID="PanelWeight0" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center" Title="炭黑称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight01" runat="server" Frame="true" Region="West" Flex="1" Title="炭黑称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel3" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column2" DataIndex="MaterialShowName" runat="server" Text="炭黑名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column3" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column4" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column5" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight02" runat="server" Frame="true" Region="Center" Flex="1" Title="炭黑称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store2" runat="server">
                                            <Model>
                                                <ext:Model ID="Model4" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel4" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column1" DataIndex="MaterialShowName" runat="server" Text="炭黑名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column6" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column7" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column8" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView2" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelWeight1" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center"  Title="油称(1)称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight11" runat="server" Frame="true" Region="West" Flex="1" Title="油称(1)称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store3" runat="server">
                                            <Model>
                                                <ext:Model ID="Model5" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel5" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn3" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column9" DataIndex="MaterialShowName" runat="server" Text="油料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column10" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column11" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column12" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView3" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight12" runat="server" Frame="true" Region="Center" Flex="1" Title="油称(1)称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store4" runat="server">
                                            <Model>
                                                <ext:Model ID="Model6" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel6" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn4" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column13" DataIndex="MaterialShowName" runat="server" Text="油料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column14" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column15" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column16" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView4" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelWeight5" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center"  Title="油称(2)称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight51" runat="server" Frame="true" Region="West" Flex="1" Title="油称(2)称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store5" runat="server">
                                            <Model>
                                                <ext:Model ID="Model7" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel7" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn5" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column17" DataIndex="MaterialShowName" runat="server" Text="油料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column18" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column19" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column20" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView5" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight52" runat="server" Frame="true" Region="Center" Flex="1" Title="油称(2)称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store6" runat="server">
                                            <Model>
                                                <ext:Model ID="Model8" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel8" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn6" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column21" DataIndex="MaterialShowName" runat="server" Text="油料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column22" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column23" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column24" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView6" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelWeight2" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center" Title="胶料称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight21" runat="server" Frame="true" Region="West" Flex="1" Title="胶料称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store7" runat="server">
                                            <Model>
                                                <ext:Model ID="Model9" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel9" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn7" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column25" DataIndex="MaterialShowName" runat="server" Text="胶料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column26" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column27" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column28" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView7" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight22" runat="server" Frame="true" Region="Center" Flex="1" Title="胶料称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store8" runat="server">
                                            <Model>
                                                <ext:Model ID="Model10" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel10" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn8" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column29" DataIndex="MaterialShowName" runat="server" Text="胶料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column30" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column31" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column32" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView8" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelWeight3" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center"  Title="小料校核称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight31" runat="server" Frame="true" Region="West" Flex="1" Title="小料校核称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store9" runat="server">
                                            <Model>
                                                <ext:Model ID="Model11" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel11" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn9" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column33" DataIndex="MaterialShowName" runat="server" Text="小料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column34" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column35" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column36" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView9" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight32" runat="server" Frame="true" Region="Center" Flex="1" Title="小料校核称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store10" runat="server">
                                            <Model>
                                                <ext:Model ID="Model12" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel12" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn10" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column37" DataIndex="MaterialShowName" runat="server" Text="小料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column38" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column39" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column40" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView10" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelWeight9" runat="server" Flex="1" Frame="true" Layout="BorderLayout" TitleAlign="Center"  Title="小料称量信息">
                            <Items>
                                <ext:GridPanel ID="PanelWeight91" runat="server" Frame="true" Region="West" Flex="1" Title="小料称量信息（1）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store11" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn11" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column41" DataIndex="MaterialShowName" runat="server" Text="原材料名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column42" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column43" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column44" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView11" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel> 
                                <ext:GridPanel ID="PanelWeight92" runat="server" Frame="true" Region="Center" Flex="1" Title="小料称量信息（2）" Scroll="None">
                                    <Store>
                                        <ext:Store ID="Store12" runat="server">
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="WeightType" />
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialCode" Type="String" />
                                                        <ext:ModelField Name="MaterialShowName" Type="String" />
                                                        <ext:ModelField Name="ActCode" Type="String" />
                                                        <ext:ModelField Name="ActionName" Type="String" />
                                                        <ext:ModelField Name="SetWeight" Type="Float" />
                                                        <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                        <ext:ModelField Name="isDifference" />
                                                        <ext:ModelField Name="LogRecordTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn12" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="Column45" DataIndex="MaterialShowName" runat="server" Text="原材量名称" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column46" DataIndex="ActionName" runat="server" Text="称量动作" Flex="1" Sortable="false">
                                            </ext:Column>
                                            <ext:Column ID="Column47" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设定重量" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="Column48" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="80" Sortable="false">
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView12" runat="server">
                                            <GetRowClass Fn="SetRowClass" />
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
