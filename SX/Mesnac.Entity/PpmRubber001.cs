//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1022
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
    public partial class PpmRubber001ArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmRubber001> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmRubber001\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBat" +
        "chUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmRubber001\" batchSize=\"10\">\r\n" +
        "  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int64\" isInherited=\"fals" +
        "e\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ObjI" +
        "D\" mappingColumnType=\"System.Int64\" sqlType=\"bigint\" isPrimaryKey=\"false\" isNotN" +
        "ull=\"false\" />\r\n    <Property name=\"StorageID\" type=\"System.String\" isInherited=" +
        "\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryK" +
        "ey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StoragePlaceID\" type=\"System." +
        "String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContaine" +
        "d=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"" +
        "false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore" +
        "=\"false\" mappingName=\"StoragePlaceID\" mappingColumnType=\"System.String\" sqlType=" +
        "\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Barc" +
        "ode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=" +
        "\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fals" +
        "e\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isS" +
        "erializationIgnore=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.Strin" +
        "g\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property" +
        " name=\"RubberType\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" i" +
        "sCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" is" +
        "LazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDe" +
        "sc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RubberType\" mappingColumnT" +
        "ype=\"System.String\" sqlType=\"nvarchar(2)\" isPrimaryKey=\"true\" isNotNull=\"true\" /" +
        ">\r\n    <Property name=\"PlanDate\" type=\"System.DateTime\" isInherited=\"false\" isRe" +
        "adOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrie" +
        "ndKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" i" +
        "sIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"PlanDate\" " +
        "mappingColumnType=\"System.DateTime\" sqlType=\"datetime\" isPrimaryKey=\"true\" isNot" +
        "Null=\"true\" />\r\n    <Property name=\"ShiftID\" type=\"System.String\" isInherited=\"f" +
        "alse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fal" +
        "se\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty" +
        "=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"" +
        "ShiftID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"true\"" +
        " isNotNull=\"true\" />\r\n    <Property name=\"ShiftClassID\" type=\"System.String\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"ShiftClassID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isP" +
        "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"EquipCode\" type=\"Sys" +
        "tem.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"EquipCode\" mappingColumnType=\"System.String\" sqlType=\"" +
        "nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Mater" +
        "Code\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"MaterCode\" mappingColumnType=\"System.St" +
        "ring\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Prope" +
        "rty name=\"ShiftNum\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" i" +
        "sReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isF" +
        "riendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false" +
        "\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ShiftNu" +
        "m\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKe" +
        "y=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StockType\" type=\"System.Int3" +
        "2\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"StockType\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPri" +
        "maryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"TotalWeight\" type=\"Syste" +
        "m.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"TotalWeight\" mappingColumnType=\"Syst" +
        "em.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"OperPerson\" type=\"System.String\" isInherited=\"fa" +
        "lse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"O" +
        "perPerson\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey" +
        "=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"SAPVersionID\" type=\"System.Stri" +
        "ng\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"f" +
        "alse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fals" +
        "e\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fa" +
        "lse\" mappingName=\"SAPVersionID\" mappingColumnType=\"System.String\" sqlType=\"nvarc" +
        "har(20)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"ToStorageI" +
        "D\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"ToStorageID\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Prop" +
        "erty name=\"ToStoragePlaceID\" type=\"System.String\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ToStoragePlaceID" +
        "\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PpmRubber001 : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PpmRubber001EntityConfiguration;
        
        protected long _ObjID;
        
        protected string _StorageID;
        
        protected string _StoragePlaceID;
        
        protected string _Barcode;
        
        protected string _RubberType;
        
        protected global::System.DateTime _PlanDate;
        
        protected string _ShiftID;
        
        protected string _ShiftClassID;
        
        protected string _EquipCode;
        
        protected string _MaterCode;
        
        protected global::System.Int32? _ShiftNum;
        
        protected int _StockType;
        
        protected global::System.Decimal? _TotalWeight;
        
        protected string _OperPerson;
        
        protected string _SAPVersionID;
        
        protected string _ToStorageID;
        
        protected string _ToStoragePlaceID;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PpmRubber001 left, global::Mesnac.Entity.PpmRubber001 right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PpmRubber001 left, global::Mesnac.Entity.PpmRubber001 right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public long ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
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
        
        public string RubberType {
            get {
                return this._RubberType;
            }
            set {
                this.OnPropertyChanged("RubberType", this._RubberType, value);
                this._RubberType = value;
            }
        }
        
        public global::System.DateTime PlanDate {
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
        
        public global::System.Int32? ShiftNum {
            get {
                return this._ShiftNum;
            }
            set {
                this.OnPropertyChanged("ShiftNum", this._ShiftNum, value);
                this._ShiftNum = value;
            }
        }
        
        public int StockType {
            get {
                return this._StockType;
            }
            set {
                this.OnPropertyChanged("StockType", this._StockType, value);
                this._StockType = value;
            }
        }
        
        public global::System.Decimal? TotalWeight {
            get {
                return this._TotalWeight;
            }
            set {
                this.OnPropertyChanged("TotalWeight", this._TotalWeight, value);
                this._TotalWeight = value;
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
        
        public string SAPVersionID {
            get {
                return this._SAPVersionID;
            }
            set {
                this.OnPropertyChanged("SAPVersionID", this._SAPVersionID, value);
                this._SAPVersionID = value;
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
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PpmRubber001._PpmRubber001EntityConfiguration == null)) {
                PpmRubber001._PpmRubber001EntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmRubber001");
            }
            return PpmRubber001._PpmRubber001EntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._StorageID,
                    this._StoragePlaceID,
                    this._Barcode,
                    this._RubberType,
                    this._PlanDate,
                    this._ShiftID,
                    this._ShiftClassID,
                    this._EquipCode,
                    this._MaterCode,
                    this._ShiftNum,
                    this._StockType,
                    this._TotalWeight,
                    this._OperPerson,
                    this._SAPVersionID,
                    this._ToStorageID,
                    this._ToStoragePlaceID};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt64(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StorageID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._StoragePlaceID = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Barcode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._RubberType = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._PlanDate = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._ShiftID = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._ShiftClassID = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._EquipCode = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._MaterCode = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._ShiftNum = reader.GetInt32(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._StockType = reader.GetInt32(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._TotalWeight = reader.GetDecimal(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._OperPerson = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._SAPVersionID = reader.GetString(14);
            }
            if ((false == reader.IsDBNull(15))) {
                this._ToStorageID = reader.GetString(15);
            }
            if ((false == reader.IsDBNull(16))) {
                this._ToStoragePlaceID = reader.GetString(16);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((long)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StorageID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._StoragePlaceID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Barcode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._RubberType = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._PlanDate = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6))) {
                this._ShiftID = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._ShiftClassID = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._EquipCode = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._MaterCode = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._ShiftNum = ((System.Nullable<int>)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._StockType = ((int)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._TotalWeight = ((System.Nullable<decimal>)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._OperPerson = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._SAPVersionID = ((string)(row[14]));
            }
            if ((false == row.IsNull(15))) {
                this._ToStorageID = ((string)(row[15]));
            }
            if ((false == row.IsNull(16))) {
                this._ToStoragePlaceID = ((string)(row[16]));
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
            if ((false == typeof(global::Mesnac.Entity.PpmRubber001).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((((((((((((this.isAttached && ((global::Mesnac.Entity.PpmRubber001)(obj)).isAttached) 
                        && (this.StorageID == ((global::Mesnac.Entity.PpmRubber001)(obj)).StorageID)) 
                        && (this.StoragePlaceID == ((global::Mesnac.Entity.PpmRubber001)(obj)).StoragePlaceID)) 
                        && (this.Barcode == ((global::Mesnac.Entity.PpmRubber001)(obj)).Barcode)) 
                        && (this.RubberType == ((global::Mesnac.Entity.PpmRubber001)(obj)).RubberType)) 
                        && (this.PlanDate == ((global::Mesnac.Entity.PpmRubber001)(obj)).PlanDate)) 
                        && (this.ShiftID == ((global::Mesnac.Entity.PpmRubber001)(obj)).ShiftID)) 
                        && (this.EquipCode == ((global::Mesnac.Entity.PpmRubber001)(obj)).EquipCode)) 
                        && (this.MaterCode == ((global::Mesnac.Entity.PpmRubber001)(obj)).MaterCode)) 
                        && (this.StockType == ((global::Mesnac.Entity.PpmRubber001)(obj)).StockType)) 
                        && (this.OperPerson == ((global::Mesnac.Entity.PpmRubber001)(obj)).OperPerson)) 
                        && (this.SAPVersionID == ((global::Mesnac.Entity.PpmRubber001)(obj)).SAPVersionID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _RubberType = new NBear.Common.PropertyItem("RubberType", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _PlanDate = new NBear.Common.PropertyItem("PlanDate", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _ShiftClassID = new NBear.Common.PropertyItem("ShiftClassID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _EquipCode = new NBear.Common.PropertyItem("EquipCode", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _ShiftNum = new NBear.Common.PropertyItem("ShiftNum", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _StockType = new NBear.Common.PropertyItem("StockType", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _TotalWeight = new NBear.Common.PropertyItem("TotalWeight", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _OperPerson = new NBear.Common.PropertyItem("OperPerson", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _SAPVersionID = new NBear.Common.PropertyItem("SAPVersionID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _ToStorageID = new NBear.Common.PropertyItem("ToStorageID", "Mesnac.Entity.PpmRubber001");
            
            protected NBear.Common.PropertyItem _ToStoragePlaceID = new NBear.Common.PropertyItem("ToStoragePlaceID", "Mesnac.Entity.PpmRubber001");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem ObjID {
                get {
                    if ((aliasName == null)) {
                        return _ObjID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ObjID", _ObjID.EntityConfiguration, _ObjID.PropertyConfiguration, aliasName);
                    }
                }
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
            
            public NBear.Common.PropertyItem ShiftNum {
                get {
                    if ((aliasName == null)) {
                        return _ShiftNum;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftNum", _ShiftNum.EntityConfiguration, _ShiftNum.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StockType {
                get {
                    if ((aliasName == null)) {
                        return _StockType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StockType", _StockType.EntityConfiguration, _StockType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem TotalWeight {
                get {
                    if ((aliasName == null)) {
                        return _TotalWeight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TotalWeight", _TotalWeight.EntityConfiguration, _TotalWeight.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem SAPVersionID {
                get {
                    if ((aliasName == null)) {
                        return _SAPVersionID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SAPVersionID", _SAPVersionID.EntityConfiguration, _SAPVersionID.PropertyConfiguration, aliasName);
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
        }
    }
}
