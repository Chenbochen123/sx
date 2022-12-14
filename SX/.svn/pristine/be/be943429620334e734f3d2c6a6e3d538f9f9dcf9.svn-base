using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_MaterialInfo_MaterialMinorType : Mesnac.Web.UI.Page
{
    protected BasMaterialMinorTypeManager manager = new BasMaterialMinorTypeManager();//业务对象
    protected BasMaterialMajorTypeManager majorManager = new BasMaterialMajorTypeManager();
    protected BasMaterialManager materialManager = new BasMaterialManager();

    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框


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
            InitTreeDept();
        }
    }

    /// <summary>
    /// 初始化部门列表树
    /// </summary>
    private void InitTreeDept()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        EntityArrayList<BasMaterialMajorType> majorList = majorManager.GetListByWhere(BasMaterialMajorType._.DeleteFlag == 0);
        treeDept.GetRootNode().RemoveAll();
        foreach (BasMaterialMajorType majorType in majorList)
        {
            Node node = new Node();
            node.NodeID = majorType.ObjID.ToString();
            node.Text = majorType.MajorTypeName;
            node.Leaf = true;
            node.Icon = Icon.Brick;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterialMinorType> GetPageResultData(PageResult<BasMaterialMinorType> pageParams)
    {
        BasMaterialMinorTypeManager.QueryParams queryParams = new BasMaterialMinorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.minorTypeID = txt_minor_type_id.Text.TrimEnd().TrimStart();
        queryParams.minorTypeName = txt_minor_type_name.Text.TrimEnd().TrimStart();
        queryParams.majorID = hidden_select_major_id.Text;
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_minor_type_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_minor_type_id.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialMinorType> pageParams = new PageResult<BasMaterialMinorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialMinorType> lst = GetPageResultData(pageParams);
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
        if (hidden_major_id.Text == "")
        {
            X.Msg.Alert("提示", "请先选择左侧大类类别！").Show();
            return;
        }
        add_minor_type_name.Text = "";
        hidden_minor_type_name.Text = "";
        add_remark.Text = "";
        add_maxstore.Text = "";
        add_minstore.Text = "";
        add_major_id.Text = majorManager.GetById(hidden_major_id.Text).MajorTypeName;
        btnAddSave.Disable(true);
        add_minstore.Text = "0";
        add_maxstore.Text = "1";
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
        BasMaterialMinorType materialMinorType = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = materialMinorType.ObjID.ToString();
        modify_minor_type_id.Text = materialMinorType.MinorTypeID;
        modify_minor_type_name.Text = materialMinorType.MinorTypeName;
        modify_maxstore.Text = materialMinorType.MaxStore.ToString();
        modify_minstore.Text = materialMinorType.MinStore.ToString();
        hidden_minor_type_name.Text = materialMinorType.MinorTypeName;
        modify_major_id.Text = majorManager.GetById(materialMinorType.MajorID).MajorTypeName;
        hidden_major_id.Text = materialMinorType.MajorID.ToString();
        modify_remark.Text = materialMinorType.Remark;
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
            BasMaterialMinorType materialMinorType = manager.GetById(obj_id);
            BasMaterialMajorType materialMajorType = majorManager.GetById(materialMinorType.MajorID);
            if (materialMajorType != null && materialMajorType.DeleteFlag == "1")
            {
                return "恢复失败：请先恢复物料大类[" + materialMajorType.MajorTypeName + "]";
            }
            materialMinorType.DeleteFlag = "0";
            manager.Update(materialMinorType);
            this.AppendWebLog("物料细类恢复", "物料细类编码：" + materialMinorType.MinorTypeID);
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
            //EntityArrayList<BasMaterial> materialList =
            //   materialManager.GetListByWhere(BasMaterial._.MinorTypeID == objID.PadLeft(2, '0') &&
            //                               BasMaterial._.DeleteFlag == "0");
            //if (materialList.Count > 0)
            //{
            //    return "删除失败：此物料细类已被使用，禁止删除！";
            //}
            X.Msg.Notify("", objID).Show();
            return "";
            BasMaterialMinorType materialMinorType = manager.GetById(objID);
            materialMinorType.DeleteFlag = "1";
            manager.Update(materialMinorType);
            this.AppendWebLog("物料细类删除", "物料细类编码：" + materialMinorType.MinorTypeID);
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
            //if (!Regex.IsMatch(add_minstore.Text.TrimEnd().TrimStart(), "^[0-9]+(\\.[0-9]+)?$"))
            //{
            //    add_minstore.Text = "";
            //    add_minstore.Focus();
            //    X.Msg.Alert("提示", "最小库存错误！").Show();
            //    return;
            //}
            //if (!Regex.IsMatch(add_maxstore.Text.TrimEnd().TrimStart(), "^[0-9]+(\\.[0-9]+)?$"))
            //{
            //    add_minstore.Text = "";
            //    add_minstore.Focus();
            //    X.Msg.Alert("提示", "最库存错误！").Show();
            //    return;
            //}

            //添加校验重复同一大类下的细类名称禁止重复
            EntityArrayList<BasMaterialMinorType> minroTypeList = manager.GetListByWhere(BasMaterialMinorType._.MinorTypeName == add_minor_type_name.Text.TrimStart().TrimEnd() && BasMaterialMinorType._.MajorID == hidden_major_id.Text);
            if (minroTypeList.Count > 0)
            {
                X.Msg.Alert("提示", "此物料细类名称已被使用！").Show();
                return;
            }
  
            
            string sql = "Select MAX(MinorTypeID) + 1 as MinorTypeID From BasMaterialMinorType WHERE MajorID = '" + hidden_major_id.Text + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string temp = ds.Tables[0].Rows[0][0].ToString();

            sql = "insert into Pmt_ikind values('" + hidden_major_id.Text + "','" + temp + "','" + add_minor_type_name.Text + "','" + add_remark.Text + "')";
            ds = manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("物料细类添加", "物料细类编码：" + temp);
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
            if (!Regex.IsMatch(modify_minstore.Text.TrimEnd().TrimStart(), "^[0-9]+(\\.[0-9]+)?$"))
            {
                add_minstore.Text = "";
                add_minstore.Focus();
                X.Msg.Alert("提示", "最小库存错误！").Show();
                return;
            }
            if (!Regex.IsMatch(modify_maxstore.Text.TrimEnd().TrimStart(), "^[0-9]+(\\.[0-9]+)?$"))
            {
                add_minstore.Text = "";
                add_minstore.Focus();
                X.Msg.Alert("提示", "最大库存错误！").Show();
                return;
            }
            //修改重复校验同一大类下的细类名称禁止重复
            EntityArrayList<BasMaterialMinorType> minorTypeList = manager.GetListByWhere(BasMaterialMinorType._.MinorTypeName == modify_minor_type_name.Text.TrimStart().TrimEnd() && BasMaterialMinorType._.MajorID == hidden_major_id.Text);
            if (minorTypeList.Count > 0)
            {
                if (minorTypeList[0].MinorTypeName != hidden_minor_type_name.Text)
                {
                    X.Msg.Alert("提示", "此物料细类名称已被使用！").Show();
                    return;
                }
            }
            decimal max = Convert.ToDecimal(modify_maxstore.Text);
            decimal min = Convert.ToDecimal(modify_minstore.Text);
            if (min >= max)
            {
                X.Msg.Alert("提示", "最小库存必须大于最大库存！").Show();
                return;
            }
            BasMaterialMinorType materialMinorType = manager.GetById(modify_obj_id.Text);
            materialMinorType.MinorTypeName = (string)(modify_minor_type_name.Text);
            materialMinorType.MinorTypeID = modify_minor_type_id.Text;
            materialMinorType.MajorID = Convert.ToInt32(hidden_major_id.Text);
            materialMinorType.Remark = (string)(modify_remark.Text);
            materialMinorType.MinStore = min;
            materialMinorType.MaxStore = max;
            manager.Update(materialMinorType);

            materialManager.Update(new PropertyItem[] { BasMaterial._.MinStock, BasMaterial._.MaxStock }, new object[] { min, max }, BasMaterial._.MinorTypeID == materialMinorType.MinorTypeID && ((BasMaterial._.MinStock == 0 && BasMaterial._.MaxStock == 0) || (BasMaterial._.MinStock == null && BasMaterial._.MaxStock == null)));

            this.AppendWebLog("物料细类修改", "物料细类编码：" + materialMinorType.MinorTypeID);
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