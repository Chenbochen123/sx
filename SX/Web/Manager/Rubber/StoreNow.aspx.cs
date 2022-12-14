using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;
using System.Collections.Generic;
using System.Text;


public partial class Manager_Rubber_StoreNow : Mesnac.Web.UI.Page
{
    protected PptShiftConfigManager Manager = new PptShiftConfigManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        DateBeginTime.SelectedDate = DateTime.Now.AddDays(-15);
        DateEndTime.SelectedDate = DateTime.Now;
        bindList();
      //  bindMaintainers();
    }


    private DataSet getList()
    {


        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        string fac = "";
        #region 
        sb.AppendLine(@"select b.mater_name,sum(shelf_num) as num,sum(real_weight-used_weigh) as weight 
from ppt_shiftconfig a
left join pmt_material b on a.mater_Code=b.mater_Code
where plan_date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "' AND plan_date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "' ");
        
//        sb.AppendLine(@"select b.mater_name,sum(shelf_num) as num,sum(real_weight-used_weigh) as weight 
//from ppt_shiftconfig a
//left join pmt_material b on a.mater_Code=b.mater_Code
//where equip_Code in (select equip_Code from pmt_equip where equip_Class='01' and sub_fac in ("+fac+")) AND plan_date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "' AND plan_date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        //if (DateBeginTime.SelectedDate != DateTime.MinValue)
        //{
        //    sb.AppendLine("AND plan_date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        //}
        //if (DateEndTime.SelectedDate != DateTime.MinValue)
        //{
        //    sb.AppendLine("AND plan_date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        //}

        if (cboType.SelectedItem.Value == "4")
        {
            sb.AppendLine("AND a.Mater_code like'" + "4%" + "'");
        }
        if (cboType.SelectedItem.Value == "5")
        {
            sb.AppendLine("AND a.Mater_code like'" + "5%" + "'");
        }
        if (cboType.SelectedItem.Value == "2")
        {
            sb.AppendLine("AND a.Mater_code like'" + "2%" + "'");
        }
        sb.AppendLine(" group by b.mater_name");
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
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
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "当前库存表");
        }
    }

    #endregion

    protected void ShiftConfig_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Rubber/ShiftConfig.aspx",true);
        //this.Response.Redirect("../Rubber/ShiftConfig.aspx");
    }
}