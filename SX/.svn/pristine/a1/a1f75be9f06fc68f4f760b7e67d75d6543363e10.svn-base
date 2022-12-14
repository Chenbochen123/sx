using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;

public partial class Manager_Technology_Manage_RecipeMixReport2 : Mesnac.Web.UI.Page
{
    protected EQM_EnergyManageManager manager = new EQM_EnergyManageManager();
    protected JCZL_SubFacManager facManager = new JCZL_SubFacManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Pmt_materialManager materialManager = new Pmt_materialManager();
    protected Pmt_recipetypeManager recipeManager = new Pmt_recipetypeManager();
    protected Ppt_ShiftClassManager ClassManager = new Ppt_ShiftClassManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            dStartDate.SetValue(DateTime.Now);
            dEndDate.SetValue(DateTime.Now.AddDays(1));
            bindPeifang();
            bindClass();
            bindMater();
            bindEquip();

        }
    }


    #region 初始化控件

    private void bindPeifang()
    {
        cbxPeifang.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_recipetype> list = recipeManager.GetListByWhereAndOrder(where, Pmt_recipetype._.Recipe_type.Asc);
        foreach (Pmt_recipetype type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Recipe_typename, type.Recipe_type);
            cbxPeifang.Items.Add(item);
        }
    }

    private void bindMater()
    {
        cbxMater.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_material> list = materialManager.GetListByWhereAndOrder(Pmt_material._.Mkind_code >=3, Pmt_material._.Mater_name.Asc);
        foreach (Pmt_material type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Mater_name, type.Mater_code);
            cbxMater.Items.Add(item);
        }
    }
    private void bindClass()
    {
        cbxClass.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Ppt_ShiftClass> list = ClassManager.GetListByWhereAndOrder(where, Ppt_ShiftClass._.Shift_ClassId.Asc);
        foreach (Ppt_ShiftClass type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Shift_ClassName, type.Shift_ClassId);
            cbxClass.Items.Add(item);
        }
    }
    private void bindEquip()
    {
        cbxEquip.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(Pmt_equip._.Equip_class=="01", Pmt_equip._.Equip_code.Asc);
        foreach (Pmt_equip type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Equip_name, type.Equip_code);
            cbxEquip.Items.Add(item);
        }
    }

    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }


    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        string equip = "";
        if (cbxEquip.SelectedItem.Value != null)
        {
            equip = cbxEquip.SelectedItem.Value.ToString();
        }
    //    else { X.Msg.Alert("提示", "没有找到符合条件的记录").Show(); }
        string material = "";
        if (cbxMater.SelectedItem.Value != null)
        {
            material = cbxMater.SelectedItem.Value.ToString();
        }
        string Peifang = "";
        if (cbxPeifang.SelectedItem.Value != null)
        {
            Peifang = cbxPeifang.SelectedItem.Value.ToString();
        }
        string Class = "";
        if (cbxClass.SelectedItem.Value != null)
        {
            Class = cbxClass.SelectedItem.Value.ToString();
        }

        sb.AppendLine(@"exec proc_RecipeMixReport2 " + "'" + dStartDate.SelectedDate.ToString("yyyy-MM-dd") + "'," + "'" + dEndDate.SelectedDate.ToString("yyyy-MM-dd") + "'," + "'" + equip + "'," + "'" + Peifang + "'," + "'" + material  + "'," + "'" + Class + "'");
        #endregion
        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {

        this.store.DataSource = getList();
        this.store.DataBind();

    }




    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    //protected void btnExportSubmit_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = getList();
    //    //huiw,2013-10-28添加，判断不为空时才导出excel
    //    if (ds == null || ds.Tables[0].Rows.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未查询出数据！").Show();
    //    }
    //    else
    //    {
    //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
    //        {
    //            bool isshow = false;
    //            DataColumn dc = ds.Tables[0].Columns[i];
    //            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
    //            {
    //                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
    //                {
    //                    dc.ColumnName = cb.Text;
    //                    isshow = true;
    //                    break;
    //                }
    //            }
    //            if (!isshow)
    //            {
    //                ds.Tables[0].Columns.Remove(dc.ColumnName);
    //                i--;
    //            }
    //        }
    //        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "工艺步骤数据导出");
    //    }
    //}

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataTable qualifyDt = getList().Tables[0];

        DataSet ds = new DataSet();
        qualifyDt.TableName = "工艺步骤报表导出";
        ds.Tables.Add(qualifyDt.Copy());


        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "工艺步骤报表导出");
    }


    #endregion


    #region 信息列表事件响应
    #endregion
}