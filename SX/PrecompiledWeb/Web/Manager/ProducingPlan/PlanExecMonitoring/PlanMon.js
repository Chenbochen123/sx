function zaoPlan() {
    Plan(1,0);
}
function zhongPlan() {
    Plan(2,0);
}
function yePlan() {
    Plan(3,0);
}

function zaoPlanInfo() {
    Plan(1, 1);
}
function zhongPlanInfo() {
    Plan(2, 1);
}
function yePlanInfo() {
    Plan(3, 1);
}

function Plan(flag,up) {

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
        App.direct.RealPlanInfo(sm.data.PlanID,up,{
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
 function Mod(flag,i){
 }


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
 function NumChange() {
     App.direct.PlanNumChage({
         success: function (result) {

         },
         failure: function (errorMsg) {
             Ext.Msg.alert('Failure', errorMsg);
         }
     });
 }