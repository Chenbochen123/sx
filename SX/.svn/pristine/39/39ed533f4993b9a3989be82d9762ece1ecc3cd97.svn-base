using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using NBear.Common;
    public interface IPmtOpenActionModelDetailManager : IBaseManager<PmtOpenActionModelDetail>
    { 
        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @2014年9月29日11:04:06
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtOpenActionModelDetail> GetTablePageDataBySql(PmtOpenActionModelDetailManager.QueryParams queryParams);



        /// <summary>
        /// 保存工艺配方
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="pmtRecipe">The PMT recipe.</param>
        /// <param name="pmtRecipeWeight">The PMT recipe weight.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string SaveOpenActionModelDetail(string MainModelID, EntityArrayList<PmtOpenActionModelDetail> pmtOpenActionModelDetailList ,string openMixingNo);
    
    }
}
