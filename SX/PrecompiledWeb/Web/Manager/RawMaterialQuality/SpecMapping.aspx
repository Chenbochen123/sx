﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_SpecMapping, App_Web_pghqvuve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>规格对应关系</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            if (record != null)
            {
                var MappingId = record.data.MappingId;
                App.direct.commandcolumn_direct_edit(MappingId, {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });
            }
            else {
                var url = "RawMaterialQuality/SpecMappingImport.aspx";
                var tabid = "Manager_RawMaterialQuality_SpecMappingImport";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent(tabid);
                if (tab) {
                    tab.close();
                }
                var title = "规格对应关系导入";
                parent.addTab(tabid, title, url, true);
                parent.refresh("");
            }
        }

        //区分操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
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
            App.storeValue.currentPage = 1;
            App.pageToolBar2.doRefresh();
            return false;
        }

        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            App.txtHiddenMaterialMinorTypeId.setValue(record.getId());
            App.direct.ResetMappingPanel();
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
       <ext:ResourceManager ID="rmSpecMapping" runat="server" />
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
                                        <ext:ToolTip ID="ttCopy" runat="server" Html="点击复制指定原材料的规格" />
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
                                        <ext:ToolTip ID="ttPaste" runat="server" Html="点击将已复制的规格粘贴到指定原材料" />
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
                                                <Select Handler="#{storeMapping}.reload();" Buffer="250" />
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

                      <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="原材料规格" Height="260" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                          <TopBar>
                                <ext:Toolbar runat="server" ID="barUnit2">
                                    <Items>
                                        <ext:Button runat="server" Icon="Accept" Text="保存规格" ID="btnSaveMapping">
                                            <ToolTips>
                                                <ext:ToolTip ID="ttSaveMapping" runat="server" Html="点击保存选中的规格" />
                                            </ToolTips>
                                            <DirectEvents>
                                                <Click OnEvent="btn_savemapping_Click">
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
                                <ext:GridPanel ID="pnlMapping" runat="server" MarginsSummary="0 5 5 5">
                                        <Store>
                                            <ext:Store ID="storeMapping" runat="server" PageSize="10" OnReadData="RowSelect">
                                                <Model>
                                                    <ext:Model ID="modelValue" runat="server" IDProperty="MappingId">
                                                        <Fields>
                                                            <ext:ModelField Name="MappingId" />
                                                            <ext:ModelField Name="MaterialCode" />
                                                            <ext:ModelField Name="MaterialName" />
                                                            <ext:ModelField Name="SpecId" />
                                                            <ext:ModelField Name="SpecName" />
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
                                                  <ext:Column ID="specName" runat="server" Text="规格名称" DataIndex="SpecName" Flex="1" />
                                                  <ext:Column ID="rematk" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                                                  <ext:CommandColumn ID="commandCol1" runat="server" Width="60" Text="操作" Align="Center">
                                                        <Commands>
                                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                                                <ToolTip Text="修改本条数据" />
                                                            </ext:GridCommand>
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="return commandcolumn_click(command, record);" />
                                                        </Listeners>
                                                  </ext:CommandColumn>
                                              </Columns>
                                          </ColumnModel>
                                            <SelectionModel>
                                                <ext:CheckboxSelectionModel ID="mappingSelectionModel" runat="server" Mode="Simple"/>
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
                                                                <Select Handler="#{pnlMapping}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar2}.doRefresh(); return false;" />
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
                <ext:Window ID="windowModifyMapping" runat="server" Icon="MonitorEdit" Closable="false" Title="修改规格映射"
                    Width="320" Height="190" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlEditMapping" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtModifyMappingId" runat="server" FieldLabel="规格映射编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifySpecId" runat="server" FieldLabel="规格编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyMaterialName" runat="server" FieldLabel="原材料型号"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtModifySpecName" runat="server" FieldLabel="规格名称"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtModifyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                                <ext:Checkbox runat="server" ID="cbxModifyActivated" FieldLabel="直接启用" LabelAlign="Right" Checked="true"></ext:Checkbox>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModifyMappingSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifyMappingSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifyMappingSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyMappingCancel" runat="server" Text="取消" Icon="Cancel">
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
