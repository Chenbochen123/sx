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

public partial class Manager_Technology_QueryRecipe_QueryRecipeSimple : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            对比 = new SysPageAction() { ActionID = 2, ActionName = "btnComparison" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 对比 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:55
    /// </summary>
    private IPmtRecipeLogManager pmtRecipeManager = new PmtRecipeLogManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:55
    /// </summary>
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:55
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
        txtBeginTime.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
        txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// Defaults the HTML.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        DataTable data = new DataTable();
        int total = 0;
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PmtRecipeLogManager.QueryParams queryParams = new PmtRecipeLogManager.QueryParams();
        queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize = prms.Limit;
        queryParams.PageParams.Orderfld = "RecipeVersionID DESC,LogRecordTime DESC";

        queryParams.BaginTime = txtBeginTime.Text;
        queryParams.EndTime = txtEndTime.Text;
        queryParams.RecipeEquipCode = txtRecipeEquipCode.Text;
        queryParams.RecipeWorkShopCode = txtRecipeWorkShopCode.Text;
        queryParams.RecipeMaterialCode = txtRecipeMaterCode.Text;
        queryParams.RecipeName = txtRecipeName.Text;
        PageResult<PmtRecipeLog> lst = pmtRecipeManager.GetTablePageDataBySql(queryParams);
        data = lst.DataSet.Tables[0];

        total = lst.RecordCount;
        return new { data, total };
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PmtRecipeLogManager.QueryParams queryParams = new PmtRecipeLogManager.QueryParams();
        //queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize =-100;
        queryParams.PageParams.Orderfld = "RecipeVersionID DESC,LogRecordTime DESC";

        queryParams.BaginTime = txtBeginTime.Text;
        queryParams.EndTime = txtEndTime.Text;
        queryParams.RecipeEquipCode = txtRecipeEquipCode.Text;
        queryParams.RecipeWorkShopCode = txtRecipeWorkShopCode.Text;
        queryParams.RecipeMaterialCode = txtRecipeMaterCode.Text;
        queryParams.RecipeName = txtRecipeName.Text;
        PageResult<PmtRecipeLog> lst = pmtRecipeManager.GetTablePageDataBySql(queryParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.gridPanel1.ColumnModel.Columns)
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "配方日志查询");
    }
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = "SJ161027027";
        billNo = e.Parameters["BillNo"];
        //X.Js.Alert(billNo); return;
        PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
        //WhereClip where = new WhereClip();
        //where.And(PmtRecipeWeight._.RecipeObjID == "61684");
        //where.And(PmtRecipeWeight._.WeightType == "2");
        //OrderByClip order = new OrderByClip();
        //order = PmtRecipeWeight._.WeightID.Asc;
        //lst = pmtRecipeWeightManager.GetListByWhereAndOrder(where, order);
        String sql = @"select materiallevel,pmtRecipeWeightlog.* ,t3.showname as showname from pmtRecipeWeightlog 
left join basmaterial on pmtRecipeWeightlog.materialcode = basmaterial.materialcode
left join PmtWeightAction t3 on pmtRecipeWeightlog.ActCode = t3.ActionCode
where WeightType = '2' 
and logobjid ='" + billNo + "' ";
        this.storeDetail.DataSource = detailManager.GetBySql(sql).ToDataSet();
        this.storeDetail.DataBind();

        return;
        this.storeDetail.DataSource = detailManager.GetByBillNo(billNo, string.Empty);
        this.storeDetail.DataBind();
    }
    #endregion
}