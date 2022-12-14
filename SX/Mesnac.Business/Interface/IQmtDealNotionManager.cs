using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IQmtDealNotionManager : IBaseManager<QmtDealNotion>
    {
        //获取处理方式信息
        DataSet GetDataByParas(QmtDealNotionParams queryParams);
    }
}
