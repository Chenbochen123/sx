﻿using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;


/// <summary>
/// Manager_Technology_Report_LotReport 实现类
/// 孙本强 @ 2013-04-03 13:17:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Report_LotReport : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public string[] COLORS = new string[] { "#ff0000", "#00ff00", "#0000FF", "#000000", "#999999", "#9900FF" };
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch,btnSearchFirst,btnSearchPrevious,btnSearchNext,btnSearchLast," };
            导出折线图 = new SysPageAction() { ActionID = 2, ActionName = "btnSaveChart" };
            打印折线图 = new SysPageAction() { ActionID = 3, ActionName = "btnPrintChart" };
            当前车详细信息 = new SysPageAction() { ActionID = 4, ActionName = "btnShowRptPmtLotInfo" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出折线图 { get; private set; } //必须为 public
        public SysPageAction 打印折线图 { get; private set; } //必须为 public
        public SysPageAction 当前车详细信息 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptClassManager pptClassManager = new PptClassManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptShiftManager pptShiftManager = new PptShiftManager();
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPpt_curvedataManager curveManager = new Ppt_curvedataManager("Curve");
    /// <summary>
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    #endregion


    #region 页面初始化
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// 初始化ComboBox
    /// 孙本强 @ 2013-04-03 13:38:59
    /// </summary>
    /// <param name="cb">The cb.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cb.Items.Clear();
        cb.Items.Add(allitem);
        foreach (SysCode m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(m.ItemName, m.ItemCode.ToString());
            cb.Items.Add(item);
        }
        if (cb.Items.Count > 0)
        {
            cb.Text = (cb.Items[0].Value);
        }
    }
    private void btnDisabled()
    {
        btnSearchFirst.Disabled = true;
        btnSearchPrevious.Disabled = true;
        btnSearchNext.Disabled = true;
        btnSearchLast.Disabled = true;
        int istart = 0;
        int iend = 0;
        int icount = 0;
        int index = 0;

        int.TryParse(txtLotBeginIndex.Text, out istart);
        int.TryParse(txtLotEndIndex.Text, out iend);
        int.TryParse(txtLotCount.Text, out icount);
        int.TryParse(txtLotIndex.Text, out index);

        if (icount == 0)
        {
            return;
        }
        btnSearchFirst.Disabled = false;
        btnSearchLast.Disabled = false;
        if (index > 1)
        {
            btnSearchPrevious.Disabled = false;
        }
        if (index < icount)
        {
            btnSearchNext.Disabled = false;
        }

    }
    /// <summary>
    /// 页面初始化
    /// 孙本强 @ 2013-04-03 13:39:51
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        btnDisabled();
        txtBeginDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        txtBeginTime.Text = "00:00:00";
        txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        txtEndTime.Text = "00:00:00";
        txtLotBeginIndex.Text = "1";
        txtLotEndIndex.Text = "1";
        txtLotIndex.Text = "0";
        txtLotCount.Text = "0";
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptClass._.UseFlag == "1");
        order = PptClass._.ObjID.Asc;
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        txtPptClass.Items.Clear();
        txtPptClass.Items.Add(allitem);
        foreach (PptClass m in pptClassManager.GetListByWhereAndOrder(where, order))
        {
            txtPptClass.Items.Add(new ListItem(m.ClassName, m.ObjID));
        }
        txtPptClass.Text = (txtPptClass.Items[0].Value);

        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptShift._.UseFlag == "1");
        order = PptShift._.ObjID.Asc;
        txtPptShift.Items.Clear();
        txtPptShift.Items.Add(allitem);
        foreach (PptShift m in pptShiftManager.GetListByWhereAndOrder(where, order))
        {
            txtPptShift.Items.Add(new ListItem(m.ShiftName, m.ObjID));
        }
        txtPptShift.Text = (txtPptShift.Items[0].Value);
    }
    /// <summary>
    /// 生成红色Html标示
    /// 孙本强 @ 2013-04-03 13:40:06
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// 默认Html标示
    /// 孙本强 @ 2013-04-03 13:40:16
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion
    /// <summary>
    /// Inis the main info.
    /// 孙本强 @ 2013-04-03 13:17:24
    /// </summary>
    /// <param name="PptList">The PPT list.</param>
    /// <remarks></remarks>
    private void IniMainInfo(PageResult<PptLotData> PptList)
    {
        string barcode = string.Empty;
        if (PptList.DataSet.Tables.Count > 0 && PptList.DataSet.Tables[0].Rows.Count > 0)
        {
            barcode = PptList.DataSet.Tables[0].Rows[0]["Barcode"].ToString();
        }
        DataSet ds = pptLotDataManager.GetLotInfoByBarcode(barcode);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];
            txtLotIndex.Text = PptList.PageIndex.ToString();

            txtShowBarcode.Text = row["Barcode"].ToString();   //条码号
            txtShowShift.Text = row["ShiftName"].ToString();   //班次
            txtShowClass.Text = row["ClassName"].ToString();   //班组
            txtShowRecipe.Text = row["MaterialName"].ToString();   //配方

            txtShowRecipeState.Text = row["RecipeStateName"].ToString();   //配方状态
            txtShowStartDatetime.Text = row["StartDatetime"].ToString();   //开始时间

            txtShowRecipeTime.Text = row["DoneAllRTime"].ToString();   //配方时间
            txtShowLotEnergy.Text = row["LotEnergy"].ToString();   //累计能量
            txtShowPjTemp.Text = row["PjTemp"].ToString();   //排胶温度

            txtShowSerialBatchID.Text = row["PlanNum"].ToString();   //设定车数
            txtShowSerialID.Text = row["SerialID"].ToString();   //车次
            txtShowDoneRtime.Text = row["DoneRtime"].ToString();   //炼胶时间

            txtShowPolyDisTime.Text = row["PolyDisTime"].ToString();   //加胶时间
            txtShowBwbTime.Text = row["BwbTime"].ToString();   //间隔时间


            txtZhuanzi.Text = row["PowderDisTime"].ToString();   
            txtCebi.Text = row["PowderBatch"].ToString();   
            txtXieliaomen.Text = row["SmallBatch"].ToString();   

        }
        else
        {
            txtLotIndex.Text = (PptList.PageIndex - 1).ToString();
            txtShowBarcode.Text = string.Empty;   //条码号
            txtShowShift.Text = string.Empty;   //班次
            txtShowClass.Text = string.Empty;   //班组
            txtShowRecipe.Text = string.Empty;   //配方

            txtShowRecipeState.Text = string.Empty;   //配方状态
            txtShowStartDatetime.Text = string.Empty;   //开始时间

            txtShowRecipeTime.Text = string.Empty;   //配方时间
            txtShowLotEnergy.Text = string.Empty;   //累计能量
            txtShowPjTemp.Text = string.Empty;   //排胶温度

            txtShowSerialBatchID.Text = string.Empty;   //设定车数
            txtShowSerialID.Text = string.Empty;   //车次
            txtShowDoneRtime.Text = string.Empty;   //炼胶时间

            txtShowPolyDisTime.Text = string.Empty;   //加胶时间
            txtShowBwbTime.Text = string.Empty;   //间隔时间

            txtZhuanzi.Text =string.Empty;
            txtCebi.Text =string.Empty;
            txtXieliaomen.Text = string.Empty;
        }
    }
    /// <summary>
    /// 初始化图表
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="PptList">The PPT list.</param>
    /// <remarks></remarks>
    private void IniChartInfo(PageResult<PptLotData> PptList)
    {
        try
        {
            string barcode = string.Empty;
            if (PptList.DataSet.Tables.Count > 0 && PptList.DataSet.Tables[0].Rows.Count > 0)
            {
                barcode = PptList.DataSet.Tables[0].Rows[0]["Barcode"].ToString();
            } 
            txtLotBeginIndex.Text = PptList.PageIndex.ToString();
            txtLotCount.Text = PptList.RecordCount.ToString();
            List<PptCurve> data = new List<PptCurve>();
          
                
            if (!string.IsNullOrWhiteSpace(barcode))
            {


                EntityArrayList<Ppt_curvedata> lst = curveManager.GetTopNListWhereOrder(1, Ppt_curvedata._.Barcode == barcode, Ppt_curvedata._.Barcode.Asc);

            
                
                if (lst.Count > 0)
                {


                    Ppt_curvedata m = lst[0];
                  
             
                    data = IniPptCurveList(m);
                }
            }
        
            Store store = this.Chart1.GetStore();
            store.DataSource = data;
            store.DataBind();
        }
        catch (Exception ex)
        {
            X.Js.Alert(ex.Message.ToString()); return;
         
            
            List<PptCurve> data = new List<PptCurve>();
            Store store = this.Chart1.GetStore();
            store.DataSource = data;
            store.DataBind();
        }
    }
    public List<PptCurve> IniPptCurveList(Ppt_curvedata data)
    {
        List<PptCurve> Result = new List<PptCurve>();
        string ss = data.Mixing_Time;
        int count = ss.Split(':').Length;
      
        for (int i = 0; i < count; i++)
        {
            try
            {
                PptCurve p = new PptCurve();
                p.Barcode = data.Barcode;
                p.CurveData = data.Curve_data;
                p.PlanDate = data.Plan_date;
                p.PlanID = data.Plan_id;
                p.SerialID = data.Serial_id == null ? string.Empty : ((int)data.Serial_id).ToString();
                p.IfSubed = data.If_Subed == null ? string.Empty : data.If_Subed;
                p.SecondSpan = Convert.ToInt32(data.Mixing_Time.Split(':')[i]);
                p.MixingTime = ((DateTime)data.Start_datetime).AddSeconds(p.SecondSpan);
                if (!String.IsNullOrEmpty(data.Mixing_Temp))
                {
                    p.MixingTemp = Convert.ToDecimal(data.Mixing_Temp.Split(':')[i]);
                }
                else
                {
                    p.MixingTemp = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Power))
                {
                    p.MixingPower = Convert.ToDecimal(data.Mixing_Power.Split(':')[i]);
                }
                else
                {
                    p.MixingPower = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Energy))
                {
                    p.MixingEnergy = Convert.ToDecimal(data.Mixing_Energy.Split(':')[i]);
                }
                else
                {
                    p.MixingEnergy = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Press))
                {
                    p.MixingPress = Convert.ToDecimal(data.Mixing_Press.Split(':')[i]);
                }
                else
                {
                    p.MixingPress = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Speed))
                {
                    p.MixingSpeed = Convert.ToDecimal(data.Mixing_Speed.Split(':')[i]);
                }
                else
                {
                    p.MixingSpeed = 0;
                }
                if (!String.IsNullOrEmpty(data.SDS_postion))
                {
                    p.MixingPosition = Convert.ToDecimal(data.SDS_postion.Split(':')[i]);
                }
                else
                {
                    p.MixingPosition = 0;
                }
 
                //if (!String.IsNullOrEmpty(data.Mixing_MixT))
                //{
                //    p.L1 = Convert.ToDecimal(data.Mixing_MixT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L1 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_RotorT))
                //{
                //    p.L2 = Convert.ToDecimal(data.Mixing_RotorT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L2 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_DumpT))
                //{
                //    p.L3 = Convert.ToDecimal(data.Mixing_DumpT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L3 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_Blend))
                //{
                //    p.L4 = Convert.ToDecimal(data.Mixing_Blend.Split(':')[i]);
                //}
                //else
                //{
                //    p.L4 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_Blend2))
                //{
                //    p.L5 = Convert.ToDecimal(data.Mixing_Blend2.Split(':')[i]);
                //}
                //else
                //{
                //    p.L5 = 0;
                //}
                Result.Add(p);
            }
            catch
            (Exception ex) 
            {
                //X.Js.Alert(ex.Message.ToString()); 
            
            }
        }
        return Result;
    }
    /// <summary>
    /// 初始化界面
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="PptList">The PPT list.</param>
    /// <remarks></remarks>
    private void IniFomrInfo(PageResult<PptLotData> PptList)
    {
        IniMainInfo(PptList);
        IniChartInfo(PptList);
        btnDisabled();
    }
    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptLotData> GetPageResultData(PageResult<PptLotData> pageParams)
    {
        pageParams.PageSize = 1;
        if (string.IsNullOrWhiteSpace(txtEquipName.Text))
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请选择机台！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        DateTime beginTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(((DateTime)txtBeginDate.Value).ToString("yyyy-MM-dd") + " " + txtBeginTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        DateTime endTime = DateTime.Now;
        try
        {
            endTime = Convert.ToDateTime(((DateTime)txtEndDate.Value).ToString("yyyy-MM-dd") + " " + txtEndTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return pageParams;
        }
        PptLotDataManager.QueryParams queryParams = new PptLotDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.BeginTime = beginTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.EndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.ClassID = txtPptClass.Text.Replace(constSelectAllText, "");
        queryParams.ShiftID = txtPptShift.Text.Replace(constSelectAllText, "");
        queryParams.PmtRecipe = hiddenPmtRecipeID.Text;
        return pptLotDataManager.GetBarcodeTablePageDataBySql(queryParams);
    }
    /// <summary>
    /// 第一条
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchFirstClick(object sender, DirectEventArgs e)
    {
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = 1;
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        IniFomrInfo(lst);
    }
    /// <summary>
    /// 上一条
    /// 孙本强 @ 2013-04-03 13:17:26
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchPreviousClick(object sender, DirectEventArgs e)
    {
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = Convert.ToInt32(txtLotBeginIndex.Text) - 1;
        if (pageParams.PageIndex < 1)
        {
            pageParams.PageIndex = 1;
        }
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        IniFomrInfo(lst);
    }
    /// <summary>
    /// 下一条
    /// 孙本强 @ 2013-04-03 13:17:26
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchNextClick(object sender, DirectEventArgs e)
    {
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = Convert.ToInt32(txtLotBeginIndex.Text) + 1;
        if (pageParams.PageIndex < 1)
        {
            pageParams.PageIndex = 1;
        }
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        IniFomrInfo(lst);
    }
    /// <summary>
    /// 最后一条
    /// 孙本强 @ 2013-04-03 13:17:26
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchLastClick(object sender, DirectEventArgs e)
    {
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = Convert.ToInt32(txtLotCount.Text);
        if (pageParams.PageIndex < 1)
        {
            pageParams.PageIndex = 1;
        }
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        IniFomrInfo(lst);
    }
}