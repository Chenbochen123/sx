using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using NBear.Common;
using System;
using System.Collections.Generic;
using System.Data;


/// <summary>
/// Manager_System_UserRole_RoleManager 实现类
/// 孙本强 @ 2013-04-03 13:09:14
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_UserRole_RoleManager : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btnAdd" };
            查询 = new SysPageAction() { ActionID = 2, ActionName = "btnSearch" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 4, ActionName = "Delete" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
    }
    #endregion
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:14
    /// </summary>
    private ISysRoleManager manager = new SysRoleManager();//业务对象
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:14
    /// </summary>
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:14
    /// </summary>
    private EntityArrayList<SysRole> entityList;

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:14
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
        }
    }
    #endregion

    #region 分页查询获取数据
    /// <summary>
    /// 分页查询获取数据
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:15
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<SysRole> GetPageResultData(PageResult<SysRole> pageParams)
    {
        SysRoleManager.QueryParams queryParams = new SysRoleManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.RoleName = txtShowName.Text;
        queryParams.Remark = txtRemark.Text;
        queryParams.DeleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }
    /// <summary>
    /// 分页查询获取数据 绑定 Grid
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:15
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx==0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysRole> pageParams = new PageResult<SysRole>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<SysRole> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:15
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (this._.添加.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行添加的权限！").Show();
            return;
        }
        addShowName.Text = "";
        addRemark.Text = "";
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:15
    /// </summary>
    /// <param name="obj_id">The obj_id.</param>
    /// <remarks></remarks>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        //X.Msg.Alert("提示", obj_id).Show();
        //return;
        if (this._.修改.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行修改的权限！").Show();
            return;
        }
        SysRole role = manager.GetListByWhere(SysRole._.ObjID == obj_id)[0];
        modifyObjID.Value = role.ObjID;
        modifyShowName.Value = role.RoleName;
        modifyRemark.Value = role.Remark;
        this.winModify.Show();
    }


    /// <summary>
    /// 点击删除激发的事件
    /// 孙本强 @ 2013-04-02 19:27:00
    /// </summary>
    /// <param name="obj_id">The obj_id.</param>
    /// <remarks></remarks>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_delete(string obj_id)
    {
        if (this._.删除.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行删除的权限！").Show();
            return;
        }
        (new SysUserRoleManager()).DeleteByWhere(SysUserRole._.RoleID == obj_id);
        (new SysDeptRoleManager()).DeleteByWhere(SysDeptRole._.RoleID == obj_id);
        (new SysRoleActionManager()).DeleteByWhere(SysRoleAction._.RoleID == obj_id);
        manager.DeleteByWhere(SysRole._.ObjID == obj_id);
        msg.Alert("提示", "删除成功").Show();
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:15
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:16
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        if (this._.添加.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行添加的权限！").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(addShowName.Text))
        {
            msg.Alert("操作", "请填写角色名称").Show();
            return;
        }
        try
        {
            //SysRole role = new SysRole();
            //role.RoleName = addShowName.Text;
            //role.Remark = addRemark.Text;
            //role.DeleteFlag = "0";
            //manager.Insert(role);

            string sql = "select MAX(role_id)+1 from SYS_ROLE";
            DataSet ds= manager.GetBySql(sql).ToDataSet();
            string id = ds.Tables[0].Rows[0][0].ToString();

            sql = "insert into SYS_ROLE values('" + id + "','" + addShowName.Text + "','" + addRemark.Text + "')";
            manager.GetBySql(sql).ToDataSet();
            pageToolbar.DoRefresh();
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
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:16
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        if (this._.修改.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行修改的权限！").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(modifyShowName.Text))
        {
            msg.Alert("操作", "请填写角色名称").Show();
            return;
        }
        try
        {
            //SysRole role = new SysRole();
            //role.ObjID = Convert.ToInt32(modifyObjID.Text);
            //role.Attach();
            //role.RoleName = modifyShowName.Text;
            //role.Remark = modifyRemark.Text;
            //manager.Update(role);

            string sql = "update SYS_ROLE set Role_Name = '" + modifyShowName.Text + "' , Role_Name_EN = '" + modifyRemark.Text + "' where Role_ID = '" + Convert.ToInt32(modifyObjID.Text) + "' ";
            manager.GetBySql(sql).ToDataSet();




            pageToolbar.DoRefresh();
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

}