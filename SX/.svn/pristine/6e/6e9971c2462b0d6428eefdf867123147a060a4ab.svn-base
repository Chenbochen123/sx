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


public partial class Manager_Equipment_Repair_RepairRecord : Mesnac.Web.UI.Page
{
    protected EQM_waiWeiWXManager manager = new EQM_waiWeiWXManager();
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
        sb.AppendLine(@"select T.* from EQM_waiWeiWX T");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "外委维修记录导出");
        }
    }


    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EQM_waiWeiWX record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("外委维修记录删除", "删除记录：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(string serialid, int MM,
                    string shenqingdan, string facname, string yanshou_date1,
                    string yanshou_date2, string yanshou_price, string ino,
                    string depname, string gongduan, string equipgroup,
                    string equipLine, string equipName, string num,
                    string equipNo, string xinghao, string spec, string guzhang, string guzhang_date,
                    string memo, string plan_price, string username, int isplan, string create_date,
                    string billno, string stopdate, string real_price, string fuwu_fac, string last_date,
                    string last_fac, string last_yuanyin)
    {
        if(Convert.ToInt32(serialid)==0)//新增
        {
            EQM_waiWeiWX record = new EQM_waiWeiWX();
            decimal Yanshou_price = 0;
            int Ino = 0;
            int Num = 0;
            decimal Plan_price = 0;
            decimal Real_price = 0;
            record.MM = MM;
            record.Shenqingdan = shenqingdan;
            record.Facname = facname;
            record.Yanshou_date1 = yanshou_date1;
            record.Yanshou_date2 = yanshou_date2;
            if (decimal.TryParse(yanshou_price, out Yanshou_price))
            {
                record.Yanshou_price = Yanshou_price;
            }
            if (Int32.TryParse(ino, out Ino))
            {
                record.Ino = Ino;
            }
            if (Int32.TryParse(num, out Num))
            {
                record.Num = Num;
            }
            if (decimal.TryParse(plan_price, out Plan_price))
            {
                record.Plan_price = Plan_price;
            }
            if (decimal.TryParse(real_price, out Real_price))
            {
                record.Real_price = Real_price;
            }
            record.Depname = depname;
            record.Gongduan = gongduan;
            record.Equipgroup = equipgroup;
            record.EquipLine = equipLine;
            record.EquipName = equipName;
            record.EquipNo = equipNo;
            record.Xinghao = xinghao;
            record.Spec = spec;
            record.Guzhang = guzhang;
            record.Guzhang_date = guzhang_date;
            record.Memo = memo;
            record.Username = username;
            record.Isplan = isplan;
            record.Create_date = create_date;
            record.Billno = billno;
            record.Stopdate = stopdate;
            record.Fuwu_fac = fuwu_fac;
            record.Last_date = last_date;
            record.Last_fac = last_fac;
            record.Last_yuanyin = last_yuanyin;
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
            EQM_waiWeiWX record = manager.GetById(int.Parse(serialid));
            decimal Yanshou_price = 0;
            int Ino = 0;
            int Num = 0;
            decimal Plan_price = 0;
            decimal Real_price = 0;
            record.MM = MM;
            record.Shenqingdan = shenqingdan;
            record.Facname = facname;
            record.Yanshou_date1 = yanshou_date1;
            record.Yanshou_date2 = yanshou_date2;
            if (decimal.TryParse(yanshou_price, out Yanshou_price))
            {
                record.Yanshou_price = Yanshou_price;
            }
            if (Int32.TryParse(ino, out Ino))
            {
                record.Ino = Ino;
            }
            if (Int32.TryParse(num, out Num))
            {
                record.Num = Num;
            }
            if (decimal.TryParse(plan_price, out Plan_price))
            {
                record.Plan_price = Plan_price;
            }
            if (decimal.TryParse(real_price, out Real_price))
            {
                record.Real_price = Real_price;
            }
            record.Depname = depname;
            record.Gongduan = gongduan;
            record.Equipgroup = equipgroup;
            record.EquipLine = equipLine;
            record.EquipName = equipName;
            record.EquipNo = equipNo;
            record.Xinghao = xinghao;
            record.Spec = spec;
            record.Guzhang = guzhang;
            record.Guzhang_date = guzhang_date;
            record.Memo = memo;
            record.Username = username;
            record.Isplan = isplan;
            record.Create_date = create_date;
            record.Billno = billno;
            record.Stopdate = stopdate;
            record.Fuwu_fac = fuwu_fac;
            record.Last_date = last_date;
            record.Last_fac = last_fac;
            record.Last_yuanyin = last_yuanyin;
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
            Response.AddHeader("Content-Disposition", "attachment;filename=外委维修导入模板.xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(Path.Combine(Request.PhysicalApplicationPath, "\\resources\\xls\\外委维修导入模板.xls"));
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
            string[] oldColName = { "月份",
                                    "申请单号",
                                    "公司",
                                    "工段验收日期",
                                    "制造部验收日期",
                                    "验收价格",
                                    "序号",
                                    "制造部",
                                    "工段",
                                    "设备组",
                                    "生产线/设备编号",
                                    "名称",
                                    "数量",
                                    "设备编号",
                                    "型号",
                                    "规格",
                                    "本次故障原因",
                                    "故障日期",
                                    "备注",
                                    "预期价格",
                                    "负责人",
                                    "计划(0外1内)",
                                    "工单建立日期",
                                    "工单号",
                                    "工单结束日期",
                                    "实际价格",
                                    "服务公司",
                                    "上次故障日期",
                                    "上次服务公司",
                                    "上次故障原因"};
            string[] newColName = { "yuefen",
                                    "danhao",
                                    "gongsi" ,
                                    "gongduanyanshouriqi",
                                    "zhizaoburiqi",
                                    "yanshoujiage",
                                    "xuhao",
                                    "zhizaobu",
                                    "gongduan",
                                    "shebeizu",
                                    "shengchanxian",
                                    "mingcheng",
                                    "shuliang",
                                    "shebeibianhao",
                                    "xnghao",
                                    "guige",
                                    "benciguzhang",
                                    "guzhangriqi",
                                    "beizhu",
                                    "yuqijiage",
                                    "fuzeren",
                                    "jihua",
                                    "gongduanriqi",
                                    "gongdanhao",
                                    "gongdanjieshuriqi",
                                    "shijijiage",
                                    "fuwugongsi",
                                    "shangciriqi",
                                    "shangcigongsi",
                                    "shangciyuanyin"};
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
                    if (!string.IsNullOrEmpty(dr["gongduanyanshouriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["gongduanyanshouriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["zhizaoburiqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["zhizaoburiqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["guzhangriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["guzhangriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["gongduanriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["gongduanriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["gongdanjieshuriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["gongdanjieshuriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["shangciriqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["shangciriqi"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "日期格式-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    EQM_waiWeiWX record = new EQM_waiWeiWX();
                    record.MM = Convert.ToInt32(dr["yuefen"].ToString());
                    record.Shenqingdan = dr["danhao"].ToString();
                    record.Facname = dr["gongsi"].ToString();
                    if (!string.IsNullOrEmpty(dr["gongduanyanshouriqi"].ToString()))
                    { record.Yanshou_date1 = Convert.ToDateTime(dr["gongduanyanshouriqi"].ToString()).ToString("yyyy-MM-dd"); }
                    if (!string.IsNullOrEmpty(dr["zhizaoburiqi"].ToString()))
                    { record.Yanshou_date2 = Convert.ToDateTime(dr["zhizaoburiqi"].ToString()).ToString("yyyy-MM-dd"); }
                    if (!string.IsNullOrEmpty(dr["yanshoujiage"].ToString()))
                    {
                        record.Yanshou_price = Convert.ToDecimal(dr["yanshoujiage"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["xuhao"].ToString()))
                    {
                        record.Ino = Convert.ToInt32(dr["xuhao"].ToString());
                    }
                    record.Depname = dr["zhizaobu"].ToString();
                    record.Gongduan = dr["gongduan"].ToString();
                    record.Equipgroup = dr["shebeizu"].ToString();
                    record.EquipLine = dr["shengchanxian"].ToString();
                    record.EquipName = dr["mingcheng"].ToString();
                    if (!string.IsNullOrEmpty(dr["shuliang"].ToString()))
                    {
                        record.Num = Convert.ToInt32(dr["shuliang"].ToString());
                    }
                    record.EquipNo = dr["shebeibianhao"].ToString();
                    record.Xinghao = dr["xnghao"].ToString();
                    record.Spec = dr["guige"].ToString();
                    record.Guzhang = dr["benciguzhang"].ToString();
                    if (!string.IsNullOrEmpty(dr["guzhangriqi"].ToString()))
                    { record.Guzhang_date = Convert.ToDateTime(dr["guzhangriqi"].ToString()).ToString("yyyy-MM-dd"); }
                    record.Memo = dr["beizhu"].ToString();
                    if (!string.IsNullOrEmpty(dr["yuqijiage"].ToString()))
                    {
                        record.Plan_price = Convert.ToDecimal(dr["yuqijiage"].ToString());
                    }
                    record.Username = dr["fuzeren"].ToString();
                    if (!string.IsNullOrEmpty(dr["jihua"].ToString()))
                    {
                        record.Isplan = Convert.ToInt32(dr["jihua"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["gongduanriqi"].ToString()))
                    { record.Create_date = Convert.ToDateTime(dr["gongduanriqi"].ToString()).ToString("yyyy-MM-dd"); }
                    record.Billno = dr["gongdanhao"].ToString();
                    if (!string.IsNullOrEmpty(dr["gongdanjieshuriqi"].ToString()))
                    { record.Stopdate = Convert.ToDateTime(dr["gongdanjieshuriqi"].ToString()).ToString("yyyy-MM-dd"); }
                    if (!string.IsNullOrEmpty(dr["shijijiage"].ToString()))
                    {
                        record.Real_price = Convert.ToDecimal(dr["shijijiage"].ToString());
                    }
                    record.Fuwu_fac = dr["fuwugongsi"].ToString();
                    if (!string.IsNullOrEmpty(dr["shangciriqi"].ToString()))
                    { record.Stopdate = Convert.ToDateTime(dr["shangciriqi"].ToString()).ToString("yyyy-MM-dd"); }
                    record.Last_fac = dr["shangcigongsi"].ToString();
                    record.Last_yuanyin = dr["shangciyuanyin"].ToString();
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