using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtQrigImportLogMasterService : BaseService<QmtQrigImportLogMaster>, IQmtQrigImportLogMasterService
    {
		#region 构造方法

        public QmtQrigImportLogMasterService() : base(){ }

        public QmtQrigImportLogMasterService(string connectStringKey) : base(connectStringKey){ }

        public QmtQrigImportLogMasterService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public System.Data.DataSet GetQrigDetailInfo(string guid)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.PlanDate, A.EquipCode, A.ShiftId, A.ShiftClass, A.MaterCode, A.SerialId, A.LLSerialID");
            sb.AppendLine(", A.CheckNum, A.CheckPlan_Date CheckPlanDate, A.CheckDate + ' ' + A.CheckTime CheckTime");
            sb.AppendLine(", A.ShiftCheckId, A.CheckPlan_Class, A.ZJSID");
            sb.AppendLine(", B.ItemCd, B.ItemCheck");
            sb.AppendLine(", C.EquipName, D.ShiftName, E.ClassName, F.MaterialName");
            sb.AppendLine(", G.ShiftName CheckShiftName, H.ClassName CheckClassName");
            sb.AppendLine(", I.ItemName, J.EquipName CheckEquipName");
            sb.AppendLine("FROM QmtQrigMaster A");
            sb.AppendLine("INNER JOIN QmtQrigDetail B ON A.SeqNo = B.SeqNo");
            sb.AppendLine("LEFT JOIN BasEquip C ON A.EquipCode = C.EquipCode");
            sb.AppendLine("LEFT JOIN PptShift D ON A.ShiftId = D.ObjID");
            sb.AppendLine("LEFT JOIN PptClass E ON A.ShiftClass = E.ObjID");
            sb.AppendLine("LEFT JOIN BasMaterial F ON A.MaterCode = F.MaterialCode");
            sb.AppendLine("LEFT JOIN PptShift G ON A.ShiftCheckId = G.ObjID");
            sb.AppendLine("LEFT JOIN PptClass H ON A.CheckPlan_Class = H.ObjID");
            sb.AppendLine("LEFT JOIN QmtCheckItem I ON B.ItemCd = I.ItemCode");
            sb.AppendLine("LEFT JOIN BasEquip J ON B.CheckEquipCode = J.EquipCode");
            sb.AppendFormat("WHERE A.GUID = '{0}'", guid);
            sb.AppendLine();
            sb.AppendLine("ORDER BY A.ZJSID, C.EquipName, D.ShiftName, E.ClassName, F.MaterialName, A.SerialId, A.LLSerialID");

            return this.GetBySql(sb.ToString()).ToDataSet();
        }
    }
}
