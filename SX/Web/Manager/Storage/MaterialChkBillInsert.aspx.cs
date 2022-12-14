using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;  
using System.Collections;  

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_Storage_MaterialChkBillInsert : Mesnac.Web.UI.Page
{
    protected PstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected PstMaterialChkDetailManager chkdetailManager = new PstMaterialChkDetailManager();
    protected BasUserManager userManager = new BasUserManager();
    protected BasMaterialManager materManager = new BasMaterialManager();
    protected BasFactoryInfoManager facManager = new BasFactoryInfoManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            this.Session["TempChkDetail"] = null;
            txtSendChkDate.Text = DateTime.Now.ToString();
            txtMakerPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;//this.User.Identity.Name
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
            {
                BindData(Request.QueryString["BillNo"].ToString());
            }
            else
            {
                hiddenBillNo.Text = Guid.NewGuid().ToString();
            }
        }
    }

    private PageResult<PstMaterialChkDetail> GetPageResultData(PageResult<PstMaterialChkDetail> pageParams)
    {
        PstMaterialChkDetailManager.QueryParams queryParams = new PstMaterialChkDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return chkdetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChkDetail> pageParams = new PageResult<PstMaterialChkDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PstMaterialChkDetail> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void BindData(string billNo)
    {
        PstMaterialChk chkBill = chkManager.GetById(billNo);
        PstMaterialChkDetail chkDetail1 = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo && PstMaterialChkDetail._.DeleteFlag == "0")[0];
        txtBillNo.Text = chkBill.BillNo;
        hiddenBillNo.Text = chkBill.BillNo;
        txtNoticeNo.Text = chkBill.NoticeNo;
        txtFactoryID.Text = facManager.GetById(chkBill.FactoryID).FacName;
        hiddenFactoryID.Text = chkBill.FactoryID.ToString();
        txtSendChkDate.Text = chkDetail1.SendChkDate.ToString();
        txtRemark.Text = chkBill.Remark;
        //btnSave.Disabled = false;

        EntityArrayList<PstMaterialChkDetail> chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo);
        //DataSet ds = chkdetailManager.GetByBillNo(billNo);

        this.Session["TempChkDetail"] = chkDetail;
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        data = new Mesnac.Util.BaseInfo.DictionaryList().GenericListTo(chkDetail.ToArray());
        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, object> dic = data[i];
            dic.Add("MaterName", GetMaterName(chkDetail[i].MaterCode));
        }

        this.store.DataSource = data;
        this.store.DataBind();
    }

    public string GetMaterName(string materCode)
    {
        return materManager.GetMaterName(materCode);
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        ////允许批次条码在一个单据下重复，如果不允许将下列代码注释去掉即可
        //EntityArrayList<PstMaterialChkDetail> chkDetails = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.Barcode == txtBarcode3.Text);
        //if (chkDetails.Count > 0)
        //{
        //    X.MessageBox.Alert("操作", "列表中已存在" + txtBarcode3.Text + "的条码,不能重复！").Show();
        //    return;
        //}
        EntityArrayList<PstMaterialChkDetail> details = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.Barcode == txtBarcode3.Text && PstMaterialChkDetail._.DeleteFlag == "0");
        foreach (PstMaterialChkDetail chkDetail1 in details)
        {
            if (chkDetail1.MaterCode != hiddenMaterCode.Text)
            {
                X.MessageBox.Alert("操作", "其他单据已存在" + txtBarcode2.Text + "的条码，物料为" + materManager.GetMaterName(details[0].MaterCode)).Show();
                return;
            }
        }
        if (txtBarcode3.Text.Length != 21)
        {
            X.Js.Alert("条码号必须为21位");
            //X.MessageBox.Alert("操作", "条码号必须为21位");
          
            return;
        }
        if (1 == 1)
        {
          

            string sql = "select erpcode from basmaterial where materialcode = '" + hiddenMaterCode.Text + "' ";
            DataSet ds = chkdetailManager.GetBySql(sql).ToDataSet();

            if (txtBarcode3.Text.Substring(0, 9) != ds.Tables[0].Rows[0][0].ToString())
            {
                X.Js.Alert( "条码与所选物料不符");
                return;
            }
        }



        PstMaterialChkDetail chkDetail = new PstMaterialChkDetail();
        chkDetail.BillNo = hiddenBillNo.Text;
        chkDetail.Barcode = txtBarcode3.Text;
        EntityArrayList<PstMaterialChkDetail> chkDetails = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.DeleteFlag == "0");
        int maxOrder = 0;
        for (int i = 0; i < chkDetails.Count; i++)
        {
            if (chkDetails[i].OrderID > maxOrder)
                maxOrder = chkDetails[i].OrderID;
        }
        maxOrder++;
        chkDetail.OrderID = maxOrder;
        chkDetail.ProductNo = txtBarcode3.Text;
        chkDetail.MaterCode = hiddenMaterCode.Text;
        chkDetail.ProcDate = Convert.ToDateTime(txtProcDate3.Text);
        chkDetail.SendNum = Convert.ToInt32(txtSendNum3.Text);
        chkDetail.PieceWeight = Convert.ToDecimal(txtPieceWeight3.Text);
        chkDetail.SendWeight = Convert.ToDecimal(txtSendWeight3.Text);
        chkDetail.RecordDate = DateTime.Now;
        chkDetail.StoreInNum = 0;
        chkDetail.DeleteFlag = "0";
        chkDetail.Remark = txtRemark3.Text;
        chkDetail.LLBarcode = chkdetailManager.GetLLBarcode(txtBarcode3.Text);
        //if (txtBarcode3.Text.Substring(15, 6) == "008021" || txtBarcode3.Text.Substring(15, 6) == "008022")
        //{
        //    chkDetail.ChkResultFlag = chkdetailManager.GetChkResult(txtBarcode3.Text);
        //    chkDetail.ChkDate = DateTime.Now;
        //    chkDetail.ChkPerson = "000001";
        //}
        chkDetail.ChkResultFlag = chkdetailManager.GetChkResult(txtBarcode3.Text);
        if (chkDetail.ChkResultFlag != "0")
        {
            chkDetail.ChkDate = DateTime.Now;
            chkDetail.ChkPerson = "000001";
        }

        chkdetailManager.Insert(chkDetail);
        pageToolBar.DoRefresh();

        txtBarcode3.Text = string.Empty;
        txtProductNo3.Text = string.Empty;
        txtMaterialName3.Text = string.Empty;
        txtProcDate3.Text = Convert.ToString(DateTime.Now);
        txtSendNum3.Text = string.Empty;
        txtPieceWeight3.Text = string.Empty;
        txtSendWeight3.Text = string.Empty;
        txtRemark3.Text = string.Empty;
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode, string orderID)
    {
        PstMaterialChkDetail chkDetail = chkdetailManager.GetEntity(hiddenBillNo.Text, barcode, orderID);

        txtBarcode2.Text = chkDetail.Barcode;
        txtProductNo2.Text = chkDetail.ProductNo;
        hiddenBarcode.Text = chkDetail.Barcode;
        hiddenOrderID.Text = orderID;
        txtMaterialName2.Text = materManager.GetMaterName(chkDetail.MaterCode);
        hiddenMaterCode.Text = chkDetail.MaterCode;
        txtProcDate2.Text = chkDetail.ProcDate.ToString();
        txtSendNum2.Text = chkDetail.SendNum.ToString();
        txtPieceWeight2.Text = chkDetail.PieceWeight.ToString();
        txtSendWeight2.Text = chkDetail.SendWeight.ToString();
        txtRemark2.Text = chkDetail.Remark;

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            EntityArrayList<PstMaterialChkDetail> chkDetails = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.DeleteFlag == "0");

            PstMaterialChkDetail chkDetail = chkdetailManager.GetEntity(hiddenBillNo.Text, hiddenBarcode.Text, hiddenOrderID.Text);
            foreach (PstMaterialChkDetail chkDetail1 in chkDetails)
            {
                if (hiddenBarcode.Text != txtBarcode2.Text && chkDetail1.Barcode == txtBarcode2.Text)
                {
                    X.MessageBox.Alert("操作", "列表中已存在" + txtBarcode2.Text + "的条码,不能重复！").Show();
                    return;
                }
            }
            //判断其他单据是否有相同的条码和物料
            EntityArrayList<PstMaterialChkDetail> details = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.Barcode == txtBarcode2.Text && PstMaterialChkDetail._.DeleteFlag == "0");
            foreach (PstMaterialChkDetail chkDetail1 in details)
            {
                if (chkDetail1.MaterCode != hiddenMaterCode.Text)
                {
                    X.MessageBox.Alert("操作", "其他单据已存在" + txtBarcode2.Text + "的条码，物料为" + materManager.GetMaterName(details[0].MaterCode)).Show();
                    return;
                }
            }

            chkDetail.Barcode = hiddenBarcode.Text;//txtBarcode2.Text如果修改之后因为是主键，必须用hiddenBarcode.Text
            chkDetail.ProductNo = hiddenBarcode.Text;
            chkDetail.MaterCode = hiddenMaterCode.Text;
            chkDetail.ProcDate = Convert.ToDateTime(txtProcDate2.Text);
            chkDetail.SendNum = Convert.ToInt32(txtSendNum2.Text);
            chkDetail.PieceWeight = Convert.ToDecimal(txtPieceWeight2.Text);
            chkDetail.SendWeight = Convert.ToDecimal(txtSendWeight2.Text);
            chkDetail.RecordDate = DateTime.Now;
            chkDetail.Remark = txtRemark2.Text;

            chkdetailManager.Update(chkDetail);
            if (txtBarcode2.Text != hiddenBarcode.Text)
                chkdetailManager.Update(new PropertyItem[] { PstMaterialChkDetail._.Barcode }, new object[] { txtBarcode2.Text }, PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.Barcode == hiddenBarcode.Text);

            pageToolBar.DoRefresh();
            this.winModifyDetail.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public String DeleteChkDetail(string barcode, string orderID)
    {
        EntityArrayList<PstMaterialChkDetail> chkDetails = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.DeleteFlag == "0");
        if (chkDetails.Count <= 1)
        {
            return "false";
        }
        PstMaterialChkDetail chkDetail = chkdetailManager.GetEntity(hiddenBillNo.Text, barcode, orderID);

        chkdetailManager.Delete(chkDetail);
        return "true";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtBarcode3.Text = string.Empty;
        txtProductNo3.Text = string.Empty;
        txtMaterialName3.Text = string.Empty;
        txtProcDate3.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtSendNum3.Text = string.Empty;
        txtPieceWeight3.Text = string.Empty;
        txtSendWeight3.Text = string.Empty;
        txtRemark3.Text = string.Empty;

        this.winAddDetail.Show();
    }

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialChkDetail> chkDetails = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == hiddenBillNo.Text && PstMaterialChkDetail._.DeleteFlag == "0");
        if (chkDetails.Count == 0)
        {
            return "false";
        }

        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
            {
                PstMaterialChk chkBill = chkManager.GetById(Request.QueryString["BillNo"].ToString());
                chkBill.NoticeNo = txtNoticeNo.Text;
                chkBill.FactoryID = Convert.ToInt32(hiddenFactoryID.Text);
                chkBill.InStockDate = DateTime.Now;
                chkBill.MakerPerson = userManager.UserID;
                chkBill.Remark = txtRemark.Text;

                chkManager.Update(chkBill);
                chkdetailManager.Update(new PropertyItem[] { PstMaterialChkDetail._.SendChkDate }, new object[] { Convert.ToDateTime(txtSendChkDate.Text) }, PstMaterialChkDetail._.BillNo == chkDetails[0].BillNo);
            }
            else
            {
                PstMaterialChk chkBill = new PstMaterialChk();
                chkBill.BillNo = chkManager.GetBillNo();
                chkBill.NoticeNo = txtNoticeNo.Text;
                chkBill.FactoryID = Convert.ToInt32(hiddenFactoryID.Text);
                chkBill.InStockDate = DateTime.Now;
                chkBill.MakerPerson = userManager.UserID;
                chkBill.SendChkFlag = "0";
                chkBill.FiledFlag = "0";
                chkBill.DeleteFlag = "0";
                chkBill.Remark = txtRemark.Text;

                chkManager.Insert(chkBill);

                chkdetailManager.Update(new PropertyItem[] { PstMaterialChkDetail._.BillNo, PstMaterialChkDetail._.SendChkDate }, new object[] { chkBill.BillNo, Convert.ToDateTime(txtSendChkDate.Text) }, PstMaterialChkDetail._.BillNo == chkDetails[0].BillNo);
            }

            //将控件清空还原
            BackControl();
            return "添加成功！";
        }
        catch (Exception ex)
        {
            return "不能添加失败：" + ex.ToString();
        }
    }

    public void BackControl()
    {
        txtNoticeNo.Text = string.Empty;
        txtFactoryID.Text = string.Empty;
        txtRemark.Text = string.Empty;

        store.Data = null;
        store.DataBind();
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAddDetail.Close();
        this.winModifyDetail.Close();
    }

    public void txtSend_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtSendNum3.Text) && !string.IsNullOrEmpty(txtSendWeight3.Text))
                txtPieceWeight3.Text = String.Format("{0:N3}", Convert.ToDecimal(txtSendWeight3.Text) / Convert.ToInt32(txtSendNum3.Text));
        }
        catch
        { }
    }

    public void txtSend2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtSendNum2.Text) && !string.IsNullOrEmpty(txtSendWeight2.Text))
                txtPieceWeight2.Text = String.Format("{0:N3}", Convert.ToDecimal(txtSendWeight2.Text) / Convert.ToInt32(txtSendNum2.Text));
        }
        catch
        { }
    }

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
}