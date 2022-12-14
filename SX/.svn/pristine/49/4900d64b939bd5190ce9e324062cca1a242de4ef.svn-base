

var refreshMain = function (closeMe) {
    var tabid = "id=Manager_Technology_Manage_MaterialRecipeDetail_Default";
    var tabp = parent.App.mainTabPanel;
    if (closeMe) {
        App.btnSave.disabled = true;
        App.btnSave.setDisabled(true);
        var tab = tabp.getComponent(tabid);
        if (tab) {
            tab.close();
        }
    }
    tabid = "id=47";
    tab = tabp.getComponent(tabid);
    if (tab) {
        //tab.reload(true);
        tabp.setActiveTab(tab);
        var window = tab.body.dom.firstChild.contentWindow;
        window.refreshLoaction();
    }
    return false;
}

var pnlMainOnShow = function () {
    var setHtml = function (pnl, html) {
        if (!pnl) {
            return;
        }
        if (pnl.getBody()) {
            pnl.getBody().update(html);
        } else {
            pnl.html = html;
        }
    }
    var recipe = App.hiddenRecipeObjID.getValue();
    var command = App.hiddenCommandID.getValue();
    var material = App.hiddenMaterialID.getValue();
    var canEdit = 1;
    if (App.btnSave.disabled) {
        canEdit = 0;
    }
    var url = "Recipe=" + recipe + "&Command=" + command + "&Material=" + material + "&canEdit=" + canEdit;
    var html = "<iframe src='Main.aspx?" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    setHtml(App.pnlMain, html);
    html = "<iframe src='Weight.aspx?" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    setHtml(App.pnlWeight, html);
    html = "<iframe src='Mixing.aspx?" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    setHtml(App.pnlMixing, html);
    html = "<iframe src='AdvanceSeparateWeight.aspx?" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    setHtml(App.pnlAdvanceSeparateWeight, html);
    html = "<iframe src='OpenMixing.aspx?" + url + "' width=100% height=100% scrolling=no frameborder=0></iframe>";
    setHtml(App.pnlOpenMixing, html);
    html = "<iframe src='QDrug.aspx?" + url + "' width=100% height=100% scrolling=no frameborder=0></iframe>";
    setHtml(App.pnlQDrug, html);
    html = "<iframe src='MILL.aspx?" + url + "' width=100% height=456 scrolling=no frameborder=0></iframe>";
    setHtml(App.pnlMILL, html);
}

//创建配方store信息对象方法
var newRecipeStore = function () {
    return {
        main: undefined,
        mixing: undefined,
        weight: undefined,
        advance: undefined,
        mix0: undefined,
        mix1: undefined,
        mix2: undefined,
        mix3: undefined,
        mix4: undefined,
        mix5: undefined,
        mix6: undefined,
        QDrug: undefined,
        QDrug2: undefined,
        CMILL: undefined,
        MILL: undefined
    }
}

//获得基础信息的store方法
var getMainStore = function () {
    var me = App.pnlMain.getBody().dom.firstChild.contentWindow.App;
    var store = newRecipeStore();
    var AuditUser = "";
    for (var i = 0; i < me.CheckboxGroupAuditUser.items.items.length; i++) {
        var item = me.CheckboxGroupAuditUser.items.items[i];
        if (item && item.checked) {
            AuditUser = AuditUser + item.inputValue + "|";
        }
    }
    try {;
        var main = {
            RecipeName: me.txtRecipeName.getValue(),   //配方编号
            RecipeMaterialCode: me.txtRecipeMaterialCode.getValue(),   //物料名称
            RecipeEquipCode: me.txtRecipeEquipCode.getValue(),   //机台名称
            RecipeType: me.txtRecipeType.getValue(),   //配方类型
            RecipeState: me.txtRecipeState.getValue(),   //配方状态
            RecipeVersionID: me.txtRecipeVersionID.getValue(),   //版本号
            LotDoneTime: me.txtLotDoneTime.getValue(),   //每车标准时间
            LotTotalWeight: me.txtLotTotalWeight.getValue(),   //配方总重
            ShelfLotCount: me.txtShelfLotCount.getValue(),   //每架车数
            OverTimeSetTime: me.txtOverTimeSetTime.getValue(),   //超时排胶时间
            OverTempSetTemp: me.txtOverTempSetTemp.getValue(),   //紧急排胶温度
            OverTempMinTime: me.txtOverTempMinTime.getValue(),   //超温排胶最短时间
            InPolyMaxTemp: me.txtInPolyMaxTemp.getValue(),   //最高进胶温度
            InPolyMinTemp: me.txtInPolyMinTemp.getValue(),   //最低进胶温度
            MakeUpTemp: me.txtMakeUpTemp.getValue(),   //补偿温度
            CarbonRecycleType: me.txtCarbonRecycleType.getValue(),   //炭黑是否回收
            FactoryCode: me.txtFactory.getValue(),
            CloseCC: me.ComboBoxCC.getValue(),  
            JieDuan: me.txJieDuan.getValue(),   
            CarbonRecycleTime: me.txtCarbonRecycleTime.getValue(),   //炭黑回收时间
            IsUseAreaTemp: me.txtIsUseAreaTemp.getValue(),   //使用三区温度
            SideTemp: me.txtSideTemp.getValue(),   //侧壁温度
            RollTemp: me.txtRollTemp.getValue(),   //转子温度
            SAPVersionID: me.txtSapVersionId.getValue(), //sap版本号
            //RollTempDiff: me.txtRollTempDiff.getValue(),   //转子温差
            DdoorTemp: me.txtDdoorTemp.getValue(),   //卸料门温度
            CanAuditUser: AuditUser,  //审核人
            command: ""  //操作类型
        }
        store.main = main;
    }
    catch (e) {
    }
    return store;
}

//获得称量数据的store信息方法
var getWeightStore = function () {
    var store = newRecipeStore();
    try {
        var weightStore = new Array();
        var panel = App.pnlWeight.getBody().dom.firstChild.contentWindow.App.pnlWeight;
        for (var i = 0; i < panel.items.items.length; i++) {
            var item = panel.items.items[i];
            item.submitData();
            for (var j = 0; j < item.store.data.length; j++) {
                item.store.data.items[j].data.WeightType = item.store.proxy.pageParam;
            }
            weightStore.push(item.store);
        }
        store.weight = weightStore;
    }
    catch (e) {
    }
    return store;
}


//获得预分散数据的store信息方法
var getAdvanceStore = function () {
    var store = newRecipeStore();
    try {
        var advanceStore = new Array();
        var panel = App.pnlAdvanceSeparateWeight.getBody().dom.firstChild.contentWindow.App.pnlWeightAdvanceSeparate;
        for (var i = 0; i < panel.items.items.length; i++) {
            var item = panel.items.items[i];
            item.submitData();
            for (var j = 0; j < item.store.data.length; j++) {
                item.store.data.items[j].data.WeightType = item.store.proxy.pageParam;
            }
            advanceStore.push(item.store);
        }
        store.advance = advanceStore;
    }
    catch (e) {
    }
    return store;
}
//获得Q药品的store信息方法
var getQDrugStore = function () {
    var store = newRecipeStore();
    try {
        var window = App.pnlQDrug.getBody().dom.firstChild.contentWindow;
        window.App.gridPanelQDrug.submitData();
        store.QDrug = window.App.gridPanelQDrug.store;

        store.QDrug2 = window.App.gridPanelRERUB.store;
//        alert(storeToJson(store.QDrug));
    }
    catch (e) {

    }
    return store;
}
//胶冷的store信息方法
var getCoolMILLStore = function () {
    var store = newRecipeStore();
    try {
        var me2 = App.pnlQDrug.getBody().dom.firstChild.contentWindow.App;
        var CMILL = {
       
            TotalWeigh: me2.TotalWeigh.getValue(),
            ErroeAllow: me2.ErroeAllow.getValue(),
            RubErroeAllow: me2.RubErroeAllow.getValue(),
            ErroeAllowMove: me2.ErroeAllowMove.getValue(),
            CTabletSpeed: me2.CTabletSpeed.getValue(),
            CTabletThick: me2.CTabletThick.getValue(),
            CTabletTemp: me2.CTabletTemp.getValue(),
            CTabletWeigh: me2.CTabletWeigh.getValue(),
            CIsUseCode: me2.CIsUseCode.getValue(),
            CIsUseCutter: me2.CIsUseCutter.getValue(),
            BatchWeigh: me2.BatchWeigh.getValue(),
            DrugNum: me2.DrugNum.getValue(),
            DrugTime: me2.DrugTime.getValue(),
            CCutterNum: me2.CCutterNum.getValue()

        }
        store.CMILL = CMILL;
    }
    catch (e) {

    }
    return store;
}
//获得PMILL和胶冷的store信息方法
var getMILLStore = function () {
      var store = newRecipeStore();



      try {
          var me = App.pnlMILL.getBody().dom.firstChild.contentWindow.App;
//          var me2 = App.pnlQDrug.getBody().dom.firstChild.contentWindow.App;
   

        
        var MILL = {
            Step1SetTime: me.Step1SetTime.getValue(),  
            Step2SetTime: me.Step2SetTime.getValue(),  
            Step3SetTime: me.Step3SetTime.getValue(),
            Step4SetTime: me.Step4SetTime.getValue(),
            Step5SetTime: me.Step5SetTime.getValue(),  
            Step6SetTime: me.Step6SetTime.getValue(),  
            Step7SetTime: me.Step7SetTime.getValue(),
            Step8SetTime: me.Step8SetTime.getValue(),
            Step1SetRollerSpace: me.Step1SetRollerSpace.getValue(),  
            Step2SetRollerSpace: me.Step2SetRollerSpace.getValue(),  
            Step3SetRollerSpace: me.Step3SetRollerSpace.getValue(),  
            Step4SetRollerSpace: me.Step4SetRollerSpace.getValue(), 
            Step5SetRollerSpace: me.Step5SetRollerSpace.getValue(),  
            Step6SetRollerSpace: me.Step6SetRollerSpace.getValue(), 
            Step7SetRollerSpace: me.Step7SetRollerSpace.getValue(),  
            Step8SetRollerSpace: me.Step8SetRollerSpace.getValue(), 
            PInitalTime: me.PInitalTime.getValue(),  
            PEndTime: me.PEndTime.getValue(), 
            PMixTime: me.PMixTime.getValue(),  
            PStartV: me.PStartV.getValue(), 
            PEndV: me.PEndV.getValue(),  
            PMixTemp: me.PMixTemp.getValue(), 
            PRatioCoef: me.PRatioCoef.getValue(),  
            PIsInject: me.PIsInject.getValue(),  
            PStartSpeed: me.PStartSpeed.getValue(),

             SStep1SetTime: me.SStep1SetTime.getValue(),  
            SStep2SetTime: me.SStep2SetTime.getValue(),  
            SStep3SetTime: me.SStep3SetTime.getValue(),
            SStep4SetTime: me.SStep4SetTime.getValue(),
            SStep5SetTime: me.SStep5SetTime.getValue(),  
            SStep6SetTime: me.SStep6SetTime.getValue(),  
            SStep7SetTime: me.SStep7SetTime.getValue(),
            SStep8SetTime: me.SStep8SetTime.getValue(),
            SStep1SetRollerSpace: me.SStep1SetRollerSpace.getValue(),  
            SStep2SetRollerSpace: me.SStep2SetRollerSpace.getValue(),  
            SStep3SetRollerSpace: me.SStep3SetRollerSpace.getValue(),  
            SStep4SetRollerSpace: me.SStep4SetRollerSpace.getValue(), 
            SStep5SetRollerSpace: me.SStep5SetRollerSpace.getValue(),  
            SStep6SetRollerSpace: me.SStep6SetRollerSpace.getValue(), 
            SStep7SetRollerSpace: me.SStep7SetRollerSpace.getValue(),  
            SStep8SetRollerSpace: me.SStep8SetRollerSpace.getValue(), 
            SStep1SetVelocity: me.SStep1SetVelocity.getValue(),  
            SStep2SetVelocity: me.SStep2SetVelocity.getValue(),  
            SStep3SetVelocity: me.SStep3SetVelocity.getValue(),  
            SStep4SetVelocity: me.SStep4SetVelocity.getValue(),  
            SStep5SetVelocity: me.SStep5SetVelocity.getValue(),  
            SStep6SetVelocity: me.SStep6SetVelocity.getValue(),  
            SStep7SetVelocity: me.SStep7SetVelocity.getValue(),  
            SStep8SetVelocity: me.SStep8SetVelocity.getValue(),  
            SIsInject: me.SIsInject.getValue(),  
            SBeforeFOpen: me.SBeforeFOpen.getValue(),
            SIsPutInto: me.SIsPutInto.getValue(),
           SJDUSE: me.SJDUSE.getValue(),
           PJDUSE: me.PJDUSE.getValue(),  
            SMixTime: me.SMixTime.getValue(), 
            SInStartTime: me.SInStartTime.getValue(),  
            SAfterFOpen: me.SAfterFOpen.getValue(), 
            SInTimeLen: me.SInTimeLen.getValue(),  
            SMixTemp: me.SMixTemp.getValue(),
            SFUse: me.SFUse.getValue(),
            SFUseTime: me.SFUseTime.getValue()
//            ,  
//            TotalWeigh: me2.TotalWeigh.getValue(),
//            ErroeAllow: me2.ErroeAllow.getValue(),
//            ErroeAllowMove: me2.ErroeAllowMove.getValue(),
//            CTabletSpeed: me2.CTabletSpeed.getValue(),
//            CTabletThick: me2.CTabletThick.getValue(),
//            CTabletTemp: me2.CTabletTemp.getValue(),
//            CTabletWeigh: me2.CTabletWeigh.getValue(),
//            CIsUseCode: me2.CIsUseCode.getValue(),
//            CIsUseCutter: me2.CIsUseCutter.getValue(),
//            BatchWeigh: me2.BatchWeigh.getValue(),
//            DrugNum: me2.DrugNum.getValue(),
//            DrugTime: me2.DrugTime.getValue(),
//             CCutterNum: me2.CCutterNum.getValue()

        }
        store.MILL = MILL;
    }
    catch (e) {
    }
    return store;


}

//获得混炼信息的store信息方法
var getMixingStore = function () {
    var store = newRecipeStore();
    try {
        var window = App.pnlMixing.getBody().dom.firstChild.contentWindow;
        window.App.gridPanelMinxing.submitData()
        store.mixing = window.App.gridPanelMinxing.store;

    }
    catch (e) {

    }
    return store;
}

//获得开炼信息的store信息方法
var getOpenMixingStore = function () {
    var store = newRecipeStore();
    try {
        var window = App.pnlOpenMixing.getBody().dom.firstChild.contentWindow;
        window.App.gridPanelMinxing0.submitData();
        window.App.gridPanelMinxing1.submitData();
        window.App.gridPanelMinxing2.submitData();
        window.App.gridPanelMinxing3.submitData();
        window.App.gridPanelMinxing4.submitData();
        window.App.gridPanelMinxing5.submitData();
        window.App.gridPanelMinxing6.submitData();
        store.mix0 = window.App.gridPanelMinxing0.store;
        store.mix1 = window.App.gridPanelMinxing1.store;
        store.mix2 = window.App.gridPanelMinxing2.store;
        store.mix3 = window.App.gridPanelMinxing3.store;
        store.mix4 = window.App.gridPanelMinxing4.store;
        store.mix5 = window.App.gridPanelMinxing5.store;
        store.mix6 = window.App.gridPanelMinxing6.store;
    }
    catch (e) {
        //alert("getOpenMixingStore：" + e);
    }
    return store;
}

var getStore = function () {
    var store = newRecipeStore();
    var main = getMainStore();
    store.main = main.main;
    var weight = getWeightStore();
    store.weight = weight.weight;
    var mixing = getMixingStore();
    store.mixing = mixing.mixing;
    var advance = getAdvanceStore();
    store.advance = advance.advance;
    var QDrug = getQDrugStore();
    store.QDrug = QDrug.QDrug;
    store.QDrug2 = QDrug.QDrug2;
    var MILL = getMILLStore();
    store.MILL = MILL.MILL;
    var CMILL = getCoolMILLStore();
    store.CMILL = CMILL.CMILL;
    var mixOpenStore = getOpenMixingStore();
    store.mix0 = mixOpenStore.mix0;
    store.mix1 = mixOpenStore.mix1;
    store.mix2 = mixOpenStore.mix2;
    store.mix3 = mixOpenStore.mix3;
    store.mix4 = mixOpenStore.mix4;
    store.mix5 = mixOpenStore.mix5;
    store.mix6 = mixOpenStore.mix6;
    return store;
}
var storeToJson = function (store) {
    if (!store) {
        return '';
    }
    if (store.type == "store") {
        arr = new Array();
        Ext.each(store.data.items, function (record) {
            arr.push(record.data);
        });
        return Ext.encode(arr);
    }
    else {
        return Ext.encode(store)
    }
}
var weightStoreToJson = function (storeArray) {
    if (!storeArray) {
        return '';
    }
    var arr = new Array();
    for (var i = 0; i < storeArray.length; i++) {
        var store = storeArray[i];
        if (store.type == "store") {
            Ext.each(store.data.items, function (record) {
                arr.push(record.data);
            });
        }
    }
    return Ext.encode(arr);
}
var doSave = function (btn) {
    if (btn != "yes") {
        return;
    }
    var after = function () {
        App.waitProgressWindow.close();
    }
    var before = function () {
        App.waitProgressWindow.show();
    }
    before();
    App.btnSave.setDisabled(true);
    try {
        var store = getStore();
        store.main.command = "Save";
//       alert(storeToJson(store.QDrug)); return;
//        console.log(storeToJson(store.QDrug)); return;
        App.direct.SaveJsonInfo(storeToJson(store.main), storeToJson(store.mixing), weightStoreToJson(store.weight), weightStoreToJson(store.advance),
            storeToJson(store.mix0), storeToJson(store.mix1), storeToJson(store.mix2), storeToJson(store.mix3),
            storeToJson(store.mix4), storeToJson(store.mix5), storeToJson(store.mix6), storeToJson(store.QDrug), storeToJson(store.MILL), storeToJson(store.QDrug2), storeToJson(store.CMILL), {
                success: function (result) {
                    after();
                    App.btnSave.setDisabled(false);
                    if (result == "") {
                        Ext.Msg.alert('成功', "工艺配方保存成功！", function (btn) { refreshMain(true) });
                    } else {
                        Ext.Msg.alert('失败', result);
                    }
                },
                failure: function (errorMsg) {
                    App.btnSave.setDisabled(false);
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
    }
    catch (ex) {
        App.btnSave.setDisabled(false);
        after();
    }
}
var Save = function () {
    Ext.Msg.confirm("提示", '您确定需要保存此工艺配方信息？', function (btn) { doSave(btn); });
}

var isCanClose = function () {
    return App.btnSave.disabled
}

var onTabClose = function () {
    if (!isCanClose()) {
        return "正在编辑配方明细信息不能关闭当前页面";
    }
    return "";
}


var setEditable = function () {
    var can = App.btnSave.disabled;
    try {
        App.pnlMain.getBody().dom.firstChild.contentWindow.SetEditable(!can);
        App.pnlMILL.getBody().dom.firstChild.contentWindow.SetEditable(!can);
        App.pnlQDrug.getBody().dom.firstChild.contentWindow.SetEditable(!can);
        
    }
    catch (ex) {
    }
}

var CanCancelSave = function (btn) {
    if (btn != "yes") {
        return;
    }
    App.btnSave.setDisabled(!App.btnSave.disabled);
    App.btnCanSave.setText("编辑");
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getActiveTab();
    if ((App.btnAuditPmtRecipePass) && (!App.btnAuditPmtRecipePass.hidden)) {
        tab.reload();
    }
    else {
        tab.close();
    }
}

var SetCanSave = function () {
    if (!App.btnSave.disabled) {
        Ext.Msg.confirm("提示", '您确定取消工艺配方编辑状态？', function (btn) { CanCancelSave(btn); });
    }
    else {
        App.btnSave.setDisabled(false);
        App.btnAuditPmtRecipePass.setDisabled(true);
        App.btnCanSave.setText("取消");
        setEditable();
        var me = App.pnlMain.getBody().dom.firstChild.contentWindow.App;
        for (var i = 0; i < me.CheckboxGroupAuditUser.items.items.length; i++) {
            var item = me.CheckboxGroupAuditUser.items.items[i];
            item.setValue(false);
        }
    }
}



var dobtnAuditPmtRecipePassClick = function (btn) {
    if (btn != "yes") {
        return;
    }
    var after = function () {
        App.waitProgressWindow.close();
    }
    var before = function () {
        App.waitProgressWindow.show();
    }
    before();
    try {
        App.direct.AuditPmtRecipe({
            success: function (result) {
                after();
                if (result == "") {
                    Ext.Msg.alert('成功', "工艺配方审核成功！", function (btn) { refreshMain(true); });
                } else {
                    Ext.Msg.alert('失败', result);
                }
            },
            failure: function (errorMsg) {
                after();
                Ext.Msg.alert('错误', errorMsg);
            }
        });
    }
    catch (ex) {
        after();
    }
}


var btnAuditPmtRecipePassClick = function (sender, e, fn) {
    Ext.Msg.confirm("提示", '您确定审核通过此配方工艺信息？', function (btn) { dobtnAuditPmtRecipePassClick(btn); });
}
//保存调解塑解剂按钮点击 yuany
var btn_edit_weight_save = function () {
    var after = function () {
        App.waitProgressWindow.close();
    }
    var before = function () {
        App.waitProgressWindow.show();
    }
    var values = Ext.encode(App.gridPanelEditWeight.getRowsValues());
    before();
    App.direct.BtnEditWeightSave_Click(values, {
        success: function (result) {
            if (result == "") {
                after();
                Ext.Msg.alert('成功', "调节塑解剂重量及补偿温度成功！", function () {
                    var tabid = "id=Manager_Technology_Manage_MaterialRecipeDetail_Default";
                    var tabp = parent.App.mainTabPanel;
                    var tab = tabp.getComponent(tabid);
                    if (tab) {
                        tab.reload(true);
                    }
                });
            } else {
                after();
                Ext.Msg.alert('失败', result);
            }
        },
        failure: function (errorMsg) {
            after();
            Ext.Msg.alert('错误', errorMsg);
        }
    });
}

//点击调整塑解剂重量按钮。
var btn_edit_sujieji_weight_click = function () {
    var values = Ext.encode(App.gridPanelEditWeight.getRowsValues());
    App.direct.btnEditSuJieJiWeightClick(values, {
        success: function (result) {
            if (result == "") {
                App.winEditWeight.show();
            } else {
                Ext.Msg.alert('提示', result);
            }
        },
        failure: function (errorMsg) {
            Ext.Msg.alert('错误', errorMsg);
        }
    });
}