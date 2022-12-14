using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmFaultReasonService : IBaseService<EqmFaultReason>
    {
        //获取停机故障原因信息
        DataSet GetDataByParas(EqmFaultReasonParams queryParams);
        PageResult<EqmFaultReason> GetEqmFaultReasonBySearchKey(Mesnac.Data.Implements.EqmFaultReasonService.QueryParams queryParams);
    }
    public class EqmFaultReasonParams
    {
        public string objID
        {
            get;
            set;
        }
        public string reasonName
        {
            get;
            set;
        }
        public string dealDesc
        {
            get;
            set;
        }
        public string faultID
        {
            get;
            set;
        }
        public string faultName
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
        public string mainTypeID
        {
            get;
            set;
        }
        public string mainTypeName
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<EqmFaultReason> pageResult
        {
            get;
            set;
        }
    }
}
