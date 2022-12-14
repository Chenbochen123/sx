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

public partial class Manager_Technology_BasicInfo_NonAuditMaterial : Mesnac.Web.UI.Page
{
    protected IPmtNonAuditMaterialManager manager = new PmtNonAuditMaterialManager();//业务对象
    protected IBasMaterialManager materManager = new BasMaterialManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasUnit> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
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
    private PageResult<PmtNonAuditMaterial> GetPageResultData(PageResult<PmtNonAuditMaterial> pageParams)
    {
        PmtNonAuditMaterialManager.QueryParams queryParams = new PmtNonAuditMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.materialCode = hidden_material_code.Value.ToString();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return "";
        //}
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtNonAuditMaterial> pageParams = new PageResult<PmtNonAuditMaterial>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<PmtNonAuditMaterial> lst = GetPageResultData(pageParams);
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

        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        PageResult<PmtNonAuditMaterial> pageParams = new PageResult<PmtNonAuditMaterial>();
        pageParams.PageSize = -100;
        PageResult<PmtNonAuditMaterial> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "免审核物料信息");
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
        add_material_name.Text = "";
        hidden_material_code.Text = "";
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
        PmtNonAuditMaterial mater = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = mater.ObjId.ToString();
        modify_material_name.Text = mater.MaterialName;
        hidden_material_code.Text = mater.MaterialCode.ToString();
        hidden_material_name.Text = mater.MaterialName;
        modify_remark.Text = mater.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            PmtNonAuditMaterial mater = manager.GetById(obj_id);
            mater.DeleteFlag = "0";
            manager.Update(mater);
            this.AppendWebLog("免审核物料信息恢复", "免审核物料编号：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
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
            PmtNonAuditMaterial mater = manager.GetById(objID);
            mater.DeleteFlag = "1";
            manager.Update(mater);
            this.AppendWebLog("免审核物料信息删除", "免审核物料编号：" + objID);
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
            EntityArrayList<PmtNonAuditMaterial> materList = manager.GetListByWhere(PmtNonAuditMaterial._.MaterialCode == hidden_material_code.Value);
            if (materList.Count > 0)
            {
                X.Msg.Alert("提示", "已含此免审核物料，请勿重复添加！").Show();
                return;
            }

            PmtNonAuditMaterial mater = new PmtNonAuditMaterial();
            try
            {
                mater.MaterialName = materManager.GetListByWhere(BasMaterial._.MaterialCode == hidden_material_code.Value)[0].MaterialName;
            }
            catch (Exception)
            {
            }
            mater.MaterialCode = hidden_material_code.Value.ToString();
            mater.Remark = (string)(add_remark.Text);
            mater.DeleteFlag = "0";
            manager.Insert(mater);
            this.AppendWebLog("免审核物料增加", "免审核物料编号：" + mater.MaterialCode);
            hidden_material_code.Value = "";
            hidden_material_name.Value = "";
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
            EntityArrayList<PmtNonAuditMaterial> materList = manager.GetListByWhere(PmtNonAuditMaterial._.MaterialName == modify_material_name.Text.TrimStart().TrimEnd());
            if (materList.Count > 0)
            {
                if (materList[0].MaterialName != hidden_material_name.Text)
                {
                    X.Msg.Alert("提示", "此免审核物料已存在！").Show();
                    return;
                }
            }
            PmtNonAuditMaterial mater = manager.GetById(modify_obj_id.Text);
            mater.MaterialCode = (string)(hidden_material_code.Text);
            try
            {
                mater.MaterialName = materManager.GetListByWhere(BasMaterial._.MaterialCode == hidden_material_code.Text)[0].MaterialName;
            }
            catch (Exception)
            {
            }
            mater.Remark = (string)(modify_remark.Text);
            manager.Update(mater);
            this.AppendWebLog("免审核物料信息修改", "免审核物料编号：" + modify_obj_id.Text);
            hidden_material_code.Value = "";
            hidden_material_name.Value = "";
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
}