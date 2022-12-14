

Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window",
    hidden: true,
    width: 360,
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
            App.txtPptMaterial.getStore().reload();
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
    var ss = "车次：" + record.get('LotIndex') + '<br />';
    ss = ss + "密炼能量：" + record.get('MixingEner') + '<br />';
    ss = ss + "投入设计重量：" + record.get('InSetWeight') + '<br />';
    ss = ss + "投入实际重量：" + record.get('InRealWeight') + '<br />';
    ss = ss + "密炼标准时间：" + record.get('StandTime') + '<br />';
    ss = ss + "密炼实际时间：" + record.get('DoneRtime') + '<br />';
    ss = ss + "产出设计重量：" + record.get('OutSetWeight') + '<br />';
    ss = ss + "产出实际重量：" + record.get('OutRealWeight') + '<br />';
    tip.setTitle(ss);
}


function TipsEnerRenderer(tip, record) {
    var ss = "车次：" + record.get('LotIndex') + '<br />';
    ss = ss + "能量：" + record.get('MixingEner') + '<br />';
    tip.setTitle(ss);
}


function TipsWeightRenderer(tip, record) {
    var ss = "车次：" + record.get('LotIndex') + '<br />';
    ss = ss + "设计重量：" + record.get('InSetWeight') + '<br />';
    ss = ss + "实际重量：" + record.get('InRealWeight') + '<br />';
    tip.setTitle(ss);
}

function TipsDoneTimeRenderer(tip, record) {
    var ss = "车次：" + record.get('LotIndex') + '<br />';
    ss = ss + "标准时间：" + record.get('StandTime') + '<br />';
    ss = ss + "实际时间：" + record.get('DoneRtime') + '<br />';
    tip.setTitle(ss);
}


function TipsShiftWeighRenderer(tip, record) {
    var ss = "车次：" + record.get('LotIndex') + '<br />';
    ss = ss + "设计重量：" + record.get('OutSetWeight') + '<br />';
    ss = ss + "实际重量：" + record.get('OutRealWeight') + '<br />';
    tip.setTitle(ss);
}


