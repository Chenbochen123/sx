<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_XLjiaozhun, App_Web_vrwfi5uj" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小料校准</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
       <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color:red;
        }
    </style>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("Er") == "1") {
                return "x-grid-row-collapsed";
            }
        }

        var commandcolumn_direct_sendchkflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SetChkFlag = function () {

            Ext.Msg.confirm("提示", '确定要清空料仓吗？', function (btn) { commandcolumn_direct_sendchkflag(btn) });

        }

        var QueryEquipmentInfo = function (field, trigger, index) { //机台添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    //App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        //点击修改按钮


        var QueryEquipmentInfo1 = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }




        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })





        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台返回信息处理

            App.txtEquip.getTrigger(0).show();
            App.txtEquip.setValue(record.data.EquipName);
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            //App.pageToolBar.doRefresh();

        }




    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMminjar" runat="server" />
    <ext:Viewport ID="vpMminjar" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMminjarTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMminjar">
                        <Items>
                          
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                         
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                             <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipmentInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                         
                                         </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                                    <ext:DateField ID="txtBeginTime" runat="server"  FieldLabel="开始时间" LabelAlign="Right" />
                               
                                        </Items>
                                    </ext:Container>
                         <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                                          <ext:DateField ID="txtEndTime" runat="server"  FieldLabel="结束时间" LabelAlign="Right" />
                              
                                        </Items>
                                    </ext:Container>
                                </Items>
                                <Listeners>
                                    <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                </Listeners>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server" IDProperty="JarID">
                                <Fields>
                        
                                    <ext:ModelField Name="EquipCode" />
                                      <ext:ModelField Name="WareNum" />
                                       <ext:ModelField Name="SetWeight" />
                                 
                                    <ext:ModelField Name="ShiftName" />
                                    <ext:ModelField Name="RealWeight" />
                                            <ext:ModelField Name="EquipName" />
                                      <ext:ModelField Name="EquipName" />
                                      <ext:ModelField Name="ErrorAllow" />
                                    <ext:ModelField Name="SaveTime" />
                                      <ext:ModelField Name="Er" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                          <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Width="200"  />
                       <ext:Column ID="WareNum" runat="server" Text="工位" DataIndex="WareNum" />
                          <ext:Column ID="Column1" runat="server" Text="标准值(kg)" DataIndex="SetWeight" />
                         <ext:Column ID="RealWeight" runat="server" Text="实际校准值(kg)" DataIndex="RealWeight"  />
                         <ext:Column ID="Column2" runat="server" Text="允许误差(kg)" DataIndex="ErrorAllow"  />
                          <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName"
                             />
                      <ext:Column ID="Column3" runat="server" Text="保存时间" DataIndex="SaveTime"  Width="200" />
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                 <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
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
                                    <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
         
     
            <ext:Hidden ID="hiddenUserID" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server">
            </ext:Hidden>
     
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
