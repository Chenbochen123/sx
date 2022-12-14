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
public partial class Manager_Equipment_SparePart_SparePartInfo : Mesnac.Web.UI.Page
{
    protected IEqmSparePartManager manager = new EqmSparePartManager();//业务对象
    protected IEqmSparePartMainTypeManager majorTypeManager = new EqmSparePartMainTypeManager();//业务对象
    protected IEqmSparePartDetailTypeManager minorTypeManager = new EqmSparePartDetailTypeManager();//业务对象
    protected IBasUnitManager unitManager = new BasUnitManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<EqmSparePart> entityList;


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
            InitTreeSparePartType();
        }
    }

    /// <summary>
    /// 初始化物料分类列表树
    /// </summary>
    private void InitTreeSparePartType()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        EntityArrayList<EqmSparePartMainType> majorList = majorTypeManager.GetListByWhere(EqmSparePartMainType._.DeleteFlag == 0);
        treeDept.GetRootNode().RemoveAll();
        foreach (EqmSparePartMainType majorType in majorList)
        {
            Node node = new Node();
            node.NodeID = majorType.ObjID.ToString();
            node.Text = majorType.MainTypeName;
            node.Icon = Icon.Brick;
            EntityArrayList<EqmSparePartDetailType> minorList = minorTypeManager.GetListByWhere(EqmSparePartDetailType._.MainTypeID == majorType.ObjID && EqmSparePartDetailType._.DeleteFlag == 0);
            foreach (EqmSparePartDetailType minorType in minorList)
            {
                Node nodeChild = new Node();
                nodeChild.NodeID = majorType.ObjID.ToString() + "|" + minorType.DetailTypeCode.ToString();
                nodeChild.Text = minorType.DetailTypeName;
                nodeChild.Leaf = true;
                nodeChild.Icon = Icon.Box;
                node.Children.Add(nodeChild);
            }
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmSparePart> GetPageResultData(PageResult<EqmSparePart> pageParams)
    {
        EqmSparePartManager.QueryParams queryParams = new EqmSparePartManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.sparePartMainType = hidden_major_type_id.Value.ToString();
        queryParams.sparePartDetailType = hidden_minor_type_id.Value.ToString();
        queryParams.sparePartName = txt_sparepare_name.Text;
        queryParams.sparePartCode = txt_sparepart_code.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        if (!Regex.IsMatch(txt_sparepart_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_sparepart_code.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmSparePart> pageParams = new PageResult<EqmSparePart>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<EqmSparePart> lst = GetPageResultData(pageParams);
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

        if (!Regex.IsMatch(txt_sparepart_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_sparepart_code.Text = "";
        }
        PageResult<EqmSparePart> pageParams = new PageResult<EqmSparePart>();
        pageParams.PageSize = -100;
        PageResult<EqmSparePart> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "备件信息");
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
        if (hidden_minor_type_id.Value == "")
        {
            msg.Alert("操作", "请选择左侧细类节点！");
            msg.Show();
            return;
        }
        string minorTypeID = hidden_minor_type_id.Text;
        string majorTypeID = hidden_major_type_id.Text;
        EqmSparePartDetailType minorType = new EqmSparePartDetailType();
        minorType = minorTypeManager.GetListByWhere(EqmSparePartDetailType._.MainTypeID == majorTypeID &&
                                                EqmSparePartDetailType._.DetailTypeCode == minorTypeID)[0];
        add_spare_part_major_type.Text = majorTypeManager.GetById(hidden_major_type_id.Value).MainTypeName;
        add_spare_part_minor_type.Text = minorType.DetailTypeName;

        add_minor_unit_code.Text = "";
        hidden_minor_unit_code.Text = "";

        add_unit_code.Text = "";
        hidden_unit_code.Text = "";

        add_spare_part_name.Text = "";
        add_spare_part_other_name.Text = "";
        add_spare_part_simple_name.Text = "";
        add_sap_code.Text = "";
        add_spare_part_standards.Text ="";
        add_price.Text = "0";
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
        EqmSparePart sparepart = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = objID;
        modify_spare_part_code.Text = sparepart.SparePartCode;
        hidden_major_type_id.Text = sparepart.SparePartMainType;
        hidden_minor_type_id.Text = sparepart.SparePartDetailType;

        modify_spare_part_major_type.Text = majorTypeManager.GetById(sparepart.SparePartMainType).MainTypeName;
        EqmSparePartDetailType minorType = new EqmSparePartDetailType();
        minorType = minorTypeManager.GetListByWhere(EqmSparePartDetailType._.MainTypeID == sparepart.SparePartMainType &&
                                                EqmSparePartDetailType._.DetailTypeCode == sparepart.SparePartDetailType)[0];
        modify_spare_part_minor_type.Text = minorType.DetailTypeName;
        try
        {
            hidden_unit_code.Text = sparepart.UnitCode;
            modify_unit_code.Text = unitManager.GetListByWhere(BasUnit._.ObjID == sparepart.UnitCode)[0].UnitName;
        }
        catch (Exception)
        {
        }
        try
        {
            hidden_minor_unit_code.Text = sparepart.MinorUnitCode;
            modify_minor_unit_code.Text = unitManager.GetListByWhere(BasUnit._.ObjID == sparepart.MinorUnitCode)[0].UnitName;
        }
        catch (Exception)
        {
            
        }
        modify_spare_part_name.Text = sparepart.SparePartName;
        hidden_modify_spare_part_name.Text = sparepart.SparePartName;
        modify_spare_part_other_name.Text = sparepart.SparePartOtherName;
        modify_spare_part_simple_name.Text = sparepart.SparePartSimpleName;
        modify_spare_part_standards.Text = sparepart.SparePartStandards;
        modify_price.Text = sparepart.Price.ToString();
        modify_sap_code.Text = sparepart.SAPCode;
        modify_remark.Text = sparepart.Remark;
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
            EqmSparePart sparepart = manager.GetById(obj_id);
            sparepart.DeleteFlag = "0";
            manager.Update(sparepart);
            this.AppendWebLog("备件信息恢复", "备件编号：" + sparepart.SparePartCode);
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
            EqmSparePart sparepart = manager.GetById(objID);
            sparepart.DeleteFlag = "1";
            manager.Update(sparepart);
            this.AppendWebLog("备件信息删除", "备件编号：" + sparepart.SparePartCode);
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
            EntityArrayList<EqmSparePart> sparepartList = manager.GetListByWhere(EqmSparePart._.SparePartName == add_spare_part_name.Text.TrimStart().TrimEnd());
            if (sparepartList.Count > 0)
            {
                X.Msg.Alert("提示", "此备件名称已被使用！").Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(add_spare_part_name.Text.TrimStart().TrimEnd()))
            {
                X.Msg.Alert("提示", "备件名称不允许为空或空格！").Show();
                return;
            }
            EqmSparePart sparepart = new EqmSparePart();
            sparepart.SparePartMainType = hidden_major_type_id.Text;
            sparepart.SparePartDetailType = hidden_minor_type_id.Text;
            sparepart.SparePartCode = manager.GetNextSparePartCode(sparepart.SparePartMainType, sparepart.SparePartDetailType);
            sparepart.SparePartName = add_spare_part_name.Text;
            sparepart.SparePartOtherName = add_spare_part_other_name.Text;
            sparepart.SparePartSimpleName = add_spare_part_simple_name.Text;
            sparepart.SparePartStandards = add_spare_part_standards.Text;
            sparepart.UnitCode = hidden_unit_code.Text;
            sparepart.MinorUnitCode = hidden_minor_unit_code.Text;
            sparepart.Price = Convert.ToDecimal(add_price.Text);
            sparepart.SAPCode = add_sap_code.Text;
            sparepart.Remark = add_remark.Text;

            sparepart.DeleteFlag = "0";
            manager.Insert(sparepart);
            this.AppendWebLog("备件信息增加", "备件编号：" + sparepart.SparePartCode);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
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
            EntityArrayList<EqmSparePart> sparepartList = manager.GetListByWhere(EqmSparePart._.SparePartName == modify_spare_part_name.Text.TrimStart().TrimEnd());
            if (sparepartList.Count > 0)
            {
                if (sparepartList[0].SparePartName != hidden_modify_spare_part_name.Text)
                {
                    X.Msg.Alert("提示", "此备件名称已被使用！").Show();
                    return;
                }
            }
            EqmSparePart sparepart = new EqmSparePart();
            sparepart.ObjID = Convert.ToInt32(modify_obj_id.Text);
            sparepart.Attach();
            sparepart.SparePartMainType = hidden_major_type_id.Text;
            sparepart.SparePartDetailType = hidden_minor_type_id.Text;
            sparepart.SparePartCode = modify_spare_part_code.Text;
            sparepart.SparePartName = modify_spare_part_name.Text;
            sparepart.SparePartOtherName = modify_spare_part_other_name.Text;
            sparepart.SparePartSimpleName = modify_spare_part_simple_name.Text;
            sparepart.SparePartStandards = modify_spare_part_standards.Text;
            sparepart.UnitCode = hidden_unit_code.Text;
            sparepart.MinorUnitCode = hidden_minor_unit_code.Text;
            sparepart.Price = Convert.ToDecimal(modify_price.Text);
            sparepart.SAPCode = modify_sap_code.Text;
            sparepart.Remark = modify_remark.Text;
            sparepart.DeleteFlag = "0";
            manager.Update(sparepart);
            this.AppendWebLog("备件信息修改", "备件编号：" + modify_spare_part_code.Text);
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
    #endregion
}