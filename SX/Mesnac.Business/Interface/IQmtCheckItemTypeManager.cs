using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IQmtCheckItemTypeManager : IBaseManager<QmtCheckItemType>
    {
        //获取检验项目类型信息
        DataSet GetDataByParas(QmtCheckItemTypeParams queryParams);

        //获取新的类型代码
        string GetNextTypeIDByParas(QmtCheckItemType qmtCheckItemType);
    }
}
