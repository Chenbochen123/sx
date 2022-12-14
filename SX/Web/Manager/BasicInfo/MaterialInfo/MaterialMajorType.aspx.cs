using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_MaterialInfo_MaterialMajorType : Mesnac.Web.UI.Page
{
    protected BasMaterialMajorTypeManager manager = new BasMaterialMajorTypeManager();//业务对象
    protected BasMaterialMinorTypeManager minorManager = new BasMaterialMinorTypeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasMaterialMajorType> entityList;


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
    private PageResult<BasMaterialMajorType> GetPageResultData(PageResult<BasMaterialMajorType> pageParams)
    {
        BasMaterialMajorTypeManager.QueryParams queryParams = new BasMaterialMajorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.majorTypeName = txt_major_type_name.Text.TrimEnd().TrimStart();
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
        PageResult<BasMaterialMajorType> pageParams = new PageResult<BasMaterialMajorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialMajorType> lst = GetPageResultData(pageParams);
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
        add_major_type_name.Text = "";
        hidden_major_type_name.Text = "";
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
        BasMaterialMajorType materialMajorType = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = materialMajorType.ObjID.ToString();
        modify_major_type_name.Text = materialMajorType.MajorTypeName;
        hidden_major_type_name.Text = materialMajorType.MajorTypeName;
        modify_remark.Text = materialMajorType.Remark;
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
            BasMaterialMajorType materialMajorType = manager.GetById(obj_id);
            materialMajorType.DeleteFlag = "0";
            manager.Update(materialMajorType);
            this.AppendWebLog("物料大类恢复", "物料大类编码：" + materialMajorType.ObjID);
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
            //EntityArrayList<BasMaterialMinorType> minorList =
            //    minorManager.GetListByWhere(BasMaterialMinorType._.MajorID == objID && BasMaterialMinorType._.DeleteFlag == "0");
            //if (minorList.Count > 0)
            //{
            //    return "删除失败：此物料大类已被使用，禁止删除！";
            //}
            //X.Msg.Notify(objID,"").Show();
           string sql = "delete Pmt_mkind where Mkind_code='" + objID + "'";
           DataSet ds = manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("物料大类删除", "物料大类编码：" + objID);
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
            EntityArrayList<BasMaterialMajorType> majorList = manager.GetListByWhere(BasMaterialMajorType._.MajorTypeName == add_major_type_name.Text.TrimStart().TrimEnd()&& BasMaterialMajorType._.DeleteFlag==0);
            if (majorList.Count > 0)
            {
                X.Msg.Alert("提示", "此物料大类名称已被使用！").Show();
                return;
            }

            string sql = "select MAX(Mkind_code)+1 from dbo.Pmt_mkind  ";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string ObjID = ds.Tables[0].Rows[0][0].ToString();

            sql = "insert into Pmt_mkind(Mkind_code,Mkind_name,del_flag, Mem_note)values('" + ObjID + "','" + add_major_type_name.Text + "','0','" + add_remark.Text + "')";
            ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("物料大类添加", "物料大类编码：" + ObjID);
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
            EntityArrayList<BasMaterialMajorType> majorList = manager.GetListByWhere(BasMaterialMajorType._.MajorTypeName == modify_major_type_name.Text.TrimStart().TrimEnd());
            if (majorList.Count > 0)
            {
                if (majorList[0].MajorTypeName != hidden_major_type_name.Text)
                {
                    X.Msg.Alert("提示", "此物料大类名称已被使用！").Show();
                    return;
                }
            }
            //BasMaterialMajorType materialMajorType = manager.GetById(modify_obj_id.Text);
            //materialMajorType.MajorTypeName = (string)(modify_major_type_name.Text);
            //materialMajorType.Remark = (string)(modify_remark.Text);
            //manager.Update(materialMajorType);

            string sql = "update Pmt_mkind set Mkind_name='" + modify_major_type_name.Text + "', Mem_note ='" + modify_remark.Text+ "' where Mkind_code='" + modify_obj_id.Text + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();


            this.AppendWebLog("物料大类修改", "物料大类编码：" + modify_obj_id.Text);
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
    #endregion
}