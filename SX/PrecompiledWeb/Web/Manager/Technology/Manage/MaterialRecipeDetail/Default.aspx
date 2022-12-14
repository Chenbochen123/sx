<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_Default, App_Web_a5rwyiqa" %>

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
    <script src="<%= Page.ResolveUrl("./") %>Default.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script src="<%= Page.ResolveUrl("./") %>Weight.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="DiskEdit" Text="编辑" ID="btnCanSave">
                                    <Listeners>
                                        <Click Handler="SetCanSave();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Disk" Text="保存" ID="btnSave" Disabled="true">
                                    <Listeners>
                                        <Click Handler="Save()"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" ID="btnAuditPmtRecipePass" Icon="AwardStarBronze2" Text="审核通过">
                                    <Listeners>
                                        <Click Fn="btnAuditPmtRecipePassClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                <ext:Button runat="server" ID="btnEditSuJieJiWeight" Icon="BrickEdit" Text="车间调整权限">
                                    <Listeners>
                                        <Click Fn="btn_edit_sujieji_weight_click"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Hidden ID="hiddenRecipeObjID" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenCommandID" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenMaterialID" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>

                <ext:TabPanel ID="Panel1" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false" AutoScroll="false" MinTabWidth="200">
                    <Items>
                        <ext:Panel ID="pnlMain" runat="server" Title="基本信息" />
                        <ext:Panel ID="pnlWeight" runat="server" Title="称量信息" CloseAction="Hide"/>
                        <ext:Panel ID="pnlMixing" runat="server" Title="混炼信息" CloseAction="Hide"/>
                        <ext:Panel ID="pnlAdvanceSeparateWeight" runat="server" Title="预分散称量信息"   CloseAction="Hide" />
                        <ext:Panel ID="pnlOpenMixing" runat="server" Title="开炼信息"   CloseAction="Hide" />
                        <ext:Panel ID="pnlQDrug" runat="server" Title="Q药品示方"   CloseAction="Hide" />
                         <ext:Panel ID="pnlMILL" runat="server" Title="MILL示方"   CloseAction="Hide" />
                    </Items>
                </ext:TabPanel>
                <ext:Window ID="winEditWeight" runat="server" Icon="BrickEdit" Closable="false" Title="车间调整"
                    Height="345" Width="710" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5"  Layout="BorderLayout">
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" Region="North">
                            <Items>
                                <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="1">
                                    <Items>
                                        <ext:NumberField ID="nonAuditMakeUpTemp" runat="server" FieldLabel="补偿温度" LabelAlign="Right" />
                                    </Items>
                                    
                                </ext:Container>
                            </Items>
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="1">
                                 
                                     <Items>
                                       <ext:NumberField ID="txtPackWeight" runat="server" FieldLabel="小料袋重(g)" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                         <ext:GridPanel ID="gridPanelEditWeight" runat="server" Flex="1" Frame="true" Height="275" Region="Center">
                            <Store>
                                <ext:Store ID="storeSetWeightRub" runat="server">
                                    <Model>
                                        <ext:Model ID="ModelRub" runat="server" Name="gridPanelWeightRubModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelRub" runat="server"  Width="800" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColRub" runat="server" Width="30" />
                                    <ext:Column ID="WeightObj" DataIndex="ObjID" runat="server" Hidden="true" >
                                    </ext:Column>
                                    <ext:Column ID="MaterialCodeRub" DataIndex="RecipeMaterialCode" runat="server" Text="胶料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count" Width="200">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelRub" DataIndex="MaterialCode" runat="server" Width="120" Text="物料代码" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeRub" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="80">
                                    </ext:Column>
                                    <ext:Column ID="SetWeightRub" DataIndex="OldSetWeight" runat="server" Text="原定重量" Hideable="false" Sortable="false" Width="120" SummaryType="Sum">
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowRub" DataIndex="SetWeight" runat="server" Width="120" Text="设定重量" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField8" runat="server" AllowBlank="false" MinValue="0" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingRub" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel4" runat="server" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnEditWeightSave" runat="server" Text="确定" Icon="Accept">
                            <Listeners>
                                <Click Fn="btn_edit_weight_save"></Click>
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnEditWeightCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnEditWeightCancel_Click"></Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
                    </Listeners>
            </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
