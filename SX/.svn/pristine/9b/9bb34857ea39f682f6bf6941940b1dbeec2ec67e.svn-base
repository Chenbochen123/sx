using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using NBear.Common;
    using NBear.Data;
    public class BasEquipService : BaseService<BasEquip>, IBasEquipService
    {
        #region 构造方法

        public BasEquipService() : base() { }

        public BasEquipService(string connectStringKey) : base(connectStringKey) { }

        public BasEquipService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法
        public class QueryParams
        {
            public string equipCode { get; set; }
            public string equipName { get; set; }
            public string equipType { get; set; }
            public string WorkShopCode { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string type { get; set; }
            public int page { get; set; }
            public int pagenum { get; set; }
            public PageResult<BasEquip> pageParams { get; set; }
            public string YLStockNO { get; set; }
            public string YLStockKW { get; set; }
            public string SJStockNO { get; set; }
            public string SJStockKW { get; set; }
            public string XLStockNO { get; set; }
            public string XLStockKW { get; set; }
            public string MJStockNO { get; set; }
            public string MJStockKW { get; set; }
            public string ZJStockNO { get; set; }
            public string ZJStockKW { get; set; }
            public string HGStockNO { get; set; }
            public string HGStockKW { get; set; }
            public string NOStockNO { get; set; }
            public string NOStockKW { get; set; }
            public string FLStockNO { get; set; }
            public string FLStockKW { get; set; }
            public string FPStockNO { get; set; }
            public string FPStockKW { get; set; }
            public string THStockNO { get; set; }
            public string THStockKW { get; set; }
        }

        public PageResult<BasEquip> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasEquip> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            //sqlstr.AppendLine(@" SELECT	    equip.ObjID, equip.EquipCode,equipType.EquipTypeName AS EquipType, EquipName, 
            //                                EquipIP, EquipGroup, shop.WorkShopName AS WorkShopCode, equip.SubFac, 
            //                                equip.MixEquipType, syscode.ItemName AS IsOneMix, equip.LEDIP, equip.AreaCode,
            //                                equip.MixType, syscode2.ItemName AS IsFinalMixing ,  syscode3.ItemName AS IsProEnvironment,
            //                                equip.Remark , equip.DeleteFlag , u.UserName AS RepairUser
            //                     FROM	    BasEquip equip
            //                     LEFT JOIN  BasEquipType equipType   ON  equip.EquipType = equipType.ObjID
            //                     LEFT JOIN  BasWorkShop  shop        ON  equip.WorkShopCode = shop.ObjID   
            //                     LEFT JOIN  SysCode syscode          ON  equip.IsOneMix = syscode.ItemCode  AND syscode.TypeID='YesNo'  
            //                     LEFT JOIN  SysCode syscode2         ON  equip.IsFinalMixing = syscode2.ItemCode  AND syscode2.TypeID='IsFinalMixing'   
            //                     LEFT JOIN  SysCode syscode3         ON  equip.IsProEnvironment = syscode3.ItemCode  AND syscode3.TypeID='YesNo'  
            //                     LEFT JOIN  BasUser u             ON  equip.RepairUser = u.WorkBarcode     
            //                     WHERE      1 = 1");
            sqlstr.AppendLine(@"SELECT	    equip.ObjID, equip.EquipCode,equipType.EquipTypeName AS EquipType, EquipName, 
                                            EquipIP, EquipGroup, shop.WorkShopName AS WorkShopCode, equip.SubFac, 
                                            equip.MixEquipType,  equip.LEDIP, equip.AreaCode,
                                            equip.MixType, 
                                            equip.Remark , equip.DeleteFlag , u.UserName AS RepairUser
                                 FROM	    BasEquip equip
                                 LEFT JOIN  BasEquipType equipType   ON  equip.EquipType = equipType.ObjID
                                 LEFT JOIN  BasWorkShop  shop        ON  equip.WorkShopCode = shop.ObjID    
                                 LEFT JOIN  BasUser u             ON  equip.RepairUser = u.WorkBarcode     
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND equip.EquipCode    like '%" + queryParams.equipCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipType))
            {
                sqlstr.AppendLine(" AND equip.EquipType    like '%" + queryParams.equipType + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.WorkShopCode))
            {
                sqlstr.AppendLine(" AND equip.WorkShopCode    = " + queryParams.WorkShopCode);
            }
            if (!string.IsNullOrEmpty(queryParams.equipName))
            {
                sqlstr.AppendLine(" AND equip.EquipName    like '%" + queryParams.equipName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND equip.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND equip.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

        public string GetNextEquipCode(string equipTypeCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" SELECT MAX(EquipCode) + 1 AS EquipCode FROM BasEquip WHERE EquipCode LIKE '" + equipTypeCode + "%'");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = equipTypeCode + "001";
            }
            return temp.PadLeft(5, '0');
        }
        public DataSet EquipStorageQuery(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_EquipStorageQuery");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.equipCode.GetType()), queryParams.equipCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipType"), this.TypeToDbType(queryParams.equipType.GetType()), queryParams.equipType);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipName"), this.TypeToDbType(queryParams.equipName.GetType()), queryParams.equipName);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("Chejian"), this.TypeToDbType(queryParams.WorkShopCode.GetType()), queryParams.WorkShopCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StorageID"), this.TypeToDbType(queryParams.storageID.GetType()), queryParams.storageID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StoragePlaceID"), this.TypeToDbType(queryParams.storagePlaceID.GetType()), queryParams.storagePlaceID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("Type"), this.TypeToDbType(queryParams.type.GetType()), queryParams.type);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("page"), this.TypeToDbType(queryParams.page.GetType()), queryParams.page);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("pagenum"), this.TypeToDbType(queryParams.pagenum.GetType()), queryParams.pagenum);
            return sps.ToDataSet();
            //return queryParams.pageParams;
        }

        public DataSet EquipStorageQueryByCode(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_EquipStorageByCode");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.equipCode.GetType()), queryParams.equipCode);
            return sps.ToDataSet();
        }
        public DataSet UpdateEquipStorage(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_UpdateEquipStorage");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.equipCode.GetType()), queryParams.equipCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("YLStockNO"), this.TypeToDbType(queryParams.YLStockNO.GetType()), queryParams.YLStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("YLStockKW"), this.TypeToDbType(queryParams.YLStockKW.GetType()), queryParams.YLStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("SJStockNO"), this.TypeToDbType(queryParams.SJStockNO.GetType()), queryParams.SJStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("SJStockKW"), this.TypeToDbType(queryParams.SJStockKW.GetType()), queryParams.SJStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("XLStockNO"), this.TypeToDbType(queryParams.XLStockNO.GetType()), queryParams.XLStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("XLStockKW"), this.TypeToDbType(queryParams.XLStockKW.GetType()), queryParams.XLStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("MJStockNO"), this.TypeToDbType(queryParams.MJStockNO.GetType()), queryParams.MJStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("MJStockKW"), this.TypeToDbType(queryParams.MJStockKW.GetType()), queryParams.MJStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ZJStockNO"), this.TypeToDbType(queryParams.ZJStockNO.GetType()), queryParams.ZJStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ZJStockKW"), this.TypeToDbType(queryParams.ZJStockKW.GetType()), queryParams.ZJStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("HGStockNO"), this.TypeToDbType(queryParams.HGStockNO.GetType()), queryParams.HGStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("HGStockKW"), this.TypeToDbType(queryParams.HGStockKW.GetType()), queryParams.HGStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("NOStockNO"), this.TypeToDbType(queryParams.NOStockNO.GetType()), queryParams.NOStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("NOStockKW"), this.TypeToDbType(queryParams.NOStockKW.GetType()), queryParams.NOStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("FLStockNO"), this.TypeToDbType(queryParams.FLStockNO.GetType()), queryParams.FLStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("FLStockKW"), this.TypeToDbType(queryParams.FLStockKW.GetType()), queryParams.FLStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("FPStockNO"), this.TypeToDbType(queryParams.FPStockNO.GetType()), queryParams.FPStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("FPStockKW"), this.TypeToDbType(queryParams.FPStockKW.GetType()), queryParams.FPStockKW);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("THStockNO"), this.TypeToDbType(queryParams.FPStockNO.GetType()), queryParams.THStockNO);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("THStockKW"), this.TypeToDbType(queryParams.FPStockKW.GetType()), queryParams.THStockKW);
            return sps.ToDataSet();
        }


        public DataSet getMiLanEquipNodeByWorkShopCode(string workshopCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    select equipcode as NodeId , equipName as ShowName from basequip where workshopcode = '" + workshopCode + "' and equiptype = '01'  ");
            return this.GetBySql(sqlstr.ToString()).ToDataSet();
        }


    }
}
