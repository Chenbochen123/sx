<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Material.aspx.cs" Inherits="Manager_Technology_Manage_Material" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工艺参数</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .x-grid-body .x-grid-cell-Cost {
            background-color: #f1f2f4;
        }

        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner {
            background-color: #e1e2e4;
        }

        .task .x-grid-cell-inner {
            padding-left: 15px;
        }

        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <style type="text/css">
        .x-grid-cell-inner {
            border-right:1px solid
                #d6d3d3;
        }
        .x-grid-row td, .x-grid-summary-row td {
            padding-right: 0px;
        }
        .x-grid-row {
            border-top-width:0px;
            border-bottom-width:0px;
        }
        </style>

    <script type="text/javascript">
        var prepareGroupToolbar = function (grid, toolbar, groupId, records) {
            // you can prepare ready toolbar
        };


        var getAdditionalData = function (data, idx, record, orig) {
            var o = Ext.grid.feature.RowBody.prototype.getAdditionalData.apply(this, arguments),
                d = data;

            Ext.apply(o, {
                rowBodyColspan: record.fields.getCount(),
                rowBody: Ext.String.format('<div style=\'padding:0 5px 5px 5px;\'>The {0} [{1}] requires light conditions of <i>{2}</i>.<br /><b>Price: {3}</b></div>', d.Common, d.Botanical, d.Light, Ext.util.Format.usMoney(d.Price)),
                rowBodyCls: ""
            });

            return o;
        };

    </script>
    <script type="text/javascript">


        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Save") {
                App.direct.pnlList_Add(record.data.Mater_code, record.data.PJtemp_up, record.data.PJtemp_down, record.data.CBtime_up, record.data.CBtime_down, record.data.rubtime_up,
                    record.data.rubtime_down, record.data.Speed_up, record.data.speed_down, record.data.press_up, record.data.press_down, record.data.ZZtemp_up,
                    record.data.ZZtemp_Down, record.data.CBlTemp_up, record.data.CBTemp_down, record.data.XLMtemp_up, record.data.XLMtemp_dowm,
                    {
                        success: function () { },
                        failure: function () { }
                    });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
            OnClick="btnExportSubmit_Click" />--%>
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
                                
                               <%-- <ext:Button runat="server" Icon="PageExcel" Text="导出Excel" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();">
                                        </Click>
                                    </Listeners>
                                </ext:Button>--%>

                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>

                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Sorters>
                                <ext:DataSorter Property="Mater_code" />
                            </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Mater_code">
                                    <Fields>
                                        <ext:ModelField Name="Mater_code" />
                                        <ext:ModelField Name="Mater_name" />
                                        <ext:ModelField Name="PJtemp_up" Type="Int" />
                                        <ext:ModelField Name="PJtemp_down" Type="Int" />
                                        <ext:ModelField Name="CBtime_up" Type="Int" />
                                        <ext:ModelField Name="CBtime_down" Type="Int" />
                                        <ext:ModelField Name="rubtime_up" Type="Int" />
                                        <ext:ModelField Name="rubtime_down" Type="Int" />
                                        <ext:ModelField Name="Speed_up" Type="Int" />
                                        <ext:ModelField Name="speed_down" Type="Int" />
                                        <ext:ModelField Name="press_up" Type="Int" />
                                        <ext:ModelField Name="press_down" Type="Int" />
                                        <ext:ModelField Name="ZZtemp_up" Type="Int" />
                                        <ext:ModelField Name="ZZtemp_Down" Type="Int" />
                                        <ext:ModelField Name="CBlTemp_up" Type="Int" />
                                        <ext:ModelField Name="CBTemp_down" Type="Int" />
                                        <ext:ModelField Name="XLMtemp_up" Type="Int" />
                                        <ext:ModelField Name="XLMtemp_dowm" Type="Int" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                    </Plugins>

                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column runat="server" Text="胶料">
                                <Columns>
                                    <ext:Column ID="Column18" runat="server" Text="物料代码" DataIndex="Mater_code" Width="110">
                                    </ext:Column>
                                    <ext:Column ID="Column1" runat="server" Text="物料名称" DataIndex="Mater_name" Width="150">
                                    </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="排胶温度">
                                <Columns>
                            <ext:Column ID="Column3" runat="server" Text="上限" DataIndex="PJtemp_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column2" runat="server" Text="下限" DataIndex="PJtemp_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="加炭黑时间">
                                <Columns>
                            <ext:Column ID="Column14" runat="server" Text="上限" DataIndex="CBtime_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="下限" DataIndex="CBtime_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="加胶时间">
                                <Columns>
                            <ext:Column ID="Column5" runat="server" Text="上限" DataIndex="rubtime_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column6" runat="server" Text="下限" DataIndex="rubtime_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="转速">
                                <Columns>
                            <ext:Column ID="Column7" runat="server" Text="上限" DataIndex="Speed_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="下限" DataIndex="speed_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="压力">
                                <Columns>
                            <ext:Column ID="Column9" runat="server" Text="上限" DataIndex="press_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column10" runat="server" Text="下限" DataIndex="press_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="转子温度">
                                <Columns>
                            <ext:Column ID="Column11" runat="server" Text="上限" DataIndex="ZZtemp_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column12" runat="server" Text="下限" DataIndex="ZZtemp_Down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="侧壁温度">
                                <Columns>
                            <ext:Column ID="Column13" runat="server" Text="上限" DataIndex="CBlTemp_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column15" runat="server" Text="下限" DataIndex="CBTemp_down" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="卸料门温度">
                                <Columns>
                            <ext:Column ID="Column16" runat="server" Text="上限" DataIndex="XLMtemp_up" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column17" runat="server" Text="下限" DataIndex="XLMtemp_dowm" Width="40">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                                </Columns>
                            </ext:Column>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="60">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存" />
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
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
