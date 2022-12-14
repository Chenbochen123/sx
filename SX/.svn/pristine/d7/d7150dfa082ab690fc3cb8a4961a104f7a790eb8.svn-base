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

public partial class Manager_Storage_MaterialChkBill : Mesnac.Web.UI.Page
{
    private PstMaterialChkManager manager = new PstMaterialChkManager();
    private BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    private BasMaterialManager materManager = new BasMaterialManager();
    private PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    private BasUserManager userManager = new BasUserManager();

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            审核 = new SysPageAction() { ActionID = 2, ActionName = "Button1" };
            撤销审核 = new SysPageAction() { ActionID = 3, ActionName = "Button2" };
            放行 = new SysPageAction() { ActionID = 4, ActionName = "Button3" };
            取消放行 = new SysPageAction() { ActionID = 5, ActionName = "Button4" };
          
        }
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 撤销审核 { get; private set; } //必须为 public
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 取消放行 { get; private set; } //必须为 public

    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(Request.QueryString["BillNO"]))
            {
                string billNo = Request.QueryString["BillNO"].ToString();
                string barcode = Request.QueryString["Barcode"].ToString();
                string orderID = Request.QueryString["OrderID"].ToString();

                txtBillNo.Text = billNo;
                this.storeDetail.DataSource = detailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            cbxFiledFlag.SelectedItem.Value = "all";
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialChk> GetPageResultData(PageResult<PstMaterialChk> pageParams)
    {
        PstMaterialChkManager.QueryParams queryParams = new PstMaterialChkManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.noticeNo = txtNoticeNo.Text;
        queryParams.factoryID = hiddenFactoryID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.hrCode = hiddenMakerPerson.Text;

        if (cbxSendChkFlag.SelectedItem.Value != "all")
            queryParams.sendChkFlag = cbxSendChkFlag.SelectedItem.Value;
        if (cbxFiledFlag.SelectedItem.Value != "all")
            queryParams.filedFlag = cbxFiledFlag.SelectedItem.Value;
        queryParams.deleteFlag = "0";

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PstMaterialChk> GetTablePageDataBySql(PstMaterialChkManager.QueryParams queryParams)
    {
                    //if (record.get("SendChkFlag") == "1") {
        PageResult<PstMaterialChk> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select distinct A.BillNo, A.NoticeNo, A.FactoryID, B.FacName, convert(bit, A.SendChkFlag) SendChkFlag, A.MakerPerson, C.UserName, convert(bit, A.FiledFlag) FiledFlag, A.Remark 
                              ,case D.ChkResultFlag when '1' then '合格'  when '2' then '不合格' else '未质检' end  as ChkResultFlag,
 case A.stockinflag when '1' then '入库' else '未入库' end  as stockinflag

from PstMaterialChk A
                                left join BasFactoryInfo B on A.FactoryID = B.ObjID
                                left join BasUser C on A.MakerPerson = C.WorkBarcode
  left join  PstMaterialChkdetail D on A.BillNo=D.BillNo
                                where 1 = 1");
        if (!string.IsNullOrEmpty(queryParams.billNo))
        {
            sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.noticeNo))
        {
            sqlstr.AppendLine(" AND A.NoticeNo like '%" + queryParams.noticeNo + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.factoryID))
        {
            sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.InStockDate >= '" + queryParams.beginDate.ToString() + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.InStockDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        if (!string.IsNullOrEmpty(queryParams.sendChkFlag))
        {
            sqlstr.AppendLine(" AND A.SendChkFlag = '" + queryParams.sendChkFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.stockInFlag))
        {
            sqlstr.AppendLine(" AND A.StockInFlag = '" + queryParams.stockInFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.filedFlag))
        {
            sqlstr.AppendLine(" AND A.FiledFlag = '" + queryParams.filedFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
        {
            sqlstr.AppendLine(" AND D.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.hrCode))
        {
            sqlstr.AppendLine(" AND C.HRCode = '" + queryParams.hrCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.NoPassFlag))
        {
            sqlstr.AppendLine(" AND D.SendNum - isnull(D.PassNum, 0) > 0");
        }
        if (ComboBox3.SelectedItem.Value=="1")
        {
            sqlstr.AppendLine(" AND D.ChkResultFlag ='1' and A.stockinflag = '0'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChk> pageParams = new PageResult<PstMaterialChk>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo desc";

        PageResult<PstMaterialChk> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = GetByBillNo(billNo, string.Empty);
        this.storeDetail.DataBind();
    }

    public DataSet GetByBillNo(string BillNo, string IsStoreIn)
    {
        string sql = @"select BillNo, ProductNo, Barcode, OrderID, MaterCode, B.MaterialName MaterialName, ProcDate, SendChkDate, SendNum, 
                            PieceWeight, SendWeight, ChkDate, RecordDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, 
                            convert(bit, SendChkFlag) SendChkFlag, A.DeleteFlag, A.Remark, A.PassNum, A.PassNum-A.StoreInNum LastNum, 
                            A.PassWeight-A.StoreInNum*A.PieceWeight LastWeight, StoreInNum, StoreInWeight, 0 NewNum, A.LLBarcode
                          , case fxflag when '1' then '放行' else '' end  as FXFlag
from PstMaterialChkDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "'";
        if (!string.IsNullOrEmpty(IsStoreIn))
            sql += " and A.StoreInNum = '0'";
        return detailManager.GetBySql(sql).ToDataSet();
    }
    #endregion

    #region 增删改查按钮激发的事件


    [Ext.Net.DirectMethod()]
    public string btnBatchSend_Click()
    {
        //string strBillNo = string.Empty;
        //foreach (SelectedRow row in this.rowSelectMutiDetail.SelectedRows)
        //{
        //    strBillNo += "'" + row.RecordID + "', ";
        //}

        //return strBillNo;
        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            PstMaterialChk chk = manager.GetById(row.RecordID);
            if (chk.SendChkFlag == "1")
                return "该单据已经审核";

        }

        bool result = manager.UpdateSendChkFlag(strBillNo.Remove(strBillNo.Length - 2, 1), this.UserID);
        if (result == true)
        {
            pageToolBar.DoRefresh();
            this.AppendWebLog("送检审核", "条码号：" + strBillNo);
            return "送检审核成功！";
        }
        else
        {
            return "送检审核失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click()
    {
       
        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo = row.RecordID ;


        }
        string strBarcode = string.Empty;
        foreach (SelectedRow row in this.rowSelectMutiDetail.SelectedRows)
        {
            strBarcode =row.RecordID ;
           

        }
 
        String sql = "update PstMaterialChkDetail set FXFlag = '1' where barcode = '" + strBarcode + "' and billno = '" + strBillNo + "'";
        manager.GetBySql(sql).ToDataSet();
        this.AppendWebLog("原材料放行", "条码号：" + strBarcode);
        PagingToolbar1.DoRefresh();
        return "放行成功！";
       
    }
    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click2()
    {

        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo = row.RecordID;


        }
        string strBarcode = string.Empty;
        foreach (SelectedRow row in this.rowSelectMutiDetail.SelectedRows)
        {
            strBarcode = row.RecordID;


        }
       
        String sql = "update PstMaterialChkDetail set FXFlag = null where barcode = '" + strBarcode + "' and billno = '" + strBillNo + "'";
        manager.GetBySql(sql).ToDataSet();
        this.AppendWebLog("原材料取消放行", "条码号：" + strBarcode);
        PagingToolbar1.DoRefresh();
        return "取消放行成功！";

    }

    [Ext.Net.DirectMethod()]
    public string btnCancelSend_Click()
    {
        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            PstMaterialChk chk = manager.GetById(row.RecordID);
            if (chk.SendChkFlag == "0")
                return "该单据未审核，不能撤销！";
            EntityArrayList<PstMaterialChkDetail> detailList = detailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == chk.BillNo && PstMaterialChkDetail._.DeleteFlag == "0");
            for (int i = 0; i < detailList.Count; i++)
            {
                if (detailList[i].StoreInNum > 0 || detailList[i].StoreInWeight > 0)
                {
                    return "该单据已经入库，不能撤销！";
                }
            }
        }

        bool result = manager.CancelSendChk(strBillNo.Remove(strBillNo.Length - 2, 1), this.UserID);
        if (result == true)
        {

            this.AppendWebLog("撤销送检审核", "条码号：" + strBillNo);
            pageToolBar.DoRefresh();
            return "撤销成功！";
        }
        else
        {
            return "撤销失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            EntityArrayList<PstMaterialChkDetail> details = detailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo && PstMaterialChkDetail._.ChkResultFlag == "1");
            if (details.Count > 0)
            {
                return "false";
            }
            PstMaterialChk store = manager.GetById(billNo);
            store.DeleteFlag = "1";

            manager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        try
        {
            //判断是否可以归档，根据是该数据所对应的明细数据全部全部处理完毕，SendChkFlag标志为1
            EntityArrayList<PstMaterialChkDetail> list = detailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo && PstMaterialChkDetail._.SendChkFlag == null && PstMaterialChkDetail._.DeleteFlag == "0");
            if (list.Count > 0)
            {
                return "不能归档：该送检单没有处理完毕，请首先处理该数据对应的明细数据！";
            }

            PstMaterialChk store = manager.GetById(billNo);
            store.FiledFlag = "1";

            manager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已归档";
    }

 
    public void txtBeginTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxSendChkFlag_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
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



    protected void btnPrintBarcode2_Click(object sender, DirectEventArgs e)
    {


        this.winPrint2.Show();

    }
    protected void btnPrintBarcode_Click(object sender, DirectEventArgs e)
    {
        string barcode = e.ExtraParams["Barcode"];
        string matName = e.ExtraParams["MatName"];

        this.txtBarcode.SetValue(barcode);
        this.txtMatName.SetValue(matName);
        this.txtLLProductNo.SetValue("");

        WebReport1.Report.Clear();
        WebReport1.Report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        this.winPrint.Show();
    }
    protected void btnBuildBarcode_Click(object sender, DirectEventArgs e)
    {
        string barcode = this.txtBarcode.Value.ToString();
        string matName = this.txtMatName.Value.ToString();
        string productNo = this.txtLLProductNo.Text.Trim();

        if (string.IsNullOrWhiteSpace(barcode))
        {
            X.Msg.Alert("提示", "没有条码号").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(matName))
        {
            X.Msg.Alert("提示", "没有物料").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(productNo))
        {
            X.Msg.Alert("提示", "没有批次").Show();
            return;
        }

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("PrintBarcode.frx"));

        report.SetParameterValue("Barcode", barcode);
        report.SetParameterValue("MatName", matName);
        report.SetParameterValue("ProductNo", productNo);

        WebReport1.Report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();


    }

    protected void btnBuildBarcode2_Click(object sender, DirectEventArgs e)
    {
        string barcode = this.txtBarcode2.Value.ToString();
        string matName = this.txtMatName2.Value.ToString();
        string productNo = this.txtLLProductNo2.Text.Trim();

        if (string.IsNullOrWhiteSpace(barcode))
        {
            X.Msg.Alert("提示", "没有条码号").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(matName))
        {
            X.Msg.Alert("提示", "没有物料").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(productNo))
        {
            X.Msg.Alert("提示", "没有批次").Show();
            return;
        }

        //初始化报表控件
        FastReport.Report report = this.WebReport2.Report;
        report.Load(Server.MapPath("PrintBarcode.frx"));

        report.SetParameterValue("Barcode", barcode);
        report.SetParameterValue("MatName", matName);
        report.SetParameterValue("ProductNo", productNo);

        WebReport2.Report.Refresh();
        WebReport2.Update();
        WebReport2.Refresh();

    }
    protected void BuildBarcode2(object sender, DirectEventArgs e)
    {
        string matCode = this.hdnMatCode2.Value == null ? "" : this.hdnMatCode2.Value.ToString();
        if (string.IsNullOrWhiteSpace(matCode))
        {
            this.txtBarcode2.SetValue("");
            return;
        }
        string facId = this.hdnFacId2.Value == null ? "" : this.hdnFacId2.Value.ToString();
        if (string.IsNullOrWhiteSpace(facId))
        {
            this.txtBarcode2.SetValue("");
            return;
        }

        string productDate = string.IsNullOrWhiteSpace(this.txtProductDate2.RawText) ? "" : this.txtProductDate2.SelectedDate.ToString("yyMMdd");
        if (string.IsNullOrWhiteSpace(productDate))
        {
            this.txtBarcode2.SetValue("");
            return;
        }

        string facSapCode = new BasFactoryInfoManager().GetById(facId).ERPCode.Trim().PadLeft(6, '0');
        string matSapCode = new BasMaterialManager().GetListByWhere(BasMaterial._.DeleteFlag == 0 & BasMaterial._.MaterialCode == matCode)[0].SAPMaterialCode;

        this.txtBarcode2.SetValue(matSapCode + productDate + facSapCode);

    }

}