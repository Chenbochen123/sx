﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnergyManage.aspx.cs" Inherits="Manager_Equipment_Energy_EnergyManage" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备能耗管理</title>
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


        //下次维修日期报警
        var validDateChange = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (value != null && value != "") {
                if (parseInt((new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 0) {

                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="最后维修日期已超期，请联系处理！">{0}</div>', value);
                }
                else {
                    return Ext.String.format('<div style="color:black;font-weight:bolder;" title="">{0}</div>', value);
                }
            }
        }

    </script>
    
    <script type="text/javascript">


        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Save") {
                App.direct.pnlList_Add(record.data.Serial_id, record.data.Start_date,
                    record.data.Fac_Name, record.data.Dian, record.data.Feng,
                    record.data.Water, record.data.ZhengQi, record.data.Dian_price,
                    record.data.Feng_Price, record.data.Water_price, record.data.Qi_price,
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
            var Serial_id = record.data.Serial_id;
            App.direct.pnlList_Delete(Serial_id, {
                success: function () { },
                failure: function () { }
            });
        }


        var addEmployee = function () {
            var grid = App.pnlList,
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();

            //store.getSorters().removeAll(); // We have to remove sorting to avoid auto-sorting on insert
            grid.getView().headerCt.setSortState(); // To update columns sort UI

            debugger;
            store.insert(0, {
                Serial_id: '0',
                Start_date: Ext.Date.clearTime(new Date()),
                Fac_Name: '子午',
                Dian: '0',
                Feng: '0',
                Water: '0',
                ZhengQi: '0',
                Dian_price: hiddenDian_price.value,
                Feng_Price: hiddenFeng_Price.value,
                Water_price: hiddenWater_price.value,
                Qi_price: hiddenQi_price.value
            });

            grid.editingPlugin.startEdit(0, 0);
        };

        var removeEmployee = function () {
            var grid = App.pnlList,
                sm = grid.getSelectionModel(),
                store = grid.getStore();

            grid.editingPlugin.cancelEdit();
            store.remove(sm.getSelection());

            if (store.getCount() > 0) {
                sm.select(0);
            }
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Text="新建" Icon="Add" ID="btnAdd">
                                    <Listeners>
                                        <Click Fn="addEmployee" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button
                                    ID="btnRemove"
                                    runat="server"
                                    Text="取消"
                                    Icon="ControlRemove"
                                    Disabled="true">
                                    <Listeners>
                                        <Click Fn="removeEmployee" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Add" Text="一键添加" ID="btnAdd1">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="查询开始时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout" FieldLabel="查询结束时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1"  runat="server" Layout="HBoxLayout" FieldLabel="添加日期" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="xinjiandate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" FieldLabel="用电量" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtdian" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" FieldLabel="用风量" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtfeng" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout" FieldLabel="用水量" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtshui" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container4"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout" FieldLabel="用汽量" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtqi" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer6"  runat="server" Layout="HBoxLayout" FieldLabel="电价" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtdianprice" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container5"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer8"  runat="server" Layout="HBoxLayout" FieldLabel="风价" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtfengprice" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer10"  runat="server" Layout="HBoxLayout" FieldLabel="水价" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtshuiprice" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container6"  runat="server" Layout="FormLayout" ColumnWidth=".16">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer11"  runat="server" Layout="HBoxLayout" FieldLabel="蒸汽价" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtqiprice" runat="server" Editable="true" AllowBlank="false" Width="90"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        
                        <%--<ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:ComboBox ID="cbxFac" runat="server"  AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>--%>
                    </Items>
                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Sorters>
                                <ext:DataSorter Property="serialid" />
                            </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Serial_id">
                                    <Fields>
                                        <ext:ModelField Name="Serial_id" Type="Int" />
                                        <ext:ModelField Name="Start_date" Type="Date" />
                                        <ext:ModelField Name="WorkShop_Code" />
                                        <ext:ModelField Name="Fac_Name" />
                                        <ext:ModelField Name="Dian" />
                                        <ext:ModelField Name="Feng" />
                                        <ext:ModelField Name="Water" />
                                        <ext:ModelField Name="ZhengQi" />
                                        <ext:ModelField Name="Dian_price" />
                                        <ext:ModelField Name="Feng_Price" />
                                        <ext:ModelField Name="Water_price" />
                                        <ext:ModelField Name="Qi_price" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                        <%--<ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" ClicksToEdit="1" />--%>
                    </Plugins>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="clserialid" runat="server" Text="序号" DataIndex="Serial_id" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:DateColumn ID="Column7" runat="server" Text="开始日期" DataIndex="Start_date" Width="100" Format="yyyy-MM-dd" >
                                <Editor>
                                    <ext:DateField
                                        runat="server"
                                        AllowBlank="false"
                                        Format="yyyy-MM-dd"
                                        MinDate="1999-01-01"
                                        MinText="不能早于1999-01-01"
                                        MaxDate="<%# DateTime.Now %>"
                                        AutoDataBind="true"
                                        />
                                </Editor>
                            </ext:DateColumn>
                            <ext:Column ID="Column17" runat="server" Text="厂房" DataIndex="Fac_Name" Width="80">
                                <Editor>
                                    <ext:ComboBox ID="cbxFac" runat="server"  AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="产胶量(吨)" DataIndex="equip_name" Width="100" Hidden="true">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="用电量(度)" DataIndex="Dian" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column5" runat="server" Text="用风量(立方)" DataIndex="Feng" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="用水量" DataIndex="Water" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column9" runat="server" Text="用汽量(吨)" DataIndex="ZhengQi" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="电价" DataIndex="Dian_price" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column2" runat="server" Text="风价" DataIndex="Feng_Price" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column6" runat="server" Text="水价" DataIndex="Water_price" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column10" runat="server" Text="蒸汽价" DataIndex="Qi_price" Width="70">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <SelectionChange Handler="App.btnRemove.setDisabled(!selected.length);" />
                    </Listeners>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    
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
                <ext:Hidden ID="hiddenDian_price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenFeng_Price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenWater_price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenQi_price" runat="server">
                </ext:Hidden>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
