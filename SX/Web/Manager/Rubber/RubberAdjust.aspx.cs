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

public partial class Manager_Rubber_RubberAdjust : Mesnac.Web.UI.Page
{
    protected PpmRubberAdjustManager rubberAdjustManager = new PpmRubberAdjustManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxShift.Value = "all";
            //cbxAdjust.Value = "0";
        }
    }

    private PageResult<PpmRubberAdjust> GetPageResultData(PageResult<PpmRubberAdjust> pageParams)
    {
        PpmRubberAdjustManager.QueryParams queryParams = new PpmRubberAdjustManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;
       // queryParams.adjustFlag = cbxAdjust.SelectedItem.Value;
        return GetTablePageDataBySql(queryParams);
        //return rubberAdjustManager.GetTablePageDataBySql(queryParams);
    }
    public PageResult<PpmRubberAdjust> GetTablePageDataBySql(PpmRubberAdjustManager.QueryParams queryParams)
    {
        PageResult<PpmRubberAdjust> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select distinct I.Barcode, I.StorageID, C.StorageName, I.StoragePlaceID, D.StoragePlaceName, A.barcode as ShelfBarcode, A.BarcodeStart, A.BarcodeEnd,
	                                A.ShelfNum, A.MemNote, A.PlanDate, A.ShiftID, E.ShiftName, A.ClassID, F.ClassName, A.EquipCode, G.EquipName, I.RecordDate,
	                                A.MaterialCode, A.MaterialName, A.ValidDate, A.StockFlag, A.RealWeight, I.AdjustWeight, case when A.RealWeight > 0 then '0' else '1' end AdjustFlag,
	                                I.ToStorageID, J.StorageName ToStorageName, I.ToStoragePlaceID, K.StoragePlaceName ToStoragePlaceName
                                from PpmRubberAdjust I  
 left join pptshiftconfig A  on I.barcode=A.barcode
	                                 left join BasStorage C on I.StorageID = C.StorageID
	                                left join BasStoragePlace D on I.StoragePlaceID = D.StoragePlaceID
	                                left join PptShift E on A.ShiftID = E.ObjID
	                                left join PptClass F on A.ClassID = F.ObjID
	                                left join BasEquip G on A.EquipCode = G.EquipCode
	                              
	                              
	                                left join BasStorage J on I.ToStorageID = J.StorageID
	                                left join BasStoragePlace K on I.ToStoragePlaceID = K.StoragePlaceID
                                where 1 = 1 ");

          //left join PpmRubberStorageDetail B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
	                             
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND I.Barcode = '" + queryParams.barcode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND I.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            sqlstr.AppendLine(" AND I.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND I.recorddate >= '" + queryParams.beginDate.ToString() + "'");
        }
        if (queryParams.endDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND I.recorddate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterialCode = '" + queryParams.materCode + "'");
        }

        if (queryParams.shiftID != "all")
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

        if (queryParams.shiftClassID != "all")
            sqlstr.AppendLine(" AND A.ClassID = '" + queryParams.shiftClassID + "'");

        //if (queryParams.adjustFlag == "0")
        //    sqlstr.AppendLine(" AND A.RealWeight > 0");
        //else if (queryParams.adjustFlag == "1")
        //    sqlstr.AppendLine(" and (A.RealWeight = 0 and B.OperType = '002' and B.RubberType = 'O')");
        //else
        //    sqlstr.AppendLine(" and (A.RealWeight > 0 or (A.RealWeight = 0 and B.OperType = '002' and B.RubberType = 'O'))");

        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = rubberAdjustManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return rubberAdjustManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberAdjust> pageParams = new PageResult<PpmRubberAdjust>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PpmRubberAdjust> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void txtQueryTime_change(object sender, EventArgs e)
    {
        if ((txtBeginTime.Text != DateTime.MinValue.ToString()) && (txtEndTime.Text != DateTime.MinValue.ToString()))
        {
            if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
            {
                X.MessageBox.Alert("提示", "开始时间不能大于结束时间!").Show();
                return;
            }
        }
        this.pageToolBar.DoRefresh();
    }
    public void page_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchStock_Click()
    {
        DataSet ds = shiftTimeManager.GetShiftDS("1", "");
        string shiftID = ds.Tables[0].Rows[0][0].ToString();
        string shiftClassID = ds.Tables[0].Rows[0][1].ToString();

        string result = rubberAdjustManager.SubmitRubberAdjust(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text, Convert.ToDecimal(hiddenCheckRealWeight.Text), this.UserID, shiftID, shiftClassID, hiddenToStorageID.Text, hiddenToStoragePlaceID.Text);
        pageToolBar.DoRefresh();
        this.winStorage.Close();

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelStock_Click()
    {
        string result = rubberAdjustManager.CancelRubberAdjust(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text, Convert.ToDecimal(hiddenCheckAdjustWeight.Text), hiddenCheckToStorageID.Text, hiddenCheckToStoragePlaceID.Text);
        pageToolBar.DoRefresh();

        return result;
    }

    protected void btnSetToStorage_Click(object sender, DirectEventArgs e)
    {
        if (this.rowSelectMuti.SelectedRows.Count == 0)
        {
            X.MessageBox.Alert("提示", "您没有选择任何项，请选择!").Show();
            return;
        }

        if (hiddenCheckAdjustFlag.Text == "1")
        {
            X.MessageBox.Alert("提示", "您选择的项已调拨，请检查！").Show();
            return;
        }

        this.winStorage.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winStorage.Close();
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenCheckAdjustFlag.Text = e.ExtraParams["AdjustFlag"];
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
        hiddenCheckRealWeight.Text = e.ExtraParams["RealWeight"];
        hiddenCheckAdjustWeight.Text = e.ExtraParams["AdjustWeight"];
        hiddenCheckStorageID.Text = e.ExtraParams["StorageID"];
        hiddenCheckStoragePlaceID.Text = e.ExtraParams["StoragePlaceID"];
        hiddenCheckToStorageID.Text = e.ExtraParams["ToStorageID"];
        hiddenCheckToStoragePlaceID.Text = e.ExtraParams["ToStoragePlaceID"];
    }

}
