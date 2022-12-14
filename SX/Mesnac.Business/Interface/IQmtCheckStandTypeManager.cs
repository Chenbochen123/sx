using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IQmtCheckStandTypeManager : IBaseManager<QmtCheckStandType>
    {
        //获取检验标准类型信息
        DataSet GetDataByParas(QmtCheckStandTypeParams queryParams);
    }
}
