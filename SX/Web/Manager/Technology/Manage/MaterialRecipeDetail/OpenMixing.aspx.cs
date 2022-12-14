using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;

public partial class Manager_Technology_Manage_MaterialRecipeDetail_OpenMixing : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 袁洋 @ 2013-04-03 13:06:35
    /// </summary>
    private IPmtOpenActionModelDetailManager openActionModelDetailManager = new PmtOpenActionModelDetailManager();
    private IPmtOpenActionModelMainManager openActionModelMainManager = new PmtOpenActionModelMainManager();
    private IPmtRecipeOpenMixingManager recipeOpenMixingManager = new PmtRecipeOpenMixingManager();
    private IPmtOpenActionManager openActionManager = new PmtOpenActionManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            //去除复制的开炼信息
            PmtRecipeOpenMixingManager pm = new PmtRecipeOpenMixingManager();
            pm.GetBySql("delete from PmtRecipeOpenMixing where RecipeEquipCode = 'test'").ToDataSet();
            FillsetOpenActionCode();
        }
    }

    /// <summary>
    /// Gets the request.
    /// 袁洋 @ 2013-04-03 13:06:36
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// Fillsets the tem code.
    /// 袁洋 @ 2013-04-03 13:06:35
    /// </summary>
    /// <remarks></remarks>
    private void FillsetOpenActionCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtOpenAction._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtOpenAction._.SeqIdx.Asc;
        EntityArrayList<PmtOpenAction> lst = openActionManager.GetListByWhereAndOrder(where, order);
        setOpenActionCode0.Items.Clear();
        setOpenActionCode1.Items.Clear();
        setOpenActionCode2.Items.Clear();
        setOpenActionCode3.Items.Clear();
        setOpenActionCode4.Items.Clear();
        setOpenActionCode5.Items.Clear();
        setOpenActionCode6.Items.Clear();
        if (true)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = "　";
            item.Value = "　";
            setOpenActionCode0.Items.Add(item);
            setOpenActionCode1.Items.Add(item);
            setOpenActionCode2.Items.Add(item);
            setOpenActionCode3.Items.Add(item);
            setOpenActionCode4.Items.Add(item);
            setOpenActionCode5.Items.Add(item);
            setOpenActionCode6.Items.Add(item);
        }
        foreach (PmtOpenAction m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = m.ShowName;
            item.Value = m.ActionCode;
            setOpenActionCode0.Items.Add(item);
            setOpenActionCode1.Items.Add(item);
            setOpenActionCode2.Items.Add(item);
            setOpenActionCode3.Items.Add(item);
            setOpenActionCode4.Items.Add(item);
            setOpenActionCode5.Items.Add(item);
            setOpenActionCode6.Items.Add(item);
        }
    }
    #endregion

    #region 开炼信息
    /// <summary>
    /// Mixings the grid panel bind data.
    /// 袁洋 @ 2013-04-03 13:06:36
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object MixingGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        int total = 0;
        int modelcount = 0;
        int pagesize = 0;
        EntityArrayList<PmtRecipeOpenMixing> lst = new EntityArrayList<PmtRecipeOpenMixing>();
        EntityArrayList<PmtOpenActionModelDetail> detailLst = new EntityArrayList<PmtOpenActionModelDetail>();
        string MixingNo = extraParams["MixingNo"].ToString();
        string recipe = GetRequest("Recipe");
        string mainModelId = "";
        if (!string.IsNullOrWhiteSpace(txtRecipeObjID.Text))
        {
            recipe = txtRecipeObjID.Text;
        }
        if (!string.IsNullOrWhiteSpace(recipe))
        {
            lst = recipeOpenMixingManager.GetListByWhereAndOrder(
                PmtRecipeOpenMixing._.RecipeObjID == recipe 
                && PmtRecipeOpenMixing._.OpenMixingNo == MixingNo,
                PmtRecipeOpenMixing._.MixingStep.Asc);
        }
        #region 加载模板
        if (!string.IsNullOrWhiteSpace(txtOpenMainModelId.Text))
        {
            mainModelId = txtOpenMainModelId.Text;
        }
        if (!string.IsNullOrWhiteSpace(mainModelId))
        {
            detailLst = openActionModelDetailManager.GetListByWhereAndOrder(
                PmtOpenActionModelDetail._.MainModelID == mainModelId 
                && PmtOpenActionModelDetail._.OpenMixingNo == MixingNo,
                PmtOpenActionModelDetail._.MixingStep.Asc);
            modelcount = detailLst.Count;
            pagesize = 20;
            for (int i = pagesize; i > modelcount; i--)
            {
                PmtOpenActionModelDetail m = new PmtOpenActionModelDetail();
                m.MixingStep = i;
                detailLst.Add(m);
            }
            total = lst.Count;
            return new { data = detailLst, total = detailLst.Count };
        }

        #endregion

        #region 复制1号开炼机信息到2-6号
        PmtRecipeOpenMixingManager pm = new PmtRecipeOpenMixingManager();
        EntityArrayList<PmtRecipeOpenMixing> tempList = pm.GetListByWhereAndOrder(PmtRecipeOpenMixing._.RecipeEquipCode == "test", PmtRecipeOpenMixing._.MixingStep.Asc);
        if (tempList.Count > 0)
        {

            modelcount = tempList.Count;
            pagesize = 20;
            for (int i = pagesize; i > modelcount; i--)
            {
                PmtRecipeOpenMixing m = new PmtRecipeOpenMixing();
                m.MixingStep = i;
                tempList.Add(m);
            }
            foreach (PmtRecipeOpenMixing pro in tempList)
            { pro.OpenMixingNo = MixingNo;
            PmtRecipeOpenMixing np = new PmtRecipeOpenMixing();
            if (pro.OpenActionCode == "0") pro.OpenActionCode = np.OpenActionCode;
            
            }
            //if (MixingNo=="6")
            //pm.GetBySql("delete from PmtRecipeOpenMixing where RecipeEquipCode = 'test'").ToDataSet();

            total = tempList.Count;
            return new { data = tempList, total = tempList.Count };
        
        
        
        }
        
        
        #endregion
        modelcount = lst.Count;
        pagesize = 20;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtRecipeOpenMixing m = new PmtRecipeOpenMixing();
            m.MixingStep = i;
            lst.Add(m);
        }
        total = lst.Count;
        return new { data = lst, total = lst.Count };
    }



    [DirectMethod]
    public string DeleteJsonInfo()
    {
        PmtRecipeOpenMixingManager pm = new PmtRecipeOpenMixingManager();
        pm.GetBySql("delete from PmtRecipeOpenMixing where RecipeEquipCode = 'test'").ToDataSet();
        return "";
    }
    [DirectMethod]
    public string SaveJsonInfo(string main)
    {
    main = Unescape(main).Replace("　", "").Replace("<br>", "");
    JavaScriptArray arry = new JavaScriptArray();
    EntityArrayList<PmtRecipeOpenMixing> tempList = new EntityArrayList<PmtRecipeOpenMixing>();
    arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(main);
    IniPmtRecipeOpenMixing(arry, ref tempList);
    PmtRecipeOpenMixingManager pm = new PmtRecipeOpenMixingManager();
    foreach (PmtRecipeOpenMixing pro in tempList)
    {
        pro.RecipeEquipCode = "test";

        pm.Insert(pro);
    
    
    
    }



    //storeMinxing2.DataSource = new { data = tempList, total = tempList.Count }; ;
    //storeMinxing2.DataBind();
        return "";}
    private string Unescape(string ss)
    {
        return System.Text.RegularExpressions.Regex.Unescape(ss);
    }


    private void IniPmtRecipeOpenMixing(JavaScriptArray arry, ref  EntityArrayList<PmtRecipeOpenMixing> lst)
    {
        if (arry == null)
        {
            return;
        }

        int MixingStep = 1;
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();
        for (int i = 0; i < arry.Count; i++)
        {
            JavaScriptObject record = (JavaScriptObject)arry[i];
            PmtRecipeOpenMixing m = new PmtRecipeOpenMixing();
            m.OpenActionCode = converter.ToString(record["OpenActionCode"]);
            m.MixingStep = MixingStep++;
            int? intNull = converter.ToInt(record["RecipeObjID"]);
            if (intNull != null)
            {
                m.RecipeObjID = (int)intNull;
            }
            m.RecipeEquipCode = converter.ToString(record["RecipeEquipCode"]);
            m.RecipeMaterialCode = converter.ToString(record["RecipeMaterialCode"]);
            intNull = converter.ToInt(record["RecipeVersionID"]);
            if (intNull != null)
            {
                m.RecipeVersionID = (int)intNull;
            }
            m.OpenMixingNo = converter.ToString(record["OpenMixingNo"]);
            m.MixTime = converter.ToInt(record["MixTime"]);
            m.CoolMixSpeed = converter.ToDecimal(record["CoolMixSpeed"]);
            m.OpenMixSpeed = converter.ToDecimal(record["OpenMixSpeed"]);
            m.MixRollor = converter.ToDecimal(record["MixRollor"]);
            m.WaterTemp = converter.ToDecimal(record["WaterTemp"]);
            m.RubberTemp = converter.ToDecimal(record["RubberTemp"]);
            m.CarSpeed = converter.ToDecimal(record["CarSpeed"]);
            if (m.OpenMixingNo == "0")
                m.SpeedDiff = converter.ToDecimal(record["SpeedDiff"]);
            lst.Add(m);
        }
    }

    #endregion
}