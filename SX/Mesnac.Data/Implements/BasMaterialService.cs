using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using NBear.Common;
    using NBear.Data;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using System.Data;
    public class BasMaterialService : BaseService<BasMaterial>, IBasMaterialService
    {
        #region 构造方法

        public BasMaterialService() : base() { }

        public BasMaterialService(string connectStringKey) : base(connectStringKey) { }

        public BasMaterialService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string materialCode { get; set; }
            public string materialName { get; set; }
            public string majorTypeID { get; set; }
            public string minorTypeID { get; set; }
            public string rubCode { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public string minMajorTypeID { get; set; }
            public string workBarCode { get; set; }
            public PageResult<BasMaterial> pageParams { get; set; }
        }

        public PageResult<BasMaterial> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasMaterial> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    mater.ObjID, mater.MaterialCode, major.MajorTypeName as MajorTypeID, 
                                            minor.MinorTypeName as MinorTypeID, rub.RubName as RubCode, 
                                            mater.MaterialName, mater.MaterialOtherName, mater.MaterialSimpleName, mater.MaterialLevel,
                                            mater.UserCode, mater.PlanPrice, mater.ProductArea, mater2.MaterialName as ProductMaterialCode, 
                                            mater.MinStock, mater.MaxStock, unit.UnitName as UnitID , unit2.UnitName as StaticUnitID, 
                                            mater.StaticUnitCoefficient, mater.CheckPermitError, mater.MaxParkTime, 
                                            mater.MinParkTime, mater.DefineDate, mater.StandardCode, mater3.MaterialName as MaterialGroup ,
                                            class.StaticClassName as StaticClass, code.ItemName as IsEqualMaterial ,code2.ItemName as IsPutJar,
                                            code3.ItemName as IsQualityRateCount, mater.ERPCode, mater.Remark, mater.DeleteFlag , mater.ValidDate
                                 FROM	    BasMaterial mater 
                                 LEFT JOIN  BasMaterialMajorType    major   on mater.MajorTypeID = major.ObjID 
                                 LEFT JOIN  BasMaterialMinorType    minor   on mater.MinorTypeID = minor.MinorTypeID  AND mater.MajorTypeID =  minor.MajorID
                                 LEFT JOIN  BasRubInfo rub                  on mater.RubCode = rub.RubCode  
                                 LEFT JOIN  BasUnit unit                    on mater.UnitID = unit.ObjID
                                 LEFT JOIN  BasUnit unit2                   on mater.StaticUnitID = unit2.ObjID
                                 LEFT JOIN  BasMaterialStaticClass  class   on mater.StaticClass = class.ObjID
                                 LEFT JOIN  BasMaterial             mater2  on mater.ProductMaterialCode = mater2.MaterialCode
                                 LEFT JOIN  BasMaterial             mater3  on mater.MaterialGroup = mater3.MaterialCode
                                 LEFT JOIN  SysCode code                    on mater.IsEqualMaterial = code.ItemCode And code.TypeID = 'YesNo'
                                 LEFT JOIN  SysCode code2                   on mater.IsPutJar = code2.ItemCode And code2.TypeID = 'YesNo'
                                 LEFT JOIN  SysCode code3                   on mater.IsQualityRateCount = code3.ItemCode And code3.TypeID = 'YesNo'
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND mater.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND mater.MaterialCode like '%" + queryParams.materialCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.materialName))
            {
                sqlstr.AppendLine(" AND mater.MaterialName like '%" + queryParams.materialName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.majorTypeID))
            {
                sqlstr.AppendLine(" AND mater.MajorTypeID like '%" + queryParams.majorTypeID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeID))
            {
                sqlstr.AppendLine(" AND mater.MinorTypeID = " + queryParams.minorTypeID);
            }
            if (!string.IsNullOrEmpty(queryParams.rubCode))
            {
                sqlstr.AppendLine(" AND mater.RubCode = " + queryParams.rubCode);
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND mater.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND mater.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.minMajorTypeID))
            {
                sqlstr.AppendLine(" AND mater.MajorTypeID >'" + queryParams.minMajorTypeID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.workBarCode))
            {
                sqlstr.AppendLine(" AND mater.MaterialCode  in (Select Distinct RecipeMaterialCode  From PmtRecipe R Join BasEquip E On E.EquipCode = R.RecipeEquipCode Where E.WorkShopCode = '" + queryParams.workBarCode + "' And R.RecipeMaterialCode >= '4' And R.RecipeMaterialCode <= '5') ");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }



        


        /// <summary>
        /// 取流水号方法
        /// </summary>
        /// <param name="MajorTypeID"></param>
        /// <param name="MinorTypeID"></param>
        /// <param name="RubCode"></param>
        /// <param name="isOriginal">true代表是原材料，false代表小料或胶料</param>
        /// <returns></returns>
        public string GetNextMaterialCode(string MajorTypeID, string MinorTypeID, string RubCode, bool isOriginal)
        {
            string startStr = MajorTypeID + MinorTypeID;
            string endStr = RubCode;
            StringBuilder sqlstr = new StringBuilder();
            if (isOriginal)//原材料
            {
                sqlstr.AppendLine(@"    SELECT  Convert(bigint , Max(MaterialCode)) + 1 
                                        AS      MaterialCode 
                                        FROM	dbo.BasMaterial ");
                sqlstr.AppendLine(@"    WHERE   MaterialCode Like '" + startStr + "%' ");
            }
            else//小料胶料
            {
                sqlstr.AppendLine(@"    SELECT  substring(Max(MaterialCode), 4 , 6) + 1
                                        AS      MaterialCode 
                                        FROM	dbo.BasMaterial ");
                sqlstr.AppendLine(@"    WHERE   MaterialCode Like '" + startStr + "%' ");
                sqlstr.AppendLine(@"    And     MaterialCode Like '%" + endStr + "' ");
            }
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "" && isOriginal)
            {
                temp = startStr + "0000000001";
            }
            if (temp == "" && !isOriginal)
            {
                temp = startStr + "000001" + RubCode;
                return temp;
            }

            if (isOriginal)
            {
                return temp;
            }
            else
            {
                return startStr + temp.PadLeft(6, '0') + RubCode;
            }
        }

        public string GetNextMaterialObjID()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasMaterial ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }

        public string GetMaterName(string MaterCode)
        {
            string sql = "select MaterialName from BasMaterial where MaterialCode = '" + MaterCode + "'";
            return this.GetBySql(sql).ToScalar().ToString();
        }

        public DataSet GetMaterInfo(string ErpCode)
        {
            string sql = "select MaterialCode, MaterialName from BasMaterial where ERPCode = '" + ErpCode + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        /// <summary>
        /// 判断物料是否属于指定的小类
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MinorType"></param>
        /// <returns></returns>
        public bool IsMinorType(string MaterialCode, string MinorType)
        {
            string sql = "select * from BasMaterialMinorType where MinorTypeName in (" + MinorType + ") and SUBSTRING('" + MaterialCode + "', 1, 3) = CONVERT(varchar, MajorID)+MinorTypeID";
            if (this.GetBySql(sql).ToDataSet().Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the distinct recipe material name and code.
        /// 袁洋 2013年4月13日14:31:21
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchKey">The search key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasMaterial> GetMaterialNameAndCodeBySearchKey(int top, string majorId, string searchKey)
        {
            EntityArrayList<BasMaterial> materialList = new EntityArrayList<BasMaterial>();
            string whereStr = "";
            switch (majorId)
            {
                case  "": whereStr = "";
                    break;
                case "1": whereStr = " MajorTypeID = 1 ";
                    break;
                case "2": whereStr = " MajorTypeID = 2 "; 
                    break;
                default:  whereStr = " MajorTypeID > 2 "; 
                    break;
            }
            string sqlstr = "";
            sqlstr = @" SELECT  DISTINCT    TOP {0} MaterialCode , MaterialName 
                        FROM    BasMaterial 
                        WHERE   [dbo].[FuncSysGetPY](MaterialName) like '%{1}%' ";
            if (whereStr != "")
            {
                sqlstr += @" AND " + whereStr;
            }
            sqlstr += " ORDER BY MaterialCode ";
            sqlstr = String.Format(sqlstr, top, searchKey);
            DataSet ds = this.GetBySql(sqlstr.ToString()).ToDataSet();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BasMaterial material = new BasMaterial();
                material.MaterialCode = row["MaterialCode"].ToString();
                material.MaterialName = row["MaterialName"].ToString() + "-" + row["MaterialCode"].ToString();
                materialList.Add(material);
            }
            return materialList;
        }

        /// <summary>
        /// Gets the distinct recipe material name and code.
        /// 袁洋 2013年4月13日14:31:21
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchKey">The search key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PageResult<BasMaterial> GetMaterialBySearchKey(QueryParams queryParams)
        {
            PageResult<BasMaterial> pageParams = queryParams.pageParams;
            EntityArrayList<BasMaterial> materialList = new EntityArrayList<BasMaterial>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    MaterialCode , MaterialName
                                    FROM        BasMaterial 
                                    WHERE       [dbo].[FuncSysGetPY](MaterialName) like '%{0}%' ");
            if (!string.IsNullOrEmpty(queryParams.majorTypeID))
            {
                sqlstr.AppendLine(" AND MajorTypeID in (" + queryParams.majorTypeID + ",6)");
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeID))
            {
                sqlstr.AppendLine(" AND MinorTypeID = " + queryParams.minorTypeID);
            }
            if (!string.IsNullOrEmpty(queryParams.minMajorTypeID))
            {
                sqlstr.AppendLine(" AND MajorTypeID >'" + queryParams.minMajorTypeID + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.materialName);
            pageParams.QueryStr = sqlresult;
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlresult);
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="materialcode"></param>
        /// <param name="maxparktime"></param>
        /// <param name="minparktime"></param>
        public void UpdateMaterialTime(string materialcode, decimal maxparktime, decimal minparktime, string restoredate, string oper)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcBasUpdateMaterialTime");
            sps.AddInputParameter("materialcode", this.TypeToDbType(materialcode.GetType()), materialcode);
            sps.AddInputParameter("minparktime", this.TypeToDbType(minparktime.GetType()), minparktime);
            sps.AddInputParameter("maxparktime", this.TypeToDbType(maxparktime.GetType()), maxparktime);
            sps.AddInputParameter("restoredate", this.TypeToDbType(restoredate.GetType()), restoredate);
            sps.AddInputParameter("oper", this.TypeToDbType(oper.GetType()), oper);
            sps.ToDataSet();
        }

        /// <summary>
        /// Gets the distinct recipe material name and code(By chinese words).
        /// 王锴添加 2013年10月24日11:05:05
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchKey">The chinese search key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PageResult<BasMaterial> GetMaterialByChineseSearchKey(QueryParams queryParams)
        {
            PageResult<BasMaterial> pageParams = queryParams.pageParams;
            EntityArrayList<BasMaterial> materialList = new EntityArrayList<BasMaterial>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    MaterialCode , MaterialName
                                    FROM        BasMaterial 
                                    WHERE       MaterialName like '%{0}%' ");
            if (!string.IsNullOrEmpty(queryParams.majorTypeID))
            {
                sqlstr.AppendLine(" AND MajorTypeID like '%" + queryParams.majorTypeID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeID))
            {
                sqlstr.AppendLine(" AND MinorTypeID = " + queryParams.minorTypeID);
            }
            if (!string.IsNullOrEmpty(queryParams.minMajorTypeID))
            {
                sqlstr.AppendLine(" AND MajorTypeID >'" + queryParams.minMajorTypeID + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.materialName);
            pageParams.QueryStr = sqlresult;
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlresult);
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

        //赵营 2014-05-29日添加，简单检验ERP编号并提取返回SAP主物料编号
        public string GetSapMaterialShortCode(string erpCode)
        {
            int erpCodeLength = erpCode.Length;
            if (erpCodeLength == 10)//正常胶 9位SAP编号+下划线
            {
                if (erpCode.EndsWith("_"))
                {
                    return erpCode;
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 11)//掺胶 C+9位SAP编号+下划线
            {
                if (erpCode.StartsWith("C") && erpCode.EndsWith("_"))
                {
                    return erpCode.Substring(1, 10);
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 12)//返回胶 9位SAP编号+下划线+中划线+F
            {
                if (erpCode.EndsWith("_-F"))
                {
                    return erpCode.Substring(0, 10);
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 13)//实验胶 S+1位数字+中划线+9位SAP编号+下划线
            {
                if (erpCode.StartsWith("S") && erpCode.EndsWith("_") && erpCode.Substring(2, 1) == "-")
                {
                    return erpCode.Substring(3, 10);
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 14)//实验掺用胶 S+1位数字+中划线+C+9位SAP编号+下划线
            {
                if (erpCode.StartsWith("S") && erpCode.EndsWith("_") && erpCode.Substring(2, 2) == "-C")
                {
                    return erpCode.Substring(4, 10);
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 15)//实验掺用胶 S+1位数字+中划线+9位SAP编号+下划线+ -F
            {
                if (erpCode.StartsWith("S")  && erpCode.EndsWith("_-F"))
                {
                    return erpCode.Substring(3, 10);
                }
                else
                {
                    return "No";
                }
            }
            if (erpCodeLength == 16)//实验掺用胶 S+1位数字+中划线+C+9位SAP编号+下划线+ -F
            {
                if (erpCode.StartsWith("S") && erpCode.Substring(2, 2) == "-C" && erpCode.EndsWith("_-F"))
                {
                    return erpCode.Substring(4, 10);
                }
                else
                {
                    return "No";
                }
            }
            return "No";
        }
    }
}
