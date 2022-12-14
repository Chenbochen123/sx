using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtCheckItemService : IBaseService<QmtCheckItem>
    {
        //获取检验项目信息
        DataSet GetDataByParas(QmtCheckItemParams queryParams);

        //获取新的检验项目代码
        string GetNextItemCodeByParas(QmtCheckItem qmtCheckItem);

    }
    public class QmtCheckItemParams
    {
        public string objID
        {
            get;
            set;
        }
        public string itemCode
        {
            get;
            set;
        }
        public string itemName
        {
            get;
            set;
        }
        public string typeID
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<QmtCheckItem> pageResult
        {
            get;
            set;
        }
    }
}
