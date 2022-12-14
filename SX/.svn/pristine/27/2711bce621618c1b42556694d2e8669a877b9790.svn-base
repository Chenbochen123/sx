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

public partial class Manager_Equipment_EquipState_EquipProblemList : Mesnac.Web.UI.Page
{
    protected Eqm_EquipProblemListManager manager = new Eqm_EquipProblemListManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
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
        sb.AppendLine(@"select * from eqm_EquipProblemList");
        sb.AppendLine("WHERE 1=1 order by serialid desc");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备问题清单导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_EquipProblemList record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("设备问题清单删除", "删除编号：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int serialid, string workshop, string equipName,
                    string P_kind, string P_describe, string P_classify,
                    string P_measures, string P_FindUser, DateTime P_FindDate,
                    DateTime P_FinishDate, string P_CheckUser,string P_State,string P_memo)
    {
        if (Convert.ToInt32(serialid) == 0)//新增
        {
            Eqm_EquipProblemList record = new Eqm_EquipProblemList();
            record.Serialid = serialid;
            record.Workshop = workshop;
            record.EquipName = equipName;
            record.P_kind = P_kind;
            record.P_describe = P_describe;
            record.P_classify = P_classify;
            record.P_measures = P_measures;
            record.P_FindUser = P_FindUser;
            record.P_FindDate = P_FindDate;
            record.P_FinishDate = P_FinishDate;
            record.P_CheckUser = P_CheckUser;
            record.P_State = P_State;
            record.P_memo = P_memo;
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
            Eqm_EquipProblemList record = manager.GetById(serialid);

            record.Workshop = workshop;
            record.EquipName = equipName;
            record.P_kind = P_kind;
            record.P_describe = P_describe;
            record.P_classify = P_classify;
            record.P_measures = P_measures;
            record.P_FindUser = P_FindUser;
            record.P_FindDate = P_FindDate;
            record.P_FinishDate = P_FinishDate;
            record.P_CheckUser = P_CheckUser;
            record.P_State = P_State;
            record.P_memo = P_memo;
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
}