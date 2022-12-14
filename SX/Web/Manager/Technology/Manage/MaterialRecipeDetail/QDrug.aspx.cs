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

public partial class Manager_Technology_Manage_MaterialRecipeDetail_QDrug : Mesnac.Web.UI.Page
{
    private IPmtTermManager pmtTermManager = new PmtTermManager();
  
    private IPmtActionManager pmtActionManager = new PmtActionManager();
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    private IPmtRecipeWeightMidManager PmtRecipeWeightMidManager = new PmtRecipeWeightMidManager();
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    private IPmtCoolMILLMainManager PmtCoolMILLMainManager = new PmtCoolMILLMainManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();

        where = new WhereClip();
        order = new OrderByClip();
        order = SysCode._.Remark.Asc;
        where.And(SysCode._.TypeID == "YesNo");
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhereAndOrder(where, order);
        IniSysCodeComboBox(CIsUseCode, lst);
        IniSysCodeComboBox(CIsUseCutter, lst);
        X.Call("SetEditable");
        string recipe = GetRequest("Recipe");
        if (string.IsNullOrWhiteSpace(recipe))
        {
            return;
        }
        PmtCoolMILLMain c = null; 
        try
        {
            c = PmtCoolMILLMainManager.GetListByWhere(PmtCoolMILLMain._.RecipeObjID == recipe)[0];
           
        }
        catch
        {
            return;
        }
        if (c == null)
        {
            return;
        }
        else
        {
            TotalWeigh.Text = c.TotalWeigh.ToString();
            ErroeAllow.Text = c.ErroeAllow.ToString();
            ErroeAllowMove.Text = c.ErroeAllowMove.ToString();
            RubErroeAllow.Text = c.RubErroeAllow.ToString();
            CTabletSpeed.Text = c.CTabletSpeed.ToString();
            CTabletThick.Text = c.CTabletThick.ToString();
            CTabletTemp.Text = c.CTabletTemp.ToString();
            CTabletWeigh.Text = c.CTabletWeigh.ToString();
            CIsUseCode.Text = c.CIsUseCode.ToString();
            CIsUseCutter.Text = c.CIsUseCutter.ToString();
            CCutterNum.Text = c.CCutterNum.ToString();
            BatchWeigh.Text = c.BatchWeigh.ToString();
            DrugNum.Text = c.DrugNum.ToString();
            DrugTime.Text = c.DrugTime.ToString();
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
    protected void ComboBoxSearchStore_ReadData(object sender, StoreReadDataEventArgs e)
    {
       
        Ext.Net.Store stor = (Store)sender;
        string submitDirectEventConfig = this.Page.Request["submitDirectEventConfig"]; //{"config":{"extraParams":{"query":"r","page":1,"start":0,"limit":25}}}
        string query = string.Empty;
        if (!string.IsNullOrWhiteSpace(submitDirectEventConfig))
        {
            try
            {
                JavaScriptObject config = (JavaScriptObject)JavaScriptConvert.DeserializeObject(submitDirectEventConfig);
                query = (((Newtonsoft.Json.JavaScriptObject)(((Newtonsoft.Json.JavaScriptObject)(config["config"]))["extraParams"]))["query"]).ToString();
            }
            catch
            {
            }
        }
        string storid = stor.ID.ToLower().Replace("ComboBoxSearchStore".ToLower(), "");
        int itype = -1;
        int.TryParse(storid, out itype);
        if (query.Length == 0 || (itype < 0))
        {
            stor.DataSource = new EntityArrayList<BasMaterial>();
            stor.DataBind();
            return;
        }
        //油2
        //if (itype == 5)
        //{
        //    itype = 1;
        //}
        itype = 9;
        stor.DataSource = GetTableByPYPmtConfig(itype.ToString(), query);
        stor.DataBind();
    }
    protected void ComboBoxSearchStore_ReadData2(object sender, StoreReadDataEventArgs e)
    {

        Ext.Net.Store stor = (Store)sender;
        string submitDirectEventConfig = this.Page.Request["submitDirectEventConfig"]; //{"config":{"extraParams":{"query":"r","page":1,"start":0,"limit":25}}}
        string query = string.Empty;
        if (!string.IsNullOrWhiteSpace(submitDirectEventConfig))
        {
            try
            {
                JavaScriptObject config = (JavaScriptObject)JavaScriptConvert.DeserializeObject(submitDirectEventConfig);
                query = (((Newtonsoft.Json.JavaScriptObject)(((Newtonsoft.Json.JavaScriptObject)(config["config"]))["extraParams"]))["query"]).ToString();
            }
            catch
            {
            }
        }
        string storid = stor.ID.ToLower().Replace("ComboBoxSearchStore".ToLower(), "");
        int itype = -1;
        int.TryParse(storid, out itype);
        if (query.Length == 0 || (itype < 0))
        {
            stor.DataSource = new EntityArrayList<BasMaterial>();
            stor.DataBind();
            return;
        }
        //油2
        //if (itype == 5)
        //{
        //    itype = 1;
        //}
        itype =7;
        stor.DataSource = GetTableByPYPmtConfig(itype.ToString(), query);
        stor.DataBind();
    }
    private EntityArrayList<BasMaterial> GetTableByPYPmtConfig(string minor, string query)
    {
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == PmtConfigManager.TypeCode.WeightMaterial.ToString());
        where.And(PmtConfig._.ItemCode == minor.Trim().ToString());
        string sqlstr = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        query = query.Trim();
        return pmtConfigManager.GetBySql(String.Format(sqlstr, query)).ToArrayList<BasMaterial>();
    }
    [DirectMethod]
    public object MinxingGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        
        int total = 0;
        EntityArrayList<PmtRecipeWeightMid> lst = new EntityArrayList<PmtRecipeWeightMid>();
        string recipe = GetRequest("Recipe");
        if (!string.IsNullOrWhiteSpace(txtRecipeObjID.Text))
        {
            recipe = txtRecipeObjID.Text;
        }
        if (!string.IsNullOrWhiteSpace(recipe))
        {
            lst = PmtRecipeWeightMidManager.GetListByWhereAndOrder(PmtRecipeWeightMid._.RecipeObjID == recipe && PmtRecipeWeightMid._.WeightID <= 9, PmtRecipeWeightMid._.WeightID.Asc);
        }
        int modelcount = lst.Count;
        int pagesize = 9;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtRecipeWeightMid m = new PmtRecipeWeightMid();
            m.WeightID = i;
            lst.Add(m);
        }
        total = lst.Count;
        return new { data = lst, total = lst.Count };
    }
    [DirectMethod]
    public object RERUBPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        int total = 0;
        EntityArrayList<PmtRecipeWeightMid> lst = new EntityArrayList<PmtRecipeWeightMid>();
        string recipe = GetRequest("Recipe");
        if (!string.IsNullOrWhiteSpace(txtRecipeObjID.Text))
        {
            recipe = txtRecipeObjID.Text;
        }
        if (!string.IsNullOrWhiteSpace(recipe))
        {
            lst = PmtRecipeWeightMidManager.GetListByWhereAndOrder(PmtRecipeWeightMid._.RecipeObjID == recipe && PmtRecipeWeightMid._.WeightID >9, PmtRecipeWeightMid._.WeightID.Asc);
        }
        int modelcount = lst.Count;
        int pagesize = 3;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtRecipeWeightMid m = new PmtRecipeWeightMid();
            m.WeightID =8+ i;
            lst.Add(m);
        }
        total = lst.Count;
        return new { data = lst, total = lst.Count };
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