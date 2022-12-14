<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipStorageInfo.aspx.cs"
    Inherits="Manager_BasicInfo_EquipmentInfo_EquipStorageInfo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            var name = App.hldname.value;
            if (name == "txtStorageName") {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "modify_yl") {
                App.modify_yl.getTrigger(0).show();
                App.modify_yl.setValue(record.data.StorageName);
                App.modify_yl2.setValue(record.data.StoragePlaceName);
                App.hldmyl2.setValue(record.data.StoragePlaceID);
                App.hldmyl1.setValue(record.data.StorageID);
            } else if (name == "modify_sj") {
                App.modify_sj.getTrigger(0).show();
                App.modify_sj.setValue(record.data.StorageName);
                App.modify_sj2.setValue(record.data.StoragePlaceName);
                App.hldmsj2.setValue(record.data.StoragePlaceID);
                App.hldmsj1.setValue(record.data.StorageID);
            } else if (name == "modify_xl") {
                App.modify_xl.getTrigger(0).show();
                App.modify_xl.setValue(record.data.StorageName);
                App.modify_xl2.setValue(record.data.StoragePlaceName);
                App.hldmxl2.setValue(record.data.StoragePlaceID);
                App.hldmxl1.setValue(record.data.StorageID);
            } else if (name == "modify_mj") {
                App.modify_mj.getTrigger(0).show();
                App.modify_mj.setValue(record.data.StorageName);
                App.modify_mj2.setValue(record.data.StoragePlaceName);
                App.hldmmj2.setValue(record.data.StoragePlaceID);
                App.hldmmj1.setValue(record.data.StorageID);
            } else if (name == "modify_zj") {
                App.modify_zj.getTrigger(0).show();
                App.modify_zj.setValue(record.data.StorageName);
                App.modify_zj2.setValue(record.data.StoragePlaceName);
                App.hldmzj2.setValue(record.data.StoragePlaceID);
                App.hldmzj1.setValue(record.data.StorageID);
            } else if (name == "modify_hg") {
                App.modify_hg.getTrigger(0).show();
                App.modify_hg.setValue(record.data.StorageName);
                App.modify_hg2.setValue(record.data.StoragePlaceName);
                App.hldmhg2.setValue(record.data.StoragePlaceID);
                App.hldmhg1.setValue(record.data.StorageID);
            } else if (name == "modify_no") {
                App.modify_no.getTrigger(0).show();
                App.modify_no.setValue(record.data.StorageName);
                App.modify_no2.setValue(record.data.StoragePlaceName);
                App.hldmno2.setValue(record.data.StoragePlaceID);
                App.hldmno1.setValue(record.data.StorageID);
            } else if (name == "modify_fl") {
                App.modify_fl.getTrigger(0).show();
                App.modify_fl.setValue(record.data.StorageName);
                App.modify_fl2.setValue(record.data.StoragePlaceName);
                App.hldmfl2.setValue(record.data.StoragePlaceID);
                App.hldmfl1.setValue(record.data.StorageID);
            } else if (name == "modify_fp") {
                App.modify_fp.getTrigger(0).show();
                App.modify_fp.setValue(record.data.StorageName);
                App.modify_fp2.setValue(record.data.StoragePlaceName);
                App.hldmfp2.setValue(record.data.StoragePlaceID);
                App.hldmfp1.setValue(record.data.StorageID);
            } else if (name == "modify_th") {
                App.modify_th.getTrigger(0).show();
                App.modify_th.setValue(record.data.StorageName);
                App.modify_th2.setValue(record.data.StoragePlaceName);
                App.hldmth2.setValue(record.data.StoragePlaceID);
                App.hldmth1.setValue(record.data.StorageID);
            }

        }
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            var name = App.hldname.value;

            if (name == "txtStoragePlaceName") {
                App.txtStoragePlaceName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "modify_yl2") {
                App.modify_yl2.getTrigger(0).show();
                App.modify_yl.setValue(record.data.StorageName);
                App.modify_yl2.setValue(record.data.StoragePlaceName);
                App.hldmyl2.setValue(record.data.StoragePlaceID);
                App.hldmyl1.setValue(record.data.StorageID);
            } else if (name == "modify_sj2") {
                App.modify_sj2.getTrigger(0).show();
                App.modify_sj.setValue(record.data.StorageName);
                App.modify_sj2.setValue(record.data.StoragePlaceName);
                App.hldmsj2.setValue(record.data.StoragePlaceID);
                App.hldmsj1.setValue(record.data.StorageID);
            } else if (name == "modify_xl2") {
                App.modify_xl2.getTrigger(0).show();
                App.modify_xl.setValue(record.data.StorageName);
                App.modify_xl2.setValue(record.data.StoragePlaceName);
                App.hldmxl2.setValue(record.data.StoragePlaceID);
                App.hldmxl1.setValue(record.data.StorageID);
            } else if (name == "modify_mj2") {
                App.modify_mj2.getTrigger(0).show();
                App.modify_mj.setValue(record.data.StorageName);
                App.modify_mj2.setValue(record.data.StoragePlaceName);
                App.hldmmj2.setValue(record.data.StoragePlaceID);
                App.hldmmj1.setValue(record.data.StorageID);
            } else if (name == "modify_zj2") {
                App.modify_zj2.getTrigger(0).show();
                App.modify_zj.setValue(record.data.StorageName);
                App.modify_zj2.setValue(record.data.StoragePlaceName);
                App.hldmzj2.setValue(record.data.StoragePlaceID);
                App.hldmzj1.setValue(record.data.StorageID);
            } else if (name == "modify_hg2") {
                App.modify_hg2.getTrigger(0).show();
                App.modify_hg.setValue(record.data.StorageName);
                App.modify_hg2.setValue(record.data.StoragePlaceName);
                App.hldmhg2.setValue(record.data.StoragePlaceID);
                App.hldmhg1.setValue(record.data.StorageID);
            } else if (name == "modify_no2") {
                App.modify_no2.getTrigger(0).show();
                App.modify_no.setValue(record.data.StorageName);
                App.modify_no2.setValue(record.data.StoragePlaceName);
                App.hldmno2.setValue(record.data.StoragePlaceID);
                App.hldmno1.setValue(record.data.StorageID);
            } else if (name == "modify_fl2") {
                App.modify_fl2.getTrigger(0).show();
                App.modify_fl.setValue(record.data.StorageName);
                App.modify_fl2.setValue(record.data.StoragePlaceName);
                App.hldmfl2.setValue(record.data.StoragePlaceID);
                App.hldmfl1.setValue(record.data.StorageID);
            } else if (name == "modify_fp2") {
                App.modify_fp2.getTrigger(0).show();
                App.modify_fp.setValue(record.data.StorageName);
                App.modify_fp2.setValue(record.data.StoragePlaceName);
                App.hldmfp2.setValue(record.data.StoragePlaceID);
                App.hldmfp1.setValue(record.data.StorageID);
            } else if (name == "modify_th2") {
                App.modify_th2.getTrigger(0).show();
                App.modify_th.setValue(record.data.StorageName);
                App.modify_th2.setValue(record.data.StoragePlaceName);
                App.hldmth2.setValue(record.data.StoragePlaceID);
                App.hldmth1.setValue(record.data.StorageID);
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            App.txtEquipName.setValue(record.data.EquipName);
            App.txtEquipName.getTrigger(0).show();

            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.close();
            //App.pageToolBar.doRefresh();
        }
        var QueryStorage = function (field, trigger, index) {//库房添加
            App.hldname.setValue(field.name);
            switch (index) {
                case 0:
                    var name = App.hldname.value;
                    if (name == "txtStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtStoragePlaceName.setValue("");
                        App.txtStoragePlaceName.getTrigger(0).hide();
                        App.hiddenStorageID.setValue("");
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_yl") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_yl.setValue("");
                        App.hldmyl1.setValue("");
                        App.hldmyl2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_sj") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_sj.setValue("");
                        App.modify_sj.getTrigger(0).hide();
                        App.hldmsj1.setValue("");
                        App.hldmsj2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_xl") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_xl.setValue("");
                        App.modify_xl.getTrigger(0).hide();
                        App.hldmxl1.setValue("");
                        App.hldmxl2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_mj") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_mj.setValue("");
                        App.modify_mj.getTrigger(0).hide();
                        App.hldmmj1.setValue("");
                        App.hldmmj2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_zj") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_zj.setValue("");
                        App.modify_zj.getTrigger(0).hide();
                        App.hldmzj1.setValue("");
                        App.hldmzj2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_hg") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_hg.setValue("");
                        App.modify_hg.getTrigger(0).hide();
                        App.hldmhg1.setValue("");
                        App.hldmhg2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_no") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_no.setValue("");
                        App.modify_no.getTrigger(0).hide();
                        App.hldmno1.setValue("");
                        App.hldmno2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_fl") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_fl.setValue("");
                        App.modify_fl.getTrigger(0).hide();
                        App.hldmfl1.setValue("");
                        App.hldmfl2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "modify_fp") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modify_fp.setValue("");
                        App.modify_fp.getTrigger(0).hide();
                        App.hldmfp1.setValue("");
                        App.hldmfp2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    } else if (name == "modify_th") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.modith_fp.setValue("");
                        App.modith_fp.getTrigger(0).hide();
                        App.hldmth1.setValue("");
                        App.hldmth2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }


        var QueryStoragePlace = function (field, trigger, index) {//库位添加
            App.hldname.setValue(field.name);
            var url = "../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }
            switch (index) {
                case 0:

                    if (field.name == "txtStoragePlaceName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");

                    } else if (field.name == "modify_yl2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hldmyl2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");

                    } else if (field.name == "modify_sj2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hldmsj2.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");

                    } else if (field.name == "modify_xl2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmxl2.setValue("");
                    } else if (field.name == "modify_mj2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmmj2.setValue("");
                    } else if (field.name == "modify_hg2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmhg2.setValue("");
                    } else if (field.name == "modify_zj2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmzj2.setValue("");
                    } else if (field.name == "modify_no2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmno2.setValue("");
                    } else if (field.name == "modify_fl2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmfl2.setValue("");
                    } else if (field.name == "modify_fp2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmfp2.setValue("");
                    }
                    else if (field.name == "modify_th2") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        App.hldmth2.setValue("");
                    }
                    break;
                case 1:

                    App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
                    break;
            }
        }
        var QueryEquipInfo = function (field, trigger, index) {

            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=all&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        Ext.create("Ext.window.Window", {//库位带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window",
            height: 460,
            hidden: true,
            width: 360,
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库位",
            modal: true
        })
        Ext.create("Ext.window.Window", {//机台带窗体
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
    </script>
    <script type="text/javascript">
        var clearFilter = function () {
            var field = App.triggerField,
                tree = App.treeDept;
            field.setValue("");
            tree.clearFilter(true);
            tree.getView().focus();
        };
        var loadPage = function (record) {
            App.hidden_select_equip_type.setValue(record.getId());
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };
        var filterTree = function (tf, e) {
            var tree = App.treeDept,
                text = tf.getRawValue();
            tree.clearFilter();
            if (Ext.isEmpty(text, false)) {
                return;
            }
            if (e.getKey() === Ext.EventObject.ESC) {
                clearFilter();
            } else {
                var re = new RegExp(".*" + text + ".*", "i");
                tree.filterBy(function (node) {
                    return re.test(node.data.text);
                });
            }
        };
    </script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.hidden_select_equip_type.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            return false;
        };

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.设备编号;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmUnit" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="210" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeDept" runat="server" Title="设备类型" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="过滤:" />
                                    <ext:ToolbarSpacer />
                                    <ext:TriggerField ID="triggerField" runat="server" EnableKeyEvents="true">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" />
                                        </Triggers>
                                        <Listeners>
                                            <KeyUp Fn="filterTree" Buffer="250" />
                                            <TriggerClick Handler="clearFilter();" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
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
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
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
                    <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_equip_code" Vtype="integer" runat="server" FieldLabel="编号"
                                                LabelAlign="Right" />
                                            <ext:TriggerField ID="txtEquipName" runat="server" FieldLabel="设备" LabelAlign="Right"
                                                Editable="true">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" Editable="false" FieldLabel="车间" runat="server" LabelAlign="Right">
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnEquipName" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStoragePlace" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxtype" Editable="false" FieldLabel="类型" EmptyText="设备是否维护了库房"
                                                runat="server" LabelAlign="Right">
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="是" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="否" Value="0">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="设备编号" />
                                    <ext:ModelField Name="设备类型" />
                                    <ext:ModelField Name="设备名称" />
                                    <ext:ModelField Name="原料库房" />
                                    <ext:ModelField Name="原料库位" />
                                    <ext:ModelField Name="生胶库房" />
                                    <ext:ModelField Name="生胶库位" />
                                    <ext:ModelField Name="炭黑库房" />
                                    <ext:ModelField Name="炭黑库位" />
                                    <ext:ModelField Name="小料库房" />
                                    <ext:ModelField Name="小料库位" />
                                    <ext:ModelField Name="母胶库房" />
                                    <ext:ModelField Name="母胶库位" />
                                    <ext:ModelField Name="终胶库房" />
                                    <ext:ModelField Name="终胶库位" />
                                    <ext:ModelField Name="合格库房" />
                                    <ext:ModelField Name="合格库位" />
                                    <ext:ModelField Name="不合格库房" />
                                    <ext:ModelField Name="不合格库位" />
                                    <ext:ModelField Name="反炼库房" />
                                    <ext:ModelField Name="反炼库位" />
                                    <ext:ModelField Name="废品库房" />
                                    <ext:ModelField Name="废品库位" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:CommandColumn ID="commandCol" runat="server" Width="80" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="维护">
                                    <ToolTip Text="维护设备和库房的对应关系" />
                                </ext:GridCommand>
                            </Commands>
                            <Listeners>
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                        <ext:Column ID="col1" runat="server" Text="设备编号" DataIndex="设备编号" Width="60" />
                        <ext:Column ID="col2" runat="server" Text="设备类型" DataIndex="设备类型" Width="80" />
                        <ext:Column ID="col3" runat="server" Text="设备名称" DataIndex="设备名称" Width="80" />
                        <ext:Column ID="col4" runat="server" Text="原料库房" DataIndex="原料库房" Width="80" />
                        <ext:Column ID="col5" runat="server" Text="原料库位" DataIndex="原料库位" Width="80" />
                        <ext:Column ID="col6" runat="server" Text="生胶库房" DataIndex="生胶库房" Width="80" />
                        <ext:Column ID="col7" runat="server" Text="生胶库位" DataIndex="生胶库位" Width="80" />
                        <ext:Column ID="Column2" runat="server" Text="炭黑库房" DataIndex="炭黑库房" Width="80" />
                        <ext:Column ID="Column1" runat="server" Text="炭黑库位" DataIndex="炭黑库位" Width="80" />
                        <ext:Column ID="col8" runat="server" Text="小料库房" DataIndex="小料库房" Width="80" />
                        <ext:Column ID="col9" runat="server" Text="小料库位" DataIndex="小料库位" Width="80" />
                        <ext:Column ID="col10" runat="server" Text="母胶库房" DataIndex="母胶库房" Width="80" />
                        <ext:Column ID="col11" runat="server" Text="母胶库位" DataIndex="母胶库位" Width="80" />
                        <ext:Column ID="col12" runat="server" Text="终胶库房" DataIndex="终胶库房" Width="80" />
                        <ext:Column ID="col13" runat="server" Text="终胶库位" DataIndex="终胶库位" Width="80" />
                        <ext:Column ID="col14" runat="server" Text="合格库房" DataIndex="合格库房" Width="80" />
                        <ext:Column ID="col15" runat="server" Text="合格库位" DataIndex="合格库位" Width="80" />
                        <ext:Column ID="col16" runat="server" Text="不合格库房" DataIndex="不合格库房" Width="80" />
                        <ext:Column ID="col17" runat="server" Text="不合格库位" DataIndex="不合格库位" Width="80" />
                        <ext:Column ID="col18" runat="server" Text="反炼库房" DataIndex="反炼库房" Width="80" />
                        <ext:Column ID="col19" runat="server" Text="反炼库位" DataIndex="反炼库位" Width="80" />
                        <ext:Column ID="col20" runat="server" Text="废品库房" DataIndex="废品库房" Width="80" />
                        <ext:Column ID="col21" runat="server" Text="废品库位" DataIndex="废品库位" Width="80" />
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
            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改设备信息"
                Width="600" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5" Layout="ColumnLayout">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:Container ID="Content_2" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TextField LabelAlign="Right" ID="modify_equipid" runat="server" ReadOnly=true Enabled="true"
                                        FieldLabel="设备编号" />
                                    <ext:TriggerField ID="modify_yl" runat="server" FieldLabel="原料库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_sj" runat="server" FieldLabel="生胶库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_xl" runat="server" FieldLabel="小料库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_th" runat="server" FieldLabel="炭黑库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_mj" runat="server" FieldLabel="母胶库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_zj" runat="server" FieldLabel="终胶库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_hg" runat="server" FieldLabel="合格库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_no" runat="server" FieldLabel="不合格库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_fl" runat="server" FieldLabel="反炼库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_fp" runat="server" FieldLabel="废品库房" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TextField LabelAlign="Right" ID="modify_equipname" runat="server" ReadOnly="true" Enabled="true"  FieldLabel="设备名称" />
                                    <ext:TriggerField ID="modify_yl2" runat="server" FieldLabel="原料库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_sj2" runat="server" FieldLabel="生胶库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_xl2" runat="server" FieldLabel="小料库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_th2" runat="server" FieldLabel="炭黑库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_mj2" runat="server" FieldLabel="母胶库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_zj2" runat="server" FieldLabel="终胶库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_hg2" runat="server" FieldLabel="合格库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_no2" runat="server" FieldLabel="不合格库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_fl2" runat="server" FieldLabel="反炼库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TriggerField ID="modify_fp2" runat="server" FieldLabel="废品库位" LabelAlign="Right"
                                        Flex="1" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Container>
                        </Items>
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
            </ext:Window>
            <ext:Hidden ID="hidden_select_equip_type" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmyl1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmyl2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmsj1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmsj2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmxl1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmxl2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmmj1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmmj2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmzj1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmzj2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmhg1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmhg2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmno1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmno2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmfl1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmfl2" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmfp1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmfp2" runat="server">
            </ext:Hidden>
             <ext:Hidden ID="hldmth1" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hldmth2" runat="server">
            </ext:Hidden>
             <ext:Hidden ID="hldname" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
