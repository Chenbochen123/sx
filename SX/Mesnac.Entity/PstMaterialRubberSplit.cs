//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.296
//
//     �Դ��ļ��ĸ��Ŀ��ܻᵼ�²���ȷ����Ϊ���������
//     �������ɴ��룬��Щ���Ľ��ᶪʧ��
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity
{
    using System;
    using System.Xml.Serialization;
    using NBear.Common;


    [System.SerializableAttribute()]
    public partial class PstMaterialRubberSplitArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PstMaterialRubberSplit>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.PstMaterialRubberSplit\" isReadOnly=\"false\" isAutoPreLoad=\"fa" +
        "lse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PstMaterialRubberSpli" +
        "t\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"BillNo\" type=\"System.Str" +
        "ing\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"" +
        "false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fal" +
        "se\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"f" +
        "alse\" mappingName=\"BillNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(3" +
        "6)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StorageID\" ty" +
        "pe=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\"" +
        " isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRe" +
        "lationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializ" +
        "ationIgnore=\"false\" mappingName=\"StorageID\" mappingColumnType=\"System.String\" sq" +
        "lType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property na" +
        "me=\"StoragePlaceID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" " +
        "isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" i" +
        "sLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyD" +
        "esc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StoragePlaceID\" mappingCo" +
        "lumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"" +
        "false\" />\r\n    <Property name=\"Barcode\" type=\"System.String\" isInherited=\"false\"" +
        " isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" i" +
        "sFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fal" +
        "se\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Barco" +
        "de\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\"" +
        " isNotNull=\"true\" />\r\n    <Property name=\"BarcodeSplit\" type=\"System.String\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"BarcodeSplit\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)" +
        "\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"MaterCode\" type=\"" +
        "System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isC" +
        "ontained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelati" +
        "onKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializatio" +
        "nIgnore=\"false\" mappingName=\"MaterCode\" mappingColumnType=\"System.String\" sqlTyp" +
        "e=\"nvarchar(18)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"" +
        "Weight\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=" +
        "\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Weight\" mappingCo" +
        "lumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"fal" +
        "se\" isNotNull=\"false\" />\r\n    <Property name=\"PlanDate\" type=\"System.Nullable`1[" +
        "System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" " +
        "isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRel" +
        "ationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializa" +
        "tionIgnore=\"false\" mappingName=\"PlanDate\" mappingColumnType=\"System.Nullable`1[S" +
        "ystem.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n " +
        "   <Property name=\"ShiftID\" type=\"System.String\" isInherited=\"false\" isReadOnly=" +
        "\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ShiftID\" mappingC" +
        "olumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"fals" +
        "e\" />\r\n    <Property name=\"ShiftClassID\" type=\"System.String\" isInherited=\"false" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Shif" +
        "tClassID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"fals" +
        "e\" isNotNull=\"false\" />\r\n    <Property name=\"OperTime\" type=\"System.Nullable`1[S" +
        "ystem.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" i" +
        "sContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRela" +
        "tionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializat" +
        "ionIgnore=\"false\" mappingName=\"OperTime\" mappingColumnType=\"System.Nullable`1[Sy" +
        "stem.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
        "  <Property name=\"OperPerson\" type=\"System.String\" isInherited=\"false\" isReadOnl" +
        "y=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey" +
        "=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInde" +
        "xPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"OperPerson\" map" +
        "pingColumnType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNot" +
        "Null=\"false\" />\r\n    <Property name=\"PrintTime\" type=\"System.String\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"PrintTime\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(1000)\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"PrintPerson\" type=\"Syste" +
        "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"PrintPerson\" mappingColumnType=\"System.String\" sqlType=\"" +
        "nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Use" +
        "dWeight\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"UsedWeight\" mapp" +
        "ingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey" +
        "=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"EquipName\" type=\"System.Strin" +
        "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"EquipName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(" +
        "50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfigu" +
        "ration>")]
    public partial class PstMaterialRubberSplit : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _PstMaterialRubberSplitEntityConfiguration;

        protected string _BillNo;

        protected string _StorageID;

        protected string _StoragePlaceID;

        protected string _Barcode;

        protected string _BarcodeSplit;

        protected string _MaterCode;

        protected global::System.Decimal? _Weight;

        protected global::System.DateTime? _PlanDate;

        protected string _ShiftID;

        protected string _ShiftClassID;

        protected global::System.DateTime? _OperTime;

        protected string _OperPerson;

        protected string _PrintTime;

        protected string _PrintPerson;

        protected global::System.Decimal? _UsedWeight;

        protected string _EquipName;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.PstMaterialRubberSplit left, global::Mesnac.Entity.PstMaterialRubberSplit right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.PstMaterialRubberSplit left, global::Mesnac.Entity.PstMaterialRubberSplit right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



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

        public string BarcodeSplit
        {
            get
            {
                return this._BarcodeSplit;
            }
            set
            {
                this.OnPropertyChanged("BarcodeSplit", this._BarcodeSplit, value);
                this._BarcodeSplit = value;
            }
        }

        public string MaterCode
        {
            get
            {
                return this._MaterCode;
            }
            set
            {
                this.OnPropertyChanged("MaterCode", this._MaterCode, value);
                this._MaterCode = value;
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

        public global::System.DateTime? PlanDate
        {
            get
            {
                return this._PlanDate;
            }
            set
            {
                this.OnPropertyChanged("PlanDate", this._PlanDate, value);
                this._PlanDate = value;
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

        public global::System.DateTime? OperTime
        {
            get
            {
                return this._OperTime;
            }
            set
            {
                this.OnPropertyChanged("OperTime", this._OperTime, value);
                this._OperTime = value;
            }
        }

        public string OperPerson
        {
            get
            {
                return this._OperPerson;
            }
            set
            {
                this.OnPropertyChanged("OperPerson", this._OperPerson, value);
                this._OperPerson = value;
            }
        }

        public string PrintTime
        {
            get
            {
                return this._PrintTime;
            }
            set
            {
                this.OnPropertyChanged("PrintTime", this._PrintTime, value);
                this._PrintTime = value;
            }
        }

        public string PrintPerson
        {
            get
            {
                return this._PrintPerson;
            }
            set
            {
                this.OnPropertyChanged("PrintPerson", this._PrintPerson, value);
                this._PrintPerson = value;
            }
        }

        public global::System.Decimal? UsedWeight
        {
            get
            {
                return this._UsedWeight;
            }
            set
            {
                this.OnPropertyChanged("UsedWeight", this._UsedWeight, value);
                this._UsedWeight = value;
            }
        }

        public string EquipName
        {
            get
            {
                return this._EquipName;
            }
            set
            {
                this.OnPropertyChanged("EquipName", this._EquipName, value);
                this._EquipName = value;
            }
        }

        public override NBear.Common.EntityConfiguration GetEntityConfiguration()
        {
            if ((PstMaterialRubberSplit._PstMaterialRubberSplitEntityConfiguration == null))
            {
                PstMaterialRubberSplit._PstMaterialRubberSplitEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PstMaterialRubberSplit");
            }
            return PstMaterialRubberSplit._PstMaterialRubberSplitEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                    this._BillNo,
                    this._StorageID,
                    this._StoragePlaceID,
                    this._Barcode,
                    this._BarcodeSplit,
                    this._MaterCode,
                    this._Weight,
                    this._PlanDate,
                    this._ShiftID,
                    this._ShiftClassID,
                    this._OperTime,
                    this._OperPerson,
                    this._PrintTime,
                    this._PrintPerson,
                    this._UsedWeight,
                    this._EquipName};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._BillNo = reader.GetString(0);
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
                this._Barcode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._BarcodeSplit = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._MaterCode = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._Weight = reader.GetDecimal(6);
            }
            if ((false == reader.IsDBNull(7)))
            {
                this._PlanDate = this.GetDateTime(reader, 7);
            }
            if ((false == reader.IsDBNull(8)))
            {
                this._ShiftID = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9)))
            {
                this._ShiftClassID = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10)))
            {
                this._OperTime = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11)))
            {
                this._OperPerson = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12)))
            {
                this._PrintTime = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13)))
            {
                this._PrintPerson = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14)))
            {
                this._UsedWeight = reader.GetDecimal(14);
            }
            if ((false == reader.IsDBNull(15)))
            {
                this._EquipName = reader.GetString(15);
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
                this._StorageID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._StoragePlaceID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._Barcode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._BarcodeSplit = ((string)(row[4]));
            }
            if ((false == row.IsNull(5)))
            {
                this._MaterCode = ((string)(row[5]));
            }
            if ((false == row.IsNull(6)))
            {
                this._Weight = ((System.Nullable<decimal>)(row[6]));
            }
            if ((false == row.IsNull(7)))
            {
                this._PlanDate = this.GetDateTime(row, 7);
            }
            if ((false == row.IsNull(8)))
            {
                this._ShiftID = ((string)(row[8]));
            }
            if ((false == row.IsNull(9)))
            {
                this._ShiftClassID = ((string)(row[9]));
            }
            if ((false == row.IsNull(10)))
            {
                this._OperTime = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11)))
            {
                this._OperPerson = ((string)(row[11]));
            }
            if ((false == row.IsNull(12)))
            {
                this._PrintTime = ((string)(row[12]));
            }
            if ((false == row.IsNull(13)))
            {
                this._PrintPerson = ((string)(row[13]));
            }
            if ((false == row.IsNull(14)))
            {
                this._UsedWeight = ((System.Nullable<decimal>)(row[14]));
            }
            if ((false == row.IsNull(15)))
            {
                this._EquipName = ((string)(row[15]));
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
            if ((false == typeof(global::Mesnac.Entity.PstMaterialRubberSplit).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return (((this.isAttached && ((global::Mesnac.Entity.PstMaterialRubberSplit)(obj)).isAttached)
                        && (this.Barcode == ((global::Mesnac.Entity.PstMaterialRubberSplit)(obj)).Barcode))
                        && (this.BarcodeSplit == ((global::Mesnac.Entity.PstMaterialRubberSplit)(obj)).BarcodeSplit));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _BillNo = new NBear.Common.PropertyItem("BillNo", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _BarcodeSplit = new NBear.Common.PropertyItem("BarcodeSplit", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _Weight = new NBear.Common.PropertyItem("Weight", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _PlanDate = new NBear.Common.PropertyItem("PlanDate", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _ShiftClassID = new NBear.Common.PropertyItem("ShiftClassID", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _OperTime = new NBear.Common.PropertyItem("OperTime", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _OperPerson = new NBear.Common.PropertyItem("OperPerson", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _PrintTime = new NBear.Common.PropertyItem("PrintTime", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _PrintPerson = new NBear.Common.PropertyItem("PrintPerson", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _UsedWeight = new NBear.Common.PropertyItem("UsedWeight", "Mesnac.Entity.PstMaterialRubberSplit");

            protected NBear.Common.PropertyItem _EquipName = new NBear.Common.PropertyItem("EquipName", "Mesnac.Entity.PstMaterialRubberSplit");

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

            public NBear.Common.PropertyItem BarcodeSplit
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _BarcodeSplit;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("BarcodeSplit", _BarcodeSplit.EntityConfiguration, _BarcodeSplit.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem MaterCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _MaterCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("MaterCode", _MaterCode.EntityConfiguration, _MaterCode.PropertyConfiguration, aliasName);
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

            public NBear.Common.PropertyItem PlanDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _PlanDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("PlanDate", _PlanDate.EntityConfiguration, _PlanDate.PropertyConfiguration, aliasName);
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

            public NBear.Common.PropertyItem OperTime
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _OperTime;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("OperTime", _OperTime.EntityConfiguration, _OperTime.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem OperPerson
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _OperPerson;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("OperPerson", _OperPerson.EntityConfiguration, _OperPerson.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem PrintTime
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _PrintTime;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("PrintTime", _PrintTime.EntityConfiguration, _PrintTime.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem PrintPerson
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _PrintPerson;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("PrintPerson", _PrintPerson.EntityConfiguration, _PrintPerson.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem UsedWeight
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _UsedWeight;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("UsedWeight", _UsedWeight.EntityConfiguration, _UsedWeight.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem EquipName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _EquipName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("EquipName", _EquipName.EntityConfiguration, _EquipName.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}