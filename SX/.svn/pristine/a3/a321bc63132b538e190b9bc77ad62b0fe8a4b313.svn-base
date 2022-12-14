using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NBear.Common.Design;

namespace Mesnac.EntityDesign
{




    public interface PpmRubberStorageDeal : Entity
    {
        [PrimaryKey]
        long DealId { get; }
        [SqlType("nvarchar(36)")]
        string StorageID { get; set; }
        [SqlType("nvarchar(36)")]
        string StoragePlaceID { get; set; }
        [SqlType("nvarchar(36)")]
        string BarCode { get; set; }
        [SqlType("varchar(30)")]
        string ShelfBarcode { get; set; }
        int? BarcodeStart { get; set; }
        int? BarcodeEnd { get; set; }
        int? ShelfNum { get; set; }
        DateTime? PlanDate { get; set; }
        [SqlType("char(1)")]
        string ShiftID { get; set; }
        [SqlType("char(1)")]
        string ShiftClassID { get; set; }
        [SqlType("nvarchar(36)")]
        string MaterCode { get; set; }
        [SqlType("nvarchar(36)")]
        string EquipCode { get; set; }
        DateTime? ValidDate { get; set; }
        decimal? ProductWeight { get; set; }
        decimal? ConsumeWeight { get; set; }
        decimal? RealWeight { get; set; }
        [SqlType("varchar(30)")]
        string LLBarCode { get; set; }
        int? RealNum { get; set; }
        [SqlType("nvarchar(36)")]
        string OperPerson { get; set; }
        DateTime DealDate { get; set; }
        [SqlType("nvarchar(36)")]
        string DealPerson { get; set; }
        [SqlType("nvarchar(50)")]
        string DealWay { get; set; }
        DateTime? DealValidDate { get; set; }
        [SqlType("nvarchar(36)")]
        string DealStorageID { get; set; }
        [SqlType("nvarchar(36)")]
        string DealStoragePlaceID { get; set; }
        [SqlType("nvarchar(500)")]
        string DealRemark { get; set; }
        [SqlType("nchar(1)")]
        string IsChecked { get; set; }
        [SqlType("nchar(1)")]
        string IsFeedBack { get; set; }
        DateTime? CheckDate { get; set; }
        [SqlType("nvarchar(50)")]
        string CheckPerson { get; set; }
    }






}