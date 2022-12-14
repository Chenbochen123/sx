<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeInfolead.aspx.cs" Inherits="Manager_BasicInfo_CommonPage_RecipeInfolead" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>胶料查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="../../../resources/js/waitwindow.js" type="text/javascript"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolbar.doRefresh();
            return false;
        }
    </script>

    <!--特殊-->
    <script type="text/javascript">

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_canAuditUser_Window",
            hidden: true,
            width: 415,
            height: 430,
            html: "<iframe src='canAuditUser.aspx' width=100% height=397px scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择审核人",
            modal: true
        })
        var delete_click = function () {
            Ext.Msg.confirm("提示", '您确定要删除选中的工艺配方信息？', function (btn) { btndelete(btn); });

        };
        var btndelete = function (btn) {
            if (btn != "yes") {
                return;
            }
            var Values = Ext.encode(App.gridPanelCenter.getRowsValues({ selectedOnly: "true" }));
            App.direct.deleteRecipe(Values, {
                success: function (result) {
                    var results = result.split(",");
                    if (result == "") {
                        after();
                        Ext.Msg.alert('成功', "工艺配方成功删除！", function (btn) {
                        });
                    }

                    else {
                        after();
                        Ext.Msg.alert('失败', result);
                    }
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        }
        var requestcanAuditUser = function (AuditUser) {
            App.hidden_AuditUser.setValue(AuditUser);
            type = App.hidden_Type.getValue();
            if (type == "1") {
                response("yes", App.hidden_Mater_code.getValue(), App.hidden_Equipname.getValue(), App.hidden_Edt_code.getValue(), App.hidden_Recipe_type.getValue(),App.hidden_Routing_type.getValue());
            }
            else {
                btnsave("yes");
            }
        }
        var select_click = function () {
            Ext.Msg.confirm("提示", '您确定需要保存选中的工艺配方信息？', function (btn) { showAudit(btn, '2'); });

        };
        var showAudit = function (command,typeName) {
            if (command == "yes") {
                App.hidden_Type.setValue(typeName);
                App.Manager_BasicInfo_CommonPage_canAuditUser_Window.show();
            }
            else {
                parent.App.Manager_BasicInfo_CommonPage_RecipeInfolead_Window.close();
            }
        };
        var btnsave = function (btn) {
            if (btn != "yes") {
                return;
            }
            var after = function () {
                App.waitProgressWindow.close();
            }
            var before = function () {

                App.waitProgressWindow.show();
            }
            before();
            var Values = Ext.encode(App.gridPanelCenter.getRowsValues({ selectedOnly: "true" }));
            App.direct.SaveSelectMutInfo(Values, {
                success: function (result) {
                    var results = result.split(",");
                    if (result == "") {
                        after();
                        Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) {
                        });

                    }
                    else if (results.length > 4) {
                        after();
                        Ext.Msg.confirm('失败', results[0] + results[4] + "," + "是否仍然导入新配方，覆盖原有配方？", function (btn) {
                            if (btn == "yes") {
                                before();
                                App.direct.invalidrecipe(results[1], results[2], results[3], {
                                    success: function (result) {
                                        //                   Ext.Msg.alert('成功', result);
                                        if (result == "") {
                                            after();
                                            Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) {
                                            });

                                        } else {
                                            after();
                                            Ext.Msg.alert('失败', result);
                                        }
                                    },
                                    failure: function (errorMsg) {
                                        after();
                                        Ext.Msg.alert('错误', errorMsg);
                                    }
                                });
                            }

                        });
                    }
                     else {
                        after();
                        Ext.Msg.alert('失败', result);
                    }
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        }


        var response = function (btn, Mater_Code, Equipename, Edt_code, Recipe_type, Routing_Type) {
            if (btn != "yes") {
                return;
            }

            var after = function () {
                App.waitProgressWindow.close();
            }
            var before = function () {
                App.waitProgressWindow.show();
            }
            before();
            App.direct.SaveJsonInfo(Mater_Code, Equipename,Edt_code,Recipe_type,Routing_Type,{
                success: function (result) {
                    var results = result.split(",");
                    if (result == "") {
                        after();
                        Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) {
                            if (btn == "ok") {
                            }
                        });
                    }
                    else if (results.length > 4) {
                        after();
                        Ext.Msg.confirm('失败', results[0] + results[4] + "," + "是否仍然导入新配方，覆盖原有配方？", function (btn) {
                            if (btn == "yes") {
                                before();
                                App.direct.invalidrecipe(results[1], results[2], results[3], results[5], {
                                    success: function (result) {
                                        //                   Ext.Msg.alert('成功', result);
                                        if (result == "") {
                                            after();
                                            Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) {
                                            });

                                        } else {
                                            after();
                                            Ext.Msg.alert('失败', result);
                                        }
                                    },
                                    failure: function (errorMsg) {
                                        after();
                                        Ext.Msg.alert('错误', errorMsg);
                                    }
                                });
                            }

                        });
                    }
                    else {
                        after();
                        Ext.Msg.alert('失败', result);
                    }
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            //触发导入验证方法
            //            parent.Manager_BasicInfo_CommonPage_RecipeInfolead_Request(record);

            //            parent.App.Manager_BasicInfo_CommonPage_RecipeInfolead_Window.close();
            return false;
        }

        var showerror = function (result) {
            //                   Ext.Msg.alert('成功', result);
            if (result == "") {
                Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) { refreshMain(true) });
            } else {
                Ext.Msg.alert('失败', result);
            }
        }
      
            //触发导入验证方法
//            parent.Manager_BasicInfo_CommonPage_RecipeInfolead_Request(record);

//            parent.App.Manager_BasicInfo_CommonPage_RecipeInfolead_Window.close();

        var commandColumn_click = function (command, record) {
            Ext.Msg.confirm("提示", '您确定需要保存此工艺配方信息？', function (btn) { showAuditUser(btn, record, '1'); });

        };
      
        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }
        var showAuditUser = function (command, record, typeName) {
            if (command == "yes") {
                App.hidden_Type.setValue(typeName);
                App.hidden_Mater_code.setValue(record.data.Mater_Code);
                App.hidden_Equipname.setValue(record.data.EquipName);
                 App.hidden_Edt_code.setValue(record.data.Edt_code);
                 App.hidden_Recipe_type.setValue(record.data.Recipe_type);
                 App.hidden_Routing_type.setValue(record.data.Routing_type);
                App.Manager_BasicInfo_CommonPage_canAuditUser_Window.show();
            }
            else {
                parent.App.Manager_BasicInfo_CommonPage_RecipeInfolead_Window.close();
            }
        };
       
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                 <ext:Button runat="server" Icon="DiskDownload" Text="批量导入配方" ID="Button1" Hidden="false">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="批量导入配方" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="select_click"></Click>
                                    </Listeners>
                                </ext:Button>
                                   <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                 <ext:Button runat="server" Icon="Delete" Text="批量删除配方" ID="Button2" Hidden="false">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="批量删除配方" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="delete_click"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                            <Items>
                                                <ext:TextField ID="txtRubName" runat="server" FieldLabel="胶料名称" LabelAlign="Right"  />
                                                <ext:TextField ID="txtEquipName" runat="server" FieldLabel="机台名称" LabelAlign="Right"  />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server" >
                                    <Fields>
                                        <ext:ModelField Name="Mater_Code" />
                                        <ext:ModelField Name="Mater_Name" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="Edt_code" />
                                        <ext:ModelField Name="Recipe_type" />
                                        <ext:ModelField Name="Recipe_typeName" />
                                        <ext:ModelField Name="Modify_Flag" />   
                                        <ext:ModelField Name="Routing_type" />                              
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="Mater_Code" DataIndex="Mater_Code" runat="server" Text="胶号" Width="100" />
                            <ext:Column ID="Mater_Name" DataIndex="Mater_Name" runat="server" Text="胶料名称" Width="100" />
                            <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="机台名称" Width="100" />
                              <ext:Column ID="Column1" DataIndex="Recipe_type" runat="server" Text="配方类型" Width="100" />
                                <ext:Column ID="Column2" DataIndex="Recipe_typeName" runat="server" Text="配方类型名称" Width="100" />
                                  <ext:Column ID="Column3" DataIndex="Edt_code" runat="server" Text="配方版本" Width="100" />
                            <ext:Column ID="HandleType" DataIndex="Modify_Flag" runat="server" Text="操类型" Width="80" />
                            <ext:CommandColumn ID="commandColumn" runat="server" Width="60" Text="导入" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="accept" CommandName="Select" Text="导入">
                                        <ToolTip Text="确认导入本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar />
                                <Listeners>
                                    <Command Handler="return commandColumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="rowSelectMuti" runat="server" Mode="Simple" />
                    </SelectionModel>
                    <Listeners>
                        <CellDblClick Fn="cellDblClick" />
                    </Listeners>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                  <ext:Hidden ID="hidden_Type" runat="server"></ext:Hidden>
                 <ext:Hidden ID="hidden_Mater_code" runat="server"></ext:Hidden>
                   <ext:Hidden ID="hidden_Equipname" runat="server"></ext:Hidden>
                 <ext:Hidden ID="hidden_AuditUser" runat="server"></ext:Hidden>
                  <ext:Hidden ID="hidden_Edt_code" runat="server"></ext:Hidden>
                 <ext:Hidden ID="hidden_Recipe_type" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_Routing_type" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
