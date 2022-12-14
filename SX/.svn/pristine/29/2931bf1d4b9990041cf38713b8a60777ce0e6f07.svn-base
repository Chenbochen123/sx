using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using NBear.Common;

using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_RubberQuality_BasicInfo_ItemClass : Mesnac.Web.UI.Page
{

    private QmtItemClassManager manager = new QmtItemClassManager();//业务对象
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<QmtItemClass> entityList;

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
    private PageResult<QmtItemClass> GetPageResultData(PageResult<QmtItemClass> pageParams)
    {
        QmtItemClassManager.QueryParams queryParams = new QmtItemClassManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.itemClass = txt_ItemClass.Text;
        queryParams.itemClassName = txt_ItemClassName.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<QmtItemClass> pageParams = new PageResult<QmtItemClass>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ItemClass ASC";

        PageResult<QmtItemClass> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// qusf   2013年2月20日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        add_item_class.Text = "";
        string[] classArray = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9"
            , "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"
            , "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        EntityArrayList<QmtItemClass> list = manager.GetAllList();
        string itemClass = "";
        foreach (string c in classArray)
        {
            itemClass = c;
            bool bl = false;
            foreach (QmtItemClass model in list)
            {
                if (model.ItemClass == itemClass)
                {
                    bl = true;
                    break;
                }
            }
            if (bl == false)
            {
                break;
            }
        }
        add_item_class.Text = itemClass;
        add_item_classname.Text = "";
        add_item_classname.Focus();
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// qusf   2013年2月20日
    /// </summary>
    /// <param name="objID"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        QmtItemClass model = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = model.ObjID.ToString();
        modify_item_class.Text = model.ItemClass;
        modify_item_classname.Text = model.ItemClassName;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// qusf   2013年2月20日
    /// </summary>
    /// <param name="objID"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            QmtItemClass model = manager.GetById(objID);
            manager.Delete(model);
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
    /// qusf   2013年2月20日
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
    /// qusf   2013年2月20日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            QmtItemClass model = new QmtItemClass();
            model.ObjID = manager.GetItemClassNextPrimaryKeyValue();
            model.ItemClass = (string)(add_item_class.Text);
            model.ItemClassName = (string)(add_item_classname.Text);
            manager.Insert(model);
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
    /// qusf   2013年2月20日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            QmtItemClass model = manager.GetById(modify_obj_id.Text);
            model.ItemClass = (string)(modify_item_class.Text);
            model.ItemClassName = (string)(modify_item_classname.Text);
            manager.Update(model);
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