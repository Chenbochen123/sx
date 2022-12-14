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

/// <summary>
/// Manager_Technology_Manage_MaterialRecipeDetail_Mixing 实现类
/// 孙本强 @ 2013-04-03 13:06:34
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Manage_MaterialRecipeDetail_Mixing : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:35
    /// </summary>
    private IPmtTermManager pmtTermManager = new PmtTermManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:35
    /// </summary>
    private IPmtActionManager pmtActionManager = new PmtActionManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:35
    /// </summary>
    private IPmtRecipeMixingManager pmtRecipeMixingManager = new PmtRecipeMixingManager();
    #endregion

    /// <summary>
    /// Fillsets the tem code.
    /// 孙本强 @ 2013-04-03 13:06:35
    /// </summary>
    /// <remarks></remarks>
    private void FillsetTemCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtTerm._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtTerm._.SeqIdx.Asc;
        EntityArrayList<PmtTerm> lst = pmtTermManager.GetListByWhereAndOrder(where, order);
        setTemCode.Items.Clear();
        if (true)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = "　";
            item.Value = "　";
            setTemCode.Items.Add(item);
        }
        foreach (PmtTerm m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = m.ShowName;
            item.Value = m.TermCode;
            setTemCode.Items.Add(item);
        }
    }
    /// <summary>
    /// Fillsets the action code.
    /// 孙本强 @ 2013-04-03 13:06:35
    /// </summary>
    /// <remarks></remarks>
    private void FillsetActionCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtAction._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtAction._.SeqIdx.Asc;
        EntityArrayList<PmtAction> lst = pmtActionManager.GetListByWhereAndOrder(where, order);
        setActionCode.Items.Clear();
        foreach (PmtAction m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = m.ShowName;
            item.Value = m.ActionCode;
            setActionCode.Items.Add(item);
        }
    }
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:06:35
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
        FillsetTemCode();
        FillsetActionCode();
    }

    #region 混炼信息

    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:06:36
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
    /// Minxings the grid panel bind data.
    /// 孙本强 @ 2013-04-03 13:06:36
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object MinxingGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        int total = 0;
        EntityArrayList<PmtRecipeMixing> lst = new EntityArrayList<PmtRecipeMixing>();
        string recipe = GetRequest("Recipe");
        if (!string.IsNullOrWhiteSpace(txtRecipeObjID.Text))
        {
            recipe = txtRecipeObjID.Text;
        }
        if (!string.IsNullOrWhiteSpace(recipe))
        {
            lst = pmtRecipeMixingManager.GetListByWhereAndOrder(PmtRecipeMixing._.RecipeObjID == recipe, PmtRecipeMixing._.MixingStep.Asc);
        }
       
        //lst[0].MixingTime = lst[0].Time_diff;
        int modelcount = lst.Count;
        int pagesize = 30;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtRecipeMixing m = new PmtRecipeMixing();
            m.MixingStep = i;
            lst.Add(m);
        }
        total = lst.Count;
        return new { data = lst, total = lst.Count };
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        EntityArrayList<PmtRecipeMixing> lst = new EntityArrayList<PmtRecipeMixing>();
        string recipe = GetRequest("Recipe");
        string sql = "";
        sql = @"select a.MixingStep 步骤,c.Term_name 条件设定
                      ,iif(a.MixingTime <> 0,a.MixingTime,null) 时间
                      ,iif(a.Time_diff <> 0,a.Time_diff,null) 时间公差
                      ,iif(a.MixingTemp <> 0,a.MixingTemp,null) 温度
                      ,iif(a.Temp_diff <> 0,a.Temp_diff,null) 温度公差
                      ,iif(a.MixingEnergy <> 0,a.MixingEnergy,null) 能量
                      ,iif(a.Ener_diff <> 0,a.Ener_diff,null) 能量公差
                      ,iif(a.MixingPower <> 0,a.MixingPower,null) 功率
                      ,b.Act_name 动作
                      ,iif(a.MixingSpeed <> 0,a.MixingSpeed,null) 转速
                      ,iif(a.MixingPress <> 0,a.MixingPress,null) 压力
                from PmtRecipeMixing a
                left join Pmt_act b on a.ActionCode = b.Act_code
                left join Pmt_term c on a.TermCode = c.Term_addr";
        sql = sql + " where RecipeObjID = '" + recipe + "'";
        sql = sql + " order by a.MixingStep ";
        DataTable dt = pmtRecipeMixingManager.GetBySql(sql).ToDataSet().Tables[0];
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "混炼信息");
    }

    #endregion
}