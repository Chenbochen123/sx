using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckDetailService : BaseService<QmtCheckDetail>, IQmtCheckDetailService
    {
        #region 构造方法

        public QmtCheckDetailService() : base() { }

        public QmtCheckDetailService(string connectStringKey) : base(connectStringKey) { }

        public QmtCheckDetailService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public DataSet GetCheckRubberQualityReportDetailByParas(IQmtCheckRubberQualityReportDetailParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.CheckCode, A.StandId, A.ItemCd, A.ItemCheck, B.ItemName");
            sb.AppendLine(", C.StandCode, D.PermMax, D.PermMin, E.StandTypeName");
            sb.AppendLine(", A.Grade, A.JudgeMemo");
            sb.AppendLine(", CASE A.Grade WHEN 1 THEN '合格' WHEN 2 THEN '不合格' ELSE '' END JudgeResultDes");
            sb.AppendLine(", CASE A.Grade WHEN 1 THEN '1' WHEN 2 THEN '0' ELSE '' END JudgeResult");
            sb.AppendLine("FROM QmtCheckDetail A");
            sb.AppendLine("LEFT JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode");
            sb.AppendLine("LEFT JOIN QmtCheckStandMaster C ON A.StandId = C.StandId");
            sb.AppendLine("LEFT JOIN QmtCheckStandDetail D ON A.StandId = D.StandId AND A.ItemCd = D.ItemCd");
            sb.AppendLine("LEFT JOIN QmtCheckStandType E ON C.StandCode = E.ObjID");
            sb.AppendLine("WHERE A.Grade IS NOT NULL AND ISNULL(A.ItemCheck, 0) > 0");
            sb.AppendFormat("AND A.CheckCode = '{0}'", paras.CheckCode);
            sb.AppendLine();
            sb.AppendFormat("AND A.SerialId = {0}", paras.SerialId);
            sb.AppendLine();
            sb.AppendFormat("AND A.LLSerialID = {0}", paras.LLSerialID);
            sb.AppendLine();
            sb.AppendFormat("AND A.IfCheckNum = {0}", paras.IfCheckNum);
            sb.AppendLine();
            if (paras.Grade != null && paras.Grade != "")
            {
                sb.AppendFormat("AND A.Grade = {0}", paras.Grade);
                sb.AppendLine();
            }
            sb.AppendLine("ORDER BY A.ItemCd");


            return GetBySql(sb.ToString()).ToDataSet();
        }

    }

    public class QmtCheckRubberQualityReportDetailParams : IQmtCheckRubberQualityReportDetailParams
    {
        public string CheckCode { get; set; }
        public string SerialId { get; set; }
        public string LLSerialID { get; set; }
        public string IfCheckNum { get; set; }
        public string Grade { get; set; }
    }
}
