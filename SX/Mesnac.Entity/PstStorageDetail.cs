//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity
{
    using System;
    using System.Xml.Serialization;
    using NBear.Common;


    [System.SerializableAttribute()]
    public partial class PstStorageDetailArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PstStorageDetail>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PstStorageDetail\" isReadOnly=\"false\" isAutoPreLoad=\"false\" i" +
        "sBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PstStorageDetail\" batchSize" +
        "=\"10\">\r\n  <Properties>\r\n    <Property name=\"Barcode\" type=\"System.String\" isInhe" +
        "rited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"Barcode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(18)\" isPrim" +
        "aryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StorageID\" type=\"System.S" +
        "tring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained" +
        "=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"f" +
        "alse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=" +
        "\"false\" mappingName=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvarc" +
        "har(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StoragePla" +
        "ceID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"StoragePlaceID\" mappingColumnType=\"Syst" +
        "em.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <" +
        "Property name=\"OrderID\" type=\"System.Int32\" isInherited=\"false\" isReadOnly=\"fals" +
        "e\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false" +
        "\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProper" +
        "tyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"OrderID\" mappingColumn" +
        "Type=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <" +
        "Property name=\"StoreInOut\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"" +
        "false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"f" +
        "alse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPr" +
        "opertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StoreInOut\" mappin" +
        "gColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"fa" +
        "lse\" />\r\n    <Property name=\"RecordDate\" type=\"System.Nullable`1[System.DateTime" +
        "]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"RecordDate\" mappingColumnType=\"System.Nullable`1[System.DateTim" +
        "e]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"Num\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Num\" mappingColu" +
        "mnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNo" +
        "tNull=\"false\" />\r\n    <Property name=\"PieceWeight\" type=\"System.Nullable`1[Syste" +
        "m.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"PieceWeight\" mappingColumnType=\"System.Nullable`1[Syst" +
        "em.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <P" +
        "roperty name=\"Weight\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"fals" +
        "e\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Wei" +
        "ght\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isP" +
        "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"InaccountDuration\" t" +
        "ype=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false" +
        "\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isR" +
        "elationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeriali" +
        "zationIgnore=\"false\" mappingName=\"InaccountDuration\" mappingColumnType=\"System.S" +
        "tring\" sqlType=\"nvarchar(6)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"InaccountDate\" type=\"System.Nullable`1[System.DateTime]\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"InaccountDate\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sqlType" +
        "=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"BillT" +
        "ype\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=" +
        "\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fals" +
        "e\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isS" +
        "erializationIgnore=\"false\" mappingName=\"BillType\" mappingColumnType=\"System.Stri" +
        "ng\" sqlType=\"nvarchar(4)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Proper" +
        "ty name=\"SourceBillNo\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fals" +
        "e\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false" +
        "\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProper" +
        "tyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"SourceBillNo\" mappingC" +
        "olumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"SourceOrderID\" type=\"System.Nullable`1[System.In" +
        "t32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"SourceOrderID\" mappingColumnType=\"System.Nullable`1[System.I" +
        "nt32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property na" +
        "me=\"StorageType\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isC" +
        "ompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLa" +
        "zyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc" +
        "=\"false\" isSerializationIgnore=\"false\" mappingName=\"StorageType\" mappingColumnTy" +
        "pe=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n" +
        "    <Property name=\"ShiftClassID\" type=\"System.String\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ShiftClassI" +
        "D\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNo" +
        "tNull=\"false\" />\r\n    <Property name=\"ShiftID\" type=\"System.String\" isInherited=" +
        "\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"ShiftID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"fal" +
        "se\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PstStorageDetail : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _PstStorageDetailEntityConfiguration;

        protected string _Barcode;

        protected string _StorageID;

        protected string _StoragePlaceID;

        protected int _OrderID;

        protected string _StoreInOut;

        protected global::System.DateTime? _RecordDate;

        protected global::System.Int32? _Num;

        protected global::System.Decimal? _PieceWeight;

        protected global::System.Decimal? _Weight;

        protected string _InaccountDuration;

        protected global::System.DateTime? _InaccountDate;

        protected string _BillType;

        protected string _SourceBillNo;

        protected global::System.Int32? _SourceOrderID;

        protected string _StorageType;

        protected string _ShiftClassID;

        protected string _ShiftID;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.PstStorageDetail left, global::Mesnac.Entity.PstStorageDetail right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.PstStorageDetail left, global::Mesnac.Entity.PstStorageDetail right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



        public string Barcode
        {
            get
            {
                return this._Barcode;
            }
            set
            {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }

        public string StorageID
        {
            get
            {
                return this._StorageID;
            }
            set
            {
                this.OnPropertyChanged("StorageID", this._StorageID, value);
                this._StorageID = value;
            }
        }

        public string StoragePlaceID
        {
            get
            {
                return this._StoragePlaceID;
            }
            set
            {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
            }
        }

        public int OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                this.OnPropertyChanged("OrderID", this._OrderID, value);
                this._OrderID = value;
            }
        }

        public string StoreInOut
        {
            get
            {
                return this._StoreInOut;
            }
            set
            {
                this.OnPropertyChanged("StoreInOut", this._StoreInOut, value);
                this._StoreInOut = value;
            }
        }

        public global::System.DateTime? RecordDate
        {
            get
            {
                return this._RecordDate;
            }
            set
            {
                this.OnPropertyChanged("RecordDate", this._RecordDate, value);
                this._RecordDate = value;
            }
        }

        public global::System.Int32? Num
        {
            get
            {
                return this._Num;
            }
            set
            {
                this.OnPropertyChanged("Num", this._Num, value);
                this._Num = value;
            }
        }

        public global::System.Decimal? PieceWeight
        {
            get
            {
                return this._PieceWeight;
            }
            set
            {
                this.OnPropertyChanged("PieceWeight", this._PieceWeight, value);
                this._PieceWeight = value;
            }
        }

        public global::System.Decimal? Weight
        {
            get
            {
                return this._Weight;
            }
            set
            {
                this.OnPropertyChanged("Weight", this._Weight, value);
                this._Weight = value;
            }
        }

        public string InaccountDuration
        {
            get
            {
                return this._InaccountDuration;
            }
            set
            {
                this.OnPropertyChanged("InaccountDuration", this._InaccountDuration, value);
                this._InaccountDuration = value;
            }
        }

        public global::System.DateTime? InaccountDate
        {
            get
            {
                return this._InaccountDate;
            }
            set
            {
                this.OnPropertyChanged("InaccountDate", this._InaccountDate, value);
                this._InaccountDate = value;
            }
        }

        public string BillType
        {
            get
            {
                return this._BillType;
            }
            set
            {
                this.OnPropertyChanged("BillType", this._BillType, value);
                this._BillType = value;
            }
        }

        public string SourceBillNo
        {
            get
            {
                return this._SourceBillNo;
            }
            set
            {
                this.OnPropertyChanged("SourceBillNo", this._SourceBillNo, value);
                this._SourceBillNo = value;
            }
        }

        public global::System.Int32? SourceOrderID
        {
            get
            {
                return this._SourceOrderID;
            }
            set
            {
                this.OnPropertyChanged("SourceOrderID", this._SourceOrderID, value);
                this._SourceOrderID = value;
            }
        }

        public string StorageType
        {
            get
            {
                return this._StorageType;
            }
            set
            {
                this.OnPropertyChanged("StorageType", this._StorageType, value);
                this._StorageType = value;
            }
        }

        public string ShiftClassID
        {
            get
            {
                return this._ShiftClassID;
            }
            set
            {
                this.OnPropertyChanged("ShiftClassID", this._ShiftClassID, value);
                this._ShiftClassID = value;
            }
        }

        public string ShiftID
        {
            get
            {
                return this._ShiftID;
            }
            set
            {
                this.OnPropertyChanged("ShiftID", this._ShiftID, value);
                this._ShiftID = value;
            }
        }

        public override NBear.Common.EntityConfiguration GetEntityConfiguration()
        {
            if ((PstStorageDetail._PstStorageDetailEntityConfiguration == null))
            {
                PstStorageDetail._PstStorageDetailEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PstStorageDetail");
            }
            return PstStorageDetail._PstStorageDetailEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                    this._Barcode,
                    this._StorageID,
                    this._StoragePlaceID,
                    this._OrderID,
                    this._StoreInOut,
                    this._RecordDate,
                    this._Num,
                    this._PieceWeight,
                    this._Weight,
                    this._InaccountDuration,
                    this._InaccountDate,
                    this._BillType,
                    this._SourceBillNo,
                    this._SourceOrderID,
                    this._StorageType,
                    this._ShiftClassID,
                    this._ShiftID};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._Barcode = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1)))
            {
                this._StorageID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2)))
            {
                this._StoragePlaceID = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3)))
            {
                this._OrderID = reader.GetInt32(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._StoreInOut = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._RecordDate = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._Num = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7)))
            {
                this._PieceWeight = reader.GetDecimal(7);
            }
            if ((false == reader.IsDBNull(8)))
            {
                this._Weight = reader.GetDecimal(8);
            }
            if ((false == reader.IsDBNull(9)))
            {
                this._InaccountDuration = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10)))
            {
                this._InaccountDate = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11)))
            {
                this._BillType = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12)))
            {
                this._SourceBillNo = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13)))
            {
                this._SourceOrderID = reader.GetInt32(13);
            }
            if ((false == reader.IsDBNull(14)))
            {
                this._StorageType = reader.GetString(14);
            }
            if ((false == reader.IsDBNull(15)))
            {
                this._ShiftClassID = reader.GetString(15);
            }
            if ((false == reader.IsDBNull(16)))
            {
                this._ShiftID = reader.GetString(16);
            }
            this.ReloadQueries(false);
        }

        public override void SetPropertyValues(System.Data.DataRow row)
        {
            if ((false == row.IsNull(0)))
            {
                this._Barcode = ((string)(row[0]));
            }
            if ((false == row.IsNull(1)))
            {
                this._StorageID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._StoragePlaceID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._OrderID = ((int)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._StoreInOut = ((string)(row[4]));
            }
            if ((false == row.IsNull(5)))
            {
                this._RecordDate = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6)))
            {
                this._Num = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7)))
            {
                this._PieceWeight = ((System.Nullable<decimal>)(row[7]));
            }
            if ((false == row.IsNull(8)))
            {
                this._Weight = ((System.Nullable<decimal>)(row[8]));
            }
            if ((false == row.IsNull(9)))
            {
                this._InaccountDuration = ((string)(row[9]));
            }
            if ((false == row.IsNull(10)))
            {
                this._InaccountDate = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11)))
            {
                this._BillType = ((string)(row[11]));
            }
            if ((false == row.IsNull(12)))
            {
                this._SourceBillNo = ((string)(row[12]));
            }
            if ((false == row.IsNull(13)))
            {
                this._SourceOrderID = ((System.Nullable<int>)(row[13]));
            }
            if ((false == row.IsNull(14)))
            {
                this._StorageType = ((string)(row[14]));
            }
            if ((false == row.IsNull(15)))
            {
                this._ShiftClassID = ((string)(row[15]));
            }
            if ((false == row.IsNull(16)))
            {
                this._ShiftID = ((string)(row[16]));
            }
            this.ReloadQueries(false);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null))
            {
                return false;
            }
            if ((false == typeof(global::Mesnac.Entity.PstStorageDetail).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return (((((this.isAttached && ((global::Mesnac.Entity.PstStorageDetail)(obj)).isAttached)
                        && (this.Barcode == ((global::Mesnac.Entity.PstStorageDetail)(obj)).Barcode))
                        && (this.StorageID == ((global::Mesnac.Entity.PstStorageDetail)(obj)).StorageID))
                        && (this.StoragePlaceID == ((global::Mesnac.Entity.PstStorageDetail)(obj)).StoragePlaceID))
                        && (this.OrderID == ((global::Mesnac.Entity.PstStorageDetail)(obj)).OrderID));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _OrderID = new NBear.Common.PropertyItem("OrderID", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _StoreInOut = new NBear.Common.PropertyItem("StoreInOut", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _Num = new NBear.Common.PropertyItem("Num", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _PieceWeight = new NBear.Common.PropertyItem("PieceWeight", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _Weight = new NBear.Common.PropertyItem("Weight", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _InaccountDuration = new NBear.Common.PropertyItem("InaccountDuration", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _InaccountDate = new NBear.Common.PropertyItem("InaccountDate", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _BillType = new NBear.Common.PropertyItem("BillType", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _SourceBillNo = new NBear.Common.PropertyItem("SourceBillNo", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _SourceOrderID = new NBear.Common.PropertyItem("SourceOrderID", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _StorageType = new NBear.Common.PropertyItem("StorageType", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _ShiftClassID = new NBear.Common.PropertyItem("ShiftClassID", "Mesnac.Entity.PstStorageDetail");

            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.PstStorageDetail");

            public @__Columns()
            {
            }

            public @__Columns(string aliasName)
            {
                this.aliasName = aliasName;
            }

            public NBear.Common.PropertyItem Barcode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Barcode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StorageID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StorageID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StorageID", _StorageID.EntityConfiguration, _StorageID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StoragePlaceID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StoragePlaceID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StoragePlaceID", _StoragePlaceID.EntityConfiguration, _StoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem OrderID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _OrderID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("OrderID", _OrderID.EntityConfiguration, _OrderID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StoreInOut
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StoreInOut;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StoreInOut", _StoreInOut.EntityConfiguration, _StoreInOut.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem RecordDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _RecordDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("RecordDate", _RecordDate.EntityConfiguration, _RecordDate.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem Num
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Num;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Num", _Num.EntityConfiguration, _Num.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem PieceWeight
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _PieceWeight;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("PieceWeight", _PieceWeight.EntityConfiguration, _PieceWeight.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem Weight
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Weight;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Weight", _Weight.EntityConfiguration, _Weight.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem InaccountDuration
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _InaccountDuration;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("InaccountDuration", _InaccountDuration.EntityConfiguration, _InaccountDuration.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem InaccountDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _InaccountDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("InaccountDate", _InaccountDate.EntityConfiguration, _InaccountDate.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem BillType
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _BillType;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("BillType", _BillType.EntityConfiguration, _BillType.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SourceBillNo
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SourceBillNo;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SourceBillNo", _SourceBillNo.EntityConfiguration, _SourceBillNo.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SourceOrderID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SourceOrderID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SourceOrderID", _SourceOrderID.EntityConfiguration, _SourceOrderID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StorageType
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StorageType;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StorageType", _StorageType.EntityConfiguration, _StorageType.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ShiftClassID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ShiftClassID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ShiftClassID", _ShiftClassID.EntityConfiguration, _ShiftClassID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ShiftID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ShiftID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ShiftID", _ShiftID.EntityConfiguration, _ShiftID.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
