<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MixingModel.aspx.cs" Inherits="Manager_Technology_BasicInfo_MixingModel" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>混炼模版维护</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>MixingModel.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript">
        var treePanelActionNodeLoad = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.treePanelActionNodeLoad(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    node.appendChild(data, undefined, true);
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Title="混炼工序" Width="200" Layout="BorderLayout">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:Panel ID="Panel3" runat="server" Region="Center" Layout="AccordionLayout">
                            <Items>
                                <ext:TreePanel ID="treePanelUser" runat="server" Title="混炼工序" Icon="FolderGo" AutoHeight="true" RootVisible="false">
                                    <Store>
                                        <ext:TreeStore ID="treeStoreUser" runat="server">
                                            <Proxy>
                                                <ext:PageProxy>
                                                    <RequestConfig Method="GET" Type="Load" />
                                                </ext:PageProxy>
                                            </Proxy>
                                            <Root>
                                                <ext:Node NodeID="Root" Expanded="true" />
                                            </Root>
                                        </ext:TreeStore>
                                    </Store>
                                    <Listeners>
                                        <BeforeLoad Fn="treePanelActionNodeLoad" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Select OnEvent="GetActinUserGrid" />
                                    </DirectEvents>
                                </ext:TreePanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="GridPanel1" runat="server" Region="Center" Title="混炼工序步骤" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="barUser">
                                    <Items>
                                        <ext:Button runat="server" Icon="Add" Text="添加步骤" ID="btnAdd">
                                            <ToolTips>
                                                <ext:ToolTip ID="ttAdd" runat="server" Html="点击添加混炼模版" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Fn="AddModel"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" Icon="Add" Text="修改步骤" ID="Button1">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="点击对现有混炼进行修改" />
                                            </ToolTips>
                                            <DirectEvents>
                                                <Click OnEvent="btnAdd_Click">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button runat="server" Icon="Add" Text="保存步骤" ID="Button2">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="点击对现有混炼进行修改" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Fn="saveInfo"></Click>
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtEquipCode" runat="server" FieldLabel="设备编号" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TextField ID="txtEquipName" runat="server" FieldLabel="设备名称" LabelAlign="Right" ReadOnly="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtEquipJarType" runat="server" FieldLabel="设备类型" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TextField ID="txtJarName" runat="server" FieldLabel="料仓分类" LabelAlign="Right" ReadOnly="true"  Text="1"/>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                <ext:Hidden ID="txtEquipID" runat="server"></ext:Hidden>
                                <ext:Hidden ID="txtJarTypeID" runat="server"></ext:Hidden>
                                <ext:Hidden ID="txtModelIndex" runat="server"></ext:Hidden>
                            </Items>
                        </ext:Panel>
                        <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" Frame="true">
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="30">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" Name="gridPanelCenterStoreModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="ModelCode" />
                                                <ext:ModelField Name="TemCode" />
                                                <ext:ModelField Name="ActionCode" />
                                                <ext:ModelField Name="TempValue" />
                                                <ext:ModelField Name="PresValue" />
                                                <ext:ModelField Name="RotaValue" />
                                                <ext:ModelField Name="PowerValue" />
                                                <ext:ModelField Name="EnerValue" />
                                                <ext:ModelField Name="TimeValue" />
                                                <ext:ModelField Name="Remark" />
                                                <ext:ModelField Name="RecordTime" />
                                                <ext:ModelField Name="SeqIdx" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                        <ext:DataSorter Property="SeqIdx" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="SeqIdx" DataIndex="SeqIdx" runat="server" Text="步骤" Align="Center" Width="60" />
                                    <ext:Column ID="TemCode" DataIndex="TemCode" runat="server" Text="条件设定" Width="200">
                                        <Editor>
                                            <ext:ComboBox ID="setTemCode" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Fn="showTemCode"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="TimeValue" DataIndex="TimeValue" runat="server" Text="时间" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField1" runat="server" MinValue="0" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="TempValue" DataIndex="TempValue" runat="server" Text="温度" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField2" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="EnerValue" DataIndex="EnerValue" runat="server" Text="能量" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField3" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="PowerValue" DataIndex="PowerValue" runat="server" Text="功率" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField4" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ActionCode" DataIndex="ActionCode" runat="server" Text="动作" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="setActionCode" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Fn="showActionCode"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="RotaValue" DataIndex="RotaValue" runat="server" Text="转速" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField5" runat="server" MinValue="0" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="PresValue" DataIndex="PresValue" runat="server" Text="压力" Align="Center" Width="60">
                                        <Editor>
                                            <ext:NumberField ID="NumberField6" runat="server" MinValue="0" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="commandCol" runat="server" Width="176" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="本条之前插入数据" />
                                            </ext:GridCommand>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="本条之后添加数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>

                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server" />
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing ID="cellEditing" runat="server"/>
                            </Plugins>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
