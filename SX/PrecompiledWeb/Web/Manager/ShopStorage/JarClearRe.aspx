<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_JarClearRe, App_Web_ampjtxsw" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划管理信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
   

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
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
            if (command.toLowerCase() == "recover") {
                Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
            }
            return false;
        };


        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

   

        //时间区间
        var onKeyUp = function () {
            var me = this,
                v = me.getValue(),
                field;

            if (me.startDateField) {
                field = Ext.getCmp(me.startDateField);
                field.setMaxValue(v);
                me.dateRangeMax = v;
            } else if (me.endDateField) {
                field = Ext.getCmp(me.endDateField);
                field.setMinValue(v);
                me.dateRangeMin = v;
            }
            field.validate();
        };
    </script>
    <script type="text/javascript">
        //-------物料代码-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料代码返回值处理
            App.txt_material_code.setValue(record.data.MaterialName);
            App.txt_material_code.getTrigger(0).show();
            App.hidden_material_code.setValue(record.data.MaterialCode);
        }

        var SelectMaterialInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_material_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料名称",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------机台代码-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
       
                App.txt_equip_code.setValue(record.data.EquipName);
                App.txt_equip_code.getTrigger(0).show();
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
           
        }

        var SelectEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    if (App.winAdd.hidden && App.winModify.hidden && App.winCreate.hidden) {
                        App.hidden_select_equip_code.setValue("");
                    }
                    else {
                        App.hidden_equip_code.setValue("");
                    }
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台名称",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
   
    <ext:ResourceManager ID="rmUnit" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
                        <Items>
                       
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <ToolTips>
                                    <ext:ToolTip ID="tt2" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                          
                             <ext:Button runat="server" Icon="Delete" Text="删除" ID="btn_all_delete" Hidden="true">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行删除" />
                                </ToolTips>
                                 <DirectEvents>
                                    <Click OnEvent="BtnAllDelete_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                           
                      
                       
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_5" />
                            <ext:Button runat="server" Icon="Accept" Text="恢复" ID="btn_createPlan">
                                <ToolTips>
                                    <ext:ToolTip ID="tt7" runat="server" Html="恢复" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnCreatePlanSave_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
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
                                            <ext:DateField ID="txt_start_plan_date" Format="yyyy-MM-dd" runat="server" Vtype="daterange"
                                                FieldLabel="计划开始日期" Editable="false" EnableKeyEvents="true">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="txt_end_plan_date" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>
                                            </ext:DateField>
                                            <ext:DateField ID="txt_end_plan_date" Format="yyyy-MM-dd" runat="server" Vtype="daterange"
                                                FieldLabel="计划结束日期" Editable="false" EnableKeyEvents="true">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="txt_start_plan_date" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="txtWorkShopCode" Editable="false" runat="server" FieldLabel="设备车间"
                                                LabelAlign="Right">
                                                <Listeners>
                                                    <Select Handler="#{cbo_equip_code}.clearValue(); #{equipCodeStore}.reload();" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <ext:ComboBox ID="cbo_equip_code" Editable="false" runat="server" FieldLabel="机台名称"
                                                LabelAlign="Right" TypeAhead="true" QueryMode="Local" ForceSelection="true" TriggerAction="All"
                                                DisplayField="name" ValueField="id">
                                                <Store>
                                                    <ext:Store runat="server" ID="equipCodeStore" AutoLoad="false" OnReadData="EquipCodeRefresh">
                                                        <Model>
                                                            <ext:Model ID="Model5" runat="server" IDProperty="Id">
                                                                <Fields>
                                                                    <ext:ModelField Name="id" Type="String" ServerMapping="Id" />
                                                                    <ext:ModelField Name="name" Type="String" ServerMapping="Name" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                        <Listeners>
                                                            <Load Handler="#{cbo_equip_code}.setValue(#{cbo_equip_code}.store.getAt(0).get('id'));" />
                                                        </Listeners>
                                                    </ext:Store>
                                                </Store>
                                            </ext:ComboBox>
                                            <%--<ext:TriggerField ID="txt_equip_code" runat="server" FieldLabel="机台名称" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>--%>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txt_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectMaterialInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
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
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="JarID" />
                                    <ext:ModelField Name="ClearTime" Type="Date"/>
                                    <ext:ModelField Name="Materbarcode" />
                                   <ext:ModelField Name="EquipCode" />
                                    <ext:ModelField Name="EquipName" />
                              <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="FeedJarNo" />
                                               <ext:ModelField Name="RWeight" />
                                  
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <SelectionModel>
                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Simple" />
                </SelectionModel>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="20" />
             
                        <ext:Column ID="Column3" runat="server" Text="投入id" DataIndex="JarID" Width="100" />
                        <ext:Column ID="Column4" runat="server" Text="条码号" DataIndex="Materbarcode" Width="80" />
                        <ext:Column ID="Column5" runat="server" Text="物料名称" DataIndex="MaterialName" Width="100" />
                       
                        <ext:Column ID="Column7" runat="server" Text="机台" DataIndex="EquipName" Width="100" />
                        <ext:Column ID="Column8" runat="server" Text="罐子号" DataIndex="FeedJarNo"
                            Width="60" />
                
                      <ext:Column ID="Column1" runat="server" Text="剩余重量" DataIndex="RWeight"
                            Width="60" />
                        <ext:DateColumn ID="Column18" runat="server" Text="清空时间" Format="yyyy-MM-dd hh:mm:ss"
                            DataIndex="ClearTime" Width="140" />
                      <%--  <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                    <ToolTip Text="删除本条数据" />
                                </ext:GridCommand>
                            </Commands>
                            <Listeners>
                                <Command Handler="return commandcolumn_click(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>--%>
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
        
         
        
            <ext:Hidden ID="hidden_equip_code" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_select_equip_code" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_code" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
