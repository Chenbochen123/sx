using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmStopFaultService : IBaseService<EqmStopFault>
    {
        //获取故障点信息
        DataSet GetDataByParas( EqmStopFaultParams queryParams );

        //获取新的故障点代码
        string GetNextFaultCodeByParas(EqmStopFault EqmStopFault);

        PageResult<EqmStopFault> GetEqmStopFaultBySearchKey(Mesnac.Data.Implements.EqmStopFaultService.QueryParams queryParams);
    }
    public class EqmStopFaultParams
    {
        public string objID
        {
            get;
            set;
        }
        public string mainTypeID
        {
            get;
            set;
        }
        public string typeID
        {
            get;
            set;
        }
        public string typeName
        {
            get;
            set;
        }
        public string faultCode
        {
            get;
            set;
        }
        public string faultName
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<EqmStopFault> pageResult
        {
            get;
            set;
        }
    }

}
