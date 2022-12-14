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

using System.IO;
using Mesnac.Util.Excel;
using Microsoft.Office;
public partial class Manager_Equipment_EquipProg : Mesnac.Web.UI.Page
{
    protected Eqm_EquipProblemListManager manager = new Eqm_EquipProblemListManager();
    protected JCZL_SubFacManager facManager = new JCZL_SubFacManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            dStartDate.SelectedDate = DateTime.Now.AddDays(-7);
            dEndDate.SelectedDate = DateTime.Now;
            bindList();

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
        sb.AppendLine(@"select * from  eqm_EquipProblemList t");
        sb.AppendLine("WHERE 1=1");
        if (dStartDate.SelectedDate != DateTime.MinValue)
            sb.AppendLine("AND T.P_FindDate>='" + dStartDate.SelectedDate + "'");
        if (dEndDate.SelectedDate != DateTime.MinValue)
            sb.AppendLine("AND T.P_FindDate<='" + dEndDate.SelectedDate + "'");
        if (!string.IsNullOrEmpty(txtequipname.Text))
        { sb.AppendLine("AND T.equipName='" + txtequipname.Text + "'"); }
        if (!string.IsNullOrEmpty(txtstate.Text))
        { sb.AppendLine("AND T.P_State='" + txtstate.Text + "'"); }
        #endregion
        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        DataSet ds = css.ToDataSet();
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备能耗管理导出");
        }
    }


    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_EquipProblemList record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("删除", "删除标准：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(string serialid, string workshop, string equipName,
                    string P_kind, string P_describe, string P_classify,
                    string P_measures, string P_FindUser, DateTime P_FindDate,
                    DateTime P_FinishDate, string P_CheckUser, string P_memo, string P_State)
    {
        if (Convert.ToInt32(serialid) == 0)//新增
        {
            Eqm_EquipProblemList record = new Eqm_EquipProblemList();

            record.Workshop = workshop;
            record.EquipName = equipName;
            record.P_kind = P_kind;
            record.P_describe = P_describe;
            record.P_classify = P_classify;
            record.P_measures = P_measures;
            record.P_FindUser = P_FindUser;
            record.P_FindDate = Convert.ToDateTime(P_FindDate);
            record.P_FinishDate = Convert.ToDateTime(P_FinishDate);
            record.P_CheckUser = P_CheckUser;
            record.P_memo = P_memo;
            record.P_State = P_State;
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
            Eqm_EquipProblemList record = manager.GetById(Convert.ToInt32(serialid));
            record.Workshop = workshop;
            record.EquipName = equipName;
            record.P_kind = P_kind;
            record.P_describe = P_describe;
            record.P_classify = P_classify;
            record.P_measures = P_measures;
            record.P_FindUser = P_FindUser;
            record.P_FindDate = Convert.ToDateTime(P_FindDate);
            record.P_FinishDate = Convert.ToDateTime(P_FinishDate);
            record.P_CheckUser = P_CheckUser;
            record.P_memo = P_memo;
            record.P_State = P_State;
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


    [DirectMethod]
    public void btnDownload_ClickEvent(object sender, DirectEventArgs args)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=设备问题导入模板.xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(Path.Combine(Request.PhysicalApplicationPath, "\\resources\\xls\\设备问题导入模板.xls"));
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("系统错误", "下载失败：" + ex.Message);
        }

    }

    //备件采购模板导入
    public void UploadClickBill(object sender, DirectEventArgs e)
    {
        var rowcount = 0;
        try
        {
            //Sheet名：Sheet1
            var file = FileUploadField2.PostedFile.InputStream;
            //var file = FileUploadField2.PostedFile.InputStream.ToString();
            //Mesnac.Util.Excel.DataToFile dtf = new Mesnac.Util.Excel.DataToFile();
            DataTable dt = Mesnac.Util.Excel.DataToFile.RenderFromExcel(file, "Sheet1");
            string[] oldColName = { "厂房",
                                    "机台",
                                    "部位",
                                    "问题描述",
                                    "问题分类",
                                    "整改措施",
                                    "提出人",
                                    "提出日期",
                                    "完成日期",
                                    "完成人",
                                    "备注",
                                    "状态"};
            string[] newColName = { "changfang",
                                    "equip",
                                    "buwei" ,
                                    "miaoshu",
                                    "fenlei",
                                    "zhenggai",
                                    "tichuren",
                                    "tichudate",
                                    "wanchengdate",
                                    "wanchengren",
                                    "beizhu",
                                    "zhuangtai"};
            rowcount = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "提示", Message = "要导入的文件中无数据：", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.WARNING });
                this.Progress2.Reset();
                this.Progress2.UpdateProgress(0, "要导入的文件中无数据");
                return;
            }
            bool existsCol = true;
            string msg = "";
            //判断文件列是否存在
            for (int i = 0; i < oldColName.Length; i++)
            {
                if (!dt.Columns.Contains(oldColName[i]))
                {
                    existsCol = false;
                    msg += oldColName[i] + "<br/>";
                }
                else
                {
                    dt.Columns[oldColName[i]].ColumnName = newColName[i];
                }
            }
            if (!existsCol)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "提示", Message = "要导入的文件中缺少列：<br/>" + msg, Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.WARNING });
                this.Progress2.Reset();
                this.Progress2.UpdateProgress(0, "要导入的文件中缺少相关列");
                return;
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["tichudate"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["tichudate"]).ToString("yyyy-MM-dd");
                            string Plan_Date2 = Convert.ToDateTime(dr["wanchengdate"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Eqm_EquipProblemList record = new Eqm_EquipProblemList();
                    record.Workshop = dr["changfang"].ToString();
                    record.EquipName = dr["equip"].ToString();
                    record.P_kind = dr["buwei"].ToString();
                    record.P_describe = dr["miaoshu"].ToString();
                    record.P_classify = dr["fenlei"].ToString();
                    record.P_measures = dr["zhenggai"].ToString();
                    record.P_FindUser = dr["tichuren"].ToString();

                    record.P_FindDate = Convert.ToDateTime(dr["tichudate"].ToString());
                    record.P_FinishDate = Convert.ToDateTime(dr["wanchengdate"].ToString());
                    record.P_CheckUser = dr["wanchengren"].ToString();
                    record.P_memo = dr["beizhu"].ToString();
                    record.P_State = dr["zhuangtai"].ToString();
                    manager.Insert(record);

                }
            }


            this.Progress2.Reset();
            this.Progress2.UpdateProgress(1, "已完成!");
        }
        catch (Exception ex)
        {
            this.Progress2.Reset();
            this.Progress2.UpdateProgress(1, "数据读取异常：" + ex.Message);
        }
    }
}