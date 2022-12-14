using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Text.RegularExpressions;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_EquipmentInfo_MixType : Mesnac.Web.UI.Page
{
    protected PmtMixTypeManager manager = new PmtMixTypeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<PmtMixType> entityList;



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
    private PageResult<PmtMixType> GetPageResultData(PageResult<PmtMixType> pageParams)
    {
        PmtMixTypeManager.QueryParams queryParams = new PmtMixTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.mixName = txt_mix_name.Text.TrimEnd().TrimStart();
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
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtMixType> pageParams = new PageResult<PmtMixType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<PmtMixType> lst = GetPageResultData(pageParams);
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
        PageResult<PmtMixType> pageParams = new PageResult<PmtMixType>();
        pageParams.PageSize = -100;
        PageResult<PmtMixType> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "密炼类型信息");
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
        add_mix_name.Text = "";
        hidden_mix_name.Text = "";
        add_mix_cubage.Value = 0;
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
        PmtMixType mix = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = mix.ObjID.ToString();
        modify_mix_name.Text = mix.MixName;
        hidden_mix_name.Text = mix.MixName;
        modify_mix_cubage.Value = mix.MixCubage;
        modify_remark.Text = mix.Remark;
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
            PmtMixType mix = manager.GetById(obj_id);
            mix.DeleteFlag = "0";
            manager.Update(mix);
            this.AppendWebLog("密炼类型信息恢复", "密炼类型编号：" + obj_id);
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
            PmtMixType mix = manager.GetById(objID);
            mix.DeleteFlag = "1";
            manager.Update(mix);
            this.AppendWebLog("密炼类型信息删除", "密炼类型编号：" + objID);
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
            EntityArrayList<PmtMixType> mixList = manager.GetListByWhere(PmtMixType._.MixName == add_mix_name.Text.TrimStart().TrimEnd());
            if (mixList.Count > 0)
            {
                X.Msg.Alert("提示", "此密炼类型名称已被使用！").Show();
                return;
            }

            PmtMixType mix = new PmtMixType();
            mix.ObjID = manager.GetPmtMixTypeNextPrimaryKeyValue();
            mix.MixName = (string)(add_mix_name.Text);
            mix.MixCubage = Convert.ToDecimal(add_mix_cubage.Value);
            mix.Remark = (string)(add_remark.Text);
            mix.DeleteFlag = "0";
            manager.Insert(mix);
            this.AppendWebLog("密炼类型信息增加", "密炼类型编号：" + mix.ObjID);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
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
            EntityArrayList<PmtMixType> mixList = manager.GetListByWhere(PmtMixType._.MixName == modify_mix_name.Text.TrimStart().TrimEnd());
            if (mixList.Count > 0)
            {
                if (mixList[0].MixName != hidden_mix_name.Text)
                {
                    X.Msg.Alert("提示", "此密炼类型名称已被使用！").Show();
                    return;
                }
            }
            PmtMixType mix = manager.GetById(modify_obj_id.Text);
            mix.MixName = (string)(modify_mix_name.Text);
            mix.MixCubage = Convert.ToDecimal(modify_mix_cubage.Text);
            mix.Remark = (string)(modify_remark.Text);
            manager.Update(mix);
            this.AppendWebLog("密炼类型信息修改", "密炼类型编号：" + modify_obj_id.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    /// <summary>
    /// 检查密炼类型名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckMixName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string mixname = field.Text;
        EntityArrayList<PmtMixType> mixList = manager.GetListByWhere(PmtMixType._.MixName == mixname);
        if (mixList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (mixList[0].MixName.ToString() == hidden_mix_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此密炼类型名称已被使用！";
            }
        }
    }
    #endregion
}