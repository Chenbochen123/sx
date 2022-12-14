using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Data.Components;
using Mesnac.Entity;
using Ext.Net;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_SupplierCustomerInfo_FactoryType : Mesnac.Web.UI.Page
{
    protected BasFactoryTypeManager manager = new BasFactoryTypeManager();//业务对象
    protected BasFactoryInfoManager facInfomanager = new BasFactoryInfoManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasFactoryType> entityList;

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
    private PageResult<BasFactoryType> GetPageResultData(PageResult<BasFactoryType> pageParams)
    {
        BasFactoryTypeManager.QueryParams queryParams = new BasFactoryTypeManager.QueryParams();
        queryParams.pageParams = pageParams;

        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.factoryTypeName = txt_factory_type_name.Text.TrimEnd().TrimStart();
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
        PageResult<BasFactoryType> pageParams = new PageResult<BasFactoryType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasFactoryType> lst = GetPageResultData(pageParams);
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
        add_factory_type_name.Text = "";
        hidden_fac_type_name.Text = "";
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
        BasFactoryType factoryType = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = factoryType.ObjID.ToString();
        modify_factory_type_name.Text = factoryType.FactoryTypeName;
        hidden_fac_type_name.Text = factoryType.FactoryTypeName;
        modify_remark.Text = factoryType.Remark;
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
            BasFactoryType factoryType = manager.GetById(obj_id);
            factoryType.DeleteFlag = "0";
            manager.Update(factoryType);
            this.AppendWebLog("厂商类别恢复", "厂商类别编号：" + obj_id);
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
            EntityArrayList<BasFactoryInfo> facInfoList = facInfomanager.GetListByWhere(BasFactoryInfo._.FacType == objID && BasFactoryInfo._.DeleteFlag == 0);
            if (facInfoList.Count > 0)
            {
                return "删除失败：此厂商类别已被使用，禁止删除！";
            }
            //BasFactoryType factoryType = manager.GetById(objID);
            //factoryType.DeleteFlag = "1";
            //manager.Update(factoryType);
            string sql = "delete JCZL_fackind where facs_code='"+ objID + "'";
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("厂商类别删除", "厂商类别编号：" + objID);
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
            EntityArrayList<BasFactoryType> facTypeList = manager.GetListByWhere(BasFactoryType._.FactoryTypeName == add_factory_type_name.Text.TrimStart().TrimEnd());
            if (facTypeList.Count > 0)
            {
                X.Msg.Alert("提示", "此厂商类别已被使用！").Show();
                return;
            }
            //BasFactoryType factoryType = new BasFactoryType();
            //factoryType.ObjID = Convert.ToInt32(manager.GetNextFactoryTypeCode());
            //factoryType.FactoryTypeName = (string)(add_factory_type_name.Text);
            //factoryType.Remark = (string)(add_remark.Text);
            //factoryType.DeleteFlag = "0";
            //manager.Insert(factoryType);
            string sql = "select MAX(facs_code)+ 1 from dbo.JCZL_fackind";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into JCZL_fackind values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + add_factory_type_name.Text + "','" + add_remark.Text + "')";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("厂商类别添加", "厂商类别编号：" + ds.Tables[0].Rows[0][0].ToString());
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
            EntityArrayList<BasFactoryType> facTypeList = manager.GetListByWhere(BasFactoryType._.FactoryTypeName == modify_factory_type_name.Text.TrimStart().TrimEnd());
            if (facTypeList.Count > 0)
            {
                if (facTypeList[0].FactoryTypeName != hidden_fac_type_name.Text)
                {
                    X.Msg.Alert("提示", "此厂商类别已被使用！").Show();
                    return;
                }
            }
            BasFactoryType factoryType = manager.GetById(modify_obj_id.Text);
            factoryType.FactoryTypeName = (string)(modify_factory_type_name.Text);
            factoryType.Remark = (string)(modify_remark.Text);
            manager.Update(factoryType);
            this.AppendWebLog("厂商类别修改", "厂商类别编号：" + factoryType.ObjID);
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
    /// 检查厂商名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckFactoryType(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string factypename = field.Text;
        EntityArrayList<BasFactoryType> factypeList = manager.GetListByWhere(BasFactoryType._.FactoryTypeName == factypename);
        if (factypeList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (factypeList[0].FactoryTypeName.ToString() == hidden_fac_type_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此厂商类别名称已被使用！";
            }
        }
    }
    #endregion
}