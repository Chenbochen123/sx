using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckStandEquipService : BaseService<QmtCheckStandEquip>, IQmtCheckStandEquipService
    {
		#region 构造方法

        public QmtCheckStandEquipService() : base(){ }

        public QmtCheckStandEquipService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckStandEquipService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetDataByParas(IQmtCheckStandEquipParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.StandId,TA.ItemCd,TA.CheckEquipCode,TA.WeightId,TA.PermMin,TA.IfMin,TA.PermMax,TA.IfMax,TA.JudgeResult,TA.Grade,TA.DrawMark,TA.DealCode,TA.DeleteFlag,TA.GUID,TB.ItemName,TC.DealNotion,TD.EquipName CheckEquipName");
            sb.AppendLine("FROM QmtCheckStandEquip TA");
            sb.AppendLine("LEFT JOIN QmtCheckItem TB ON TA.ItemCd=TB.ItemCode");
            sb.AppendLine("LEFT JOIN QmtDealNotion TC ON TA.DealCode=TC.ObjID");
            sb.AppendLine("LEFT JOIN BasEquip TD ON TA.CheckEquipCode=TD.EquipCode");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.StandId))
                sb.AppendLine("AND TA.StandId=" + queryParams.StandId);
            if (!string.IsNullOrEmpty(queryParams.ItemCd))
                sb.AppendLine("AND TA.ItemCd='" + queryParams.ItemCd + "'");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }

    public class QmtCheckStandEquipParams : IQmtCheckStandEquipParams
    {
        public string StandId
        {
            get;
            set;
        }
        public string ItemCd
        {
            get;
            set;
        }
        public string DeleteFlag
        {
            get;
            set;
        }

    }
}
