using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Text;

public partial class Manager_Storage_StorageInfo : System.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            //txtBeginTime.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            //txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<PstStorage> GetPageResultData(PageResult<PstStorage> pageParams)
    {
        PstStorageManager.QueryParams queryParams = new PstStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.productNo = txtProductNo.Text;
        queryParams.barcode = txtBarcode.Text;
        queryParams.FacErpcode = txtFactory.Text;
        if (!cbxScreen.Checked)
            queryParams.IsEmptyWeight = "0";

        DataSet ds = GetStorageTotal(queryParams);
        txtTotalNum.Text = "数量合计:" + ds.Tables[0].Rows[0][0].ToString();
        txtTotalWeight.Text = "重量合计:" + ds.Tables[0].Rows[0][1].ToString();

        return GetTablePageDataBySql(queryParams);
    }

    public DataSet GetStorageTotal(PstStorageManager.QueryParams queryParams)
    {
        PageResult<PstStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select SUM(Num) TotalNum, SUM(RealWeight) TotalWeight from PstStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID
                                where 1 = 1  and B.storagetype = '0'  ");
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.factoryID))
        {
            sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.FacErpcode))
        {
            sqlstr.AppendLine(" AND SUBSTRING(A.Barcode, 15, 4) = '" + queryParams.FacErpcode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.productNo))
        {
            sqlstr.AppendLine(" AND A.ProductNo = '" + queryParams.productNo + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
        }

        //if (queryParams.beginDate != DateTime.MinValue)
        //    sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
        //if (queryParams.endDate != DateTime.MinValue)
        //    sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        //sqlstr.AppendLine(" AND A.RecordDate >= '" + DateTime.Parse(txtBeginTime.Text).ToString("yyyy-MM-dd") + "'");
        //sqlstr.AppendLine(" AND A.RecordDate <= '" + DateTime.Parse(txtEndTime.Text).AddDays(1).ToString("yyyy-MM-dd") + "'");
        if (Checkbox1.Checked)
        { sqlstr.AppendLine(" AND CONVERT(varchar(10), DATEADD(DAY, D.ValidDate, A.ProcDate), 120) <= '" + DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + "'"); }

        if (queryParams.IsEmptyWeight == "1")
            sqlstr.AppendLine(" AND A.num = 0");
        else if (queryParams.IsEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.num > 0");
        pageParams.QueryStr = sqlstr.ToString();

        return storageManager.GetBySql(sqlstr.ToString()).ToDataSet();
    }
    public PageResult<PstStorage> GetTablePageDataBySql(PstStorageManager.QueryParams queryParams)
    {
        PageResult<PstStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName
                                    , A.FactoryID, E.FacName, A.MaterCode, D.MaterialName, A.ProcDate
                                    , dbo.FuncGetValidDateByBarcode(A.Barcode) ValidDate
                                    , A.Num, A.PieceWeight, A.RealWeight, A.RecordDate, 0 NewNum
                                    , SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate
                                    , dbo.FuncGetStorageStatus(A.Barcode, A.StorageID, A.StoragePlaceID) Status, A.Batch
                                from PstStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID

                                where 1 = 1  and B.storagetype = '0'   ");
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.factoryID))
        {
            sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.FacErpcode))
        {
            sqlstr.AppendLine(" AND SUBSTRING(A.Barcode, 16, 6) = '" + queryParams.FacErpcode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.productNo))
        {
            sqlstr.AppendLine(" AND A.Batch like '%" + queryParams.productNo + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
        }
        //queryParams.beginDate = DateTime.Parse(txtBeginTime.Text);
        //queryParams.endDate = DateTime.Parse(txtEndTime.Text);
        //if (queryParams.beginDate != DateTime.MinValue)
        //    sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
        //if (queryParams.endDate != DateTime.MinValue)
        //    sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");

        //sqlstr.AppendLine(" AND A.RecordDate >= '" + DateTime.Parse(txtBeginTime.Text).ToString("yyyy-MM-dd") + "'");
        //sqlstr.AppendLine(" AND A.RecordDate <= '" + DateTime.Parse(txtEndTime.Text).AddDays(1).ToString("yyyy-MM-dd") + "'");
        if (queryParams.IsEmptyWeight == "1")
            sqlstr.AppendLine(" AND A.RealWeight = 0");
        else if (queryParams.IsEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.RealWeight > 0");

       // AND CONVERT(varchar(10), DATEADD(DAY, D.ValidDate, A.ProcDate), 120)
        if (Checkbox1.Checked)
        { sqlstr.AppendLine(" AND dbo.FuncGetValidDateByBarcode(A.Barcode) <= '" + DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + "'"); }

        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = storageManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return storageManager.GetPageDataBySql(pageParams);
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstStorage> pageParams = new PageResult<PstStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        //pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;
        string order = String.Empty;
        foreach (DataSorter sort in prms.Sort)
        {
            order = String.IsNullOrEmpty(order) ? sort.Property + " " + sort.Direction : order + "," + sort.Property + " " + sort.Direction;
        }
        if (!String.IsNullOrEmpty(order))
        {
            pageParams.Orderfld = order;
        }

        PageResult<PstStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PstStorage> pageParams = new PageResult<PstStorage>();
        pageParams.PageSize = -100;
        PageResult<PstStorage> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "原材料库存报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];

        this.storeDetail.DataSource = storageDetailManager.GetByInfo(barcode, storageID, storagePlaceID);
        this.storeDetail.DataBind();
    }

    protected void Search_Change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
    }
    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;

        if (field.Text != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须填写！";
        }
    }

    protected void CheckCombo(object sender, RemoteValidationEventArgs e)
    {
        ComboBox combo = (ComboBox)sender;

        if (combo.SelectedItem.Value != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须选择！";
        }
    }
    #endregion
}