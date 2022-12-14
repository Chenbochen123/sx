using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IEqmMaintainRecordManager : IBaseManager<EqmMaintainRecord>
    {
        //获取维修记录信息
        DataSet GetDataByParas(EqmMaintainRecordParams queryParams);

        //新增维修记录信息
        int InsertRecord(EqmMaintainRecord record);

        //获取维修记录统计信息
        DataSet GetGroupDataByParas(List<string> list);

        //获取维修记录统计明细信息
        DataSet GetGroupDetailDataByParas(List<string> list);
    }
}
