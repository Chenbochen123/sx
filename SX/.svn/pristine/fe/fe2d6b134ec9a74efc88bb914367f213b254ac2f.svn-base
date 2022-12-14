using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Manager_Technology_Comparison_Weight 实现类
/// 孙本强 @ 2013-04-03 13:06:02
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Comparison_Weight : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:02
    /// </summary>
    private IPmtRecipeWeightLogManager pmtRecipeWeightLogManager = new PmtRecipeWeightLogManager();
    #endregion

    #region 页面初始化

    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:06:02
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
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:06:02
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
        try
        {
            IniForm();
        }
        catch
        {
        }
    }
    #endregion
    /// <summary>
    /// Objects the equals.
    /// 孙本强 @ 2013-04-03 13:06:03
    /// </summary>
    /// <param name="a">A.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private bool ObjectEquals(object a, object b)
    {
        if (a == DBNull.Value)
        {
            a = null;
        }
        if (a == DBNull.Value)
        {
            a = b;
        }
        if (a == null && b == null)
        {
            return true;
        }
        if (a == null)
        {
            if (string.IsNullOrWhiteSpace(b.ToString()))
            {
                return true;
            }
            try
            {
                if (Convert.ToDecimal(a.ToString().Trim()) == 0)
                {

                    return true;
                }
            }
            catch
            {
            }
        }
        if (b == null)
        {
            if (string.IsNullOrWhiteSpace(a.ToString()))
            {

                return true;
            }
            try
            {
                if (Convert.ToDecimal(a.ToString().Trim()) == 0)
                {

                    return true;
                }
            }
            catch
            {
            }
        }
        try
        {
            if (Convert.ToDecimal(a.ToString().Trim()) == Convert.ToDecimal(b.ToString().Trim()))
            {

                return true;
            }
        }
        catch
        {
        }
        if (a.ToString().Trim() != b.ToString().Trim())
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Inis the form panel.
    /// 孙本强 @ 2013-04-03 13:06:03
    /// </summary>
    /// <param name="Main">The main.</param>
    /// <param name="aPanel">A panel.</param>
    /// <param name="bPanel">The b panel.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private int IniFormPanel(Ext.Net.Panel Main, Ext.Net.GridPanel aPanel, Ext.Net.GridPanel bPanel)
    {
        int Result = 0;
        string weightType = Main.ID.ToString().Replace("PanelWeight", "");
        DataSet ds1 = pmtRecipeWeightLogManager.GetPmtRecipeWeightLog(GetRequest("a"), weightType);
        DataSet ds2 = pmtRecipeWeightLogManager.GetPmtRecipeWeightLog(GetRequest("b"), weightType);
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        if (ds1.Tables.Count > 0)
        {
            dt1 = ds1.Tables[0];
        }
        if (ds2.Tables.Count > 0)
        {
            dt2 = ds2.Tables[0];
        }
        if ((dt1.Rows.Count == 0) && (dt2.Rows.Count == 0))
        {
            Main.Visible = false;
        }
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            DataRow row1 = dt1.Rows[i];
            if (dt2.Rows.Count <= i)
            {
                row1["isDifference"] = "1";
                Result++;
                continue;
            }
            DataRow row2 = dt2.Rows[i];
            foreach (DataColumn dc in dt1.Columns)
            {
                if (dc.ToString().ToLower().Contains("obj"))
                {
                    continue;
                }
                if (dc.ToString().ToLower().Contains("log"))
                {
                    continue;
                }
                if (dc.ToString().ToLower().Contains("ver"))
                {
                    continue;
                }
                if (!ObjectEquals(row1[dc.ToString()], row2[dc.ToString()]))
                {
                    row1["isDifference"] = "1";
                    row2["isDifference"] = "1";
                    Result++;
                    break;
                }
            }
        }
        for (int i = dt1.Rows.Count; i < dt2.Rows.Count; i++)
        {
            DataRow row2 = dt2.Rows[i];
            row2["isDifference"] = "1";
            Result++;
        }

        Store store1 = aPanel.GetStore();
        store1.DataSource = dt1;
        store1.DataBind();
        Store store2 = bPanel.GetStore();
        store2.DataSource = dt2;
        store2.DataBind();
        return Result;
    }
    /// <summary>
    /// Inis the form.
    /// 孙本强 @ 2013-04-03 13:06:03
    /// </summary>
    /// <remarks></remarks>
    private void IniForm()
    {
        int Result = IniFormPanel(PanelWeight0, PanelWeight01, PanelWeight02);
        if (Result > 0)
        {
            PanelWeight0.Title = "炭黑称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight0.Title = "炭黑称量信息相同";
        }
        Result = IniFormPanel(PanelWeight1, PanelWeight11, PanelWeight12);
        if (Result > 0)
        {
            PanelWeight1.Title = "油称(1)称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight1.Title = "油称(1)称量信息相同";
        }
        Result = IniFormPanel(PanelWeight2, PanelWeight21, PanelWeight22);
        if (Result > 0)
        {
            PanelWeight2.Title = "胶料称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight2.Title = "胶料称量信息相同";
        }
        Result = IniFormPanel(PanelWeight3, PanelWeight31, PanelWeight32);
        if (Result > 0)
        {
            PanelWeight3.Title = "小料校核称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight3.Title = "小料校核称量信息相同";
        }
        Result = IniFormPanel(PanelWeight5, PanelWeight51, PanelWeight52);
        if (Result > 0)
        {
            PanelWeight5.Title = "油称(2)称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight5.Title = "油称(2)称量信息相同";
        }
        Result = IniFormPanel(PanelWeight9, PanelWeight91, PanelWeight92);
        if (Result > 0)
        {
            PanelWeight9.Title = "小料称量信息-不同条数=" + Result.ToString();
        }
        else
        {
            PanelWeight9.Title = "小料称量信息相同";
        }
    }
}