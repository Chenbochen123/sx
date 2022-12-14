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

public partial class Manager_Equipment_EquipRepairProtectPlan_KongTiaoRecord : Mesnac.Web.UI.Page
{
    protected Eqm_KongTiaoRecordManager manager = new Eqm_KongTiaoRecordManager();
  //  protected Eqm_EquipArchivesManager manager = new Eqm_EquipArchivesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["INO"]))
            {
                hidden_type.Text = Request.QueryString["INO"].ToString();
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
        sb.AppendLine(@"SELECT * FROM Eqm_KongTiaoRecord ");
        sb.AppendLine("WHERE 1=1 ");
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND INO='" + hidden_type.Text + "'");
        }
        sb.AppendLine("order by serialid desc");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "空调维修记录导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(int ObjID)
    {
        Eqm_KongTiaoRecord record = manager.GetById(ObjID);
        manager.Delete(ObjID);
        this.AppendWebLog("记录删除", "删除序号：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int serialid, int INO, string EquipNO, string PosName,
                    DateTime lastdate, string lastfac, string WX_reason, float WX_money, string pseron, string memo)
    {
        if (Convert.ToInt32(serialid) == 0)//新增
        {
            Eqm_KongTiaoRecord record = new Eqm_KongTiaoRecord();

            record.INO = INO;
            record.EquipNO = EquipNO;
            record.PosName = PosName;
            record.Lastdate = lastdate;
            record.Lastfac = lastfac;
            record.WX_reason = WX_reason;
            record.WX_money = Convert.ToDecimal(WX_money);
            record.Pseron = pseron;
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
            Eqm_KongTiaoRecord record = manager.GetById(serialid);

            record.INO = INO;
            record.EquipNO = EquipNO;
            record.PosName = PosName;
            record.Lastdate = lastdate;
            record.Lastfac = lastfac;
            record.WX_reason = WX_reason;
            record.WX_money = Convert.ToDecimal(WX_money);
            record.Pseron = pseron;
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
    [DirectMethod]
    public void btnDownload_ClickEvent(object sender, DirectEventArgs args)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=空调维修导入模板.xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(Path.Combine(Request.PhysicalApplicationPath, "\\resources\\xls\\空调维修导入模板.xls"));
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
            string[] oldColName = { "序号",
                                    "设备编号",
                                    "安装位置",
                                    "维修日期",
                                    "维修厂家",
                                    "修理原因",
                                    "修理费用",
                                    "负责人",
                                    "备注"};
            string[] newColName = { "xuhao",
                                    "shebei",
                                    "anzhuang" ,
                                    "weixiuriqi",
                                    "weixiuchangjia",
                                    "yuanyin",
                                    "feiyong",
                                    "fuzeren",
                                    "memo"};
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
                    if (!string.IsNullOrEmpty(dr["weixiuriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["weixiuriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Eqm_KongTiaoRecord record = new Eqm_KongTiaoRecord();
                    record.INO = Convert.ToInt32(dr["xuhao"].ToString());
                    record.EquipNO = dr["shebei"].ToString();
                    record.PosName = dr["anzhuang"].ToString();
                    record.Lastdate = Convert.ToDateTime(dr["weixiuriqi"].ToString());
                    record.Lastfac = dr["weixiuchangjia"].ToString();
                    record.WX_reason = dr["yuanyin"].ToString();
                    record.WX_money = Convert.ToDecimal(dr["feiyong"].ToString());
                    record.Pseron =dr["fuzeren"].ToString();
                    record.Memo = dr["memo"].ToString();
                    manager.Insert(record);

                }
            }
            bindList();
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