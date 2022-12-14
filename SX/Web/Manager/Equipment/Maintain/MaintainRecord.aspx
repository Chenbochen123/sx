<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintainRecord.aspx.cs" Inherits="Manager_Equipment_Maintain_MaintainRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>维修记录</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.ObjID, {
                    success: function () { },
                    failure: function () { }
                });
            }
            else if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteMaintainRecord(btn, record) });
            }
            else if (command == "Add") {
                App.direct.pnlList_Add(record.data.ObjID, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }
        // 对Date的扩展，将 Date 转化为指定格式的String   
        // 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
        // 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
        // 例子：   
        // (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
        // (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
        Date.prototype.Format = function (fmt) { //author: meizz   
            var o = {
                "M+": this.getMonth() + 1,                 //月份   
                "d+": this.getDate(),                    //日   
                "h+": this.getHours(),                   //小时   
                "m+": this.getMinutes(),                 //分   
                "s+": this.getSeconds(),                 //秒   
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
                "S": this.getMilliseconds()             //毫秒   
            };
            if (/(y+)/.test(fmt))
                fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }
        //根据按钮类别进行删除操作
        var commandcolumn_click = function (command, record) {
            App.direct.commandcolumn_direct_delete(new Date(record.data.SendDate).Format("yyyy-MM-dd hh:mm:ss"), record.data.SparePartCode, record.data.StoreOutNum, record.data.SendUser, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        };

        var deleteMaintainRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.pnlList_Delete(ObjID, {
                success: function () { },
                failure: function () { }
            });
        }

        var gridPanelCellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            if (record.data.SparePartOutNo != null) {//出库单详情
                var url = "Equipment/SapSparePart/SparePartOut.aspx?SendNo=" + record.data.SparePartOutNo;
                var tabid = "Manager_Equipment_SapSparePart_SparePartOut";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "备件出库管理", url, true);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryBasUsers_Request = function (result) {
            App.txtMaintainers.setValue(result);
        }
        var QueryBasUsers = function () {
            App.Manager_BasicInfo_CommonPage_QueryBasUsers_Window.show();
        }
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryBasUsers_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUsers.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择维修人员",
            modal: true
        })
    </script>
    <script type="text/javascript">
         //-------领用人-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//领用人返回值处理
             App.add_send_user.setValue(record.data.UserName);
             App.hidden_send_user.setValue(record.data.WorkBarcode);
         }

         var SelectUserInfo = function (field, trigger, index) {
             switch (index) {
                 case 0:
                     field.getTrigger(0).hide();
                     field.setValue('');
                     App.hidden_send_user.setValue("");
                     field.getEl().down('input.x-form-text').setStyle('background', "white");
                     break;
                 case 1:
                     App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                     break;
             }
         }

         Ext.create("Ext.window.Window", {//领用人查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
             height: 460,
             hidden: true,
             width: 360,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择领用人",
             modal: true
         })
         //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEqmStopType_Request = function (result) {
            App.cbType.setValue(result.data.TypeName);
            App.hidden_stop_type.setValue(result.data.TypeCode);
        }
        var QueryEqmStopType = function () {
            App.Manager_BasicInfo_CommonPage_QueryEqmStopType_Window.show();
        }
        Ext.create("Ext.window.Window", {//停机类型查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmStopType_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmStopType.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择停机类型",
            modal: true
        })
    </script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEqmStopFault_Request = function (result) {
            App.cbFault.setValue(result.data.FaultName);
            App.hidden_stop_fault.setValue(result.data.FaultCode);
        }
        var QueryEqmStopFault = function () {
            var url = "../../BasicInfo/CommonPage/QueryEqmStopFault.aspx?TypeID=" + App.hidden_stop_type.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.show();
        }
        Ext.create("Ext.window.Window", {//故障类型查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmStopFault.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择停机故障点",
            modal: true
        })
    </script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Request = function (result) {
            App.direct.setFaultReason(result, {
                success: function (result) {
                },
                failture: function () {
                }
            });

        }
        var QueryEqmFaultReason = function () {
            var url = "../../BasicInfo/CommonPage/QueryEqmFaultReason.aspx?FaultID=" + App.hidden_stop_fault.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.show();
        }
        Ext.create("Ext.window.Window", {//停机类型查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window",
            height: 460,
            hidden: true,
            width: 450,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmFaultReason.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择故障点原因",
            modal: true
        })

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("Status") == "1") {
                return "x-grid-row-collapsed";
            }
        }
    </script>
    <script type="text/javascript">
        //-------备件代码-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryEqmSparePart_Request = function (record) {//备件代码返回值处理
            if (!App.winAdd.hidden) {
                App.add_sparepart_code.setValue(record.data.SparePartName);
                App.add_sparepart_code.getTrigger(0).show();
                App.hidden_sparepart_code.setValue(record.data.SparePartCode);
            }
            else if (!App.winModify.hidden) {
                App.modify_sparepart_code.setValue(record.data.SparePartName);
                App.modify_sparepart_code.getTrigger(0).show();
                App.hidden_sparepart_code.setValue(record.data.SparePartCode);
            } else {
                App.txt_sparepart_code.setValue(record.data.SparePartName);
                App.txt_sparepart_code.getTrigger(0).show();
                App.hidden_select_sparepart_code.setValue(record.data.SparePartCode);
            }
        }

        var SelectSparePartInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_sparepart_code.setValue("");
                    App.hidden_select_sparepart_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//备件代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmSparePart.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择备件名称",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" />
        <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
        <ext:Viewport ID="vw1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnl1" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="bar1">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel" >
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
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
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".32">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="开始时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                <ext:TimeField ID="dStartTime" runat="server" Width="65"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout" FieldLabel="结束时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                <ext:TimeField ID="dEndTime" runat="server" Width="65"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".23">
                                    <Items>
                                        <ext:ComboBox ID="cbWorkShop" runat="server" FieldLabel="车间"  LabelAlign="Right" LabelWidth="60" Width="180" DisplayField="WorkShopName" ValueField="ObjID" Editable="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storeWorkShop">
                                                    <Model>
                                                        <ext:Model runat="server" ID="mWorkShop">
                                                            <Fields>
                                                                <ext:ModelField Name="ObjID" />
                                                                <ext:ModelField Name="WorkShopName" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>
                                                <Change OnEvent="cbWorkShop_SelectChanged" />
                                            </DirectEvents>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbStopEquip" runat="server" FieldLabel="机台"  LabelAlign="Right" LabelWidth="60" Width="180" DisplayField="EquipName" ValueField="EquipCode">
                                            <Store>
                                                <ext:Store runat="server" ID="storeEquip">
                                                    <Model>
                                                        <ext:Model runat="server" ID="mEquip">
                                                            <Fields>
                                                                <ext:ModelField Name="EquipCode" />
                                                                <ext:ModelField Name="EquipName" />
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
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".45" Border="true">
                                    <Items>
                                        <ext:ComboBox ID="cbStopMainType" runat="server" FieldLabel="停机大类"  LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="ItemName" ValueField="ItemCode" Editable="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storeStopMainType">
                                                    <Model>
                                                        <ext:Model runat="server" ID="Model1">
                                                            <Fields>
                                                                <ext:ModelField Name="ItemCode" />
                                                                <ext:ModelField Name="ItemName" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>
                                                <Change OnEvent="cbStopMainType_SelectChanged" />
                                            </DirectEvents>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbStopType" runat="server" FieldLabel="停机类型"  LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="TypeName" ValueField="TypeCode" Editable="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storeStopType">
                                                    <Model>
                                                        <ext:Model runat="server" ID="Model3">
                                                            <Fields>
                                                                <ext:ModelField Name="TypeCode" />
                                                                <ext:ModelField Name="TypeName" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>
                                                <Change OnEvent="cbStopType_SelectChanged" />
                                            </DirectEvents>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbStopFault" runat="server" FieldLabel="停机故障点"  LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="FaultName" ValueField="FaultCode" Editable="false">
                                            <Store>
                                                <ext:Store runat="server" ID="storeStopFault">
                                                    <Model>
                                                        <ext:Model runat="server" ID="Model4">
                                                            <Fields>
                                                                <ext:ModelField Name="FaultCode" />
                                                                <ext:ModelField Name="FaultName" />
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
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="ShiftName" />
                                        <ext:ModelField Name="ClassName" />
                                        <ext:ModelField Name="StartTime"/>
                                        <ext:ModelField Name="EndTime"/>
                                        <ext:ModelField Name="StopTime" />
                                        <ext:ModelField Name="ReportTime"/>
                                        <ext:ModelField Name="MaintainStartTime"/>
                                        <ext:ModelField Name="MaintainEndTime"/>
                                        <ext:ModelField Name="ReportDuration" />
                                        <ext:ModelField Name="RespondDuration" />
                                        <ext:ModelField Name="MainTypeName" />
                                        <ext:ModelField Name="TypeName" />
                                        <ext:ModelField Name="FaultName" />
                                        <ext:ModelField Name="StopReason" />
                                        <ext:ModelField Name="DealDesc" />
                                        <ext:ModelField Name="Maintainers" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="Status" />
                                        <ext:ModelField Name="StatusName" />
                                        <ext:ModelField Name="AffirmOper" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="StopMainTypeID" />
                                        <ext:ModelField Name="StopTypeID" />
                                        <ext:ModelField Name="FaultID" />
                                        <ext:ModelField Name="ReasonID" />
                                        <ext:ModelField Name="SparePartOutNo" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <%--<ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35"<ext:ModelField Name="ObjID" />/>--%>
                            <ext:Column ID="Column16" runat="server" Text="ObjID" DataIndex="ObjID" Hidden="true"/>
                            <ext:Column ID="clEquip" runat="server" Text="机台" DataIndex="EquipName" Width="60"/>
                            <ext:Column ID="clShiftName" runat="server" Text="班次" DataIndex="ShiftName" Width="40"/>
                            <ext:Column ID="clClassName" runat="server" Text="班组" DataIndex="ClassName" Width="40"/>
                            <ext:Column ID="clStartTime" runat="server" Text="开始时间" DataIndex="StartTime" Width="135"/>
                            <ext:Column ID="clEndTime" runat="server" Text="结束时间" DataIndex="EndTime" Width="135" Visible="false"/>
                            <ext:Column ID="Column1" runat="server" Text="停机间隔(分)" DataIndex="StopTime" Width="80" Visible="false"/>
                            <ext:Column ID="Column2" runat="server" Text="报修时间" DataIndex="ReportTime" Width="135"/>
                            <ext:Column ID="Column13" runat="server" Text="报修间隔(分)" DataIndex="ReportDuration" Width="80"/>
                            <%--<ext:Column ID="Column14" runat="server" Text="人员到位(分)" DataIndex="RespondDuration" Width="80"/>--%>
                            <ext:Column ID="Column3" runat="server" Text="维修开始时间" DataIndex="MaintainStartTime" Width="135"/>
                            <ext:Column ID="Column4" runat="server" Text="维修结束时间" DataIndex="MaintainEndTime" Width="135"/>
                            <ext:Column ID="Column5" runat="server" Text="停机大类" DataIndex="MainTypeName" Width="80"/>
                            <ext:Column ID="Column6" runat="server" Text="停机类型" DataIndex="TypeName" Width="80"/>
                            <ext:Column ID="Column7" runat="server" Text="故障点" DataIndex="FaultName" Width="120"/>
                            <ext:Column ID="Column10" runat="server" Text="维修人" DataIndex="Maintainers" Width="120"/>
                            <ext:Column ID="Column15" runat="server" Text="修复" DataIndex="StatusName" Width="40"/>
                            <ext:Column ID="Column11" runat="server" Text="记录人" DataIndex="UserName" Width="60"/>
                            <ext:Column ID="Column12" runat="server" Text="备注" DataIndex="Remark" Width="120"/>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                    <ext:GridCommand Icon="Add" CommandName="Add" Text="备件领用"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();#{sendStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <Listeners>
                        <CellDblClick Fn="gridPanelCellDblClick" />
                    </Listeners>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Container ID="southPnl" runat="server" Region="South" Layout="BorderLayout" Height ="130" >
                    <Items>
                        <ext:GridPanel ID="detailPnl" Title="故障原因&处理措施" runat="server" Region="West" AutoScroll="true" Height ="130" Flex="3">
                            <Store>
                                <ext:Store ID="detailStore" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <Model>
                                        <ext:Model ID="model5" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="ReasonName" />
                                                <ext:ModelField Name="DealDesc" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="StopReason" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StopReason : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column8" runat="server" Text="故障原因名称" DataIndex="ReasonName" Flex="1" />
                                    <ext:Column ID="Column9" runat="server" Text="处理措施" DataIndex="DealDesc" Flex="1"  />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                        <ext:GridPanel ID="sendPnl" Title="领用备件明细信息" runat="server" Region="Center" AutoScroll="true" Height ="130"  Flex="6">
                            <Store>
                                <ext:Store ID="sendStore" runat="server" PageSize="10" OnReadData="SendRowSelect">
                                    <Model>
                                        <ext:Model ID="model7" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="SendNo" />
                                                <ext:ModelField Name="SendDate" />
                                                <ext:ModelField Name="SparePartCode" />
                                                <ext:ModelField Name="StoreOutNum" />
                                                <ext:ModelField Name="SendUser" />
                                                <ext:ModelField Name="DeleteFlag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="OrderID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.ObjID : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel3" runat="server">
                                <Columns>
                                    <ext:Column ID="Column17" runat="server" Text="出库单号" DataIndex="SendNo" Flex="1" />
                                    <ext:DateColumn Format="yyyy-MM-dd" ID="Column18" runat="server" Text="出库日期" DataIndex="SendDate" Flex="1"  />
                                    <ext:Column ID="Column19" runat="server" Text="备件名称" DataIndex="SparePartCode" Flex="1"  />
                                    <ext:Column ID="Column20" runat="server" Text="出库数量" DataIndex="StoreOutNum" Flex="1"  />
                                    <ext:Column ID="Column21" runat="server" Text="领用人" DataIndex="SendUser" Flex="1"  />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Container>
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="维护维修记录" Width="550" Height="545" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID" />
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="停机信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="Container5"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="机台&班次" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtEquip" runat="server" Disabled="true" Width="100" Margins="0 3 0 0"/>
                                                <ext:TextField ID="txtShift" runat="server" Disabled="true" Width="100"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="Container4"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="停机时间" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtStartTime" runat="server" Disabled="true" Width="160" Margins="0 3 0 0"/>
                                                <ext:DisplayField ID="DisplayField1" runat="server" Text="~" Margins="0 3 0 0"/>
                                                <ext:TextField ID="txtEndTime" runat="server" Disabled="true" Width="160"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet2" runat="server" Title="维修信息">
                                    <Items>
                                        <ext:Container ID="Container9"  runat="server" Layout="FormLayout" ColumnWidth=".32">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer8"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="报修时间" AnchorHorizontal="100%">
                                                    <Items>
                                                        <ext:TextField ID="txtReportTime" runat="server" ReadOnly="true" Width="160" />
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer6"  runat="server" Layout="HBoxLayout" FieldLabel="维修时间">
                                                    <Items>
                                                        <ext:DateField ID="dfMaintainStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                        <ext:TimeField ID="dfMaintainStartTime" runat="server" AllowBlank="false" Width="65" Margins="0 3 0 0" />
                                                        <ext:DisplayField ID="DisplayField2" runat="server" Text="~" Margins="0 3 0 0"/>
                                                        <ext:DateField ID="dfMaintainEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                        <ext:TimeField ID="dfMaintainEndTime" runat="server" AllowBlank="false" Width="65"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:Container>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%" FieldLabel="故障类型" LabelWidth="70">
                                            <Items>
                                                <ext:ComboBox ID="cbFaultType" runat="server" Width="150" DisplayField="ItemName" ValueField="ItemCode">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeFaultType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model8">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ItemCode" />
                                                                        <ext:ModelField Name="ItemName" />
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
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="维修人" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TriggerField ID="txtMaintainers" runat="server" Editable="false" Width="400" Enabled="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryBasUsers" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="备注">
                                            <Items>
                                                <ext:TextField ID="txtRemark" runat="server" Width="400" MaxLength="200"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%" >
                                            <Items>
                                                <ext:TriggerField ID="cbType" runat="server" Width="230" FieldLabel="停机类型"  LabelWidth="70" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEqmStopType" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="cbFault" runat="server" Width="225" FieldLabel="故障点" LabelWidth="60" Margins="0 0 0 20" >
                                                     <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEqmStopFault" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:Container ID="Container6"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%" Margins="0 0 0 0">
                                            <Items>
                                                <ext:Container ID="Container7"  runat="server" Layout="VBoxLayout" AnchorHorizontal="100%" Flex="1" Margins="0 0 0 0">
                                                    <Items>
                                                        <ext:TriggerField ID="cbReason" runat="server" Width="400" Editable="false"  FieldLabel="故障原因"  LabelWidth="70">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="QueryEqmFaultReason" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                    </Items>
                                                </ext:Container>
                                                
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8"  runat="server" Layout="VBoxLayout" AnchorHorizontal="100%" Flex="1" Margins="0 0 0 0">
                                            <Items>
                                                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" AutoScroll="true" Height ="130">
                                                    <Store>
                                                        <ext:Store ID="reasonStore" runat="server" PageSize="10">
                                                            <Model>
                                                                <ext:Model ID="model2" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ObjID" />
                                                                        <ext:ModelField Name="ReasonName" />
                                                                        <ext:ModelField Name="DealDesc" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel ID="columnModel" runat="server">
                                                        <Columns>
                                                            <ext:Column ID="reason_name" runat="server" Text="故障原因名称" DataIndex="ReasonName" Width="235"  />
                                                            <ext:Column ID="deal_desc" runat="server" Text="处理措施" DataIndex="DealDesc" Width="235"  />
                                                        </Columns>
                                                    </ColumnModel>
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>       
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加备件领用信息"
                    Width="900" Height="330" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server" Layout="ColumnLayout">
                                    <Items>
                                        <ext:DateField ID="add_send_date" runat="server" FieldLabel="领用日期" LabelAlign="Right" Editable="false" ColumnWidth="0.25" />
                                        <ext:TriggerField ID="add_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Right" Editable="false" AllowBlank="false" ColumnWidth="0.25" >
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="SelectSparePartInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:NumberField ID="add_number" runat="server" FieldLabel="出库数量" MinValue="1" LabelAlign="Right"  ColumnWidth="0.2" />
                                        <ext:TriggerField ID="add_send_user" runat="server" FieldLabel="领取人" LabelAlign="Right" Editable="false"  AllowBlank="false"  ColumnWidth="0.25" >
                                                <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="SelectUserInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:Button ID="storeOutBtn" runat="server" Icon="Add" Text="添加"  >
                                            <DirectEvents>
                                                <Click OnEvent="Store_Out_Btn_Click_Event" />
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>     
                            <Items>
                                <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" Height="200" AutoScroll="true">
                                    <Store>
                                        <ext:Store ID="store1" runat="server" PageSize="15">
                                            <Model>
                                                <ext:Model ID="model6" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="SendDate" Type="Date" />
                                                        <ext:ModelField Name="SparePartCode" />
                                                        <ext:ModelField Name="SparePartModel" />
                                                        <ext:ModelField Name="StoreOutNum" />
                                                        <ext:ModelField Name="SendUser" />
                                                        <ext:ModelField Name="Remark" />
                                                        <ext:ModelField Name="SendNo" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                            <ext:DateColumn Format="yyyy-MM-dd" ID="send_date" runat="server" Text="出库日期" DataIndex="SendDate" Width="120"  />
                                            <ext:Column ID="sparepart_code" runat="server" Text="备件名称" DataIndex="SparePartModel" Width="150"  />
                                            <ext:Column ID="send_no" runat="server" Text="当前库存数量" DataIndex="SendNo" Width="100"  />
                                            <ext:Column ID="store_out_num" runat="server" Text="出库数量" DataIndex="StoreOutNum" Width="100"  />
                                            <ext:Column ID="send_user" runat="server" Text="领取人" DataIndex="Remark" Width="100"  />
                                            <ext:CommandColumn ID="commandCol" runat="server" Width="80" Text="操作" Align="Center">
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
                                </ext:GridPanel>
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
                        <Show Handler="for(var i=0;i<#{vw1}.items.length;i++){#{vw1}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vw1}.items.length;i++){#{vw1}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>         
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hidden_stop_type" runat="server" />
        <ext:Hidden ID="hidden_stop_fault" runat="server" />
        <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Hidden ID="hidden_DealDesc" runat="server" />
        <ext:Hidden ID="hidden_ObjID" runat="server" />
        <ext:Hidden ID="hidden_send_user"  runat="server"></ext:Hidden>
        <ext:Hidden ID="hidden_sparepart_code"  runat="server" />
        <ext:Hidden ID="hidden_select_sparepart_code"  runat="server"></ext:Hidden>
        <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
        <ext:Hidden ID="hidden_storeOutInfo" runat="server" />
    </form>
</body>
</html>
