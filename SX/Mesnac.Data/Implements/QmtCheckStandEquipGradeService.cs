using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;

    public class QmtCheckStandEquipGradeService : BaseService<QmtCheckStandEquipGrade>, IQmtCheckStandEquipGradeService
    {
		#region ���췽��

        public QmtCheckStandEquipGradeService() : base(){ }

        public QmtCheckStandEquipGradeService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckStandEquipGradeService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        public DataSet GetDataByParas(IQmtCheckStandEquipGradeParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.StandId,TA.ItemCd,TA.CheckEquipCode,TA.WeightId,TA.PermMin,TA.IfMin,TA.PermMax,TA.IfMax,TA.JudgeResult,TA.Grade,TA.DrawMark,TA.DealCode,TA.DeleteFlag,TA.GUID,TB.ItemName,TC.DealNotion,TD.EquipName CheckEquipName");
            sb.AppendLine("FROM QmtCheckStandEquipGrade TA");
            sb.AppendLine("LEFT JOIN QmtCheckItem TB ON TA.ItemCd=TB.ItemCode");
            sb.AppendLine("LEFT JOIN QmtDealNotion TC ON TA.DealCode=TC.ObjID");
            sb.AppendLine("LEFT JOIN BasEquip TD ON TA.CheckEquipCode=TD.EquipCode");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.StandId))
                sb.AppendLine("AND TA.StandId=" + queryParams.StandId);
            if (!string.IsNullOrEmpty(queryParams.ItemCd))
                sb.AppendLine("AND TA.ItemCd='" + queryParams.ItemCd + "'");
            if (!string.IsNullOrEmpty(queryParams.CheckEquipCode))
                sb.AppendLine("AND TA.CheckEquipCode='" + queryParams.CheckEquipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }

    public class QmtCheckStandEquipGradeParams : IQmtCheckStandEquipGradeParams
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
        public string CheckEquipCode
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
