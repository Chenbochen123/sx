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


public partial class Manager_ShopStorage_Shopout : Mesnac.Web.UI.Page
{
    protected PstmmshopoutManager Manager = new PstmmshopoutManager();
    protected JCZL_SubFacManager SubManager = new JCZL_SubFacManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            txtStratDate.SelectedDate = DateTime.Now;
            txtEndDate.SelectedDate = DateTime.Now;
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        bindSub();
        bindList();
    }


    private void bindSub()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_SubFac> list = SubManager.GetListByWhere(where);
        foreach (JCZL_SubFac main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Fac_Name, main.Dep_Code);
            cbxbumen.Items.Add(item);
        }
}

    private DataSet getList()
    {


        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
select t2.dep_name,Mater_name,PDM_Code,sum(Balance_Qty) Qty from Pst_mmshopout t
left join Pmt_equip t1 on t.Equip_code=t1.Equip_code
left join JCZL_dep t2 on t2.dep_num=t1.Sub_Fac
left join Pmt_material t3 on t3.Mater_code=t.Mater_code
");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND t.plan_date >='" + txtStratDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        sb.AppendLine("AND t.plan_date <='" + txtEndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(cbxbumen.Text))
        {
            sb.AppendLine("AND t1.Sub_Fac='" + cbxbumen.SelectedItem.Value + "'");
        }
        sb.AppendLine("group by dep_name,Mater_name,PDM_Code");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "车间原料消耗统计");
        }
    }
   
    }

    #endregion

   

    
