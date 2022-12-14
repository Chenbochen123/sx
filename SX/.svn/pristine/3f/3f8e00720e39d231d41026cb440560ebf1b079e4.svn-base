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

public partial class Manager_Equipment_EquipRepairProtectPlan_ImportantBJ : Mesnac.Web.UI.Page
{
    protected Eqm_ImportantBJManager manager = new Eqm_ImportantBJManager();
    protected Eqm_ImportantBJrecordManager recordmanager = new Eqm_ImportantBJrecordManager();
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
        sb.AppendLine(@"select * from Eqm_ImportantBJ");
        sb.AppendLine("WHERE 1=1 ");
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND INO='" + hidden_type.Text + "'");
        }
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "关键配件台账导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_ImportantBJ record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("记录删除", "删除序号：" + record.INO.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int INO, string Workshop, string EquipName, string BJName, string BJCode, string changeBJ, int WorkTime, DateTime beginDate,
        DateTime lastdate, string reson, string memo)
    {
        if (Convert.ToInt32(INO) == 0)//新增
        {
            Eqm_ImportantBJ record = new Eqm_ImportantBJ();
            record.INO = Convert.ToInt32(GetMaxPlanID2());
            record.Workshop = Workshop;
            record.EquipName = EquipName;
            record.BJName = BJName;
            record.BJCode = BJCode;
            record.ChangeBJ = changeBJ;
            record.WorkTime = WorkTime;
            record.BeginDate = beginDate;
            record.Lastdate = lastdate;
            record.Reson = reson;
            record.Memo = memo;

            if (manager.Insert(record) >= 0)
            {
                X.Msg.Alert("提示", "添加完成！").Show(); bindList();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            Eqm_ImportantBJ record = manager.GetById(INO);
            //if (record.EquipNo != EquipNo)
            //{ X.Msg.Alert("提示", "不允许修改设备编号！").Show(); return; }
            record.Workshop = Workshop;
            record.EquipName = EquipName;
            record.BJName = BJName;
            record.BJCode = BJCode;
            record.ChangeBJ = changeBJ;
            record.WorkTime = WorkTime;
            record.BeginDate = beginDate;
            record.Lastdate = lastdate;
            record.Reson = reson;
            record.Memo = memo;

            if (manager.Update(record) >= 0)
            {
                X.Msg.Alert("提示", "修改完成！").Show(); bindList();
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

            EntityArrayList<Eqm_ImportantBJ> listEdit = manager.GetListByWhere(Eqm_ImportantBJ._.INO == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准！").Show();
                return;
            }
            txtino.Text = listEdit[0].INO.ToString();
            txtchangfang.Text = listEdit[0].Workshop;
            txtequip.Text = listEdit[0].EquipName;
            txtbeijian.Text = listEdit[0].BJName;
            txtbeijianNO.Text = listEdit[0].BJCode;
            txtchangebujian.Text = listEdit[0].ChangeBJ;
            datetime.Text = DateTime.Now.ToString();
            txtreason.Text = listEdit[0].Reson;
            txtmemo.Text = listEdit[0].Memo;

         
            hidePlanObjID.Text = row.RecordID;
        }
        this.winPlanSave.Hidden = false;
    }

    protected void btnPlanSave_Click(object sender, EventArgs e)
    {
        Eqm_ImportantBJrecord record = new Eqm_ImportantBJrecord();
        if (record != null)
        {
            record.INO = Convert.ToInt32(txtino.Text);
            record.Workshop = txtchangfang.Text;
            record.EquipName = txtequip.Text;
            record.BJName = txtbeijian.Text;
            record.BJCode = txtbeijianNO.Text;
            record.ChangeBJ = txtchangebujian.Text;
            record.Lastdate = datetime.SelectedDate;
            record.Reson = txtreason.Text;
            record.Memo = txtmemo.Text;

            //在维护清洗记录页面，修改记录表的同时，修改设备表
            EntityArrayList<Eqm_ImportantBJ> listEdit = manager.GetListByWhere(Eqm_ImportantBJ._.INO == hidePlanObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此设备，请核实！").Show();
                return;
            }
            if (listEdit[0].INO != Convert.ToInt32(txtino.Text))
            { X.Msg.Alert("提示", "不允许修改序号！").Show(); return; }
          
            listEdit[0].Lastdate = datetime.SelectedDate;
            listEdit[0].Reson = txtreason.Text;


            manager.Update(listEdit[0]);

            if (recordmanager.Insert(record) >= 0)
            {
                this.AppendWebLog("设备维修记录添加完成", "完成编号：" + record.Serialid);
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
    protected string GetMaxPlanID2()
    {
        string planID = "";
        EntityArrayList<Eqm_ImportantBJ> list = manager.GetAllListOrder(Eqm_ImportantBJ._.INO.Desc);
        if (list.Count > 0)
        {
            planID = (list[0].INO + 1).ToString();
        }
        else { planID = "1"; }
        return planID;
    }
}