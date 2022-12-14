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


public partial class Manager_Technology_Manage_MaterialToRecipe : System.Web.UI.Page
{
    protected BasWorkManager manager = new BasWorkManager();//业务对象
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
            IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
            EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
                BasMaterialMinorType._.DeleteFlag == "0"
                & BasMaterialMinorType._.MajorID == 1
                , BasMaterialMinorType._.ObjID.Asc);
            foreach (BasMaterialMinorType mBasMaterialMinorType in mBasMaterialMinorTypeList)
            {
                ComboBoxNorthMaterMinorType.AddItem(mBasMaterialMinorType.MinorTypeName, mBasMaterialMinorType.MinorTypeID);
            }
        }
    }
    protected void ComboBoxNorthMaterMinorType_Change(object sender, DirectEventArgs e)
    {
        ComboBoxNorthMater.GetStore().RemoveAll();
      

        ComboBoxNorthMater.SetValue("");
       

        string minorTypeId = ComboBoxNorthMaterMinorType.Value.ToString();
        if (minorTypeId == "")
        {
            return;
        }

        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.DeleteFlag == "0"
            & BasMaterial._.MajorTypeID == 1
            & BasMaterial._.MinorTypeID == minorTypeId
            , BasMaterial._.MaterialName.Asc);
        foreach (BasMaterial mBasMaterial in mBasMaterialList)
        {
            ComboBoxNorthMater.AddItem(mBasMaterial.MaterialName, mBasMaterial.MaterialCode);
        }

  
    }

    #endregion

    #region 分页相关方法
    

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //X.Js.Alert(ComboBoxNorthMater.SelectedItem.Value.ToString()); return null;
        try
        {
            if (String.IsNullOrEmpty(ComboBoxNorthMater.SelectedItem.Value.ToString())) return null;
        }
        catch { return null; }
        DataTable data = manager.GetBySql(@"select * from dbo.PmtRecipe t1
left join PmtRecipeWeight t2 on t1.objid = t2.recipeobjid
left join  basequip on t1.recipeequipcode =basequip.equipcode
where materialcode = '"+ComboBoxNorthMater.SelectedItem.Value.ToString()+@"'
and deleteflag = '0'
order by t1.recipeequipcode").ToDataSet().Tables[0];

        int total = data.Rows.Count;
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
        //if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        //{
        //    txt_obj_id.Text = "";
        //}
        //PageResult<BasWork> pageParams = new PageResult<BasWork>();
        //pageParams.PageSize = -100;
        //PageResult<BasWork> lst = GetPageResultData(pageParams);

        DataSet data = manager.GetBySql(@"select * from dbo.PmtRecipe t1
left join PmtRecipeWeight t2 on t1.objid = t2.recipeobjid
left join  basequip on t1.recipeequipcode =basequip.equipcode
where materialcode = '" + ComboBoxNorthMater.SelectedItem.Value.ToString() + @"'
and deleteflag = '0'
order by t1.recipeequipcode").ToDataSet();
        for (int i = 0; i < data.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = data.Tables[0].Columns[i];
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
                data.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(data, "原材料追溯配方");
    }
    #endregion

 

  
}