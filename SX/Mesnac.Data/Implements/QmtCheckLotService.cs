using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckLotService : BaseService<QmtCheckLot>, IQmtCheckLotService
    {
		#region 构造方法

        public QmtCheckLotService() : base(){ }

        public QmtCheckLotService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckLotService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetCheckLotResultByParas(IQmtCheckLotParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@PlanDate", paras.PlanDate);
            dict.Add("@ZJSID", paras.ZJSID);
            dict.Add("@EquipCode", paras.EquipCode);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@ShiftId", paras.ShiftId);
            dict.Add("@StandCode", paras.StandCode);
            return GetDataSetByStoreProcedure("ProcQmtShowCheckResultDataByStandCode", dict);
        }

        public class QmtCheckLotParams : IQmtCheckLotParams
        {
            public string PlanDate { get; set; }
            public string ZJSID { get; set; }
            public string MaterCode { get; set; }
            public string EquipCode { get; set; }
            public string ShiftId { get; set; }
            public string StandCode { get; set; }
        }
    }
}
