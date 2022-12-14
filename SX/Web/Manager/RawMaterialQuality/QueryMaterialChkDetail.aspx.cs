using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Text;

public partial class Manager_RawMaterialQuality_QueryMaterialChkDetail : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected IPstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected IPstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    #endregion

    #region 页面加载
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.dtfSendTime.Value = DateTime.Now.Date;//初始化送检日期
            //this.cbxStatus.SelectedItem.Index = 0;
        }
    }
    #endregion

    #region 分页方法
    /// <summary>
    /// 查询送检单
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<PstMaterialChkDetail> GetPageResultData(PageResult<PstMaterialChkDetail> pageParams)
    {
        PstMaterialChkDetailManager.QueryParams queryParams = new PstMaterialChkDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text.TrimEnd().TrimStart();
        queryParams.SendChkFlag = "1";
        if (cbxStatus.Value.ToString() != "")
        {
            queryParams.chkResultFlag = cbxStatus.Value.ToString();//0代表未检测，1代表检测通过，2代表检测不通过，9代表检验中
        }
        if (txtManualAddMaterialCode.Text.ToString() != "")
        {
            queryParams.materCode = txtManualAddMaterialCode.Value.ToString();
        }
        queryParams.beginDate = dtfSendTime.Value.ToString();
        string beginDate = "";
        string endDate = "";
        try
        {
            DateTime temp = Convert.ToDateTime(queryParams.beginDate).AddDays(1);
            endDate = temp.ToString();
        }
        catch (Exception ex)
        {
            queryParams.beginDate = beginDate;
        }
        queryParams.endDate = endDate;
        return GetCheckSequence(queryParams);
    }
    public PageResult<PstMaterialChkDetail> GetCheckSequence(PstMaterialChkDetailManager.QueryParams queryParams)
    {
        PageResult<PstMaterialChkDetail> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.ObjID,A.BillNo,A.Barcode,A.OrderId,A.MaterCode,B.NoticeNo, C.MaterialName from PstMaterialChkDetail A
                                left join PstMaterialChk B on A.BillNo = B.BillNo
                                left join BasMaterial C on A.MaterCode = C.MaterialCode
                                where A.DeleteFlag = '0'
                                and B.DeleteFlag = '0'");
        if (!string.IsNullOrEmpty(queryParams.billNo))
        {
            sqlstr.AppendFormat("AND A.BillNo LIKE '%{0}%'", queryParams.billNo);
            sqlstr.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendFormat("AND A.Barcode LIKE '%{0}%'", queryParams.barcode);
            sqlstr.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendFormat("AND A.MaterCode = '{0}'", queryParams.materCode);
            sqlstr.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.beginDate) && !string.IsNullOrEmpty(queryParams.endDate))
        {
            if (!(queryParams.beginDate.Contains("0001")) && !(queryParams.endDate.Contains("0001")))
            {
                sqlstr.AppendLine("AND A.RecordDate BETWEEN '" + queryParams.beginDate + "' AND '" + queryParams.endDate + "'");
            }
        }
        if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
        {
            if (queryParams.chkResultFlag == "null")
            {
                sqlstr.AppendLine(" AND A.ChkResultFlag IS NULL");
            }
            else
            {
                sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
            }
        }
        if (!string.IsNullOrEmpty(queryParams.SendChkFlag))
        {
            sqlstr.AppendLine(" AND A.SendChkFlag = '" + queryParams.SendChkFlag + "'");
        }
        if (!string.IsNullOrEmpty(TextBarcode.Text))
        {
            sqlstr.AppendLine(" AND A.Barcode like  '%" + TextBarcode.Text + "%'");
        }

        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = detailManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return detailManager.GetPageDataBySql(pageParams);
        }
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChkDetail> pageParams = new PageResult<PstMaterialChkDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";
        PageResult<PstMaterialChkDetail> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}