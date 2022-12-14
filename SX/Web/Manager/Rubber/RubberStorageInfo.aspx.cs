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
using Mesnac.Web.UI;

public partial class Manager_Rubber_RubberStorageInfo : BasePage
{
    protected PpmRubberStorageManager rubberStorageManager = new PpmRubberStorageManager();
    protected PpmRubberStorageDetailManager rubberStorageDetailManager = new PpmRubberStorageDetailManager();
    protected BasUserManager userManager = new BasUserManager();
    //protected PstStorageManager storageManager = new PstStorageManager();
    //protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
      
            放行 = new SysPageAction() { ActionID = 7, ActionName = "ButtonFX" };
            取消放行 = new SysPageAction() { ActionID = 8, ActionName = "ButtonQXFX" };
        }
     
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 取消放行 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxShift.Value = "all";
            cbxStock.Value = "0";
        }
    }

    #region 分页相关方法
 
    private PageResult<PpmRubberStorage> GetPageResultData(PageResult<PpmRubberStorage> pageParams)
    {
        PpmRubberStorageManager.QueryParams queryParams = new PpmRubberStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.stockFlag = cbxStock.SelectedItem.Value;
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;
        if (!cbxScreen.Checked)
            queryParams.isEmptyWeight = "0";
        DataSet ds = GetStorageTotal(queryParams);
        //txtTotalNum.Text = "数量合计:" + ds.Tables[0].Rows[0][0].ToString();
        txtTotalWeight.Text = "重量合计:" + ds.Tables[0].Rows[0][0].ToString();

        return GetTablePageDataBySql(queryParams);
    }

    public PageResult<PpmRubberStorage> GetTablePageDataBySql(PpmRubberStorageManager.QueryParams queryParams)
    {
        PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.ShelfBarcode, A.BarcodeStart,A.BarcodeEnd,Convert(nvarchar(10),A.BarcodeStart)+'-'+Convert(nvarchar(10),A.BarcodeEnd) as 'Checi',
	                                A.ShelfNum, A.MemNote, A.PlanDate, A.ShiftID, D.ShiftName, A.ShiftClassID, E.ClassName, A.EquipCode, F.EquipName, 
	                                A.MaterCode, G.MaterialName, A.ValidDate, A.ProductWeight, A.StockFlag, A.CheckFlag, A.TecDealFlag, A.TecDealIdea,H.ShiftBarcode,
	                                A.ConsumeWeight, A.RealWeight, A.RecordDate, A.OperPerson,A.RealNum, case A.fxflag when '1' then '放行' else '' end as fxflag
                                from PpmRubberStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
left join pptshiftconfig H on A.Barcode = H.Barcode
                                where 1 = 1 and a.storageplaceid not in ('010002001','010001001','010004000') ");
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
        }
        if (queryParams.endDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }

        if (queryParams.shiftID != "all")
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

        if (queryParams.shiftClassID != "all")
            sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

        if (queryParams.stockFlag != "all")
            sqlstr.AppendLine(" AND A.StockFlag = '" + queryParams.stockFlag + "'");

        if (queryParams.isEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.RealWeight > 0");

        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = rubberStorageManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return rubberStorageManager.GetPageDataBySql(pageParams);
        }

    }
    public DataSet GetStorageTotal(PpmRubberStorageManager.QueryParams queryParams)
    {
        PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select SUM(RealWeight) TotalWeight
                                from PpmRubberStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1  and a.storageplaceid not in ('010002001','010001001','010004000') ");
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
        }
        if (queryParams.endDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }

        if (queryParams.shiftID != "all")
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

        if (queryParams.shiftClassID != "all")
            sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

        if (queryParams.stockFlag != "all")
            sqlstr.AppendLine(" AND A.StockFlag = '" + queryParams.stockFlag + "'");

        if (queryParams.isEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.RealWeight > 0");
        return rubberStorageManager.GetBySql(sqlstr.ToString()).ToDataSet() ;
       

    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberStorage> pageParams = new PageResult<PpmRubberStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        string order = String.Empty;
        foreach (DataSorter sort in prms.Sort)
        {
            order = String.IsNullOrEmpty(order) ? sort.Property + " " + sort.Direction : order + "," + sort.Property + " " + sort.Direction;
        }
        if (!String.IsNullOrEmpty(order))
        {
            pageParams.Orderfld = order;
        }

        PageResult<PpmRubberStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubberStorage> pageParams = new PageResult<PpmRubberStorage>();
        pageParams.PageSize = -100;
        PageResult<PpmRubberStorage> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料库存报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];
        FBarcode.Text = barcode;
        FStoragePlaceID.Text = storagePlaceID;
        this.storeDetail.DataSource = GetByInfo(barcode, storageID, storagePlaceID);//rubberStorageDetailManager.
        this.storeDetail.DataBind();
    }
    public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
    {
        string sql = @"select Barcode, OrderID, OperType, PlanDate, ShiftID, B.ShiftName, ShiftClassID, C.ClassName,B.ShiftName+'-'+C.ClassName as 'Banci',MaterCode, D.MaterialName, 
	                            Weight*StockType as Weight,	ToStorageID, E.StorageName, ToStoragePlaceID, F.StoragePlaceName, RecordDate, OperPerson,Num*StockType as Num,G.EquipName
                            from PpmRubberStorageDetail A
	                            left join PptShift B on A.ShiftID = B.ObjID
	                            left join PptClass C on A.ShiftClassID = C.ObjID
                                 left join BasEquip G on a.EquipCode = G.EquipCode
	                            left join BasMaterial D on A.MaterCode = D.MaterialCode
	                            left join BasStorage E on A.ToStorageID = E.StorageID
	                            left join BasStoragePlace F on A.ToStoragePlaceID = F.StoragePlaceID
                            where 1 = 1 and Barcode ='" + Barcode + "' and A.StorageID = '" + StorageID + "' and A.StoragePlaceID = '" + StoragePlaceID + "'";
        return rubberStorageDetailManager.GetBySql(sql).ToDataSet();
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


    protected void ButtonFX_Click(object sender, DirectEventArgs e)
    {

        if (String.IsNullOrEmpty(FBarcode.Text))
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        String sql = "update PpmRubberStorage set FXFlag = '1' where barcode = '" + FBarcode.Text + "' and StoragePlaceID = '" + FStoragePlaceID.Text + "'";
        rubberStorageDetailManager.GetBySql(sql).ToDataSet();
        try
        {
            this.AppendWebLog("胶料放行，放行人编号：" + this.UserID + "，编号：" + FBarcode.Text);
        }
        catch (Exception ex)
        {
            this.AppendWebLog("胶料放行", "日志记录导常:" + ex.Message);
        }
        X.Msg.Alert("提示", "放行成功").Show();
        //this.pageToolBar.DoRefresh();
    }


    protected void ButtonQXFX_Click(object sender, DirectEventArgs e)
    {

        if (String.IsNullOrEmpty(FBarcode.Text))
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        String sql = "update PpmRubberStorage set FXFlag = Null where barcode = '" + FBarcode.Text + "' and StoragePlaceID = '" + FStoragePlaceID.Text + "'";
        rubberStorageDetailManager.GetBySql(sql).ToDataSet();
        try
        {
            this.AppendWebLog("取消胶料放行，放行人编号：" + this.UserID + "，编号：" + FBarcode.Text);
        }
        catch (Exception ex)
        {
            this.AppendWebLog("取消胶料放行", "日志记录导常:" + ex.Message);
        }
        X.Msg.Alert("提示", "取消放行成功").Show();
        //this.pageToolBar.DoRefresh();
    }
}
