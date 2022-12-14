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

public partial class Manager_Rubber_RubberStoreOut : BasePage
{ 
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            出库 = new SysPageAction() { ActionID = 2, ActionName = "Button1" };
            撤销出库 = new SysPageAction() { ActionID = 3, ActionName = "Button2" };
        
        }
        public SysPageAction 出库 { get; private set; } //必须为 public
        public SysPageAction 撤销出库 { get; private set; } //必须为 public
     
    }
    #endregion
    protected PpmRubberStorageManager rubberStorageManager = new PpmRubberStorageManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxShift.Value = "all";
            cbxStock.Value = "1";

            RecordDate.Hidden = false;
            Weight.Hidden = false;
            RealWeight.Hidden = true;
            OperPerson.Hidden = false;
            Column1.Hidden = false;
            Column2.Hidden = false;
        }
    }

    private PageResult<PpmRubberStorage> GetPageResultData(PageResult<PpmRubberStorage> pageParams)
    {
        PpmRubberStorageManager.QueryParams queryParams = new PpmRubberStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenToStorageCheckID.Text;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.stockFlag = cbxStock.SelectedItem.Value;
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;
        queryParams.Oper = hiddenMakerPerson.Text;
        return GetTablePageStoreoutBySql(queryParams);
    }
    public PageResult<PpmRubberStorage> GetTablePageStoreoutBySql(PpmRubberStorageManager.QueryParams queryParams)
    {
        PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();


        sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.ShelfBarcode, BarcodeStart,BarcodeEnd,
                                    ShelfNum, MemNote, A.PlanDate, A.ShiftID, D.ShiftName, A.ShiftClassID, E.ClassName, A.EquipCode, F.EquipName, 
                                    A.MaterCode, G.MaterialName, A.ValidDate, A.ProductWeight, A.StockFlag, A.CheckFlag, A.TecDealFlag, A.TecDealIdea,
                                    A.ConsumeWeight, A.RealWeight");
        if (queryParams.stockFlag == "1")
        {
            sqlstr.AppendLine(@",H.RecordDate, H.Weight,BS.StorageName as  ToStorageName,CS.StoragePlaceName as ToStoragePlaceName, u.UserName as OperPerson");
        }
        else
        {
            sqlstr.AppendLine(@",'' as RecordDate, '' as Weight,'' as  ToStorageName,'' as ToStoragePlaceName, '' as OperPerson");
        }
        sqlstr.AppendLine(@"from PpmRubberStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                
                                ");
        if (queryParams.stockFlag == "1")
        {
            sqlstr.AppendLine(@"left join PpmRubberStorageDetail H on H.OperType = '003' and A.StorageID = H.StorageID and A.StoragePlaceID = H.StoragePlaceID and A.Barcode = H.Barcode
                                left join BasStorage BS on H.ToStorageID = BS.StorageID
                                left join BasStoragePlace CS on H.ToStoragePlaceID = CS.StoragePlaceID
                                left join BasUser u on H.OperPerson = u.WorkBarcode  where 1 = 1 ");
        }
        else
        {
            sqlstr.AppendLine(@" where 1 = 1 ");
        }
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.Oper))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
        {
            if (queryParams.stockFlag == "1")
            {
                sqlstr.AppendLine(" AND H.ToStorageID = '" + queryParams.storagePlaceID + "'");
            }
        }
        if (!string.IsNullOrEmpty(queryParams.Oper))
        {
            if (queryParams.stockFlag == "1")
            {
                sqlstr.AppendLine(" AND H.OperPerson = '" + queryParams.Oper + "'");
            }
        }
        if (queryParams.stockFlag == "0")
        {
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            }
        }
        else
        {
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND H.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND H.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            }
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }
        if (queryParams.stockFlag == "1")
        {
            sqlstr.AppendLine(" and H.OperType = '003'");
        }
        if (queryParams.shiftID != "all")
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

        if ((!string.IsNullOrEmpty(queryParams.shiftClassID)) && queryParams.shiftClassID != "all")
            sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

        //if (queryParams.stockFlag != "all")
        if ((!string.IsNullOrEmpty(queryParams.stockFlag)) && queryParams.stockFlag != "all")
            sqlstr.AppendLine(" AND A.StockFlag = '" + queryParams.stockFlag + "'");

        if (queryParams.isEmptyWeight == "0")
            sqlstr.AppendLine(" AND A.RealWeight > 0");

        pageParams.QueryStr = sqlstr.ToString();
        //txtBarcode.Text = sqlstr.ToString();
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
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberStorage> pageParams = new PageResult<PpmRubberStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PpmRubberStorage> lst = GetPageResultData(pageParams);
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
        if (cbxStock.SelectedItem.Value == "0")
        {
            RecordDate.Hidden = true;
            Weight.Hidden = true;
            RealWeight.Hidden = false;
            OperPerson.Hidden = true;
            Column1.Hidden = true;
            Column2.Hidden = true;
        }
        else
        {
            RecordDate.Hidden = false;
            Weight.Hidden = false;
            RealWeight.Hidden = true;
            OperPerson.Hidden = false;
            Column1.Hidden = false;
            Column2.Hidden = false;
        }

        pageToolBar.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchStock_Click()
    {

        try
        {
            WebReference.Service ws2 = new WebReference.Service();
            ws2.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
            string checkResult = ws2.CheckRubOutVal("", "", hiddenCheckBarcode.Text);
           
            if (checkResult != "OK")
            { X.Js.Alert(checkResult); return  "";  }
        }
        catch (Exception ex)
        {
         
            //this.AppendWebLog("群科中间表插入" + ex.Message, "群科中间表插入");
        }


     





        DataSet ds = shiftTimeManager.GetShiftDS("1", "");
        string shiftID = ds.Tables[0].Rows[0][0].ToString();
        string shiftClassID = ds.Tables[0].Rows[0][1].ToString();

      


       
        string result = rubberStorageManager.SubmitRubberStoreOut(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text, shiftID, shiftClassID, this.UserID, hiddenToStorageID.Text, hiddenToStoragePlaceID.Text);
        pageToolBar.DoRefresh();
        this.winStorage.Close();

        try 
        {
            WebReference.Service ws = new WebReference.Service();
            ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
            ws.InsertRubOutToMidServer(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text, shiftID, shiftClassID, this.UserID, hiddenToStorageID.Text, hiddenToStoragePlaceID.Text);
        }
        catch(Exception ex)
        {
            this.AppendWebLog("群科中间表插入" + ex.Message, "群科中间表插入");
        }

        if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
        {
            try
            {
                //WebReference.Service ws = new WebReference.Service();
                //ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                //ws.ToSapRubberAdjustInfo(hiddenCheckBarcode.Text, "0", "0");
            }
            catch (Exception ex)
            {
                //return ex.ToString();
            }
        }

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelStock_Click()
    {
        if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
        {
            try
            {
                //WebReference.Service ws = new WebReference.Service();
                //ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                //ws.ToSapRubberAdjustInfo(hiddenCheckBarcode.Text, "1", "0");
            }
            catch (Exception ex)
            {
                //return ex.ToString();
            }
        }

        string result = rubberStorageManager.CancelReturnRubber(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text);
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

        if(hiddenStockFlag.Text=="1")
        {
            X.MessageBox.Alert("提示", "您选择的项已出库，请检查！").Show();
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
        hiddenStockFlag.Text = e.ExtraParams["StockFlag"];
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
        hiddenCheckStorageID.Text = e.ExtraParams["StorageID"];
        hiddenCheckStoragePlaceID.Text = e.ExtraParams["StoragePlaceID"];
    }
    
}
