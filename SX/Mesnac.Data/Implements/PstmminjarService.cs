using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstmminjarService : BaseService<Pstmminjar>, IPstmminjarService
    {
		#region 构造方法

        public PstmminjarService() : base(){ }

        public PstmminjarService(string connectStringKey) : base(connectStringKey){ }

        public PstmminjarService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string equipCode { get; set; }
            public string shiftID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string storagePlaceId { get; set; }
            public PageResult<Pstmminjar> pageParams { get; set; }
        }

        public PageResult<Pstmminjar> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<Pstmminjar> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.JarID, A.Materbarcode, A.StockDate, A.MaterCode, B.MaterialName, A.EquipCode, C.EquipName, A.ShiftDate, 
                                    A.ShiftID, A.InTime, A.RealNum, A.RealWeight, A.handLename, D.UserName, A.Usedweigh, A.UsedFlag, 
                                    A.ClearFlag, A.AuditFlag, A.InaccountDuration, A.InaccountFlag,E.StorageName, G.JarNum
                                from dbo.Pstmminjar A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasUser D on A.handLename = D.WorkBarcode
                                left join BasStoragePlace F on A.StoragePlaceID = F.StoragePlaceID
                                left join BasStorage E on F.StorageID = E.StorageID
                                left join 
                                (
                                SELECT * FROM (SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) b
                                WHERE id IN (
                                SELECT MIN(id) FROM (
                                SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) a GROUP BY EquipCode,StoragePlaceCode,MaterialCode )
                                ) G on A.EquipCode = G.EquipCode and A.StoragePlaceID = G.StoragePlaceCode and A.MaterCode = G.MaterialCode
                                where A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceId))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID= '" + queryParams.storagePlaceId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.ShiftDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.ShiftDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
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
    }
}
