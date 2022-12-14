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

/// <summary>
/// Manager_Technology_ProductReview_Search 实现类
/// 孙本强 @ 2013-04-03 13:05:10
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_ProductReview_Search : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            详细信息查看 = new SysPageAction() { ActionID = 2 };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 详细信息查看 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptClassManager pptClassManager = new PptClassManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptShiftManager pptShiftManager = new PptShiftManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    /// <summary>
    /// 袁洋 @ 2014年3月4日09:44:41
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// 
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
        if (this.IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        //if (X.IsAjaxRequest)
        //{
        //    return;
        //}
        txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
        txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptClass._.UseFlag == "1");
        order = PptClass._.ObjID.Asc;
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        txtPptClass.Items.Clear();
        txtPptClass.Items.Add(allitem);
        foreach (PptClass m in pptClassManager.GetListByWhereAndOrder(where, order))
        {
            txtPptClass.Items.Add(new ListItem(m.ClassName, m.ObjID));
        }
        txtPptClass.Text = (txtPptClass.Items[0].Value);

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
        txtPptShift.Text = (txtPptClass.Items[0].Value);
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

    #region 分页加载相关
    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptLotData> GetPageResultData(PageResult<PptLotData> pageParams)
    {
        try
        {
            Convert.ToDateTime(txtBeginTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        try
        {
            Convert.ToDateTime(txtEndTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        PptLotDataManager.QueryParams queryParams = new PptLotDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.BeginTime = Convert.ToDateTime(txtBeginTime.Text).ToString("yyMMdd");
        queryParams.EndTime = Convert.ToDateTime(txtEndTime.Text).ToString("yyMMdd");
        queryParams.ClassID = txtPptClass.Text.Replace(constSelectAllText, "");
        queryParams.ShiftID = txtPptShift.Text.Replace(constSelectAllText, "");
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.MaterCode = hiddenMaterialCode.Text;
        queryParams.ShelfBarcode = txtshiftbarcode.Text;
        queryParams.Barcode = txtBarcode.Text;
        return pptLotDataManager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:05:11
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (refreshHidden.Value.ToString() != "1")//首次进入页面不加载的处理方式，以后可以进行改变。
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "SeqIdx";

        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 点击列表记录加载右侧基本信息数据
    [Ext.Net.DirectMethod]
    public void BasicInfoLoad(string barcode)
    {
        
        Dictionary<string, string>[] dics = JSON.Deserialize<Dictionary<string, string>[]>(barcode);
        foreach (Dictionary<string, string> row in dics)
        {
            detailUserName.Text = "";
            detailShelfBarcode.Text = row["ShelfBarcode"];
            detailBarcode.Text = row["Barcode"];
            detailEquipName.Text = row["EquipName"];
            detailShiftName.Text = row["ShiftName"];
            detailClassName.Text = row["ClassName"];
            detailMaterName.Text = row["MaterName"];
            detailPlanID.Text = row["PlanID"];
            detailSerialID.Text = row["SerialID"];
            detailStartDatetime.Text = row["StartDatetime"];
            detailSetWeight.Text = row["SetWeight"];
            detailRealWeight.Text = row["RealWeight"];
            detailTestResultName.Text = row["TestResultName"];
            detailSerialBatchID.Text = row["SerialBatchID"];
            detailMixStatusName.Text = row["MixStatusName"];
            detailDoneAllRtime.Text = row["DoneAllRtime"];
            detailLotEnergy.Text = row["LotEnergy"];
            detailBwbTime.Text = row["BwbTime"];
            detailPjTemp.Text = row["PjTemp"];
            detailPolyDisTime.Text = row["PolyDisTime"];
            detailCBDisTime.Text = row["CBDisTime"];
            detailOilDisTime.Text = row["OilDisTime"];
            EntityArrayList<BasUser> userList = basUserManager.GetListByWhere(BasUser._.HRCode == row["Workerbarcode"]);
            if (userList.Count > 0)
            {
                detailUserName.Text = userList[0].UserName;
            }
            detailMemNote.Text = row["MemNote"];
        }

    }
    #endregion


    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
      
        DataSet data = new DataSet();

 
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex =  - 100;
        pageParams.PageSize = 99999;
        pageParams.Orderfld = "SeqIdx";

        PageResult<PptLotData> lst = GetPageResultData(pageParams);
    

         for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
         {
             bool isshow = false;
             DataColumn dc = lst.DataSet.Tables[0].Columns[i];
             foreach (ColumnBase cb in this.gridPanelCenter.ColumnModel.Columns)
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

         data = lst.DataSet;

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(data, "条码追溯");
    }
}