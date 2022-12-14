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

public partial class Manager_BasicInfo_MainHanderInfo_MainHanderInfo : Mesnac.Web.UI.Page
{
    protected BasMainHanderManager manager = new BasMainHanderManager();//业务对象
    protected BasUserManager userManager = new BasUserManager();
    protected BasWorkShopManager workshopManager = new BasWorkShopManager();
    protected PptClassManager classManager = new PptClassManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
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
        EntityArrayList<PptClass> classList = classManager.GetAllList();
        foreach (PptClass pptClass in classList)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(pptClass.ClassName, pptClass.ObjID.ToString());
            add_class_id.Items.Add(item);
            modify_class_id.Items.Add(item);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMainHander> GetPageResultData(PageResult<BasMainHander> pageParams)
    {
        BasMainHanderManager.QueryParams queryParams = new BasMainHanderManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.mainHanderCode = txt_main_hander_code.Text.TrimEnd().TrimStart();
        queryParams.userName = txt_user_name.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        if (!Regex.IsMatch(txt_main_hander_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_main_hander_code.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMainHander> pageParams = new PageResult<BasMainHander>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<BasMainHander> lst = GetPageResultData(pageParams);
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

        if (!Regex.IsMatch(txt_main_hander_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_main_hander_code.Text = "";
        }
        PageResult<BasMainHander> pageParams = new PageResult<BasMainHander>();
        pageParams.PageSize = -100;
        PageResult<BasMainHander> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "主机手信息");
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        BasMainHander mainHander = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = mainHander.ObjID.ToString();
        modify_main_hander.Text = mainHander.MainHanderCode.ToString();
        EntityArrayList<BasUser> userList = userManager.GetListByWhere(BasUser._.HRCode == mainHander.UserCode);
        if (userList.Count != 0)
        {
            modify_user_code.Text = userList[0].UserName;
            hidden_user_code.Text = mainHander.UserCode;
        }
        else
        {
            modify_user_code.Text = "";
            hidden_user_code.Text = "";
        }
        EntityArrayList<BasWorkShop> workshopList = workshopManager.GetListByWhere(BasWorkShop._.ObjID == mainHander.WorkShopCode);
        if (workshopList.Count != 0)
        {
            modify_workshop_code.Text = workshopList[0].WorkShopName;
            hidden_workshop_code.Text = mainHander.WorkShopCode.ToString();
        }
        else
        {
            modify_workshop_code.Text = "";
            hidden_workshop_code.Text = "";
        }
        modify_remark.Text = mainHander.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        add_user_code.Text = string.Empty;
        hidden_user_code.Text = string.Empty;
        add_workshop_code.Text = string.Empty;
        hidden_workshop_code.Text = string.Empty;
        this.winAdd.Close();
        this.winModify.Close();
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
            DataSet ds = manager.IshaveUserInfo(string.Empty, hidden_user_code.Text, modify_obj_id.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                msg.Alert("操作", "修改失败，该人员已经是" + ds.Tables[0].Rows[0][0].ToString() + "主机手了！");
                msg.Show();
                return;
            }
            BasMainHander mainHander = manager.GetById(modify_obj_id.Text);
            mainHander.MainHanderCode = (string)(modify_main_hander.Text);
            mainHander.UserCode = (string)(hidden_user_code.Text);
            mainHander.WorkShopCode = Convert.ToInt32(hidden_workshop_code.Text);
            mainHander.ClassID = Convert.ToInt32(modify_class_id.Value.ToString());
            mainHander.Remark = (string)(modify_remark.Text);
            manager.Update(mainHander);
            this.AppendWebLog("主机手信息修改", "主机手编号：" + modify_main_hander.Text);
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

    /// <summary>
    /// 点击添加按钮事件 --2013-09-07 赵营
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.winAdd.Show();
    }

    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(add_main_hander.Text))
        {
            msg.Alert("操作", "请输入主机手号！");
            msg.Show();
            return;
        }
        if (string.IsNullOrEmpty(hidden_user_code.Text))
        {
            msg.Alert("操作", "请选择人员！");
            msg.Show();
            return;
        } 
        if (string.IsNullOrEmpty(hidden_workshop_code.Text))
        {
            msg.Alert("操作", "请选择所属车间！");
            msg.Show();
            return;
        }
        try
        {
            DataSet ds = manager.IshaveUserInfo(add_main_hander.Text, hidden_user_code.Text, string.Empty);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    msg.Alert("操作", "添加失败，已存在" + add_main_hander.Text + "主机手了！");
                    msg.Show();
                    return;
                }
                else
                {
                    msg.Alert("操作", "添加失败，该人员已经是" + ds.Tables[0].Rows[0][0].ToString() + "主机手了！");
                    msg.Show();
                    return;
                }
            }
            BasMainHander mainHander = new BasMainHander();
            mainHander.MainHanderCode = add_main_hander.Text;
            mainHander.UserCode = hidden_user_code.Text;
            mainHander.WorkShopCode = Convert.ToInt32(hidden_workshop_code.Text);
            mainHander.ClassID = Convert.ToInt32(add_class_id.Value.ToString());
            mainHander.Remark = txtRemark1.Text;
            mainHander.DeleteFlag = "0";

            manager.Insert(mainHander);
            this.pageToolBar.DoRefresh();
            this.winAdd.Close();
            add_main_hander.Text = string.Empty;
            add_user_code.Text = string.Empty;
            txtRemark1.Text = string.Empty;
            hidden_user_code.Text = string.Empty;

            msg.Notify("操作", "添加成功！");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "添加失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    #endregion
}