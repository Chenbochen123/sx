using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtDealNotionService : IBaseService<QmtDealNotion>
    {
        //获取处理方式信息
        DataSet GetDataByParas(QmtDealNotionParams queryParams);

    }
    public class QmtDealNotionParams
    {
        public string objID
        {
            get;
            set;
        }
        public string dealNotion
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<QmtDealNotion> pageResult
        {
            get;
            set;
        }
    }
}
