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

public partial class Manager_ShopStorage_ShopStorageInfo : Mesnac.Web.UI.Page
{
    private PstShopStorageManager manager = new PstShopStorageManager();
    protected BasUserManager userManager = new BasUserManager();
    protected PstShopStorageManager storageManager = new PstShopStorageManager();
    protected PstShopStorageDetailManager storageDetailManager = new PstShopStorageDetailManager();

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            放行 = new SysPageAction() { ActionID = 2, ActionName = "Button3" };
            取消放行 = new SysPageAction() { ActionID = 3, ActionName = "Button4" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 取消放行 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            
        }
    }

    #region 分页相关方法
    private PageResult<PstShopStorage> GetPageResultData(PageResult<PstShopStorage> pageParams)
    {
        PstShopStorageManager.QueryParams queryParams = new PstShopStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.productNo = txtProductNo.Text;
        queryParams.barcode = txtBarcode.Text;
        queryParams.FacErpcode = txtFactory.Text;
        queryParams.llbarcode = txtBoxCode.Text;
        DataSet ds = storageManager.GetShopStorageTotal(queryParams);
        txtTotalNum.Text = "数量合计:" + ds.Tables[0].Rows[0][0].ToString();
        txtTotalWeight.Text = "重量合计:" + ds.Tables[0].Rows[0][1].ToString();

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PstShopStorage> GetTablePageDataBySql(PstShopStorageManager.QueryParams queryParams)
    {
        //case when (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) )>0 then  (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) ) else 0 end  as RealWeight,  CONVERT(varchar(10), DATEADD(DAY, D.ValidDate, A.ProcDate), 120) ValidDate 
        PageResult<PstShopStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.FactoryID, E.FacName, A.MaterCode, D.MaterialName, A.ProcDate, dbo.FuncGetValidDateByBarcode (A.Barcode) ValidDate, A.Num,
A.PieceWeight,
 A.RealWeight, 
A.RecordDate, 0 NewNum, SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate,case when A.Fxflag ='1' then '放行' else ''end as Fxflag
                                from PstShopStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID
                                ");
        if (!string.IsNullOrEmpty(queryParams.llbarcode))
        {
            sqlstr.AppendLine("inner join (select barcode,storageid,storageplaceid from PstShopStorageDetail where boxcode like '%" + queryParams.llbarcode + "%') h on a.barcode = h.barcode and a.storageid = h.storageid and a.storageplaceid = h.storageplaceid ");
        }
        sqlstr.AppendLine("  where 1 = 1 and  len( A.Barcode) = 21  ");

        if (string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" and  (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) )>0  ");
        }



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
            sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
        }
        if (cbxFxfalgFlag.SelectedItem.Value == "1") sqlstr.AppendLine(" AND A.fxflag = '1'");
        if (cbxFxfalgFlag.SelectedItem.Value == "0") sqlstr.AppendLine(" AND  A.fxflag is null");
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        if (queryParams.IsEmptyWeight == "1")
            sqlstr.AppendLine(" AND A.num = 0");
        else if (queryParams.IsEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.num > 0");
        pageParams.QueryStr = sqlstr.ToString();
        //txtBoxCode.Text = sqlstr.ToString();
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
        PageResult<PstShopStorage> pageParams = new PageResult<PstShopStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;
        string order = String.Empty;
        foreach (DataSorter sort in prms.Sort)
        {
            order = String.IsNullOrEmpty(order) ? sort.Property + " " + sort.Direction : order + "," + sort.Property + " " + sort.Direction;
        }
        if (!String.IsNullOrEmpty(order))
        {
            pageParams.Orderfld = order;
        }

        PageResult<PstShopStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PstShopStorage> pageParams = new PageResult<PstShopStorage>();
        pageParams.PageSize = -100;
        PageResult<PstShopStorage> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "车间原材料库存报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];
        string boxcode = this.txtBoxCode.Text.Trim();
        this.storeDetail.DataSource = storageDetailManager.GetByInfo(barcode, storageID, storagePlaceID,boxcode);
        this.storeDetail.DataBind();
    }
    #endregion



    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click()
    {

        //string strBarcode = string.Empty;
        //foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        //{
        //    strBarcode = row.RecordID;
          


        //}


        String sql = "update PstShopStorage set FXFlag = '1' where barcode = '" + hiddenCheckBarcode.Text + "'";
        manager.GetBySql(sql).ToDataSet();
        this.AppendWebLog("车间原材料放行", "条码号：" + hiddenCheckBarcode.Text);
        pageToolBar.DoRefresh();
        return "放行成功！" ;

    }
    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click2()
    {

        string strBarcode = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBarcode = row.RecordID;


        }


        String sql = "update PstShopStorage set FXFlag = null where barcode = '" + hiddenCheckBarcode.Text + "' ";
        manager.GetBySql(sql).ToDataSet();
        this.AppendWebLog("车间原材料取消放行", "条码号：" + hiddenCheckBarcode.Text);
        pageToolBar.DoRefresh();
        return "取消放行成功！";

    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
      
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
  
    }
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