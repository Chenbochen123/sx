using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;

public partial class Manager_RawMaterialQuality_Standard : BasePage
{

    #region 属性注入
    protected IBasUserManager userManager = new BasUserManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected IQmcCheckItemManager itemManager = new QmcCheckItemManager();
    protected IQmcCheckItemDetailManager detailManager = new QmcCheckItemDetailManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新建 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新建 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<QmcStandard> lst = standardManager.GetListByWhere(QmcStandard._.ActivateFlag == "1");
            if (lst.Count > 0)
            {
                txtCurrentStandard.Text = lst[0].StandardName;
            }
            else
            {
                txtCurrentStandard.Text = "无";
            }
        }
    }
    #endregion

    #region 分页相关方法
    /// <summary>
    /// 根据筛选条件获取分页数据
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<QmcStandard> GetPageResultData(PageResult<QmcStandard> pageParams)
    {
        QmcStandardManager.QueryParams queryParams = new QmcStandardManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.deleteFlag = "0";
        return standardManager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// GridPanel数据绑定
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<QmcStandard> pageParams = new PageResult<QmcStandard>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ActivateFlag ASC";

        PageResult<QmcStandard> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 点击增删改按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //初始化添加窗口
        txtAddStandardId.Value = "";
        txtAddCreatorId.Value = this.UserID;
        txtAddStandardName.Value = "";
        txtAddRemark.Value = "";
        dtfAddActivateDate.Value = DateTime.Now.Date;
        btnAddStandardSave.Disable();
        this.windowAddStandard.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string standardId)
    {
        try
        {
            QmcStandard standard = standardManager.GetById(standardId);
            if (standard.ActivateFlag == "1")
            {
                return "删除失败：不能删除当前正在使用的执行标准！";
            }
            if (standard.ActivateFlag == "2")
            {
                return "删除失败：不能删除已过期的执行标准！";
            }
            standard.DeleteFlag = "1";
            standardManager.Update(standard);
            this.AppendWebLog("执行标准删除", "执行标准编号：" + standardId);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string standardId)
    {
        QmcStandard standard = standardManager.GetById(standardId);
        if (standard.ActivateFlag == "1")
        {
            msg.Alert("错误", "修改失败：不能修改当前生效的执行标准！");
            msg.Show();
            return;
        }
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("错误", "修改失败：不能修改已过期的执行标准！");
            msg.Show();
            return;
        }
        //初始化修改窗口
        txtModifyStandardId.Value = standard.StandardId;
        txtModifyCreatorId.Value = standard.CreatorId;
        txtModifyStandardName.Value = standard.StandardName;
        txtModifyRemark.Value = standard.Remark;
        dtfModifyActivateDate.Value = standard.ActivateDate;
        this.windowModifyStandard.Show();
    }

    /// <summary>
    /// 点击添加项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddStandardSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            QmcStandard current = new QmcStandard();
            //添加校验重复
            EntityArrayList<QmcStandard> lst = standardManager.GetListByWhere((QmcStandard._.StandardName == txtAddStandardName.Value.ToString()) && (QmcStandard._.DeleteFlag == "0"));
            if (lst.Count > 0)
            {
                X.Msg.Alert("提示", "标准名称已被使用！").Show();
                return;
            }
            EntityArrayList<QmcStandard> lst0 = standardManager.GetListByWhere(QmcStandard._.ActivateFlag == "1");
            if (lst0.Count > 0)
            {
                current = lst0[0];
                if (current.ActivateDate >= Convert.ToDateTime(dtfAddActivateDate.Value).Date)
                {
                    X.Msg.Alert("提示", "新建的执行标准生效日期必须晚于当前执行标准的生效日期！").Show();
                    return;
                }
            }
            if (DateTime.Now.Date >= Convert.ToDateTime(dtfAddActivateDate.Value).Date)
            {
                X.Msg.Alert("提示", "新建的执行标准最早要在次日生效！").Show();
                return;
            }
            //日期重复
            EntityArrayList<QmcStandard> lstAll = standardManager.GetListByWhere(QmcStandard._.DeleteFlag == "0");
            foreach (QmcStandard standardAll in lstAll)
            {
                if (standardAll.ActivateDate.Date == Convert.ToDateTime(dtfAddActivateDate.Value).Date)
                {
                    X.Msg.Alert("提示", "同一天不能有两个或两个以上的执行标准！").Show();
                    return;
                }
            }
            QmcStandard standard = new QmcStandard();
            standard.StandardId = Convert.ToInt32(standardManager.GetNextStandardId());
            standard.CreatorId = this.UserID;
            standard.StandardName = txtAddStandardName.Value.ToString();
            standard.ActivateDate = Convert.ToDateTime(dtfAddActivateDate.Value);
            standard.Remark = txtAddRemark.Text.ToString();
            standard.DeleteFlag = "0";
            standard.ActivateFlag = "0";
            standardManager.Insert(standard);
            if (cbxDoCopy.Checked)
            {
                String sql = "select MAX(ItemId) from QmcCheckItem ";
                DataSet ds = standardManager.GetBySql(sql).ToDataSet();
                String maxitemid = ds.Tables[0].Rows[0][0].ToString();
                sql =String.Format( @"insert into QmcCheckItem
(itemid,StandardId,SeriesId,ItemName,ItemCode,ValueType,Remark,DeleteFlag,CopyItemId)
 (
select RANK() OVER (ORDER BY itemid ) +{2} , '{1}',SeriesId,ItemName,ItemCode,ValueType,Remark,DeleteFlag,itemid from QmcCheckItem
where  StandardId ='{0}')", current.StandardId, standard.StandardId, maxitemid);
                standardManager.GetBySql(sql).ToDataSet();

                sql = "select MAX(ItemDetailId) from QmcCheckItemDetail ";
                 ds = standardManager.GetBySql(sql).ToDataSet();
                 maxitemid = ds.Tables[0].Rows[0][0].ToString();
                 sql = String.Format(@"insert into QmcCheckItemDetail
select RANK() OVER (ORDER BY ItemDetailId ) +{2} ,MaterialCode,t2.ItemId,Frequency,PrimeMaxValue,PrimeMinValue,PrimeOperator,PrimeTextValue,
GoodMaxValue,GoodMinValue,GoodOperator,GoodTextValue,CheckMethod,t1.Remark,
PrimeIncludeMinBorder,PrimeIncludeMaxBorder,GoodIncludeMinBorder,GoodIncludeMaxBorder,
Version,LatestFlag,ActivateFlag,t1.DeleteFlag,LastDate,GoodDisplayValue,PrimeDisplayValue,
InputGoodMaxValue,InputGoodMinValue,InputPrimeMaxValue,InputPrimeMinValue,OrderID,TeXing,
MinMaxValue,MinMinValue,MinOperator,MinTextValue,MinIncludeMinBorder,MinIncludeMaxBorder,
MinDisplayValue,InputMinMaxValue,InputMinMinValue,MaxMaxValue,MaxMinValue,MaxOperator,
MaxTextValue,MaxIncludeMinBorder,MaxIncludeMaxBorder,MaxDisplayValue,InputMaxMaxValue,InputMaxMinValue
 from QmcCheckItemDetail t1
left join QmcCheckItem t2 on t1.ItemId=t2.CopyItemId and t2.StandardId ='{1}'
where t1.ItemId in 
(select ItemId from QmcCheckItem where  StandardId ='{0}')", current.StandardId, standard.StandardId, maxitemid);
                 ds = standardManager.GetBySql(sql).ToDataSet();
        }
            #region 以前复制标准的代码 理论正确但是算法太简单导致数据库压力太大 已注释
            //复制当前生效的执行标准细节到新的执行标准
            //if (cbxDoCopy.Checked)
            //{
            //    EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.StandardId == current.StandardId) && (QmcCheckItem._.DeleteFlag == "0"));
            //    if (itemList.Count > 0)
            //    {
            //        foreach (QmcCheckItem item in itemList)
            //        {
            //            QmcCheckItem newItem = new QmcCheckItem();
            //            int originId = item.ItemId;
            //            newItem.ItemId = Convert.ToInt32(itemManager.GetNextItemId());
            //            newItem.DeleteFlag = item.DeleteFlag;
            //            newItem.ItemCode = item.ItemCode;
            //            newItem.ItemName = item.ItemName;
            //            newItem.Remark = item.Remark;
            //            newItem.SeriesId = item.SeriesId;
            //            newItem.StandardId = standard.StandardId;
            //            newItem.ValueType = item.ValueType;
            //            itemManager.Insert(newItem);
            //            EntityArrayList<QmcCheckItemDetail> detailList = detailManager.GetListByWhere((QmcCheckItemDetail._.ItemId == originId) && (QmcCheckItemDetail._.DeleteFlag == "0") && (QmcCheckItemDetail._.LatestFlag == "1"));
            //            if (detailList.Count > 0)
            //            {
            //                foreach (QmcCheckItemDetail detail in detailList)
            //                {
            //                    QmcCheckItemDetail newDetail = new QmcCheckItemDetail();
            //                    newDetail.ItemDetailId = Convert.ToInt32(detailManager.GetNextDetailId());
            //                    newDetail.Version = 1;
            //                    newDetail.ActivateFlag = detail.ActivateFlag;
            //                    newDetail.CheckMethod = detail.CheckMethod;
            //                    newDetail.DeleteFlag = detail.DeleteFlag;
            //                    newDetail.Frequency = detail.Frequency;
            //                    newDetail.GoodIncludeMaxBorder = detail.GoodIncludeMaxBorder;
            //                    newDetail.GoodIncludeMinBorder = detail.GoodIncludeMinBorder;
            //                    newDetail.GoodMaxValue = detail.GoodMaxValue;
            //                    newDetail.GoodMinValue = detail.GoodMinValue;
            //                    newDetail.GoodOperator = detail.GoodOperator;
            //                    newDetail.GoodTextValue = detail.GoodTextValue;
            //                    newDetail.ItemId = newItem.ItemId;
            //                    newDetail.LastDate = DateTime.Now;
            //                    newDetail.LatestFlag = detail.LatestFlag;
            //                    newDetail.MaterialCode = detail.MaterialCode;
            //                    newDetail.PrimeIncludeMaxBorder = detail.PrimeIncludeMaxBorder;
            //                    newDetail.PrimeIncludeMinBorder = detail.PrimeIncludeMinBorder;
            //                    newDetail.PrimeMaxValue = detail.PrimeMaxValue;
            //                    newDetail.PrimeMinValue = detail.PrimeMinValue;
            //                    newDetail.PrimeOperator = detail.PrimeOperator;
            //                    newDetail.PrimeTextValue = detail.PrimeTextValue;
            //                    newDetail.Remark = detail.Remark;
            //                    detailManager.Insert(newDetail);
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion 
            this.AppendWebLog("新建执行标准", "标准编号：" + standard.StandardId);
            pageToolBar.DoRefresh();
            this.windowAddStandard.Close();
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
    /// 点击修改关系中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyStandardSave_Click(object sender, EventArgs e)
    {
        try
        {
            QmcStandard current = new QmcStandard();
            //添加校验重复
            EntityArrayList<QmcStandard> lst = standardManager.GetListByWhere((QmcStandard._.StandardName == txtModifyStandardName.Value.ToString()) && (QmcStandard._.StandardId != Convert.ToInt32(txtModifyStandardId.Value)) && (QmcStandard._.DeleteFlag == "0"));
            if (lst.Count > 0)
            {
                X.Msg.Alert("提示", "标准名称已被使用！").Show();
                return;
            }
            EntityArrayList<QmcStandard> lst0 = standardManager.GetListByWhere(QmcStandard._.ActivateFlag == "1");
            if (lst0.Count > 0)
            {
                current = lst0[0];
                if (current.ActivateDate >= Convert.ToDateTime(dtfModifyActivateDate.Value))
                {
                    X.Msg.Alert("提示", "未生效的执行标准生效日期必须晚于当前执行标准的生效日期！").Show();
                    return;
                }
            }
            if (DateTime.Now.Date >= Convert.ToDateTime(dtfModifyActivateDate.Value).Date)
            {
                X.Msg.Alert("提示", "新建的执行标准最早要在次日生效！").Show();
                return;
            }
            //日期重复
            EntityArrayList<QmcStandard> lstAll = standardManager.GetListByWhere(QmcStandard._.DeleteFlag == "0");
            foreach (QmcStandard standardAll in lstAll)
            {
                if (standardAll.ActivateDate.Date == Convert.ToDateTime(dtfModifyActivateDate.Value).Date)
                {
                    if (standardAll.StandardId.ToString() != txtModifyStandardId.Value.ToString())
                    {
                        X.Msg.Alert("提示", "同一天不能有两个或两个以上的执行标准！").Show();
                        return;
                    }
                }
            }
            QmcStandard standard = standardManager.GetById(txtModifyStandardId.Value.ToString());
            standard.StandardName = txtModifyStandardName.Value.ToString();
            standard.ActivateDate = Convert.ToDateTime(dtfModifyActivateDate.Value);
            standard.Remark = txtModifyRemark.Text.ToString();
            standardManager.Update(standard);
            this.AppendWebLog("执行标准修改", "标准编号：" + standard.StandardId);
            pageToolBar.DoRefresh();
            this.windowModifyStandard.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.windowModifyStandard.Close();
        this.windowAddStandard.Close();
    }
    #endregion
}