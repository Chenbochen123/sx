using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Entity.Custom;
using System.Web;
using System.Web.Security;
using System.Data;


public class SJ
{
    public string CheckSJ(HttpContext context)
    {
       
        try
        {
            //System.Threading.Thread.Sleep(100000000);
            //tohf();
            //gethfweight();

          
        }
        catch (Exception e2)
        {
           
        }
        return "";
    }
    protected void tohf()
    {
        
        BasMaterialManager manager = new BasMaterialManager();
        String sql = "select * from pptplan where recipeequipcode = '01038' and plandate >='2017-11-15' and tbflag is null and planstate>=2 order by plandate";
        //and tbflag is null
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count == 0)
        {
            //msg.Alert("操作", "无计划！");
            //msg.Show(); 
            return;
        }
        DataRow dr = ds.Tables[0].Rows[0];
        string planid = dr["PlanID"].ToString();
        string RecipeMaterialCode = dr["RecipeMaterialCode"].ToString();
        string RecipeMaterialName = dr["RecipeMaterialName"].ToString();
        string RecipeVerSionID = dr["RecipeVerSionID"].ToString();
        string PlanNum = dr["PlanNum"].ToString();
        string RecipeType = dr["RecipeType"].ToString();
        try
        {
            sql = "update pptplan set tbflag ='1' where planid ='" + planid + "' ";
            ds = manager.GetBySql(sql).ToDataSet();

            sql = "   select * from pmtrecipe where recipeequipcode = '01038' and recipematerialcode = '" + RecipeMaterialCode + "' and recipeversionid = '" + RecipeVerSionID + "' and RecipeType = '" + RecipeType + "'  and auditflag ='1'";
            ds = manager.GetBySql(sql).ToDataSet(); dr = ds.Tables[0].Rows[0];
            String Recipeobjid = dr["ObjID"].ToString();
            String HFFlag = dr["HFFlag"].ToString();
            sql = " select * from PmtTypeToHF where  code = '" + RecipeType + "'";
            DataSet dst = manager.GetBySql(sql).ToDataSet();
            String RecipeTypeName = dst.Tables[0].Rows[0]["Name"].ToString();
            String HFRcode = RecipeMaterialName + "(" + dst.Tables[0].Rows[0]["HFCode"].ToString().Trim() + ")";


            HF.Service ws = new HF.Service();//  插入配方 删除称量信息 插入物料 插入称量信息 更新配方 插入计划
            ws.Url = "http://192.168.1.137:8018/MesToHf/Service.asmx";
//            DataSet hfrecipe = ws.GetData("SELECT * FROM OPENQUERY(HFDB, 'select * from recipes_import where reci_recp_code = ''" + HFRcode + "'' and RECI_RECP_VERSION = " + RecipeVerSionID + "')");
//            if (hfrecipe.Tables[0].Rows.Count == 0)
//            {
//                sql = ("insert into openquery(HFDB,'select RECI_LINE_NAME, RECI_RECP_CODE,RECI_RECP_NAME, RECI_RECP_VERSION, RECI_BLOCKED, RECI_FUNCTION, RECI_HOST_ID ,RECI_RECP_CODE_2 from recipes_import where 1=0') values ('Line01','" + HFRcode + "','" + RecipeMaterialName + RecipeTypeName + "'," + RecipeVerSionID + ", 1, 9, 1,'" + RecipeMaterialCode + "');   "); 
                
//                ws.GetData("insert into openquery(HFDB,'select RECI_LINE_NAME, RECI_RECP_CODE,RECI_RECP_NAME, RECI_RECP_VERSION, RECI_BLOCKED, RECI_FUNCTION, RECI_HOST_ID ,RECI_RECP_CODE_2 from recipes_import where 1=0') values ('Line01','" + HFRcode + "','" + RecipeMaterialName + RecipeTypeName + "'," + RecipeVerSionID + ", 1, 9, 1,'" + RecipeMaterialCode + "');   ");
//            HFFlag="";
//            System.Threading.Thread.Sleep(2000);
//            }

//            hfrecipe = ws.GetData("SELECT * FROM OPENQUERY(HFDB, 'select * from recipes_import where reci_recp_code = ''" + HFRcode + "'' and RECI_RECP_VERSION = " + RecipeVerSionID + " ')");
//            String hfreid = hfrecipe.Tables[0].Rows[0]["RECI_ID"].ToString();


//            if (string.IsNullOrEmpty(HFFlag.Trim()))

//            {
//            sql = (" delete from OPENQUERY(HFDB, 'select * from recipes_cmp_import where rcmp_reci_id = " + hfreid + "');   ");
//            ws.GetData(" delete from OPENQUERY(HFDB, 'select * from recipes_cmp_import where rcmp_reci_id = " + hfreid + "');   ");


//            sql = "select * from pmtrecipeweight where recipeobjid = '" + Recipeobjid + "'";
//            ds = manager.GetBySql(sql).ToDataSet();
//            foreach (DataRow drow in ds.Tables[0].Rows)
//            {
//                sql = "select * from basmaterial where materialcode  = '" + drow["MaterialCode"].ToString() + "'";
//                DataSet dm = manager.GetBySql(sql).ToDataSet();
//                if (dm.Tables[0].Rows.Count == 0)
//                {
                 
//                    continue;
//                }
//                String hfcode = dm.Tables[0].Rows[0]["HFCode"].ToString();
//                if (string.IsNullOrEmpty(hfcode)) hfcode = dm.Tables[0].Rows[0]["ERPCode"].ToString();
//                if (string.IsNullOrEmpty(hfcode)) hfcode = "0";
//                string mtype = "Mes";
//                if (drow["MaterialCode"].ToString().Substring(0, 3) == "104") mtype = "Carbon";
//                if (drow["MaterialCode"].ToString().Substring(0, 1) == "2") mtype = "Chemical";
//                if (drow["MaterialCode"].ToString().Substring(0, 3) == "107") mtype = "Oil";
//                if (drow["MaterialCode"].ToString().Substring(0, 3) == "101") mtype = "Rubber";
//                if (drow["MaterialCode"].ToString().Substring(0, 3) == "102") mtype = "Rubber";
//                if (drow["MaterialCode"].ToString().Substring(0, 3) == "103") mtype = "Rubber";

//                dm = ws.GetData("SELECT * FROM OPENQUERY(HFDB, 'select * from materials_import where MATI_NAME =''" + drow["MaterialCode"].ToString() + "'' ')");
//                if (dm.Tables[0].Rows.Count == 0)
//                {
//                    sql = (@"insert into openquery(HFDB,'select MATI_MAT_GROUP,MATI_MAT_CODE,MATI_NAME,MATI_MAT_TYPE,MATI_FUNCTION from materials_import where 1=0') values ('" + mtype + "','" + hfcode + "','" + drow["MaterialCode"].ToString() + "','RAW',9);  ");
//                    ws.GetData(@"insert into openquery(HFDB,'select MATI_MAT_GROUP,MATI_MAT_CODE,MATI_NAME,MATI_MAT_TYPE,MATI_FUNCTION from materials_import where 1=0') values ('" + mtype + "','" + hfcode + "','" + drow["MaterialCode"].ToString() + "','RAW',9);  ");
//                }
//                else
//                {
//                    sql = (@" delete from OPENQUERY(HFDB, 'select * from materials_import  where MATI_NAME =''" + drow["MaterialCode"].ToString() + "'' ');    ");
//                    ws.GetData(@" delete from OPENQUERY(HFDB, 'select * from materials_import  where MATI_NAME =''" + drow["MaterialCode"].ToString() + "'' ');    ");
//                    sql = (@"insert into openquery(HFDB,'select MATI_MAT_GROUP,MATI_MAT_CODE,MATI_NAME,MATI_MAT_TYPE,MATI_FUNCTION from materials_import where 1=0') values ('" + mtype + "','" + hfcode + "','" + drow["MaterialCode"].ToString() + "','RAW',4);  ");
//                    ws.GetData(@"insert into openquery(HFDB,'select MATI_MAT_GROUP,MATI_MAT_CODE,MATI_NAME,MATI_MAT_TYPE,MATI_FUNCTION from materials_import where 1=0') values ('" + mtype + "','" + hfcode + "','" + drow["MaterialCode"].ToString() + "','RAW',4);  ");

//                    //ws.GetData(@"   update OPENQUERY(HFDB, 'select * from materials_import where MATI_NAME=" + drow["MaterialCode"].ToString() + "') set MATI_FUNCTION=4  ,MATI_MAT_CODE  = '" + hfcode + "'");

//                }
//                sql = (@"insert into openquery(HFDB,'select RCMP_RECI_ID,RCMP_MAT_CODE,RCMP_WEIGHT from recipes_cmp_import where 1=0') 
//values (" + hfreid + ",'" + hfcode + "'," + drow["SetWeight"].ToString() + ");  ");
//                ws.GetData(@"insert into openquery(HFDB,'select RCMP_RECI_ID,RCMP_MAT_CODE,RCMP_WEIGHT from recipes_cmp_import where 1=0') 
//values (" + hfreid + ",'" + hfcode + "'," + drow["SetWeight"].ToString() + ");  ");


//            }
           
//            sql = ("   update OPENQUERY(HFDB, 'select * from recipes_import where RECI_ID= " + hfreid + "') set RECI_FUNCTION=4  ");
//            ws.GetData(sql);


//            sql = "update pmtrecipe set hfflag ='2' where objid ='" + Recipeobjid + "' ";
//            ds = manager.GetBySql(sql).ToDataSet();



//        }
            sql = (@" delete from OPENQUERY(HFDB, 'select * from prod_orders_imp  where pori_order_number =''" + planid + "'' ');    ");
            ws.GetData(sql);
            sql = (@"  insert into openquery(HFDB,'select pori_line_name, pori_order_number, pori_recipe_code, pori_recipe_version, 
                                              pori_batch_quantity_set, pori_order_weight, pori_pror_blocked, pori_function, 
                                              pori_host_id from prod_orders_imp where 1=0') 
values ('Line01','" + planid + "','" + HFRcode + "'," + RecipeVerSionID + "," + PlanNum + ", 0, 0, 9, 1);  ");
            ws.GetData(sql);



            System.Threading.Thread.Sleep(2000);


            sql = (@" SELECT *  from OPENQUERY(HFDB, 'select * from prod_orders_imp  where pori_order_number =''" + planid + "'' ');    ");
            ds = ws.GetData(sql);

            String RECI_STATUS = ds.Tables[0].Rows[0]["PORI_STATUS"].ToString();
            if (RECI_STATUS == "1")
            {
                sql = "update pptplan set tbflag ='2' where planid ='" + planid + "' ";
                ds = manager.GetBySql(sql).ToDataSet();
            }

            else
            {
                sql = "update pptplan set planstate='1' where planid ='" + planid + "' ";
                ds = manager.GetBySql(sql).ToDataSet();
            }




          
            //msg.Alert("操作", "导入HF成功！");
            //msg.Show();
        }

        catch (Exception ex)
        {

            sql = sql.Replace("'", "''");// ex.Message.ToString() 

            string Esql = @"insert into SysWebLog (remark,methodresult,recordtime,UserCode,MethodID)
values('HF','失败 " + sql + planid + "',getdate(),'000001','1')";
            manager.GetBySql(Esql).ToDataSet();



        }

    }

    protected void gethfweight()
    {
        BasMaterialManager manager = new BasMaterialManager();
        String sql = "SELECT * FROM OPENQUERY(HFDB, 'select * from materials_Consumption where maco_date>''" + DateTime.Now.AddHours(-1).ToString("yyyy-mm-dd hh:MMM:ss") + " '' and maco_order_stage is null')";
        try
        {
            HF.Service ws = new HF.Service();
        ws.Url = "http://192.168.1.137:8018/MesToHf/Service.asmx";
        DataSet hfrecipe = ws.GetData(sql);
     if (hfrecipe.Tables[0].Rows.Count == 0) return;
     foreach (DataRow dr in hfrecipe.Tables[0].Rows)
     {
         ws.GetData("   update OPENQUERY(HFDB, 'select * from materials_Consumption where MACO_ID= " + dr["MACO_ID"].ToString() + "') set maco_order_stage=1 ");
         string hfcode = dr["MACO_MAT_CODE"].ToString();
         String materialcode="";
         sql = "select * from Basmaterial where hfcode =  '" + hfcode + "'";

         DataSet ds = manager.GetBySql(sql).ToDataSet();
         if (ds.Tables[0].Rows.Count == 0)
         {
             return;

         }
         else
         { materialcode = ds.Tables[0].Rows[0]["MaterialCode"].ToString(); }


         String stockKW = "";
         sql = "   select * from BasEquipStorage where Equipcode = '01038' ";
         ds = manager.GetBySql(sql).ToDataSet(); DataRow drkw = ds.Tables[0].Rows[0];
         if (materialcode.Substring(0, 1) == "2")
         {

             stockKW = drkw["XLStockKW"].ToString();
         }
         else
             if (materialcode.Substring(0, 3) == "101" || materialcode.Substring(0, 3) == "102" || materialcode.Substring(0, 3) == "103")
             {

                 stockKW = drkw["SJStockKW"].ToString();
             }
             else
             { stockKW = drkw["YLStockKW"].ToString(); }



         String barcode = "";

         if (materialcode.Substring(0, 1) == "2")
         {

             sql = @"
select HFWS1.* from HFWS1 inner join PpmRubberStorage on HFWS1.barcode= PpmRubberStorage.barcode
where PpmRubberStorage.realweight>0  and storageplaceid='" + stockKW + "' order by HFWS1.recordtime";
                                                        
             ds = manager.GetBySql(sql).ToDataSet();
             if (ds.Tables[0].Rows.Count == 0)
             {
                 return;

             }
             barcode = ds.Tables[0].Rows[0]["Barcode"].ToString();
         
         }
         else
         {


             sql = @"select HFWS1.* from HFWS1 inner join PstshopStorage on HFWS1.barcode= PstshopStorage.barcode
where PstshopStorage.realweight>0  and storageplaceid='" + stockKW + "' order by HFWS1.recordtime";

ds = manager.GetBySql(sql).ToDataSet();
if (ds.Tables[0].Rows.Count == 0)
{
    return;

}
         barcode=ds.Tables[0].Rows[0]["Barcode"].ToString();
         
         
         
         }

         if (String.IsNullOrEmpty(barcode)) return;
         sql = "  update PpmRubberStorage  set RealWeight =RealWeight - " + dr["MACO_CONSUMED _QUANTITY"].ToString() + "where barcode ='' and storageplaceid = '" + stockKW + "' ";
         ds = manager.GetBySql(sql).ToDataSet();
     }
            

           }

        catch (Exception ex)
        {

            sql = sql.Replace("'", "''");// ex.Message.ToString() 

            string Esql = @"insert into SysWebLog (remark,methodresult,recordtime,UserCode,MethodID)
values('HF','失败 " + sql  + "',getdate(),'000001','1')";
            manager.GetBySql(Esql).ToDataSet();



        }




    }

}
