using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    /// <summary>
    /// IPmtRecipeService 接口定义
    /// 孙本强 @ 2013-04-03 12:55:24
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeService : IBaseService<PmtRecipe>
    {
        /// <summary>
        /// 根据物料名称获取物料配方别名信息
        /// 孙宜建
        /// 2013-2-25
        /// </summary>
        /// <param name="recipeMaterialName">物料名称</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetRecipeNameByRecipeMaterialName(string recipeMaterialName);

        /// <summary>
        /// 获取物料信息
        /// 孙本强 @ 2013-04-03 12:55:24
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasMaterial> GetBasMaterial(PmtRecipeService.QueryParams queryParams);

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtRecipe> GetTablePageDataBySql(PmtRecipeService.QueryParams queryParams);

        /// <summary>
        /// 通过拼音获取前物料信息
        /// 孙本强 @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchkey">The searchkey.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<PmtRecipe> GetDistinctRecipeMaterialNameAndCode(int top , string equipCode , string searchkey);

        /// <summary>
        /// 保存工艺配方日志
        /// 孙本强 @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="pmtRecipeID">The PMT recipe ID.</param>
        /// <remarks></remarks>
        void SavePmtRecipeLog(string pmtRecipeID);

        /// <summary>
        /// 刷新工艺配方，主要针对小料修改
        /// 孙本强 @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="pmtRecipeID">The PMT recipe ID.</param>
        /// <remarks></remarks>
        void RefreshPmtRecipe(string pmtRecipeID);
    }
}
