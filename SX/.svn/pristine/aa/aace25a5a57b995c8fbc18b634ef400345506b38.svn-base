

Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window",
    hidden: true,
    width: 400,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryPmtRecipe.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择工艺配方",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryPmtRecipe_Request = function (record) {
    App.txtPmtRecipe.getTrigger(0).show();
    App.hiddenPmtRecipeID.setValue(record.data.ObjID);
    App.txtPmtRecipe.setValue(record.data.MaterialName);
}

var QueryPmtRecipeWindowShow = function () {
    var equipName = App.txtEquipName.getValue();
    if (equipName.length == 0) {
        Ext.Msg.alert('提示', "请选择机台");
        return;
    }
    var window = App.Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window;
    var html = "<iframe src='../../BasicInfo/CommonPage/QueryPmtRecipe.aspx?EquipName=" + equipName + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    if (window.getBody()) {
        window.getBody().update(html);
    } else {
        window.html = html;
    }
    window.show();
}

var QueryPmtRecipeInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenPmtRecipeID.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            QueryPmtRecipeWindowShow();
            break;
    }
}


Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
    App.txtEquipName.getTrigger(0).show();
    App.hiddenEquipCode.setValue(record.data.EquipCode);
    App.txtEquipName.setValue(record.data.EquipName);
}
var QueryEquipInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenEquipCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
            break;
    }
}



function saveChart(btn) {
    var url = '../../ReportCenter/SvgToImage.aspx';
    Ext.MessageBox.confirm('提示', '确定将当前的曲线图导出为图片？', function (choice) {
        if (choice == 'yes') {
            App.Chart1.save({
                type: 'image/png',
                url: url
            });
        }
    });
}
function printChart(btn) {
    $("#Chart1").printArea();
}


function TipsRenderer(tip, record) {
    var ss = "时间：" + record.data.SecondSpan + '<br />';
    ss = ss + "温度：" + record.get('MixingTemp') + '<br />';
    ss = ss + "功率：" + record.get('MixingPower') + '<br />';
    ss = ss + "压力：" + record.get('MixingPress') + '<br />';
    ss = ss + "能量：" + record.get('MixingEnergy') + '<br />';
    ss = ss + "转速：" + record.get('MixingSpeed') + '<br />';

    ss = ss + "侧壁温度：" + record.get('L1') + '<br />';
    ss = ss + "转子温度：" + record.get('L2') + '<br />';
    ss = ss + "卸料门温度：" + record.get('L3') + '<br />';

    tip.setTitle(ss);
}


var ShowRptPmtLotInfo = function () {

    var barcode = App.txtShowBarcode.getValue();
    if ((!barcode) || (barcode.length == 0)) {
        Ext.Msg.alert('提示', "没有需要显示的车条码");
        return;
    }
    parent.addTab("id=RptPmtLotInfo", "车报表详细信息[" + barcode + "]", "ReportCenter/RptPmtLotInfo/RptPmtLotInfo.aspx?BarCode=" + barcode, true);

}