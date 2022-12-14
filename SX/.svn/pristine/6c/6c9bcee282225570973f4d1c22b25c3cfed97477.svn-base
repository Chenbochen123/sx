using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
using Mesnac.Data.Components;
    using System.Data;
    using NBear.Data;
    public class BasWorkUserInfoService : BaseService<BasWorkUserInfo>, IBasWorkUserInfoService
    {
		#region 构造方法

        public BasWorkUserInfoService() : base(){ }

        public BasWorkUserInfoService(string connectStringKey) : base(connectStringKey){ }

        public BasWorkUserInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {

            public string objID { get; set; }
            public string startpdtDate { get; set; }
            public string endpdtDate { get; set; }
            public string pdtDate { get; set; }
            public string equipCode { get; set; }
            public string shiftID { get; set; }
            public string classID { get; set; }
            public string workBarcode { get; set; }
            public string workID { get; set; }
            public string attendance { get; set; }
            public string recordTime { get; set; }
            public string recordWorkBarcode { get; set; }
            public string deleteFlag { get; set; }
            public string remark { get; set; }
            public PageResult<BasWorkUserInfo> pageParams { get; set; }
        }

        public PageResult<BasWorkUserInfo> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasWorkUserInfo> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT A.*,B.EquipName,C.ShiftName,D.ClassName,E.UserName,F.WorkName
                                 FROM BasWorkUserInfo A
                                 LEFT JOIN BasEquip B ON A.EquipCode=B.EquipCode
                                 LEFT JOIN PptShift C ON A.ShiftID=C.ObjID
                                 LEFT JOIN PptClass D ON A.ClassID=D.ObjID
                                 LEFT JOIN BasUser E ON A.WorkBarcode=E.WorkBarcode
                                 LEFT JOIN BasWork F ON A.WorkID=F.ObjID
                                 WHERE 1=1 ");
            if (queryParams.startpdtDate != "0001-01-01 0:00:00" && queryParams.startpdtDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND A.pdtDate >= '" + queryParams.startpdtDate + "'");
            }
            if (queryParams.endpdtDate != "0001-01-01 0:00:00" && queryParams.endpdtDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND A.pdtDate <= '" + queryParams.endpdtDate + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.recordWorkBarcode))
            {
                sqlstr.AppendLine(" AND A.RecordWorkBarcode = " + queryParams.recordWorkBarcode);
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND A.ShiftID = " + queryParams.shiftID);
            }
            if (!string.IsNullOrEmpty(queryParams.classID))
            {
                sqlstr.AppendLine(" AND A.ClassID = " + queryParams.classID);
            }
            if (!string.IsNullOrEmpty(queryParams.workBarcode))
            {
                sqlstr.AppendLine(" AND A.WorkBarcode = " + queryParams.workBarcode);
            }
            if (!string.IsNullOrEmpty(queryParams.workID))
            {
                sqlstr.AppendLine(" AND A.WorkID = " + queryParams.workID);
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
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

        public string GetNextObjID()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasWorkUserInfo");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(10, '0');
        }
        public DataSet UserQueryByCode(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_WorkUserByCode");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("startpdtDate"), this.TypeToDbType(queryParams.startpdtDate.GetType()), queryParams.startpdtDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("endpdtDate"), this.TypeToDbType(queryParams.endpdtDate.GetType()), queryParams.endpdtDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("recordWorkBarcode"), this.TypeToDbType(queryParams.recordWorkBarcode.GetType()), queryParams.recordWorkBarcode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("shiftID"), this.TypeToDbType(queryParams.shiftID.GetType()), queryParams.shiftID);
            return sps.ToDataSet();
        }
    }
}
