﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

public partial class Manager_BasicInfo_MaterialInfo_MaterialInfo : Mesnac.Web.UI.Page
{
    protected BasMaterialManager manager = new BasMaterialManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected BasMaterialMajorTypeManager majorTypeManager = new BasMaterialMajorTypeManager();
    protected BasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected BasUnitManager unitManager = new BasUnitManager();
    protected BasRubInfoManager rubInfoManager = new BasRubInfoManager();
    protected BasMaterialStaticClassManager staticClassManager = new BasMaterialStaticClassManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected PmtMixTypeManager mixTypeManager = new PmtMixTypeManager();

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
            设置物料分组 = new SysPageAction() { ActionID = 8, ActionName = "btn_material_group" };
            设置停放时间 = new SysPageAction() { ActionID = 9, ActionName = "btn_set_stock" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 设置物料分组 { get; private set; } //必须为 public
        public SysPageAction 设置停放时间 { get; private set; } //必须为 public
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
        //EntityArrayList<PmtMixType> mixTypeList = mixTypeManager.GetAllList();
        //foreach (PmtMixType mixType in mixTypeList)
        //{
        //    Ext.Net.ListItem item = new Ext.Net.ListItem();
        //    item.Text = mixType.MixName;
        //    item.Value = mixType.MixName;
        //    add_mix_type.Items.Add(item);
        //    modify_mix_type.Items.Add(item);
        //}
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
        }
    }
    /// <summary>
    /// 初始化物料分类列表树
    /// </summary>
    private void InitTreeDept()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        EntityArrayList<BasMaterialMajorType> majorList = majorTypeManager.GetListByWhere(BasMaterialMajorType._.DeleteFlag == 0);
        treeDept.GetRootNode().RemoveAll();
        foreach (BasMaterialMajorType majorType in majorList)
        {
            
            Node node = new Node();
            node.NodeID = majorType.ObjID.ToString();
            node.Text = majorType.MajorTypeName;
            node.Icon = Icon.Brick;
            //EntityArrayList<BasMaterialMinorType> minorList = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MajorID == majorType.ObjID && BasMaterialMinorType._.DeleteFlag == 0);
            //foreach (BasMaterialMinorType minorType in minorList)
            //{
                
            //    Node nodeChild = new Node();
            //    nodeChild.NodeID = majorType.ObjID.ToString() +"|" + minorType.MinorTypeID.ToString();
            //    nodeChild.Text = minorType.MinorTypeName;
            //    nodeChild.Leaf = true;
            //    nodeChild.Icon = Icon.Box;
            //    node.Children.Add(nodeChild);
            //    //X.Msg.Notify(nodeChild.NodeID.ToString(), nodeChild.Text).Show();
            //}

            string sql = "select * from Pmt_ikind where  Mkind_code='" + majorType.ObjID + "' order by Ikind_code";
            DataTable dt = minorTypeManager.GetBySql(sql).ToDataSet().Tables[0];


            foreach (DataRow dr in dt.Rows)
            {
                Node nodeChild = new Node();
                nodeChild.NodeID = majorType.ObjID.ToString() + "|" + dr["Ikind_code"].ToString();
                nodeChild.Text = dr["Ikind_name"].ToString();
                nodeChild.Leaf = true;
                nodeChild.Icon = Icon.Box;
                node.Children.Add(nodeChild);
            }
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterial> GetPageResultData(PageResult<BasMaterial> pageParams)
    {
        BasMaterialManager.QueryParams queryParams = new BasMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txt_material_code.Text.TrimEnd().TrimStart();
        queryParams.minorTypeID = hidden_minor_type_id.Text;
        queryParams.majorTypeID = hidden_major_type_id.Text;
        queryParams.materialName = txt_material_name.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasMaterial> GetTablePageDataBySql(BasMaterialManager.QueryParams queryParams)
    {
        PageResult<BasMaterial> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT	    mater.ObjID, mater.MaterialCode, major.MajorTypeName as MajorTypeID, mater.SAPMaterialCode as PDM_Code,
                                            minor.MinorTypeName as MinorTypeID, rub.RubName as RubCode, 
                                            mater.MaterialName, mater.MaterialOtherName, mater.MaterialSimpleName, mater.MaterialLevel,
                                            mater.UserCode, mater.PlanPrice, mater.ProductArea, mater2.MaterialName as ProductMaterialCode, 
                                            mater.MinStock, mater.MaxStock, unit.UnitName as UnitID , unit2.UnitName as StaticUnitID, 
                                            mater.StaticUnitCoefficient, mater.CheckPermitError, mater.MaxParkTime, 
                                            mater.MinParkTime, mater.DefineDate, mater.StandardCode, mater3.MaterialName as MaterialGroup ,
 mater.MaterialGroup as MaterialGroupID,mater.CMaterialLevel,mater.CMaterialGroup as CMaterialGroupID,mater4.MaterialName as CMaterialGroup ,
                                            '' as StaticClass, '' as IsEqualMaterial ,'' as IsPutJar,
                                            '' as IsQualityRateCount, mater.ERPCode, mater.Remark, mater.DeleteFlag , mater.ValidDate
                                 FROM	    BasMaterial mater 
                                 LEFT JOIN  BasMaterialMajorType    major   on mater.MajorTypeID = major.ObjID 
                                 LEFT JOIN  BasMaterialMinorType    minor   on mater.MinorTypeID = minor.MinorTypeID  AND mater.MajorTypeID =  minor.MajorID
                                 LEFT JOIN  BasRubInfo rub                  on mater.RubCode = rub.Objid  
                                 LEFT JOIN  BasUnit unit                    on mater.UnitID = unit.ObjID
                                 LEFT JOIN  BasUnit unit2                   on mater.StaticUnitID = unit2.ObjID
                                 --LEFT JOIN  BasMaterialStaticClass  class   on mater.StaticClass = class.ObjID
                                 LEFT JOIN  BasMaterial             mater2  on mater.ProductMaterialCode = mater2.MaterialCode
                                 LEFT JOIN  BasMaterial             mater3  on mater.MaterialGroup = mater3.MaterialCode
                                 LEFT JOIN  BasMaterial             mater4  on mater.CMaterialGroup = mater4.MaterialCode
                                 WHERE      1 = 1");
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND mater.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.materialCode))
        {
            sqlstr.AppendLine(" AND mater.MaterialCode like '%" + queryParams.materialCode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.materialName))
        {
            sqlstr.AppendLine(" AND mater.MaterialName like '%" + queryParams.materialName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.majorTypeID))
        {
            sqlstr.AppendLine(" AND mater.MajorTypeID like '%" + queryParams.majorTypeID + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.minorTypeID))
        {
            sqlstr.AppendLine(" AND mater.MinorTypeID = " + queryParams.minorTypeID);
        }
        if (!string.IsNullOrEmpty(queryParams.rubCode))
        {
            sqlstr.AppendLine(" AND mater.RubCode = " + queryParams.rubCode);
        }
        if (!string.IsNullOrEmpty(queryParams.remark))
        {
            sqlstr.AppendLine(" AND mater.Remark like '%" + queryParams.remark + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND mater.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.minMajorTypeID))
        {
            sqlstr.AppendLine(" AND mater.MajorTypeID >'" + queryParams.minMajorTypeID + "'");
        }
        if (!string.IsNullOrEmpty(TextERP.Text))
        {
            sqlstr.AppendLine(" AND mater.ERPCode like '%" + TextERP.Text + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.workBarCode))
        {
            sqlstr.AppendLine(" AND mater.MaterialCode  in (Select Distinct RecipeMaterialCode  From PmtRecipe R Join BasEquip E On E.EquipCode = R.RecipeEquipCode Where E.WorkShopCode = '" + queryParams.workBarCode + "' And R.RecipeMaterialCode >= '4' And R.RecipeMaterialCode <= '5') ");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_material_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_material_code.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
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
        if (!Regex.IsMatch(txt_material_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_material_code.Text = "";
        }
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageSize = -100;
        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "物料信息");
    }
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [Ext.Net.DirectMethod()]
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (hidden_minor_type_id.Value == "")
        {
            msg.Alert("操作", "请选择左侧细类节点！");
            msg.Show();
            return;
        }
        string minorTypeID = hidden_minor_type_id.Value.ToString().PadLeft(2, '0');
        string majorTypeID = hidden_major_type_id.Text;
        BasMaterialMinorType minorType = new BasMaterialMinorType();
        minorType = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MinorTypeID == minorTypeID &&
                                                BasMaterialMinorType._.MajorID == majorTypeID)[0];

        
        hidden_material_part1.Value = "";
        hidden_material_part2.Value = "";
        hidden_material_part3.Value = "";
        hidden_rub_code.Value = "";
        hidden_static_class.Value = "";
        hidden_static_unit_id.Value = "";
        hidden_unit_id.Value = "";
        BtnAddFunction(majorTypeID , minorTypeID);

        add_major_type_id.Text = majorTypeManager.GetById(hidden_major_type_id.Value).MajorTypeName;
        add_minor_type_id.Text = minorType.MinorTypeName;
        add_material_name.Text = "";
        hidden_material_name.Text = "";
        add_material_other_name.Text = "";
        add_material_simple_name.Text = "";
        add_price_code.Text = "";
        add_valid_date.Text = "0";
        add_pruduct_area.Text = "";
        add_min_stock.Text = "";
        add_max_stock.Text = "";
        add_unit_id.Text = "0";
        add_static_unit_id.Text = "";
        add_static_unit_coefficient.Text = "";
        add_check_permit_error.Text = "";
        add_max_park_time.Text = "";
        add_min_park_time.Text = "";
        add_define_date.Text = DateTime.Now.ToString("yyyy-MM-dd") ;
        add_erp_code.Text = "";
        add_is_put_jar.Value = false;
        add_is_equal_material.Value = false;
        add_is_quality_rate_count.Value = false;
        add_rub_sect.Value = "";
        add_product_material_code.Value = "";
        //add_static_class.Value = "";
        add_remark.Text = "";
        //btnAddSave.Disable(false);
        this.winAdd.Show();
    }

    /// <summary>
    /// 根据原料、小料、胶料的类别不同，对增加框输入项的初始化
    /// author：yuany
    /// </summary>
    /// <param name="minor_type_id"></param>
    public void BtnAddFunction(string major_type_id , string minor_type_id)
    {
      //所有输入项显示
      add_rub_sect.Hidden = false;
      //add_product_material_code.Hidden = false;
      add_rub_sect.AllowBlank = true;
      add_product_material_code.AllowBlank = true;
      add_rub_code.Hidden = false;
      if (major_type_id == "1")
      {
          add_rub_code.Hidden = true;
          add_valid_date.ReadOnly = false;
          add_max_park_time.ReadOnly = false;
          add_max_park_time.AllowBlank = true;
          add_max_park_time.EmptyText = "";
          FieldSet1.Title = "原材料基本信息";
          add_max_park_time.IndicatorText = "天";
          add_min_park_time.IndicatorText = "天";
          add_rub_sect.Hidden = true;
          add_product_material_code.Hidden = true;
          add_rub_sect.AllowBlank = true;
          add_product_material_code.AllowBlank = true;
          add_material_name.Text = "";
      }
      else if (major_type_id == "2")
      {
          add_max_park_time.ReadOnly = false;
          add_valid_date.ReadOnly = true;
          add_max_park_time.AllowBlank = false;
          add_max_park_time.EmptyText = "必填";
          FieldSet1.Title = "小料基本信息";
          add_max_park_time.IndicatorText = "小时";
          add_min_park_time.IndicatorText = "小时";
          //add_static_class.Hidden = true;
          //add_static_class.AllowBlank = true;
          //hidden_material_part1.Value = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MinorTypeID == minor_type_id
          //                                                          && BasMaterialMinorType._.MajorID == major_type_id)[0].Remark;
      }
      else
      {
          add_max_park_time.ReadOnly = false;
          add_valid_date.ReadOnly = true;
          add_max_park_time.AllowBlank = false;
          add_max_park_time.EmptyText = "必填";
          FieldSet1.Title = "混(终)炼胶基本信息";
          add_max_park_time.IndicatorText = "小时";
          add_min_park_time.IndicatorText = "小时";
          add_rub_sect.Hidden = true;
          add_product_material_code.Hidden = true;
          add_rub_sect.AllowBlank = true;
          add_product_material_code.AllowBlank = true;
          //hidden_material_part1.Value = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MinorTypeID == minor_type_id
          //                                                              && BasMaterialMinorType._.MajorID == major_type_id)[0].Remark;
      }
    }
    /// <summary>
    /// 根据原料、小料、胶料的类别不同，对修改框输入项的初始化
    /// author：yuany
    /// </summary>
    /// <param name="minor_type_id"></param>
    public void BtnModifyFunction(string major_type_id ,string minor_type_id)
    {
        //所有输入项显示
        modify_mix_type.Hidden = true;
        modify_rub_code.Hidden = false;
        modify_rub_sect.Hidden = false;
        //modify_product_material_code.Hidden = false;
        modify_mix_type.AllowBlank = true;
        modify_rub_code.AllowBlank = true;
        modify_rub_sect.AllowBlank = true;
        modify_product_material_code.AllowBlank = true;
        modify_rub_sect.Hidden = false;
        if (major_type_id == "1")
        {
            modify_rub_sect.Hidden = true;
            add_valid_date.ReadOnly = false;
            add_max_park_time.ReadOnly = true;
            add_max_park_time.AllowBlank = true;
            add_max_park_time.EmptyText = "";
            FieldSet1.Title = "原材料基本信息";
            modify_max_park_time.IndicatorText = "小时";
            modify_min_park_time.IndicatorText = "小时";
            modify_mix_type.Hidden = true;
            modify_rub_code.Hidden = true;
            modify_rub_sect.Hidden = true;
            modify_product_material_code.Hidden = true;
            modify_mix_type.AllowBlank = true;
            modify_rub_code.AllowBlank = true;
            modify_rub_sect.AllowBlank = true;
            modify_product_material_code.AllowBlank = true;
            modify_material_name.Text = "";
        }
        else if (major_type_id == "2")
        {
            add_max_park_time.ReadOnly = false;
            add_valid_date.ReadOnly = true;
            add_max_park_time.AllowBlank = false;
            modify_rub_sect.Hidden = true;
            add_max_park_time.EmptyText = "必填";
            FieldSet1.Title = "小料基本信息";
            modify_max_park_time.IndicatorText = "小时";
            modify_min_park_time.IndicatorText = "小时";
            //hidden_material_part1.Value = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MinorTypeID == minor_type_id
            //                                                            && BasMaterialMinorType._.MajorID == major_type_id)[0].Remark;
        }
        else
        {
            add_max_park_time.ReadOnly = false;
            add_valid_date.ReadOnly = true;
            add_max_park_time.AllowBlank = false;
            add_max_park_time.EmptyText = "必填";
            FieldSet1.Title = "混(终)炼胶基本信息";
            modify_max_park_time.IndicatorText = "小时";
            modify_min_park_time.IndicatorText = "小时";
            modify_mix_type.Hidden = true;
            modify_rub_sect.Hidden = true;
            modify_product_material_code.Hidden = true;
            modify_mix_type.AllowBlank = true;
            modify_rub_sect.AllowBlank = true;
            modify_product_material_code.AllowBlank = true;
            //hidden_material_part1.Value = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MinorTypeID == minor_type_id
            //                                                            && BasMaterialMinorType._.MajorID == major_type_id)[0].Remark;

        }
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string MaterialCode)
    {
       


        string sql = "select * from BasMaterial where MaterialCode = '"+ MaterialCode + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        //X.Msg.Notify(ds.Tables[0].Rows[0]["MajorTypeID"].ToString(), "").Show();

        BtnModifyFunction(ds.Tables[0].Rows[0]["MajorTypeID"].ToString(), ds.Tables[0].Rows[0]["MinorTypeID"].ToString());
        modify_obj_id.Value = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
        modify_material_code.Value = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
        hidden_minor_type_id.Value = ds.Tables[0].Rows[0]["MinorTypeID"].ToString();
        hidden_rub_code.Value = ds.Tables[0].Rows[0]["RubCode"].ToString();
        modify_material_name.Value = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        hidden_material_name.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        modify_material_other_name.Value = ds.Tables[0].Rows[0]["MaterialOtherName"].ToString();
        //modify_valid_date.Value = ds.Tables[0].Rows[0]["ValidDate"].ToString(); 
        modify_pruduct_area.Value = ds.Tables[0].Rows[0]["ProductArea"].ToString(); 
        modify_min_stock.Value = ds.Tables[0].Rows[0]["MinStock"].ToString();
        modify_max_stock.Value = ds.Tables[0].Rows[0]["MaxStock"].ToString();
        
        TextXB.Value = ds.Tables[0].Rows[0]["XBStock"].ToString();
        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UnitID"].ToString()))
        {
            modify_unit_id.Value = ds.Tables[0].Rows[0]["UnitID"].ToString();
            hidden_unit_id.Value = ds.Tables[0].Rows[0]["UnitID"].ToString();
        }
        if (  String.IsNullOrEmpty(ds.Tables[0].Rows[0]["PlanPrice"].ToString()) ) modify_JG.Text = "0";
        else
        modify_JG.Text = ds.Tables[0].Rows[0]["PlanPrice"].ToString();
        modify_k3.Text = ds.Tables[0].Rows[0]["UserCode"].ToString();
        modify_max_park_time.Value = ds.Tables[0].Rows[0]["MaxParkTime"].ToString();
        modify_min_park_time.Value = ds.Tables[0].Rows[0]["MinParkTime"].ToString();
        modify_define_date.Value = ds.Tables[0].Rows[0]["DefineDate"].ToString();
        modify_erp_code.Value = ds.Tables[0].Rows[0]["ERPCode"].ToString();
        modify_pdm_code.Value = ds.Tables[0].Rows[0]["SAPMaterialCode"].ToString();
        modify_is_equal_material.Value = ds.Tables[0].Rows[0]["IsEqualMaterial"].ToString();
        modify_is_put_jar.Value = ds.Tables[0].Rows[0]["IsPutJar"].ToString();
        modify_is_quality_rate_count.Value = ds.Tables[0].Rows[0]["IsQualityRateCount"].ToString();

        modify_weight.Value = ds.Tables[0].Rows[0]["StaticUnitCoefficient"].ToString();
        modify_remark.Value = ds.Tables[0].Rows[0]["Remark"].ToString();
        modify_product_material_code.Select(0);
        modify_major_type_id.Value = majorTypeManager.GetById(ds.Tables[0].Rows[0]["MajorTypeID"].ToString()).MajorTypeName;
        //sql = "select * from BasMaterialMajorType where objid = '" + ds.Tables[0].Rows[0]["MajorTypeID"].ToString() +"'";
        // ds = manager.GetBySql(sql).ToDataSet();
        hidden_rub_code.Value =ds.Tables[0].Rows[0]["RubCode"].ToString() ;
        if (hidden_rub_code.Value != null && hidden_rub_code.Value.ToString().Trim() != "")
        { 
            sql = "select * from BasRubInfo where objid ='"+ds.Tables[0].Rows[0]["RubCode"].ToString()+"'";
     DataSet dr = manager.GetBySql(sql).ToDataSet();
              modify_rub_code.Value = dr.Tables[0].Rows[0]["RubName"].ToString();
         
        
        }
         sql = "select * from BasMaterialMinorType where MinorTypeid = '" + ds.Tables[0].Rows[0]["MinorTypeID"].ToString() + "' and majorid ='"+ds.Tables[0].Rows[0]["MajorTypeID"].ToString()+"'";
         ds = manager.GetBySql(sql).ToDataSet();
         modify_minor_type_id.Text = ds.Tables[0].Rows[0]["MinorTypeName"].ToString();

         
  
        this.winModify.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
         
            string sql = "select * from Pmt_weight where Recipe_Code ='" + obj_id + "' or Mater_Code ='" + obj_id + "' ";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            { return "删除失败 物料已被配方使用"; }
             sql = "delete Pmt_material where Mater_code='" + obj_id + "'";
             ds= manager.GetBySql(sql).ToDataSet();
      
            this.AppendWebLog("物料信息删除", "物料代码：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
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
            BasMaterial materialInfo = manager.GetById(obj_id);
            EntityArrayList<BasMaterialMinorType> minorTypeList = minorTypeManager.GetListByWhere(
                BasMaterialMinorType._.MinorTypeID == materialInfo.MinorTypeID &&
                BasMaterialMinorType._.MajorID == materialInfo.MajorTypeID && 
                BasMaterialMinorType._.DeleteFlag == "1");
            if (minorTypeList.Count > 0)
            {
                return "恢复失败：请先恢复物料细类[" + minorTypeList[0].MinorTypeName + "]";
            }
            BasMaterialStaticClass staticClass = staticClassManager.GetById(materialInfo.StaticClass);
            if (staticClass != null && staticClass.DeleteFlag.Equals("1"))
            {
                return "恢复失败：请先恢复统计分类[" + staticClass.StaticClassName + "]";
            }
            materialInfo.DeleteFlag = "0";
            manager.Update(materialInfo);
            this.AppendWebLog("物料信息恢复", "物料代码：" + materialInfo.MaterialCode);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
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
        this.winMaterialGroup.Close();
        hidden_material_group_code.Value = "";
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        string minorTypeID = hidden_minor_type_id.Value.ToString().PadLeft(2, '0');
        try
        {
            //添加校验重复
            if (String.IsNullOrEmpty(add_material_name.Text))
            {
                X.Msg.Alert("提示", "此物料名称不能为空！").Show();
                return;
            }
            // if (String.IsNullOrEmpty(add_min_park_time.Text))
            //{
            //    X.Msg.Alert("提示", "最短停放时间必填！").Show();
            //    return;
            //}

            try
            {
                EntityArrayList<BasMaterial> materialList = manager.GetListByWhere(BasMaterial._.MaterialName == add_material_name.Text.TrimStart().TrimEnd());
                if (materialList.Count > 0)
                {
                    X.Msg.Alert("提示", "此物料名称已被使用！").Show();
                    return;
                }
            }
            catch(Exception ex)
                {  X.Msg.Alert("提示", "此物料名称已被使用！").Show();
                    return;}
          
            BasMaterial material = new BasMaterial();
            string nextRubInfoCode = manager.GetNextMaterialCode(hidden_major_type_id.Text, minorTypeID, hidden_rub_code.Text, true);

            string MaterialCode = nextRubInfoCode;
            int MajorTypeID = hidden_major_type_id.Text == "" ? 0 : Convert.ToInt32(hidden_major_type_id.Text);
            string MinorTypeID = minorTypeID;

            string MaterialName = add_material_name.Text;
            string K3Code = add_user_code.Text;
            string simple_name = add_material_simple_name.Text;
            int price = add_price_code.Text == "" ? 0 : Convert.ToInt32(add_price_code.Text);
            int maxParking= add_max_park_time.Text == "" ? 0 : Convert.ToInt32(add_max_park_time.Text);
            int minParking = add_min_park_time.Text == "" ? 0 : Convert.ToInt32(add_min_park_time.Text);
            string otherName = add_material_other_name.Text;

            string sql = @"insert into Pmt_material(Mater_code,Mkind_code,Ikind_code,Mater_name,dis_name,Plan_price,Unit_name,Max_time,Limit_time,Define_date,Mater_byname,least_flag,user_code,stand_code,rubkind,rubname,Country_cd,Sap_Code,PDM_Code)
                          values('" + MaterialCode + @"', '" + MajorTypeID + "', '" + MinorTypeID + @"','" + MaterialName + @"', '" + simple_name + @"', '" + price + @"', '" + add_unit_id.Text + @"', '" + maxParking + @"', '" + minParking + @"',convert(char(10),GETDATE(),121), '" + otherName + "', '1', '" + K3Code + @"', '', '', '', '', '" + add_erp_code.Text + @"', '" + text_pdm_code.Text + @"')";

            //add_material_name.Text = sql;
            //return;
            
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("物料信息添加", "物料代码：" + material.MaterialCode);
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

    protected bool GetFromSap(string sapcode)
    {
        EntityArrayList<BasMaterial> materialList = manager.GetListByWhere(BasMaterial._.ERPCode==sapcode);
        if (materialList.Count > 0)
            return true;
        else
            return false;
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

      
            double maxPark= modify_max_park_time.Text == "" ? 0 : Convert.ToDouble(modify_max_park_time.Text);
            double minPark = modify_min_park_time.Text == "" ? 0 : Convert.ToDouble(modify_min_park_time.Text);

            double maxStock = modify_max_stock.Text == "" ? 0 : Convert.ToDouble(modify_max_stock.Text);
            double minStock = modify_min_stock.Text == "" ? 0 : Convert.ToDouble(modify_min_stock.Text);
            decimal weight = 0;
            if (string.IsNullOrEmpty(modify_weight.Text))
            { weight = 0; }
            else { weight = Convert.ToDecimal(modify_weight.Text); }
            string sql = "update Pmt_material set Plan_price = '" + modify_JG.Text+ "' ,user_code = '"+ modify_k3.Text +"', Max_time='" + modify_max_park_time.Text + "',Limit_time='" + minPark + "',Mater_byname='" + modify_material_other_name.Text + "',Mater_name='"
                + modify_material_name.Text + "',SAP_Code='" + modify_erp_code.Text + "',PDM_Code='" + modify_pdm_code.Text + "' ,Mem_note = '" + modify_remark.Text + "',Statc_unit_coef = '" + weight + "',Min_Stock ='" + minStock + "', Max_stock ='" + maxStock + "' , rubkind ='" + hidden_rub_code.Text + "' , rubname = '" + modify_rub_code.Value + "' where Mater_code='" + modify_material_code.Text + "'";

            //modify_material_name.Text = sql;
            //return;
            
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("物料信息修改", "物料代码：" + modify_material_code.Text);

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

    #region 设置分组
    [Ext.Net.DirectMethod()]
    protected void btn_group_Click(object sender, DirectEventArgs e)
    {
        hiddenCgroup.Text = "0";
        bool flag = true;
        group_material_group.ClearValue();
        string json = e.ExtraParams["Values"];
        hidden_material_group.Value = json;
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterial> materialList = new List<BasMaterial>();
        string mname = "";
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = new BasMaterial();
            material.MaterialName = row["MaterialName"];
            material.MaterialCode = row["MaterialCode"];
            material.MaterialGroup = row["MaterialGroup"];
            material.MaterialLevel = row["MaterialLevel"];
            material.MaterialSimpleName = row["MaterialGroupID"];
            if (string.IsNullOrEmpty(material.MaterialLevel))
            {
                mname = material.MaterialName;
                flag = false;
            }
            string materialcode = row["MaterialCode"];
            string materialname = row["MaterialName"];
            data.Add(new { MaterialName = materialname + "-" + materialcode, MaterialCode = materialcode });
            materialList.Add(material);
        }
        if (flag)
        {
            groupStore.Data = materialList;
            groupStore.DataBind();

            group_material_group_store.Data = data;
            group_material_group_store.DataBind();
            this.winMaterialGroup.Show();
        }
        else
        {
            this.msg.Alert("提示", mname + "物料等级不存在，请先修改物料等级！").Show();
        }
    }
    
    [Ext.Net.DirectMethod()]
    protected void btn_group_Click2(object sender, DirectEventArgs e)
    {
        hiddenCgroup.Text = "1";
        bool flag = true;
        group_material_group.ClearValue();
        string json = e.ExtraParams["Values"];
        hidden_material_group.Value = json;
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterial> materialList = new List<BasMaterial>();
        string mname = "";
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = new BasMaterial();
            material.MaterialName = row["MaterialName"];
            material.MaterialCode = row["MaterialCode"];
            material.MaterialGroup = row["CMaterialGroup"];
            material.MaterialLevel = row["CMaterialLevel"];
            material.MaterialSimpleName = row["CMaterialGroupID"];
            if (string.IsNullOrEmpty(material.MaterialLevel))
            {
                mname = material.MaterialName;
                flag = false;
            }
            string materialcode = row["MaterialCode"];
            string materialname = row["MaterialName"];
            data.Add(new { MaterialName = materialname + "-" + materialcode, MaterialCode = materialcode });
            materialList.Add(material);
        }
        if (1==1)//flag
        {
            groupStore.Data = materialList;
            groupStore.DataBind();

            group_material_group_store.Data = data;
            group_material_group_store.DataBind();
            this.winMaterialGroup.Show();
        }
        else
        {
            this.msg.Alert("提示", mname + "物料等级不存在，请先修改物料等级！").Show();
        }
    }
    //在分组中添加修改物料等级功能，赵营 2013-08-07 添加
    public void BtnGroupSave_Click(object sender, DirectEventArgs e)
    {
        string json = hidden_material_group.Value.ToString();
        if (hidden_material_group_code.Value == "")
        {
            this.msg.Alert("错误", "物料分组值不能为空!").Show();
            return;
        }
        Regex rgx = new Regex(@"^\d{13}$");
        if (hidden_material_group_code.Value.ToString().Length != 13 || !rgx.IsMatch(hidden_material_group_code.Value.ToString()))
        {
            this.msg.Alert("错误", "请选择有效物料分组值!").Show();
            return;
        }

        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);

        if (hiddenCgroup.Text == "1")
        {
            foreach (Dictionary<string, string> row in materialDic)
            {
                BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == row["MaterialCode"])[0];
                material.CMaterialGroup = hidden_material_group_code.Value.ToString();
                if (string.IsNullOrEmpty(material.CMaterialLevel))
                { material.CMaterialLevel = "0"; }

                manager.Update(material);
            }
            //修改物料等级
            ChangeRecords<BasMaterial> basMaterial = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<BasMaterial>();
            for (int i = 0; i < basMaterial.Updated.Count; i++)
            {
                BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == basMaterial.Updated[i].MaterialCode)[0];
                //if (String.IsNullOrEmpty(basMaterial.Created[i].MaterialLevel))
                //    material.CMaterialLevel = "0";
                //else
                    material.CMaterialLevel = basMaterial.Updated[i].MaterialLevel;
                manager.Update(material);
            }
        }
        else {

        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == row["MaterialCode"])[0];
            material.MaterialGroup = hidden_material_group_code.Value.ToString();
            manager.Update(material);
        }
        //修改物料等级
        ChangeRecords<BasMaterial> basMaterial = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<BasMaterial>();
        for (int i = 0; i < basMaterial.Updated.Count; i++)
        {
            BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == basMaterial.Updated[i].MaterialCode)[0];
            material.MaterialLevel = basMaterial.Updated[i].MaterialLevel;
            manager.Update(material);
        }
    }
        hidden_material_group.Value = "";
        this.winMaterialGroup.Close();
        this.pageToolBar.DoRefresh();
        hidden_material_group_code.Value = "";
        this.msg.Notify("提示", "设置物料分组值成功!").Show();
    }

    public void BtnGroupClear_Click(object sender, DirectEventArgs e)
    {
        string json = hidden_material_group.Value.ToString();

        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == row["MaterialCode"])[0];

            if (hiddenCgroup.Text =="1")
                material.CMaterialGroup = "";
            else 
            material.MaterialGroup = "";

            manager.Update(material);
        }
        hidden_material_group.Value = "";
        this.winMaterialGroup.Close();
        this.pageToolBar.DoRefresh();
        hidden_material_group_code.Value = "";
        this.msg.Notify("提示", "清除物料分组值成功!").Show();
    }

    [Ext.Net.DirectMethod()]
    public void MaterialGroupData_DbClick(string MaterialName, string MaterialCode)
    { 
        List<object> data = new List<object>();
        data.Add(new { MaterialName = MaterialName + "-" + MaterialCode, MaterialCode = MaterialCode });
        group_material_group_store.Data = data;
        group_material_group_store.DataBind();
        group_material_group.Select(MaterialName + "-" + MaterialCode);
        hidden_material_group_code.Value = MaterialCode;
        group_material_group.ShowTrigger(0);
    }

    protected void MaterialGroupStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        EntityArrayList<BasMaterial> materialList = GetMaterialNameAndCodeBySearchKey(20, hidden_major_type_id.Value.ToString(), group_material_group.Text);
        this.group_material_group_store.DataSource = materialList;
        this.group_material_group_store.DataBind();
    }

    public EntityArrayList<BasMaterial> GetMaterialNameAndCodeBySearchKey(int top, string majorId, string searchKey)
    {
        EntityArrayList<BasMaterial> materialList = new EntityArrayList<BasMaterial>();
        string whereStr = "";
        switch (majorId)
        {
            case "": whereStr = "";
                break;
            case "1": whereStr = " MajorTypeID = 1 ";
                break;
            case "2": whereStr = " MajorTypeID = 2 ";
                break;
            default: whereStr = " MajorTypeID > 2 ";
                break;
        }
        string sqlstr = "";
        sqlstr = @" SELECT  DISTINCT    TOP {0} MaterialCode , MaterialName  ,len (MaterialName)
                        FROM    BasMaterial 
                        WHERE   [dbo].[FuncSysGetPY](MaterialName) like '%{1}%' ";
        if (whereStr != "")
        {
            sqlstr += @" AND " + whereStr;
        }
        sqlstr += "   ORDER BY  len (MaterialName),MaterialCode ";
        sqlstr = String.Format(sqlstr, top, searchKey);
        DataSet ds = manager.GetBySql(sqlstr.ToString()).ToDataSet();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            BasMaterial material = new BasMaterial();
            material.MaterialCode = row["MaterialCode"].ToString();
            material.MaterialName = row["MaterialName"].ToString() + "-" + row["MaterialCode"].ToString();
            materialList.Add(material);
        }
        return materialList;
    }
    #endregion

    #region 设置物料库存停放时间
    [Ext.Net.DirectMethod()]
    protected void btn_set_stock_Click(object sender, DirectEventArgs e)
    {
        set_max_stock.Text = "0";
        cb_set_max_stock.Checked = true;
        set_min_stock.Text = "0";
        cb_set_min_stock.Checked = true;
        set_max_part_time.Text = "0";
        set_max_part_time.Disabled = true;
        cb_set_max_part_time.Checked = false;
        set_min_part_time.Text = "0";
        set_min_part_time.Disabled = true;
        cb_set_min_part_time.Checked = false;
        set_is_quality_rate_count.Value = false;
        cb_set_is_quality_rate_count.Checked = true;
        string json = e.ExtraParams["StockValues"];
        hidden_set_Stock.Value = json;
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterial> materialList = new List<BasMaterial>();
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = new BasMaterial();
            material.MaterialName = row["MaterialName"];
            material.MaterialCode = row["MaterialCode"];
            material.MaxStock = Convert.ToDecimal(row["MaxStock"]);
            material.MinStock = Convert.ToDecimal(row["MinStock"]);
            material.MaxParkTime = Convert.ToDecimal(row["MaxParkTime"]);
            material.MinParkTime = Convert.ToDecimal(row["MinParkTime"]);
            material.IsQualityRateCount = row["IsQualityRateCount"];
            materialList.Add(material);
        }
        stockStore.Data = materialList;
        stockStore.DataBind();
        this.winSetStock.Show();
    }
    /// <summary>
    /// 最大最小停放时间修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnSetStockSave_Click(object sender, DirectEventArgs e)
    {
        string json = hidden_set_Stock.Value.ToString();
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == row["MaterialCode"])[0];
            if (cb_set_max_stock.Checked == true)
            {
                material.MaxStock = Convert.ToDecimal(set_max_stock.Text);
            }
            if (cb_set_min_stock.Checked == true)
            {
                material.MinStock = Convert.ToDecimal(set_min_stock.Text);
            }
            if (cb_set_max_part_time.Checked == true)
            {
                material.MaxParkTime = Convert.ToDecimal(set_max_part_time.Text);
            }
            if (cb_set_min_part_time.Checked == true)
            {
                material.MinParkTime = Convert.ToDecimal(set_min_part_time.Text);
            }
            if (cb_set_is_quality_rate_count.Checked == true)
            {
                material.IsQualityRateCount = set_is_quality_rate_count.Value.ToString().Equals("True") ? "1" : "0";
            }
            manager.Update(material);
            this.AppendWebLog("物料信息停放时间修改", "物料代码：" + material.MaterialCode);
        }
        hidden_set_Stock.Value = "";
        this.winSetStock.Close();
        this.pageToolBar.DoRefresh();
        msg.Notify("提示", "批量修改完成!").Show();
    }

    public void BtnSetStockCancel_Click(object sender, DirectEventArgs e)
    {
        hidden_set_Stock.Value = "";
        this.winSetStock.Close();
    }

    #endregion

    #region 校验方法
    /// <summary>
    /// 检查胶料名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckMaterialName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string materialName = field.Text;
        EntityArrayList<BasMaterial> rubInfoList = manager.GetListByWhere(BasMaterial._.MaterialName == materialName);
        if (rubInfoList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (rubInfoList[0].MaterialName.ToString() == hidden_material_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此物料名称已被使用！";
            }
        }
    }
    /// <summary>
    /// 检查胶料段数和胶料名称是否选择，筛选出产生物料填充下拉框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CheckFulfillProductMaterial(object sender, DirectEventArgs e)
    {
        return;
        string field = ((TextField)sender).ID;
        //说明是添加功能
        if (field.IndexOf("add") != -1)
        {
            string rub_sect = add_rub_sect.Value + "";
            string rub_sectValue = hidden_rub_sect.Value + "";
            //string rub_code = add_rub_code.Value + "";
            string rub_codeValue = hidden_rub_code.Value + "";
            add_product_material_code.ClearValue();
            //if (rub_code != "" && rub_sect != "")
            //{
            //    List<object> data = new List<object>();
            //    EntityArrayList<BasMaterial> productMaterialList = manager.GetListByWhere(BasMaterial._.MaterialCode.Like(rub_sectValue + "%") && BasMaterial._.MaterialCode.Like("%" + rub_codeValue));
            //    foreach (BasMaterial product in productMaterialList)
            //    {
            //        data.Add(new { text = product.MaterialName, value = product.MaterialCode });
            //    }
            //    add_product_material_store.Data = data;
            //    add_product_material_store.DataBind();
            //}
            //else
            //{
            //    List<object> data = new List<object>();
            //    add_product_material_store.Data = data;
            //    add_product_material_store.DataBind();
            //    if (data.Count > 0)
            //    {
            //        add_product_material_code.Select(0);
            //    }
            //}
        }
        else
        {
            string rub_sect = modify_rub_sect.Value + "";
            string rub_sectValue = hidden_rub_sect.Value + "";
            string rub_code = modify_rub_code.Value + "";
            string rub_codeValue = hidden_rub_code.Value + "";
            if (rub_code != "" && rub_sect != "")
            {
                modify_product_material_code.ClearValue();
                List<object> data = new List<object>();
                EntityArrayList<BasMaterial> productMaterialList = manager.GetListByWhere(BasMaterial._.MaterialCode.Like(rub_sectValue + "%") && BasMaterial._.MaterialCode.Like("%" + rub_codeValue));
                foreach (BasMaterial product in productMaterialList)
                {
                    data.Add(new { text = product.MaterialName, value = product.MaterialCode });
                }
                modify_product_material_store.Data = data;
                modify_product_material_store.DataBind();
                if (data.Count > 0)
                {
                    modify_product_material_code.Select(0);
                }
            }
        }
    }

    public void ProductMaterialBlur(object sender, DirectEventArgs e)
    {
        if (hidden_major_type_id.Value.ToString() != "2")//如果不是小料则不需生产物料相关操作
        {
            return;
        }
        string field = ((Ext.Net.ComboBox)sender).ID;
        if (field.IndexOf("add") != -1)
        {
            hidden_material_part3.Value = add_product_material_code.SelectedItem.Text + "-";
            add_material_name.Value = "" + hidden_material_part3.Value + hidden_material_part2.Value + hidden_material_part1.Value;
        }
        else
        {
            hidden_material_part3.Value = modify_product_material_code.SelectedItem.Text + "-";
            modify_material_name.Value = "" + hidden_material_part3.Value + hidden_material_part2.Value + hidden_material_part1.Value;
        }
    }

    public void ChangeMixType(object sender, DirectEventArgs e)
    {
        if (hidden_major_type_id.Value.ToString() != "2")//如果不是小料则不需机台选择相关操作
        {
            return;
        }
        string field = ((Ext.Net.ComboBox)sender).ID;
        if (field.IndexOf("add") != -1)
        {
            //hidden_material_part2.Value = add_mix_type.SelectedItem.Text + "-";
            add_material_name.Value = "" + hidden_material_part3.Value + hidden_material_part2.Value + hidden_material_part1.Value;
        }
        else
        {
            hidden_material_part2.Value = modify_mix_type.SelectedItem.Text + "-";
            modify_material_name.Value = "" + hidden_material_part3.Value + hidden_material_part2.Value + hidden_material_part1.Value;
        }
    }
    #endregion
}