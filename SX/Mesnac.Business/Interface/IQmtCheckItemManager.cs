using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IQmtCheckItemManager : IBaseManager<QmtCheckItem>
    {
        //获取检验项目信息
        DataSet GetDataByParas(QmtCheckItemParams queryParams);

        //获取新的检验项目代码
        string GetNextItemCodeByParas(QmtCheckItem qmtCheckItem);
    }
}
