
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using NBear.Common;
using System.Text;


public partial class Manager_BasicInfo_CommonPage_RecipeInfolead : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasRubTypeManager basRubTypeManager = new BasRubTypeManager();
    protected BasRubInfoManager basRubInfoManager = new BasRubInfoManager();
    protected BasMaterialManager basMaterialManager = new BasMaterialManager();
    protected TblMaterManager tblMaterManager = new TblMaterManager();
    protected BasEquipManager basEquipManager = new BasEquipManager();
    protected TblRecipeManager tblRecipeManager = new TblRecipeManager();
    protected TblWeightManager tblWeightManager = new TblWeightManager();
    protected TblMixManager tblMixManager = new TblMixManager();
    protected PmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected PmtConfigManager pmtConfigManager = new PmtConfigManager();
    protected PmtRecipeWeightManager pmtRecipeWeightManager = new PmtRecipeWeightManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion
    public int isplm = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private PageResult<TblRecipe> GetPageResultData(PageResult<TblRecipe> pageParams)
    {

        TblRecipeManager.QueryParams queryParams = new TblRecipeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.Mater_name = txtRubName.Text.TrimEnd().TrimStart();
        queryParams.Equip_Name = txtEquipName.Text.TrimEnd().TrimStart();
        queryParams.Modify_Flag = "1";
        queryParams.Modify_Flag1 = "5";

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<TblRecipe> GetTablePageDataBySql(TblRecipeManager.QueryParams queryParams)
    {
        PageResult<TblRecipe> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" select  tr.Mater_Code,tm.Mater_Name ,be.EquipName ,case when tr.Modify_Flag=1  then '新配方' when  tr.Modify_Flag=5 then '作废配方' end as Modify_Flag,

   tr.Edt_code,tr.Recipe_type, SysCodePmt.name as Recipe_typeName
from TblRecipe tr
            join TblMater tm on tr.Mater_code=tm.Mater_Code join BasEquip be on tr.Equip_code=be.EquipCode
            left join SysCodePmt on SysCodePmt.id  collate Chinese_PRC_CS_AS_WS = Recipe_type
             WHERE 1=1  ");

          //left join SysCode on SysCode.TypeID='PmtType' and  Convert(varchar,ltrim(rtrim(Recipe_type)))+Convert(varchar,ltrim(rtrim(Routing_type))) =SysCode.Remark
          
        if (!string.IsNullOrEmpty(queryParams.Mater_code))
        {
            sqlstr.AppendLine(" AND tr.Mater_code = " + queryParams.Mater_code);
        }
        if (!string.IsNullOrEmpty(queryParams.Equip_code))
        {
            sqlstr.AppendLine(" AND tr.Equip_code like '%" + queryParams.Equip_code + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.Edt_code))
        {
            sqlstr.AppendLine(" AND tr.Edt_code =" + queryParams.Edt_code);
        }
        if (!string.IsNullOrEmpty(queryParams.Mater_name))
        {
            sqlstr.AppendLine(" AND tm.Mater_name like '%" + queryParams.Mater_name + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.Equip_Name))
        {
            sqlstr.AppendLine(" AND be.EquipName like '%" + queryParams.Equip_Name + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.Modify_Flag) && !string.IsNullOrEmpty(queryParams.Modify_Flag1))
        {
            sqlstr.AppendLine(" AND tr.Modify_Flag like '%" + queryParams.Modify_Flag + "%' or  tr.Modify_Flag like '%" + queryParams.Modify_Flag1 + "%' ");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = tblRecipeManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return tblRecipeManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<TblRecipe> pageParams = new PageResult<TblRecipe>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<TblRecipe> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #region 添加配方

    #region 获取误差值
    /// <summary>
    /// 获取除小料药品外物料允许误差值
    /// 于小鹏 @ 2013-05-07 08:37:05
    /// </summary>
    /// <param name="MaterCode"></param>
    /// <returns></returns>
    private double getErrorAllow(string recipeMaterCode, string MaterCode)
    {
        double ErrorAllow = 0;
      
            string Code = recipeMaterCode.Substring(0, 1);
            string str = MaterCode.Substring(0, 3);
            switch (str)
            {
                case "101":
                case "102":
                case "103":
                    ErrorAllow = 0.300;
                    break;
                case "104":
                    ErrorAllow = 0.300;
                    break;
                case "107":
                    ErrorAllow = 0.100;
                    break;
                case "114":
                    ErrorAllow = 0.100;
                    break;
                case "201":
                case "202":
                    if (Code == "5")
                    {
                        ErrorAllow = 0.1;
                    }
                    else if (Code == "4")
                    {
                        ErrorAllow = 0.1;
                    }
                    else
                    {
                        ErrorAllow = 0.1;
                    }
                    break;
                default:
                    str = str.Substring(0, 1);
                    if (str == "3" || str == "4")
                    {
                        ErrorAllow = 0.300;
                    }
                    else if (str == "6")
                    {
                        ErrorAllow = 2.000;
                    }
                    else
                    {
                        ErrorAllow = 0.300;
                    }
                    break;
            }
            string erpcode = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == MaterCode)[0].ERPCode;
            int? Type = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == MaterCode)[0].MajorTypeID;
            if (!string.IsNullOrEmpty(erpcode) && Type!=2)
            {
                try
                {
                    decimal? error = tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode)[0].Error_allow;
                    if (error > 0)
                    {
                        ErrorAllow = Convert.ToDouble(error);
                    }
                }
                catch (Exception ex)
                {
                    return 999999999;
                }
          
            }
        return ErrorAllow;
    }
    /// <summary>
    /// 获取小料允许误差值
    /// 于小鹏 @ 2013-05-07 09:07:05
    /// </summary>
    /// <param name="MaterCode"></param>
    /// <returns></returns>
    private double getXLErrorAllow(string MaterCode, double weight)
    {
        double ErrorAllow = 0;
        string str = MaterCode.Substring(0, 3);
        switch (str)
        {
            case "105":
                string name = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == MaterCode)[0].MaterialName;
                if (name.Contains("硫磺"))
                {
                    ErrorAllow = 0.010;
                }
                else if (name.Contains("促进剂"))
                {
                    ErrorAllow = 0.010;
                }
                else if (name.Contains("活性剂"))
                {
                    ErrorAllow = 0.020;
                }
                else if (name.Contains("防焦剂"))
                {
                    ErrorAllow = 0.005;
                }
                else if (name.Contains("氧化锌"))
                {
                    ErrorAllow = 0.010;
                }
                else
                {
                    ErrorAllow = 0.010;
                }
                break;
            case "106":
                ErrorAllow = 0.020;
                break;
            case "107":
                ErrorAllow = 0.005;
                break;
            default:
                ErrorAllow = 0.005;
                break;
        }
        string erpcode = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == MaterCode)[0].ERPCode;
        if (!string.IsNullOrEmpty(erpcode))
        {
            decimal? error = tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode)[0].Error_allow;
            if (error > 0)
            {
                ErrorAllow = Convert.ToDouble(error);
            }
        }

        return ErrorAllow;
    }
    #endregion

    #region 生成配方号
    private SysCode GetSysCode(string TypeID, string code)
    {
        SysCode Result = null;
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == TypeID && SysCode._.ItemCode == code);
        if (lst.Count > 0)
        {
            Result = lst[0];
        }
        return Result;
    }
    private string IniRecipeName(string Recipe_type, int Edt_code)
    {
        SysCode sysCode = GetSysCode("PmtType", Recipe_type);

        string RecipeName = DateTime.Now.ToString("yyMMdd") + sysCode.ItemName + Edt_code.ToString("D3");
        return RecipeName;
    }
    #endregion

    #region 判断获取物料编号
    /// <summary>
    /// 为洗车胶重写GetMatercode方法
    /// </summary>
    /// <param name="erpcode"></param>
    /// <param name="IsClear"></param>
    /// <returns></returns>
    private string GetMatercode(string erpcode, string IsClear)
    {
       string Erpcode="X-"+erpcode;
        string materialcode = string.Empty;
        TblMater tm = new TblMater();
        tm = tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode)[0];
        EntityArrayList<BasMaterial> list = new EntityArrayList<BasMaterial>();
        list = basMaterialManager.GetListByWhere(BasMaterial._.ERPCode == Erpcode);
        if (list.Count == 0)
        {
            
            BasMaterial material = new BasMaterial();
            string str = tm.Mater_Name;
            string majorTypeID = string.Empty;
            string minorTypeID = string.Empty;
           
            if (str.Substring(str.Length - 3, 1) == "/")
            {
                majorTypeID = "4";
                minorTypeID = str.Substring(str.Length - 1);
                string fatherName = str.Substring(0, str.IndexOf("/"));
                tm.Mater_Name = "X-" + str;
                tm.Mater_Code = Erpcode;
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
            else
            {
                majorTypeID = "5";
                minorTypeID = "1";
                string fatherName = str;
                tm.Mater_Name = "X-" + str;
                tm.Mater_Code = Erpcode;
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
        }
     
        else
        {
            if (list[0].DeleteFlag == "1")
            {
                BasMaterial bm = list[0];
                bm.DeleteFlag = "0";
                basMaterialManager.Update(bm);
            }
            if (list[0].MaterialName != tm.Mater_Name)
            {
                BasMaterial bm = list[0];
                bm.MaterialName = tm.Mater_Name;
                basMaterialManager.Update(bm);
            }
           
             materialcode = list[0].MaterialCode;
            
        }
        return materialcode;
    }
    /// <summary>
    /// 获得物料编号
    /// 于小鹏 @ 2013-04-18 15:17:05
    /// </summary>
    /// <param name="recipeInfo"></param>
    private string GetMatercode(string erpcode)
    {
        #region 判断掺胶，截断erp编码
        //if (erpcode.Substring(0, 1) == "C")
        //{
        //    erpcode = erpcode.Substring(1);
        //}
        #endregion
        string materialcode = string.Empty;
        TblMater tm = new TblMater();
        tm = tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode)[0];
        EntityArrayList<BasMaterial> list = new EntityArrayList<BasMaterial>();
        list = basMaterialManager.GetListByWhere(BasMaterial._.ERPCode == erpcode);
        if (list.Count == 0)
        {

            BasMaterial material = new BasMaterial();
            string str = tm.Mater_Name;
            string majorTypeID = string.Empty;
            string minorTypeID = string.Empty;
            EntityArrayList<BasMaterial> ls = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == str);//如果存在此重名物料，则修改覆盖此物料
            if (ls.Count > 0)
            {
                material = ls[0];
                material.ERPCode = tm.Mater_Code;
                basMaterialManager.Update(material);
                materialcode = ls[0].MaterialCode;
            }
            else if (str.Substring(str.Length - 2, 2) == "-F")
            {
                string fatherName = str.Substring(0, str.Length - 2);
                majorTypeID = "6";
                minorTypeID = "1";
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
            else if (erpcode.Substring(erpcode.Length - 1, 1) == "e")
            {

                if (str.Substring(str.Length - 4, 1) == "/")
                {
                    string fatherName = str.Substring(0, str.IndexOf("/"));
                    majorTypeID = "4";
                    minorTypeID = str.Substring(str.Length - 2, 1);
                    materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
                }
                else
                {
                    string fatherName = str.Substring(1);
                    majorTypeID = "5";
                    minorTypeID = "1";
                    materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
                }
            }
            else if (erpcode.Substring(0, 1) == "C")
            {
                string fatherName = str.Substring(1);
                majorTypeID = "5";
                minorTypeID = "1";
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
            else if (str.Substring(str.Length - 3, 1) == "/")
            {
                string fatherName = str.Substring(0, str.IndexOf("/"));
                majorTypeID = "4";
                minorTypeID = str.Substring(str.Length - 1);
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
            
            else //终炼
            {
                majorTypeID = "5";
                minorTypeID = "1";
                string fatherName = str;
                materialcode = InsertMaterialCode(majorTypeID, minorTypeID, fatherName, tm, erpcode);
            }
        }
        else
        {
            //yzx 180625
            if (list[0].MajorTypeID == 2 ||  list[0].MajorTypeID == 5)
            {
                if (string.IsNullOrEmpty(list[0].RubCode))
                {
                    string RubCode;
                    EntityArrayList<BasRubInfo> Rubs = basRubInfoManager.GetListByWhere(BasRubInfo._.RubName == tm.Mater_Name);
                    if (Rubs.Count == 0)
                    {
                        RubCode = InsertRubinfo(tm.Mater_Name, erpcode, tm.Mater_Type);
                    }
                    else
                    {
                        RubCode = Rubs[0].RubCode;
                    }
                  

                    BasMaterial bm = list[0];
                    bm.RubCode = RubCode;
                    basMaterialManager.Update(bm);

                }
            }

            //yzx 180926
            if ( list[0].MajorTypeID == 4 )
            {
                if (string.IsNullOrEmpty(list[0].RubCode))
                {
                    string RubCode;
                    int i = tm.Mater_Name.IndexOf("/");
                    if (i >= 0)
                    {
                        EntityArrayList<BasRubInfo> Rubs = basRubInfoManager.GetListByWhere(BasRubInfo._.RubName == tm.Mater_Name.Substring(0,i));
                        if (Rubs.Count == 0)
                        {
                            RubCode = InsertRubinfo(tm.Mater_Name, erpcode, tm.Mater_Type);
                        }
                        else
                        {
                            RubCode = Rubs[0].RubCode;
                        }


                        BasMaterial bm = list[0];
                        bm.RubCode = RubCode;
                        basMaterialManager.Update(bm);
                    }
                }
            }
            //yzx 180926
            if (list[0].MajorTypeID == 2)
            {
                if (string.IsNullOrEmpty(list[0].RubCode))
                {
                    string RubCode;
                    int i = tm.Mater_Name.IndexOf("-");
                    if (i >= 0)
                    {
                        EntityArrayList<BasMaterial> Rubs = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == tm.Mater_Name.Substring(0, i));
                        if (Rubs.Count == 0)
                        {
                            //RubCode = InsertRubinfo(tm.Mater_Name, erpcode, tm.Mater_Type);
                        }
                        else
                        {
                            RubCode = Rubs[0].RubCode;
                            BasMaterial bm = list[0];
                            bm.RubCode = RubCode;
                            basMaterialManager.Update(bm);
                        }


                       
                    }
                }
            }


            if (list[0].DeleteFlag == "1")
            {
                BasMaterial bm = list[0];
                bm.DeleteFlag = "0";
                basMaterialManager.Update(bm);
            }
            if (list[0].MaterialName != tm.Mater_Name)
            {
                BasMaterial bm = list[0];
                bm.MaterialName = tm.Mater_Name;
                basMaterialManager.Update(bm);
            }
           
            materialcode = list[0].MaterialCode;
        }
        return materialcode;

    }
    #endregion

    #region 添加胶料及物料信息
    /// <summary>
    /// 添加物料信息
    /// 于小鹏 @ 2013-04-18 18:27:00 
    /// </summary>
    /// <param name="majorTypeID"></param>
    /// <param name="minorTypeID"></param>
    /// <param name="fatherName"></param>
    /// <param name="tm"></param>
    /// <param name="erpcode"></param>
    /// <returns></returns>
    private string InsertMaterialCode(string majorTypeID, string minorTypeID, string fatherName,TblMater tm,string erpcode )
    {
        string materialcode = string.Empty;
        BasMaterial material = new BasMaterial();
        EntityArrayList<BasMaterial> list = new EntityArrayList<BasMaterial>();
        list = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == fatherName);
        string RubCode = string.Empty;
        EntityArrayList<BasRubInfo> Rubs = basRubInfoManager.GetListByWhere(BasRubInfo._.RubName == fatherName);
        if (Rubs.Count == 0)
        {
            RubCode = InsertRubinfo(fatherName, erpcode,tm.Mater_Type);
        }
        else
        {
            RubCode = Rubs[0].RubCode;
        }
        material.RubCode = RubCode;
        material.MajorTypeID = Convert.ToInt32(majorTypeID);
        material.MinorTypeID = minorTypeID.PadLeft(2, '0');
        string nextRubInfoCode = basMaterialManager.GetNextMaterialCode(majorTypeID, material.MinorTypeID, material.RubCode, false);
        material.ObjID = Convert.ToInt32(basMaterialManager.GetNextMaterialObjID());
        material.MaterialCode = nextRubInfoCode;
        //if (list.Count == 0)
        //{
        //    material.ProductMaterialCode =material.MaterialCode;
        //}
        //else
        //{
        //    material.ProductMaterialCode = list[0].MaterialCode;
        //}
        if (majorTypeID == "4")
        {
            material.MaxParkTime = 336;
            material.MinParkTime = 4;
        }
        else if (majorTypeID == "5" || majorTypeID == "6")
        {
            material.MaxParkTime = 168;
            material.MinParkTime = 8;
        }

       



        #region  插入胶料分类
        if (tm.Mater_Type.Length > 1)
        {
            switch (tm.Mater_Type.Substring(0, 2))
            {
                case "全钢":
                    material.MaterialSimpleName = "全钢混炼胶";
                    break;
                case "半钢":
                    material.MaterialSimpleName = "半钢混炼胶";
                    break;
                case "特胎":
                    material.MaterialSimpleName = "特胎混炼胶";
                    break;
                case "斜胶":
                    material.MaterialSimpleName = "斜胶混炼胶";
                    break;
                case "杂胶":
                    material.MaterialSimpleName = "杂胶混炼胶";
                    break;
                default:
                     material.MaterialSimpleName = "";///胶料类型
                    break;
            }
        }
        #endregion
        material.MaterialName = tm.Mater_Name;
        material.MaterialOtherName = tm.Mater_Name;
        material.ERPCode = tm.Mater_Code;
        string[] str1 = material.ERPCode.Split('_');
        int len=str1[0].Length;
        material.SAPMaterialShortCode = str1[0].Substring(len - 9, 9) + "_";
        material.DeleteFlag = "0";
        material.MaterialLevel = "0";
        material.IsQualityRateCount = "1";
        material.OperSourceTemp = "PDM";
        basMaterialManager.Insert(material);
        materialcode = material.MaterialCode;
        return materialcode;
    }
    /// <summary>
    /// 添加新胶料
    /// 于小鹏@ 2013-04-17 8:47:00 
    /// </summary>
    /// <param name="fatherName"></param>
    /// <param name="erpcode"></param>
    /// <returns></returns>
    protected string InsertRubinfo(string fatherName, string erpcode,string materType)
    {
        string rubcode = string.Empty;
        BasRubInfo rub = new BasRubInfo();

        rub.RubName = fatherName;
        rub.RubCode = basRubInfoManager.GetNextRubInfoCode();
        rub.ObjID = Convert.ToInt32(rub.RubCode);
        rub.FactoryID = 4344;
        rub.DeleteFlag = "0";
        if (materType.Length > 1)
        {
            switch (materType.Substring(0, 2))
            {
                case "全钢":
                    rub.RubTypeCode = "1";
                    break;
                case "半钢":
                    rub.RubTypeCode = "2";
                    break;
                case "特胎":
                    rub.RubTypeCode = "3";
                    break;
                case "斜胶":
                    rub.RubTypeCode = "4";
                    break;
                case "杂胶":
                    rub.RubTypeCode = "5";
                    break;
                default:
                    rub.RubTypeCode = "1";///胶料类型
                    break;
            }
        }

   
     
        basRubInfoManager.Insert(rub);
        rubcode= rub.RubCode;
        return rubcode;
    }
    /// <summary>
    /// 获取物料代码，但首先判断是否存在此胶料（母炼），如否则添加
    /// </summary>
    /// <param name="erpcode"></param>
    /// <returns></returns>
    private string GetChild_Matercode(string erpcode, string fatherErpcode)
    {
        TblMater tm = new TblMater();
        if (tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode).Count > 0)
        {
            tm = tblMaterManager.GetListByWhere(TblMater._.Mater_Code == erpcode)[0];
        }
        else
        {
            return "请认真确认 " + erpcode + " 是否为正确erp号！！";
        }
        BasMaterial material = new BasMaterial();
        string str = tm.Mater_Name;

        EntityArrayList<BasMaterial> list = new EntityArrayList<BasMaterial>();
        #region 判断反回胶
        if (str.Substring(str.Length - 2, 2) == "-F")
        {
            list = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == str);

            if (list.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(list[0].ERPCode) || list[0].ERPCode != erpcode)
                {
                    list[0].ERPCode = erpcode;
                    basMaterialManager.Update(list[0]);
                }

            }

        }
        #endregion
        else
        {
            list = basMaterialManager.GetListByWhere(BasMaterial._.ERPCode == erpcode);
        }

        if (list.Count == 0)
        {
            list = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == str);
            if (list.Count > 0)
            {
                list[0].ERPCode = erpcode;
                basMaterialManager.Update(list[0]);
            }
            else
            { 
         
                string majorTypeID = string.Empty;
                string minorTypeID = string.Empty;
                if (erpcode.Substring(0, 1) == "3")//终炼胶
                {
                    majorTypeID = "5";
                    minorTypeID = "1";
                }
                if (str.Substring(str.Length - 1, 1) == "e")
                {
                    if (str.Substring(str.Length - 4, 1) == "/")//母炼胶
                    {
                        majorTypeID = "4";
                        minorTypeID = str.Substring(str.Length - 1);
                    }
                    else
                    {
                        majorTypeID = "5";
                        minorTypeID = "1";
                    }
                }
                else if (str.Substring(str.Length - 3, 1) == "/")//母炼胶
                {
                    majorTypeID = "4";
                    minorTypeID = str.Substring(str.Length - 1);
                }
                else if (str.Substring(str.Length - 2, 2) == "-F")//返回胶
                {
                    majorTypeID = "6";
                    minorTypeID = "1";
                }
                else if (erpcode.Substring(0, 1) == "1")//辅料
                {
                    majorTypeID = "1";
                    switch (erpcode.Substring(0, 6))
                    {
                        case "101001":
                            minorTypeID = "1";
                            break;
                        case "101002":
                            minorTypeID = "2";
                            break;
                        case "101003":
                            minorTypeID = "3";
                            break;
                        case "102001":
                            minorTypeID = "4";
                            break;
                        case "102002":
                            minorTypeID = "5";
                            break;
                        case "102003":
                            minorTypeID = "6";
                            break;
                        case "102004":
                            minorTypeID = "7";
                            break;
                        case "102005":
                            minorTypeID = "14";
                            break;
                        default:
                            minorTypeID = "14";// yzx 待维护
                            break;
                    }

                 }
                material.RubCode = getrubcode(fatherErpcode);
                material.MajorTypeID = Convert.ToInt32(majorTypeID);
                material.MinorTypeID = minorTypeID.PadLeft(2, '0');
                string nextRubInfoCode = basMaterialManager.GetNextMaterialCode(majorTypeID, material.MinorTypeID, material.RubCode, false);
                material.ObjID = Convert.ToInt32(basMaterialManager.GetNextMaterialObjID());
                material.MaterialCode = nextRubInfoCode;
                material.MaterialName = tm.Mater_Name;
                material.MaterialOtherName = tm.Mater_Name;
                material.ERPCode = tm.Mater_Code;
                material.DeleteFlag = "0";
                if (fatherErpcode.Substring(0, 1) == "2" || fatherErpcode.Substring(0, 1) == "4"||fatherErpcode.Substring(0, 1) =="5")
                {
                    material.ProductMaterialCode = fatherErpcode;
                }
                else
                {
                    material.ProductMaterialCode = GetMatercode(fatherErpcode);
                }
                 basMaterialManager.Insert(material);
            }
        }
        string materialcode = basMaterialManager.GetListByWhere(BasMaterial._.ERPCode == erpcode)[0].MaterialCode;
        return materialcode;
    }
    #endregion

    #region 获取 erp、物料名、机台类型 方法
    /// <summary>
    /// 获取ERP编码
    /// </summary>
    /// <param name="matercode"></param>
    /// <returns></returns>
    private string GetErpcode(string matercode)
    {
        string materialcode = string.Empty;
        EntityArrayList<BasMaterial> list = new EntityArrayList<BasMaterial>();
        list = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == matercode);
        string erpcode = list[0].ERPCode;
        return erpcode;
    }
    /// <summary>
    /// 获得物料名称
    /// 于小鹏 @ 2013-04-18 15:17:05
    /// </summary>
    /// <param name="recipeInfo"></param>
    private string GetMaterName(string erpcode)
    {
        string matercode = GetMatercode(erpcode);
        string materialname = string.Empty;
        materialname = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == matercode)[0].MaterialName;
        return materialname;     
    }
  
    /// <summary>
    /// 获得机台类型
    /// 于小鹏 @ 2013-04-18 15:17:05
    /// </summary>
    /// <param name="recipeInfo"></param>
    private string GetEquiType(string erpcode)
    {
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == "EquipType");
        string str = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        string EquiType = string.Empty;
        BasEquipManager basEquipManager = new BasEquipManager();
        string EquiName = basEquipManager.GetListByWhere(BasEquip._.EquipCode == erpcode)[0].EquipName;
        //丁基机台特殊处理
        if (pmtConfigManager.GetBySql(String.Format(str, erpcode)).ToArrayList<BasEquip>().Count != 0)
        {
            string[] Arry = EquiName.Split('-');
            EquiType = Arry[1].ToString();
            EquiType = EquiType + "丁基";
        }
        else
        {
            string[] Arry = EquiName.Split('-');
            EquiType = Arry[1].ToString();
        }

        return EquiType;
    }
    #endregion


    /// <summary>
    /// 获取配方版本号
    /// 孙本强 @ 2013-04-03 12:46:40
    /// </summary>
    /// <param name="recipeInfo">The recipe info.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private int GetMaxRecipeVersionID(PmtRecipe pmtRecipe)
    {
        int Result = 0;
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(
            PmtRecipe._.RecipeEquipCode == pmtRecipe.RecipeEquipCode
            && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode);
        foreach (PmtRecipe m in lst)
        {
            if (pmtRecipe.ObjID == m.ObjID)
            {
                return Convert.ToInt32(m.RecipeVersionID);
            }
            if (Result < m.RecipeVersionID)
            {
                Result = Convert.ToInt32(m.RecipeVersionID);
            }
        }
        return Result + 1;
    }

    #region 基础信息
    /// <summary>
    /// Inis the PMT recipe.
    /// 孙本强 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="record">The record.</param>
    /// <param name="m">The m.</param>
    /// <remarks></remarks>
    private void IniPmtRecipe(TblRecipeManager.QueryParams queryParams, ref PmtRecipe m)
    {
        #region 判断是更新配方还是新增配方
        EntityArrayList<PmtRecipe> listPR = new EntityArrayList<PmtRecipe>();
        WhereClip whereR = new WhereClip();
        if (!String.IsNullOrEmpty(queryParams.Mater_code))
        {
            whereR.And(PmtRecipe._.RecipeMaterialCode == GetMatercode(queryParams.Mater_code));
        }
        if (!String.IsNullOrEmpty(queryParams.Equip_code))
        {
            whereR.And(PmtRecipe._.RecipeEquipCode == queryParams.Equip_code);
        }
        //2016-09-30 李昊
        SysCodeManager basSysCodeManager = new SysCodeManager();
        EntityArrayList<SysCode> sc = basSysCodeManager.GetListByWhere(SysCode._.Remark == (queryParams.Recipe_type.ToString().Trim() + queryParams.Routing_type.ToString().Trim()));
        if (sc.Count > 0)
        {
            if (sc[0].Remark.Substring(0, 1) == queryParams.Recipe_type.ToString().Trim())
                whereR.And(PmtRecipe._.RecipeType == sc[0].ItemCode);
            else
            { whereR.And(PmtRecipe._.RecipeType == sc[1].ItemCode); sc[0] = sc[1]; }
        }
        else
        { X.Js.Alert(queryParams.Recipe_type.ToString().Trim() + queryParams.Routing_type.ToString().Trim()+" 对应配方类型不存在");
        return;
        }

        listPR = pmtRecipeManager.GetListByWhere(whereR);
        if (listPR.Count > 0)
        {
            foreach (PmtRecipe p in listPR)
            {
                p.RecipeState = "2";
                string result = pmtRecipeManager.UpdateRecipe(p);
            }
        }
        #endregion
        #region 查询配方源数据
        TblRecipe re = new TblRecipe();

        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(queryParams.Mater_code))
        {
            where.And(TblRecipe._.Mater_code == queryParams.Mater_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Equip_code))
        {
            where.And(TblRecipe._.Equip_code == queryParams.Equip_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Edt_code))
        {
            where.And(TblRecipe._.Edt_code == queryParams.Edt_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Recipe_type))
        {
            where.And(TblRecipe._.Recipe_type == queryParams.Recipe_type);
        }
        if (!String.IsNullOrEmpty(queryParams.Routing_type))
        {
            where.And(TblRecipe._.Routing_type == queryParams.Routing_type);
        }
       
       
        try
        {
            re = tblRecipeManager.GetListByWhere(where)[0];
        }
        catch
        { }
        #endregion
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();

        BasEquipManager basEquipManager = new BasEquipManager();

        EntityArrayList<BasEquip> eq = basEquipManager.GetListByWhere(BasEquip._.EquipCode == queryParams.Equip_code);

        //whereR.And(PmtRecipe._.RecipeVersionID == re.Edt_code);
        //whereR.And(PmtRecipe._.RecipeType == converter.ToInt(sc[0].ItemCode));
        EntityArrayList<PmtRecipe> listOPR = new EntityArrayList<PmtRecipe>();
        OrderByClip order = new OrderByClip();
        order = PmtRecipe._.RecipeVersionID.Asc;
        listOPR = pmtRecipeManager.GetListByWhereAndOrder(whereR, order);

        int? iNull = converter.ToInt(re.Edt_code);  //版本号
        if (iNull != null)
        {
            m.RecipeVersionID = (int)iNull;
        }
        else m.RecipeVersionID = 1;  //--yzx180912


        if (listOPR.Count > 0)
        {
            m = listOPR[0];//如果要每次导入都新增版本号则注释本行 yzx 19.03.11

        }
       

     
        //m.RecipeType = converter.ToInt(re.Recipe_type);
        m.RecipeType = converter.ToInt(sc[0].ItemCode);

        m.RecipeName = converter.ToString(IniRecipeName(m.RecipeType.ToString(), Convert.ToInt32(m.RecipeVersionID))); //配方编号
        m.RecipeMaterialCode = GetMatercode(re.Mater_code);  //物料名称
        m.RecipeUserVersion = re.Edt_code.ToString();//pdm版本
        m.RecipeEquipCode = converter.ToString(re.Equip_code);  //机台名称
        m.RecipeState = converter.ToString("1");  //配方状态
        m.RecipeDefineDate = Convert.ToDateTime(re.Modify_time);//配方修改时间
        m.LotTotalWeight = converter.ToDecimal(re.Set_weigh);  //配方总重
        m.ShelfLotCount = converter.ToInt(re.Shelf_num);  //每架车数
        m.OverTimeSetTime = converter.ToInt(re.OverTime_Time);  //超时排胶时间
        m.OverTempSetTemp = converter.ToInt(re.OverTemp_Temp);  //紧急排胶温度
        m.OverTempMinTime = converter.ToInt(re.OverTemp_MinTime);  //超温排胶最短时间
        m.InPolyMaxTemp = converter.ToInt(re.Max_InPloyTemp);  //最高进胶温度
        m.InPolyMinTemp = converter.ToInt(re.Min_InPloyTemp);  //最低进胶温度
        m.MakeUpTemp = converter.ToInt(re.MakeUp_Temp);//补偿温度
        m.CarbonRecycleType = converter.ToString(re.CB_RecycleType);  //炭黑是否回收
        m.CarbonRecycleTime = converter.ToInt(re.CB_RecycleTime);  //炭黑回收时间
        try
        {
            //m.LotDoneTime = converter.ToInt(re.LotDoneTime);//每车标准时间
        }
        catch
        { m.LotDoneTime = 0; }
        //闫志旭 2015.6.9
        m.B_Version = re.B_Version;
        m.R_Version = re.R_Version;
        m.S_Factory = re.S_Factory;
        m.JieDuan = re.JieDuan;
        //m.FactoryCode = re.FactoryCode;
        //李昊 2016-09-09
        if (sc.Count > 0)
        {
            m.RecipeType = converter.ToInt(sc[0].ItemCode);
        }
        else
        {
            m.RecipeType = 0;
        }
      
        //m.RecipeVersionID = re.Edt_code;
        //--yzx180912
       // m.RecipeName = converter.ToString(IniRecipeName(m.RecipeType.ToString(), re.Edt_code)); //配方编号
      
       //DataSet ds = tblRecipeManager.GetBySql(" select MAX(RecipeVersionID) from pmtrecipe  where RecipeEquipCode= '" + m.RecipeEquipCode + "' and RecipeMaterialCode = '" + m.RecipeMaterialCode + "'").ToDataSet();
       //int vid = 1;
       //try { vid = int.Parse(ds.Tables[0].Rows[0][0].ToString()); vid++; }
       // catch(Exception e2){}
       //m.RecipeVersionID = vid;

        if (string.IsNullOrEmpty(re.Is_UseAreaTemp))
        {
            m.IsUseAreaTemp = "0";
        }
        else
        {
            m.IsUseAreaTemp = converter.ToString(re.Is_UseAreaTemp);  //使用三区温度
        }
        m.SideTemp = converter.ToInt(re.Side_Temp);  //侧壁温度
        m.RollTemp = converter.ToInt(re.Roll_Temp);  //转子温度
        //m.RollTempDiff = converter.ToInt(record["RollTempDiff"]);  //转子温差
        m.DdoorTemp = converter.ToInt(re.Ddoor_Temp);  //卸料门温度
        m.CanAuditUser = hidden_AuditUser.Text.ToString();
        m.OperCode = this.UserID + "+"; //用户名//加“+”的用意是区别导入配方
        m.NewFlag = "0";


        if (string.IsNullOrEmpty(re.SAP_VersionID))
        { m.SAPVersionID = "M" + Convert.ToString(eq[0].WorkShopCode) + re.SAP_VersionID; isplm = 0; }
        else
        if (re.SAP_VersionID.Length <= 2 )
        {
            m.SAPVersionID = "M" + Convert.ToString(eq[0].WorkShopCode) + re.SAP_VersionID; isplm = 0;
        }
        else
        { m.SAPVersionID = re.SAP_VersionID; isplm = 1; }

    }
#endregion

    #region 称量信息
    /// <summary>
    /// 控制油（2）称的选择
    /// 于小鹏@ 2013-4-26
    /// </summary>
    /// <param name="minor"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    private bool GetTableByPYPmtConfig(string query1, string query2)
    {
        EntityArrayList<TblWeight> list = new EntityArrayList<TblWeight>();
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == PmtConfigManager.TypeCode.WeightTypey.ToString());

        string sqlstr = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        query1 = query1.Trim();
        query2 = query2.Trim();

        list = pmtConfigManager.GetBySql(String.Format(sqlstr, query1, query2)).ToArrayList<TblWeight>();
        if (list.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Inis the PMT recipe weight.
    /// 孙本强 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="arry">The arry.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private string IniPmtRecipeWeight(TblRecipeManager.QueryParams queryParams, ref EntityArrayList<PmtRecipeWeight> lst)
    {
        EntityArrayList<TblWeight> we = new EntityArrayList<TblWeight>();
        EntityArrayList<TblWeight> TanHei = new EntityArrayList<TblWeight>();
        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(queryParams.Mater_code))
        {
            where.And(TblWeight._.Mater_code == queryParams.Mater_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Equip_code))
        {
            where.And(TblWeight._.Equip_code == queryParams.Equip_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Edt_code))
        {
            where.And(TblWeight._.Edt_code == queryParams.Edt_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Recipe_type))
        {
            where.And(TblWeight._.Recipe_type == queryParams.Recipe_type);
        }
        if (!String.IsNullOrEmpty(queryParams.Routing_type))
        {
            where.And(TblWeight._.Routing_type == queryParams.Routing_type);
        }
        try
        {
            //we = tblWeightManager.GetListByWhere(where, BasWorkShop._.ObjID.Asc);
            we = tblWeightManager.GetListByWhereAndOrder(where, TblWeight._.Weight_id.Asc);
            //tblWeightManager.GetListByWhereAndOrder
        }
        catch
        { }
        where.And(TblWeight._.Weight_type == "0");
        TanHei = tblWeightManager.GetListByWhere(where);

        EntityArrayList<TblWeight> xlz = new EntityArrayList<TblWeight>();
        EntityArrayList<TblWeight> xlr = new EntityArrayList<TblWeight>();
        decimal? setweightz = 0;
        decimal? setweightr = 0;
        string result = string.Empty;
        int[] weight = new int[18] { 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1 ,20,30,40,50,60,64,68};
        weight[9] = 2;//终炼胶料称称量小料过渡时取用
        int XLWeight = 10; int PLMXLWeightR = 10; int PLMXLWeightZ = 10;
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();
        for (int i = 0; i < we.Count; i++)
        {
            PmtRecipeWeight m = new PmtRecipeWeight();

            if (String.IsNullOrEmpty(we[i].Weight_Prop) || we[i].Weight_type>6)
            {
                
                m.RecipeMaterialCode = GetMatercode(we[i].Mater_code);
                m.RecipeEquipCode = converter.ToString(we[i].Equip_code);
                
                    m.MaterialCode = GetChild_Matercode(we[i].Child_matercode, we[i].Mater_code);
              
                if  (m.MaterialCode.Length>15)
                {
                    return m.MaterialCode;
                }
                string MajorTypeID = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == m.MaterialCode)[0].MajorTypeID.ToString();

                m.SetWeight = decimal.Round(Convert.ToDecimal(we[i].Set_Weight), 1, MidpointRounding.AwayFromZero);
                m.OldSetWeight = m.SetWeight;
                if (we[i].Error_Allow == 0)
                {

                    m.ErrorAllow = (decimal?)getErrorAllow(m.RecipeMaterialCode, m.MaterialCode);
                }
                else
                    m.ErrorAllow =we[i].Error_Allow;
              
                //闫志旭 2015.6.9
                m.B_Version = we[i].B_Version;
                m.S_Factory = we[i].S_Factory;
                try
                {
                    if (m.MaterialCode.Substring(0, 1) == "2")
                    {
                        m.CheckWeight = m.SetWeight;
                        if (m.CheckWeight >= 1)
                            m.CheckError = Decimal.Parse("0.030");
                        if (m.CheckWeight < 1 && m.CheckWeight>0)
                            m.CheckError = Decimal.Parse("0.020");
                        //m.CheckError = m.ErrorAllow;
                    }
                    String sql = "select materialcode from PmtCheckMaterSet where materialcode = '"+m.MaterialCode+"'";
                    DataSet ds = pmtRecipeWeightManager.GetBySql(sql).ToDataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        m.CheckWeight = m.SetWeight;
                        if (m.SetWeight == converter.ToDecimal(0.102))
                        {
                            m.CheckWeight = converter.ToDecimal(0.102);
                            m.CheckError = converter.ToDecimal(0.003);
                        }

                        if (m.SetWeight == converter.ToDecimal(0.204))
                        {
                            m.CheckWeight = converter.ToDecimal(0.204);
                            m.CheckError = converter.ToDecimal(0.005);
                        }
                        if (m.SetWeight == converter.ToDecimal(0.306))
                        {
                            m.CheckWeight = converter.ToDecimal(0.306);
                            m.CheckError = converter.ToDecimal(0.008);
                        }
                        if (m.SetWeight == converter.ToDecimal(0.408))
                        {
                            m.CheckWeight = converter.ToDecimal(0.408);
                            m.CheckError = converter.ToDecimal(0.010);
                        }
                        if (m.SetWeight == converter.ToDecimal(0.510))
                        {
                            m.CheckWeight = converter.ToDecimal(0.510);
                            m.CheckError = converter.ToDecimal(0.013);
                        }
                        if (m.SetWeight == converter.ToDecimal(0.612))
                        {
                            m.CheckWeight = converter.ToDecimal(0.612);
                            m.CheckError = converter.ToDecimal(0.015);
                        }
                    }



                }
                catch(Exception e2){}

                if (String.IsNullOrEmpty(we[i].Into_routing.ToString()))
                {
                    //if (!String.IsNullOrEmpty(we[i].Into_type))
                    
                    //m.Into_type = we[i].Into_type.Trim();
                }
                else
                {
                    if (!String.IsNullOrEmpty(we[i].Into_type.ToString()))
                    {
                        m.Into_type = we[i].Into_type.Trim() + we[i].Into_routing.ToString().Trim();
                    }
                }
                if (!String.IsNullOrEmpty(we[i].Supply_code.ToString()))
                { m.Supply_code = we[i].Supply_code; }
            
                SysCodeManager basSysCodeManager = new SysCodeManager();
                EntityArrayList<SysCode> sc = basSysCodeManager.GetListByWhere(SysCode._.Remark == (we[i].Recipe_type.Trim() + we[i].Routing_type.ToString().Trim()));
                if (sc.Count > 0)
                {
                    if (sc[0].Remark.Substring(0, 1) == we[i].Recipe_type.Trim())
                        m.Recipe_type = converter.ToInt(sc[0].ItemCode);
                    else
                    { m.Recipe_type = converter.ToInt(sc[1].ItemCode); sc[0] = sc[1]; }
                  
                }
                else
                {
                    m.Recipe_type = 0;
                }



                //m.r
                //判断分配油2称
                if (we[i].Weight_type == 1)
                {

                    if (GetTableByPYPmtConfig(we[i].Equip_code, we[i].Child_matercode))
                    {
                        m.WeightType = "5";
                    }
                    else
                    {
                        m.WeightType = converter.ToString(we[i].Weight_type);
                    }
                }
                else
                {
                    m.WeightType = converter.ToString(we[i].Weight_type);
                }




                if (we[i].Weight_type == 7)//开炼
                {


                    m.WeightType = "6";

                }
                if (we[i].Weight_type == 8)//开炼返回胶
                {


                    m.WeightType = "7";

                }
                if (m.RecipeMaterialCode.Substring(0, 1) == "4")
                {
                 #region 为母炼胶料称分配称量顺序
                    if (m.WeightType == "2")
                    {
                        if (m.MaterialCode.Substring(0, 1) == "4")
                        {
                            m.WeightID = weight[13];
                            weight[13]++;
                        }
                        else if (m.MaterialCode.Substring(0, 3) == "101")
                        {
                            m.WeightID = weight[11];
                            weight[11]++;
                        }
                        else if (m.MaterialCode.Substring(0, 3) == "102")
                        {
                            m.WeightID = weight[12];
                            weight[12]++;
                        }
                        else if (m.MaterialCode.Substring(0, 3) == "107")
                        {
                            m.WeightID = weight[14];
                            weight[14]++;
                        }
                        else 
                        {
                            m.WeightID = weight[12];
                            weight[12]++;
                        }
                        //yzx 18.0727
                        m.WeightID = we[i].Weight_id;
                        weight[2]++;
                    }
                    #endregion
                 #region 为炭黑分配添加顺序
                    else if (m.WeightType == "0")
                    {
                        if (TanHei.Count >= 3)
                        {
                            string name = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == m.MaterialCode)[0].MaterialName;
                            if (name.Contains("白炭黑"))
                            {
                                m.WeightID = weight[15];
                                weight[15]++;
                            }
                            else if (name.Contains("炭黑"))
                            {
                                m.WeightID = weight[16];
                                weight[16]++;
                            }
                            else if (name.Contains("碳酸钙"))
                            {
                                m.WeightID = weight[17];
                                weight[17]++;
                            }
                            else
                            {
                                m.WeightID = weight[17];
                                weight[17]++;
                            }
                        }
                        else if (TanHei.Count ==2)
                        {
                            if (we[i].Set_Weight >= TanHei[0].Set_Weight && we[i].Set_Weight >= TanHei[1].Set_Weight)
                            {
                                m.WeightID = weight[15];
                                weight[15]++;
                            }
                            else
                            {
                                m.WeightID = weight[16];
                                weight[16]++;
                            }
                        }
                        else
                        {
                            m.WeightID = weight[Convert.ToInt32(m.WeightType)];
                            weight[Convert.ToInt32(m.WeightType)]++;
                        }
                
                    }
                 #endregion
                    else
                    {
                        m.WeightID = weight[Convert.ToInt32(m.WeightType)];
                        weight[Convert.ToInt32(m.WeightType)]++;
                    }
                    XLWeight = weight[13];
                }
                else
                {
                    if (m.WeightType == "2")
                    {
                        if (m.MaterialCode.Substring(0, 1) == "4")
                        {
                            m.WeightID = weight[11];
                            weight[11]++;
                        }
                        else if (m.MaterialCode.Substring(0, 1) == "2")
                        {
                            m.WeightID = weight[12];
                            weight[12]++;
                        }
                        else if (m.MaterialCode.Substring(0, 1) == "6")
                        {
                            m.WeightID = weight[14];
                            weight[13]++;
                        }
                        else
                        {
                            m.WeightID = weight[12];
                            weight[12]++;
                        }
                    }
                    //m.WeightID = weight[Convert.ToInt32(m.WeightType)];
                    m.WeightID = we[i].Weight_id;
                    weight[Convert.ToInt32(m.WeightType)]++;
                    if (weight[2] == 2)
                    {
                        weight[2] = weight[2] + 2;
                    }
                    XLWeight = weight[9];
                }
                m.ActCode = "0";////////////////油and炭黑需要卸料
                string pagetypename = "Page@";
                if (m.WeightType.ToLower().StartsWith(pagetypename.ToLower()))
                {
                    m.WeightType = m.WeightType.Substring(pagetypename.Length).Trim();
                }
                lst.Add(m);
            }
            else if (we[i].Weight_Prop.Contains("自"))
            {
                xlz.Add(we[i]);
                setweightz = setweightz + decimal.Round(Convert.ToDecimal(we[i].Set_Weight), 2, MidpointRounding.AwayFromZero);
               PLMXLWeightZ= we[i].Weight_id;
            }
            else if (we[i].Weight_Prop.Contains("人"))
            {
                xlr.Add(we[i]);
                setweightr = setweightr + decimal.Round(Convert.ToDecimal(we[i].Set_Weight), 2, MidpointRounding.AwayFromZero);
                PLMXLWeightR = we[i].Weight_id;
            }

        }
       #region 判断一个配方中使用人工或自动小料
        string weightType = string.Empty;
        string materialcode = string.Empty;
        if (xlz.Count > 0 && xlr.Count > 0)
        {
            if (setweightr < setweightz)
            {
                 //weight[3]替换为 PLMXLWeight
                PmtRecipeWeight m1 = getXLRecipeWeight(xlz, setweightz, weight[3], PLMXLWeightZ, ref  weightType);
                weight[Convert.ToInt32(weightType)]++;
                result = Inixiaoliao(xlz, setweightz, m1.MaterialName, queryParams.Mater_code,ref materialcode);
                m1.MaterialCode = materialcode;
                m1.ErrorAllow = (decimal?)getErrorAllow(m1.RecipeMaterialCode, m1.MaterialCode);
                lst.Add(m1);
                PmtRecipeWeight m = getXLRecipeWeight(xlr, setweightr, weight[3], PLMXLWeightR, ref weightType);
                weight[Convert.ToInt32(weightType)]++;
                result = Inixiaoliao(xlr, setweightr, m.MaterialName, queryParams.Mater_code, ref materialcode);
                m.MaterialCode = materialcode;
                m.ErrorAllow = (decimal?)getErrorAllow(m.RecipeMaterialCode, m.MaterialCode);
                lst.Add(m);
            }
            else
            {
                PmtRecipeWeight m = getXLRecipeWeight(xlr, setweightr, weight[3], PLMXLWeightR, ref weightType);
                weight[Convert.ToInt32(weightType)]++;
                result = Inixiaoliao(xlr, setweightr, m.MaterialName, queryParams.Mater_code, ref materialcode);
                m.MaterialCode = materialcode;
                m.ErrorAllow = (decimal?)getErrorAllow(m.RecipeMaterialCode, m.MaterialCode);
                lst.Add(m);

                PmtRecipeWeight m1 = getXLRecipeWeight(xlz, setweightz, weight[3], PLMXLWeightZ, ref weightType);
                weight[Convert.ToInt32(weightType)]++;
                result = Inixiaoliao(xlz, setweightz, m1.MaterialName, queryParams.Mater_code, ref materialcode);
                m1.MaterialCode = materialcode;
                m1.ErrorAllow = (decimal?)getErrorAllow(m1.RecipeMaterialCode, m1.MaterialCode);
                lst.Add(m1);
            }

        }
        else
        {
            if (xlz.Count > 0)
            {
                PmtRecipeWeight m = getXLRecipeWeight(xlz, setweightz, weight[3], PLMXLWeightZ, ref weightType);
                result = Inixiaoliao(xlz, setweightz, m.MaterialName, queryParams.Mater_code, ref materialcode);
                m.MaterialCode = materialcode;
                m.ErrorAllow = (decimal?)getErrorAllow(m.RecipeMaterialCode, m.MaterialCode);
                lst.Add(m);
            }
            if (xlr.Count > 0)
            {
                PmtRecipeWeight m = getXLRecipeWeight(xlr, setweightr, weight[3], PLMXLWeightR, ref weightType);
                result = Inixiaoliao(xlr, setweightr, m.MaterialName, queryParams.Mater_code, ref materialcode);
                m.MaterialCode = materialcode;
                m.ErrorAllow = (decimal?)getErrorAllow(m.RecipeMaterialCode, m.MaterialCode);
                lst.Add(m);
            }
        }
#endregion
       #region 为炭黑、油等分配添加卸料动作
        if (weight[0] > 1 || weight[15] > 60 || weight[16] > 64 || weight[17] > 68)//炭黑称
        {
            PmtRecipeWeight m = IniDischarge("0", weight[17], queryParams);
            lst.Add(m);
        }
        if (weight[1] > 1)//油（1）称
        {
            PmtRecipeWeight m = IniDischarge("1", weight[1], queryParams);
            lst.Add(m);
        }
        if (weight[5] > 1)//油（2）称
        {
            PmtRecipeWeight m = IniDischarge("5", weight[5], queryParams);
            lst.Add(m);
        }
        if (weight[7] > 1)//一次法返回胶
        {
            PmtRecipeWeight m = IniDischarge("7", weight[7], queryParams);
            lst.Add(m);
        }
        #endregion
       #region 按步序排定顺序
        EntityArrayList<PmtRecipeWeight> tws = new EntityArrayList<PmtRecipeWeight>();
        tws = lst;

        for (int j = 1; j < tws.Count; j++)
        {
            if (string.IsNullOrWhiteSpace(tws[j].WeightID.ToString()))
            {
                tws = lst;
                break;
            }

            for (int i = 0; i < tws.Count - 1; i++)
            {
                if (Convert.ToInt32(tws[i].WeightID) > Convert.ToInt32(tws[i + 1].WeightID))
                {
                    PmtRecipeWeight tl = new PmtRecipeWeight();
                    tl = tws[i + 1];
                    tws[i + 1] = tws[i];
                    tws[i] = tl;
                }
            }
        }
        lst = tws;
        #endregion
        foreach (PmtRecipeWeight MW in lst)
        {
            if (MW.ErrorAllow == 999999999)
            {
                return "请判断物料" + MW.MaterialName + "的ERP号是否正常！！";
            }
        }
        return result;
    }
    /// <summary>
    /// 称量信息中加卸料动作
    /// 于小鹏 2013-04-26
    /// </summary>
    /// <param name="type"></param>
    /// <param name="weightID"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    protected PmtRecipeWeight IniDischarge(string type, int weightID, TblRecipeManager.QueryParams queryParams)
    {
        PmtRecipeWeight m = new PmtRecipeWeight();
        m.RecipeMaterialCode = GetMatercode(queryParams.Mater_code);
        m.RecipeEquipCode = queryParams.Equip_code;
        m.RecipeVersionID = Convert.ToInt32(queryParams.Edt_code);
        m.WeightType = type;
        m.WeightID = weightID;
        m.ActCode = "2";//卸料
        return m;
    }

    /// <summary>
    /// 称量信息中捏合药品数据为小料
    /// 于小鹏 2013-04-22
    /// </summary>
    /// <param name="xl"></param>
    /// <param name="setweight"></param>
    /// <param name="WeightID"></param>
    /// <returns></returns>
    private PmtRecipeWeight getXLRecipeWeight(EntityArrayList<TblWeight> xl, decimal? setweight, int WeightIDX, int WeightXLIdex, ref string weightType)
    {
        int? workshop;
        PmtRecipeWeight m = new PmtRecipeWeight();
       EntityArrayList< BasEquip> BEs =new EntityArrayList<BasEquip>();
        BEs= basEquipManager.GetListByWhere(BasEquip._.EquipCode == xl[0].Equip_code);
        workshop = BEs[0].WorkShopCode;
        m.RecipeMaterialCode = GetMatercode(xl[0].Mater_code);
       
        if (m.RecipeMaterialCode.Substring(0, 1) == "5" && xl[0].Weight_type==3)//终炼胶小料全放入胶料称中称量，母炼胶中如果包含多种小料，则按先后顺序先放入小料称后放入胶料称（顺序按重量已排定）
        {
            m.WeightType = "3";
            m.WeightID = WeightXLIdex;
            weightType = m.WeightType;
        }
        else if (m.RecipeMaterialCode.Substring(0, 1) == "5")
        {
            m.WeightType = "2";
            m.WeightID = WeightXLIdex;
            weightType = "9";
        }
        else
        {
            if (xl[0].Weight_type.ToString() == "6")
            {
                m.WeightType = "2";
                m.WeightID = WeightXLIdex;
                weightType = "13";
            }

            else
            {
                m.WeightType = xl[0].Weight_type.ToString();
                m.WeightID = WeightIDX;
                if (xl[0].Weight_type == 2)
                {
                    weightType = "13";
                }
                else
                {
                    weightType = m.WeightType;
                }
            }
            string q = m.RecipeMaterialCode.Substring(1);
        }
        m.RecipeEquipCode = xl[0].Equip_code;
        //2015.7.18 闫志旭 小料名字前添加配方类型 
        // 16.11.23 zy不需要车间 + "-M" + workshop
        //m.MaterialName = xl[0].Recipe_type.Trim()+xl[0].Routing_type.ToString().Trim()  + "-" + getxlname(xl[0].Mater_code, xl[0].Equip_code, xl[0].Weight_Prop);
        m.MaterialName = "M" + workshop + "-" + getxlname(xl[0].Mater_code, xl[0].Equip_code, xl[0].Weight_Prop, xl[0].Recipe_type.Trim() + xl[0].Routing_type.ToString().Trim());
        m.ActCode = "0";
        #region 为小料增加包装重量
        try
        {
            decimal XLStr = (decimal)pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == m.MaterialName)[0].Packweight;
            if (XLStr != 0 && XLStr != null)
            { setweight = setweight + XLStr / 1000; }
        }

        catch(Exception e2){}
      

        #endregion
        //if (m.WeightType == "2")//如果在胶料称称量小料，取两位小数
        //{
        setweight = decimal.Round(Convert.ToDecimal(setweight), 2, MidpointRounding.AwayFromZero);
       // }
        m.SetWeight = setweight;
        m.OldSetWeight = m.SetWeight;
        string pagetypename = "Page@";
        if (m.WeightType.ToLower().StartsWith(pagetypename.ToLower()))
        {
            m.WeightType = m.WeightType.Substring(pagetypename.Length).Trim();
        }
        return m;
    }
    /// <summary>
    /// 生成小料名
    /// </summary>
    /// <param name="matercode"></param>
    /// <param name="equip"></param>
    /// <param name="Weight_Prop"></param>
    /// <returns></returns>
    private string getxlname(string matercode, string equip, string Weight_Prop)
    {
        string workshopcode = basEquipManager.GetListByWhere(BasEquip._.EquipCode == equip)[0].WorkShopCode.ToString();
      //string xlName = GetMaterName(matercode) + "-" + GetEquiType(equip) + "-" + Weight_Prop + "*" + workshopcode;
        string xlName = GetMaterName(matercode) + "-" + GetEquiType(equip) + "-" + Weight_Prop;
        return xlName;

    }
    private string getxlname(string matercode, string equip, string Weight_Prop,string recipetype)
    {

        String sql = "select * from syscode where typeid ='pmttype' and remark  collate Chinese_PRC_CS_AS_WS  ='" + recipetype + "'";

        DataSet ds = basEquipManager.GetBySql(sql).ToDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //{recipetype=ds.Tables[0].Rows[0]["ItemName"].ToString(); }

        recipetype = ds.Tables[0].Rows[0]["ItemName"].ToString();

        string xlName = GetMaterName(matercode) + "(" + recipetype + ")" + "-" + GetEquiType(equip) + "-" + Weight_Prop;
        if (equip == "01028")
            xlName = GetMaterName(matercode) + "(" + recipetype + ")" + "-F370(增)-" + Weight_Prop;


        return xlName;

    }
    /// <summary>
    /// 根据erp号获取胶号
    /// </summary>
    /// <param name="erpcode"></param>
    /// <returns></returns>
    private string getrubcode(string code)
    {
        string rubcode = string.Empty;
        if (code.Substring(0, 1) == "C")
        {
            code = code.Substring(1);
        }
        if (code.Length > 12 && code.Trim().Substring(0, 1) != "S")
        {
            try
            {
                rubcode = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == code)[0].RubCode;
            }
            catch (Exception ex)
            {
                msg.Alert("操作", "erp编号格式不符合规范：" + ex);
                msg.Show();
            }
            
        }
        else
        {
            rubcode = basMaterialManager.GetListByWhere(BasMaterial._.ERPCode == code)[0].RubCode;
        }
        if (string.IsNullOrEmpty(rubcode))
        { throw new Exception("需要先维护" + code + "所属胶料信息！"); }
        return rubcode;

    }
    /// <summary>
    /// 导入小料配方
    /// 于小鹏 @ 2013-04-20 16:06:17
    /// </summary>
    /// <param name="xl"></param>
    private string Inixiaoliao(EntityArrayList<TblWeight> xl, decimal? setweight, string XiaoLiaoName, string ErpRecipeMatercode,ref string NewMaterialcode)
    {
        BasEquipManager basEquipManager = new BasEquipManager();
        EntityArrayList<BasEquip> eq = basEquipManager.GetListByWhere(BasEquip._.EquipCode == xl[0].Equip_code);
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();
        string RecipeMatercodeType = GetMatercode(ErpRecipeMatercode).Substring(0, 1);
        string EquipName = string.Empty;
     #region  区分招远和德州的机台
        if (xl[0].S_Factory == "8000")
        {
            if (eq[0].WorkShopCode == 2)//只有M2拥有两个自动小料
            {
                if (RecipeMatercodeType == "5")
                {
                    EquipName = "称量（终炼）";
                }
                else
                {
                    EquipName = "称量（母炼）";
                }
                if (xl[0].Weight_Prop.Contains("自"))
                {
                    EquipName = "自动" + EquipName;
                }
                else
                {
                    EquipName = "手工" + EquipName;
                }
            }
            else if (eq[0].WorkShopCode == 3)
            {
                if (RecipeMatercodeType == "5")
                {
                    EquipName = "称量（终炼）";
                }
                else
                {
                    EquipName = "称量（母炼）";
                }
                if (xl[0].Weight_Prop.Contains("自"))
                {
                    EquipName = "自动" + EquipName;
                }
                else
                {
                    EquipName = "手工" + EquipName;
                }
            }
            else
            {
                if (xl[0].Weight_Prop.Contains("自"))
                    EquipName = "自动称量";
                else
                {
                    if (RecipeMatercodeType == "5")
                    {
                        EquipName = "手工称量（终炼）";
                    }
                    else
                    {
                        EquipName = "手工称量（母炼）";
                    }
                }

            }


            if (eq[0].WorkShopCode == 4)
            {
                if ((xl[0].Weight_Prop.Contains("自"))&&(RecipeMatercodeType == "5"))
                    EquipName = "万向自动称量终炼1";
            }

        }
        else
        {
             if (RecipeMatercodeType == "5")
             {
                 EquipName = "称量（终炼）";
             }
             else
             {
                 EquipName = "称量（母炼）";
             }
             if (xl[0].Weight_Prop.Contains("自"))
             {
                 EquipName = "自动" + EquipName;
             }
             else
             {
                 EquipName = "手工" + EquipName;
             }      
        }
#endregion

        EquipName = "M" + eq[0].WorkShopCode+EquipName;
        WhereClip where = new WhereClip();
        where.And(BasEquip._.WorkShopCode == eq[0].WorkShopCode); 
        where.And(BasEquip._.EquipGroup == "小料");
        where.And(BasEquip._.EquipName.Like("%"+ EquipName+"%"));
        EntityArrayList<BasEquip> bes = basEquipManager.GetListByWhere(where);
        EntityArrayList<BasMaterial> lst = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == XiaoLiaoName);
        EntityArrayList<PmtRecipe> PRE = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == XiaoLiaoName);
        string materialcode = string.Empty;
        string result = string.Empty;
        #region 判断物料表中是否已存在此小料
        string RubCode= getrubcode(xl[0].Mater_code);
        #region 判断是否有不在同一车间的同一配方（玲珑定制）
        //    bool Isequipe = false;
        //    bool IsNotInSameworkshope = false;
        //    bool IsNewCreate = false;
        //    foreach (PmtRecipe br in PRE)
        //    {
        //        if (basEquipManager.GetListByWhere(BasEquip._.EquipCode == br.RecipeEquipCode)[0].WorkShopCode != eq[0].WorkShopCode)
        //            IsNotInSameworkshope = true;
        //        else
        //        {
        //            Isequipe = true;
        //            materialcode = br.RecipeMaterialCode;
        //        }
        //    }
        //    if (Isequipe)
        //        IsNewCreate = false;
        //    else if (!IsNewCreate && IsNotInSameworkshope)
        //        IsNewCreate = true;
        #endregion
        //if (lst.Count <= 0 || lst[0].MaterialCode.Substring(lst[0].MaterialCode.Length - 4, 4) != RubCode || IsNewCreate)

        while (lst.Count > 0)
        {
            if (lst[0].MaterialName != XiaoLiaoName)
            { lst.Remove(lst[0]); }
            else
            { break; }
        }


      if (lst.Count <= 0 || lst[0].MaterialCode.Substring(lst[0].MaterialCode.Length - 4, 4) != RubCode)
        {
         //   if (lst.Count > 0 && !IsNewCreate)
            if (lst.Count > 0 )
            {
                basMaterialManager.DeleteByWhere(BasMaterial._.MaterialCode == lst[0].MaterialCode);
            }
            string minorTypeID = string.Empty;
            if (xl[0].Weight_Prop.Contains("自"))
            {
                minorTypeID = "01";
            }
            else
            {
                minorTypeID = "02";
            }
            try
            {//22
                BasMaterial material = new BasMaterial();
                material.RubCode = getrubcode(xl[0].Mater_code);
                string nextRubInfoCode = basMaterialManager.GetNextMaterialCode("2", minorTypeID, material.RubCode, false);
                material.ObjID = Convert.ToInt32(basMaterialManager.GetNextMaterialObjID());
                material.MaterialCode = nextRubInfoCode;
                material.MajorTypeID = 2;
                material.MinorTypeID = minorTypeID;
                material.MaterialName = XiaoLiaoName;
                material.MaterialLevel = "0";
                //当别名为空时，储存物料名为别名；
                material.MaterialOtherName = XiaoLiaoName ;
                material.PlanPrice = 0;
                material.MinStock = 0;
                material.MaxStock = 0;
                material.UnitID = 0;
                material.StaticUnitID = 0;
                material.StaticUnitCoefficient = 0;
                material.CheckPermitError = 0;
                if (XiaoLiaoName.IndexOf("/")>-1)
                    material.MaxParkTime = 96;
                else
                    material.MaxParkTime = 72;

                material.ValidDate = material.MaxParkTime;//yzx180930

                material.MinParkTime = 0;
                material.IsEqualMaterial = "0";
                material.IsPutJar = "0";
                material.ProductMaterialCode = GetMatercode(xl[0].Mater_code);
                material.DeleteFlag = "0";
                material.OperSourceTemp = "PDM";
                basMaterialManager.Insert(material);
                //存留小料编号
                materialcode = material.MaterialCode;

            }
            catch (Exception ex)
            {
                msg.Alert("操作", "小料添加失败：" + ex);
                msg.Show();
            }
            ////////////插入小料 （原材料）

        }
        else
        {
            if (materialcode == "")
            {
                materialcode = lst[0].MaterialCode;
            }
        }
        NewMaterialcode = materialcode;
#endregion
        //判断是否存在此小料配方
        EntityArrayList<PmtRecipe> lpr = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == materialcode);
        #region 添加小料配方
      
            string equip = string.Empty;
            foreach (BasEquip be in bes)
            {
                PmtRecipe pr = new PmtRecipe();
                bool ishaving = false;
                for (int i = 0; i < lpr.Count; i++)
                {
                    if (lpr[i].RecipeEquipCode == be.EquipCode)
                    {
                        ishaving = true;
                        break;
                    }
                }
                if (ishaving)
                {
                    pr = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == materialcode && PmtRecipe._.RecipeEquipCode == be.EquipCode)[0];//&& PmtRecipe._.RecipeType == be.EquipCode
                     pr.RecipeState = "2";
                     result = pmtRecipeManager.UpdateRecipe(pr);
                 
                }

                    EntityArrayList<PmtRecipeWeight> pws = new EntityArrayList<PmtRecipeWeight>();

                    pr.RecipeMaterialCode = materialcode;  //物料编号
                    pr.RecipeName = IniRecipeName("1", Convert.ToInt32(xl[0].Edt_code)); //配方编号
                    ///插入basmaterial表中 小料信息
                    pr.RecipeEquipCode = be.EquipCode;  //机台名称
                    pr.RecipeType = 1;  //配方类型
                    pr.RecipeState = "1";  //配方状态
                    pr.IsUseAreaTemp = "0";
                    pr.CanAuditUser = hidden_AuditUser.Text.ToString();
                    pr.RecipeModifyUser = this.UserID;
                    int Version = GetMaxRecipeVersionID(pr);
                    if (Version > 0)
                    {
                        pr.RecipeVersionID = Version+1;
                    }
                    else
                    {
                        int? iNull = xl[0].Edt_code;  //版本号
                        if (iNull != null)
                        {
                            pr.RecipeVersionID = (int)iNull;
                        }
                    }
                    //----------------------------

                    pr.ShelfLotCount = 1;  //每架车数

                    int WeightID = 1;
                    foreach (TblWeight tw in xl)
                    {
                        PmtRecipeWeight pw = new PmtRecipeWeight();

                        if (!string.IsNullOrEmpty(tw.Packweight.ToString()))
                            pr.Packweight = tw.Packweight;
                     
                        pw.RecipeMaterialCode = materialcode;
                        pw.RecipeEquipCode = converter.ToString(be.EquipCode);
                        pw.MaterialCode = GetChild_Matercode(tw.Child_matercode, materialcode);
                        if (pw.MaterialCode.Length > 15)
                        {
                            return pw.MaterialCode;
                        }
                        pw.WeightID = WeightID;
                        WeightID++;
                        pw.SetWeight = decimal.Round(Convert.ToDecimal(tw.Set_Weight), 2, MidpointRounding.AwayFromZero);
                   
                        //yanzx 增加小料供应商
                        if (!String.IsNullOrEmpty(tw.Supply_code))
                        pw.Supply_code = tw.Supply_code;

                        ///////////////////////////////////////////////误差
                        //  m.ErrorAllow = converter.ToDecimal(record["ErrorAllow"]);
                        pw.WeightType = "9";
                        pw.ActCode = "0";
                        #region 如果药品误差大于10克则默认10克

                        if (tw.Error_Allow == 0)
                        {
                            pw.ErrorAllow = (decimal?)getXLErrorAllow(pw.MaterialCode, (double)pw.SetWeight);
                            if (xl[0].Weight_Prop.Contains("自") || xl[0].Weight_Prop.Contains("人"))//yzx 增加对人工药品的限定 18.4.9
                            {
                                decimal? er = (decimal?)0.02;
                                if (pw.ErrorAllow >= er)
                                {
                                    pw.ErrorAllow = pw.ErrorAllow / 2;
                                }
                            }
                        }
                        else pw.ErrorAllow = tw.Error_Allow;
                        #endregion
                        string pagetypename = "Page@";
                        if (pw.WeightType.ToLower().StartsWith(pagetypename.ToLower()))
                        {
                            pw.WeightType = pw.WeightType.Substring(pagetypename.Length).Trim();
                        }
                        pws.Add(pw);
                    }
                    EntityArrayList<PmtRecipeMixing> pms = new EntityArrayList<PmtRecipeMixing>();
                    EntityArrayList<PmtRecipeOpenMixing> recipeOpenMixing = new EntityArrayList<PmtRecipeOpenMixing>();
                    result = pmtRecipeManager.SavePmtRecipe(pr, pws, pms, recipeOpenMixing);
                    if (!String.IsNullOrEmpty(result))
                    {

                        return result;
                    }

      
        }
        #endregion
        return result;
    }
    #endregion

    #region 混炼信息
    /// <summary>
    /// Inis the PMT recipe mixing.
    /// 孙本强 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="arry">The arry.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniPmtRecipeMixing(TblRecipeManager.QueryParams queryParams, ref EntityArrayList<PmtRecipeMixing> lst)
    {
        EntityArrayList<TblMix> Mi = new EntityArrayList<TblMix>();

        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(queryParams.Mater_code))
        {
            where.And(TblMix._.Mater_code == queryParams.Mater_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Equip_code))
        {
            where.And(TblMix._.Equip_code == queryParams.Equip_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Recipe_type))
        {
            where.And(TblMix._.Recipe_type == queryParams.Recipe_type);
        }
        if (!String.IsNullOrEmpty(queryParams.Edt_code))
        {
            where.And(TblMix._.Edt_code == queryParams.Edt_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Routing_type))
        {
            where.And(TblMix._.Routing_type == queryParams.Routing_type);
        }


        where.And(TblMix._.OpenEquip_ID == "-1");
        try
        {
            Mi = tblMixManager.GetListByWhere(where);
        }
        catch
        { }

        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();

        EntityArrayList<TblMix> tms = new EntityArrayList<TblMix>();//根据混炼步骤排定混炼按顺序
        tms = Mi;

        for (int j = 1; j < tms.Count; j++)
        {
            if (string.IsNullOrWhiteSpace(tms[j].Mix_id.ToString()))
            {
                tms = Mi;
                break;
            }

            for (int i = 0; i < tms.Count - 1; i++)
            {
                if (Convert.ToInt32(tms[i].Mix_id) > Convert.ToInt32(tms[i + 1].Mix_id))
                {
                    TblMix tl = new TblMix();
                    tl = tms[i + 1];
                    tms[i + 1] = tms[i];
                    tms[i] = tl;
                }
            }
        }
        for (int i = 0; i < tms.Count; i++)
        {
            PmtRecipeMixing m = new PmtRecipeMixing();

            if (isplm == 0)
            {
                if (i == 0)
                {
                    m.TermCode = "";
                    m.MixingTime = 0;
                    m.MixingTemp = 0;
                    m.MixingEnergy = 0;
                    m.MixingPower = 0;
                }
                else
                {
                    if (tms[i - 1].Term_Code != 1)
                    {
                        m.TermCode = converter.ToString(tms[i - 1].Term_Code);
                    }
                    else
                    {
                        m.TermCode = "";
                    }
                    m.MixingTime = converter.ToInt(tms[i - 1].Set_time);
                    m.MixingTemp = converter.ToDecimal(tms[i - 1].Set_temp);
                    m.MixingEnergy = converter.ToDecimal(tms[i - 1].Set_ener);
                    m.MixingPower = converter.ToDecimal(tms[i - 1].Set_power);
                }
            }
            else
            {

                if (tms[i].Term_Code != 1)
                {
                    m.TermCode = converter.ToString(tms[i].Term_Code);
                }
                else
                {
                    m.TermCode = "";
                }
                m.MixingTime = converter.ToInt(tms[i].Set_time);
                m.MixingTemp = converter.ToDecimal(tms[i].Set_temp);
                m.MixingEnergy = converter.ToDecimal(tms[i].Set_ener);
                m.MixingPower = converter.ToDecimal(tms[i].Set_power);
            
            
            
            
            
            
            }
            m.RecipeMaterialCode = GetMatercode(tms[i].Mater_code);
            m.RecipeEquipCode = tms[i].Equip_code;
            m.ActionCode = converter.ToString(tms[i].Act_Code);
            if (m.ActionCode == "10")
            {
                m.MixingTime = converter.ToInt(tms[i].Set_time);
            }
            m.MixingStep = tms[i].Mix_id;
            //intNull = converter.ToInt(record["RecipeVersionID"]);
            //if (intNull != null)
            //{
            //    m.RecipeVersionID = (int)intNull;-------------版本号（缺）
            //}           

            m.MixingPress = converter.ToDecimal(tms[i].Set_Press);// * 10;//由于导入配方压力为兆帕，需转换为帕，需乘以10 //现已统一 不需要乘以10
          
            m.MixingSpeed = Convert.ToInt32(tms[i].Set_Rota);


            m.B_Version = tms[i].B_Version;
            m.R_Version = tms[i].R_Version;
            m.S_Factory = tms[i].S_Factory;
            //2016-09-30 李昊
            SysCodeManager basSysCodeManager = new SysCodeManager();
            EntityArrayList<SysCode> sc = basSysCodeManager.GetListByWhere(SysCode._.Remark == (tms[i].Recipe_type.ToString().Trim() + tms[i].Routing_type.ToString().Trim()));
            if (sc.Count > 0)
            {

                if (sc[0].Remark.Substring(0, 1) == tms[i].Recipe_type.ToString().Trim())
                    m.Recipe_type = converter.ToInt(sc[0].ItemCode);
                else
                { m.Recipe_type = converter.ToInt(sc[1].ItemCode); sc[0] = sc[1]; }


               
            }
            else
            {
                m.Recipe_type = 0;
            }
          
            m.RecipeVersionID = Convert.ToInt32(tms[i].Edt_code); 



            lst.Add(m);
        }

    }
    #endregion
    #region 混炼信息
    /// <summary>
    /// Inis the PMT recipe mixing.
    /// 孙本强 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="arry">The arry.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniPmtOpenRecipeMixing(TblRecipeManager.QueryParams queryParams, ref EntityArrayList<PmtRecipeOpenMixing> lst)
    {
        EntityArrayList<TblMix> Mi = new EntityArrayList<TblMix>();

        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(queryParams.Mater_code))
        {
            where.And(TblMix._.Mater_code == queryParams.Mater_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Equip_code))
        {
            where.And(TblMix._.Equip_code == queryParams.Equip_code);
        }
        if (!String.IsNullOrEmpty(queryParams.Recipe_type))
        {
            where.And(TblMix._.Recipe_type == queryParams.Recipe_type);
        }
        if (!String.IsNullOrEmpty(queryParams.Edt_code))
        {
            where.And(TblMix._.Edt_code == queryParams.Edt_code);
        }

        where.And(TblMix._.OpenEquip_ID != "-1");
        try
        {
            Mi = tblMixManager.GetListByWhere(where);
        }
        catch
        { }

        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();

        EntityArrayList<TblMix> tms = new EntityArrayList<TblMix>();//根据混炼步骤排定混炼按顺序
        tms = Mi;

        for (int j = 1; j < tms.Count; j++)
        {
            if (string.IsNullOrWhiteSpace(tms[j].Mix_id.ToString()))
            {
                tms = Mi;
                break;
            }

            for (int i = 0; i < tms.Count - 1; i++)
            {
                if (Convert.ToInt32(tms[i].Mix_id) > Convert.ToInt32(tms[i + 1].Mix_id))
                {
                    TblMix tl = new TblMix();
                    tl = tms[i + 1];
                    tms[i + 1] = tms[i];
                    tms[i] = tl;
                }
            }
        }
        for (int i = 0; i < tms.Count; i++)
        {
            PmtRecipeOpenMixing m = new PmtRecipeOpenMixing();
         
            m.RecipeMaterialCode = GetMatercode(tms[i].Mater_code);
            m.RecipeEquipCode = tms[i].Equip_code;
           
            m.MixingStep = tms[i].Mix_id;
          
            m.RecipeVersionID = Convert.ToInt32(tms[i].Edt_code);

            m.OpenMixingNo = tms[i].OpenEquip_ID;
            m.MixingStep = tms[i].Mix_id;
            m.OpenActionCode = (Convert.ToInt32(tms[i].Act_Code) - 50).ToString();
            m.MixTime = tms[i].Set_time;
            m.CoolMixSpeed = tms[i].CoolSpeed_Mix;
            m.OpenMixSpeed = tms[i].OpenMixSpeed_Mix;
            m.MixRollor = tms[i].Rollor_Mix;
            m.WaterTemp = tms[i].WatrTemp_Mix;
            m.RubberTemp = tms[i].RubTemp_Mix;
            m.CarSpeed = 1;
            m.SpeedDiff = tms[i].SpeedDiff_Mix;
            lst.Add(m);
        }

    }
    #endregion
    #region 删除导入配方列表中数据
    /// <summary>
    /// 删除导入配方列表中过时数据
    /// 于小鹏
    /// </summary>
    /// <param name="Values"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod]
    public string deleteRecipe(string Values)
    {
        string result = "您未选中任何配方!!";
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(Values);
        if (planDic.Length == 0)
        {
            return result;
        } 
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string Mater_Code = planRow["Mater_Code"];
            string EquipName = planRow["EquipName"];
            try
            {
                string equipecode = basEquipManager.GetListByWhere(BasEquip._.EquipName == EquipName)[0].EquipCode;
                tblRecipeManager.DeleteByWhere(TblRecipe._.Mater_code == Mater_Code && TblRecipe._.Equip_code == equipecode);
                tblWeightManager.DeleteByWhere(TblWeight._.Mater_code == Mater_Code && TblWeight._.Equip_code == equipecode);
                tblMixManager.DeleteByWhere(TblMix._.Mater_code == Mater_Code && TblMix._.Equip_code == equipecode);
            }
            catch
            { }
            
        }
        this.pageToolbar.DoRefresh();
        return result;
    }
    #endregion
    
    #region 导入配方
    /// <summary>
    /// 批量导入配方
    /// 于小鹏 @ 2013-05-09 17:09:54
    /// </summary>
    /// <returns></returns>
    [Ext.Net.DirectMethod]
    public string SaveSelectMutInfo(string Values)
    {
        string result = "您未选中任何配方！！";
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(Values);
        if (planDic.Length == 0)
        {
            return result;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string Mater_Code = planRow["Mater_Code"];
            string EquipName = planRow["EquipName"];
            string Edt_code = planRow["Edt_code"];
            string Recipe_type = planRow["Recipe_type"];
            string Routing_Type = planRow["Routing_type"];
            result = SaveJsonInfo(Mater_Code, EquipName, Edt_code, Recipe_type,Routing_Type);
            if (!string.IsNullOrWhiteSpace(result))
            {
                break;
            }
        }
        return result;

    }
    /// <summary>
    /// 重写方法单个保存配方方法
    /// </summary>
    /// <param name="Mater_Code"></param>
    /// <returns></returns>
    /// 
    public string SaveJsonInfo(string Mater_Code)
    {
        EntityArrayList<TblRecipe> tr = new EntityArrayList<TblRecipe>();
        string Result = string.Empty;
        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(Mater_Code))
        {
            where.And(TblRecipe._.Mater_code == Mater_Code);
        }
        where.And(TblRecipe._.Modify_Flag !="0");

        tr = tblRecipeManager.GetListByWhere(where);

        if (tr.Count > 0)
        {
            foreach (TblRecipe re in tr)
            {
                string Equipname = basEquipManager.GetListByWhere(BasEquip._.EquipCode == re.Equip_code)[0].EquipName;
               Result= SaveJsonInfo(Mater_Code, Equipname,re.Edt_code.ToString(),re.Recipe_type.ToString(),re.Routing_type.ToString());
            }
 
        }
        return Result;
    }
    /// <summary>
    /// 单个保存配方
    /// 于小鹏
    /// </summary>
    /// <param name="main">The main.</param>
    /// <param name="mixing">The mixing.</param>
    /// <param name="weight">The weight.</param>
    /// <returns></returns>
    /// <remarks></remarks>SaveJsonInfo(Mater_Code, EquipName, Edt_code, Recipe_type);
    [DirectMethod]
    public string SaveJsonInfo(string Mater_Code, string equipename, string Edt_code, string Recipe_type, string Routing_Type)
    {
        
        EntityArrayList<TblRecipe> tr = new EntityArrayList<TblRecipe>();
        string Result = string.Empty;
        WhereClip where = new WhereClip();
        if (!String.IsNullOrEmpty(Mater_Code))
        {
            where.And(TblRecipe._.Mater_code == Mater_Code);
        }
        if (!String.IsNullOrEmpty(equipename))
        {
            string equipecode = basEquipManager.GetListByWhere(BasEquip._.EquipName == equipename)[0].EquipCode;
            where.And(TblRecipe._.Equip_code == equipecode);
        }
        if (!String.IsNullOrEmpty(Edt_code))
        {
            where.And(TblRecipe._.Edt_code == Edt_code);
        }
        if (!String.IsNullOrEmpty(Recipe_type))
        {
            where.And(TblRecipe._.Recipe_type == Recipe_type);
        }
        if (!String.IsNullOrEmpty(Routing_Type))
        {
            where.And(TblRecipe._.Routing_type == Routing_Type);
        }
        tr = tblRecipeManager.GetListByWhere(where);

        if (tr.Count > 0)
        {


            foreach (TblRecipe re in tr)
            {
                #region 新增或修改配方
                if (re.Modify_Flag == "1")
                {
                    TblRecipeManager.QueryParams queryParams = new TblRecipeManager.QueryParams();
                    queryParams.Mater_code = re.Mater_code;
                    queryParams.Equip_code = re.Equip_code;
                    queryParams.Edt_code = re.Edt_code.ToString();
                    queryParams.Recipe_type = re.Recipe_type.ToString();
                    queryParams.Routing_type = re.Routing_type.ToString();
                    Result = string.Empty;

                    if (re.Mater_code == "E00000")
                    {
                       Result= updateRecycle(queryParams.Equip_code);
                    }
                    else
                    {
                        #region 基本信息
                        PmtRecipe recipe = new PmtRecipe();
                        IniPmtRecipe(queryParams, ref recipe);
                        #endregion
                        #region 称量信息
                        EntityArrayList<PmtRecipeWeight> recipeWeight = new EntityArrayList<PmtRecipeWeight>();
                        Result = IniPmtRecipeWeight(queryParams, ref recipeWeight);
                        if (!String.IsNullOrEmpty(Result))
                        {
                            break;
                        }
                        #endregion
                        #region 混炼信息
                        EntityArrayList<PmtRecipeMixing> recipeMixing = new EntityArrayList<PmtRecipeMixing>();
                        IniPmtRecipeMixing(queryParams, ref recipeMixing);
                        #endregion
                        recipe.RecipeModifyUser = this.UserID;
                        EntityArrayList<PmtRecipeOpenMixing> recipeOpenMixing = new EntityArrayList<PmtRecipeOpenMixing>();
                        IniPmtOpenRecipeMixing(queryParams, ref recipeOpenMixing);
                        Result = pmtRecipeManager.SavePmtRecipe(recipe, recipeWeight, recipeMixing, recipeOpenMixing);
                        if (!String.IsNullOrEmpty(Result))
                        {
                            break;
                        }
                        else
                        {
                            re.Modify_Flag = "0";
                            tblRecipeManager.Update(re);//返填导入标志
                        }
                    #region 导入反炼胶以及洗车胶
                    //    if (re.IsRecycle == "1")
                    //    {
                    //        Result = SaveRecycle(re.Mater_code, re.Equip_code, recipe.SAPVersionID);
                    //        if (!String.IsNullOrEmpty(Result))
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    else if (re.IsRecycle == "2")
                    //    {
                    //        Result = SaveClearRecipe(re.Mater_code, recipe, recipeWeight, recipeMixing);
                    //        if (!String.IsNullOrEmpty(Result))
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    else if (re.IsRecycle == "3")
                    //    {
                    //        Result = SaveRecycle(re.Mater_code, re.Equip_code, recipe.SAPVersionID);
                    //        if (!String.IsNullOrEmpty(Result))
                    //        {
                    //            break;
                    //        }
                    //        Result = SaveClearRecipe(re.Mater_code, recipe, recipeWeight, recipeMixing);
                    //        if (!String.IsNullOrEmpty(Result))
                    //        {
                    //            break;
                    //        }
                    //    }
                    #endregion
                    }
                }
                #endregion
                #region 作废配方
                else if (re.Modify_Flag == "5")
                {
                    EntityArrayList<PmtRecipe> listPR = new EntityArrayList<PmtRecipe>();
                    WhereClip whereR = new WhereClip();
                    if (!String.IsNullOrEmpty(re.Mater_code))
                    {
                        whereR.And(PmtRecipe._.RecipeMaterialCode == GetMatercode(re.Mater_code));
                    }
                    if (!String.IsNullOrEmpty(re.Equip_code))
                    {
                        whereR.And(PmtRecipe._.RecipeEquipCode == re.Equip_code);
                    }
                    if (!String.IsNullOrEmpty(re.Edt_code.ToString()))
                    {
                        whereR.And(PmtRecipe._.RecipeVersionID == re.Edt_code);
                    }
                    whereR.And(PmtRecipe._.RecipeState == "1");


                    listPR = pmtRecipeManager.GetListByWhere(whereR);
                    if (listPR.Count > 0)
                    {
                        PmtRecipe recipe = listPR[0];
                        recipe.RecipeState = "0";
                        Result = pmtRecipeManager.UpdateRecipe(recipe);
                        re.Modify_Flag = "0";
                        tblRecipeManager.Update(re);//返填导入标志
                    }
                    else
                    {
                        Result = "无此正用配方，所以不能作废！";
                    }
                     
                }
                #endregion
            }

        }
        else
        {
            Result = "此配方已导入!";
        }
        this.pageToolbar.DoRefresh();
        return Result;
    }

    /// <summary>
    /// Unescapes the specified ss.
    /// 孙本强 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string Unescape(string ss)
    {
        return System.Text.RegularExpressions.Regex.Unescape(ss);
    }

    #endregion

    #endregion

    #region 反炼胶
    #region 新增反炼胶配方
    /// <summary>
    /// 插入反炼胶标准配方
    /// 于小鹏
    /// </summary>
    /// <param name="matercode"></param>
    /// <param name="equipcode"></param>
    public string SaveRecycle(string matercode, string equipcode, string SAPVersionID)
    {
        string Result = string.Empty;
        //int? workshop = basEquipManager.GetListByWhere(BasEquip._.EquipCode == equipcode)[0].WorkShopCode;
        //string[] index = GetMaterName(matercode).Split('/');
        //string hsign = index[0].Substring(0, 1);
        //if (hsign == "C")
        //{
        //    hsign = index[0].Substring(1, 1);
        //}
        //string FinalMixing = "0";
        //string ProEnvironment = "0";
        //if (index.Length > 1)//判断是否是终炼机台（0是母炼）
        //{
        //    FinalMixing = "1";//取反值以判断不是终炼（有既是母炼又是终炼的情况）
        //}
        //if (hsign == "H")
        //{
        //    ProEnvironment = "1";
        //}
        //EntityArrayList<BasEquip> basequipS = new EntityArrayList<BasEquip>();
        //basequipS = basEquipManager.GetListByWhere(BasEquip._.IsFinalMixing != FinalMixing && BasEquip._.IsProEnvironment == ProEnvironment && BasEquip._.WorkShopCode == workshop);
        //foreach (BasEquip be in basequipS)
        //{

        int count = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == GetMatercode(matercode) && PmtRecipe._.RecipeEquipCode == equipcode && PmtRecipe._.RecipeType == "3").Count;
        if (count == 0)
        {
            try
            {
                #region 生成返炼配方主表
                Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();


                TblRecipe re = tblRecipeManager.GetListByWhere(TblRecipe._.Mater_code == "E00000" && TblRecipe._.Equip_code == equipcode)[0];
                PmtRecipe PR = new PmtRecipe();
                PR.RecipeMaterialCode = GetMatercode(matercode);  //物料名称
                PR.RecipeEquipCode = equipcode;  //机台号
                PR.RecipeType = 3;//标注配方类型为“返炼”
                PR.RecipeVersionID = Convert.ToInt32(re.Edt_code);
                PR.RecipeName = converter.ToString(IniRecipeName(PR.RecipeType.ToString(), Convert.ToInt32(PR.RecipeVersionID))); //配方编号
                PR.RecipeState = converter.ToString("1");  //配方状态
                PR.RecipeDefineDate = Convert.ToDateTime(re.Modify_time);//配方修改时间
                PR.LotTotalWeight = converter.ToDecimal(re.Set_weigh);  //配方总重
                PR.ShelfLotCount = converter.ToInt(re.Shelf_num);  //每架车数
                PR.OverTimeSetTime = converter.ToInt(re.OverTime_Time);  //超时排胶时间
                PR.OverTempSetTemp = converter.ToInt(re.OverTemp_Temp);  //紧急排胶温度
                PR.OverTempMinTime = converter.ToInt(re.OverTemp_MinTime);  //超温排胶最短时间
                PR.InPolyMaxTemp = converter.ToInt(re.Max_InPloyTemp);  //最高进胶温度
                PR.InPolyMinTemp = converter.ToInt(re.Min_InPloyTemp);  //最低进胶温度
                PR.MakeUpTemp = converter.ToInt(re.MakeUp_Temp);//补偿温度
                PR.CarbonRecycleType = converter.ToString(re.CB_RecycleType);  //炭黑是否回收
                PR.CarbonRecycleTime = converter.ToInt(re.CB_RecycleTime);  //炭黑回收时间
                //PR.AuditUser = "021635";//审核人
                //PR.AuditFlag = "1";
                if (string.IsNullOrEmpty(re.Is_UseAreaTemp))
                {
                    PR.IsUseAreaTemp = "0";
                }
                else
                {
                    PR.IsUseAreaTemp = converter.ToString(re.Is_UseAreaTemp);  //使用三区温度
                }
                PR.SideTemp = converter.ToInt(re.Side_Temp);  //侧壁温度
                PR.RollTemp = converter.ToInt(re.Roll_Temp);  //转子温度
                //m.RollTempDiff = converter.ToInt(record["RollTempDiff"]);  //转子温差
                PR.DdoorTemp = converter.ToInt(re.Ddoor_Temp);  //卸料门温度
                PR.CanAuditUser = hidden_AuditUser.Text.ToString();
                PR.OperCode = this.UserID + "+"; //用户名//加“+”的用意是区别导入配方
                PR.NewFlag = "0";

                PR.SAPVersionID = SAPVersionID;
                //    PR.AuditFlag = "1";//审核通过标志

                #endregion
                #region 生成反炼胶称量表
                EntityArrayList<PmtRecipeWeight> pws = new EntityArrayList<PmtRecipeWeight>();
                PmtRecipeWeight pw = new PmtRecipeWeight();
                pw.RecipeMaterialCode = GetMatercode(matercode);
                pw.RecipeEquipCode = equipcode;
                pw.MaterialCode = GetMatercode(matercode);
                pw.SetWeight = re.Set_weigh;
                pw.OldSetWeight = re.Set_weigh;
                pw.ErrorAllow = (decimal?)0.300;
                pw.WeightType = "2";
                pw.WeightID = 1;
                pw.ActCode = "0";
                pws.Add(pw);
                #endregion
                #region 生成返炼配方的混炼工艺
                EntityArrayList<TblMix> TXS = tblMixManager.GetListByWhere(TblMix._.Mater_code == "E00000" && TblMix._.Equip_code == equipcode);
                EntityArrayList<PmtRecipeMixing> PMS = new EntityArrayList<PmtRecipeMixing>();
                for (int i = 0; i < TXS.Count; i++)
                {
                    PmtRecipeMixing m = new PmtRecipeMixing();
                    if (i == 0)
                    {
                        m.TermCode = "";
                        m.MixingTime = 0;
                        m.MixingTemp = 0;
                        m.MixingEnergy = 0;
                        m.MixingPower = 0;
                    }
                    else
                    {
                        if (TXS[i - 1].Term_Code != 1)
                        {
                            m.TermCode = converter.ToString(TXS[i - 1].Term_Code);
                        }
                        else
                        {
                            m.TermCode = "";
                        }
                        m.MixingTime = converter.ToInt(TXS[i - 1].Set_time);
                        m.MixingTemp = converter.ToDecimal(TXS[i - 1].Set_temp);
                        m.MixingEnergy = converter.ToDecimal(TXS[i - 1].Set_ener);
                        m.MixingPower = converter.ToDecimal(TXS[i - 1].Set_power);
                    }
                    m.RecipeMaterialCode = GetMatercode(matercode);
                    m.RecipeEquipCode = equipcode;
                    m.ActionCode = converter.ToString(TXS[i].Act_Code);
                    if (m.ActionCode == "10")
                    {
                        m.MixingTime = converter.ToInt(TXS[i].Set_time);
                    }
                    m.MixingStep = TXS[i].Mix_id;
                    //intNull = converter.ToInt(record["RecipeVersionID"]);
                    //if (intNull != null)
                    //{
                    //    m.RecipeVersionID = (int)intNull;-------------版本号（缺）
                    //}           
                    m.MixingPress = converter.ToDecimal(TXS[i].Set_Press) * 10;//由于导入配方压力不标准，需乘以10
                    m.MixingSpeed = Convert.ToInt32(TXS[i].Set_Rota);
                    PMS.Add(m);
                }

                #endregion
                EntityArrayList<PmtRecipeOpenMixing> PROM = new EntityArrayList<PmtRecipeOpenMixing>();
                Result = pmtRecipeManager.SavePmtRecipe(PR, pws, PMS, PROM);
                if (!String.IsNullOrEmpty(Result))
                {
                    return Result;
                }
            }
            catch
            { }
        }

        return Result;
    }
    #endregion
    #region 更新对应返回胶配方
    /// <summary>
    /// 返炼胶标准变化之后更新对应返炼胶配方
    /// 于小鹏
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public string updateRecycle(string equipcode)
    {
       string Result = string.Empty;
       string matercode = string.Empty;

       EntityArrayList<PmtRecipe> PmtRs = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeEquipCode == equipcode && PmtRecipe._.RecipeType==3);
       if (PmtRs.Count > 0)
       {
           foreach (PmtRecipe pmtr in PmtRs)
           {
               matercode = pmtr.RecipeMaterialCode;
               try
               {
                   EntityArrayList<PmtRecipe> listPR = new EntityArrayList<PmtRecipe>();
                   listPR = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == matercode && PmtRecipe._.RecipeEquipCode == equipcode && PmtRecipe._.RecipeType == 3 && PmtRecipe._.RecipeState == "1");
                   if (listPR.Count > 0)
                   {
                       //PmtRecipe recipe = listPR[0];
                       //recipe.RecipeState = "2";
                       //Result = pmtRecipeManager.UpdateRecipe(recipe);
                       //if (!String.IsNullOrEmpty(Result))
                       //{
                       //    return Result;
                       //}
                   }
                   #region 生成返炼配方主表
                   Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();


                   TblRecipe re = tblRecipeManager.GetListByWhere(TblRecipe._.Mater_code == "E00000" && TblRecipe._.Equip_code == equipcode)[0];

                   PmtRecipe PR = new PmtRecipe();
                   PR.RecipeMaterialCode = matercode;  //物料名称
                   PR.RecipeEquipCode = equipcode;  //机台号
                   PR.RecipeType = 3;//标注配方类型为“返炼”
                   PR.RecipeVersionID = Convert.ToInt32(re.Edt_code);
                   PR.RecipeName = converter.ToString(IniRecipeName(PR.RecipeType.ToString(), Convert.ToInt32(PR.RecipeVersionID))); //配方编号
                   PR.RecipeState = converter.ToString("1");  //配方状态
                   PR.RecipeDefineDate = Convert.ToDateTime(re.Modify_time);//配方修改时间
                   PR.LotTotalWeight = converter.ToDecimal(re.Set_weigh);  //配方总重
                   PR.ShelfLotCount = converter.ToInt(re.Shelf_num);  //每架车数
                   PR.OverTimeSetTime = converter.ToInt(re.OverTime_Time);  //超时排胶时间
                   PR.OverTempSetTemp = converter.ToInt(re.OverTemp_Temp);  //紧急排胶温度
                   PR.OverTempMinTime = converter.ToInt(re.OverTemp_MinTime);  //超温排胶最短时间
                   PR.InPolyMaxTemp = converter.ToInt(re.Max_InPloyTemp);  //最高进胶温度
                   PR.InPolyMinTemp = converter.ToInt(re.Min_InPloyTemp);  //最低进胶温度
                   PR.MakeUpTemp = converter.ToInt(re.MakeUp_Temp);//补偿温度
                   PR.CarbonRecycleType = converter.ToString(re.CB_RecycleType);  //炭黑是否回收
                   PR.CarbonRecycleTime = converter.ToInt(re.CB_RecycleTime);  //炭黑回收时间
                   if (string.IsNullOrEmpty(re.Is_UseAreaTemp))
                   {
                       PR.IsUseAreaTemp = "0";
                   }
                   else
                   {
                       PR.IsUseAreaTemp = converter.ToString(re.Is_UseAreaTemp);  //使用三区温度
                   }
                   PR.SideTemp = converter.ToInt(re.Side_Temp);  //侧壁温度
                   PR.RollTemp = converter.ToInt(re.Roll_Temp);  //转子温度
                   //m.RollTempDiff = converter.ToInt(record["RollTempDiff"]);  //转子温差
                   PR.DdoorTemp = converter.ToInt(re.Ddoor_Temp);  //卸料门温度
                   PR.CanAuditUser = hidden_AuditUser.Text.ToString();
                   PR.OperCode = this.UserID + "+"; //用户名//加“+”的用意是区别导入配方
                   PR.NewFlag = "0";
                   //    PR.AuditFlag = "1";//审核通过标志
                   PR.RecipeModifyUser = this.UserID;
                   #endregion
                   #region 生成反炼胶称量表
                   EntityArrayList<PmtRecipeWeight> pws = new EntityArrayList<PmtRecipeWeight>();
                   PmtRecipeWeight pw = new PmtRecipeWeight();
                   pw.RecipeMaterialCode = matercode;
                   pw.RecipeEquipCode = equipcode;
                   pw.MaterialCode = matercode;
                   pw.SetWeight = re.Set_weigh;
                   pw.OldSetWeight = re.Set_weigh;
                   pw.ErrorAllow = (decimal?)0.300;
                   pw.WeightType = "2";
                   pw.WeightID = 1;
                   pw.ActCode = "0";
                   pws.Add(pw);
                   #endregion
                   #region 生成返炼配方的混炼工艺
                   EntityArrayList<TblMix> TXS = tblMixManager.GetListByWhere(TblMix._.Mater_code == "E00000" && TblMix._.Equip_code == equipcode);
                   EntityArrayList<PmtRecipeMixing> PMS = new EntityArrayList<PmtRecipeMixing>();
                   for (int i = 0; i < TXS.Count; i++)
                   {
                       PmtRecipeMixing m = new PmtRecipeMixing();
                       if (i == 0)
                       {
                           m.TermCode = "";
                           m.MixingTime = 0;
                           m.MixingTemp = 0;
                           m.MixingEnergy = 0;
                           m.MixingPower = 0;
                       }
                       else
                       {
                           if (TXS[i - 1].Term_Code != 1)
                           {
                               m.TermCode = converter.ToString(TXS[i - 1].Term_Code);
                           }
                           else
                           {
                               m.TermCode = "";
                           }
                           m.MixingTime = converter.ToInt(TXS[i - 1].Set_time);
                           m.MixingTemp = converter.ToDecimal(TXS[i - 1].Set_temp);
                           m.MixingEnergy = converter.ToDecimal(TXS[i - 1].Set_ener);
                           m.MixingPower = converter.ToDecimal(TXS[i - 1].Set_power);
                       }
                       m.RecipeMaterialCode = matercode;
                       m.RecipeEquipCode = equipcode;
                       m.ActionCode = converter.ToString(TXS[i].Act_Code);
                       if (m.ActionCode == "10")
                       {
                           m.MixingTime = converter.ToInt(TXS[i].Set_time);
                       }
                       m.MixingStep = TXS[i].Mix_id;
                       //intNull = converter.ToInt(record["RecipeVersionID"]);
                       //if (intNull != null)
                       //{
                       //    m.RecipeVersionID = (int)intNull;-------------版本号（缺）
                       //}           
                       m.MixingPress = converter.ToDecimal(TXS[i].Set_Press) * 10;//由于导入配方压力不标准，需乘以10
                       m.MixingSpeed = Convert.ToInt32(TXS[i].Set_Rota);
                       PMS.Add(m);
                   }

                   #endregion

                   EntityArrayList<PmtRecipeOpenMixing> PROM = new EntityArrayList<PmtRecipeOpenMixing>();
                   Result = pmtRecipeManager.SavePmtRecipe(PR, pws, PMS, PROM);
                   if (!String.IsNullOrEmpty(Result))
                   {
                       return Result;
                   }
                   else
                   {
                       re.Modify_Flag = "0";
                       tblRecipeManager.Update(re);//返填导入标志
                   }
               }
               catch
               { }
           }
       }
       else
       {
           Result = "此返炼胶标准没有对应返回胶配方！！";
       }
   
        return Result;
    }
#endregion
    #endregion

    #region 替换已有正用配方
    /// <summary>
    /// 替换已有正用配方
    /// 于小鹏
    /// </summary>
    /// <param name="Mater_Code"></param>
    /// <param name="equipcode"></param>
    /// <param name="RecipeType"></param>
    /// <returns></returns>
    [DirectMethod]
    public string invalidrecipe(string Mater_Code, string equipcode, string RecipeType, string R_Version)
    {
        string result = string.Empty;
        EntityArrayList<PmtRecipe> listPR = new EntityArrayList<PmtRecipe>();
        listPR = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == Mater_Code && PmtRecipe._.RecipeEquipCode == equipcode && PmtRecipe._.RecipeType == RecipeType && PmtRecipe._.RecipeState == "1");
        if (listPR.Count > 0)
        {
            PmtRecipe recipe = listPR[0];


             recipe.RecipeState = "2";
            result = pmtRecipeManager.UpdateRecipe(recipe);
        }
        else
        {
            result = "无此正用配方，所以不能作废！";
        }
        if (!string.IsNullOrWhiteSpace(result))
        {
            return result;
        }
        else
        {
            string equipname = basEquipManager.GetListByWhere(BasEquip._.EquipCode == equipcode)[0].EquipName;
            string matercode = string.Empty;
            //if (RecipeType == "4")
            //{
            //    matercode = "C" + GetErpcode(Mater_Code);
            //}
            //else
            //{
                matercode = GetErpcode(Mater_Code);
 //           }

            //2016-09-30 李昊
            //    result = SaveJsonInfo(matercode, equipname, RecipeType, R_Version);
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
        }
            return result;
    }
    #endregion

    #region 洗车胶
    /// <summary>
    /// 生成洗车胶配方
    /// </summary>
    /// <param name="matercode"></param>
    /// <param name="recipe"></param>
    /// <param name="recipeWeight"></param>
    /// <param name="recipeMixing"></param>
    /// <returns></returns>
    public string SaveClearRecipe(string matercode, PmtRecipe recipe, EntityArrayList<PmtRecipeWeight> recipeWeight, EntityArrayList<PmtRecipeMixing> recipeMixing)
    {
        string materialcode = GetMatercode(matercode, "1");
        string Result = string.Empty;
        EntityArrayList<PmtRecipe> pmS = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == materialcode && PmtRecipe._.RecipeEquipCode == recipe.RecipeEquipCode);
        if (pmS.Count == 0)
        {
            #region 生成洗车胶配方主表
            recipe.RecipeMaterialCode = materialcode;  //物料名称
            recipe.ObjID = 0;
            #endregion
            #region 生成洗车胶称量表
            foreach (PmtRecipeWeight rw in recipeWeight)
            {
                rw.RecipeMaterialCode = materialcode;
            }

            #endregion
            #region 生成洗车胶配方的混炼工艺
            foreach (PmtRecipeMixing rm in recipeMixing)
            {
                rm.RecipeMaterialCode = materialcode;
            }

            #endregion

            EntityArrayList<PmtRecipeOpenMixing> PROM = new EntityArrayList<PmtRecipeOpenMixing>();
            Result = pmtRecipeManager.SavePmtRecipe(recipe, recipeWeight, recipeMixing, PROM);
        }
        return Result;
    }
    #endregion


    //public string SaveRecycle(string matercode, string equipcode)
    //{
    //    string Result = string.Empty;
    //    int? workshop = basEquipManager.GetListByWhere(BasEquip._.EquipCode == equipcode)[0].WorkShopCode;
    //    string[] index = GetMaterName(matercode).Split('/');
    //    string hsign = index[0].Substring(0, 1);
    //    if (hsign == "C")
    //    {
    //        hsign = index[0].Substring(1, 1);
    //    }
    //    string FinalMixing = "0";
    //    string ProEnvironment = "0";
    //    if (index.Length > 1)//判断是否是终炼机台（0是母炼）
    //    {
    //        FinalMixing = "1";//取反值以判断不是终炼（有既是母炼又是终炼的情况）
    //    }
    //    if (hsign == "H")
    //    {
    //        ProEnvironment = "1";
    //    }
    //    EntityArrayList<BasEquip> basequipS = new EntityArrayList<BasEquip>();
    //    basequipS = basEquipManager.GetListByWhere(BasEquip._.IsFinalMixing != FinalMixing && BasEquip._.IsProEnvironment == ProEnvironment && BasEquip._.WorkShopCode == workshop);
    //    foreach (BasEquip be in basequipS)
    //    {
           
    //       int count= pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode==GetMatercode(matercode)&&PmtRecipe._.RecipeEquipCode==be.EquipCode&&PmtRecipe._.RecipeType=="3").Count;
    //       if (count == 0)
    //       {
    //           try
    //           {
    //               #region 生成返炼配方主表
    //               Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();


    //               TblRecipe re = tblRecipeManager.GetListByWhere(TblRecipe._.Mater_code == "E00000" && TblRecipe._.Equip_code == equipcode)[0];

    //               PmtRecipe PR = new PmtRecipe();
    //               PR.RecipeMaterialCode = GetMatercode(matercode);  //物料名称
    //               PR.RecipeEquipCode = be.EquipCode;  //机台号
    //               PR.RecipeType = 3;//标注配方类型为“返炼”
    //               PR.RecipeVersionID = Convert.ToInt32(re.Edt_code);
    //               PR.RecipeName = converter.ToString(IniRecipeName(PR.RecipeType.ToString(), PR.RecipeVersionID)); //配方编号
    //               PR.RecipeState = converter.ToString("1");  //配方状态
    //               PR.RecipeDefineDate = Convert.ToDateTime(re.Modify_time);//配方修改时间
    //               PR.LotTotalWeight = converter.ToDecimal(re.Set_weigh);  //配方总重
    //               PR.ShelfLotCount = converter.ToInt(re.Shelf_num);  //每架车数
    //               PR.OverTimeSetTime = converter.ToInt(re.OverTime_Time);  //超时排胶时间
    //               PR.OverTempSetTemp = converter.ToInt(re.OverTemp_Temp);  //紧急排胶温度
    //               PR.OverTempMinTime = converter.ToInt(re.OverTemp_MinTime);  //超温排胶最短时间
    //               PR.InPolyMaxTemp = converter.ToInt(re.Max_InPloyTemp);  //最高进胶温度
    //               PR.InPolyMinTemp = converter.ToInt(re.Min_InPloyTemp);  //最低进胶温度
    //               PR.MakeUpTemp = converter.ToInt(re.MakeUp_Temp);//补偿温度
    //               PR.CarbonRecycleType = converter.ToString(re.CB_RecycleType);  //炭黑是否回收
    //               PR.CarbonRecycleTime = converter.ToInt(re.CB_RecycleTime);  //炭黑回收时间
    //               if (string.IsNullOrEmpty(re.Is_UseAreaTemp))
    //               {
    //                   PR.IsUseAreaTemp = "0";
    //               }
    //               else
    //               {
    //                   PR.IsUseAreaTemp = converter.ToString(re.Is_UseAreaTemp);  //使用三区温度
    //               }
    //               PR.SideTemp = converter.ToInt(re.Side_Temp);  //侧壁温度
    //               PR.RollTemp = converter.ToInt(re.Roll_Temp);  //转子温度
    //               //m.RollTempDiff = converter.ToInt(record["RollTempDiff"]);  //转子温差
    //               PR.DdoorTemp = converter.ToInt(re.Ddoor_Temp);  //卸料门温度
    //               PR.CanAuditUser = hidden_AuditUser.Text.ToString();
    //               PR.OperCode = this.UserID + "+"; //用户名//加“+”的用意是区别导入配方
    //               PR.NewFlag = "0";
    //               //    PR.AuditFlag = "1";//审核通过标志

    //               #endregion
    //               #region 生成反炼胶称量表
    //               EntityArrayList<PmtRecipeWeight> pws = new EntityArrayList<PmtRecipeWeight>();
    //               PmtRecipeWeight pw = new PmtRecipeWeight();
    //               pw.RecipeMaterialCode = GetMatercode(matercode);
    //               pw.RecipeEquipCode = be.EquipCode;
    //               pw.MaterialCode = GetMatercode(matercode);
    //               pw.SetWeight = re.Set_weigh;
    //               pw.ErrorAllow = (decimal?)0.300;
    //               pw.WeightType = "2";
    //               pw.WeightID = 1;
    //               pw.ActCode = "0";
    //               pws.Add(pw);
    //               #endregion
    //               #region 生成返炼配方的混炼工艺
    //               EntityArrayList<TblMix> TXS = tblMixManager.GetListByWhere(TblMix._.Mater_code == "E00000" && TblMix._.Equip_code == equipcode);
    //               EntityArrayList<PmtRecipeMixing> PMS = new EntityArrayList<PmtRecipeMixing>();
    //               for (int i = 0; i < TXS.Count; i++)
    //               {
    //                   PmtRecipeMixing m = new PmtRecipeMixing();
    //                   if (i == 0)
    //                   {
    //                       m.TermCode = "";
    //                       m.MixingTime = 0;
    //                       m.MixingTemp = 0;
    //                       m.MixingEnergy = 0;
    //                       m.MixingPower = 0;
    //                   }
    //                   else
    //                   {
    //                       if (TXS[i - 1].Term_Code != 1)
    //                       {
    //                           m.TermCode = converter.ToString(TXS[i - 1].Term_Code);
    //                       }
    //                       else
    //                       {
    //                           m.TermCode = "";
    //                       }
    //                       m.MixingTime = converter.ToInt(TXS[i - 1].Set_time);
    //                       m.MixingTemp = converter.ToDecimal(TXS[i - 1].Set_temp);
    //                       m.MixingEnergy = converter.ToDecimal(TXS[i - 1].Set_ener);
    //                       m.MixingPower = converter.ToDecimal(TXS[i - 1].Set_power);
    //                   }
    //                   m.RecipeMaterialCode = GetMatercode(matercode);
    //                   m.RecipeEquipCode = be.EquipCode;
    //                   m.ActionCode = converter.ToString(TXS[i].Act_Code);
    //                   if (m.ActionCode == "10")
    //                   {
    //                       m.MixingTime = converter.ToInt(TXS[i].Set_time);
    //                   }
    //                   m.MixingStep = TXS[i].Mix_id;
    //                   //intNull = converter.ToInt(record["RecipeVersionID"]);
    //                   //if (intNull != null)
    //                   //{
    //                   //    m.RecipeVersionID = (int)intNull;-------------版本号（缺）
    //                   //}           
    //                   m.MixingPress = converter.ToDecimal(TXS[i].Set_Press) * 10;//由于导入配方压力不标准，需乘以10
    //                   m.MixingSpeed = Convert.ToInt32(TXS[i].Set_Rota);
    //                   PMS.Add(m);
    //               }

    //               #endregion
    //                Result = pmtRecipeManager.SavePmtRecipe(PR, pws, PMS);
    //               if (!String.IsNullOrEmpty(Result))
    //               {
    //                   return Result;
    //               }
    //           }
    //           catch
    //           { }
    //       }
    //    }
    //    return Result;
    //}
  
}