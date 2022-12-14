using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckStandDetailService : BaseService<QmtCheckStandDetail>, IQmtCheckStandDetailService
    {
		#region 构造方法

        public QmtCheckStandDetailService() : base(){ }

        public QmtCheckStandDetailService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckStandDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetDataByParas(IQmtCheckStandDetailParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.StandId,TA.ItemCd,TA.WeightId,TA.PermMin,TA.IfMin,TA.PermMax,TA.IfMax,TA.JudgeResult,TA.Grade,TA.DrawMark,TA.DealCode,TA.CardMark2,TA.QuaFrequency,TA.DeleteFlag,TA.GUID,TB.ItemName,TC.DealNotion");
            sb.AppendLine("FROM QmtCheckStandDetail TA");
            sb.AppendLine("LEFT JOIN QmtCheckItem TB ON TA.ItemCd=TB.ItemCode");
            sb.AppendLine("LEFT JOIN QmtDealNotion TC ON TA.DealCode=TC.ObjID");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.StandId))
                sb.AppendLine("AND TA.StandId=" + queryParams.StandId);
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

    }

    public class QmtCheckStandDetailParams : IQmtCheckStandDetailParams
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
        public string WeightId
        {
            get;
            set;
        }
        public string PermMin
        {
            get;
            set;
        }
        public string IfMin
        {
            get;
            set;
        }
        public string PermMax
        {
            get;
            set;
        }
        public string IfMax
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
