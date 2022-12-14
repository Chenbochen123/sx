using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;

public partial class Manager_Equipment_SapSparePart_SparePartOut : Mesnac.Web.UI.Page
{
    protected IEqmSparePartRepairOutManager manager = new EqmSparePartRepairOutManager();//业务对象
    protected IEqmSparePartStoreManager storeManager = new EqmSparePartStoreManager();//业务对象
    protected IEqmSparePartManager sparePartManager = new EqmSparePartManager();//业务对象
    protected IEqmSparePartMainTypeManager majorTypeManager = new EqmSparePartMainTypeManager();//业务对象
    protected IEqmSparePartDetailTypeManager minorTypeManager = new EqmSparePartDetailTypeManager();//业务对象
    protected IBasEquipManager equipManager = new BasEquipManager();//业务对象
    protected IBasUserManager userManager = new BasUserManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<EqmSapSparePart> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SendNo"]))
            {
                string sendNo = Request.QueryString["SendNo"].ToString();
                txtSendNo.Text = sendNo;
            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmSparePartRepairOut> GetPageResultData(PageResult<EqmSparePartRepairOut> pageParams)
    {
        EqmSparePartRepairOutManager.QueryParams queryParams = new EqmSparePartRepairOutManager.QueryParams();
        queryParams.pageParams = pageParams;
        try
        {
            queryParams.beginSendDate =
                Convert.ToDateTime(txt_begin_send_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_begin_send_date.Value.ToString();
        }
        catch (Exception)
        {
        }
        try
        {
            queryParams.endSendDate =
                Convert.ToDateTime(txt_end_send_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_end_send_date.Value.ToString();
        }
        catch (Exception)
        {
        }
        queryParams.sparePartCode = hidden_select_sparepart_code.Text;
        queryParams.sendNo = txtSendNo.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return "";
        //}
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmSparePartRepairOut> pageParams = new PageResult<EqmSparePartRepairOut>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<EqmSparePartRepairOut> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<EqmSparePartRepairOut> pageParams = new PageResult<EqmSparePartRepairOut>();
        pageParams.PageSize = -100;
        PageResult<EqmSparePartRepairOut> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "备件出库信息");
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        hidden_send_user.Text = "";
        hidden_sparepart_code.Text = "";
        add_send_date.Text = DateTime.Now.ToString();
        add_sparepart_code.Text = "";
        add_model.Text = "";
        add_number.Text = "0";
        add_send_user.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        EqmSparePartRepairOut partOut = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = partOut.ObjID.ToString();
        modify_send_no.Text = partOut.SendNo.ToString();
        modify_send_date.Text = partOut.SendDate.ToString();
        try
        {
            modify_sparepart_code.Text = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == partOut.SparePartCode)[0].SparePartName;
            hidden_sparepart_code.Text = partOut.SparePartCode;
        }
        catch (Exception)
        {
        }
        modify_model.Text = partOut.SparePartModel.ToString();
        modify_number.Text = partOut.StoreOutNum.ToString();
        try
        {
            modify_send_user.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == partOut.SendUser)[0].UserName;
            hidden_send_user.Text = partOut.SendUser;
        }
        catch (Exception)
        {
        }
        modify_remark.Text = partOut.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            EqmSparePartRepairOut partOut = manager.GetById(objID);
            partOut.DeleteFlag = "1";
            string flag = storeManager.UpdateSparePartStore(true, partOut.SparePartCode, partOut.SparePartModel, Convert.ToDecimal(partOut.StoreOutNum));
            if (flag.IndexOf("ERROR") > -1)
            {
                return flag;
            }
            manager.Update(partOut);
            this.AppendWebLog("备件出库记录删除", "出库单号：" + partOut.SendNo);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }


    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            EqmSparePartRepairOut partOut = new EqmSparePartRepairOut();
            partOut.SendDate = Convert.ToDateTime(add_send_date.Text);
            partOut.SendNo = manager.GetNextSparePartStoreOutCode(Convert.ToDateTime(add_send_date.Text));
            partOut.SparePartCode = hidden_sparepart_code.Text;
            partOut.SparePartModel = add_model.Text;
            partOut.StoreOutNum = Convert.ToDecimal(add_number.Text == "" ? "1" : add_number.Text);
            partOut.SendUser = hidden_send_user.Text;
            partOut.RecordDate = DateTime.Now;
            partOut.Remark = (string)(add_remark.Text);
            partOut.DeleteFlag = "0";
            string flag = storeManager.UpdateSparePartStore(false, partOut.SparePartCode, partOut.SparePartModel, Convert.ToDecimal(partOut.StoreOutNum));
            if (flag.IndexOf("ERROR") > -1)
            {
                msg.Notify("操作", flag).Show();
                return;
            }
            manager.Insert(partOut);
            this.AppendWebLog("备件出库记录增加", "出库单号：" + partOut.SendNo);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            EqmSparePartRepairOut partOut = manager.GetById(modify_obj_id.Text);
            partOut.SendNo = modify_send_no.Text;
            partOut.SendDate = Convert.ToDateTime(modify_send_date.Text);
            partOut.SparePartCode = hidden_sparepart_code.Text;
            partOut.SparePartModel = modify_model.Text;
            decimal oldStoreOutNum = Convert.ToDecimal(partOut.StoreOutNum);
            partOut.StoreOutNum = Convert.ToDecimal(modify_number.Text == "" ? "1" : modify_number.Text);
            partOut.SendUser = hidden_send_user.Text;
            partOut.RecordDate = DateTime.Now;
            partOut.Remark = (string)(modify_remark.Text);
            partOut.DeleteFlag = "0";

            string flag = storeManager.UpdateSparePartStore(
                ( oldStoreOutNum - partOut.StoreOutNum ) > 0,
                partOut.SparePartCode, 
                partOut.SparePartModel, 
                Math.Abs(Convert.ToDecimal(oldStoreOutNum - partOut.StoreOutNum)));
            if (flag.IndexOf("ERROR") > -1)
            {
                msg.Notify("操作", flag).Show();
                return;
            }
            manager.Update(partOut);
            this.AppendWebLog("备件出库记录修改", "出库单号：" + partOut.SendNo);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 选择机台后关联型号值改变
    public void Hidden_SparePart_Code_Change_Event(object sender, DirectEventArgs e)
    {
        EntityArrayList<EqmSparePart> sparePartList = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == hidden_sparepart_code.Value);
        if (sparePartList.Count > 0)
        {
            EntityArrayList<EqmSparePartDetailType> minorList = minorTypeManager.GetListByWhere(EqmSparePartDetailType._.DetailTypeCode == sparePartList[0].SparePartDetailType && EqmSparePartDetailType._.MainTypeID == sparePartList[0].SparePartMainType);
            EntityArrayList<EqmSparePartMainType> majorList = majorTypeManager.GetListByWhere(EqmSparePartMainType._.ObjID == sparePartList[0].SparePartMainType);

            if (minorList.Count > 0 && majorList.Count > 0)
            {
                add_model.Text = majorList[0].MainTypeName + "-" + minorList[0].DetailTypeName;
            }
        }
    }
    #endregion
}