<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialToRecipe, App_Web_zqfhdfip" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>原材料查询配方</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
       
     
        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
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
            App.hidden_delete_flag.setValue("0");
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
     

    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmWork" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlWorkTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barWork">
                            <Items>
                              
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
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
                        <ext:Panel ID="pnlWorkQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                       <ext:ComboBox runat="server" ID="ComboBoxNorthMaterMinorType" FieldLabel="原材料分类"
                        LabelAlign="Right" LabelWidth="80" Editable="false" InputWidth="250" MatchFieldWidth="false"
                        ColumnWidth="0.3" Padding="2">
                        <ListConfig Width="250" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                        <DirectEvents>
                            <Change OnEvent="ComboBoxNorthMaterMinorType_Change">
                                <EventMask ShowMask="true" />
                            </Change>
                        </DirectEvents>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthMater" FieldLabel="原材料型号" LabelAlign="Right"
                        LabelWidth="80" Editable="false" InputWidth="250" MatchFieldWidth="false" ColumnWidth="0.5"
                        Padding="2">
                        <ListConfig Width="250" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
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
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="RecipeName" />
                                        <ext:ModelField Name="RecipeMaterialName" />
                                        <ext:ModelField Name="RecipeModifyTime" />
                                        <ext:ModelField Name="SetWeight" />
                                        <ext:ModelField Name="ErrorAllow"  />
                                        <%--<ext:ModelField Name="Remark" />--%>
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="EquipName" runat="server" Text="机台名称" DataIndex="EquipName" Width="150"  />
                            <ext:Column ID="RecipeName" runat="server" Text="配方名称" DataIndex="RecipeName" Width="120"  />
                            <ext:Column ID="RecipeMaterialName" runat="server" Text="配方物料名称" DataIndex="RecipeMaterialName" Width="120"  />
                            <ext:Column ID="erp_code" runat="server" Text="配方修改时间" DataIndex="RecipeModifyTime" Width="120" Hidden="true" />
                            <ext:Column ID="delete_flag" runat="server" Text="设定重量" DataIndex="SetWeight" Width="150"   />
                            <ext:Column ID="remark" runat="server" Text="允许误差" DataIndex="ErrorAllow" Width="120"  />
                        
                        </Columns>
                    </ColumnModel> 
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            
                <ext:Hidden ID="hidden_work_name"  runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>