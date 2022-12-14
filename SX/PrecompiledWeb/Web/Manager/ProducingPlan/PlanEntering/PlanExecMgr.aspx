<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanEntering_PlanExecMgr, App_Web_qlhoypu3" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划管理信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //点击添加计划触发事件
        var btnAddClick = function (sender, e, fn) {
            var url = "ProducingPlan/PlanEntering/AddPlan.aspx";
            var tabid = "Manager_ProducingPlan_PlanEntering_AddPlan";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            parent.addTab(tabid, "添加计划", url, true);
        }
        //点击导入计划触发事件
        var btnImportClick = function (sender, e, fn) {
            var url = "ProducingPlan/PlanEntering/ImportPlan.aspx";
            var tabid = "Manager_ProducingPlan_PlanEntering_ImportPlan";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            parent.addTab(tabid, "导入生产计划", url, true);
        }
        //复制计划
        var btnCopyClick = function () {
            var Values = Ext.encode(App.pnlList.getRowsValues({ selectedOnly: "true" }));
            App.direct.BtnCopyPlan_Click(Values, {
                success: function (result) {
                    if (result != "") {
                        Ext.Msg.alert("操作",result);
                        return;
                    }
                    var url = "ProducingPlan/PlanEntering/AddPlan.aspx";
                    var tabid = "Manager_ProducingPlan_PlanEntering_AddPlan";
                    var tabp = parent.App.mainTabPanel;
                    var tab = tabp.getComponent("id=" + tabid);
                    if (tab) {
                        tab.close();
                    }
                    parent.addTab(tabid, "添加计划", url, true);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });


        }

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

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_delete(ObjID, {
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

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项格式为正整数"
        });


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //审核状态颜色改变        
        var template = '<span style="color:{0};font-weight:bold;">{1}</span>';
        var auditChange = function (value) {
            return Ext.String.format(template, (value != "未审核") ? "green" : "red", value);
        }
        var createPlanChange = function (value) {
            return Ext.String.format(template, (value != "未下达") ? "green" : "red", value);
        }

        var stateChange = function (value) {
            switch (value) {
                case "新计划":
                    return Ext.String.format(template, "black", value);
                    break;
                case "下达计划":
                    return Ext.String.format(template, "red", value);
                    break;
                case "已接受计划":
                    return Ext.String.format(template, "blue", value);
                    break;
                case "运行计划":
                    return Ext.String.format(template, "green", value);
                    break;
                case "完成计划":
                    return Ext.String.format(template, "purple", value);
                    break;
                default:
                    return Ext.String.format(template, "black", "");
            }
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
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MinMajorTypeID=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
            if (!App.winAdd.hidden) {
                App.add_equip_code.setValue(record.data.EquipName);
                App.hidden_equip_code.setValue(record.data.EquipCode);
                App.add_material_code.setValue('');
                App.add_recipe_name.setValue('');
                App.add_erp_code.setValue('');
                App.AddRecipeMaterialNameStore.reload();
            }
            else if (!App.winModify.hidden) {
                App.modify_equip_code.setValue(record.data.EquipName);
                App.hidden_equip_code.setValue(record.data.EquipCode);
                App.modify_material_code.setValue('');
                App.modify_recipe_name.setValue('');
                App.modify_erp_code.setValue('');
                App.ModifyRecipeMaterialNameStore.reload();
            }
            else if (!App.winCreate.hidden) {
                App.create_equip_code.setValue(record.data.EquipName);
                App.hidden_equip_code.setValue(record.data.EquipCode);
                App.create_material_code.setValue('');
                App.create_equip_code.getTrigger(0).show();
                App.CreateRecipeMaterialNameStore.reload();
            }
            else {
                App.txt_equip_code.setValue(record.data.EquipName);
                App.txt_equip_code.getTrigger(0).show();
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
            }
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
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmUnit" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                <ToolTips>
                                    <ext:ToolTip ID="tt1" runat="server" Html="点击进行添加" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="btnAddClick">
                                    </Click>
                                </Listeners>
                                <%--<DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                        </Click>
                                    </DirectEvents>--%>
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
                             <ext:Button runat="server" Icon="Delete" Text="删除" ID="btn_all_delete">
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
                            <ext:ToolbarSeparator ID="toolbarSeparator2" />
                            <ext:Button runat="server" Icon="ArrowDown" Text="审核" ID="btn_audit">
                                <ToolTips>
                                    <ext:ToolTip ID="tt3" runat="server" Html="点击进行审核" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnAuditPlan_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                            <ext:Button runat="server" Icon="ArrowUp" Text="反审核" ID="btn_unAudit">
                                <ToolTips>
                                    <ext:ToolTip ID="tt4" runat="server" Html="点击进行反审核" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnUnAuditPlan_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_3" />
                            <ext:Button runat="server" Icon="Disk" Text="复制计划" ID="btn_copyPlan">
                                <ToolTips>
                                    <ext:ToolTip ID="tt5" runat="server" Html="点击进行复制计划" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="btnCopyClick">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator1" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导入计划" ID="btnImport">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Html="点击将进入导入生产计划功能" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="btnImportClick">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_4" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出计划" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="tt6" runat="server" Html="点击将查询结果导出到Excel中" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_5" />
                            <ext:Button runat="server" Icon="Accept" Text="下达计划" ID="btn_createPlan">
                                <ToolTips>
                                    <ext:ToolTip ID="tt7" runat="server" Html="点击进行下达计划" />
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
                                    <ext:ModelField Name="ObjID" />
                                    <ext:ModelField Name="PlanDate" Type="Date"/>
                                    <ext:ModelField Name="EquipCode" />
                                    <ext:ModelField Name="ERPCode" />
                                    <ext:ModelField Name="MaterialCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="RecipeVersionID" />
                                    <ext:ModelField Name="RecipeName" />
                                    <ext:ModelField Name="RecommendTotalCount" />
                                    <ext:ModelField Name="PlanTotalCount" />
                                    <ext:ModelField Name="ActualOnePlan" />
                                    <ext:ModelField Name="ActualOneProNum" />
                                    <ext:ModelField Name="ActualOnePlanID" />
                                    <ext:ModelField Name="ActualOneRemark" />
                                    <ext:ModelField Name="ActualTwoPlan" />
                                    <ext:ModelField Name="ActualTwoProNum" />
                                    <ext:ModelField Name="ActualTwoPlanID" />
                                    <ext:ModelField Name="ActualTwoRemark" />
                                    <ext:ModelField Name="ActualThreePlan" />
                                    <ext:ModelField Name="ActualThreeProNum" />
                                    <ext:ModelField Name="ActualThreePlanID" />
                                    <ext:ModelField Name="ActualThreeRemark" />
                                    <ext:ModelField Name="ExecSheet" />
                                    <ext:ModelField Name="ExecSheetDate" />
                                    <ext:ModelField Name="Auditor" />
                                    <ext:ModelField Name="AuditDate" Type="Date"/>
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
                            Width="80" />
                        <ext:Column ID="Column1" runat="server" Text="审核状态" DataIndex="AuditFlag" Width="60">
                            <Renderer Fn="auditChange" />
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="下达状态" DataIndex="CreatePlanFlag" Width="60">
                            <Renderer Fn="createPlanChange" />
                        </ext:Column>
                        <ext:Column ID="Column3" runat="server" Text="机台名称" DataIndex="EquipCode" Width="100" />
                        <ext:Column ID="Column4" runat="server" Text="ERP编号" DataIndex="ERPCode" Width="80" />
                        <ext:Column ID="Column5" runat="server" Text="物料名称" DataIndex="MaterialCode" Width="100" />
                        <ext:Column ID="Column6" runat="server" Text="配方名称" DataIndex="RecipeName" Width="100" />
                        <ext:Column ID="Column7" runat="server" Text="计划量" DataIndex="PlanTotalCount" Width="60" />
                        <ext:Column ID="Column8" runat="server" Text="推荐量" DataIndex="RecommendTotalCount"
                            Width="60" />
                        <ext:Column ID="Column9" runat="server" Text="一班计划" DataIndex="ActualOnePlan" Width="70" />
                        <ext:Column ID="Column10" runat="server" Text="一班备注" DataIndex="ActualOneRemark"
                            Width="70" />
                        <ext:Column ID="Column15" runat="server" Text="一班状态" DataIndex="ActualOnePlanID" Width="70" >
                            <Renderer Fn="stateChange" />
                        </ext:Column>
                        <ext:Column ID="Column11" runat="server" Text="二班计划" DataIndex="ActualTwoPlan" Width="70" />
                        <ext:Column ID="Column12" runat="server" Text="二班备注" DataIndex="ActualTwoRemark"
                            Width="70" />
                        <ext:Column ID="Column16" runat="server" Text="二班状态" DataIndex="ActualTwoPlanID" Width="70" >
                            <Renderer Fn="stateChange" />
                        </ext:Column>
                        <ext:Column ID="Column13" runat="server" Text="三班计划" DataIndex="ActualThreePlan"
                            Width="70" />
                        <ext:Column ID="Column14" runat="server" Text="三班备注" DataIndex="ActualThreeRemark"
                            Width="70" />
                        <ext:Column ID="Column19" runat="server" Text="三班状态" DataIndex="ActualThreePlanID" Width="70" >
                            <Renderer Fn="stateChange" />
                        </ext:Column>
                        <ext:Column ID="Column17" runat="server" Text="审核人" DataIndex="Auditor" Width="60" />
                        <ext:DateColumn ID="Column18" runat="server" Text="审核时间" Format="yyyy-MM-dd hh:mm:ss"
                            DataIndex="AuditDate" Width="140" />
                        <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
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
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改单位信息"
                Width="530" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:FieldSet ID="FieldSet2" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Pmodifying="5">
                                <Items>
                                    <ext:Container ID="Container7" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="自增ID" Hidden="true" />
                                            <ext:DateField ID="modify_plan_date" Editable="false" runat="server" FieldLabel="计划日期"
                                                LabelAlign="Right" Flex="1" />
                                            <ext:TriggerField ID="modify_equip_code" runat="server" FieldLabel="机台名称" LabelAlign="Right"
                                                Flex="1" Enabled="true" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:ComboBox ID="modify_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                MinChars="1" IndicatorText="*" IndicatorCls="red-text" TypeAhead="false" Flex="1"
                                                ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName">
                                                <Store>
                                                    <ext:Store ID="ModifyRecipeMaterialNameStore" runat="server" OnReadData="ModifyRecipeMaterialNameStoreRefresh">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model1" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); App.modify_material_code.setValue('');App.modify_recipe_name.setValue('');App.modify_erp_code.setValue('');this.getTrigger(0).hide(); App.ModifyRecipeMaterialNameStore.reload();}" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Change OnEvent="FillModifyRecipeComboBox">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="shiftFlag" Value="1">
                                                            </ext:Parameter>
                                                        </ExtraParams>
                                                    </Change>
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:ComboBox ID="modify_recipe_name" runat="server" FieldLabel="配方名称" LabelAlign="Right"
                                                Editable="false" IndicatorText="*" IndicatorCls="red-text" Flex="1" ValueField="RecipeName"
                                                DisplayField="RecipeName">
                                                <Store>
                                                    <ext:Store ID="ModifyRecipeNameStore" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model2" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TextField ID="modify_erp_code" runat="server" FieldLabel="ERP编号" LabelAlign="Right"
                                                Width="231" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet ID="FieldSet4" runat="server" Title="计划信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Pmodifying="5">
                                <Items>
                                    <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modify_plan_total_count" runat="server" Disabled="true" FieldLabel="计划量"
                                                LabelAlign="Right" Flex="1" />
                                            <ext:NumberField ID="modify_recommend_total_count" runat="server" FieldLabel="推荐量"
                                                LabelAlign="Right" Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modify_actual_one_plan" runat="server" FieldLabel="一班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="modify_actual_one_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modify_actual_two_plan" runat="server" FieldLabel="二班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="modify_actual_two_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modify_actual_three_plan" runat="server" FieldLabel="三班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="modify_actual_three_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnModifySave_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加计划信息"
                Width="530" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:FieldSet ID="FieldSet3" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Pmodifying="5">
                                <Items>
                                    <ext:Container ID="Container29" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:DateField ID="add_plan_date" Editable="false" runat="server" FieldLabel="计划日期"
                                                LabelAlign="Right" Flex="1" />
                                            <ext:TriggerField ID="add_equip_code" runat="server" FieldLabel="机台名称" LabelAlign="Right"
                                                Flex="1" Enabled="true" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:ComboBox ID="add_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                MinChars="1" IndicatorText="*" IndicatorCls="red-text" Flex="1" TypeAhead="true"
                                                ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName">
                                                <Store>
                                                    <ext:Store ID="AddRecipeMaterialNameStore" runat="server" OnReadData="AddRecipeMaterialNameStoreRefresh">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model8" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); App.add_material_code.setValue('');App.add_recipe_name.setValue('');App.add_erp_code.setValue('');this.getTrigger(0).hide(); App.AddRecipeMaterialNameStore.reload();}" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Change OnEvent="FillAddRecipeComboBox">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="shiftFlag" Value="1">
                                                            </ext:Parameter>
                                                        </ExtraParams>
                                                    </Change>
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:ComboBox ID="add_recipe_name" runat="server" FieldLabel="配方名称" LabelAlign="Right"
                                                Editable="false" IndicatorText="*" IndicatorCls="red-text" Flex="1" ValueField="RecipeName"
                                                DisplayField="RecipeName">
                                                <Store>
                                                    <ext:Store ID="AddRecipeNameStore" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model7" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TextField ID="add_erp_code" runat="server" FieldLabel="ERP编号" LabelAlign="Right"
                                                Width="231" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet ID="FieldSet1" runat="server" Title="计划信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Pmodifying="5">
                                <Items>
                                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="add_plan_total_count" runat="server" Disabled="true" FieldLabel="计划量"
                                                LabelAlign="Right" Flex="1" />
                                            <ext:NumberField ID="add_recommend_total_count" runat="server" FieldLabel="推荐量" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="add_actual_one_plan" runat="server" FieldLabel="一班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="add_actual_one_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="add_actual_two_plan" runat="server" FieldLabel="二班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="add_actual_two_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="add_actual_three_plan" runat="server" FieldLabel="三班" LabelAlign="Right"
                                                Flex="1" />
                                            <ext:TextField ID="add_actual_three_remark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                                Flex="1" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnAddSave_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="winCreate" runat="server" Icon="MonitorAdd" Closable="false" Title="下达计划信息"
                Width="530" Height="200" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:FieldSet ID="FieldSet5" runat="server" Title="下达基本信息" Layout="AnchorLayout"
                                DefaultAnchor="100%" Pmodifying="5">
                                <Items>
                                    <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:DateField ID="create_plan_date" Editable="false" runat="server" FieldLabel="计划日期"
                                                LabelAlign="Right" Flex="1" />
                                            <ext:TriggerField ID="create_equip_code" runat="server" FieldLabel="机台名称" LabelAlign="Right"
                                                Flex="1" Enabled="true" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container15" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:ComboBox ID="create_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                MinChars="1" IndicatorText="*" IndicatorCls="red-text" Flex="1" TypeAhead="true"
                                                ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName">
                                                <Store>
                                                    <ext:Store ID="CreateRecipeMaterialNameStore" runat="server" OnReadData="CreateRecipeMaterialNameStoreRefresh">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model3" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); App.create_material_code.setValue('');App.create_recipe_name.setValue('');this.getTrigger(0).hide(); App.CreateRecipeMaterialNameStore.reload();}" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Change OnEvent="FillCreateRecipeComboBox">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="shiftFlag" Value="1">
                                                            </ext:Parameter>
                                                        </ExtraParams>
                                                    </Change>
                                                </DirectEvents>
                                            </ext:ComboBox>
                                            <ext:ComboBox ID="create_recipe_name" runat="server" FieldLabel="配方名称" LabelAlign="Right"
                                                Editable="false" IndicatorText="*" IndicatorCls="red-text" Flex="1" ValueField="RecipeName"
                                                DisplayField="RecipeName">
                                                <Store>
                                                    <ext:Store ID="CreateRecipeNameStore" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model4" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="确认下达计划" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnCreatePlanSave_Click">
                                <EventMask ShowMask="true" Msg="Createing..." MinDelay="50" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button2" runat="server" Text="取消" Icon="Cancel">
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
