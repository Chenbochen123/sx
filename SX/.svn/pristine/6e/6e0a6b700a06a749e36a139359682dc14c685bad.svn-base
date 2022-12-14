using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasWorkCoefficientService : BaseService<BasWorkCoefficient>, IBasWorkCoefficientService
    {
		#region 构造方法

        public BasWorkCoefficientService() : base(){ }

        public BasWorkCoefficientService(string connectStringKey) : base(connectStringKey){ }

        public BasWorkCoefficientService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法


        public class QueryParams
        {
            
            public string objID { get; set; }
            public string workID { get; set; }
            public string coefficient { get; set; }
            public string recordTime { get; set; }
            public string recordWorkBarcode { get; set; }
            public string deleteFlag { get; set; }
            public string remark { get; set; }

            public PageResult<BasWorkCoefficient> pageParams { get; set; }
        }

        public PageResult<BasWorkCoefficient> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasWorkCoefficient> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT A.ObjID ,A.WorkID, B.WorkName ,A.Coefficient,A.RecordTime,A.RecordWorkBarcode, A.DeleteFlag,A.Remark  
                                 FROM BasWorkCoefficient A
                                 LEFT JOIN BasWork B ON A.WorkID=B.ObjID
                                 WHERE 1=1 ");
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
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasWorkCoefficient ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(10, '0');
        }
    }
}
