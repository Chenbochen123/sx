//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
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
    public partial class PpmRubberCarryoverArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmRubberCarryover> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmRubberCarryover\" isReadOnly=\"false\" isAutoPreLoad=\"false\"" +
        " isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmRubberCarryover\" batch" +
        "Size=\"10\">\r\n  <Properties>\r\n    <Property name=\"InaccountDuration\" type=\"System." +
        "String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContaine" +
        "d=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"" +
        "false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore" +
        "=\"false\" mappingName=\"InaccountDuration\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"nvarchar(6)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Ba" +
        "rcode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"nvarchar(18)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Proper" +
        "ty name=\"StorageID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" " +
        "isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" i" +
        "sLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyD" +
        "esc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StorageID\" mappingColumnT" +
        "ype=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" " +
        "/>\r\n    <Property name=\"StoragePlaceID\" type=\"System.String\" isInherited=\"false\"" +
        " isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" i" +
        "sFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fal" +
        "se\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Stora" +
        "gePlaceID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey" +
        "=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"ProductNo\" type=\"System.String\"" +
        " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
        "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
        "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
        "\" mappingName=\"ProductNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(18" +
        ")\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"MaterCode\" typ" +
        "e=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" " +
        "isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRel" +
        "ationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializa" +
        "tionIgnore=\"false\" mappingName=\"MaterCode\" mappingColumnType=\"System.String\" sql" +
        "Type=\"nvarchar(13)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property nam" +
        "e=\"ProcDate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ProcDate\" m" +
        "appingColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Num\" type=\"System.Nullab" +
        "le`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false" +
        "\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isR" +
        "elationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeriali" +
        "zationIgnore=\"false\" mappingName=\"Num\" mappingColumnType=\"System.Nullable`1[Syst" +
        "em.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Propert" +
        "y name=\"PieceWeight\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Piec" +
        "eWeight\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RealWeight\" type" +
        "=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCo" +
        "mpoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLaz" +
        "yLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=" +
        "\"false\" isSerializationIgnore=\"false\" mappingName=\"RealWeight\" mappingColumnType" +
        "=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNo" +
        "tNull=\"false\" />\r\n    <Property name=\"RecordDate\" type=\"System.Nullable`1[System" +
        ".DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"RecordDate\" mappingColumnType=\"System.Nullable`1[Syste" +
        "m.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <" +
        "Property name=\"FactoryID\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"fa" +
        "lse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"F" +
        "actoryID\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPr" +
        "imaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PpmRubberCarryover : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PpmRubberCarryoverEntityConfiguration;
        
        protected string _InaccountDuration;
        
        protected string _Barcode;
        
        protected string _StorageID;
        
        protected string _StoragePlaceID;
        
        protected string _ProductNo;
        
        protected string _MaterCode;
        
        protected global::System.DateTime? _ProcDate;
        
        protected global::System.Int32? _Num;
        
        protected global::System.Decimal? _PieceWeight;
        
        protected global::System.Decimal? _RealWeight;
        
        protected global::System.DateTime? _RecordDate;
        
        protected global::System.Int32? _FactoryID;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PpmRubberCarryover left, global::Mesnac.Entity.PpmRubberCarryover right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PpmRubberCarryover left, global::Mesnac.Entity.PpmRubberCarryover right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string InaccountDuration {
            get {
                return this._InaccountDuration;
            }
            set {
                this.OnPropertyChanged("InaccountDuration", this._InaccountDuration, value);
                this._InaccountDuration = value;
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
        
        public string ProductNo {
            get {
                return this._ProductNo;
            }
            set {
                this.OnPropertyChanged("ProductNo", this._ProductNo, value);
                this._ProductNo = value;
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
        
        public global::System.DateTime? ProcDate {
            get {
                return this._ProcDate;
            }
            set {
                this.OnPropertyChanged("ProcDate", this._ProcDate, value);
                this._ProcDate = value;
            }
        }
        
        public global::System.Int32? Num {
            get {
                return this._Num;
            }
            set {
                this.OnPropertyChanged("Num", this._Num, value);
                this._Num = value;
            }
        }
        
        public global::System.Decimal? PieceWeight {
            get {
                return this._PieceWeight;
            }
            set {
                this.OnPropertyChanged("PieceWeight", this._PieceWeight, value);
                this._PieceWeight = value;
            }
        }
        
        public global::System.Decimal? RealWeight {
            get {
                return this._RealWeight;
            }
            set {
                this.OnPropertyChanged("RealWeight", this._RealWeight, value);
                this._RealWeight = value;
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
        
        public global::System.Int32? FactoryID {
            get {
                return this._FactoryID;
            }
            set {
                this.OnPropertyChanged("FactoryID", this._FactoryID, value);
                this._FactoryID = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PpmRubberCarryover._PpmRubberCarryoverEntityConfiguration == null)) {
                PpmRubberCarryover._PpmRubberCarryoverEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmRubberCarryover");
            }
            return PpmRubberCarryover._PpmRubberCarryoverEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._InaccountDuration,
                    this._Barcode,
                    this._StorageID,
                    this._StoragePlaceID,
                    this._ProductNo,
                    this._MaterCode,
                    this._ProcDate,
                    this._Num,
                    this._PieceWeight,
                    this._RealWeight,
                    this._RecordDate,
                    this._FactoryID};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._InaccountDuration = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Barcode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._StorageID = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._StoragePlaceID = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._ProductNo = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._MaterCode = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._ProcDate = this.GetDateTime(reader, 6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._Num = reader.GetInt32(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._PieceWeight = reader.GetDecimal(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._RealWeight = reader.GetDecimal(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._RecordDate = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._FactoryID = reader.GetInt32(11);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._InaccountDuration = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Barcode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._StorageID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._StoragePlaceID = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._ProductNo = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._MaterCode = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._ProcDate = this.GetDateTime(row, 6);
            }
            if ((false == row.IsNull(7))) {
                this._Num = ((System.Nullable<int>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._PieceWeight = ((System.Nullable<decimal>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._RealWeight = ((System.Nullable<decimal>)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._RecordDate = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11))) {
                this._FactoryID = ((System.Nullable<int>)(row[11]));
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
            if ((false == typeof(global::Mesnac.Entity.PpmRubberCarryover).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((((this.isAttached && ((global::Mesnac.Entity.PpmRubberCarryover)(obj)).isAttached) 
                        && (this.InaccountDuration == ((global::Mesnac.Entity.PpmRubberCarryover)(obj)).InaccountDuration)) 
                        && (this.Barcode == ((global::Mesnac.Entity.PpmRubberCarryover)(obj)).Barcode)) 
                        && (this.StorageID == ((global::Mesnac.Entity.PpmRubberCarryover)(obj)).StorageID)) 
                        && (this.StoragePlaceID == ((global::Mesnac.Entity.PpmRubberCarryover)(obj)).StoragePlaceID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _InaccountDuration = new NBear.Common.PropertyItem("InaccountDuration", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _ProductNo = new NBear.Common.PropertyItem("ProductNo", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _ProcDate = new NBear.Common.PropertyItem("ProcDate", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _Num = new NBear.Common.PropertyItem("Num", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _PieceWeight = new NBear.Common.PropertyItem("PieceWeight", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _RealWeight = new NBear.Common.PropertyItem("RealWeight", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PpmRubberCarryover");
            
            protected NBear.Common.PropertyItem _FactoryID = new NBear.Common.PropertyItem("FactoryID", "Mesnac.Entity.PpmRubberCarryover");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem InaccountDuration {
                get {
                    if ((aliasName == null)) {
                        return _InaccountDuration;
                    }
                    else {
                        return new NBear.Common.PropertyItem("InaccountDuration", _InaccountDuration.EntityConfiguration, _InaccountDuration.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem ProductNo {
                get {
                    if ((aliasName == null)) {
                        return _ProductNo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ProductNo", _ProductNo.EntityConfiguration, _ProductNo.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem ProcDate {
                get {
                    if ((aliasName == null)) {
                        return _ProcDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ProcDate", _ProcDate.EntityConfiguration, _ProcDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Num {
                get {
                    if ((aliasName == null)) {
                        return _Num;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Num", _Num.EntityConfiguration, _Num.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PieceWeight {
                get {
                    if ((aliasName == null)) {
                        return _PieceWeight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PieceWeight", _PieceWeight.EntityConfiguration, _PieceWeight.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RealWeight {
                get {
                    if ((aliasName == null)) {
                        return _RealWeight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RealWeight", _RealWeight.EntityConfiguration, _RealWeight.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem FactoryID {
                get {
                    if ((aliasName == null)) {
                        return _FactoryID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FactoryID", _FactoryID.EntityConfiguration, _FactoryID.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
