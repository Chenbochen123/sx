<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_FactoryMapping, App_Web_pghqvuve" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>厂商对应关系</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" language="javascript">
            //列表刷新数据重载方法
            var pnlCenterRefresh = function () {
                App.store.currentPage = 1;
                App.pageToolBar.doRefresh();
                return false;
            }

            //点击修改按钮
            var commandcolumn_direct_edit = function (record) {
                if (record != null) {
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
                    var url = "RawMaterialQuality/FactoryMappingImport.aspx";
                    var tabid = "Manager_RawMaterialQuality_FactoryMappingImport";
                    var tabp = parent.App.mainTabPanel;
                    var tab = tabp.getComponent(tabid);
                    if (tab) {
                        tab.close();
                    }
                    var title = "对应关系导入";
                    parent.addTab(tabid, title, url, true);
                    parent.refresh("");
                }
            }

            //点击删除按钮
            var commandcolumn_direct_delete = function (btn, record) {
                if (btn != "yes") {
                    return;
                }
                var MappingId = record.data.MappingId;
                App.direct.commandcolumn_direct_delete(MappingId, {
                    success: function (result) {
                        Ext.Msg.alert('操作', result);
                    },

                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });
            }

            //区分删除操作，并进行二次确认操作
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
    </script>
    <script type="text/javascript">
        //-------厂商绑定-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//厂商绑定
            var command = App.txtHiddenSelectCommand.getValue();
            switch (command) {
                case "supplier":
                    {
                        if (!App.windowModifyMapping.hidden) {
                            App.trfModifySupplierName.setValue(record.data.FacName);
                            App.txtModifySupplierId.setValue(record.data.ObjID);
                        }
                        if (!App.windowAddMapping.hidden) {
                            App.trfAddSupplierName.setValue(record.data.FacName);
                            App.txtAddSupplierId.setValue(record.data.ObjID);
                        }
                    }
                    break;
                case "manufacturer":
                    {
                        if (!App.windowModifyMapping.hidden) {
                            App.trfModifyManufacturerName.setValue(record.data.FacName);
                            App.txtModifyManufacturerId.setValue(record.data.ObjID);
                        }
                        if (!App.windowAddMapping.hidden) {
                            App.trfAddManufacturerName.setValue(record.data.FacName);
                            App.txtAddManufacturerId.setValue(record.data.ObjID);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        var SelectSupplier = function () {//供应商绑定查询
            App.txtHiddenSelectCommand.setValue("supplier");
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var SelectManufacturer = function () {//处置人绑定查询
            App.txtHiddenSelectCommand.setValue("manufacturer");
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }

        Ext.create("Ext.window.Window", {//生产厂家带窗体
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择生产厂家",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmFactoryMapping" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="新增对应关系" ID="btnAdd">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击新增供应商与生产厂商的对应关系" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlCenterRefresh"></Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle0" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导入" ID="btnImport">
                                <Listeners>
                                    <Click Handler="commandcolumn_direct_edit(null)" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
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
                    <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>   
                                    <ext:ComboBox ID="cbxSeriesName" runat="server" FieldLabel="原材料系列" LabelAlign="Right"
                                       Editable="false" Flex="1">
                                        <Listeners>
                                            <Select Fn="pnlCenterRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:TextField ID="txtSupplierName" runat="server" FieldLabel="供应商名称" LabelAlign="Right"
                                        Visible="true"  LabelWidth="80" Flex="1">
                                    </ext:TextField>                              
                                    <ext:TextField ID="txtSupplierERPCode" runat="server" FieldLabel="供应商ERP编号" LabelAlign="Right"
                                        Visible="true" LabelWidth="100" Flex="1">
                                    </ext:TextField>     
                                    <ext:TextField ID="txtManufacturerName" runat="server" FieldLabel="生产厂名称" LabelAlign="Right"
                                        Visible="true"  LabelWidth="80" Flex="1">
                                    </ext:TextField>                              
                                    <ext:TextField ID="txtManufacturerERPCode" runat="server" FieldLabel="生产厂ERP编号" LabelAlign="Right"
                                        Visible="true" LabelWidth="100" Flex="1">
                                    </ext:TextField>                                                        
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit"
                MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlMapping" runat="server" MarginsSummary="0 5 5 5">
                      <Store>
                           <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="MappingId">
                                        <Fields>
                                        <ext:ModelField Name="MappingId" />
                                        <ext:ModelField Name="SeriesId" />
                                        <ext:ModelField Name="SupplierId" />
                                        <ext:ModelField Name="SeriesName" />
                                        <ext:ModelField Name="SupplierName" />
                                        <ext:ModelField Name="SupplierERPCode" />
                                        <ext:ModelField Name="ManufacturerId" />
                                        <ext:ModelField Name="ManufacturerName" />
                                        <ext:ModelField Name="ManufacturerERPCode" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                           </ext:Store>
                      </Store>
                      <ColumnModel ID="colModel1" runat="server">
                          <Columns>
                              <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                              <ext:Column ID="mappingId" runat="server" Text="关系ID" DataIndex="MappingId" Visible="false"/>
                              <ext:Column ID="supplierId" runat="server" Text="供应商ID" DataIndex="SupplierId" Visible="false"/>
                              <ext:Column ID="manufacturerId" runat="server" Text="生产商ID" DataIndex="ManufacturerId" Visible="false"/>
                              <ext:Column ID="seriesName" runat="server" Text="原材料系列" DataIndex="SeriesName" Flex="1"/>
                              <ext:Column ID="supplierName" runat="server" Text="供应商名称" DataIndex="SupplierName" Flex="1"/>
                              <ext:Column ID="supplierERPCode" runat="server" Text="供应商ERP编号" DataIndex="SupplierERPCode" Flex="1"/>
                              <ext:Column ID="manufacturerName" runat="server" Text="生产商名称" DataIndex="ManufacturerName" Flex="1"/>
                              <ext:Column ID="manufacturerERPCode" runat="server" Text="生产商ERP编号" DataIndex="ManufacturerERPCode" Flex="1"/>
                              <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1"/>
                              <ext:CommandColumn ID="commandCol1" runat="server" Width="130" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                            <ToolTip Text="修改本条数据" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
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
                                                    <Select Handler="#{pnlMapping}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
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
            <ext:Window ID="windowModifyMapping" runat="server" Icon="MonitorEdit" Closable="false" Title="修改对应关系"
                Width="360" Height="195" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlModifyMapping" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="100" Mode="Raw" />
                                <ext:ConfigItem Name="Width" Value="280" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtModifyMappingId" runat="server" FieldLabel="关系ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtModifySupplierId" runat="server" FieldLabel="供应商ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtModifyManufacturerId" runat="server" FieldLabel="生产商ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtModifySeriesId" runat="server" FieldLabel="系列ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            
                            <ext:ComboBox ID="cbxModifySeriesName" runat="server" FieldLabel="原材料系列" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                            </ext:ComboBox>
                            <ext:TriggerField ID="trfModifySupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectSupplier" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="trfModifyManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectManufacturer" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
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
            <ext:Window ID="windowAddMapping" runat="server" Icon="MonitorEdit" Closable="false" Title="添加对应关系"
                Width="360" Height="195" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlAddMapping" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="100" Mode="Raw" />
                                <ext:ConfigItem Name="Width" Value="280" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtAddMappingId" runat="server" FieldLabel="关系ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtAddSupplierId" runat="server" FieldLabel="供应商ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtAddManufacturerId" runat="server" FieldLabel="生产商ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:TextField ID="txtAddSeriesId" runat="server" FieldLabel="系列ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                            <ext:ComboBox ID="cbxAddSeriesName" runat="server" FieldLabel="原材料系列" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                            </ext:ComboBox>
                            <ext:TriggerField ID="trfAddSupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectSupplier" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="trfAddManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right"
                                Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" Width="287">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectManufacturer" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnAddMappingSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddMappingSave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnAddMappingSave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnAddMappingCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Hidden ID="txtHiddenSelectCommand" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
