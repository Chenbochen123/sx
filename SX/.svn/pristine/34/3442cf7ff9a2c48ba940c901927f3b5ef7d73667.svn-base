<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPlan.aspx.cs" Inherits="Manager_ProducingPlan_PlanEntering_AddPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加计划</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var Values = Ext.encode(App.pnlList.getRowsValues({ selectedOnly: "true" }));
            App.direct.BtnDeletePlan_Click(Values, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click = function (command, record) {
            var Values = Ext.encode(App.pnlList.getRowsValues({ selectedOnly: "true" }));
            if (Values == '[]') {
                Ext.Msg.alert("提示", '请选择需要删除的计划信息！');
            }
            else {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };



        //-------物料代码-----查询带回弹出框--BEGIN

        //------------查询带回弹出框--END
        var SelectEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_equip_code.setValue("");
                    App.hidden_equip_code.setValue("");
                    App.hidden_material_code.setValue("")
                    App.hiddenflag.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    if (field.id == "UpEquipCodeName") {
                        App.hiddenflag.setValue("Equip");
                    }
                    else {
                        App.hiddenflag.setValue("");
                    }
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

        //-------机台代码-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            var type = App.hiddenflag.getValue();
            if (type == "Equip") {
                App.UpEquipCodeName.getTrigger(0).show();
                App.UpEquipCodeName.setValue(record.data.EquipName);
                App.UpEquipCode.setValue(record.data.EquipCode);
            }
            else {
                App.txt_equip_code.setValue(record.data.EquipName);
                App.txt_equip_code.getTrigger(0).show();
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
            }
        }
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.direct.LoadGridData({
                success: function (result) {
                },
                failure: function (errorMsg) {
                }
            });
        }

        //编辑之前相应
        var beforeEdit = function (ed, e) {
            var field = this.getEditor(e.record, e.column).field;
            switch (e.field) {

                case "MaterialName":
                    field.allQuery = e.record.get('EquipCode');
                    break;
                case "RecipeName":
                    field.allQuery = e.record.get('MaterialCode') + "_" + e.record.get('EquipCode');
                    break;
                case "EquipName":
                    field.allQuery = e.record.get('EquipCode');
                    break;
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmUnit" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加行" ID="btn_add">
                                <ToolTips>
                                    <ext:ToolTip ID="tt1" runat="server" Html="点击进行添加" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_reload">
                                <ToolTips>
                                    <ext:ToolTip ID="tt2" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
                            <ext:Button runat="server" Icon="Accept" Text="批量提交" ID="btn_audit">
                                <ToolTips>
                                    <ext:ToolTip ID="tt3" runat="server" Html="点击进行批量提交" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnSummitAuditPlan_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator2" />
                            <ext:Button runat="server" Icon="Delete" Text="删除" ID="Button2">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行批量删除" />
                                </ToolTips>
                                <Listeners>
                                <Click Fn="commandcolumn_click"></Click>
                                </Listeners>
                            <%--    <DirectEvents>
                                    <Click OnEvent="BtnDeletePlan_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>--%>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator1" />
                            <ext:Button runat="server" Icon="ApplicationEdit" Text="修改计划日期" ID="Button1">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行批量修改计划日期" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnUpdatePlanDate_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
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
                                                FieldLabel="计划日期" Editable="false" EnableKeyEvents="true">
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
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
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
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                <Plugins>
                    <ext:CellEditing ClicksToEdit="2">
                        <Listeners>
                            <BeforeEdit Fn="beforeEdit" />
                        </Listeners>
                        <DirectEvents>
                            <Edit OnEvent="Edit">
                                <EventMask ShowMask="true" CustomTarget="#{store}" />
                                <ExtraParams>
                                    <ext:Parameter Name="field" Value="e.field" Mode="Raw" />
                                    <ext:Parameter Name="index" Value="e.rowIdx" Mode="Raw" />
                                    <ext:Parameter Name="record" Value="e.record.data" Mode="Raw" Encode="true" />
                                </ExtraParams>
                            </Edit>
                        </DirectEvents>
                    </ext:CellEditing>
                </Plugins>
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="ObjID" />
                                    <ext:ModelField Name="PlanDate" Type="Date" />
                                    <ext:ModelField Name="EquipCode" />
                                    <ext:ModelField Name="EquipName" />
                                    <ext:ModelField Name="ERPCode" />
                                    <ext:ModelField Name="MaterialCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="RecipeVersionID" />
                                    <ext:ModelField Name="RecipeName" />
                                    <ext:ModelField Name="RecommendTotalCount" />
                                    <ext:ModelField Name="PlanTotalCount" />
                                    <ext:ModelField Name="ActualOnePlan" />
                                    <ext:ModelField Name="ActualOneProNum" />
                                    <ext:ModelField Name="ActualOneRemark" />
                                    <ext:ModelField Name="ActualTwoPlan" />
                                    <ext:ModelField Name="ActualTwoProNum" />
                                    <ext:ModelField Name="ActualTwoRemark" />
                                    <ext:ModelField Name="ActualThreePlan" />
                                    <ext:ModelField Name="ActualThreeProNum" />
                                    <ext:ModelField Name="ActualThreeRemark" />
                                    <ext:ModelField Name="ExecSheet" />
                                    <ext:ModelField Name="ExecSheetDate" />
                                    <ext:ModelField Name="Auditor" />
                                    <ext:ModelField Name="AuditDate" />
                                    <ext:ModelField Name="AuditFlag" />
                                    <ext:ModelField Name="CreatePlanFlag" />
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
                        <ext:DateColumn ID="Column0" runat="server" Text="计划日期" Format="yyyy-MM-dd" DataIndex="PlanDate"
                            Width="100" />
                        <ext:Column ID="Column3" runat="server" Text="机台名称" DataIndex="EquipName" Width="120">
                            <%-- <Editor>
                                <ext:TriggerField ID="UpEquipCodeName" runat="server" LabelAlign="Right" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="SelectEquipInfo" />
                                    </Listeners>
                                </ext:TriggerField>
                            </Editor>--%>
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="物料名称" DataIndex="MaterialName" Width="150">
                            <Editor>
                                <ext:ComboBox ID="cboRecipeMaterialName" ValueField="RecipeMaterialName" MinChars="1"
                                    DisplayField="RecipeMaterialName" runat="server" TypeAhead="false">
                                    <Store>
                                        <ext:Store ID="RecipeMaterialNameStore" runat="server" OnReadData="RecipeMaterialNameStoreRefresh">
                                            <Model>
                                                <ext:Model ID="Model10" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Proxy>
                                                <ext:PageProxy>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:PageProxy>
                                            </Proxy>
                                        </ext:Store>
                                    </Store>
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                    </CustomConfig>
                                </ext:ComboBox>
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="Column6" runat="server" Text="配方编号" DataIndex="RecipeName" Width="150">
                            <Editor>
                                <ext:ComboBox ID="StateCombo" runat="server" ValueField="RecipeName" Editable="false"
                                    DisplayField="RecipeName">
                                    <Store>
                                        <ext:Store ID="RecipeNameStore" runat="server" OnReadData="RecipeNameStoreRefresh">
                                            <Proxy>
                                                <ext:PageProxy />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model6" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                    </CustomConfig>
                                </ext:ComboBox>
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="ERP编号" DataIndex="ERPCode" Width="80" />
                        <ext:Column ID="Column7" runat="server" Text="计划量" DataIndex="PlanTotalCount" Width="60" />
                        <ext:Column ID="Column8" runat="server" Text="推荐量" DataIndex="RecommendTotalCount"
                            Width="60" />
                        <ext:Column ID="Column9" runat="server" Text="一班计划" DataIndex="ActualOnePlan" Width="70">
                            <Editor>
                                <ext:NumberField ID="NumberField1" runat="server" MinValue="0" />
                            </Editor>
                            <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                        </ext:Column>
                        <ext:Column ID="Column10" runat="server" Text="一班备注" DataIndex="ActualOneRemark"
                            Width="70">
                            <Editor>
                                <ext:TextField ID="txtActualOneRemark" runat="server">
                                </ext:TextField>
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="Column11" runat="server" Text="二班计划" DataIndex="ActualTwoPlan" Width="70">
                            <Editor>
                                <ext:NumberField ID="NumberField2" runat="server" MinValue="0" />
                            </Editor>
                            <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                        </ext:Column>
                        <ext:Column ID="Column12" runat="server" Text="二班备注" DataIndex="ActualTwoRemark"
                            Width="70">
                            <Editor>
                                <ext:TextField ID="txtActualTwoRemark" runat="server">
                                </ext:TextField>
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="Column13" runat="server" Text="三班计划" DataIndex="ActualThreePlan"
                            Width="70">
                            <Editor>
                                <ext:NumberField ID="NumberField3" runat="server" MinValue="0" />
                            </Editor>
                            <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                        </ext:Column>
                        <ext:Column ID="Column14" runat="server" Text="三班备注" DataIndex="ActualThreeRemark"
                            Width="70">
                            <Editor>
                                <ext:TextField ID="txtActualThreeRemark" runat="server">
                                </ext:TextField>
                            </Editor>
                        </ext:Column>
                       <%-- <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="提交">
                                    <ToolTip Text="提交去审核" />
                                </ext:GridCommand>
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
            <ext:Hidden ID="hiddenflag" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="UpEquipCode" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    <%--<ext:KeyMap runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
        <Binding>
            <ext:KeyBinding Handler="App.txt_start_plan_date.setValue('2011-11-11');">
                <Keys>
                    <ext:Key Code="A" />
                </Keys>
            </ext:KeyBinding>
        </Binding>
    </ext:KeyMap>--%>
    </form>
</body>
</html>
