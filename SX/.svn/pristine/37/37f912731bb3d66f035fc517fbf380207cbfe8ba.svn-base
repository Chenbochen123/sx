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
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using Mesnac.Business.Interface;

public partial class Manager_System_UserAction_MesManager : System.Web.UI.Page
{
    protected SysMesActionManager manager = new SysMesActionManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasWork> entityList;

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            删除 = new SysPageAction() { ActionID = 2, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            查询 = new SysPageAction() { ActionID = 4, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 5, ActionName = "btn_history_search" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            导出 = new SysPageAction() { ActionID = 7, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
            String sql = "  select * from PptShift";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            //Ext.Net.ListItem allitem = new Ext.Net.ListItem("全部", "全部");
            txtshift.Items.Clear();
            //txtshift.Items.Add(allitem);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(dr[1].ToString(), dr[1].ToString());
                txtshift.Items.Add(item);
            }
            if (txtshift.Items.Count > 0)
            {
                txtshift.Text = (txtshift.Items[0].Value);
            }
        }
    }
    #endregion

    #region 分页相关方法
    //private PageResult<BasWork> GetPageResultData(PageResult<BasWork> pageParams)
    //{
    //    BasWorkManager.QueryParams queryParams = new BasWorkManager.QueryParams();
    //    queryParams.pageParams = pageParams;

    //    queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
    //    queryParams.workName = txt_work_name.Text.TrimEnd().TrimStart();
    //    queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
    //    queryParams.deleteFlag = hidden_delete_flag.Text;
    //   return manager.GetTablePageDataBySql(queryParams);
    //}

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        


        String sql = "  select * from SysMesAction";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        DataTable data = ds.Tables[0];

        int total = data.Rows.Count;
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
        add_work_name.Text = "";
        hidden_work_name.Value = "";
        add_hr_code.Text = "";
        //add_erp_code.Text = "";
        //add_Remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
        return;
        

    }

 
 
 

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            SysMesAction work = manager.GetById(Convert.ToInt32(obj_id));
          
            manager.Delete(work);
          
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

            SysMesAction work = new SysMesAction();
         String sql = " Select MAX(ObjID) + 1 as ObjID From SysMesAction ";



         string temp = manager.GetBySql(sql).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            work.ObjID = Convert.ToInt32(temp);
            work.WorkName = (string)(add_work_name.Text);
            //work.Remark = (string)(add_Remark.Text);
            //work.ERPCode = add_erp_code.Text;
            work.PhoneNum = add_hr_code.Text;
            work.ShiftName = txtshift.Text;
            manager.Insert(work);
       
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


    #endregion

    #region 校验方法
    protected void CheckWorkName(object sender, RemoteValidationEventArgs e)
    {
       
    }
    #endregion
}