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
using System.Globalization;


/// <summary>
/// Manager_Technology_Analysis_Technology 实现类
/// 孙本强 @ 2013-04-03 13:17:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Analysis_Technology : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptPlanManager pptPlanManager = new PptPlanManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptShiftManager pptShiftManager = new PptShiftManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    #endregion


    #region 页面初始化
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// 初始化ComboBox
    /// 孙本强 @ 2013-04-03 13:38:59
    /// </summary>
    /// <param name="cb">The cb.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cb.Items.Clear();
        cb.Items.Add(allitem);
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
    /// <summary>
    /// 页面初始化
    /// 孙本强 @ 2013-04-03 13:39:51
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
        txtBeginDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptShift._.UseFlag == "1");
        order = PptShift._.ObjID.Asc;
        txtPptShift.Items.Clear();
        txtPptShift.Items.Add(allitem);
        foreach (PptShift m in pptShiftManager.GetListByWhereAndOrder(where, order))
        {
            txtPptShift.Items.Add(new ListItem(m.ShiftName, m.ObjID));
        }
        txtPptShift.Text = (txtPptShift.Items[0].Value);
    }

    protected void storeMaterial_ReadData(object sender, StoreReadDataEventArgs e)
    {
        try
        {
            PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
            queryParams.equipCode = hiddenEquipCode.Text;
            queryParams.planStartDate = txtBeginDate.Text;
            queryParams.shiftID = txtPptShift.Text.Replace(constSelectAllText, "");
            EntityArrayList<BasMaterial> data = pptPlanManager.GetPlanPptMaterial(queryParams);
            txtPptMaterial.GetStore().DataSource = data;
            txtPptMaterial.GetStore().DataBind();
        }
        catch
        {
        }
    }
    /// <summary>
    /// 生成红色Html标示
    /// 孙本强 @ 2013-04-03 13:40:06
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// 默认Html标示
    /// 孙本强 @ 2013-04-03 13:40:16
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    private void SetFieldValue(DataRow row, string field)
    {
        if (row[field] == DBNull.Value || row[field] == null)
        {
            row[field] = 0;
        }
        else
        {
            decimal value = 0;
            decimal.TryParse(row[field].ToString(), out value);
            row[field] = value;
        }
    }
    /// <summary>
    /// Inis the chart info.
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="PptList">The PPT list.</param>
    /// <remarks></remarks>
    private DataTable IniPptLotDataInfo(PageResult<PptLotData> lst)
    {
        DataTable Result = new DataTable();
        try
        {
            Result = lst.DataSet.Tables[0];
            for (int i = 0; i < Result.Rows.Count; i++)
            {
                DataRow row = Result.Rows[i];
                row["LotIndex"] = i + 1;
                SetFieldValue(row, "MixingEner");
                SetFieldValue(row, "InSetWeight");
                SetFieldValue(row, "InRealWeight");
                SetFieldValue(row, "DoneRtime");
                SetFieldValue(row, "StandTime");
                SetFieldValue(row, "OutSetWeight");
                SetFieldValue(row, "OutRealWeight");
            }
        }
        catch
        {
        }
        return Result;
    }

    /// <summary>
    /// Inis the fomr info.
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="PptList">The PPT list.</param>
    /// <remarks></remarks>
    private void IniFomrInfo(PageResult<PptLotData> lst)
    {
        DataTable data = IniPptLotDataInfo(lst);
        ChartEner.GetStore().DataSource = data;
        ChartEner.GetStore().DataBind();
        ChartWeight.GetStore().DataSource = data;
        ChartWeight.GetStore().DataBind();
        ChartDoneTime.GetStore().DataSource = data;
        ChartDoneTime.GetStore().DataBind();
        ChartShiftWeigh.GetStore().DataSource = data;
        ChartShiftWeigh.GetStore().DataBind();
    }
    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptLotData> GetPageResultData(PageResult<PptLotData> pageParams)
    {
        DateTime beginTime = DateTime.Now;
        DateTime endTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(txtBeginDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        } 
        try
        {
            endTime = Convert.ToDateTime(txtEndDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        if (string.IsNullOrEmpty(txtPptMaterial.Value.ToString()))
        {
            X.Msg.Show(new MessageBoxConfig { Title = "提示", Message = "请选择物料信息！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            return pageParams;
        }
        PptLotDataManager.QueryParams queryParams = new PptLotDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        //return pptLotDataManager.GetAnalysisTechnology(queryParams);
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.MaterCode = txtPptMaterial.Value.ToString();
        queryParams.BeginTime = beginTime.ToString("yyyy-MM-dd");
        queryParams.EndTime = endTime.ToString("yyyy-MM-dd");
        queryParams.ShiftID = txtPptShift.Text.Replace(constSelectAllText, "");
        return pptLotDataManager.GetAnalysisTechnology(queryParams);
    }
    /// <summary>
    /// BTNs the search first click.
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchClick(object sender, DirectEventArgs e)
    {
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageSize = -1;
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        IniFomrInfo(lst);
    }
}