<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialAutoSet.aspx.cs" Inherits="Manager_BasicInfo_MaterialInfo_MaterialAutoSet" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物料基础信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
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
                if (App.hidden_major_type_id.getValue() != 2) {
                    App.add_material_name.setValue(App.hidden_material_part3.getValue() +
                        App.hidden_material_part1.getValue());
                }
            }
            else if (!App.winModify.hidden) {
                App.modify_rub_code.setValue(record.data.RubName);
                App.hidden_rub_code.setValue(record.data.RubCode);
                App.hidden_material_part3.setValue(record.data.RubName);
                if (App.hidden_major_type_id.getValue() != 2) {
                    App.modify_material_name.setValue(App.hidden_material_part3.getValue() +
                    App.hidden_material_part1.getValue());
                }
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
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            var browser = myBrowser();
            App.direct.commandcolumn_direct_edit(ObjID, browser, {
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
    <script type="text/javascript">
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
                            <ext:Button runat="server" Icon="Bell" Text="设置库存及停放时间" ID="btn_set_stock">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip4" runat="server" Html="点击设置库存及停放时间" />
                                </ToolTips>
                               <DirectEvents>
                                    <Click OnEvent="btn_set_stock_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="StockValues" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
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
                                    <ext:Container ID="cbxChejian2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" >
                                                <Items>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('');" />
                                                </Listeners>
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
                    <ext:Store ID="store" runat="server" PageSize="15">
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
                                    <ext:ModelField Name="UserCode" />
                                    <ext:ModelField Name="PlanPrice" />
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
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="45" />
                        <ext:Column ID="obj_id" runat="server" Text="物料编码" DataIndex="ObjID" Width="80" Visible="false" />
                        <ext:Column ID="Column5" runat="server" Text="状态" DataIndex="MaterialCode"
                            Width="120" />
                        <ext:Column ID="material_code" runat="server" Text="物料代码" DataIndex="MaterialCode"
                            Width="120" />
                        <ext:Column ID="material_name" runat="server" Text="物料名称" DataIndex="MaterialName"
                            Width="150" />
                        <ext:Column ID="rub_code" runat="server" Text="胶料名称" DataIndex="RubCode" Width="80" />
                        <ext:Column ID="max_park_time" runat="server" Text="最大停放时间" DataIndex="MaxParkTime"
                            Width="80" />
                        <ext:Column ID="min_park_time" runat="server" Text="最小停放时间" DataIndex="MinParkTime"
                            Width="80" />
                        <ext:Column ID="unit_id" runat="server" Text="单位名称" DataIndex="UnitID" Width="80" />
                        <ext:Column ID="static_unit_id" runat="server" Text="统计单位" DataIndex="StaticUnitID"
                            Width="80" />
                        <ext:Column ID="standard_code" runat="server" Text="标准码" DataIndex="StandardCode"
                            Width="80" />
                        <ext:Column ID="erp_code" runat="server" Text="ERP代码" DataIndex="ERPCode" Width="80" />
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
            <ext:Window ID="winSetStock" runat="server" Icon="Bell" Closable="false" Title="设定停放时间"
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
                            <ext:Container ID="container1" runat="server" Layout="ColumnLayout" ColumnWidth=".2"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtSetTime" runat="server" Format="yyyy-MM-dd" FormatControlForValue="yyyy-MM-dd"
                                        FieldLabel="恢复时间" LabelAlign="Right" />
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
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
