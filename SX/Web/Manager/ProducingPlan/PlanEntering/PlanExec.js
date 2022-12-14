//插入**************
/****************
计划录入
*/
function zaoinsertPlan() {
    insertPlan(1);
}
function zhonginsertPlan() {
    insertPlan(2);
}
function yeinsertPlan() {
    insertPlan(3);
}
function insertPlan(flag) {

    if (flag == 1) {
        grid = App.ZaoPlanGridPanel;
    }
    else if (flag == 2) {
        grid = App.ZhongGridPanel;
    }
    else if (flag == 3) {
        grid = App.YeGridPanel;
    }
    sm = grid.getSelectionModel().getSelection()[0];
    if (sm) {
        var priLevel = sm.data.PriLevel;
        App.direct.InsertPlan(priLevel, flag, {
            success: function (result) {
                Ext.Msg.alert('提示', result);
            },
            failure: function (errorMsg) {
                Ext.Msg.alert('提示', errorMsg);
            }
        });
    } else {
        Ext.Msg.alert('提示', "请点击选择插入位置!");
    }
}

//新增
function zaoaddPlan() {
    addPlan(1);
}

function zhongaddPlan() {
    addPlan(2);
}
function yeaddPlan() {
    addPlan(3);
}
function addPlan(flag) {
    if (flag == 1) {
        grid = App.ZaoPlanGridPanel;
    }
    else if (flag == 2) {
        grid = App.ZhongGridPanel;
    }
    else if (flag == 3) {
        grid = App.YeGridPanel;
    }
    App.direct.AddPlan(flag, {
        success: function (result) {
            if (result) {

            }
            else {
                // Ext.Msg.alert('错误！', '请检查计划日期或者班次信息！');
            }
        },
        failure: function (errorMsg) {
            Ext.Msg.alert('Failure', errorMsg);
        }
    });
}
///调用方法 参数命令 endname="up"
//上移动 第一个参数 0 表示上移 1表示下移
function zaomoveUpPlan() {
    movePlan(0,1);
}

function zaomoveDnPlan() {
    movePlan(1, 1);
}

function zhongmoveUpPlan() {
    movePlan(0, 2);
 }
 function zhongmoveDnPlan() {
     movePlan(1, 2);
 }

 function yemoveUpPlan() {
     movePlan(0, 3);
 }
 function yemoveDnPlan() {
     movePlan(1, 3);
 }
    function movePlan(upordn,flag) {
        if (flag == 1) {
            grid = App.ZaoPlanGridPanel;
        }
        else if (flag == 2) {
            grid = App.ZhongGridPanel;
        }
        else if (flag == 3) {
            grid = App.YeGridPanel;
        }
        sm = grid.getSelectionModel().getSelection()[0];
        if (sm) {
            var priLevel = sm.data.PriLevel;
            var planId = sm.data.PlanID;
            App.direct.MovePlan(upordn, planId, priLevel, flag, {
                success: function (result) {


                    //if (result=="") {

                    //}
                    //else {
                    //    Ext.Msg.alert('错误！', result);
                    //}





                    if (result) {

                    }
                    else {
                        // Ext.Msg.alert('错误！', '请检查计划日期或者班次信息！');
                    }
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
        }
    }
    function zaoupPlanState() {
        upPlanState(1);
    }
    function zhongupPlanState() {
        upPlanState(2);
    }
    function yeupPlanState() {
        upPlanState(3);
    }
    function upPlanState(flag) {
        if (flag == 1) {
            grid = App.ZaoPlanGridPanel;
        }
        else if (flag == 2) {
            grid = App.ZhongGridPanel;
        }
        else if (flag == 3) {
            grid = App.YeGridPanel;
        }
        store = grid.store;
        sm = grid.getSelectionModel().getSelection()[0];
        if (sm) {
            Ext.Msg.confirm("提示", '您确定要下达该条计划？', function (btn) {
                if (btn == 'yes') {
                    App.direct.UpPlanState(sm.data.PlanID.toString(), {
                        success: function (result) {
                                Ext.Msg.alert('提示', result);
                        },
                        failure: function (errorMsg) {
                            Ext.Msg.alert('错误', errorMsg);
                        }
                    });
                } else {
                }
            }, this);
        }
    }

    //点击删除按钮
    function zaodeleteplan() {
        delePlan(1);
    }
    function zhongdeleteplan() {
        delePlan(2);
    }
    function yedeleteplan() {
        delePlan(3);
    }
    //删除公共方法
    var delePlan = function deleteplan(flag) {
       
        if (flag == 1) {
            grid = App.ZaoPlanGridPanel;
        }
        else if (flag == 2) {
            grid = App.ZhongGridPanel;
        }
        else if (flag == 3) {
            grid = App.YeGridPanel;
        }
        store = grid.store;
        sm = grid.getSelectionModel().getSelection()[0];
        if (sm) {
            Ext.Msg.confirm("提示", '您确定要删除此条信息？', function (btn) {
                if (btn == 'yes') {
                    App.direct.DelePlan(sm.data.PlanID.toString(), {
                        success: function (result) {
                            if (result == "") {
                                store.data.removeAt(store.indexOf(sm));
                                grid.store.loadRecords(store.data.items, false);
                                Ext.Msg.alert('提示', "删除计划成功!");
                            }
                            else
                                Ext.Msg.alert('提示', result);
                        },
                        failure: function (errorMsg) {
                            Ext.Msg.alert('Failure', errorMsg);
                        }
                    });
                } else {
                }
            }, this);
        }
    }



//编辑之前相应
    var beforeEdit = function (ed, e) {
        var field = this.getEditor(e.record, e.column).field;
        switch (e.field) {
            case "RecipeName":
                field.allQuery = e.record.get('RecipeMaterialName');
                break;
        }
    };



//树形结构点击刷新右侧方法
//点击tree产生配方相信信息 绑定到Gridpanel
var menuItemClick = function (view, rcd, item, idx, event, eOpts) {
    var s = rcd.get('qtip');
    if (s) {
        App.direct.LoadGridData(s, {
            success: function (result) {
            },
            failure: function (errorMsg) {
                Ext.Msg.alert('Failure', errorMsg);
            }
        });
    }
    else {
        Ext.Msg.alert('提示', '请选择机台！');
    }
};

 

// 改变行式样   
var getRowClass = function (r) {
    var d = r.data;
    if (d.PlanState == '2') {
        return "indoor-r";
    }
    else if (d.PlanState == '3') {
        return "indoor-b";
    }
    else if (d.PlanState == '4') {
        return "indoor-g";
    }
    else if (d.PlanState== '5') {
        return "indoor-y";
    }
};

//药品自动排产

var zaoplanExec = function (flag) {
    planExec(1);
}
var zhongplanExec = function (flag) {
    planExec(2);
}
var yeplanExec = function (flag) {
    planExec(3);
}
var planExec = function (flag) {
    if (flag == 1) {
        grid = App.ZaoPlanGridPanel;
    }
    else if (flag == 2) {
        grid = App.ZhongGridPanel;
    }
    else if (flag == 3) {
        grid = App.YeGridPanel;
    }
    store = grid.store;
    sm = grid.getSelectionModel().getSelection()[0];
    if (sm) {
        App.direct.UpPlanState(sm.data, {
            success: function (result) {
                if (result) {
                    var win = App.winModify;
                    win.show();
                }
                else {
                    //                    Ext.Msg.alert('错误！', '不能删除已接受后的计划！');
                }
            },
            failure: function (errorMsg) {
                Ext.Msg.alert('Failure', errorMsg);
            }
        });
    }
}

var zaoallPlanExec = function (flag) {
    allPlanExec(1);

}
var zhongallPlanExec = function (flag) {
    allPlanExec(2);
}
var yeallPlanExec = function (flag) {
    allPlanExec(3);
}
var allPlanExec = function (flag) {
    if (flag == 1) {
        grid = App.ZaoPlanGridPanel;
    }
    else if (flag == 2) {
        grid = App.ZhongGridPanel;
    }
    else if (flag == 3) {
        grid = App.YeGridPanel;
    }
    var typejson = "[]";

    var store = grid.store;
    arr = new Array();
    Ext.each(store.data.items, function (record) {
        arr.push(record.data);
    });
    var datajson = Ext.encode(arr);

    App.direct.AllUpdatePlanState(typejson, arr, {
        success: function (result) {
            if (result) {
                var win = App.AllWin;
                win.show();
            }
            else {
                //                    Ext.Msg.alert('错误！', '不能删除已接受后的计划！');
            }
        },
        failure: function (errorMsg) {
            Ext.Msg.alert('Failure', errorMsg);
        }
    });
}
