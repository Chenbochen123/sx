<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Report_RubberDE, App_Web_0yyi2bmr" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>胶料定额计算</title>
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
      
        var fn_lock = function (ctrl) {
            App.direct.equip_lock_func(ctrl, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                    pnlListFresh();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

    </script>
    <script type="text/javascript">
        //------所属车间------查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryWorkShop_Request = function (record) {//所属车间返回值处理
            App.txt_workshop_code.setValue(record.data.WorkShopName);
            App.hidden_workshop_code.setValue(record.data.ObjID);
        }

        var SelectWorkShop = function (field, trigger, index, hiddenId) {//人员绑定查询
            switch (index) {
                case 0:
                    field.setValue('');
                    document.getElementById(hiddenId).value = '';
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryWorkShop_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//所属车间查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryWorkShop_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择所属车间",
            modal: true
        })
        //------------查询带回弹出框--END 

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            hidden: true,
            width: 370,
            height: 470,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
            App.txtEquipName.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            App.txtEquipName.setValue(record.data.EquipName);
        }
        var QueryEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.txtPptMaterial.getStore().reload();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }

    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <ext:ResourceManager ID="rmUnit" runat="server" />
          <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
           <ext:Hidden ID="hidden1" runat="server"></ext:Hidden>
          <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
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
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />  
                           <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator3" />
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
                                                    <ext:DateField ID="txtBeginDate" runat="server" Flex="1" FieldLabel="生产时间"  LabelAlign="Right" Editable="false" AllowBlank="false">
                                           
                                        </ext:DateField>     <ext:TriggerField ID="txt_workshop_code" runat="server" FieldLabel="所属车间" Editable="false" AllowBlank="false" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectWorkShop(this, trigger, index , 'hidden_workshop_code')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                               <%--  <ext:DateField ID="txtEndDate" runat="server" Flex="1" FieldLabel="结束生产时间"  LabelAlign="Right" Editable="false" AllowBlank="false">
                                         
                                        </ext:DateField>  --%>
                                         <ext:ComboBox ID="txtPptShift" runat="server" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班次信息" MinWidth="150" Flex="1">
                                        </ext:ComboBox>
                                             <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" FieldLabel="机台名称"  LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                               
                                            </Listeners>
                                        </ext:TriggerField>  
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_user" runat="server" FieldLabel="主机手" LabelAlign="Right" />
                                                 
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
                                        <ext:ModelField Name="materialname" />
                                        <ext:ModelField Name="itemname" />
                                        <ext:ModelField Name="username" />
                                        <ext:ModelField Name="EquipName"  />
                                         <ext:ModelField Name="PlanCount"  />
                                         <ext:ModelField Name="avgDoneAllRtime"  />
                                         <ext:ModelField Name="avgBwbTime"  />
                                         <ext:ModelField Name="sumRealweight"  />
                                       <ext:ModelField Name="LotTotalWeight"  />
                                         <ext:ModelField Name="BZTime"  />
                                           <ext:ModelField Name="JZTime"  />
                                            <ext:ModelField Name="RubberDE"  />
                                             <ext:ModelField Name="wcl"  />
                                              <ext:ModelField Name="DETime"  />
                                        <ext:ModelField Name="plandate"  />
                                        <ext:ModelField Name="shiftname"  />
                                        <ext:ModelField Name="sumber"  />
                                    </Fields>
                                </ext:Model>
                            </Model>
                           <%-- <Sorters>
                                <ext:DataSorter Property="ObjID" Direction="ASC" />
                            </Sorters>--%>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                    <ext:Column ID="plandate" runat="server" Text="计划日期" DataIndex="plandate" Width="100"  />
                     <ext:Column ID="shiftname" runat="server" Text="班次" DataIndex="shiftname" Width="100"  />
                            <ext:Column ID="equip_code" runat="server" Text="设备名称" DataIndex="EquipName" Width="100"  />
                            <ext:Column ID="state" runat="server" Text="配方名称" DataIndex="materialname" Width="80"  />
                            <ext:Column ID="equip_electric_current" runat="server" Text="配方类型" DataIndex="itemname" Width="80"  />
                            <ext:Column ID="Column3" runat="server" Text="主机手" DataIndex="username" Width="100"  />
                            <ext:Column ID="LockType" runat="server" Text="执行计划数" DataIndex="PlanCount" Width="80"  />
                               <ext:Column ID="Column4" runat="server" Text="单次标准时间" DataIndex="BZTime" Width="100"  />
                            <ext:Column ID="delete_flag" runat="server" Text="配方时间" DataIndex="avgDoneAllRtime" Width="80" />
                            <ext:Column ID="remark" runat="server" Text="间隔时间" DataIndex="avgBwbTime" Width="70"  />
                           <ext:Column ID="Column5" runat="server" Text="进胶时间" DataIndex="JZTime" Width="80"  />
                             <ext:Column ID="Column6" runat="server" Text="定额时间" DataIndex="DETime" Width="80"  />
                              <ext:Column ID="Column2" runat="server" Text="标准重量" DataIndex="LotTotalWeight" Width="80"  />
                                 <ext:Column ID="Column7" runat="server" Text="胶料定额" DataIndex="RubberDE" Width="120"  />
                                   <ext:Column ID="Column9" runat="server" Text="产出车数" DataIndex="sumber" Width="120"  />
                                 <ext:Column ID="Column1" runat="server" Text="实际完成总重" DataIndex="sumRealweight" Width="120"  />
                                  <ext:Column ID="Column8" runat="server" Text="定额完成率" DataIndex="wcl" Width="120"  />
                          
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
             
                <ext:Hidden runat="server" ID="hidden_workshop_code" />
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>