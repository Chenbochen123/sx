<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialInfo.aspx.cs" Inherits="Manager_BasicInfo_MaterialInfo_MaterialInfo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物料基础信息</title>
     <%--   <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />--%>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/extExtra.css" />
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources\css\font-awesome\css\font-awesome.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        function myBrowser() {
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
            var isOpera = userAgent.indexOf("Opera") > -1;

            if (isOpera) { return "Opera" }; //判断是否Opera浏览器
            if (userAgent.indexOf("Firefox") > -1) { return "FF"; } //判断是否Firefox浏览器
            if (userAgent.indexOf("Safari") > -1) { return "Safari"; } //判断是否Safari浏览器
            if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) { return "IE"; }; //判断是否IE浏览器
        }
        //-------单位-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryUnit_Request = function (record) {//单位返回值处理
            var type = $("#hidden_select_unit").val();
            if (type == "Unit") {
                if (!App.winAdd.hidden) {
                    App.add_unit_id.setValue(record.data.UnitName);
                    App.hidden_unit_id.setValue(record.data.ObjID);
                }
                else if (!App.winModify.hidden) {
                    App.modify_unit_id.setValue(record.data.UnitName);
                    App.hidden_unit_id.setValue(record.data.ObjID);
                }
            }
            if (type == "StaticUnit") {
                if (!App.winAdd.hidden) {
                    App.add_static_unit_id.setValue(record.data.UnitName);
                    App.hidden_static_unit_id.setValue(record.data.ObjID);
                }
                else if (!App.winModify.hidden) {
                    App.modify_static_unit_id.setValue(record.data.UnitName);
                    App.hidden_static_unit_id.setValue(record.data.ObjID);
                }
            }
        }

        var SelectUnitID = function () {
            App.hidden_select_unit.setValue("Unit");
            App.Manager_BasicInfo_CommonPage_QueryUnit_Window.show();
        }
        var SelectStaticUnitID = function () {
            App.hidden_select_unit.setValue("StaticUnit");
            App.Manager_BasicInfo_CommonPage_QueryUnit_Window.show();
        }

        Ext.create("Ext.window.Window", {//厂商类别查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryUnit_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryUnit.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择单位",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------胶料代码-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryRubberInfo_Request = function (record) {//胶料代码返回值处理
            if (!App.winAdd.hidden) {
                App.add_rub_code.setValue(record.data.RubName);
                App.hidden_rub_code.setValue(record.data.RubCode);
                App.hidden_material_part3.setValue(record.data.RubName);
//                if (App.hidden_major_type_id.getValue() != 2) {
//                    App.add_material_name.setValue(App.hidden_material_part3.getValue() +
//                        App.hidden_material_part1.getValue());
//                }
            }
            else if (!App.winModify.hidden) {
                App.modify_rub_code.setValue(record.data.RubName);
                App.hidden_rub_code.setValue(record.data.RubCode);
                App.hidden_material_part3.setValue(record.data.RubName);
//                if (App.hidden_major_type_id.getValue() != 2) {
//                    App.modify_material_name.setValue(App.hidden_material_part3.getValue() +
//                    App.hidden_material_part1.getValue());
//                }
            }
        }

        var SelectRubberInfo = function () {
            App.Manager_BasicInfo_CommonPage_QueryRubberInfo_Window.show();
        }

        Ext.create("Ext.window.Window", {//胶料代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryRubberInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberInfo.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料名称",
            layout: 'fit',
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------统计分类-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryMaterialStaticClass_Request = function (record) {//统计分类返回值处理
            if (!App.winAdd.hidden) {
                App.add_static_class.setValue(record.data.StaticClassName);
                App.hidden_static_class.setValue(record.data.ObjID);
            }
            else if (!App.winModify.hidden) {
                App.modify_static_class.setValue(record.data.StaticClassName);
                App.hidden_static_class.setValue(record.data.ObjID);
            }
        }

        var SelectMaterialStaticClass = function () {
            App.Manager_BasicInfo_CommonPage_QueryMaterialStaticClass_Window.show();
        }

        Ext.create("Ext.window.Window", {//统计分类查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterialStaticClass_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterialStaticClass.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择统计分类",
            layout: 'fit',
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------胶料段数-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryMaterialRubSect_Request = function (record) {//胶料段数返回值处理
            if (!App.winAdd.hidden) {
                App.add_rub_sect.setValue(record.data.SectName);
                App.hidden_rub_sect.setValue(record.data.SectCode);
            }
            else if (!App.winModify.hidden) {
                App.modify_rub_sect.setValue(record.data.SectName);
                App.hidden_rub_sect.setValue(record.data.SectCode);
            }
        }

        var SelectRubSect = function () {
            App.Manager_BasicInfo_CommonPage_QueryMaterialRubSect_Window.show();
        }

        Ext.create("Ext.window.Window", {//胶料段数查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterialRubSect_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterialRubSect.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料段数",
            layout: 'fit',
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var MaterialCode = record.data.MaterialCode;
            var browser = myBrowser();
            App.direct.commandcolumn_direct_edit(MaterialCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_recover(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
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
            var MaterialCode = record.data.MaterialCode;
            App.direct.commandcolumn_direct_delete(MaterialCode, {
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
            if (command.toLowerCase() == "reject") {
                record.reject();
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
                    return true;
                }
                try {
                    if (/^[\d]+\.?[\d]*$/.test(val)) {
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
            integerText: "此填入项为数字格式！"

        });


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>
    <script>
        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            if (record.getDepth() == 2) {
                App.hidden_major_type_id.setValue(record.getId().split('|')[0]);
                App.hidden_minor_type_id.setValue(record.getId().split('|')[1]);
            }
            if (record.getDepth() == 1) {
                App.hidden_major_type_id.setValue(record.getId());
                App.hidden_minor_type_id.setValue("");
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_major_type_id.setValue("");
            App.hidden_minor_type_id.setValue("");
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
            App.hidden_major_type_id.setValue("");
            App.hidden_minor_type_id.setValue("");
            App.hidden_delete_flag.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DeleteFlag") == "1") {
                return "x-grid-row-deleted";
            }
        }
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("DeleteFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            } else {
                toolbar.items.getAt(4).hide();
            }
        };

        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            App.direct.MaterialGroupData_DbClick(record.data.MaterialName, record.data.MaterialCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var edit2 = function (editor, e) {
            if (e.value > 10) {
                Ext.Msg.alert("提示", "您设置的等级超出最大等级，请重新设置！");
                e.record.reject();
            }
        };


        var setStockCondition = function (cb, tf) {
            var flag = cb.getValue();
            if (flag == true) {
                tf.setDisabled(false);
            } else {
                tf.setDisabled(true);
            }
        };
    </script>
</head>
<body>
    <form id="fmRubber" runat="server">
        <asp:Button ID="Button2" Style="display:none"  runat="server" Text="Button" OnClientClick="pnlListFresh" />
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmRubber" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="200" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeDept" runat="server" Title="物料分类" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
                        <Store>
                            <ext:TreeStore ID="treeDeptStore" runat="server">
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
            <ext:Panel ID="pnlRubberTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barRubber">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                            <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            <ext:ToolbarFill ID="toolbarFill_end" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlRubberQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_material_code" Vtype="integer" runat="server" FieldLabel="物料代码"
                                                LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_material_name" runat="server" FieldLabel="物料名称" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_remark" runat="server" FieldLabel="备注" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                       <ext:Container ID="container33" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="TextERP" runat="server" FieldLabel="ERP代码" LabelAlign="Right" />
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
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="ObjID" />
                                    <ext:ModelField Name="MaterialCode" />
                                    <ext:ModelField Name="MajorTypeID" />
                                    <ext:ModelField Name="MinorTypeID" />
                                    <ext:ModelField Name="RubCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="MaterialOtherName" />
                                    <ext:ModelField Name="MaterialSimpleName" />
                                    <ext:ModelField Name="MaterialLevel" />
                                    <ext:ModelField Name="MaterialGroup" />
                                    <ext:ModelField Name="MaterialGroupID" />
                                     <ext:ModelField Name="CMaterialLevel" />
                                    <ext:ModelField Name="CMaterialGroup" />
                                    <ext:ModelField Name="CMaterialGroupID" />
                                    <ext:ModelField Name="UserCode" />
                                    <ext:ModelField Name="ValidDate" />
                                    <ext:ModelField Name="ProductArea" />
                                    <ext:ModelField Name="MinStock" />
                                    <ext:ModelField Name="MaxStock" />
                                    <ext:ModelField Name="UnitID" />
                                    <ext:ModelField Name="StaticUnitID" />
                                    <ext:ModelField Name="StaticUnitCoefficient" />
                                    <ext:ModelField Name="CheckPermitError" />
                                    <ext:ModelField Name="MaxParkTime" />
                                    <ext:ModelField Name="MinParkTime" />
                                    <ext:ModelField Name="DefineDate" Type="Date" />
                                    <ext:ModelField Name="StandardCode" />
                                    <ext:ModelField Name="ProductMaterialCode" />
                                    <ext:ModelField Name="StaticClass" />
                                    <ext:ModelField Name="IsEqualMaterial" />
                                    <ext:ModelField Name="IsPutJar" />
                                    <ext:ModelField Name="IsQualityRateCount" />
                                    <ext:ModelField Name="ERPCode" />
                                    <ext:ModelField Name="DeleteFlag" />
                                    <ext:ModelField Name="Remark" />
                                    <ext:ModelField Name="PlanPrice" />
                                    <ext:ModelField Name="PDM_Code" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="45" />
                        <ext:Column ID="obj_id" runat="server" Text="物料编码" DataIndex="ObjID" Width="80" Visible="false" />
                        <ext:Column ID="material_code" runat="server" Text="物料代码" DataIndex="MaterialCode"
                            Width="120" />
                        <ext:Column ID="material_name" runat="server" Text="物料名称" DataIndex="MaterialName"
                            Width="150" />
                        <ext:Column ID="material_other_name" runat="server" Text="物料别名" DataIndex="MaterialOtherName"
                            Width="150" />
                        <ext:Column ID="material_simple_name" runat="server" Text="物料简称" DataIndex="MaterialSimpleName"
                            Width="80" />
                        <ext:Column ID="major_type_id" runat="server" Text="物料大类" DataIndex="MajorTypeID"
                            Width="80" />
                        <ext:Column ID="minor_type_id" runat="server" Text="物料细类" DataIndex="MinorTypeID"
                            Width="80" />
<%--                        <ext:Column ID="material_level" runat="server" Text="物料等级" DataIndex="MaterialLevel"
                            Width="80" />--%>
<%--                        <ext:Column ID="material_group" runat="server" Text="物料分组" DataIndex="MaterialGroup"
                            Width="80" />
                           <ext:Column ID="Column8" runat="server" Text="物料分组ID" DataIndex="MaterialGroupID"
                            Width="80" />
                            <ext:Column ID="Column9" runat="server" Text="掺用等级" DataIndex="CMaterialLevel"
                            Width="80" />
                        <ext:Column ID="Column10" runat="server" Text="掺用分组" DataIndex="CMaterialGroup"
                            Width="80" />
                           <ext:Column ID="Column11" runat="server" Text="掺用分组ID" DataIndex="CMaterialGroupID"
                            Width="80" />--%>
                        <ext:Column ID="rub_code" runat="server" Text="胶料名称" DataIndex="RubCode" Width="80" />
                        <ext:Column ID="Column8" runat="server" Text="单价" DataIndex="PlanPrice" Width="160" />
                        <ext:Column ID="Column9" runat="server" Text="单重" DataIndex="StaticUnitCoefficient" Width="160" />
                        <ext:Column ID="user_code" runat="server" Text="K3编码" DataIndex="UserCode" Width="160" />
                       <%-- <ext:Column ID="valid_date" runat="server" Text="保质期" DataIndex="ValidDate" Width="80" />--%>
                        <ext:Column ID="min_stock" runat="server" Text="最小库存" DataIndex="MinStock" Width="80" />
                        <ext:Column ID="max_stock" runat="server" Text="最大库存" DataIndex="MaxStock" Width="80" />
                <%--        <ext:Column ID="unit_id" runat="server" Text="单位名称" DataIndex="UnitID" Width="80" />
                        <ext:Column ID="static_unit_id" runat="server" Text="统计单位" DataIndex="StaticUnitID"
                            Width="80" />--%>
            <%--            <ext:Column ID="static_unit_coefficient" runat="server" Text="统计单位转换系数" DataIndex="StaticUnitCoefficient"
                            Width="80" />--%>
                    <%--    <ext:Column ID="check_permit_error" runat="server" Text="盘点允许误差" DataIndex="CheckPermitError"
                            Width="80" />--%>
                        <ext:Column ID="max_park_time" runat="server" Text="最大停放时间" DataIndex="MaxParkTime"
                            Width="80" />
                        <ext:Column ID="min_park_time" runat="server" Text="最小停放时间" DataIndex="MinParkTime"
                            Width="80" />
                        <ext:DateColumn ID="define_date" runat="server" Text="定义日期" Format="yyyy-MM-dd" DataIndex="DefineDate"
                            Width="80" />
                      <%--  <ext:Column ID="standard_code" runat="server" Text="标准码" DataIndex="StandardCode"
                            Width="80" />--%>
                        <ext:Column ID="erp_code" runat="server" Text="ERP代码" DataIndex="ERPCode" Width="80" />
                        <ext:Column ID="PDM_Code" runat="server" Text="PDM代码" DataIndex="PDM_Code" Width="80" />
                       <%-- <ext:Column ID="product_material_code" runat="server" Text="生产物料" DataIndex="ProductMaterialCode"
                            Width="80" />--%>
<%--                        <ext:Column ID="static_class" runat="server" Text="统计分类" DataIndex="StaticClass"
                            Width="80" />
                        <ext:Column ID="is_equal_material" runat="server" Text="是否有等同物料" DataIndex="IsEqualMaterial"
                            Width="80" />--%>
                       <%-- <ext:Column ID="is_put_jar" runat="server" Text="是否投罐" DataIndex="IsPutJar" Width="80" />--%>
 <%--                       <ext:Column ID="is_quality_rate_count" runat="server" Text="质检合格率统计" DataIndex="IsQualityRateCount" Width="80" />--%>
                        <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="80"
                            Hidden="true" />
<%--                        <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80" />--%>
                        <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                            <Commands>
                                 <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                    <ToolTip Text="修改本条数据" />
                                </ext:GridCommand>
                                <ext:CommandSeparator />
                                <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                    <ToolTip Text="删除本条数据" />
                                </ext:GridCommand>
                                <ext:CommandSeparator />
                                <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复">
                                    <ToolTip Text="恢复本条数据" />
                                </ext:GridCommand>
                            </Commands>
                            <PrepareToolbar Fn="prepareToolbar" />
                            <Listeners>
                                <Command Handler="return commandcolumn_click(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" Mode="Simple" />
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
                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80" Editable="false">
                                <Items>
                                    <ext:ListItem Text="15" />
                                    <ext:ListItem Text="50" />
                                    <ext:ListItem Text="200" />
                                    <ext:ListItem Text="500" />
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
            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改物料基础信息"
                Height="420" Width="650" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="1" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="1">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:Container ID="Container1" runat="server" AutoHeight="true">
                                <Items>
                                    <ext:FieldSet ID="FieldSet3" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                        Pmodifying="5">
                                        <Items>
                                            <ext:Container ID="Container29" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_obj_id" runat="server" Flex="1" FieldLabel="物料编码" LabelAlign="Right"
                                                        Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                    <ext:TextField ID="modify_material_code" runat="server" Flex="1" FieldLabel="物料代码"
                                                        LabelAlign="Right" Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_material_name" runat="server" Flex="1" FieldLabel="物料名称" EmptyText="必填"
                                                        MaxLength="80" LabelAlign="Right" Enabled="true" AllowBlank="false" IndicatorText="*"
                                                        IndicatorCls="red-text" />
                                                    <ext:TextField ID="modify_material_other_name" runat="server" Flex="1" FieldLabel="物料别名"
                                                        MaxLength="40" LabelAlign="Right" Enabled="true" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_major_type_id" runat="server" Flex="1" FieldLabel="物料大类"
                                                        LabelAlign="Right" Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                    <ext:TextField ID="modify_minor_type_id" runat="server" Flex="1" FieldLabel="物料细类"
                                                        LabelAlign="Right" Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container19" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:NumberField ID="modify_JG" runat="server" Flex="1" FieldLabel="价格" MaxLength="10" DecimalPrecision="4"
                                                        LabelAlign="Right" Enabled="true" />
                                                    <ext:TextField ID="modify_k3" runat="server" Flex="1" FieldLabel="K3编码"
                                                        LabelAlign="Right" Enabled="true" />


                                                    <ext:ComboBox ID="modify_mix_type" runat="server" Flex="1" FieldLabel="密炼类型" LabelAlign="Right"
                                                        Enabled="true" Editable="false" Hidden="true">
                                                        <DirectEvents>
                                                            <Blur OnEvent="ChangeMixType">
                                                            </Blur>
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                    <ext:TriggerField ID="modify_rub_code" runat="server" Flex="1" FieldLabel="胶料名称"
                                                        LabelAlign="Right" Enabled="true" Editable="false">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectRubberInfo" />
                                                        </Listeners>
                                               <%--         <DirectEvents>
                                                            <Change OnEvent="CheckFulfillProductMaterial">
                                                            </Change>
                                                        </DirectEvents>--%>
                                                    </ext:TriggerField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container20" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TriggerField ID="modify_rub_sect" runat="server" Flex="1" FieldLabel="胶料段数"
                                                        LabelAlign="Right" Enabled="true" Editable="false">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectRubSect" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Change OnEvent="CheckFulfillProductMaterial">
                                                            </Change>
                                                        </DirectEvents>
                                                    </ext:TriggerField>
                                                    <ext:ComboBox ID="modify_product_material_code" runat="server" Flex="1" FieldLabel="产生物料"
                                                        LabelAlign="Right" Enabled="true" DisplayField="text" ValueField="value" EmptyText="Loading..."
                                                        ValueNotFoundText="Loading..." IndicatorText="*" IndicatorCls="red-text" Editable="false" Hidden="true">
                                                        <Store>
                                                            <ext:Store ID="modify_product_material_store" runat="server">
                                                                <Model>
                                                                    <ext:Model ID="Model1" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="text" Type="String" Mapping="text" />
                                                                            <ext:ModelField Name="value" Type="String" Mapping="value" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <DirectEvents>
                                                            <Blur OnEvent="ProductMaterialBlur" />
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                       
                                            <ext:Container ID="Container22" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_pruduct_area" runat="server" Flex="1" FieldLabel="物料产地"
                                                        LabelAlign="Right" Enabled="true" Hidden="true" />
                                                    <ext:TextField ID="modify_min_stock" runat="server" Flex="1" FieldLabel="最小库存" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="千克" />
                                                    <ext:TextField ID="modify_max_stock" runat="server" Flex="1" FieldLabel="最大库存" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="千克" />
                                                          <ext:TextField ID="TextXB" runat="server" Flex="1" FieldLabel="线边库存" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  IndicatorText="千克" hidden="true"/>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container23" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5" Hidden="true">
                                                <Items>
                                                    <ext:TriggerField ID="modify_unit_id" runat="server" Flex="1" FieldLabel="单位名称" LabelAlign="Right"
                                                        Enabled="true" Editable="false">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectUnitID" />
                                                        </Listeners>
                                                    </ext:TriggerField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container24" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5"  Hidden="true">
                                                <Items>
                                                    <ext:DateField ID="modify_define_date" runat="server" Flex="1" FieldLabel="定义日期"
                                                        Format="yyyy-MM-dd" LabelAlign="Right" Enabled="true" Editable="false" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container25" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_max_park_time" runat="server" Flex="1" FieldLabel="最长停放" EmptyText="必填"
                                                        Vtype="integer" LabelAlign="Right" Enabled="true" IndicatorText="小时" AllowBlank="false" />
                                                    <ext:TextField ID="modify_min_park_time" runat="server" Flex="1" FieldLabel="最短停放" EmptyText="必填"
                                                        Vtype="integer" LabelAlign="Right" Enabled="true" IndicatorText="小时" AllowBlank="false" />
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container27" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_erp_code" runat="server" Flex="1" FieldLabel="ERP编号" MaxLength="20"
                                                        LabelAlign="Right" Enabled="true" />
                                                    <ext:TextField ID="modify_pdm_code" runat="server" Flex="1" FieldLabel="PDM编号" MaxLength="20"
                                                        LabelAlign="Right" Enabled="true" />
                                       <%--         </Items>
                                            </ext:Container>

                                          
                                            <ext:Container ID="Container28" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>--%>
                                                    <ext:Checkbox ID="modify_is_equal_material" runat="server" Flex="1" FieldLabel="是否等同物料"
                                                        LabelAlign="Right" Enabled="true" Hidden="true"/>
                                                    <ext:Checkbox ID="modify_is_put_jar" runat="server" Flex="1" FieldLabel="是否投罐" LabelAlign="Right"
                                                        Enabled="true" Hidden="true" />
                                                    <ext:Checkbox ID="modify_is_quality_rate_count" runat="server" Flex="1" FieldLabel="质检合格率"
                                                        LabelAlign="Right" Enabled="true" Hidden="true" />
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container21" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="modify_weight" runat="server" Flex="1" FieldLabel="单重" MaxLength="50"
                                                        LabelAlign="Right" Enabled="true" />
                                                    <ext:TextField ID="modify_remark" runat="server" Flex="1" FieldLabel="备注" MaxLength="50"
                                                        LabelAlign="Right" Enabled="true" />
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
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
            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加物料基础信息"
                Height="500" Width="650" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="1" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="1">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:Container ID="Container4" runat="server" AutoHeight="true">
                                <Items>
                                    <ext:FieldSet ID="FieldSet1" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                        Padding="5">
                                        <Items>
                                            <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_material_name" runat="server" Flex="1" FieldLabel="物料名称" MaxLength="80" EmptyText="必填"
                                                        LabelAlign="Right" Enabled="true" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                                    <ext:TextField ID="add_material_other_name" runat="server" Flex="1" FieldLabel="物料别名"
                                                        MaxLength="40" LabelAlign="Right" Enabled="true" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container7" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_major_type_id" runat="server" Flex="1" FieldLabel="物料大类" LabelAlign="Right"
                                                        Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                    <ext:TextField ID="add_minor_type_id" runat="server" Flex="1" FieldLabel="物料细类" LabelAlign="Right"
                                                        Enabled="true" ReadOnly="true" ReadOnlyCls="read-only" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Con_add_1" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_material_simple_name" runat="server" Flex="1" FieldLabel="物料简称"
                                                        MaxLength="40" LabelAlign="Right" Enabled="true" />
    <ext:TextField ID="add_price_code" runat="server" Flex="1" FieldLabel="价格" MaxLength="10"
                                                        LabelAlign="Right" Enabled="true" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container18" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TriggerField ID="add_rub_sect" runat="server" Flex="1" FieldLabel="胶料段数" LabelAlign="Right"
                                                        Enabled="true" Editable="false" Hidden="true">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectRubSect" />
                                                        </Listeners>
                                                        <DirectEvents>
                                                            <Change OnEvent="CheckFulfillProductMaterial">
                                                            </Change>
                                                        </DirectEvents>
                                                    </ext:TriggerField>
                                                  
                                                    <ext:TriggerField ID="add_rub_code" runat="server" FieldLabel="胶料名称" LabelAlign="Left" AllowBlank="false" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectRubberInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                   
                                                </Items>
                                            </ext:Container>
                                            <%--</Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2"  runat="server" Title="其他信息" Layout="AnchorLayout" DefaultAnchor="100%" Padding="5">
                                            <Items>--%>
                                            <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_user_code" runat="server" Flex="1" FieldLabel="K3编码" MaxLength="20"
                                                        LabelAlign="Right" Enabled="true" />
                                                    <ext:NumberField ID="add_valid_date" runat="server" Flex="1" FieldLabel="保质期" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="天" Hidden="true" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_pruduct_area" runat="server" Flex="1" FieldLabel="物料产地" LabelAlign="Right"
                                                        Enabled="true" Hidden="true" />
                                                    <ext:TextField ID="add_min_stock" runat="server" Flex="1" FieldLabel="最小库存" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="千克" />
                                                    <ext:TextField ID="add_max_stock" runat="server" Flex="1" FieldLabel="最大库存" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="千克" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TriggerField ID="add_unit_id" runat="server" Flex="1" FieldLabel="单位名称" LabelAlign="Right"
                                                        Enabled="true" Editable="false" Hidden="true">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectUnitID" />
                                                        </Listeners>
                                                    </ext:TriggerField>
                                                    <ext:TriggerField ID="add_static_unit_id" runat="server" Flex="1" FieldLabel="统计单位"
                                                        LabelAlign="Right" Enabled="true" Editable="false" Hidden="true">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Fn="SelectStaticUnitID" />
                                                        </Listeners>
                                                    </ext:TriggerField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_static_unit_coefficient" runat="server" Flex="1" FieldLabel="单位转换系数"
                                                        Vtype="integer" LabelAlign="Right" Enabled="true" hidden="true"/>
                                                    <ext:DateField ID="add_define_date" runat="server" Flex="1" FieldLabel="定义日期" Format="yyyy-MM-dd"
                                                        LabelAlign="Right" Enabled="true" Editable="false"  Hidden="true"/>
                                                         <ext:ComboBox ID="add_product_material_code" runat="server" Flex="1" FieldLabel="产生物料"
                                                        LabelAlign="Right" Enabled="False" Editable="false" DisplayField="text" ValueField="value"
                                                        EmptyText="Loading..." ValueNotFoundText="Loading..." IndicatorText="*" IndicatorCls="red-text" 
                                                        Hidden ="true">
                                                        <Store>
                                                            <ext:Store ID="add_product_material_store" runat="server">
                                                                <Model>
                                                                    <ext:Model ID="Model4" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="text" Type="String" Mapping="text" />
                                                                            <ext:ModelField Name="value" Type="String" Mapping="value" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <DirectEvents>
                                                            <Change OnEvent="ProductMaterialBlur" />
                                                        </DirectEvents>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_max_park_time" runat="server" Flex="1" FieldLabel="最长停放" Vtype="integer" EmptyText="必填"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="小时" AllowBlank="false" />
                                                    <ext:TextField ID="add_min_park_time" runat="server" Flex="1" FieldLabel="最短停放" Vtype="integer" EmptyText="必填"
                                                        LabelAlign="Right" Enabled="true" IndicatorText="小时" AllowBlank="false" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container15" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_check_permit_error" runat="server" Flex="1" FieldLabel="盘点允许误差"
                                                        Vtype="integer" LabelAlign="Right" Enabled="true" Hidden="true" />
                                                
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container16" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_erp_code" runat="server" Flex="1" FieldLabel="ERP编号" MaxLength="20" 
                                                        LabelAlign="Right" Enabled="true"  />
                                                    <ext:TextField ID="text_pdm_code" runat="server" Flex="1" FieldLabel="PDM编号" MaxLength="20" 
                                                        LabelAlign="Right" Enabled="true"  />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:TextField ID="add_remark" runat="server" Flex="1" FieldLabel="备注" MaxLength="50"
                                                        LabelAlign="Right" Enabled="true" />
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container17" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                <Items>
                                                    <ext:Checkbox ID="add_is_equal_material" runat="server" Flex="1" FieldLabel="是否等同物料"
                                                        LabelAlign="Right" Enabled="true" Hidden="true"/>
                                                    <ext:Checkbox ID="add_is_put_jar" runat="server" Flex="1" FieldLabel="是否投罐" LabelAlign="Right"
                                                        Enabled="true" Hidden="true"/>
                                                    <ext:Checkbox ID="add_is_quality_rate_count" runat="server" Flex="1" FieldLabel="质检合格率"
                                                        LabelAlign="Right" Enabled="true" Hidden="true" />
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                         <%--   <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />--%>
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" >
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
             <ext:Window ID="winMaterialGroup" runat="server" Icon="Group" Closable="false" Title="设定物料分组"
                Height="340" Width="500" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="1"  Layout="BorderLayout">
                <Items>
                    <ext:FormPanel ID="pnlGroup" runat="server" BodyPadding="1" Region="North">
                        <Items>
                            <ext:ComboBox ID="group_material_group"  runat="server" FieldLabel="物料分组" 
                                LabelAlign="Left" DisplayField="MaterialName" TypeAhead="false"
                                ValueField="MaterialCode" IndicatorText="*" IndicatorCls="red-text"
                                Width="400" MinChars="1">
                                <Store>
                                    <ext:Store ID="group_material_group_store" runat="server" OnReadData="MaterialGroupStoreRefresh">
                                        <Model>
                                            <ext:Model ID="Model2" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="MaterialName" Type="String" Mapping="MaterialName" />
                                                    <ext:ModelField Name="MaterialCode" Type="String" Mapping="MaterialCode" />
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
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Listeners>
                                    <Select Handler="this.getTrigger(0).show();" />
                                    <Change Handler="App.hidden_material_group_code.setValue(App.group_material_group.getValue());" />
                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); App.group_material_group.setValue('');App.hidden_material_group_code.setValue('');this.getTrigger(0).hide();}" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:FormPanel>
                    <ext:GridPanel ID="groupPanel" Collapsible="true" runat="server" Region="Center" Title="所选物料">
                        <Store>
                            <ext:Store ID="groupStore" runat="server" PageSize="5">
                                <Model>
                                    <ext:Model ID="model3" runat="server" IDProperty="MaterialCode">
                                        <Fields>
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="MaterialGroup" />
                                            <ext:ModelField Name="MaterialLevel" Type="Int" />
                                              <ext:ModelField Name="MaterialSimpleName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="20" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="MaterialCode" runat="server" Text="物料代码" DataIndex="MaterialCode" Flex="1" />
                                <ext:Column ID="MaterialGroup" runat="server" Text="物料分组" DataIndex="MaterialGroup" Flex="1" />
                                 <ext:Column ID="MaterialGroupID" runat="server" Text="物料分组编号" DataIndex="MaterialSimpleName" Flex="1" />
                                <ext:Column ID="MaterialLevel" runat="server" Text="物料等级" DataIndex="MaterialLevel" Width="60" >
                                    <Editor>
                                        <ext:NumberField ID="NumberField" runat="server" MinValue="0" MaxValue="5" />
                                    </Editor>
                                </ext:Column>
                                <ext:CommandColumn ID="CommandColumn2" runat="server" Width="50">
                                    <Commands>
                                        <ext:GridCommand Text="取消" ToolTip-Text="取消修改" CommandName="reject" Icon="ArrowUndo" />
                                    </Commands>
                                    <PrepareToolbar Handler="toolbar.items.get(0).setVisible(record.dirty);" />
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                        <Plugins>
                            <ext:CellEditing ID="CellEditing" runat="server">
                                <Listeners>
                                    <Edit Fn="edit2" />
                                </Listeners>
                            </ext:CellEditing>
                        </Plugins>
                        <Listeners>
                            <CellClick Fn="cellDblClick" />
                        </Listeners>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnGroupSave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnGroupSave_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                <ExtraParams>
                                    <ext:Parameter Name="data" Value="#{groupStore}.getChangedData({skipIdForNewRecords : false})" Mode="Raw" Encode="true" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnGroupCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="BtnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnGroupClear" runat="server" Text="清除物料分组" Icon="Delete">
                        <DirectEvents>
                            <Click OnEvent="BtnGroupClear_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Window ID="winSetStock" runat="server" Icon="Bell" Closable="false" Title="设定物料库存及停放时间"
                Height="500" Width="1000" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5"  Layout="BorderLayout">
                <Items>
                    <ext:FormPanel ID="stockFrmPnl" runat="server" BodyPadding="5" Region="North" Layout="ColumnLayout">
                        <Items>
                             <ext:Container ID="container6" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                                    <Items>
                                        <ext:NumberField ID="set_max_part_time" runat="server" FieldLabel="最大停放时间" LabelAlign="Right" ColumnWidth=".85"  >
                                        </ext:NumberField>
                                        <ext:Checkbox ID="cb_set_max_part_time" runat="server" ColumnWidth=".10" Checked="false">
                                            <Listeners>
                                                <Change Handler="setStockCondition(#{cb_set_max_part_time},#{set_max_part_time})"></Change>
                                            </Listeners>
                                        </ext:Checkbox>
                                    </Items>
                             </ext:Container>
                             <ext:Container ID="container9" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                                    <Items>
                                        <ext:NumberField ID="set_min_part_time" runat="server" FieldLabel="最小停放时间" LabelAlign="Right" ColumnWidth=".85" ></ext:NumberField>
                                        <ext:Checkbox ID="cb_set_min_part_time" runat="server" ColumnWidth=".10" Checked="false">
                                            <Listeners>
                                                <Change Handler="setStockCondition(#{cb_set_min_part_time},#{set_min_part_time})"></Change>
                                            </Listeners>
                                        </ext:Checkbox>
                                    </Items>
                             </ext:Container>
                             <ext:Container ID="container30" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                                    <Items>
                                        <ext:NumberField ID="set_max_stock" runat="server" FieldLabel="最大库存" LabelAlign="Right" ColumnWidth=".85" ></ext:NumberField>
                                        <ext:Checkbox ID="cb_set_max_stock" runat="server" ColumnWidth=".10" Checked="true">
                                            <Listeners>
                                                <Change Handler="setStockCondition(#{cb_set_max_stock},#{set_max_stock})"></Change>
                                            </Listeners>
                                        </ext:Checkbox>
                                    </Items>
                             </ext:Container> 
                             <ext:Container ID="container31" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                                    <Items>
                                        <ext:NumberField ID="set_min_stock" runat="server" FieldLabel="最小库存" LabelAlign="Right" ColumnWidth=".85" ></ext:NumberField>
                                        <ext:Checkbox ID="cb_set_min_stock" runat="server" ColumnWidth=".10" Checked="true">
                                            <Listeners>
                                                <Change Handler="setStockCondition(#{cb_set_min_stock},#{set_min_stock})"></Change>
                                            </Listeners>
                                        </ext:Checkbox>
                                    </Items>
                             </ext:Container> 
                             <ext:Container ID="container32" runat="server" Layout="ColumnLayout" ColumnWidth=".2" Padding="5">
                                    <Items>
                                        <ext:Checkbox ID="set_is_quality_rate_count" runat="server" FieldLabel="质检合格率" LabelAlign="Right" ColumnWidth=".60" ></ext:Checkbox>
                                        <ext:Checkbox ID="cb_set_is_quality_rate_count" runat="server" ColumnWidth=".15" Checked="true">
                                            <Listeners>
                                                <Change Handler="setStockCondition(#{cb_set_is_quality_rate_count},#{set_is_quality_rate_count})"></Change>
                                            </Listeners>
                                        </ext:Checkbox>
                                    </Items>
                             </ext:Container> 
                        </Items>
                    </ext:FormPanel>
                    <ext:GridPanel ID="stockPnl" Collapsible="true" runat="server" Region="Center" Title="所选物料">
                        <Store>
                            <ext:Store ID="stockStore" runat="server" PageSize="-1">
                                <Model>
                                    <ext:Model ID="model5" runat="server" IDProperty="MaterialCode">
                                        <Fields>
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="MaxParkTime" />
                                            <ext:ModelField Name="MinParkTime" />
                                            <ext:ModelField Name="MinStock" />
                                            <ext:ModelField Name="MaxStock" />
                                            <ext:ModelField Name="IsQualityRateCount" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel2" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="50" />
                                <ext:Column ID="Column1" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="物料代码" DataIndex="MaterialCode" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="最大停放时间" DataIndex="MaxParkTime" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="最小停放时间" DataIndex="MinParkTime" Flex="1" />
                                <ext:Column ID="Column5" runat="server" Text="最大库存" DataIndex="MinStock" Flex="1" />
                                <ext:Column ID="Column6" runat="server" Text="最小库存" DataIndex="MaxStock" Flex="1" />
                                <ext:Column ID="Column7" runat="server" Text="质检合格率" DataIndex="IsQualityRateCount" Flex="1" />
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btn_set_stock_save" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnSetStockSave_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                <ExtraParams>
                                    <ext:Parameter Name="data" Value="#{stockStore}.getChangedData({skipIdForNewRecords : false})" Mode="Raw" Encode="true" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btn_set_stock_cancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="BtnSetStockCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Hidden Hidden="true" ID="hidden_major_type_id" runat="server">
            </ext:Hidden>
            <ext:Hidden Hidden="true" ID="hidden_minor_type_id" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_select_unit" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_unit_id" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_static_unit_id" runat="server">
            </ext:Hidden>
            <ext:Hidden Hidden="true" ID="hidden_rub_code" runat="server">
            </ext:Hidden>
            <ext:Hidden Hidden="true" ID="hidden_static_class" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_name" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_part1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_part2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_part3" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_rub_sect" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_group" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_set_Stock" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_material_group_code" runat="server">
            </ext:Hidden>
              <ext:Hidden ID="hiddenCgroup" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
