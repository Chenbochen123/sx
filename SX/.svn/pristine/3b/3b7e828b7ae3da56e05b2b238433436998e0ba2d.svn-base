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
/// Manager_Technology_Comparison_Mixing 实现类
/// 孙本强 @ 2013-04-03 13:05:47
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Comparison_Mixing : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:48
    /// </summary>
    private IPmtRecipeMixingLogManager pmtRecipeMixingLogManager = new PmtRecipeMixingLogManager();
    #endregion

    #region 页面初始化

    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:05:48
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
    /// 孙本强 @ 2013-04-03 13:05:48
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
    /// 孙本强 @ 2013-04-03 13:05:48
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
    /// Inis the form.
    /// 孙本强 @ 2013-04-03 13:05:49
    /// </summary>
    /// <remarks></remarks>
    private void IniForm()
    {
        DataTable dt1 = pmtRecipeMixingLogManager.GetPmtRecipeMixingLog(GetRequest("a")).Tables[0];
        DataTable dt2 = pmtRecipeMixingLogManager.GetPmtRecipeMixingLog(GetRequest("b")).Tables[0];

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            DataRow row1 = dt1.Rows[i];
            if (dt2.Rows.Count <= i)
            {
                row1["isDifference"] = "1";
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
                  
                    break;
                }
            }
        }
        for (int i = dt1.Rows.Count; i < dt2.Rows.Count; i++)
        {
            DataRow row2 = dt2.Rows[i];
            row2["isDifference"] = "1";
        }

        Store store1 = this.gridPanel1.GetStore();
        store1.DataSource = dt1;
        store1.DataBind();
        Store store2 = this.gridPanel2.GetStore();
        store2.DataSource = dt2;
        store2.DataBind();
    }
}