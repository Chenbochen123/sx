using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using NBear.Common;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_RubberInfo_RubberTyrePart : Mesnac.Web.UI.Page
{
    protected BasRubTyrePartManager manager = new BasRubTyrePartManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasRubTyrePart> entityList;

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
    private PageResult<BasRubTyrePart> GetPageResultData(PageResult<BasRubTyrePart> pageParams)
    {
        BasRubTyrePartManager.QueryParams queryParams = new BasRubTyrePartManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.tyrePartName = txt_tyre_part_name.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$") || txt_obj_id.Text.TrimEnd().TrimStart() == "")
        {
            txt_obj_id.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasRubTyrePart> pageParams = new PageResult<BasRubTyrePart>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasRubTyrePart> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
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
        add_tyre_part_name.Text = "";
        hidden_tyre_part_name.Text = "";
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
        BasRubTyrePart rubTyrePart = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = rubTyrePart.ObjID.ToString();
        modify_tyre_part_name.Text = rubTyrePart.TyrePartName;
        hidden_tyre_part_name.Text = rubTyrePart.TyrePartName;
        modify_remark.Text = rubTyrePart.Remark;
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
            BasRubTyrePart rubTyrePart = manager.GetById(obj_id);
            rubTyrePart.DeleteFlag = "0";
            manager.Update(rubTyrePart);
            this.AppendWebLog("轮胎部件恢复", "轮胎部件编号：" + rubTyrePart.ObjID);
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
            BasRubTyrePart rubTyrePart = manager.GetById(objID);
            rubTyrePart.DeleteFlag = "1";
            manager.Update(rubTyrePart);
            this.AppendWebLog("轮胎部件删除", "轮胎部件编号：" + rubTyrePart.ObjID);
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
            //添加校验重复
            EntityArrayList<BasRubTyrePart> tyrePartList = manager.GetListByWhere(BasRubTyrePart._.TyrePartName == add_tyre_part_name.Text.TrimStart().TrimEnd());
            if (tyrePartList.Count > 0)
            {
                X.Msg.Alert("提示", "此轮胎部件名称已被使用！").Show();
                return;
            }
            BasRubTyrePart rubTyrePart = new BasRubTyrePart();
            rubTyrePart.ObjID = Convert.ToInt32(manager.GetNextTyrePartCode());
            rubTyrePart.TyrePartName = (string)(add_tyre_part_name.Text);
            rubTyrePart.Remark = (string)(add_remark.Text);
            rubTyrePart.DeleteFlag = "0";
            manager.Insert(rubTyrePart);
            this.AppendWebLog("轮胎部件添加", "轮胎部件编号：" + rubTyrePart.ObjID);
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
            //修改重复校验
            EntityArrayList<BasRubTyrePart> tyrePartList = manager.GetListByWhere(BasRubTyrePart._.TyrePartName == modify_tyre_part_name.Text.TrimStart().TrimEnd());
            if (tyrePartList.Count > 0)
            {
                if (tyrePartList[0].TyrePartName != hidden_tyre_part_name.Text)
                {
                    X.Msg.Alert("提示", "此轮胎部件名称已被使用！").Show();
                    return;
                }
            }
            BasRubTyrePart rubTyrePart = manager.GetById(modify_obj_id.Text);
            rubTyrePart.TyrePartName = (string)(modify_tyre_part_name.Text);
            rubTyrePart.Remark = (string)(modify_remark.Text);
            manager.Update(rubTyrePart);
            this.AppendWebLog("轮胎部件修改", "轮胎部件编号：" + rubTyrePart.ObjID);
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
    /// 检查轮胎部件名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckTyrePart(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string rubtypename = field.Text;
        EntityArrayList<BasRubTyrePart> unitList = manager.GetListByWhere(BasRubTyrePart._.TyrePartName == rubtypename);
        if (unitList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (unitList[0].TyrePartName.ToString() == hidden_tyre_part_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此轮胎部件名称已被使用！";
            }
        }
    }
    #endregion
}