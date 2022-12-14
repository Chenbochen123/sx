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

public partial class Manager_Equipment_Energy_EnergyOEE : Mesnac.Web.UI.Page
{
    protected EQM_EnergyManageManager manager = new EQM_EnergyManageManager();
    protected JCZL_SubFacManager facManager = new JCZL_SubFacManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            dStartDate.SetValue(DateTime.Now);
            dEndDate.SetValue(DateTime.Now.AddDays(1));
            bindFac();

        }
    }


    #region 初始化控件

    private void bindFac()
    {
        cbxFac.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_SubFac> list = facManager.GetListByWhereAndOrder(where, JCZL_SubFac._.Fac_Id.Asc);
        foreach (JCZL_SubFac type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Fac_Name, type.Dep_Code);
            cbxFac.Items.Add(item);
        }
        cbxFac.Select(0);
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
        sb.AppendLine(@"exec Proc_GetEquipOEE " + "'" + cbxFac.SelectedItem.Value +"',"+"'"+ dStartDate.SelectedDate.ToString("yyyy-MM-dd")+"',"+"'"+dEndDate.SelectedDate.ToString("yyyy-MM-dd")+"'");
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

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
        //huiw,2013-10-28添加，判断不为空时才导出excel
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                bool isshow = false;
                DataColumn dc = ds.Tables[0].Columns[i];
                foreach (ColumnBase cb in this.GridPanelCenter.ColumnModel.Columns)
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
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备OEE导出");
        }
    }


    #endregion


    #region 信息列表事件响应
    #endregion
}