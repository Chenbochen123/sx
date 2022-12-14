<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftNum.aspx.cs" Inherits="Manager_ProducingPlan_ShiftNum" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>班组维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
     <style type="text/css">
        .x-grid-body .x-grid-cell-Cost
        {
            background-color: #f1f2f4;
        }
        
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner
        {
            background-color: #e1e2e4;
        }
        
        .task .x-grid-cell-inner
        {
            padding-left: 15px;
        }
        
        .x-grid-row-summary .x-grid-cell-inner
        {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <script type="text/javascript">
       

    </script>
    
    <script type="text/javascript">

        //点击修改按钮
        var cmdcol_click = function (command, record) {
             if (command == "Save") {
                 App.direct.pnlList_Add(record.data.objid, record.data.Equip_name, record.data.ClassName, record.data.shift_num, 
                    {
                        success: function () { },
                        failure: function () { }
                    });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var iid = record.data.iid;
            App.direct.pnlList_Delete(iid, {
                success: function () { },
                failure: function () { }
            });
        }


        var addEmployee = function () {
            var grid = App.pnlList,
                store = grid.getStore();
            grid.editingPlugin.cancelEdit();
            grid.getView().headerCt.setSortState(); // To update columns sort UI

            store.insert(0, {
                objid: '',
                Equip_name: '',
                ClassName: '',
                shift_num: '',
            });

            grid.editingPlugin.startEdit(0, 0);
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Sorters>
                            <ext:DataSorter Property="objid" />
                        </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="objid">
                                    <Fields>
                                        <ext:ModelField Name="objid" Type="Int" />
                                        <ext:ModelField Name="Equip_name" Type="String" />
                                        <ext:ModelField Name="ClassName"  Type="String"/>
                                        <ext:ModelField Name="shift_num"  Type="Int"/>
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                    </Plugins>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Column18" runat="server" Text="主键" DataIndex="objid" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="机台" DataIndex="Equip_name" Width="140">
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="工段" DataIndex="ClassName" Width="80" >
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="班组" DataIndex="shift_num" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="80">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                   
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
               
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
