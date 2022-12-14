﻿using System;
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
using System.Text;
using NBear.Data;
using Mesnac.Data.Implements;


/// <summary>
/// Manager_Technology_Analysis_Technology 实现类
/// 孙本强 @ 2013-04-03 13:17:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Analysis_Weigh : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            SearchOverWeigh = new SysPageAction() { ActionID = 1, ActionName = "btnSearchOverWeigh", ShowName = "查询-超差记录" };
            ExportOverWeigh = new SysPageAction() { ActionID = 2, ActionName = "btnExportOverWeigh", ShowName = "导出-超差记录" };
            SearchWeighRate = new SysPageAction() { ActionID = 3, ActionName = "btnSearchWeighRate", ShowName = "查询-称量合格率" };
            ExportWeighRate = new SysPageAction() { ActionID = 4, ActionName = "btnExportWeighRate", ShowName = "导出-称量合格率" };
        }
        public SysPageAction SearchOverWeigh { get; private set; } //必须为 public
        public SysPageAction ExportOverWeigh { get; private set; } //必须为 public
        public SysPageAction SearchWeighRate { get; private set; } //必须为 public
        public SysPageAction ExportWeighRate { get; private set; } //必须为 public
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
    private IPptWeighDataManager pptWeighDataManager = new PptWeighDataManager();
    private ISysCodeManager sysCodeManager = new SysCodeManager();

    PptWeighDataService procService = new PptWeighDataService();
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
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == "PptWeightType");
        IniComboBox(txtWeightType, lst);

        EntityArrayList<SysCode> lst2 = sysCodeManager.GetListByWhere(SysCode._.TypeID == "生产方式");
        IniComboBox(ComboBox1, lst2);
        txtBeginDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
            BasWorkShop._.DeleteFlag == "0" && BasWorkShop._.ObjID <= "5"
            , BasWorkShop._.ObjID.Asc);
        foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
        {
            ComboBoxWorkShopId.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
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

    #region 查询显示超差

    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptWeighData> OverWeighGridPanelBindData(PageResult<PptWeighData> pageParams)
    {
        DateTime beginTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(txtBeginDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        DateTime endTime = DateTime.Now;
        try
        {
            endTime = Convert.ToDateTime(txtEndDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        PptWeighDataManager.QueryParams queryParams = new PptWeighDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        //return pptWeighDataManager.GetWeighRatePageDataBySql(queryParams);
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.MaterCode = txtPptMaterial.Text;
        queryParams.BeginTime = beginTime.ToString("yyyy-MM-dd");
        queryParams.EndTime = endTime.ToString("yyyy-MM-dd");
        queryParams.WeightType = txtWeightType.Value.ToString();
        return GetOverErrorAllowPageDataBySql(queryParams);
    }

    private PageResult<PptWeighData> OverWeighGridPanelBindData2(PageResult<PptWeighData> pageParams)
    {
        DateTime beginTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(txtBeginDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        DateTime endTime = DateTime.Now;
        try
        {
            endTime = Convert.ToDateTime(txtEndDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        PptWeighDataManager.QueryParams queryParams = new PptWeighDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        //return pptWeighDataManager.GetWeighRatePageDataBySql(queryParams);
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.MaterCode = txtPptMaterial.Text;
        queryParams.BeginTime = beginTime.ToString("yyyy-MM-dd");
        queryParams.EndTime = endTime.ToString("yyyy-MM-dd");
        queryParams.WeightType = txtWeightType.Value.ToString();
        return GetOverErrorAllowPageDataBySql2(queryParams);
    }

    public PageResult<PptWeighData> GetOverErrorAllowPageDataBySql(PptWeighDataManager.QueryParams queryParams)
    {
        PageResult<PptWeighData> pageParams = queryParams.PageParams;

        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT t1.WeighTime as PlanDate,t3.EquipName,convert(int,right(rtrim(t1.barcode),3)) as num,t2.realnum,CASE WeighType WHEN '1' THEN '炭黑' WHEN '3' THEN '胶料' WHEN '2' THEN '油' WHEN '4' THEN '小料' ELSE WeighType end weightype,
                                    t2.RecipeMaterialName,t1.MaterName,
                                    t1.SetWeight,t1.RealWeight,
                                    t1.ErrorAllow,(t1.RealWeight - t1.SetWeight) as ErrorOut , case t1.WeighState when '0' then '手动' else '自动' end as WeighState
                                    FROM dbo.PptWeighData t1
                                    INNER JOIN dbo.PptPlan t2 ON t1.PlanID=t2.PlanID
                                    INNER JOIN dbo.BasEquip t3 ON t1.EquipCode=t3.EquipCode ");
        sqlstr.AppendLine(@" WHERE 1=1  AND ABS(case when  Round(t1.ErrorAllow,1) > t1.ErrorAllow then Round(t1.ErrorAllow,1)
else t1.ErrorAllow end) < ABS(t1.RealWeight-t1.SetWeight) ");//
        //if (!string.IsNullOrWhiteSpace(queryParams.BeginTime)) -1*t1.ErrorOut  
        //{
        //    sqlstr.AppendLine(@"AND t1.WeighTime>='" + queryParams.BeginTime + "'");
        //}
        //if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
        //{
        //    sqlstr.AppendLine(@"AND t1.WeighTime<='" + queryParams.EndTime + "'");
        //}
        sqlstr.AppendLine(@"AND t1.Barcode between '" + queryParams.BeginTime.Replace("-", "").Substring(2, 6) + "' and '" + queryParams.EndTime.Replace("-", "").Substring(2, 6) + "'");
        if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
        {
            sqlstr.AppendLine(@"AND t1.EquipCode='" + queryParams.EquipCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
        {
            sqlstr.AppendLine(@"AND t2.RecipeMaterialCode='" + queryParams.MaterCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.WeightType) && !"---请选择---".Equals(queryParams.WeightType))
        {
            sqlstr.AppendLine(@"AND t1.WeighType='" + queryParams.WeightType + "'");
        }

        if (!string.IsNullOrWhiteSpace(ComboBox1.Value.ToString()) && (!"---请选择---".Equals(ComboBox1.SelectedItem.Text)))
        {
            sqlstr.AppendLine(@" AND t1.WeighState='" + ComboBox1.Value.ToString() + "'");
        }

        if (!string.IsNullOrWhiteSpace(ComboBoxWorkShopId.Value.ToString()))
        {
            sqlstr.AppendLine(@" AND t3.workshopcode='" + ComboBoxWorkShopId.Value.ToString() + "'");
        }
        sqlstr.AppendLine(@"ORDER BY t1.Barcode,t1.WeighTime");
        //X.Js.Alert(sqlstr.ToString());

        //txtPptMaterial.Text = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pptWeighDataManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            pageParams.QueryStr = sqlstr.ToString();
            return pptWeighDataManager.GetPageDataByReader(pageParams);
        }
    }


    public PageResult<PptWeighData> GetOverErrorAllowPageDataBySql2(PptWeighDataManager.QueryParams queryParams)
    {
        PageResult<PptWeighData> pageParams = queryParams.PageParams;
        //-1*t1.ErrorOut
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT  t1.WeighTime as PlanDate,t3.EquipName,convert(int,right(rtrim(t1.barcode),3)) as num,t2.realnum,CASE WeighType WHEN '1' THEN '炭黑' WHEN '3' THEN '胶料' WHEN '2' THEN '油' WHEN '4' THEN '小料' ELSE WeighType end weightype,
                                    t2.RecipeMaterialName,t1.MaterName,
                                    t1.SetWeight,t1.RealWeight,
                                    t1.ErrorAllow, (t1.RealWeight-t1.SetWeight)  as ErrorOut , case t1.WeighState when '0' then '手动' else '自动' end as WeighState
                                    FROM dbo.PptWeighData t1
                                    INNER JOIN dbo.PptPlan t2 ON t1.PlanID=t2.PlanID
                                    INNER JOIN dbo.BasEquip t3 ON t1.EquipCode=t3.EquipCode ");
        sqlstr.AppendLine(@" WHERE 1=1  ");//
       
        sqlstr.AppendLine(@"AND t1.Barcode between '" + queryParams.BeginTime.Replace("-", "").Substring(2, 6) + "' and '" + queryParams.EndTime.Replace("-", "").Substring(2, 6) + "'");
        if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
        {
            sqlstr.AppendLine(@"AND t1.EquipCode='" + queryParams.EquipCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
        {
            sqlstr.AppendLine(@"AND t2.RecipeMaterialCode='" + queryParams.MaterCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.WeightType) && !"---请选择---".Equals(queryParams.WeightType))
        {
            sqlstr.AppendLine(@"AND t1.WeighType='" + queryParams.WeightType + "'");
        }
        if (!string.IsNullOrWhiteSpace(ComboBox1.Value.ToString()) && (!"---请选择---".Equals(ComboBox1.SelectedItem.Text)))
        {
            sqlstr.AppendLine(@" AND t1.WeighState='" + ComboBox1.Value.ToString() + "'");
        }
        if (!string.IsNullOrWhiteSpace(ComboBoxWorkShopId.Value.ToString()))
        {
            sqlstr.AppendLine(@" AND t3.workshopcode='" + ComboBoxWorkShopId.Value.ToString() + "'");
        }


        sqlstr.AppendLine(@"ORDER BY t1.Barcode,t1.WeighTime");
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pptWeighDataManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
         
            return pageParams;
        }
        else
        {
            pageParams.QueryStr = sqlstr.ToString();
           
            return pptWeighDataManager.GetPageDataByReader(pageParams);
        }
    }
    /// <summary>
    /// 
    /// 导出
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportOverWeighSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PptWeighData> pageParams = new PageResult<PptWeighData>();
        pageParams.PageSize = -100;
        PageResult<PptWeighData> lst = OverWeighGridPanelBindData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.OverWeighGridPanel.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "超差记录");


       
    }
    protected void btnExportOverWeighSubmit_Click2(object sender, EventArgs e)
    {
        PageResult<PptWeighData> pageParams = new PageResult<PptWeighData>();
        pageParams.PageSize = -100;
        PageResult<PptWeighData> lst = OverWeighGridPanelBindData2(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.OverWeighGridPanel2.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "称量记录");



    }
    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object OverWeighGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptWeighData> page = new PageResult<PptWeighData>();
        page.PageIndex = prms.Page;
        page.PageSize = prms.Limit;
        page = OverWeighGridPanelBindData(page);
        return new { data = page.DataSet.Tables[0], total = page.RecordCount };
    }


    [DirectMethod]
    public object OverWeighGridPanelBindData2(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptWeighData> page = new PageResult<PptWeighData>();
        page.PageIndex = prms.Page;
        page.PageSize = prms.Limit;
     
        page = OverWeighGridPanelBindData2(page);
        PageResult<PptWeighData> page2 = new PageResult<PptWeighData>();
        page2 = OverWeighGridPanelBindData(page2);
        txtTotalWeight.Text = "总数：" + page.RecordCount.ToString() + "超差数：" + page2.RecordCount.ToString();
        return new { data = page.DataSet.Tables[0], total = page.RecordCount };
    }
    #endregion

    #region 动态生成列
    private void ClearGridPanelColumnModel(GridPanel grid)
    {
        Store store = grid.GetStore();
        store.Reader.Clear();
        grid.SelectionModel.Clear();
        grid.ColumnModel.Columns.Clear();
        store.Model.Clear();
        store.Model.Add(new Model());
    }
    private void AddGridPanelColumnModel(GridPanel grid, string name)
    {
        grid.GetStore().Model[0].Fields.Add(new ModelField(name));
        grid.ColumnModel.Columns.Add(new Column { DataIndex = name, Text = name, Flex = 1 });
    }
    private void IniGridPanelColumnModel(GridPanel grid, DataTable dt)
    {
        foreach (DataColumn dc in dt.Columns)
        {
            string name = dc.ColumnName.ToString();
            AddGridPanelColumnModel(grid, name);
        }
        grid.GetStore().DataSource = dt;
        grid.GetStore().DataBind();
        grid.Render();
    }
    #endregion

    #region 查询显示合格率
    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptWeighData> WeightRateGridPanelBindData(PageResult<PptWeighData> pageParams)
    {
        DateTime beginTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(txtBeginDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        DateTime endTime = DateTime.Now;
        try
        {
            endTime = Convert.ToDateTime(txtEndDate.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        PptWeighDataManager.QueryParams queryParams = new PptWeighDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        //return pptWeighDataManager.GetWeighRatePageDataBySql(queryParams);
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.MaterCode = txtPptMaterial.Text;
        queryParams.BeginTime = beginTime.ToString("yyyy-MM-dd");
        queryParams.EndTime = endTime.ToString("yyyy-MM-dd");
        queryParams.WeightType = txtWeightType.Text;
        //return pptWeighDataManager.GetWeighRatePageDataBySql(queryParams);
        return GetWeighRatePageDataBySql(queryParams);
    }

    public PageResult<PptWeighData> GetWeighRatePageDataBySql(PptWeighDataManager.QueryParams queryParams)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("@StartDate", queryParams.BeginTime);
        dict.Add("@EndDate", queryParams.EndTime);
        dict.Add("@EquipCode", queryParams.EquipCode);
        dict.Add("@MaterCode", queryParams.MaterCode);
        dict.Add("@WeighType", "---请选择---".Equals(queryParams.WeightType) ? "" : queryParams.WeightType);
        dict.Add("@ProdShop", "".Equals(ComboBoxWorkShopId.Value.ToString().Trim()) ? "" : ComboBoxWorkShopId.Value.ToString().Trim());
        queryParams.PageParams.DataSet = procService.GetDataSetByStoreProcedure("ProcPptGetWeighRate", dict);

        return queryParams.PageParams;
    }

    /// <summary>
    /// 导出
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportWeighRateSubmit_Click(object sender, EventArgs e)
    {

        PageResult<PptWeighData> pageParams = new PageResult<PptWeighData>();
        pageParams.PageSize = -100;
        PageResult<PptWeighData> lst = WeightRateGridPanelBindData(pageParams);
        //for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        //{
        //    bool isshow = false;
        //    DataColumn dc = lst.DataSet.Tables[0].Columns[i];
        //    foreach (ColumnBase cb in this.WeighRateGridPanel.ColumnModel.Columns)
        //    {
        //        if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
        //        {
        //            dc.ColumnName = cb.Text;
        //            isshow = true;
        //            break;
        //        }
        //    }
        //    if (!isshow)
        //    {
        //        lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
        //        i--;
        //    }
        //}
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "合格率");

    }

    protected void btnSearchWeighRateClick(object sender, DirectEventArgs e)
    {
        PageResult<PptWeighData> page = new PageResult<PptWeighData>();
        page.PageSize = -1;
        page = WeightRateGridPanelBindData(page);
        DataTable data = page.DataSet.Tables[0];
        IniGridPanelColumnModel(this.WeighRateGridPanel, data);
        tabPanel1.ActiveIndex = 2;
    }
    #endregion
    protected void storeMaterial_ReadData(object sender, StoreReadDataEventArgs e)
    {
        try
        {
            PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
            queryParams.equipCode = hiddenEquipCode.Text;
            queryParams.planStartDate = txtBeginDate.Text;
            queryParams.planEndDate = txtEndDate.Text;
            txtPptMaterial.Items.Clear();
            if (string.IsNullOrEmpty(hiddenEquipCode.Text))
                return;
            EntityArrayList<BasMaterial> data = pptPlanManager.GetPlanPptMaterial(queryParams);
            data.Add(new BasMaterial() { MaterialCode = "", MaterialName="全部" });
            txtPptMaterial.GetStore().DataSource = data;
            txtPptMaterial.GetStore().DataBind();
            
        }
        catch
        {
        }
    }
}