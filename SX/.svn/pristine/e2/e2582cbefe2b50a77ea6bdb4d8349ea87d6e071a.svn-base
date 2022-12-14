using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtCheckItemTypeService : IBaseService<QmtCheckItemType>
    {
        //获取检验项目类型信息
        DataSet GetDataByParas(QmtCheckItemTypeParams queryParams);

        //获取新的类型代码
        string GetNextTypeIDByParas(QmtCheckItemType qmtCheckItemType);
    }
    public class QmtCheckItemTypeParams
    {
        public string objID
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
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<QmtCheckItemType> pageResult
        {
            get;
            set;
        }
    }
}
