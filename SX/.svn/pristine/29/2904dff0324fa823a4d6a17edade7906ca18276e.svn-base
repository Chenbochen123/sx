using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;

    public class QmtQrigDetailService : BaseService<QmtQrigDetail>, IQmtQrigDetailService
    {
        #region 构造方法

        public QmtQrigDetailService() : base() { }

        public QmtQrigDetailService(string connectStringKey) : base(connectStringKey) { }

        public QmtQrigDetailService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public DataSet GetDataByParas(IQmtQrigDetailParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("SELECT  TA.IncId,TA.SeqNo,TA.ItemCd,TA.ItemCheck,TA.StandCode,TA.StandId,TA.UnitName");
            sb.AppendLine(",TA.JudgeValue,TA.CheckEquipCode,TA.PlanDate,TA.DeleteFlag");
            sb.AppendLine(",TB.PermMin,TB.PermMax,TB.IfMax,TB.IfMin,TC.ItemName,TD.EquipName,TE.StandTypeName");
            sb.AppendLine("FROM dbo.QmtQrigDetail TA");
            sb.AppendLine("LEFT JOIN dbo.QmtCheckStandDetail TB ON TA.StandId = TB.StandId AND TA.ItemCd = TB.ItemCd and TB.JudgeResult=1");
            sb.AppendLine("LEFT JOIN dbo.QmtCheckItem TC ON TA.ItemCd = TC.ItemCode");
            sb.AppendLine("LEFT JOIN dbo.BasEquip TD ON TA.CheckEquipCode = TD.EquipCode");
            sb.AppendLine("LEFT JOIN dbo.QmtCheckStandType TE ON TA.StandCode = TE.ObjID");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.SeqNo))
                sb.AppendLine("AND TA.SeqNo=" + queryParams.SeqNo + "");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            if (queryParams.PageResult != null && string.IsNullOrEmpty(queryParams.PageResult.Orderfld) == false)
            {
                sb.AppendLine("ORDER BY " + queryParams.PageResult.Orderfld);
            }
            else
            {
                sb.AppendLine("ORDER BY TA.IncId");
            }
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }

    public class QmtQrigDetailParams : IQmtQrigDetailParams
    {
        public string SeqNo { get; set; }
        public string DeleteFlag { get; set; }

        public PageResult<QmtQrigDetail> PageResult { get; set; }

    }

}
