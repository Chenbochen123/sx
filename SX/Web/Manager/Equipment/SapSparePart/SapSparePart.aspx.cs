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

public partial class Manager_Equipment_SapSparePart_SapSparePart : Mesnac.Web.UI.Page
{
    protected IEqmSapSparePartManager manager = new EqmSapSparePartManager();//业务对象
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
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmSapSparePart> GetPageResultData(PageResult<EqmSapSparePart> pageParams)
    {
        EqmSapSparePartManager.QueryParams queryParams = new EqmSapSparePartManager.QueryParams();
        queryParams.pageParams = pageParams;
        try
        {
            queryParams.beginReceiveDate =
                Convert.ToDateTime(txt_begin_receive_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_begin_receive_date.Value.ToString();
        }
        catch (Exception)
        {
        }
        try
        {
            queryParams.endReceiveDate = 
                Convert.ToDateTime(txt_end_receive_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_end_receive_date.Value.ToString();
        }
        catch (Exception)
        {
        }
        queryParams.sparepartCode = hidden_select_sparepart_code.Text;
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
        PageResult<EqmSapSparePart> pageParams = new PageResult<EqmSapSparePart>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<EqmSapSparePart> lst = GetPageResultData(pageParams);
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
        PageResult<EqmSapSparePart> pageParams = new PageResult<EqmSapSparePart>();
        pageParams.PageSize = -100;
        PageResult<EqmSapSparePart> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "单位信息");
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
        hidden_receive_user.Text = "";
        hidden_sparepart_code.Text = "";
        add_receive_date.Text = DateTime.Now.ToString();
        add_sparepart_code.Text = "";
        add_model.Text = "";
        add_number.Text = "0";
        add_receive_user.Text = "";
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
        EqmSapSparePart sap = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = sap.ObjID.ToString();
        modify_receive_no.Text = sap.ReceiveNo.ToString();
        modify_receive_date.Text = sap.ReceiveDate.ToString();
        try
        {
            modify_sparepart_code.Text = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == sap.SparePartCode)[0].SparePartName;
            hidden_sparepart_code.Text = sap.SparePartCode;
        }
        catch (Exception)
        {
        }
        modify_model.Text = sap.SparePartModel.ToString();
        modify_number.Text = sap.StoreInNum.ToString();
        try
        {
            modify_receive_user.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == sap.ReceiveUser)[0].UserName;
            hidden_receive_user.Text = sap.ReceiveUser;
        }
        catch (Exception)
        {
        }
        modify_remark.Text = sap.Remark;
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
            EqmSapSparePart sap = manager.GetById(objID);
            sap.DeleteFlag = "1";

            string flag = storeManager.UpdateSparePartStore(false, sap.SparePartCode, sap.SparePartModel, Convert.ToDecimal(sap.StoreInNum));
            if (flag.IndexOf("ERROR") > -1)
            {
                return flag;
            }
            manager.Update(sap);
            this.AppendWebLog("备件入库记录删除", "编号：" + sap.ReceiveNo);
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
            EqmSapSparePart sapSparePart = new EqmSapSparePart();
            sapSparePart.ReceiveDate = Convert.ToDateTime(add_receive_date.Text);
            sapSparePart.ReceiveNo = manager.GetNextSparePartStoreInCode(Convert.ToDateTime(add_receive_date.Text));
            sapSparePart.SparePartCode = hidden_sparepart_code.Text;
            sapSparePart.SparePartModel = add_model.Text;
            sapSparePart.StoreInNum = Convert.ToDecimal(add_number.Text == "" ? "1" : add_number.Text);
            sapSparePart.ReceiveUser = hidden_receive_user.Text;
            sapSparePart.RecordDate = DateTime.Now;
            sapSparePart.Remark = (string)(add_remark.Text);
            sapSparePart.DeleteFlag = "0";
            string flag = storeManager.UpdateSparePartStore(true, sapSparePart.SparePartCode, sapSparePart.SparePartModel, Convert.ToDecimal(sapSparePart.StoreInNum));
            if (flag.IndexOf("ERROR") > -1)
            {
                msg.Notify("操作", flag).Show();
                return; 
            }
            manager.Insert(sapSparePart);
            this.AppendWebLog("备件入库记录增加", "编号：" + sapSparePart.ReceiveNo);
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
            EqmSapSparePart sapSparePart = manager.GetById(modify_obj_id.Text);
            sapSparePart.ReceiveNo =  modify_receive_no.Text;
            sapSparePart.ReceiveDate = Convert.ToDateTime(modify_receive_date.Text);
            sapSparePart.SparePartCode = hidden_sparepart_code.Text;
            sapSparePart.SparePartModel = modify_model.Text;
            decimal oldStoreInNum = Convert.ToDecimal(sapSparePart.StoreInNum);
            sapSparePart.StoreInNum = Convert.ToDecimal(modify_number.Text == "" ? "1" : modify_number.Text);
            sapSparePart.ReceiveUser = hidden_receive_user.Text;
            sapSparePart.RecordDate = DateTime.Now;
            sapSparePart.Remark = (string)(modify_remark.Text);
            sapSparePart.DeleteFlag = "0";

            string flag = storeManager.UpdateSparePartStore((sapSparePart.StoreInNum - oldStoreInNum) > 0, sapSparePart.SparePartCode, sapSparePart.SparePartModel, Math.Abs(Convert.ToDecimal(oldStoreInNum - sapSparePart.StoreInNum)));
            if (flag.IndexOf("ERROR") > -1)
            {
                msg.Notify("操作", flag).Show();
                return;
            }
            manager.Update(sapSparePart);
            this.AppendWebLog("备件入库记录修改", "编号：" + modify_obj_id.Text);
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