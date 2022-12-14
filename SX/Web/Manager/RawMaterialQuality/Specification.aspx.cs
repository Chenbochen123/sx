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

public partial class Manager_RawMaterialQuality_Specification : BasePage
{
    #region 属性注入
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected static EntityArrayList<QmcSpec> copyList = new EntityArrayList<QmcSpec>();//存放复制的数据
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            复制 = new SysPageAction() { ActionID = 3, ActionName = "btnCopy" };
            粘贴 = new SysPageAction() { ActionID = 4, ActionName = "btnPaste" };
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增 { get; private set; } //必须为 public
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
    #endregion

    #region 分页相关方法
    /// <summary>
    /// 根据筛选条件获取分页数据
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<QmcSpec> GetPageResultData(PageResult<QmcSpec> pageParams)
    {
        QmcSpecManager.QueryParams queryParams = new QmcSpecManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.specName = txtSpecName.Text.TrimStart().TrimEnd();
        queryParams.seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        queryParams.deleteFlag = "0";
        return specManager.GetTablePageDataBySql(queryParams);
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
        PageResult<QmcSpec> pageParams = new PageResult<QmcSpec>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "SpecId ASC";//Id升序排列

        PageResult<QmcSpec> lst = GetPageResultData(pageParams);
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
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));//获取当前的原材料系列Id
        txtAddSpecSeriesName.Value = type.MinorTypeName;
        txtAddSpecId.Value = "";
        txtAddSpecName.Value = "";
        txtHiddenSpecName.Value = "";
        txtAddSpecRemark.Value = "";
        btnAddSpecSave.Disable(true);
        this.windowAddSpec.Show();
    }

    /// <summary>
    /// 点击复制按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();//获取当前的原材料系列Id
        EntityArrayList<QmcSpec> specList = specManager.GetListByWhere((QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0"));
        if (specList.Count > 0)
        {
            copyList.Clear();
            foreach (QmcSpec spec in specList)
            {
                copyList.Add(spec);
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
            //不能粘贴给自身
            if (copyList[0].SeriesId.ToString() == txtHiddenMaterialMinorTypeId.Value.ToString())
            {
                msg.Alert("操作", "不能粘贴到源系列！");
                msg.Show();
            }
            else
            {
                EntityArrayList<QmcSpec> pasteList = new EntityArrayList<QmcSpec>();//存放要粘贴的数据
                EntityArrayList<QmcSpec> originList = specManager.GetListByWhere(QmcSpec._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString());//目标系列原有的规格
                int idIndex = 0;//Id自增量
                //略过重复的项目
                foreach (QmcSpec spec in copyList)
                {
                    bool hasRepeat = false;
                    foreach (QmcSpec originSpec in originList)
                    {
                        if (spec.SpecName == originSpec.SpecName && originSpec.DeleteFlag == "0")
                        {
                            hasRepeat = true;
                        }
                    }
                    if (hasRepeat)
                    {
                        continue;
                    }
                    //建立要粘贴的数据
                    QmcSpec pastedSpec = new QmcSpec();
                    pastedSpec.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value);
                    pastedSpec.SpecId = Convert.ToInt32(specManager.GetNextSpecId()) + idIndex;//下一个可用的Id+自增量
                    pastedSpec.SpecName = spec.SpecName;
                    pastedSpec.Remark = spec.Remark;
                    pastedSpec.DeleteFlag = spec.DeleteFlag;
                    pasteList.Add(pastedSpec);
                    idIndex++;
                }
                //写入粘贴的数据
                try
                {
                    int pasteCount = 0;//粘贴成功计数
                    foreach (QmcSpec spec in pasteList)
                    {
                        specManager.Insert(spec);
                        pasteCount++;
                    }
                    this.AppendWebLog("规格粘贴", "粘贴数量：" + pasteCount);
                    pageToolBar.DoRefresh();
                    msg.Alert("操作", "粘贴了" + pasteCount + "条规格！");
                    msg.Show();
                }
                catch (Exception ex)
                {
                    msg.Alert("操作", "粘贴失败：" + ex);
                    msg.Show();
                }
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
        PageResult<QmcSpec> pageParams = new PageResult<QmcSpec>();
        pageParams.PageSize = -100;//全部数据导出
        PageResult<QmcSpec> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlSpec.ColumnModel.Columns)
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "规格报表-" + minorTypeManager.GetById(txtHiddenMaterialMinorTypeId.Text).MinorTypeName);//生成Excel文件下载
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
    public string commandcolumn_direct_delete(string specId)
    {
        try
        {
            QmcSpec spec = specManager.GetById(Convert.ToInt32(specId));
            spec.DeleteFlag = "1";
            specManager.Update(spec);
            this.AppendWebLog("规格删除", "规格编号：" + specId);
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
    public void commandcolumn_direct_edit(string specId)
    {
        //初始化修改窗口
        QmcSpec spec = specManager.GetById(Convert.ToInt32(specId));
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));
        txtModifySpecId.Value = spec.SpecId;
        txtModifySpecSeriesName.Value = type.MinorTypeName;
        txtModifySpecName.Value = spec.SpecName;
        txtModifySpecRemark.Value = spec.Remark;
        txtHiddenSpecName.Value = spec.SpecName;
        this.windowModifySpec.Show();
    }

    /// <summary>
    /// 点击添加规格中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSpecSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcSpec> specList = specManager.GetListByWhere((QmcSpec._.SeriesId == txtHiddenMaterialMinorTypeId.Text) && (QmcSpec._.SpecName == txtAddSpecName.Text.TrimStart().TrimEnd()) && (QmcSpec._.DeleteFlag == "0"));
            if (specList.Count > 0)
            {
                X.Msg.Alert("提示", "此规格名称已存在！").Show();
                return;
            }
            QmcSpec spec = new QmcSpec();
            spec.SpecId = Convert.ToInt32(specManager.GetNextSpecId());
            spec.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            spec.SpecName = (string)(txtAddSpecName.Text);
            spec.Remark = (string)(txtAddSpecRemark.Text);
            spec.DeleteFlag = "0";
            specManager.Insert(spec);
            this.AppendWebLog("规格添加", "规格编号：" + spec.SpecId);
            pageToolBar.DoRefresh();
            this.windowAddSpec.Close();
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
    /// 点击修改规格中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySpecSave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            EntityArrayList<QmcSpec> specList = specManager.GetListByWhere((QmcSpec._.SeriesId == txtHiddenMaterialMinorTypeId.Text) && (QmcSpec._.SpecName == txtModifySpecName.Text.TrimStart().TrimEnd()) && (QmcSpec._.DeleteFlag == "0"));
            if (specList.Count > 0)
            {
                if (specList[0].SpecName != txtHiddenSpecName.Text)
                {
                    X.Msg.Alert("提示", "此规格名称已被使用！").Show();
                    return;
                }
            }
            QmcSpec spec = new QmcSpec();
            spec.SpecId = Convert.ToInt32(txtModifySpecId.Text);
            spec.Attach();
            spec.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            spec.SpecName = (string)(txtModifySpecName.Text);
            spec.Remark = (string)(txtModifySpecRemark.Text);
            specManager.Update(spec);
            this.AppendWebLog("规格修改", "规格编号：" + txtModifySpecId.Text);
            pageToolBar.DoRefresh();
            this.windowModifySpec.Close();
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
        this.windowModifySpec.Close();
        this.windowAddSpec.Close();
    }
    #endregion
}