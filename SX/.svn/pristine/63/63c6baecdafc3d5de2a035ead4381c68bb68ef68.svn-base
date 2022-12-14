<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_StopManage_MixerFault_MixerFaultMain, App_Web_44oxblsy" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>密炼机故障分析</title>
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

        var btnAddClick = function (sender, e, fn) {
            var url = "Equipment/StopManage/MixerFault/MixerGroupAnalysis.aspx";
            var tabid = "Manager_Equipment_StopManage_MixerFault_MixerGroupAnalysis";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            parent.addTab(tabid, "密炼机故障统计分析", url, true);
        }
    </script>
    
     <script type="text/javascript">
         Ext.create("Ext.window.Window", {
             id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
             height: 450,
             hidden: true,
             width: 370,
             html: "<iframe src='../../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
                                 <ext:Button runat="server" Icon="ChartBarLink" Text="统计分析" ID="btn_Analysis">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击查看密炼机故障统计分析" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnAddClick">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                                 <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_fault_name" runat="server" FieldLabel="故障名称" LabelAlign="Right" />
                                                 <ext:TriggerField ID="txt_equip_name"  runat="server" FieldLabel="设备名称" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="txtEquipName_click" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
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
                                                  <ext:ComboBox ID="txtAlarmState" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="报警状态">
                                         
                                        </ext:ComboBox>
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
                        <ext:Store ID="store" runat="server" PageSize="15" RemoteSort="true"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="FaultCode" />
                                        <ext:ModelField Name="FaultName"  />
                                        <ext:ModelField Name="FaultPosition" />
                                        <ext:ModelField Name="AlarmState" />
                                        <ext:ModelField Name="FaultDate" Type="Date"/>
                                        <ext:ModelField Name="FaultType" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="WorkShopCode" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="ObjID" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="故障编号" DataIndex="FaultCode" Width="150"  />
                            <ext:Column ID="fault_name" runat="server" Text="故障名称" DataIndex="FaultName" Width="150"  />
                            <ext:Column ID="fault_position" runat="server" Text="故障位置" DataIndex="FaultPosition" Width="150"  />
                            <ext:Column ID="alarm_state" runat="server" Text="警报状态" DataIndex="AlarmState" Width="150"  />
                            <ext:DateColumn Format="yyyy-MM-dd HH:mm:ss" ID="fault_date" runat="server" Text="故障日期" DataIndex="FaultDate" Width="150"  />
                            <ext:Column ID="fault_type" runat="server" Text="故障类型" DataIndex="FaultType" Width="150"  />
                            <ext:Column ID="equip_code" runat="server" Text="所属机台" DataIndex="EquipCode" Width="150"  />
                            <ext:Column ID="workshop_code" runat="server" Text="所属车间" DataIndex="WorkShopCode" Width="150" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="150"  />
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