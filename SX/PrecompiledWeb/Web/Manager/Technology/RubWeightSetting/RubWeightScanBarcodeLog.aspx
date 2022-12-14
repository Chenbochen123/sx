<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_RubWeightSetting_RubWeightScanBarcodeLog, App_Web_pxnhn0ln" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>胶料秤扫描日志查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>
     <script type="text/javascript">
         Ext.create("Ext.window.Window", {
             id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
             height: 450,
             hidden: true,
             width: 370,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择机台",
             modal: true
         })
         var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
             var queryWindow = App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window;
             var thisIsAddWindow = function (record) {
             }
             var thisIsEditWindow = function (record) {

             }
             var thisIsDefaultWindow = function (record) {
                 App.hidden_equip_code.setValue(record.data.EquipCode);
                 App.txt_equip_name.setValue(record.data.EquipName);
             }
             App.txt_equip_name.getTrigger(0).show();
             thisIsAddWindow(record);
             thisIsEditWindow(record);
             thisIsDefaultWindow(record);
             queryWindow.close();
         }
         var QueryEquipmentInfo = function (field, trigger, index) {
             App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
         }

         var txtEquipName_click = function (field, trigger, index) {
             switch (index) {
                 case 0:
                     field.getTrigger(0).hide();
                     field.setValue('');
                     App.txt_equip_name.setValue("");
                     App.hidden_equip_code.setValue("");
                     field.getEl().down('input.x-form-text').setStyle('background', "white");
                     break;
                 case 1:
                     QueryEquipmentInfo();
                     break;
             }
         }
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUnitQuery" runat="server" Height="85">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" Height="85">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="扫描开始时间" Layout="HBoxLayout" LabelAlign="Right" Flex="1">
                                                    <Items>
                                                        <ext:DateField ID="txtBeginDate" runat="server" Format="yyyy-MM-dd" Flex="6" />
                                                        <ext:TextField ID="txtBeginTime" runat="server" Flex="4">
                                                            <Plugins>
                                                                <ext:InputMask ID="InputMask1" runat="server" Mask="ah:bm:cs">
                                                                    <MaskSymbols>
                                                                        <ext:MaskSymbol Name="a" Regex="[012]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="h" Regex="[0-9]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="b" Regex="[0-5]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="m" Regex="[0-9]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="c" Regex="[0-5]" Placeholder="s" />
                                                                        <ext:MaskSymbol Name="s" Regex="[0-9]" Placeholder="s" />
                                                                    </MaskSymbols>
                                                                </ext:InputMask>
                                                            </Plugins>
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:TextField ID="txt_scan_barcode"  FieldLabel="扫描条码" runat="server" LabelAlign="Right" />
                                             <ext:TextField ID="ProName"  FieldLabel="生产物料" runat="server" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer2" runat="server" FieldLabel="扫描结束时间" Layout="HBoxLayout" LabelAlign="Right" Flex="1">
                                                        <Items>
                                                            <ext:DateField ID="txtEndDate" runat="server" Format="yyyy-MM-dd" Flex="6" />
                                                            <ext:TextField ID="txtEndTime" runat="server" Flex="4">
                                                                <Plugins>
                                                                    <ext:InputMask ID="InputMask3" runat="server" Mask="ah:bm:cs">
                                                                        <MaskSymbols>
                                                                            <ext:MaskSymbol Name="a" Regex="[012]" Placeholder="h" />
                                                                            <ext:MaskSymbol Name="h" Regex="[0-9]" Placeholder="h" />
                                                                            <ext:MaskSymbol Name="b" Regex="[0-5]" Placeholder="i" />
                                                                            <ext:MaskSymbol Name="m" Regex="[0-9]" Placeholder="i" />
                                                                            <ext:MaskSymbol Name="c" Regex="[0-5]" Placeholder="s" />
                                                                            <ext:MaskSymbol Name="s" Regex="[0-9]" Placeholder="s" />
                                                                        </MaskSymbols>
                                                                    </ext:InputMask>
                                                                </Plugins>
                                                            </ext:TextField>
                                                        </Items>
                                                </ext:FieldContainer>
                                                 <ext:ComboBox ID="txt_info_type" runat="server" FieldLabel="分类查询" LabelAlign="Right"  Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                  <ext:TextField ID="MateName"  FieldLabel="扫描物料" runat="server" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items> 
                                                <ext:TriggerField ID="txt_equip_name"  runat="server" FieldLabel="设备名称" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="txtEquipName_click" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox ID="cbxst" runat="server" EmptyText="全部" Editable="false" FieldLabel="扫描" LabelAlign="Right">
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="-1"></ext:ListItem>
                                                        <ext:ListItem Text="自动扫描" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="手工录入" Value="1"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="100" RemoteSort="true"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Dt" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="RecipeCode" />
                                        <ext:ModelField Name="ScanBarcode"  />
                                        <ext:ModelField Name="ScanMaterCode"  />
                                        <ext:ModelField Name="Msg"  />
                                        <ext:ModelField Name="Usercode"  />
                                        <ext:ModelField Name="ShiftId"  />
                                        <ext:ModelField Name="ClassId" />
                                        <ext:ModelField Name="ScanLogSignal" />
                                        <ext:ModelField Name="scanusedbarmsg" />
                                        <ext:ModelField Name="ProcDate" />
                                        <ext:ModelField Name="RecordDate" />
                                        <ext:ModelField Name="facname" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="Dt" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="dt" runat="server" Text="扫描时间" DataIndex="Dt" Width="150"  />
                            <ext:Column ID="equip_code" runat="server" Text="设备名称" DataIndex="EquipCode" Width="100"  />
                            <ext:Column ID="recipe_code" runat="server" Text="生产物料" DataIndex="RecipeCode" Width="120"  />
                            <ext:Column ID="scan_barcode" runat="server" Text="扫描条码" DataIndex="ScanBarcode" Width="150"  />
                             <ext:Column ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Width="120"  />
                            <ext:Column ID="RecordDate" runat="server" Text="入库日期" DataIndex="RecordDate" Width="120"  />
                            <ext:Column ID="facname" runat="server" Text="供应商" DataIndex="facname" Width="120"  />
                           
                            <ext:Column ID="scan_mater_code" runat="server" Text="扫描物料" DataIndex="ScanMaterCode" Width="120"  />
                            <ext:Column ID="msg" runat="server" Text="消息" DataIndex="Msg" Width="300"  />
                            <ext:Column ID="user_code" runat="server" Text="主机手名称" DataIndex="Usercode" Width="80" />
                            <ext:Column ID="shift_id" runat="server" Text="班次" DataIndex="ShiftId" Width="60"  />
                            <ext:Column ID="class_id" runat="server" Text="班组" DataIndex="ClassId" Width="60"  />
                            <ext:Column ID="Column1" runat="server" Text="状态" DataIndex="ScanLogSignal" Width="80"  />
                            <ext:Column ID="Column2" runat="server" Text="历史使用" DataIndex="scanusedbarmsg" Width="600"  />
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Hidden ID="hidden_equip_code" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>