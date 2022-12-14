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

public partial class Manager_Technology_Manage_TechAnalysis : Mesnac.Web.UI.Page
{
    protected EQM_EnergyManageManager manager = new EQM_EnergyManageManager();
    protected JCZL_SubFacManager facManager = new JCZL_SubFacManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Pmt_materialManager materialManager = new Pmt_materialManager();
    protected SysCodeManager codeManager = new SysCodeManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            cbxQueryType.Select(0);
            dStartDate.SetValue(DateTime.Now);
            dEndDate.SetValue(DateTime.Now.AddDays(1));
            bindEquip();
            bindMaterial();
            bindType();

        }
    }


    #region 初始化控件



    private void bindEquip()
    {
        cbxEquipQuery.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(where, Pmt_equip._.Equip_code.Asc);
        this.storeEquipQuery.DataSource = list;
        this.storeEquipQuery.DataBind();
    }
    private void bindMaterial()
    {
        cbxMaterialQuery.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_material> list = materialManager.GetListByWhereAndOrder(Pmt_material._.Mkind_code == 3 || Pmt_material._.Mkind_code == 4 || Pmt_material._.Mkind_code == 5, Pmt_material._.Mater_name.Asc);
        foreach (Pmt_material type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Mater_name, type.Mater_code);
            cbxMaterialQuery.Items.Add(item);
        }
    }  private void bindType()
    {
        cbxMaterialQuery.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<SysCode> list = codeManager.GetListByWhere(SysCode._.TypeID == "PmtType");
        foreach (SysCode type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.ItemName, type.ItemCode);
            cbxtype.Items.Add(item);
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
        string shiftClass = "";
        if (cbxShiftClass.SelectedItem.Value != null)
        {
            shiftClass = cbxShiftClass.SelectedItem.Value.ToString();
        }
        string material = "";
        if (cbxMaterialQuery.SelectedItem.Value != null)
        {
            material = cbxMaterialQuery.SelectedItem.Value.ToString();
        }
        string equip = "";
        if (cbxEquipQuery.SelectedItem.Value != null)
        {
            equip = cbxEquipQuery.SelectedItem.Value.ToString();
        }

        if (cbxQueryType.SelectedItem.Value.ToString() == "0")//加料时间
        {
            sb.AppendLine(@"select equip_name '机台',a.Mater_name '胶料名称',poly_distime '加胶时间',CB_distime '加炭黑时间',Oil_distime '加油时间',Done_AllRtime '每车时间',Bwb_Time '间隔时间',a.plan_date '生产日期',shift_ClassName '班组',Serial_BatchId '辊次',i.ItemName '配方类型'
            from ppt_lot a 
           left join pmt_equip b on a.equip_Code=b.equip_Code 
           left join ppt_shiftclass c on a.shift_Class=c.shift_Classid 
           left join ppt_plan d on a.plan_id=d.plan_id  
           left join pmt_recipetype e on d.recipe_type=e.recipe_type 
            left Join pmt_mstopkind f on a.mstopcode=f.mkind_COde 
            left join pmt_istopkind g on a.istopcode=g.ikind_Code 
			left join Ppt_Plan h on h.Plan_id=a.Plan_Id
			left join SysCode i on i.ItemCode=h.Recipe_Type and TypeID='PmtType'
            where left(a.equip_Code ,2)='01'");
            if (!string.IsNullOrEmpty(shiftClass))
            {
                sb.AppendLine(@"and a.shift_Class = " + "'" + shiftClass + "'");
            }
            if (!string.IsNullOrEmpty(material))
            {
                sb.AppendLine(@"and a.Mater_Code = " + "'" + material + "'");
            }
            if (!string.IsNullOrEmpty(equip))
            {
                sb.AppendLine(@"and a.equip_Code = " + "'" + equip + "'");
            }
            if (!string.IsNullOrEmpty(cbxtype.Text))
            {
                sb.AppendLine(@"and i.ItemCode = " + "'" + cbxtype.SelectedItem.Value + "'");
            }
            sb.AppendLine(@"and a.plan_date>= " + "'" + dStartDate.SelectedDate.ToString("yyyy-MM-dd") + "'and a.plan_date<= " + "'" + dEndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        else if (cbxQueryType.SelectedItem.Value.ToString() == "1")//换料时间
        {
            sb.AppendLine(@"select a.plan_date '生产日期',equip_name '机台',a.mater_name '胶料名称',c.shift_Classname '班次' ,change_time '换料时间',  nomal_time '正常时间',poly_time '加胶时间',i.ItemName '配方类型' 
            from ppt_plantime a left join pmt_equip b on a.equip_Code=b.equip_Code 
			left join Ppt_Plan h on h.Plan_id=a.Plan_Id
			left join SysCode i on i.ItemCode=h.Recipe_Type and TypeID='PmtType' 
            join ppt_shiftclass c on a.shift_Class=c.shift_Classid");
            if (!string.IsNullOrEmpty(shiftClass))
            {
                sb.AppendLine(@"and a.shift_Class = " + "'" + shiftClass + "'");
            }
            if (!string.IsNullOrEmpty(material))
            {
                sb.AppendLine(@"and a.Mater_Code = " + "'" + material + "'");
            }
            if (!string.IsNullOrEmpty(equip))
            {
                sb.AppendLine(@"and a.equip_Code = " + "'" + equip + "'");
            }
            sb.AppendLine(@"and a.plan_date>= " + "'" + dStartDate.SelectedDate.ToString("yyyy-MM-dd") + "'and a.plan_date<= " + "'" + dEndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");

            if (!string.IsNullOrEmpty(cbxtype.Text))
            {
                sb.AppendLine(@" where  i.ItemCode = " + "'" + cbxtype.SelectedItem.Value + "'");
            }
        }
        #endregion
        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        DataSet ds = getList();
        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            GridPanelCenter.ColumnModel.Columns.Add(new Column { DataIndex = dc.ColumnName, Text = dc.ColumnName });
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();

    }

    #region 下拉列表事件响应
    #endregion


    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "工艺数据分析导出");
    }


    #endregion


    #region 信息列表事件响应
    #endregion
}