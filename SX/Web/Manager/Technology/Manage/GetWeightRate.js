

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
    App.cbxEquip.getTrigger(0).show();
    App.hiddenRecipeEquipCode.setValue(record.data.EquipCode);
    App.cbxEquip.setValue(record.data.EquipName);
}
var QueryEquipInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenRecipeEquipCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
            break;
    }
}




Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=2,3,4,5,6' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
    App.cbxmater.getTrigger(0).show();
    App.hiddenmater.setValue(record.data.MaterialCode);
    App.cbxmater.setValue(record.data.MaterialName);
}
var QueryMaterialInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenmater.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
            break;
    }
}

