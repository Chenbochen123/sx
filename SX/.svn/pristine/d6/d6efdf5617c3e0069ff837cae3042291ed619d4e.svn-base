using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;

public partial class Manager_Technology_Manage_MaterialRecipeDetail_MILL : System.Web.UI.Page
{
    private IPmtTermManager pmtTermManager = new PmtTermManager();

    private IPmtActionManager pmtActionManager = new PmtActionManager();
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    private IPmtPMILLMainManager PmtPMILLMainManager = new PmtPMILLMainManager();
    private IPmtSMILLMainManager PmtSMILLMainManager = new PmtSMILLMainManager();
    private IPmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        //this.cbPageCanEdit.Checked = GetRequest("canEdit") == "1";
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
       
        where = new WhereClip();
        order = new OrderByClip();
        order = SysCode._.Remark.Asc;
        where.And(SysCode._.TypeID == "YesNo");
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhereAndOrder(where, order);
        IniSysCodeComboBox(PIsInject, lst);
        IniSysCodeComboBox(SIsInject, lst);
        IniSysCodeComboBox(SFUse, lst);
        IniSysCodeComboBox(SIsPutInto, lst);
        IniSysCodeComboBox(PJDUSE, lst);
        IniSysCodeComboBox(SJDUSE, lst);
        IniPage();
        X.Call("SetEditable");
    }
    private void IniPage()
    {
        string recipe = GetRequest("Recipe");
        if (string.IsNullOrWhiteSpace(recipe))
        {
            return;
        }
        PmtPMILLMain m = null; PmtSMILLMain s = null;
        try
        {
            m = PmtPMILLMainManager.GetListByWhere(PmtPMILLMain._.RecipeObjID == recipe)[0];
            s = PmtSMILLMainManager.GetListByWhere(PmtSMILLMain._.RecipeObjID == recipe)[0];
        }
        catch
        {
            return;
        }
        if (m == null)
        {
          
        }
        else {
            Step1SetTime.Text = m.Step1SetTime.ToString();
            Step2SetTime.Text = m.Step2SetTime.ToString();
            Step3SetTime.Text = m.Step3SetTime.ToString();
            Step4SetTime.Text = m.Step4SetTime.ToString();
            Step5SetTime.Text = m.Step5SetTime.ToString();
            Step6SetTime.Text = m.Step6SetTime.ToString();
            Step7SetTime.Text = m.Step7SetTime.ToString();
            Step8SetTime.Text = m.Step8SetTime.ToString();

            Step1SetRollerSpace.Text = m.Step1SetRollerSpace.ToString();
            Step2SetRollerSpace.Text = m.Step2SetRollerSpace.ToString();
            Step3SetRollerSpace.Text = m.Step3SetRollerSpace.ToString();
            Step4SetRollerSpace.Text = m.Step4SetRollerSpace.ToString();
            Step5SetRollerSpace.Text = m.Step5SetRollerSpace.ToString();
            Step6SetRollerSpace.Text = m.Step6SetRollerSpace.ToString();
            Step7SetRollerSpace.Text = m.Step7SetRollerSpace.ToString();
            Step8SetRollerSpace.Text = m.Step8SetRollerSpace.ToString();

            PInitalTime.Text = m.PInitalTime.ToString();
            PEndTime.Text = m.PEndTime.ToString();
            PMixTime.Text = m.PMixTime.ToString();
            PStartV.Text = m.PStartV.ToString();
            PEndV.Text = m.PEndV.ToString();
            PMixTemp.Text = m.PMixTemp.ToString();
            PRatioCoef.Text = m.PRatioCoef.ToString();
            PIsInject.Text = m.PIsInject.ToString();
            PStartSpeed.Text = m.PStartSpeed.ToString();
            PJDUSE.Text = m.PJDUSE.ToString();
        }
        if (s == null)
        {

        }
        else
        {
            SStep1SetTime.Text = s.Step1SetTime.ToString();
            SStep2SetTime.Text = s.Step2SetTime.ToString();
            SStep3SetTime.Text = s.Step3SetTime.ToString();
            SStep4SetTime.Text = s.Step4SetTime.ToString();
            SStep5SetTime.Text = s.Step5SetTime.ToString();
            SStep6SetTime.Text = s.Step6SetTime.ToString();
            SStep7SetTime.Text = s.Step7SetTime.ToString();
            SStep8SetTime.Text = s.Step8SetTime.ToString();

            SStep1SetRollerSpace.Text = s.Step1SetRollerSpace.ToString();
            SStep2SetRollerSpace.Text = s.Step2SetRollerSpace.ToString();
            SStep3SetRollerSpace.Text = s.Step3SetRollerSpace.ToString();
            SStep4SetRollerSpace.Text = s.Step4SetRollerSpace.ToString();
            SStep5SetRollerSpace.Text = s.Step5SetRollerSpace.ToString();
            SStep6SetRollerSpace.Text = s.Step6SetRollerSpace.ToString();
            SStep7SetRollerSpace.Text = s.Step7SetRollerSpace.ToString();
            SStep8SetRollerSpace.Text = s.Step8SetRollerSpace.ToString();

            SStep1SetVelocity.Text = s.Step1SetVelocity.ToString();
            SStep2SetVelocity.Text = s.Step2SetVelocity.ToString();
            SStep3SetVelocity.Text = s.Step3SetVelocity.ToString();
            SStep4SetVelocity.Text = s.Step4SetVelocity.ToString();
            SStep5SetVelocity.Text = s.Step5SetVelocity.ToString();
            SStep6SetVelocity.Text = s.Step6SetVelocity.ToString();
            SStep7SetVelocity.Text = s.Step7SetVelocity.ToString();
            SStep8SetVelocity.Text = s.Step8SetVelocity.ToString();

            SIsInject.Text = s.SIsInject.ToString();
            SFUse.Text = s.SFUse.ToString();
            SFUseTime.Text = s.SFUseTime.ToString();
            SBeforeFOpen.Text = s.SBeforeFOpen.ToString();
            SIsPutInto.Text = s.SIsPutInto.ToString();
            SMixTime.Text = s.SMixTime.ToString();
            SInStartTime.Text = s.SInStartTime.ToString();
            SAfterFOpen.Text = s.SAfterFOpen.ToString();
            SInTimeLen.Text = s.SInTimeLen.ToString();
            SMixTemp.Text = s.SMixTemp.ToString();
            SJDUSE.Text = s.SJDUSE.ToString();
        }
    }
    private void IniSysCodeComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        cb.Items.Clear();
        string ss = string.Empty;
        foreach (SysCode m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(m.ItemName, m.ItemCode.ToString());
            cb.Items.Add(item);
        }
        if (cb.Items.Count > 0)
        {
            cb.Text = (cb.Items[0].Value);
        }
    }
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }
}