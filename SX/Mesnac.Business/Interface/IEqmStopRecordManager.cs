using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IEqmStopRecordManager : IBaseManager<EqmStopRecord>
    {
        //获取停机记录信息
        DataSet GetDataByParas( EqmStopRecordParams queryParams );
    }
}
