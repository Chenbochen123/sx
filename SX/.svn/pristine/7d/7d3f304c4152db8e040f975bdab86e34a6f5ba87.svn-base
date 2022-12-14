using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberStorageDetailService : BaseService<PpmRubberStorageDetail>, IPpmRubberStorageDetailService
    {
		#region 构造方法

        public PpmRubberStorageDetailService() : base(){ }

        public PpmRubberStorageDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberStorageDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            string sql = @"select Barcode, OrderID, OperType, PlanDate, ShiftID, B.ShiftName, ShiftClassID, C.ClassName,B.ShiftName+'-'+C.ClassName as 'Banci',MaterCode, D.MaterialName, 
	                            Weight*StockType as Weight,	ToStorageID, E.StorageName, ToStoragePlaceID, F.StoragePlaceName, RecordDate, OperPerson,Num*StockType as Num,G.EquipName
                            from PpmRubberStorageDetail A
	                            left join PptShift B on A.ShiftID = B.ObjID
	                            left join PptClass C on A.ShiftClassID = C.ObjID
                                 left join BasEquip G on a.EquipCode = G.EquipCode
	                            left join BasMaterial D on A.MaterCode = D.MaterialCode
	                            left join BasStorage E on A.ToStorageID = E.StorageID
	                            left join BasStoragePlace F on A.ToStoragePlaceID = F.StoragePlaceID
                            where 1 = 1 and Barcode ='" + Barcode + "' and A.StorageID = '" + StorageID + "' and A.StoragePlaceID = '" + StoragePlaceID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
