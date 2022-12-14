using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;

    public class QmtPassInfoService : BaseService<NBear.Common.Entity>, IQmtPassInfoService
    {
        #region 构造方法

        public QmtPassInfoService() : base() { }

        public QmtPassInfoService(string connectStringKey) : base(connectStringKey) { }

        public QmtPassInfoService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public DataSet GetDataInfoByQueryParams(IQmtPassInfoQueryParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("Select A.*");
            sb.AppendLine(", B.EquipName, C.ShiftName, D.ClassName");
            sb.AppendLine("From PptShiftConfig A");
            sb.AppendLine("Left Join BasEquip B On A.EquipCode = B.EquipCode");
            sb.AppendLine("Left Join PptShift C On A.ShiftID = C.ObjID");
            sb.AppendLine("Left Join PptClass D On A.ClassID = D.ObjID");
            sb.AppendLine("WHERE 1 = 1 And A.MaterialCode > '5' And A.CheckFlag = '2'");
            if (!string.IsNullOrEmpty(queryParams.BeginPlanDate))
            {
                sb.AppendFormat("And A.PlanDate >= '{0}'", queryParams.BeginPlanDate);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.EndPlanDate))
            {
                sb.AppendFormat("And DATEADD(DAY, -1, A.PlanDate) < '{0}'", queryParams.EndPlanDate);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.BeginPassDate))
            {
                sb.AppendFormat("And A.PassTime >= '{0}'", queryParams.BeginPassDate);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.EndPassDate))
            {
                sb.AppendFormat("And DATEADD(DAY, -1, A.PassTime) < '{0}'", queryParams.EndPassDate);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.PassFlag))
            {
                sb.AppendFormat("And ISNULL(A.PassFlag, '0') = '{0}'", queryParams.PassFlag);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.ShiftId))
            {
                sb.AppendFormat("And A.ShiftId = '{0}'", queryParams.ShiftId);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.ZJSID))
            {
                sb.AppendFormat("And A.ZJSID = '{0}'", queryParams.ZJSID);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.Barcode))
            {
                sb.AppendFormat("And A.Barcode = '{0}'", queryParams.Barcode);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.LLBarcode))
            {
                sb.AppendFormat("And A.LLBarcode = '{0}'", queryParams.LLBarcode);
                sb.AppendLine();
            }


            if (queryParams.PageResult != null && string.IsNullOrEmpty(queryParams.PageResult.Orderfld) == false)
            {
                sb.AppendLine("ORDER BY " + queryParams.PageResult.Orderfld);
            }
            else
            {
                sb.AppendLine("ORDER BY A.Barcode");
            }
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();

        }

        public void Pass(string barcode, string passUserId, string passUserName, string passMemo)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Update PptShiftConfig");
            sb.AppendLine("Set PassFlag = '1'");
            sb.AppendLine(", PassTime = GETDATE()");
            sb.AppendFormat(", PassUserId = '{0}'", passUserId);
            sb.AppendLine();
            sb.AppendFormat(", PassUserName = '{0}'", passUserName);
            sb.AppendLine();
            sb.AppendFormat(", PassMemo = '{0}'", passMemo);
            sb.AppendLine();
            sb.AppendFormat("Where Barcode = '{0}'", barcode);

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            css.ExecuteNonQuery();
        }
    }

    public class QmtPassInfoQueryParams : IQmtPassInfoQueryParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string BeginPassDate { get; set; }
        public string EndPassDate { get; set; }
        public string PassFlag { get; set; }
        public string ShiftId { get; set; }
        public string ZJSID { get; set; }
        public string Barcode { get; set; }
        public string LLBarcode { get; set; }

        public PageResult<NBear.Common.Entity> PageResult { get; set; }

    }

}
