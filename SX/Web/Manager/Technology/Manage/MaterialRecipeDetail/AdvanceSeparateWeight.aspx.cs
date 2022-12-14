using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

public partial class Manager_Technology_Manage_MaterialRecipeDetail_AdvanceSeparateWeight : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 袁洋 @ 2013-04-03 13:06:43
    /// </summary>
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    /// <summary>
    /// 
    /// 袁洋 @ 2013-04-03 13:06:43
    /// </summary>
    private IBasMaterialManager basMaterialManager = new BasMaterialManager();
    /// <summary>
    /// 
    /// 袁洋 @ 2013-04-03 13:06:43
    /// </summary>
    private IPmtRecipeWeightManager pmtRecipeWeightManager = new PmtRecipeWeightManager();
    #endregion

    #region 内部类方法
    /// <summary>
    /// Gets the table by PY PMT config.
    /// 袁洋 @ 2013-04-03 13:06:43
    /// </summary>
    /// <param name="minor">The minor.</param>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    /// <remarks></remarks>
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
    /// <summary>
    /// Fills the combo box.
    /// 袁洋 @ 2013-04-03 13:06:43
    /// </summary>
    /// <param name="typeCode">The type code.</param>
    /// <param name="cb">The cb.</param>
    /// <remarks></remarks>
    private void FillComboBox(PmtConfigManager.TypeCode typeCode, Ext.Net.ComboBox cb)
    {
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == typeCode.ToString());
        string sqlstr = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        DataSet data = pmtConfigManager.GetBySql(sqlstr).ToDataSet();
        cb.Items.Clear();
        if (data != null && data.Tables.Count > 0)
        {
            foreach (DataRow dr in data.Tables[0].Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = dr["ShowInfo"].ToString();
                item.Value = dr["ValueInfo"].ToString();
                cb.Items.Add(item);
            }
        }
    }
    /// <summary>
    /// Sets the panel title.
    /// 袁洋 @ 2014-10-11 10:17:11
    /// </summary>
    /// <remarks></remarks>
    private void SetPanelTitle()
    {
        string recipe = GetRequest("Recipe");
        string material = GetRequest("Material");
        bool isMixing = true;
        if (!string.IsNullOrWhiteSpace(material))
        {
            EntityArrayList<BasMaterial> lst = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == material);
            if (lst.Count > 0)
            {
                BasMaterial m = lst[0];
                if (m.MajorTypeID.ToString().Trim() == "2")
                {
                    isMixing = false;
                }
            }
        }
        if (isMixing)
        {
            gridPanelWeightAdvanceSeparate.Visible = true;
            gridPanelWeightAdvanceSeparate.Title = "预分散称量信息-数量=0";
            try
            {
                gridPanelWeightAdvanceSeparate.Title = "预分散称量信息-数量=" + pmtRecipeWeightManager.GetRowCountByWhere(PmtRecipeWeight._.RecipeObjID == recipe && PmtRecipeWeight._.ActCode != "2"
                    && PmtRecipeWeight._.WeightType == "0");
            }
            catch
            {
            }
        }
        else
        {
            gridPanelWeightAdvanceSeparate.Visible = false;
        }
    }
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// 袁洋 @ 2014-10-11 10:17:11
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        this.cbPageCanEdit.Checked = GetRequest("canEdit") == "1";
        SetPanelTitle();
        FillComboBox(PmtConfigManager.TypeCode.WeightAction, setActCodeAdvanceSeparate);
        FillComboBox(PmtConfigManager.TypeCode.WeightAction, setActCodeRub);
    }
    /// <summary>
    /// Handles the ReadData event of the ComboBoxSearchStore control.
    /// 袁洋 @ 2014-10-11 10:17:11
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Ext.Net.StoreReadDataEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void ComboBoxSearchStore_ReadData(object sender, StoreReadDataEventArgs e)
    {
        Ext.Net.Store stor = (Store)sender;
        string submitDirectEventConfig = this.Page.Request["submitDirectEventConfig"];
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
        stor.DataSource = GetTableByPYPmtConfig(itype.ToString(), query);
        stor.DataBind();
    }
    #region 称量信息

    /// <summary>
    /// Gets the request.
    /// 袁洋 @ 2014-10-11 10:17:11
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }
    /// <summary>
    /// Weights the grid panel bind data.
    /// 袁洋 @ 2014-10-11 10:17:11
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object WeightGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        EntityArrayList<PmtRecipeWeight> lst = new EntityArrayList<PmtRecipeWeight>();
        string recipe = GetRequest("Recipe");
        if (!string.IsNullOrWhiteSpace(recipe))
        {
            string type = string.Empty;
            foreach (KeyValuePair<string, object> p in extraParams)
            {
                string pagetypename = "Page@";
                if (p.Key.ToLower().StartsWith(pagetypename.ToLower()))
                {
                    type = p.Key.Substring(pagetypename.Length).Trim();
                    break;
                }
            }
            WhereClip where = new WhereClip();
            where.And(PmtRecipeWeight._.RecipeObjID == recipe);
            where.And(PmtRecipeWeight._.WeightType == type);
            OrderByClip order = new OrderByClip();
            order = PmtRecipeWeight._.WeightID.Asc;
            lst = pmtRecipeWeightManager.GetListByWhereAndOrder(where, order);
        }
        if (lst.Count == 0)
        {
            PmtRecipeWeight m = new PmtRecipeWeight();
            m.ActCode = "0";
            m.RecipeMaterialCode = m.MaterialName;
            lst.Add(m);
            return new { data = lst, total = lst.Count };
        }
        for (int i = 0; i < lst.Count; i++)
        {
            PmtRecipeWeight p = lst[i];
            p.RecipeMaterialCode = p.MaterialName;
            try
            {
                p.RecipeEquipCode = "";
                p.RecipeEquipCode = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == p.MaterialCode)[0].MaterialLevel;
            }
            catch { }
        }
        
        return new { data = lst, total = lst.Count };
    }
    #endregion
}