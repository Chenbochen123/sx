<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_SmallMaterialWeigh_SmallWeigh, App_Web_s5lbchza" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
     <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">

        var template = '<span style="color:{0};">{1}</span>';
        var change = function (value, cellmeta, record, rowIndex, columnIndex, store) {
            try {
                if (store.data.items.length > 0) {
                    if (record.data.物料 == "误差(±)" || record.data.物料 == "标准" || record.data.物料 == "平均" || record.data.物料 == "最大" || record.data.物料 == "最小" || record.data.物料 == "总计") return value;
                    var standardValue = Ext.JSON.encodeValue(store.data.items[0].data).substr(1, Ext.JSON.encodeValue(store.data.items[0].data).length - 2).split(',')[columnIndex].split(':')[1];

                    var errValue = Ext.JSON.encodeValue(store.data.items[1].data).substr(1, Ext.JSON.encodeValue(store.data.items[1].data).length - 2).split(',')[columnIndex].split(':')[1];
                    return Ext.String.format(template, (Math.abs(value - standardValue)) > errValue ? "red" : "black", value);
                }
            }
            catch (exception) {
                alert(exception);
            }
        };

        // 改变行式样   
        var getRowClass = function (r) {
            var d = r.data;
            var flag = parseInt(d.物料);
            if (d.物料 == '标准' || d.物料 == '误差(±)' || d.物料 == '平均' || d.物料 == '最大' || d.物料 == '最小' || d.物料 == '总计') {
                return "indoor-t";
            }
        };
    </script>
     <style type="text/css">
        .indoor-t .x-grid-cell
        {
            background-color: Yellow !important;
        }
        .indoor-f
        {
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
           <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
        <Items>
            <ext:GridPanel ID="pnlDetailList" runat="server" Region="Center" MarginsSummary="0 5 5 5">
                  <TopBar>
                    <ext:Toolbar runat="server" ID="tbChkBill">
                        <Items>
                     
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Store>
                    <ext:Store ID="storeDetail" runat="server" IgnoreExtraFields="false">
                        <Model>
                            <ext:Model ID="Model1" runat="server">
                                <Fields>
                                    <ext:ModelField Name="物料" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <SelectionModel>
                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                </SelectionModel>
                <View>
                    <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                        <GetRowClass Fn="getRowClass" />
                    </ext:GridView>
                </View>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
