using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;

public partial class Manager_RubberQuality_Report_PmtCheckChart : Mesnac.Web.UI.Page
{
    public string TipsRenderer = "";
    public string days = "";
    public string series = "";
    public string width = "";
    public string ymin = "";
    public string ymax = "";
    public string tickInterval = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            QmtCheckItemManager cm = new QmtCheckItemManager();
            DataTable dt1 = cm.GetAllDataSet().Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                cbxXiang.Items.Add(new Ext.Net.ListItem(dr["ItemName"].ToString(),dr["ItemCode"].ToString()));
            }
            if (!Page.IsPostBack)
            {
                txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
                txtEndTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CltQmtCheckCtrlManager ppmrub = new CltQmtCheckCtrlManager();
        ICltQmtCheckCtrlQueryParams parms = new CltQmtCheckCtrlQueryParams();
        if (string.IsNullOrEmpty(txtBeginTime.Text) || string.IsNullOrEmpty(txtEndTime.Text) || string.IsNullOrEmpty(hiddenMaterCode.Text) || string.IsNullOrEmpty(cbxXiang.Value.ToString()))
        {
            X.Msg.Alert("提示", "查询条件不完整!").Show();
            return;
        }
        if (cbxWorkCode.Value.ToString() == "0" || cbxWorkCode.Value.ToString() == "全部")
        {
            cbxWorkCode.Value = "";
        }
        string tt=cbxType.Value.ToString();
        if (tt != "1" && tt != "2")
        {
            tt = "0";
        }
        parms.BeginDate = Convert.ToDateTime(txtBeginTime.Text.Trim()).ToString("yyyy-MM-dd");
        parms.EndDate = Convert.ToDateTime(txtEndTime.Text.Trim()).ToString("yyyy-MM-dd");
        parms.MaterCode = hiddenMaterCode.Text.Trim();
        parms.ItemCd = cbxXiang.Value.ToString();
        parms.WorkShopCode = cbxWorkCode.Value.ToString();
        parms.UserID = hiddenMakerPerson.Text.ToString().Trim();
        parms.StatisticType = tt;
        parms.ZJSID = "0";
        DataTable dt = ppmrub.GetCheckChart(parms).Tables[0];
        DataTable dtv = ppmrub.GetFormatValue(parms).Tables[0];
        DataTable dtsource = new DataTable();
        dtsource.Columns.Add("天数",typeof(String));
        dtsource.Columns.Add("中值",typeof(double));
        dtsource.Columns.Add("上限",typeof(double));
        dtsource.Columns.Add("下限",typeof(double));
        if (dt != null && dt.Rows.Count>0)
        {
            //Model m = Store2.ModelInstance;
            double dmax = 0;
            double dmin = 0;
            double davg = 0;
            if (dtv != null && dtv.Rows.Count > 0)
            {
                DataRow dr1 = dtv.Rows[0];
                dmax = Convert.ToDouble(dr1["PermMax"].ToString());
                dmin = Convert.ToDouble(dr1["PermMin"].ToString());
                davg = Convert.ToDouble(dr1["AVGValue"].ToString());
            }
            DateTime dtstart = Convert.ToDateTime(txtBeginTime.Text.Trim());
            DateTime dtend = Convert.ToDateTime(txtEndTime.Text.Trim());
            foreach (DataRow dr in dt.Rows)
            {
                string date = dr["FDate"].ToString();
                string name = dr["FName"].ToString();
                string v = dr["FValue"].ToString();
                dtsource.Columns.Add(name,typeof(Double));
                //ModelField m1 = new ModelField(name);
                //m.Fields.Add(m1);
                //LineSeries se1 = new LineSeries();
                //se1.Axis = Position.Left;
                //se1.AutoDataBind = true;
                //se1.XField = new string[] { "天数" };
                //se1.YField = new string[] { name };
                //ChartTip ct1 = Chart2.Series[0].Tips;
                //ChartTip ct = new ChartTip();
                //ct.TrackMouse = ct1.TrackMouse;
                //ct.Width = ct1.Width;
                //ct.Height = ct1.Height;
                //ct.Renderer.Handler = "TipsRenderer(this,storeItem);";
                //se1.Tips = ct;
                //Chart2.Series.Add(se1);
                //TipsRenderer += "ss = ss + \"" + name + "：\" + record.get('" + name + "') + '<br />';";
            }
            int i = 0;
            DateTime dtnow = dtstart;
            while (dtnow.CompareTo(dtend) <= 0)
            {
                DataRow dr = dtsource.NewRow();
                dr["天数"] = dtnow.ToString("MM-dd");
                dr["中值"] = 0;
                dr["上限"] = 0;
                dr["下限"] = 0;
                i++;
                //foreach (DataRow dr1 in dt.Rows)
                //{
                //    dr[dr1["FName"].ToString()] = 0;
                //}
                dtsource.Rows.Add(dr);
                dtnow = dtnow.AddDays(1);
            }
        //    int count = 0;
        //    double t = 0;
        //    double _v = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string date = dr["FDate"].ToString();
                string v = dr["FValue"].ToString();
                string name =  dr["FName"].ToString();
                string[] narray = date.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] varray = v.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

           
                for (int j = 0; j < narray.Length; j++)
                { 
                    dtnow = dtstart;
                    i = 0;
                    bool flag = false;
                    while (dtnow.CompareTo(dtend) <= 0)
                    {
                        if (dtnow.ToString("MM-dd") == narray[j])
                        {
                            flag = true;
                            break;
                        }
                        i++;
                        dtnow = dtnow.AddDays(1);
                    }
                    if (flag)
                    {
                        dtsource.Rows[i][name] = varray[j];
                        //dtsource.Rows[i]["天数"] = "123";// narray[i];
                        //.ToString() + narray[j].ToString()  varray[j]
                    }
                }
            }
            for (int z = 0; z < dtsource.Rows.Count; z++)
            {
                DataRow dr = dtsource.Rows[z];
                dr["中值"] = davg;
                dr["上限"] = dmax;
                dr["下限"] = dmin;
            }
            //画曲线
            DrawPic(dtsource);
            //Store store1 = this.Chart2.GetStore();
            //store1.DataSource = dtsource;
            //store1.DataBind();
            //X.Msg.Alert("提示", "查询完毕!").Show();
        }
        else
        {

            X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
            return;
        }

    }
    public void DrawPic(DataTable dt)
    {
        try
        {
            int wid = 0;
            double max = 0;
            double min = 0;
            double tickInter = 0;
     
            List<string> list = new List<string>();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                list.Add("{name:'"+dt.Columns[i].ColumnName+"',data:[");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                //days += "'" + (i+1).ToString() + "',";
                  days += "'" +    dr["天数"].ToString() + "',";
            
                for (int z = 1; z < dt.Columns.Count; z++)
                {
                    double w = 0;
                    if(dr[z]!=null&&(!string.IsNullOrEmpty(dr[z].ToString())))
                    {
                        w = double.Parse(dr[z].ToString());

                    }
                    if (w != 0)
                    {
                        if (w > max)
                        {
                            max = w;
                        }

                        if (w < min)
                        {
                            min = w;
                        }
                    }
                    if (w == 0)
                    {
                        list[z - 1] += "null,";
                    }
                    else
                    {
                        list[z - 1] += w.ToString()+",";
                    }
                }
                wid += 40;    
            }

            tickInter = double.Parse(((max - min) / 10).ToString("0.0"));
            if (tickInter == 0)
            {
                tickInter = 0.2;
            }
            tickInterval = tickInter.ToString();
            min = min - tickInter;
            max = max + tickInter;
            ymax = max.ToString();
            ymin = min.ToString();
            series += ",series:[";
            for (int j = 0; j < list.Count;j++ )
            {
                string line = list[j];
                if (line.Length > 0)
                {
                    line = line.Substring(0, line.Length - 1);
                    line += "]}";
                    series += line+",";
                }
                
            }
            if (series.Length > 10)
            {
                series = series.Substring(0, series.Length - 1);
            }
            series += "]";
            if (days != "")
            {
                days = days.Substring(0, days.Length - 1);
            }
            if (wid <= 400)
            {
                wid = 400;
            }
            width = wid.ToString();
            //ResourceManager1.AddScript("<script>newchart();</script>");
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "newchart();", true);
        }
        catch
        {

            //this.ddlmt.Visible = false;
            //this.ddlmt.Items.Clear();
        }
    }
}