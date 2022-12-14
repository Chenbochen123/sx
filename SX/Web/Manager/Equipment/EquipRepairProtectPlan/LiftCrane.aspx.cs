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

public partial class Manager_Equipment_EquipRepairProtectPlan_LiftCrane : Mesnac.Web.UI.Page
{
    protected Eqm_EquipArchivesPlanManager planmanager = new Eqm_EquipArchivesPlanManager();
    protected Eqm_EquipArchivesManager manager = new Eqm_EquipArchivesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            winPlanSave.Hidden = true;
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
        sb.AppendLine(@"select *,Convert(datetime,Use_date,101) user_date1 from  Eqm_EquipArchives");
        sb.AppendLine("WHERE 1=1 and Etype=2 ");
        //if (!string.IsNullOrEmpty(cbxType.Text))
        //{
        //    sb.AppendLine("AND Etype='" + cbxType.Value + "'");
        //}
        //if (!string.IsNullOrEmpty(txtmainid.Text))
        //{
        //    sb.AppendLine("AND mainid='" + txtmainid.Text + "'");
        //}
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND iid='" + hidden_type.Text + "'");
        }
        sb.AppendLine("order by iid desc");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "电梯吊车信息导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_EquipArchives record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("记录删除", "删除序号：" + record.Iid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int iid, int serialid, string Workshop, string equip_name, string equip_Spec, float num_value1, string num_value2, string equip_param,
        string fac_no, string equip_No, string depname, string Use_pos, DateTime Use_date, string made_fac, string use_No, int CYCLE, DateTime check_date, DateTime check_next, string mem1)
    {
        if (Convert.ToInt32(iid) == 0)//新增
        {
            Eqm_EquipArchives record = new Eqm_EquipArchives();

            record.Iid = iid;
            record.Serialid = serialid;
            record.Etype = 2;
            record.Workshop = Workshop;
            record.Equip_name = equip_name.ToString();
            record.Equip_Spec = equip_Spec;
            record.Num_value1 = Convert.ToDecimal(num_value1);
            record.Num_value2 = num_value2;
            record.Equip_param = equip_param;
            record.Fac_no = fac_no;
            record.Equip_No = equip_No;
            record.Depname = depname;
            record.Use_pos = Use_pos;
            record.Use_date = Use_date.ToString("yyyy-MM-dd");
            record.Made_fac = made_fac;
            record.Use_No = use_No;
            record.CYCLE = CYCLE;
            record.Check_date = check_date;
            record.Check_next = check_date.AddYears(CYCLE);
            record.Mem1 = mem1;
            if (manager.Insert(record) >= 0)
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
            Eqm_EquipArchives record = manager.GetById(iid);
            record.Serialid = serialid;
            record.Workshop = Workshop;
            record.Equip_name = equip_name.ToString();
            record.Equip_Spec = equip_Spec;
            record.Num_value1 = Convert.ToDecimal(num_value1);
            record.Num_value2 = num_value2;
            record.Equip_param = equip_param;
            record.Fac_no = fac_no;
            record.Equip_No = equip_No;
            record.Depname = depname;
            record.Use_pos = Use_pos;
            record.Use_date = Use_date.ToString("yyyy-MM-dd");
            record.Made_fac = made_fac;
            record.Use_No = use_No;
            record.CYCLE = CYCLE;
            record.Check_date = check_date;
            record.Check_next = check_date.AddYears(CYCLE);
            record.Mem1 = mem1;
            if (manager.Update(record) >= 0)
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

    protected void btnCreatePlan_Click(object sender, EventArgs e)
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "您没有选择任何行,请选择").Show();
            return;
        }
        string objid = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            objid = row.RecordID;
            EntityArrayList<Eqm_EquipArchives> listEdit = manager.GetListByWhere(Eqm_EquipArchives._.Iid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准！").Show();
                return;
            }
            Eqm_EquipArchives record = listEdit[0];
            txtmainid.Text = record.Serialid.ToString();
            txtEtype.Text = "2";
            datecheckdate.SetValue(DateTime.Now);
            txtEquipNo.Text = record.Equip_No.ToString();
            txtmemo.Text = record.Mem1.ToString();
            hidePlanObjID.Text = row.RecordID;
        }
        this.winPlanSave.Hidden = false;
    }

    protected void btnPlanSave_Click(object sender, EventArgs e)
    {
        Eqm_EquipArchivesPlan record = new Eqm_EquipArchivesPlan();
        if (record != null)
        {
            record.Mainid = Convert.ToInt32(txtmainid.Text);
            record.Etype = Convert.ToInt32(txtEtype.Text);
            record.Checkdate = datecheckdate.SelectedDate;
            record.EquipNo = txtEquipNo.Text;
            record.Memo = txtmemo.Text;

            EntityArrayList<Eqm_EquipArchives> listEdit = manager.GetListByWhere(Eqm_EquipArchives._.Iid == hidePlanObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此设备，请核实！").Show();
                return;
            }
            listEdit[0].Check_date = datecheckdate.SelectedDate;
            listEdit[0].Check_next = datecheckdate.SelectedDate.AddYears(Convert.ToInt32(listEdit[0].CYCLE));
            listEdit[0].Equip_No = txtEquipNo.Text;
            listEdit[0].Mem1 = txtmemo.Text;
            manager.Update(listEdit[0]);

            if (planmanager.Insert(record) >= 0)
            {
                this.AppendWebLog("设备维修记录添加完成", "完成编号：" + record.Iid);
                this.winPlanSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "完成成功！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "完成失败！").Show();
            }
        }
    }

    protected void btnPlanCancel_Click(object sender, EventArgs e)
    {
        winPlanSave.Hidden = true;
    }
}