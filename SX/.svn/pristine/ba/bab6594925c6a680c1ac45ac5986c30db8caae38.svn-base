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
using Mesnac.Data.Components;

public partial class Manager_RawMaterialQuality_CheckItem : BasePage
{
    #region 属性注入
    protected IQmcCheckItemManager itemManager = new QmcCheckItemManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected static EntityArrayList<QmcCheckItem> copyList = new EntityArrayList<QmcCheckItem>();//存放复制的数据
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增项目 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            复制 = new SysPageAction() { ActionID = 3, ActionName = "btnCopy" };
            粘贴 = new SysPageAction() { ActionID = 4, ActionName = "btnPaste" };
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增项目 { get; private set; } //必须为 public
        public SysPageAction 复制 { get; private set; } //必须为 public
        public SysPageAction 粘贴 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    /// <summary>
    /// 页面初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeSeries();//初始化原材料系列树
            InitStandard();//初始化执行标准
        }
    }

    /// <summary>
    /// 初始化原材料系列树
    /// </summary>
    private void InitTreeSeries()
    {
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));
        foreach (BasMaterialMinorType minorType in seriesList)
        {
            Node nodeChild = new Node();
            nodeChild.NodeID = minorType.ObjID.ToString();
            nodeChild.Text = minorType.MinorTypeName;
            nodeChild.Leaf = true;
            nodeChild.Icon = Icon.Box;
            treeSeries.GetRootNode().AppendChild(nodeChild);
        }
    }

    /// <summary>
    /// 初始化执行标准
    /// </summary>
    private void InitStandard()
    {
        EntityArrayList<QmcStandard> standardList = standardManager.GetListByWhereAndOrder(QmcStandard._.DeleteFlag == "0", QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> activeList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "1")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> readyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "0")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> historyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "2")), QmcStandard._.ActivateFlag.Asc);
        if (standardList.Count == 0)
        {
            btnAdd.Disable();
            btnCopy.Disable();
            btnExport.Disable();
            btnPaste.Disable();
            btnSearch.Disable();
            msg.Alert("提示", "当前没有执行标准，请新建执行标准！");
            msg.Show();
        }
        else
        {
            foreach (QmcStandard standard in standardList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = standard.StandardName;
                item.Value = standard.StandardId.ToString();
                if (standard.ActivateFlag == "2")
                {
                    item.Text = item.Text + "(过期)";
                }
                else if (standard.ActivateFlag == "0")
                {
                    item.Text = item.Text + "(未生效)";
                }
                else if (standard.ActivateFlag == "1")
                {
                    item.Text = item.Text + "(当前)";
                    cbxStandard.Text = item.Text;
                    cbxStandard.Value = item.Value;
                }
                cbxStandard.Items.Add(item);
            }
        }
        if (activeList.Count == 0)
        {
            if (readyList.Count > 0)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = readyList[0].StandardName + "(未生效)";
                item.Value = readyList[0].StandardId.ToString();
                cbxStandard.Text = item.Text;
                cbxStandard.Value = item.Value;
            }
            else if (historyList.Count > 0)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = historyList[0].StandardName + "(过期)";
                item.Value = historyList[0].StandardId.ToString();
                cbxStandard.Text = item.Text;
                cbxStandard.Value = item.Value;
            }
            else
            {
                btnAdd.Disable();
                btnCopy.Disable();
                btnExport.Disable();
                btnPaste.Disable();
                btnSearch.Disable();
                msg.Alert("提示", "当前没有执行标准，请新建执行标准！");
                msg.Show();
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
    private PageResult<QmcCheckItem> GetPageResultData(PageResult<QmcCheckItem> pageParams)
    {
        QmcCheckItemManager.QueryParams queryParams = new QmcCheckItemManager.QueryParams();
        queryParams.pageParams = pageParams;
        if (cbxValueType.SelectedItem.Value != "all")
        {
            queryParams.valueType = cbxValueType.SelectedItem.Value;
        }
        if (cbxStandard.Value != null)
        {
            queryParams.standardId = cbxStandard.Value.ToString();
        }
        queryParams.seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        queryParams.deleteFlag = "0";
        return itemManager.GetTablePageDataBySql(queryParams);
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
        PageResult<QmcCheckItem> pageParams = new PageResult<QmcCheckItem>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ItemId ASC";//Id升序排列

        PageResult<QmcCheckItem> lst = GetPageResultData(pageParams);
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
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        //放开当前执行标准的删改权限
        //if (standard.ActivateFlag == "1")
        //{
        //    msg.Alert("操作", "所选标准已生效，不能新增检测项目！");
        //    msg.Show();
        //    return;
        //}
        //else 
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能新增检测项目！");
            msg.Show();
            return;
        }
        //初始化添加窗口
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));//获取当前的原材料系列Id
        txtAddItemSeriesName.Value = type.MinorTypeName;
        txtAddItemId.Value = "";
        txtAddItemName.Value = "";
        txtHiddenItemName.Value = "";
        txtHiddenItemCode.Value = "";
        txtAddItemCode.Value = "";
        txtAddItemRemark.Value = "";
        cbxAddValueType.Value = "文字";
        btnAddItemSave.Disable(true);
        this.windowAddItem.Show();
    }

    /// <summary>
    /// 点击复制按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();//获取当前的原材料系列Id
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == seriesId) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"));
        if (itemList.Count > 0)
        {
            copyList.Clear();
            foreach (QmcCheckItem item in itemList)
            {
                copyList.Add(item);
            }
            msg.Alert("操作", "复制了" + copyList.Count + "条记录！");
            msg.Show();
        }
        else
        {
            msg.Alert("操作", "没有可以复制的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击粘贴按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Paste_Click(object sender, EventArgs e)
    {
        if (copyList.Count > 0)
        {
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                msg.Alert("操作", "没有选择执行标准！");
                msg.Show();
                return;
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //放开当前执行标准的删改权限
            //if (standard.ActivateFlag == "1")
            //{
            //    msg.Alert("操作", "所选标准已生效，不能执行此操作！");
            //    msg.Show();
            //    return;
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                msg.Alert("操作", "所选标准已过期，不能执行此操作！");
                msg.Show();
                return;
            }
            //不能粘贴给自身
            if (copyList[0].SeriesId.ToString() == txtHiddenMaterialMinorTypeId.Value.ToString() && copyList[0].StandardId.ToString() == standardId)
            {
                msg.Alert("操作", "不能粘贴到源系列！");
                msg.Show();
                return;
            }
            EntityArrayList<QmcCheckItem> pasteList = new EntityArrayList<QmcCheckItem>();//存放要粘贴的数据
            EntityArrayList<QmcCheckItem> originList = itemManager.GetListByWhere(QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString() && QmcCheckItem._.StandardId == standardId);//目标系列原有的检测项目
            int idIndex = 0;//Id自增量
            //略过重复的项目
            foreach (QmcCheckItem item in copyList)
            {
                bool hasRepeat = false;
                foreach (QmcCheckItem originItem in originList)
                {
                    if (item.ItemName == originItem.ItemName && originItem.DeleteFlag == "0")
                    {
                        hasRepeat = true;
                    }
                }
                if (hasRepeat)
                {
                    continue;
                }
                //建立要粘贴的数据
                QmcCheckItem pastedItem = new QmcCheckItem();
                pastedItem.StandardId = Convert.ToInt32(standardId);
                pastedItem.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value);
                pastedItem.ItemId = Convert.ToInt32(itemManager.GetNextItemId()) + idIndex;//下一个可用的Id+自增量
                pastedItem.ItemName = item.ItemName;
                pastedItem.ItemCode = item.ItemCode;
                pastedItem.ValueType = item.ValueType;
                pastedItem.Remark = item.Remark;
                pastedItem.DeleteFlag = item.DeleteFlag;
                pasteList.Add(pastedItem);
                idIndex++;
            }
            //写入粘贴的数据
            try
            {
                int pasteCount = 0;//粘贴成功计数
                foreach (QmcCheckItem item in pasteList)
                {
                    itemManager.Insert(item);
                    pasteCount++;
                }
                this.AppendWebLog("检测项目粘贴", "粘贴数量：" + pasteCount);
                pageToolBar.DoRefresh();
                msg.Alert("操作", "粘贴了" + pasteCount + "条检测项目！");
                msg.Show();
            }
            catch (Exception ex)
            {
                msg.Alert("操作", "粘贴失败：" + ex);
                msg.Show();
            }
        }
        else
        {
            msg.Alert("操作", "没有可以粘贴的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<QmcCheckItem> pageParams = new PageResult<QmcCheckItem>();
        pageParams.PageSize = -100;//全部数据导出
        PageResult<QmcCheckItem> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlItem.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()) && (cb.Visible == true))
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
        if (lst.DataSet.Tables[0].Rows.Count > 0)
        {
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "检测项目报表-" + cbxStandard.SelectedItem.Text + "-" + minorTypeManager.GetById(txtHiddenMaterialMinorTypeId.Text).MinorTypeName);//生成Excel文件下载
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string itemId)
    {
        try
        {
            QmcCheckItem item = itemManager.GetById(Convert.ToInt32(itemId));
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                return "删除失败：没有选择执行标准！";
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //放开当前执行标准的删改权限
            //if (standard.ActivateFlag == "1")
            //{
            //    return "删除失败：所选标准已生效，不能删除检测项目！";
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                return "删除失败：所选标准已过期，不能删除检测项目！";
            }
            item.DeleteFlag = "1";
            itemManager.Update(item);
            this.AppendWebLog("项目删除", "检测项目编号：" + itemId);
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
    public void commandcolumn_direct_edit(string itemId)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        //放开当前执行标准的删改权限
        //if (standard.ActivateFlag == "1")
        //{
        //    msg.Alert("操作", "所选标准已生效，不能修改检测项目！");
        //    msg.Show();
        //    return;
        //}
        //else 
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能修改检测项目！");
            msg.Show();
            return;
        }
        //初始化修改窗口
        QmcCheckItem item = itemManager.GetById(Convert.ToInt32(itemId));
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));
        txtModifyItemId.Value = item.ItemId;
        txtModifyItemSeriesName.Value = type.MinorTypeName;
        txtModifyItemName.Value = item.ItemName;
        txtModifyItemCode.Value = item.ItemCode;
        txtModifyItemRemark.Value = item.Remark;
        txtHiddenItemName.Value = item.ItemName;
        txtHiddenItemCode.Value = item.ItemCode;
        cbxModifyValueType.Value = item.ValueType;
        this.windowModifyItem.Show();
    }

    /// <summary>
    /// 点击添加项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddItemSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                msg.Alert("操作", "没有选择执行标准！");
                msg.Show();
                return;
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //放开当前执行标准的删改权限
            //if (standard.ActivateFlag == "1")
            //{
            //    msg.Alert("操作", "所选标准已生效，不能修改检测项目！");
            //    msg.Show();
            //    return;
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                msg.Alert("操作", "所选标准已过期，不能修改检测项目！");
                msg.Show();
                return;
            }
            //添加校验重复
            EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Text) && (QmcCheckItem._.StandardId == standardId) && ((QmcCheckItem._.ItemName == txtAddItemName.Text.TrimStart().TrimEnd()) || (QmcCheckItem._.ItemCode == txtAddItemCode.Text.TrimStart().TrimEnd())) && (QmcCheckItem._.DeleteFlag == "0") && ((QmcCheckItem._.ItemCode != null) && (QmcCheckItem._.ItemCode != "")));
            if (itemList.Count > 0)
            {
                X.Msg.Alert("提示", "此项目名称或英文名称已被使用！").Show();
                return;
            }
            QmcCheckItem item = new QmcCheckItem();
            item.ItemId = Convert.ToInt32(itemManager.GetNextItemId());
            if (cbxStandard.Value != null)
            {
                item.StandardId = Convert.ToInt32(cbxStandard.Value);
            }
            else
            {
                X.Msg.Alert("提示", "未选择执行标准！").Show();
                return;
            }
            item.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            item.ItemName = (string)(txtAddItemName.Text);
            item.ItemCode = (string)(txtAddItemCode.Text);
            item.Remark = (string)(txtAddItemRemark.Text);
            item.ValueType = (string)(cbxAddValueType.Value);
            item.DeleteFlag = "0";
            itemManager.Insert(item);
            this.AppendWebLog("检测项目添加", "项目编号：" + item.ItemId);
            pageToolBar.DoRefresh();
            this.windowAddItem.Close();
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
    /// 点击修改项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyItemSave_Click(object sender, EventArgs e)
    {
        try
        {
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                msg.Alert("操作", "没有选择执行标准！");
                msg.Show();
                return;
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //放开当前执行标准的删改权限
            //if (standard.ActivateFlag == "1")
            //{
            //    msg.Alert("操作", "所选标准已生效，不能修改检测项目！");
            //    msg.Show();
            //    return;
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                msg.Alert("操作", "所选标准已过期，不能修改检测项目！");
                msg.Show();
                return;
            }
            //修改重复校验
            EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Text) && (QmcCheckItem._.StandardId == standardId) && ((QmcCheckItem._.ItemName == txtModifyItemName.Text.TrimStart().TrimEnd()) || (QmcCheckItem._.ItemCode == txtModifyItemCode.Text.TrimStart().TrimEnd())) && (QmcCheckItem._.DeleteFlag == "0") && ((QmcCheckItem._.ItemCode != null) && (QmcCheckItem._.ItemCode != "")));
            if (itemList.Count > 0)
            {
                if (itemList[0].ItemName != txtHiddenItemName.Text)
                {
                    X.Msg.Alert("提示", "此项目名称已被使用！").Show();
                    return;
                }
                if (itemList[0].ItemCode != txtHiddenItemCode.Text)
                {
                    X.Msg.Alert("提示", "此英文名称已被使用！").Show();
                    return;
                }
            }
            QmcCheckItem item = new QmcCheckItem();
            item.ItemId = Convert.ToInt32(txtModifyItemId.Text);
            item.Attach();
            if (cbxStandard.Value != null)
            {
                item.StandardId = Convert.ToInt32(cbxStandard.Value);
            }
            else
            {
                X.Msg.Alert("提示", "未选择执行标准！").Show();
                return;
            }
            item.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            item.ItemName = (string)(txtModifyItemName.Text);
            item.ItemCode = (string)(txtModifyItemCode.Text);
            item.Remark = (string)(txtModifyItemRemark.Text);
            item.ValueType = (string)(cbxModifyValueType.Value);
            itemManager.Update(item);
            this.AppendWebLog("检测项目修改", "项目编号：" + txtModifyItemId.Text);
            pageToolBar.DoRefresh();
            this.windowModifyItem.Close();
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
        this.windowModifyItem.Close();
        this.windowAddItem.Close();
    }
    #endregion
}