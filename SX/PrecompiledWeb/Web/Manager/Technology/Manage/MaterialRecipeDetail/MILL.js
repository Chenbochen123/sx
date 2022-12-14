






var enableEdit = function () {
    var can = true;
    try {
        can = !parent.App.btnSave.disabled;
    }
    catch (ex) {
    }
    return can;
}
var SetEditable = function () {
   
    var can = true;
    try {
        can = !parent.App.btnSave.disabled;
    }
    catch (ex) {
        
    }
   
    App.Step1SetTime.setReadOnly(!can);
    App.Step2SetTime.setReadOnly(!can);
    App.Step3SetTime.setReadOnly(!can);
    App.Step4SetTime.setReadOnly(!can);
    App.Step5SetTime.setReadOnly(!can);
    App.Step6SetTime.setReadOnly(!can);
    App.Step7SetTime.setReadOnly(!can);
    App.Step8SetTime.setReadOnly(!can);
    App.Step1SetRollerSpace.setReadOnly(!can);
    App.Step2SetRollerSpace.setReadOnly(!can);
    App.Step3SetRollerSpace.setReadOnly(!can);
    App.Step4SetRollerSpace.setReadOnly(!can);
    App.Step5SetRollerSpace.setReadOnly(!can);
    App.Step6SetRollerSpace.setReadOnly(!can);
    App.Step7SetRollerSpace.setReadOnly(!can);
    App.Step8SetRollerSpace.setReadOnly(!can);
    App.PInitalTime.setReadOnly(!can);
    App.PEndTime.setReadOnly(!can);
    App.PMixTime.setReadOnly(!can);
    App.PStartV.setReadOnly(!can);
    App.PEndV.setReadOnly(!can);
    App.PMixTemp.setReadOnly(!can);
    App.PRatioCoef.setReadOnly(!can);
    App.PIsInject.setReadOnly(!can);
    App.PStartSpeed.setReadOnly(!can);

    App.SStep1SetTime.setReadOnly(!can);
    App.SStep2SetTime.setReadOnly(!can);
    App.SStep3SetTime.setReadOnly(!can);
    App.SStep4SetTime.setReadOnly(!can);
    App.SStep5SetTime.setReadOnly(!can);
    App.SStep6SetTime.setReadOnly(!can);
    App.SStep7SetTime.setReadOnly(!can);
    App.SStep8SetTime.setReadOnly(!can);
    App.SStep1SetRollerSpace.setReadOnly(!can);
    App.SStep2SetRollerSpace.setReadOnly(!can);
    App.SStep3SetRollerSpace.setReadOnly(!can);
    App.SStep4SetRollerSpace.setReadOnly(!can);
    App.SStep5SetRollerSpace.setReadOnly(!can);
    App.SStep6SetRollerSpace.setReadOnly(!can);
    App.SStep7SetRollerSpace.setReadOnly(!can);
    App.SStep8SetRollerSpace.setReadOnly(!can);
    App.SStep1SetVelocity.setReadOnly(!can);
    App.SStep2SetVelocity.setReadOnly(!can);
    App.SStep3SetVelocity.setReadOnly(!can);
    App.SStep4SetVelocity.setReadOnly(!can);
    App.SStep5SetVelocity.setReadOnly(!can);
    App.SStep6SetVelocity.setReadOnly(!can);
    App.SStep7SetVelocity.setReadOnly(!can);
    App.SStep8SetVelocity.setReadOnly(!can);

    App.SIsInject.setReadOnly(!can);
    App.SBeforeFOpen.setReadOnly(!can);
    App.SIsPutInto.setReadOnly(!can);
    App.SMixTime.setReadOnly(!can);
    App.SInStartTime.setReadOnly(!can);
    App.SAfterFOpen.setReadOnly(!can);
    App.SInTimeLen.setReadOnly(!can);
    App.SMixTemp.setReadOnly(!can);
    App.SFUse.setReadOnly(!can);
    App.SFUseTime.setReadOnly(!can);
    App.SJDUSE.setReadOnly(!can);
    App.PJDUSE.setReadOnly(!can);
}
