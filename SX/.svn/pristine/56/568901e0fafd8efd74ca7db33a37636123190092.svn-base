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

public partial class Manager_Equipment_EquipRepairProtectPlan_EquipArchivesPlan : Mesnac.Web.UI.Page
{
    protected Eqm_EquipArchivesPlanManager planmanager = new Eqm_EquipArchivesPlanManager();
    protected Eqm_EquipArchivesManager manager = new Eqm_EquipArchivesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["mainid"]))
            {
                hidden_type.Text = Request.QueryString["mainid"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Etype"]))
            {
                hidden_EType.Text = Request.QueryString["Etype"].ToString();
            }
            bindList();
        }
    }


    #region 初始化控件
    


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }


    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from  Eqm_EquipArchivesPlan");
        sb.AppendLine("WHERE 1=1 ");
        if (!string.IsNullOrEmpty(cbxType.Text))
        {
            sb.AppendLine("AND Etype='" + cbxType.Value + "'");
        }
        if (!string.IsNullOrEmpty(txtmainid.Text))
        {
            sb.AppendLine("AND mainid='" + txtmainid.Text + "'");
        }
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND mainid='" + hidden_type.Text + "'");
        }
        if (!string.IsNullOrEmpty(hidden_EType.Text))
        {
            sb.AppendLine("AND Etype='" + hidden_EType.Text + "'");
        }
        sb.AppendLine("order by iid desc");
        #endregion

        NBear.Data.CustomSqlSection css = planmanager.GetBySql(sb.ToString());
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "特种设备检验记录导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_EquipArchivesPlan record = planmanager.GetById(int.Parse(ObjID));
        planmanager.Delete(int.Parse(ObjID));
        this.AppendWebLog("记录删除", "删除序号：" + record.Iid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int iid, int mainid, int Etype, DateTime checkdate,
                    string EquipNo, string memo)
    {
        if (Convert.ToInt32(iid) == 0)//新增
        {
            Eqm_EquipArchivesPlan record = new Eqm_EquipArchivesPlan();

            record.Mainid = mainid;
            record.Etype = Etype;
            record.Checkdate = checkdate;
            record.EquipNo = EquipNo;
            record.Memo = memo;
            if (planmanager.Insert(record) >= 0)
            {
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            Eqm_EquipArchivesPlan record = planmanager.GetById(iid);

            record.Mainid = mainid;
            record.Etype = Etype;
            record.Checkdate = checkdate;
            record.EquipNo = EquipNo;
            record.Memo = memo;

            if (planmanager.Update(record) >= 0)
            {
                X.Msg.Alert("提示", "修改完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        }
    }
    #endregion
}