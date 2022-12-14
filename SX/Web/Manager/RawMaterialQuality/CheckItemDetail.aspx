<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckItemDetail.aspx.cs" Inherits="Manager_RawMaterialQuality_CheckItemDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>检测指标维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            if (record != null) {
                var DetailId = record.data.DetailId;
                App.direct.commandcolumn_direct_edit(DetailId, {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });
            }
            else {
                var url = "RawMaterialQuality/CheckItemDetailImport.aspx";
                var tabid = "Manager_RawMaterialQuality_CheckItemDetailImport";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent(tabid);
                if (tab) {
                    tab.close();
                }
                var title = "检测指标导入";
                parent.addTab(tabid, title, url, true);
                parent.refresh("");
            }
        }


        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var DetailId = record.data.DetailId;
            App.direct.commandcolumn_direct_delete(DetailId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        //刷新下方列表
        var pnlSouthRefresh = function () {
            App.storeDetail.currentPage = 1;
            App.pageToolBar2.doRefresh();
            return false;
        }

        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            App.txtHiddenMaterialMinorTypeId.setValue(record.getId());
            App.direct.ResetDetailPanel();
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };

        //列表刷新数据重载方法
        var pnlCenterRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //转换DeleteFlag显示方式
        var deleteConvert = function (value) {
            return Ext.String.format(value == "1" ? "是" : "否");
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
       <ext:ResourceManager ID="rmProperty" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlWest" runat="server" Region="West" Width="200" Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel ID="treeSeries" runat="server" Title="原材料系列" Region="Center" Icon="FolderGo"
                            AutoHeight="true" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeSeriesStore" runat="server">
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
                                <ItemClick Handler="loadPage(record)" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>

              <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit1">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlCenterRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle0" />
                                <ext:Button runat="server" Icon="DatabaseCopy" Text="复制" ID="btnCopy">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttCopy" runat="server" Html="点击复制指定原材料的检测标准" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_Copy_Click">
                                            <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle1" />
                                <ext:Button runat="server" Icon="PastePlain" Text="粘贴" ID="btnPaste">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttPaste" runat="server" Html="点击将已复制的检测标准粘贴到指定原材料" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_Paste_Click">
                                            <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导入" ID="btnImport">
                                    <Listeners>
                                        <Click Handler="commandcolumn_direct_edit(null)" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle3" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end1" />
                                <ext:ToolbarFill ID="toolbarFill_end1" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                      <ext:FormPanel runat="server" ID="pnlForm" Layout="ColumnLayout" AutoHeight="true">
                         <Items>
                              <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                <Items>
                                    <ext:ComboBox ID="cbxStandard" runat="server" FieldLabel="执行标准" LabelAlign="Right" Visible="true" Editable="false" Width="270">
                                        <Items>
                                        </Items>
                                        <Listeners>
                                            <Select Fn="pnlSouthRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                 </Items>
                            </ext:Container>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                <Items>
                                    <ext:TextField ID="txtMaterialCode" runat="server" FieldLabel="原材料代码" LabelAlign="Right" Visible="true"></ext:TextField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                <Items>
                                 <ext:TextField ID="txtMaterialName" runat="server" FieldLabel="原材料名称" LabelAlign="Right" Visible="true"></ext:TextField>
                                </Items>
                              </ext:Container>
                         </Items>
                      </ext:FormPanel>
                    </Items>
              </ext:Panel>

              <ext:Panel ID="pnlEast" runat="server" Region="Center" Layout="BorderLayout">
                  <Items>
                      <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                        <Items>
                            <ext:GridPanel ID="pnlMaterial" runat="server" MarginsSummary="0 5 5 5">
                                <Store>
                                    <ext:Store ID="store" runat="server" PageSize="15">
                                        <Proxy>
                                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                        </Proxy>
                                        <Model>
                                            <ext:Model ID="model" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="ObjID" />
                                                    <ext:ModelField Name="MaterialCode" />
                                                    <ext:ModelField Name="MinorTypeID" />
                                                    <ext:ModelField Name="MaterialName" />
                                                    <ext:ModelField Name="MaterialOtherName" />
                                                    <ext:ModelField Name="MaterialSimpleName" />
                                                    <ext:ModelField Name="ERPCode" />
                                                    <ext:ModelField Name="DeleteFlag" />
                                                    <ext:ModelField Name="Remark" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel ID="colModel" runat="server">
                                    <Columns>
                                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                        <ext:Column ID="objId" runat="server" Text="物料编码" DataIndex="ObjID" Visible="false" Flex="1"/>
                                        <ext:Column ID="materialCode" runat="server" Text="物料代码" DataIndex="MaterialCode" Flex="1"/>
                                        <ext:Column ID="materialName1" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1"/>
                                        <ext:Column ID="materialOtherName" runat="server" Text="物料别名" DataIndex="MaterialOtherName" Flex="1"/>
                                        <ext:Column ID="materialSimpleName" runat="server" Text="物料简称" DataIndex="MaterialSimpleName" Flex="1"/>
                                        <ext:Column ID="minorTypeId" runat="server" Text="物料细类" DataIndex="MinorTypeID" Flex="1"/>
                                        <ext:Column ID="erpCode" runat="server" Text="ERP代码" DataIndex="ERPCode" Flex="1"/>
                                        <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1"/>
                                    </Columns>
                                </ColumnModel>
                                  <SelectionModel>
                                        <ext:RowSelectionModel ID="rowSelect" runat="server" Mode="Single">
                                            <Listeners>
                                                <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                  </SelectionModel>
                              <BottomBar>
                                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                                        <Items>
                                            <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="10" />
                                                    <ext:ListItem Text="50" />
                                                    <ext:ListItem Text="100" />
                                                    <ext:ListItem Text="200" />
                                                </Items>
                                                <SelectedItems>
                                                    <ext:ListItem Value="10" />
                                                </SelectedItems>
                                                <Listeners>
                                                    <Select Handler="#{pnlMaterial}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Plugins>
                                            <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                        </Plugins>
                                    </ext:PagingToolbar>
                              </BottomBar>
                            </ext:GridPanel>
                        </Items>
                      </ext:Panel>

                      <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="检测指标" Height="260" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                          <TopBar>
                                <ext:Toolbar runat="server" ID="barUnit2">
                                    <Items>
                                        <ext:Button runat="server" Icon="DatabaseSave" Text="保存指标" ID="btnSaveDetail">
                                            <ToolTips>
                                                <ext:ToolTip ID="ttSaveDetail" runat="server" Html="点击保存选中的检测指标" />
                                            </ToolTips>
                                            <DirectEvents>
                                                <Click OnEvent="btn_savedetail_Click">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparatorMiddle" />
                                        <ext:Button runat="server" Icon="Add" Text="添加指标" ID="btnAddDetail">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="点击新建一条当前原材料的检测指标" />
                                            </ToolTips>
                                            <DirectEvents>
                                                <Click OnEvent="btn_adddetail_Click">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator_end2" />
                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end2" />
                                        <ext:ToolbarFill ID="toolbarFill_end2" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:GridPanel ID="pnlDetail" runat="server" MarginsSummary="0 5 5 5">
                                        <Store>
                                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                                <Model>
                                                    <ext:Model ID="modelValue" runat="server" IDProperty="DetailId">
                                                        <Fields>
                                                            <ext:ModelField Name="ItemId" />
                                                            <ext:ModelField Name="DetailId" />
                                                            <ext:ModelField Name="MaterialName" />
                                                            <ext:ModelField Name="Frequency" />
                                                            <ext:ModelField Name="ItemName" />
                                                            <ext:ModelField Name="GoodTextValue" />
                                                             <ext:ModelField Name="MinTextValue" />
                                                              <ext:ModelField Name="MaxTextValue" />
                                                            <ext:ModelField Name="PrimeTextValue" />
                                                            <ext:ModelField Name="Version" />
                                                            <ext:ModelField Name="CheckMethod" />
                                                            <ext:ModelField Name="Remark" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                                <Parameters>
                                                    <ext:StoreParameter Name="MaterialCode" Mode="Raw" Value="#{pnlMaterial}.getSelectionModel().hasSelection() ? #{pnlMaterial}.getSelectionModel().getSelection()[0].data.MaterialCode : -1" />
                                                </Parameters>
                                            </ext:Store>
                                        </Store>
                                          <ColumnModel ID="colModel2" runat="server">
                                              <Columns>
                                                  <ext:RowNumbererColumn ID="rowNumCol2" runat="server" Width="35" />
                                                  <ext:Column ID="materialName2" runat="server" Text="原材料型号" DataIndex="MaterialName" Flex="1" />
                                                  <ext:Column ID="frequency" runat="server" Text="检测频次" DataIndex="Frequency" Flex="1" MaxWidth="70"/>
                                                  <ext:Column ID="itemName" runat="server" Text="项目名称" DataIndex="ItemName" Flex="1" />
                                                    <ext:Column ID="Column1" runat="server" Text="最小值指标" DataIndex="MinTextValue" Flex="1" />
                                                  <ext:Column ID="goodTextValue" runat="server" Text="合格指标" DataIndex="GoodTextValue" Flex="1" />
                                                    
                                                      <ext:Column ID="Column2" runat="server" Text="最大值指标" DataIndex="MaxTextValue" Flex="1" />
                                    <%--              <ext:Column ID="primeTextValue" runat="server" Text="一级品指标" DataIndex="PrimeTextValue" Flex="1" />--%>
                                                  <ext:Column ID="version" runat="server" Text="版本" DataIndex="Version" Flex="1" MaxWidth="50"/>
                                                  <ext:Column ID="checkMethod" runat="server" Text="检测方法" DataIndex="CheckMethod" Flex="1" />
                                                  <ext:Column ID="rematk" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                                                    <ext:CommandColumn ID="commandCol1" runat="server" Width="120" Text="操作" Align="Center">
                                                        <Commands>
                                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                                                <ToolTip Text="修改本条数据" />
                                                            </ext:GridCommand>
                                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" >
                                                                <ToolTip Text="删除本条数据" />
                                                            </ext:GridCommand>
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="return commandcolumn_click(command, record);" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                              </Columns>
                                          </ColumnModel>
                                            <SelectionModel>
                                                <ext:CheckboxSelectionModel ID="detailSelectionModel" runat="server" Mode="Simple"/>
                                            </SelectionModel>   
                                          <BottomBar>
                                                <ext:PagingToolbar ID="pageToolBar2" runat="server">
                                                    <Items>
                                                        <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                                        <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                                        <ext:ComboBox ID="ComboBox2" runat="server" Width="80" Editable="false">
                                                            <Items>
                                                                <ext:ListItem Text="10" />
                                                                <ext:ListItem Text="50" />
                                                                <ext:ListItem Text="100" />
                                                                <ext:ListItem Text="200" />
                                                            </Items>
                                                            <SelectedItems>
                                                                <ext:ListItem Value="10" />
                                                            </SelectedItems>
                                                            <Listeners>
                                                                <Select Handler="#{pnlDetail}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar2}.doRefresh(); return false;" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Items>
                                                    <Plugins>
                                                        <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                                    </Plugins>
                                                </ext:PagingToolbar>
                                          </BottomBar>
                                </ext:GridPanel>
                            </Items>
                          </ext:Panel>
                      </Items>
                </ext:Panel> 
                <ext:Window ID="windowAddDetail" runat="server" Icon="MonitorAdd" Closable="false" Title="添加检测指标"
                    Width="320" Height="545" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlAddItem" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtAddItemId" runat="server" FieldLabel="项目编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtAddMaterialName" runat="server" FieldLabel="原材料型号"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:ComboBox ID="cbxAddItemName"  runat="server" FieldLabel="检测项目"  LabelAlign="Right" Editable="false" OnDirectChange="cbxAddItemName_DirectChange" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"> 
                                    <Items>
                                    </Items>
                                </ext:ComboBox>
                                <ext:ComboBox ID="cbxAddFrequency"  runat="server" FieldLabel="检测频次"  LabelAlign="Right" Editable="false"> 
                                    <SelectedItems>
                                        <ext:ListItem Text="C" Value="C" />
                                    </SelectedItems>
                                    <Items>
                                        <ext:ListItem Text="C" Value="C" />
                                        <ext:ListItem Text="M" Value="M" />
                                        <ext:ListItem Text="N" Value="N" />
                                        <ext:ListItem Text="F" Value="F" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:TextField ID="txtAddCheckMethod" runat="server" FieldLabel="检测方法" LabelAlign="Right" MaxLength="50" />
                                <ext:TextField ID="TextOrderID" runat="server" FieldLabel="项目序号" LabelAlign="Right" MaxLength="50" />
                                <ext:TextField ID="TextTexing" runat="server" FieldLabel="特殊特性" LabelAlign="Right" MaxLength="50" />
                                <ext:TextField ID="txtAddRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                                <ext:Checkbox runat="server" ID="cbxAddActivated" FieldLabel="直接启用" LabelAlign="Right" Checked="true"></ext:Checkbox>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnAddDetailSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:TabPanel ID="pnlAddDetail" runat="server" Flex="1" BodyPadding="5" Y="4" >
                            <Items>
                                <ext:FormPanel ID="pnlAddDetailPrime" runat="server" Title="一级品">
                                    <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtAddPrimeMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddPrimeIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxAddPrimeOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxAddPrimeOperator_change">
                                            <Items>
                                                <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                            <%--    <ext:ListItem Text="最大值" Value="A">
                                                </ext:ListItem>
                                                <ext:ListItem Text="最小值" Value="I">
                                                </ext:ListItem>
                                                <ext:ListItem Text="平均值" Value="P">
                                                </ext:ListItem>--%>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtAddPrimeMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddPrimeIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtAddPrimeTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                <ext:FormPanel ID="pnlAddGoodPrime" runat="server" Title="合格品">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtAddGoodMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddGoodIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxAddGoodOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxAddGoodOperator_change">
                                            <Items>
                                                    <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                              
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtAddGoodMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddGoodIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtAddGoodTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                 <ext:FormPanel ID="pnlAddMinPrime" runat="server" Title="最小值">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtAddMinMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddMinIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxAddMinOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxAddMinOperator_change">
                                            <Items>
                                                    <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                                  
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtAddMinMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddMinIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtAddMinTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                  <ext:FormPanel ID="pnlAddMaxPrime" runat="server" Title="最大值">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtAddMaxMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddMaxIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxAddMaxOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxAddMaxOperator_change">
                                            <Items>
                                                    <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                                 
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtAddMaxMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxAddMaxIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtAddMaxTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                         </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddDetailSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnAddDetailSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddDetailCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="windowModifyDetail" runat="server" Icon="MonitorEdit" Closable="false" Title="修改检测指标"
                    Width="320" Height="545" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlEditItem" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtModifyDetailId" runat="server" FieldLabel="具体项目编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyItemId" runat="server" FieldLabel="项目编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyMaterialName" runat="server" FieldLabel="原材料型号"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtModifyItemName" runat="server" FieldLabel="项目名称"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:ComboBox ID="cbxModifyFrequency" runat="server" FieldLabel="检测频次"  LabelAlign="Right" Editable="false">
                                    <Items>
                                        <ext:ListItem Text="C" Value="C" />
                                        <ext:ListItem Text="M" Value="M" />
                                        <ext:ListItem Text="N" Value="N" />
                                        <ext:ListItem Text="F" Value="F" />
                                    </Items>
                                </ext:ComboBox>
                                <ext:TextField ID="txtModifyCheckMethod" runat="server" FieldLabel="检测方法" LabelAlign="Right" MaxLength="50" />
                                  <ext:TextField ID="txtModifyOrderID" runat="server" FieldLabel="项目序号" LabelAlign="Right" MaxLength="50" />
                                <ext:TextField ID="txtModifyTexing" runat="server" FieldLabel="特殊特性" LabelAlign="Right" MaxLength="50" />
                              
                                <ext:TextField ID="txtModifyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                                <ext:Checkbox runat="server" ID="cbxModifyActivated" FieldLabel="直接启用" LabelAlign="Right" Checked="true"></ext:Checkbox>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifyDetailSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:TabPanel ID="pnlEditDetail" runat="server" Flex="1" BodyPadding="5" Y="4" >
                            <Items>
                                <ext:FormPanel ID="pnlEditDetailPrime" runat="server" Title="一级品">
                                    <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtModifyPrimeMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyPrimeIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxModifyPrimeOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxModifyPrimeOperator_change">
                                            <Items>
                                                <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                                <%-- <ext:ListItem Text="最大值" Value="A">
                                                </ext:ListItem>
                                                <ext:ListItem Text="最小值" Value="I">
                                                </ext:ListItem>
                                                <ext:ListItem Text="平均值" Value="P">
                                                </ext:ListItem>--%>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtModifyPrimeMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyPrimeIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtModifyPrimeTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                <ext:FormPanel ID="pnlEditDetailGood" runat="server" Title="合格品">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtModifyGoodMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyGoodIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxModifyGoodOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxModifyGoodOperator_change">
                                            <Items>
                                                  <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                            
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtModifyGoodMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyGoodIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtModifyGoodTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                      <ext:FormPanel ID="FormPanel1" runat="server" Title="最小值">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtModifyMinMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyMinIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxModifyMinOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxModifyMinOperator_change">
                                            <Items>
                                                  <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                               
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtModifyMinMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyMinIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtModifyMinTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                                      <ext:FormPanel ID="FormPanel2" runat="server" Title="最大值">
                                     <FieldDefaults>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                        </CustomConfig>
                                    </FieldDefaults>
                                    <Items>
                                        <ext:TextField ID="txtModifyMaxMinValue" runat="server" FieldLabel="起始值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyMaxIncludeMinBorder" FieldLabel="起始边界" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxModifyMaxOperator" runat="server" FieldLabel="关系符" LabelAlign="Right" Editable="false" Width="170" OnDirectChange="cbxModifyMaxOperator_change">
                                            <Items>
                                                  <ext:ListItem Text="无" Value="无">
                                                </ext:ListItem>
                                                <ext:ListItem Text="－" Value="－">
                                                </ext:ListItem>
                                                <ext:ListItem Text=">" Value=">">
                                                </ext:ListItem>
                                                <ext:ListItem Text="<" Value="<">
                                                </ext:ListItem>
                                            
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtModifyMaxMaxValue" runat="server" FieldLabel="结束值" LabelAlign="Right" />
                                        <ext:Checkbox runat="server" ID="cbxModifyMaxIncludeMaxBorder" FieldLabel="结束边界" LabelAlign="Right" />
                                        <ext:TextField ID="txtModifyMaxTextValue" runat="server" FieldLabel="文字标准" LabelAlign="Right" MaxLength="50"/>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                         </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifyDetailSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifyDetailSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyDetailCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="txtHiddenMaterialName" runat="server"></ext:Hidden>
                <ext:Hidden ID="txtHiddenMaterialCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="txtHiddenMaterialMinorTypeId" runat="server" Text="1"></ext:Hidden>
                <ext:Hidden ID="txtHiddenSeriesId" runat="server" Text="1"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
