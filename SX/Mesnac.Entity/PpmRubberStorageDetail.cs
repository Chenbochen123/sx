//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity {
    using System;
    using System.Xml.Serialization;
    using NBear.Common;
    
    
    [System.SerializableAttribute()]
    public partial class PpmRubberStorageDetailArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmRubberStorageDetail> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmRubberStorageDetail\" isReadOnly=\"false\" isAutoPreLoad=\"fa" +
        "lse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmRubberStorageDetai" +
        "l\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"StorageID\" type=\"System." +
        "String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContaine" +
        "d=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"" +
        "false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore" +
        "=\"false\" mappingName=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvar" +
        "char(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StoragePl" +
        "aceID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"StoragePlaceID\" mappingColumnType=\"Sys" +
        "tem.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    " +
        "<Property name=\"Barcode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fa" +
        "lse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fal" +
        "se\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProp" +
        "ertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Barcode\" mappingColu" +
        "mnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"tru" +
        "e\" />\r\n    <Property name=\"ShelfBarcode\" type=\"System.String\" isInherited=\"false" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Shel" +
        "fBarcode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=" +
        "\"false\" isNotNull=\"false\" />\r\n    <Property name=\"OrderID\" type=\"System.Int32\" i" +
        "sInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\"" +
        " isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" is" +
        "IndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" " +
        "mappingName=\"OrderID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKe" +
        "y=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"RubberType\" type=\"System.Strin" +
        "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"RubberType\" mappingColumnType=\"System.String\" sqlType=\"char(1)\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"OperType\" type=\"" +
        "System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isC" +
        "ontained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelati" +
        "onKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializatio" +
        "nIgnore=\"false\" mappingName=\"OperType\" mappingColumnType=\"System.String\" sqlType" +
        "=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"P" +
        "lanDate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnl" +
        "y=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey" +
        "=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInde" +
        "xPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"PlanDate\" mappi" +
        "ngColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKe" +
        "y=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ShiftID\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"ShiftID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isP" +
        "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ShiftClassID\" type=\"" +
        "System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isC" +
        "ontained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelati" +
        "onKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializatio" +
        "nIgnore=\"false\" mappingName=\"ShiftClassID\" mappingColumnType=\"System.String\" sql" +
        "Type=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Eq" +
        "uipCode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"EquipCode\" mappingColumnType=\"System" +
        ".String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <" +
        "Property name=\"MaterCode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"f" +
        "alse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fa" +
        "lse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPro" +
        "pertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"MaterCode\" mappingC" +
        "olumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"Weight\" type=\"System.Nullable`1[System.Decimal]\"" +
        " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
        "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
        "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
        "\" mappingName=\"Weight\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sql" +
        "Type=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"To" +
        "StorageID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoun" +
        "dUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad" +
        "=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"fals" +
        "e\" isSerializationIgnore=\"false\" mappingName=\"ToStorageID\" mappingColumnType=\"Sy" +
        "stem.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n " +
        "   <Property name=\"ToStoragePlaceID\" type=\"System.String\" isInherited=\"false\" is" +
        "ReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFr" +
        "iendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\"" +
        " isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ToStorag" +
        "ePlaceID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=" +
        "\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RecordDate\" type=\"System.Nulla" +
        "ble`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"RecordDate\" mappingColumnType=\"System.Null" +
        "able`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"fals" +
        "e\" />\r\n    <Property name=\"OperPerson\" type=\"System.String\" isInherited=\"false\" " +
        "isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"OperPe" +
        "rson\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"fal" +
        "se\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PpmRubberStorageDetail : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PpmRubberStorageDetailEntityConfiguration;
        
        protected string _StorageID;
        
        protected string _StoragePlaceID;
        
        protected string _Barcode;
        
        protected string _ShelfBarcode;
        
        protected int _OrderID;
        
        protected string _RubberType;
        
        protected string _OperType;
        
        protected global::System.DateTime? _PlanDate;
        
        protected string _ShiftID;
        
        protected string _ShiftClassID;
        
        protected string _EquipCode;
        
        protected string _MaterCode;
        
        protected global::System.Decimal? _Weight;
        
        protected string _ToStorageID;
        
        protected string _ToStoragePlaceID;
        
        protected global::System.DateTime? _RecordDate;
        
        protected string _OperPerson;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PpmRubberStorageDetail left, global::Mesnac.Entity.PpmRubberStorageDetail right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PpmRubberStorageDetail left, global::Mesnac.Entity.PpmRubberStorageDetail right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string StorageID {
            get {
                return this._StorageID;
            }
            set {
                this.OnPropertyChanged("StorageID", this._StorageID, value);
                this._StorageID = value;
            }
        }
        
        public string StoragePlaceID {
            get {
                return this._StoragePlaceID;
            }
            set {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
            }
        }
        
        public string Barcode {
            get {
                return this._Barcode;
            }
            set {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }
        
        public string ShelfBarcode {
            get {
                return this._ShelfBarcode;
            }
            set {
                this.OnPropertyChanged("ShelfBarcode", this._ShelfBarcode, value);
                this._ShelfBarcode = value;
            }
        }
        
        public int OrderID {
            get {
                return this._OrderID;
            }
            set {
                this.OnPropertyChanged("OrderID", this._OrderID, value);
                this._OrderID = value;
            }
        }
        
        public string RubberType {
            get {
                return this._RubberType;
            }
            set {
                this.OnPropertyChanged("RubberType", this._RubberType, value);
                this._RubberType = value;
            }
        }
        
        public string OperType {
            get {
                return this._OperType;
            }
            set {
                this.OnPropertyChanged("OperType", this._OperType, value);
                this._OperType = value;
            }
        }
        
        public global::System.DateTime? PlanDate {
            get {
                return this._PlanDate;
            }
            set {
                this.OnPropertyChanged("PlanDate", this._PlanDate, value);
                this._PlanDate = value;
            }
        }
        
        public string ShiftID {
            get {
                return this._ShiftID;
            }
            set {
                this.OnPropertyChanged("ShiftID", this._ShiftID, value);
                this._ShiftID = value;
            }
        }
        
        public string ShiftClassID {
            get {
                return this._ShiftClassID;
            }
            set {
                this.OnPropertyChanged("ShiftClassID", this._ShiftClassID, value);
                this._ShiftClassID = value;
            }
        }
        
        public string EquipCode {
            get {
                return this._EquipCode;
            }
            set {
                this.OnPropertyChanged("EquipCode", this._EquipCode, value);
                this._EquipCode = value;
            }
        }
        
        public string MaterCode {
            get {
                return this._MaterCode;
            }
            set {
                this.OnPropertyChanged("MaterCode", this._MaterCode, value);
                this._MaterCode = value;
            }
        }
        
        public global::System.Decimal? Weight {
            get {
                return this._Weight;
            }
            set {
                this.OnPropertyChanged("Weight", this._Weight, value);
                this._Weight = value;
            }
        }
        
        public string ToStorageID {
            get {
                return this._ToStorageID;
            }
            set {
                this.OnPropertyChanged("ToStorageID", this._ToStorageID, value);
                this._ToStorageID = value;
            }
        }
        
        public string ToStoragePlaceID {
            get {
                return this._ToStoragePlaceID;
            }
            set {
                this.OnPropertyChanged("ToStoragePlaceID", this._ToStoragePlaceID, value);
                this._ToStoragePlaceID = value;
            }
        }
        
        public global::System.DateTime? RecordDate {
            get {
                return this._RecordDate;
            }
            set {
                this.OnPropertyChanged("RecordDate", this._RecordDate, value);
                this._RecordDate = value;
            }
        }
        
        public string OperPerson {
            get {
                return this._OperPerson;
            }
            set {
                this.OnPropertyChanged("OperPerson", this._OperPerson, value);
                this._OperPerson = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PpmRubberStorageDetail._PpmRubberStorageDetailEntityConfiguration == null)) {
                PpmRubberStorageDetail._PpmRubberStorageDetailEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmRubberStorageDetail");
            }
            return PpmRubberStorageDetail._PpmRubberStorageDetailEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._StorageID,
                    this._StoragePlaceID,
                    this._Barcode,
                    this._ShelfBarcode,
                    this._OrderID,
                    this._RubberType,
                    this._OperType,
                    this._PlanDate,
                    this._ShiftID,
                    this._ShiftClassID,
                    this._EquipCode,
                    this._MaterCode,
                    this._Weight,
                    this._ToStorageID,
                    this._ToStoragePlaceID,
                    this._RecordDate,
                    this._OperPerson};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._StorageID = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StoragePlaceID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Barcode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ShelfBarcode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._OrderID = reader.GetInt32(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._RubberType = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._OperType = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._PlanDate = this.GetDateTime(reader, 7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._ShiftID = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._ShiftClassID = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._EquipCode = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._MaterCode = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._Weight = reader.GetDecimal(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._ToStorageID = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._ToStoragePlaceID = reader.GetString(14);
            }
            if ((false == reader.IsDBNull(15))) {
                this._RecordDate = this.GetDateTime(reader, 15);
            }
            if ((false == reader.IsDBNull(16))) {
                this._OperPerson = reader.GetString(16);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._StorageID = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StoragePlaceID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Barcode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ShelfBarcode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._OrderID = ((int)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._RubberType = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._OperType = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._PlanDate = this.GetDateTime(row, 7);
            }
            if ((false == row.IsNull(8))) {
                this._ShiftID = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._ShiftClassID = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._EquipCode = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._MaterCode = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._Weight = ((System.Nullable<decimal>)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._ToStorageID = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._ToStoragePlaceID = ((string)(row[14]));
            }
            if ((false == row.IsNull(15))) {
                this._RecordDate = this.GetDateTime(row, 15);
            }
            if ((false == row.IsNull(16))) {
                this._OperPerson = ((string)(row[16]));
            }
            this.ReloadQueries(false);
        }
        
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        
        public override bool Equals(object obj) {
            if ((obj == null)) {
                return false;
            }
            if ((false == typeof(global::Mesnac.Entity.PpmRubberStorageDetail).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((((this.isAttached && ((global::Mesnac.Entity.PpmRubberStorageDetail)(obj)).isAttached) 
                        && (this.StorageID == ((global::Mesnac.Entity.PpmRubberStorageDetail)(obj)).StorageID)) 
                        && (this.StoragePlaceID == ((global::Mesnac.Entity.PpmRubberStorageDetail)(obj)).StoragePlaceID)) 
                        && (this.Barcode == ((global::Mesnac.Entity.PpmRubberStorageDetail)(obj)).Barcode)) 
                        && (this.OrderID == ((global::Mesnac.Entity.PpmRubberStorageDetail)(obj)).OrderID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _ShelfBarcode = new NBear.Common.PropertyItem("ShelfBarcode", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _OrderID = new NBear.Common.PropertyItem("OrderID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _RubberType = new NBear.Common.PropertyItem("RubberType", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _OperType = new NBear.Common.PropertyItem("OperType", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _PlanDate = new NBear.Common.PropertyItem("PlanDate", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _ShiftClassID = new NBear.Common.PropertyItem("ShiftClassID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _EquipCode = new NBear.Common.PropertyItem("EquipCode", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _Weight = new NBear.Common.PropertyItem("Weight", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _ToStorageID = new NBear.Common.PropertyItem("ToStorageID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _ToStoragePlaceID = new NBear.Common.PropertyItem("ToStoragePlaceID", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PpmRubberStorageDetail");
            
            protected NBear.Common.PropertyItem _OperPerson = new NBear.Common.PropertyItem("OperPerson", "Mesnac.Entity.PpmRubberStorageDetail");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem StorageID {
                get {
                    if ((aliasName == null)) {
                        return _StorageID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StorageID", _StorageID.EntityConfiguration, _StorageID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StoragePlaceID {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceID", _StoragePlaceID.EntityConfiguration, _StoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Barcode {
                get {
                    if ((aliasName == null)) {
                        return _Barcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShelfBarcode {
                get {
                    if ((aliasName == null)) {
                        return _ShelfBarcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShelfBarcode", _ShelfBarcode.EntityConfiguration, _ShelfBarcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OrderID {
                get {
                    if ((aliasName == null)) {
                        return _OrderID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OrderID", _OrderID.EntityConfiguration, _OrderID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RubberType {
                get {
                    if ((aliasName == null)) {
                        return _RubberType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RubberType", _RubberType.EntityConfiguration, _RubberType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OperType {
                get {
                    if ((aliasName == null)) {
                        return _OperType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OperType", _OperType.EntityConfiguration, _OperType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PlanDate {
                get {
                    if ((aliasName == null)) {
                        return _PlanDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PlanDate", _PlanDate.EntityConfiguration, _PlanDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShiftID {
                get {
                    if ((aliasName == null)) {
                        return _ShiftID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftID", _ShiftID.EntityConfiguration, _ShiftID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShiftClassID {
                get {
                    if ((aliasName == null)) {
                        return _ShiftClassID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftClassID", _ShiftClassID.EntityConfiguration, _ShiftClassID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem EquipCode {
                get {
                    if ((aliasName == null)) {
                        return _EquipCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("EquipCode", _EquipCode.EntityConfiguration, _EquipCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem MaterCode {
                get {
                    if ((aliasName == null)) {
                        return _MaterCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MaterCode", _MaterCode.EntityConfiguration, _MaterCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Weight {
                get {
                    if ((aliasName == null)) {
                        return _Weight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Weight", _Weight.EntityConfiguration, _Weight.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ToStorageID {
                get {
                    if ((aliasName == null)) {
                        return _ToStorageID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ToStorageID", _ToStorageID.EntityConfiguration, _ToStorageID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ToStoragePlaceID {
                get {
                    if ((aliasName == null)) {
                        return _ToStoragePlaceID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ToStoragePlaceID", _ToStoragePlaceID.EntityConfiguration, _ToStoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RecordDate {
                get {
                    if ((aliasName == null)) {
                        return _RecordDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RecordDate", _RecordDate.EntityConfiguration, _RecordDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OperPerson {
                get {
                    if ((aliasName == null)) {
                        return _OperPerson;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OperPerson", _OperPerson.EntityConfiguration, _OperPerson.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
