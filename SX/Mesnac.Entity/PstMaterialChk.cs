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
    public partial class PstMaterialChkArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PstMaterialChk>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PstMaterialChk\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isB" +
        "atchUpdate=\"false\" isRelation=\"false\" mappingName=\"PstMaterialChk\" batchSize=\"10" +
        "\">\r\n  <Properties>\r\n    <Property name=\"BillNo\" type=\"System.String\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"BillNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey" +
        "=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"NoticeNo\" type=\"System.String\" " +
        "isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false" +
        "\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" i" +
        "sIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\"" +
        " mappingName=\"NoticeNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"FactoryID\" type=" +
        "\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompo" +
        "undUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLo" +
        "ad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"fa" +
        "lse\" isSerializationIgnore=\"false\" mappingName=\"FactoryID\" mappingColumnType=\"Sy" +
        "stem.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"fal" +
        "se\" />\r\n    <Property name=\"MakerPerson\" type=\"System.String\" isInherited=\"false" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Make" +
        "rPerson\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"" +
        "false\" isNotNull=\"false\" />\r\n    <Property name=\"InStockDate\" type=\"System.Nulla" +
        "ble`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"InStockDate\" mappingColumnType=\"System.Nul" +
        "lable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"fal" +
        "se\" />\r\n    <Property name=\"FiledFlag\" type=\"System.String\" isInherited=\"false\" " +
        "isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"FiledF" +
        "lag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" is" +
        "NotNull=\"false\" />\r\n    <Property name=\"SendChkFlag\" type=\"System.String\" isInhe" +
        "rited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\" isCompoundUnit=\"false\" is" +
        "Contained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelat" +
        "ionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializati" +
        "onIgnore=\"false\" mappingName=\"SendChkFlag\" mappingColumnType=\"System.String\" sql" +
        "Type=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"St" +
        "ockInFlag\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isRead" +
        "Only=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriend" +
        "Key=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isI" +
        "ndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StockInFlag\"" +
        " mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotN" +
        "ull=\"false\" />\r\n    <Property name=\"DeleteFlag\" type=\"System.String\" isInherited" +
        "=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isConta" +
        "ined=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKe" +
        "y=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgn" +
        "ore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\" sqlType=\"" +
        "char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" " +
        "type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fals" +
        "e\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" is" +
        "RelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerial" +
        "izationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sql" +
        "Type=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r" +
        "\n</EntityConfiguration>")]
    public partial class PstMaterialChk : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _PstMaterialChkEntityConfiguration;

        protected string _BillNo;

        protected string _NoticeNo;

        protected global::System.Int32? _FactoryID;

        protected string _MakerPerson;

        protected global::System.DateTime? _InStockDate;

        protected string _FiledFlag;

        protected string _SendChkFlag;

        protected string _StockInFlag;

        protected string _DeleteFlag;

        protected string _Remark;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.PstMaterialChk left, global::Mesnac.Entity.PstMaterialChk right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.PstMaterialChk left, global::Mesnac.Entity.PstMaterialChk right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



        public string BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                this.OnPropertyChanged("BillNo", this._BillNo, value);
                this._BillNo = value;
            }
        }

        public string NoticeNo
        {
            get
            {
                return this._NoticeNo;
            }
            set
            {
                this.OnPropertyChanged("NoticeNo", this._NoticeNo, value);
                this._NoticeNo = value;
            }
        }

        public global::System.Int32? FactoryID
        {
            get
            {
                return this._FactoryID;
            }
            set
            {
                this.OnPropertyChanged("FactoryID", this._FactoryID, value);
                this._FactoryID = value;
            }
        }

        public string MakerPerson
        {
            get
            {
                return this._MakerPerson;
            }
            set
            {
                this.OnPropertyChanged("MakerPerson", this._MakerPerson, value);
                this._MakerPerson = value;
            }
        }

        public global::System.DateTime? InStockDate
        {
            get
            {
                return this._InStockDate;
            }
            set
            {
                this.OnPropertyChanged("InStockDate", this._InStockDate, value);
                this._InStockDate = value;
            }
        }

        public string FiledFlag
        {
            get
            {
                return this._FiledFlag;
            }
            set
            {
                this.OnPropertyChanged("FiledFlag", this._FiledFlag, value);
                this._FiledFlag = value;
            }
        }

        public string SendChkFlag
        {
            get
            {
                return this._SendChkFlag;
            }
            set
            {
                this.OnPropertyChanged("SendChkFlag", this._SendChkFlag, value);
                this._SendChkFlag = value;
            }
        }

        public string StockInFlag
        {
            get
            {
                return this._StockInFlag;
            }
            set
            {
                this.OnPropertyChanged("StockInFlag", this._StockInFlag, value);
                this._StockInFlag = value;
            }
        }

        public string DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
            }
        }

        public override NBear.Common.EntityConfiguration GetEntityConfiguration()
        {
            if ((PstMaterialChk._PstMaterialChkEntityConfiguration == null))
            {
                PstMaterialChk._PstMaterialChkEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PstMaterialChk");
            }
            return PstMaterialChk._PstMaterialChkEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                    this._BillNo,
                    this._NoticeNo,
                    this._FactoryID,
                    this._MakerPerson,
                    this._InStockDate,
                    this._FiledFlag,
                    this._SendChkFlag,
                    this._StockInFlag,
                    this._DeleteFlag,
                    this._Remark};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._BillNo = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1)))
            {
                this._NoticeNo = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2)))
            {
                this._FactoryID = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3)))
            {
                this._MakerPerson = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._InStockDate = this.GetDateTime(reader, 4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._FiledFlag = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._SendChkFlag = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7)))
            {
                this._StockInFlag = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8)))
            {
                this._DeleteFlag = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9)))
            {
                this._Remark = reader.GetString(9);
            }
            this.ReloadQueries(false);
        }

        public override void SetPropertyValues(System.Data.DataRow row)
        {
            if ((false == row.IsNull(0)))
            {
                this._BillNo = ((string)(row[0]));
            }
            if ((false == row.IsNull(1)))
            {
                this._NoticeNo = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._FactoryID = ((System.Nullable<int>)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._MakerPerson = ((string)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._InStockDate = this.GetDateTime(row, 4);
            }
            if ((false == row.IsNull(5)))
            {
                this._FiledFlag = ((string)(row[5]));
            }
            if ((false == row.IsNull(6)))
            {
                this._SendChkFlag = ((string)(row[6]));
            }
            if ((false == row.IsNull(7)))
            {
                this._StockInFlag = ((string)(row[7]));
            }
            if ((false == row.IsNull(8)))
            {
                this._DeleteFlag = ((string)(row[8]));
            }
            if ((false == row.IsNull(9)))
            {
                this._Remark = ((string)(row[9]));
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
            if ((false == typeof(global::Mesnac.Entity.PstMaterialChk).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PstMaterialChk)(obj)).isAttached)
                        && (this.BillNo == ((global::Mesnac.Entity.PstMaterialChk)(obj)).BillNo));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _BillNo = new NBear.Common.PropertyItem("BillNo", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _NoticeNo = new NBear.Common.PropertyItem("NoticeNo", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _FactoryID = new NBear.Common.PropertyItem("FactoryID", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _MakerPerson = new NBear.Common.PropertyItem("MakerPerson", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _InStockDate = new NBear.Common.PropertyItem("InStockDate", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _FiledFlag = new NBear.Common.PropertyItem("FiledFlag", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _SendChkFlag = new NBear.Common.PropertyItem("SendChkFlag", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _StockInFlag = new NBear.Common.PropertyItem("StockInFlag", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.PstMaterialChk");

            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PstMaterialChk");

            public @__Columns()
            {
            }

            public @__Columns(string aliasName)
            {
                this.aliasName = aliasName;
            }

            public NBear.Common.PropertyItem BillNo
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _BillNo;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("BillNo", _BillNo.EntityConfiguration, _BillNo.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem NoticeNo
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _NoticeNo;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("NoticeNo", _NoticeNo.EntityConfiguration, _NoticeNo.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem FactoryID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _FactoryID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("FactoryID", _FactoryID.EntityConfiguration, _FactoryID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem MakerPerson
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _MakerPerson;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("MakerPerson", _MakerPerson.EntityConfiguration, _MakerPerson.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem InStockDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _InStockDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("InStockDate", _InStockDate.EntityConfiguration, _InStockDate.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem FiledFlag
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _FiledFlag;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("FiledFlag", _FiledFlag.EntityConfiguration, _FiledFlag.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SendChkFlag
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SendChkFlag;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SendChkFlag", _SendChkFlag.EntityConfiguration, _SendChkFlag.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StockInFlag
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StockInFlag;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StockInFlag", _StockInFlag.EntityConfiguration, _StockInFlag.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem DeleteFlag
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _DeleteFlag;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("DeleteFlag", _DeleteFlag.EntityConfiguration, _DeleteFlag.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem Remark
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Remark;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Remark", _Remark.EntityConfiguration, _Remark.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
