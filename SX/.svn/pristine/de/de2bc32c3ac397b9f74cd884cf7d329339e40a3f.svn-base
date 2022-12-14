using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Data;
using System.Text;

public partial class Manager_ProducingPlan_BarcodeScan_BarCodeMissScan : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPptShiftManager pptShiftManaer = new PptShiftManager();
    private IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    private IPptClassManager pptClassManager = new PptClassManager();
    private IPmtConfigManager pmtconfig = new PmtConfigManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            修改 = new SysPageAction() { ActionID = 2, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtStratShiftDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillShift();
        }
    }

    #region 页面初始化
    private void FillShift()
    {
        EntityArrayList<PptShift> lstShift = pptShiftManaer.GetListByWhere(PptShift._.UseFlag == 1);
        if (lstShift.Count >= 0)
        {
            foreach (PptShift shift in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = shift.ShiftName;
                item.Value = shift.ObjID.ToString();
                cboShift.Items.Add(item);
            }
        }
        EntityArrayList<PptClass> lstClass = pptClassManager.GetListByWhere(PptClass._.UseFlag == 1);
        if (lstClass.Count >= 0)
        {
            foreach (PptClass classes in lstClass)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = classes.ClassName;
                item.Value = classes.ObjID.ToString();
                cboClass.Items.Add(item);
            }
        }



        Ext.Net.ListItem Item = new Ext.Net.ListItem();
        Item.Text = "漏扫";
        Item.Value = "0";
       
        ComboBox1.Items.Add(Item);
        Item = new Ext.Net.ListItem();
        Item.Text = "自补";
        Item.Value = "1";
        ComboBox1.Items.Add(Item);
        Item = new Ext.Net.ListItem();
        Item.Text = "手补";
        Item.Value = "2";
        ComboBox1.Items.Add(Item);
        ComboBox1.SetValue("0");
        
    }
    #endregion

    #region 分页相关方法
    private PageResult<PptLotData> GetPageResultData(PageResult<PptLotData> pageParams)
    {
        PptLotDataManager.QueryParams queryParams = new PptLotDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.BeginTime = Convert.ToDateTime(txtStratShiftDate.Text).ToString("yyyy-MM-dd");
        if (this.txtEquipCode.Text != null)
        {
            queryParams.EquipCode = this.hidden_select_equip_code.Text;
        }
        if (this.cboShift.SelectedItem.Value != null)
        {
            queryParams.ShiftID = this.cboShift.SelectedItem.Value;
        }
        if (this.cboClass.SelectedItem.Value != null)
        {
            queryParams.ClassID = this.cboClass.SelectedItem.Value;
        }
        EntityArrayList<PmtConfig> list = pmtconfig.GetListByWhere(PmtConfig._.TypeCode == "BarCodeMissScan");
        string sqlwhere = "";
        foreach (PmtConfig pmt in list)
        {
            if (!string.IsNullOrEmpty(pmt.ItemInfo))
            {
                sqlwhere += pmt.ItemInfo;
            }
        }
        return GetBarCodeScannPageDataBySql(queryParams, sqlwhere);
    }


    public PageResult<PptLotData> GetBarCodeScannPageDataBySql(PptLotDataManager.QueryParams queryParams, string sqlwhere)
    {
        PageResult<PptLotData> pageParams = queryParams.PageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT d.Memo,b.EquipName,s.ShiftName,c.ClassName,p.MaterName,p.SerialBatchID,convert(varchar, p.StartDatetime, 120) StartDatetime,
            p.Barcode,w.WeightID,w.MaterName WMaterName,w.SetWeight,w.RealWeight,w.MaterBarcode,w.ErrorAllow,w.ErrorOut, CASE WHEN  w.WarningSgn='0' THEN '不报警' WHEN w.WarningSgn='1' THEN '报警' END WarningSgn
            ,CASE WHEN w.MaterQua='1' THEN '合格' WHEN w.MaterQua='2' THEN '不合格' WHEN w.MaterQua='3' THEN '检验' WHEN w.MaterQua='4' THEN '未达到停放时间' END MaterQua
            ,w.WeighTime,w.WeighType,CASE WHEN w.WeighState='0' THEN '手动' WHEN w.WeighState='1' THEN '自动' END WeighState
            FROM dbo.PptLotData p INNER JOIN dbo.PptWeighData w
            ON p.Barcode = w.Barcode LEFT JOIN dbo.BasEquip b ON p.EquipCode=b.EquipCode
            LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID
            LEFT JOIN dbo.PptClass c ON c.ObjID=p.ClassID
left join ppt_NoscanInfo d on p.barcode =d.barcode 
            WHERE 1=1  ");
        if (ComboBox1.SelectedItem.Value == "0" || ComboBox1.SelectedItem.Value == "-1")
        { sqlstr.AppendLine(@" and   isnull(w.MaterBarcode,'')='' "); }
        if (ComboBox1.SelectedItem.Value == "1")
        { sqlstr.AppendLine(@" and w.FlagAmend = '1' "); }
        if (ComboBox1.SelectedItem.Value == "2")
        { sqlstr.AppendLine(@" and  w.FlagAmend = '2' "); }
        if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
        {
            sqlstr.AppendLine(@"AND p.EquipCode='" + queryParams.EquipCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
        {
            sqlstr.AppendLine(@"AND p.PlanDate='" + queryParams.BeginTime + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.ClassID))
        {
            sqlstr.AppendLine(@"AND p.ClassID='" + queryParams.ClassID + "'");
        }
        //if (!string.IsNullOrEmpty(sqlwhere))
        //{
        //    sqlstr.AppendLine(sqlwhere);
        //}
        if (!string.IsNullOrWhiteSpace(queryParams.ShiftID))
        {
            sqlstr.AppendLine(@"AND p.ShiftID='" + queryParams.ShiftID + "'");
        }
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pptLotDataManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            sqlstr.AppendLine(@"ORDER BY p.EquipCode");
            pageParams.QueryStr = sqlstr.ToString();
          
            return pptLotDataManager.GetPageDataByReader(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "PlanDate ASC";
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion


    [DirectMethod]
    public static object GetData(Dictionary<string, string> parameters)
    {
        return new
        {
            index = parameters["index"],
            Barcode = parameters["Barcode"],
            WeightID = parameters["WeightID"],
            WMaterName = parameters["WMaterName"],
            SetWeight = parameters["SetWeight"],
            RealWeight = parameters["RealWeight"],
            MaterBarcode = parameters["MaterBarcode"],
            ErrorAllow = parameters["ErrorAllow"],
            ErrorOut = parameters["ErrorOut"],
            WarningSgn = parameters["WarningSgn"],
            MaterQua = parameters["MaterQua"],
            WeighTime = parameters["WeighTime"],
            WeighType = parameters["WeighType"],
            WeighState = parameters["WeighState"]
        };
    }

     [Ext.Net.DirectMethod()]
    public string btnBatchSend_Click()
    {
        String Fcode = modify_fbarcode.Text.Trim();
        String Scode = modify_sbarcode.Text;
        String SMaterName = modify_matername.Text.Trim();
        String RealWeight = modify_weight.Text.Trim();
        PptLotData PL = pptLotDataManager.GetListByWhere(PptLotData._.Barcode==Fcode)[0];
        String FMaterCode = PL.MaterCode;
        String FShelfCode = PL.ShelfBarcode;
        String SMaterCode = "";


        String sql = "select * from ppt_NoscanInfo where barcode = '" + Fcode + "'";
         
           DataSet ds = pptShiftManaer.GetBySql(sql).ToDataSet();
           if (ds.Tables[0].Rows.Count > 0)
           { sql = "update ppt_NoscanInfo set memo = '" + TextYY.Text + "' where barcode = '" + Fcode + "'";
           ds = pptShiftManaer.GetBySql(sql).ToDataSet();
           }
           else

           { sql = "insert into ppt_NoscanInfo values ('" + Fcode + "','"+PL.PlanDate+"','"+
              PL.ClassID + "','" + PL.EquipCode + "','" + PL.MaterCode + "','" + PL.SerialID + "','" + TextYY.Text + "',getdate())"; ds = pptShiftManaer.GetBySql(sql).ToDataSet();
           }

       this.AppendWebLog("条码追溯补充", "条码号：" + Fcode + "  " + Scode);
           winModify.Close();
           pageToolBar.DoRefresh();
           return "条码补充成功!";








           #region 验证
           if ((Scode.Length != 25) && (Scode.Length != 21) && (Scode.Length != 15) && (Scode.Length != 16)) {

               return "条码长度不合法";
           
           }
           sql = "select * from PptWeigh where barcode = '" + Fcode + "' and mater_name = '" + SMaterName + "'";
           ds = pptShiftManaer.GetBySql(sql).ToDataSet();
           String Mater_barcode = ds.Tables[0].Rows[0]["Mater_barcode"].ToString();
           if (!string.IsNullOrEmpty(Mater_barcode))
           { return "已有追溯信息，不需要补充！"; }


           #region 验证物料
           if (Scode.Length == 21)
           {
               sql = "select * from PstShopStorage where barcode = '" + Scode + "'";
               ds = pptShiftManaer.GetBySql(sql).ToDataSet();
               SMaterCode = ds.Tables[0].Rows[0]["MaterCode"].ToString();

               sql = "select * from BasMaterial where materialname = '" + SMaterName + "'";
               ds = pptShiftManaer.GetBySql(sql).ToDataSet();
               String MaterialCode = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
               String MaterialGroup = ds.Tables[0].Rows[0]["MaterialGroup"].ToString();
               int MaterialLevel = int.Parse(ds.Tables[0].Rows[0]["MaterialLevel"].ToString());
               sql = "select * from BasMaterial where materialcode = '" + SMaterCode + "'";
               ds = pptShiftManaer.GetBySql(sql).ToDataSet();
               String SMaterialGroup = ds.Tables[0].Rows[0]["MaterialGroup"].ToString();
               int SMaterialLevel = int.Parse(ds.Tables[0].Rows[0]["MaterialLevel"].ToString());

               if ((SMaterCode == MaterialCode) || ((SMaterialGroup == MaterialGroup) && (SMaterialLevel <= MaterialLevel)))
               { }
               else
               { return "补充条码物料信息不正确！"; }


           }
           else
           {
               sql = "select * from PPTCheBarCodeUseSJList where   chebarcode = '" + Scode + "'";
               ds = pptShiftManaer.GetBySql(sql).ToDataSet();
               if (ds.Tables[0].Rows.Count == 0)
               {
                   return "补充条码没有追溯信息，请先维护补充条码的追溯信息！";
               
               }
    
           }
           #endregion 



           #endregion 
           #region 扫描天然胶
        
       
        if (Scode.Length == 21)
        {
          //  sql = "select * from PstShopStorage where barcode = '" + Scode + "'";
          //  ds = pptShiftManaer.GetBySql(sql).ToDataSet();
          //SMaterCode= ds.Tables[0].Rows[0]["MaterCode"].ToString();
         
       
             
         sql = "select * from pptbarbom where F_barcode ='" + Fcode + "' and S_barcode='" + Scode + "'";
         ds = pptShiftManaer.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        { }
        else
        { 
            
            sql =String.Format(@" insert into pptbarbom values ('{0}','{1}','{2}','{3}',convert(varchar(19),getdate(),120),null,'{4}','{5}','{6}',null) ",Fcode,
                Scode, Scode, RealWeight, FMaterCode, SMaterCode, SMaterCode);
         
        pptShiftManaer.GetBySql(sql).ToDataSet();}
        sql ="update pptweigh set mater_barcode = '" + Scode + "'  ,FlagAmend = '2' where barcode = '" + Fcode + "' and mater_name ='" + SMaterName + "'";
         pptShiftManaer.GetBySql(sql).ToDataSet();
        }

            #endregion 

        #region 扫描车条码
        if (Scode.Length == 16 || Scode.Length == 15)
        {

            sql = "select * from PptLot where barcode = '" + Scode + "'";
            ds = pptShiftManaer.GetBySql(sql).ToDataSet();
          SMaterCode= ds.Tables[0].Rows[0]["Mater_Code"].ToString();
        String  ShelfCode = ds.Tables[0].Rows[0]["Shelf_Braocde"].ToString();
             sql = "select * from pptbarbom where F_barcode ='" + Fcode + "' and S_barcode='" + Scode + "'";
         ds = pptShiftManaer.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        { }
        else
        { 
            
            sql =String.Format(@" insert into pptbarbom values ('{0}','{1}','{2}','{3}',convert(varchar(19),getdate(),120),null,'{4}','{5}','{6}',null) ",Fcode,
                Scode, ShelfCode, RealWeight, SMaterName, SMaterCode, SMaterCode); 
        pptShiftManaer.GetBySql(sql).ToDataSet();}
        sql = "update pptweigh set mater_barcode = '" + Scode + "'  ,FlagAmend = '2' where bacode = '" + Fcode + "' and mater_name ='" + SMaterName + "'";
         pptShiftManaer.GetBySql(sql).ToDataSet();
       

        }
          #endregion 

        #region PPTCheBarCodeUseSJList

        sql = "select * from PPTCheBarCodeUseSJList where barcode = '" + FShelfCode + "' and  chebarcode = '" + Fcode + "'";
        ds = pptShiftManaer.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Scode.Length == 21)
            {
                sql = "select * from PstShopStorage where barcode = '" + Scode + "'";
         
                SMaterCode = pptShiftManaer.GetBySql(sql).ToDataSet().Tables[0].Rows[0]["MaterCode"].ToString();

                if (SMaterName.IndexOf("塑解剂") > 0)
                {
                    sql = String.Format(@" update PPTCheBarCodeUseSJList set  SJJName ='{0}', SJJWeight = '{1}' 
where chebarcode = '{3}' and barcode = '{4}'",
                  Scode, RealWeight,       Fcode, FShelfCode);

                    pptShiftManaer.GetBySql(sql).ToDataSet();
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["SJName"].ToString().IndexOf(SMaterCode) < 0)
                    {
                        sql = "select * from PstShopStorage where barcode = '" + Scode + "'";
                        ds = pptShiftManaer.GetBySql(sql).ToDataSet();
                        String LLProductNo = ds.Tables[0].Rows[0]["LLProductNo"].ToString();
                        sql = String.Format(@" update PPTCheBarCodeUseSJList set  SJName = isnull(SJName,'')+'{0}', SJPHNO =isnull(SJPHNO,'')+ '{1}' 
where chebarcode = '{2}' and barcode = '{3}' ",
                          SMaterCode + ",", SMaterName + " " + LLProductNo + "\n", Fcode, FShelfCode);
                       
                        pptShiftManaer.GetBySql(sql).ToDataSet();
                    }
                }
            
            
            
            }
            else
            { }
        
        
        
        
        }
        else
        {

            if (Scode.Length == 21)
            {
                sql = "select * from PstShopStorage where barcode = '" + Scode + "'";
                ds = pptShiftManaer.GetBySql(sql).ToDataSet();
                String LLProductNo = ds.Tables[0].Rows[0]["LLProductNo"].ToString();




                if (SMaterName.IndexOf("塑解剂") > 0)
                {
                    sql = String.Format(@" insert in to PPTCheBarCodeUseSJList values ('{0}','{1}',' ','{2}','{3}','') ",
                        Fcode, FShelfCode, Scode, RealWeight);

                    pptShiftManaer.GetBySql(sql).ToDataSet();
                }
                else
                {
                    sql = String.Format(@" insert in to PPTCheBarCodeUseSJList values ('{0}','{1}','{2}','',null,'{3}') ",
                        Fcode, FShelfCode, Scode + ",", SMaterName + " " + LLProductNo + "\n");

                    pptShiftManaer.GetBySql(sql).ToDataSet();
                }
            }
            else

            {
                sql = "select * from PPTCheBarCodeUseSJList where chebarcode = '" + Scode + "' order by  len(SJname) desc";
                ds = pptShiftManaer.GetBySql(sql).ToDataSet();
                if (ds.Tables[0].Rows.Count > 0) { 
                String SJName = ds.Tables[0].Rows[0]["SJName"].ToString();
                String SJJName = ds.Tables[0].Rows[0]["SJJName"].ToString();
                String SJJWeight = ds.Tables[0].Rows[0]["SJJWeight"].ToString();
                String SJPHNO = ds.Tables[0].Rows[0]["SJPHNO"].ToString();


                sql = String.Format(@" insert in to PPTCheBarCodeUseSJList values ('{0}','{1}','{2}','{3}','{4}','{5}') ",
                Fcode, FShelfCode, SJName, SJJName, SJJWeight, SJPHNO);

                pptShiftManaer.GetBySql(sql).ToDataSet();
                }
              
            }
        }
        #endregion
     
        this.AppendWebLog("条码追溯补充", "条码号：" + Fcode + "  " + Scode);
        winModify.Close();
        return "条码补充成功!";
    }


     public void BtnCancel_Click(object sender, DirectEventArgs e)
     {
       
         this.winModify.Close();
     }
}