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
    public partial class PpmRubberStoreoutDetailArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmRubberStoreoutDetail> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmRubberStoreoutDetail\" isReadOnly=\"false\" isAutoPreLoad=\"f" +
        "alse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmRubberStoreoutDet" +
        "ail\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"BillNo\" type=\"System.S" +
        "tring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained" +
        "=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"f" +
        "alse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=" +
        "\"false\" mappingName=\"BillNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar" +
        "(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Barcode\" type" +
        "=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" i" +
        "sContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRela" +
        "tionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializat" +
        "ionIgnore=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.String\" sqlTyp" +
        "e=\"nvarchar(18)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Or" +
        "derID\" type=\"System.Int32\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"OrderID\" mappingColumnType=\"System.Int3" +
        "2\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Pr" +
        "oductNo\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"ProductNo\" mappingColumnType=\"System" +
        ".String\" sqlType=\"nvarchar(18)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <" +
        "Property name=\"StoragePlaceID\" type=\"System.String\" isInherited=\"false\" isReadOn" +
        "ly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKe" +
        "y=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInd" +
        "exPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StoragePlaceID" +
        "\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"MaterCode\" type=\"System.String\" isInhe" +
        "rited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"MaterCode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(13)\" isPr" +
        "imaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ProcDate\" type=\"Syste" +
        "m.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"ProcDate\" mappingColumnType=\"System" +
        ".Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"OutputNum\" type=\"System.Nullable`1[System.Int32]" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"OutputNum\" mappingColumnType=\"System.Nullable`1[System.Int32]\" s" +
        "qlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Piec" +
        "eWeight\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"PieceWeight\" map" +
        "pingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKe" +
        "y=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"OutputWeight\" type=\"System.N" +
        "ullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"OutputWeight\" mappingColumnType=\"System" +
        ".Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"f" +
        "alse\" />\r\n    <Property name=\"RecordDate\" type=\"System.Nullable`1[System.DateTim" +
        "e]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"f" +
        "alse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fals" +
        "e\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fa" +
        "lse\" mappingName=\"RecordDate\" mappingColumnType=\"System.Nullable`1[System.DateTi" +
        "me]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property" +
        " name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Dele" +
        "teFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\"" +
        " isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"System.String\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(100)\" isPrimar" +
        "yKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PpmRubberStoreoutDetail : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PpmRubberStoreoutDetailEntityConfiguration;
        
        protected string _BillNo;
        
        protected string _Barcode;
        
        protected int _OrderID;
        
        protected string _ProductNo;
        
        protected string _StoragePlaceID;
        
        protected string _MaterCode;
        
        protected global::System.DateTime? _ProcDate;
        
        protected global::System.Int32? _OutputNum;
        
        protected global::System.Decimal? _PieceWeight;
        
        protected global::System.Decimal? _OutputWeight;
        
        protected global::System.DateTime? _RecordDate;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PpmRubberStoreoutDetail left, global::Mesnac.Entity.PpmRubberStoreoutDetail right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PpmRubberStoreoutDetail left, global::Mesnac.Entity.PpmRubberStoreoutDetail right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string BillNo {
            get {
                return this._BillNo;
            }
            set {
                this.OnPropertyChanged("BillNo", this._BillNo, value);
                this._BillNo = value;
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
        
        public int OrderID {
            get {
                return this._OrderID;
            }
            set {
                this.OnPropertyChanged("OrderID", this._OrderID, value);
                this._OrderID = value;
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
        
        public string StoragePlaceID {
            get {
                return this._StoragePlaceID;
            }
            set {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
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
        
        public global::System.Int32? OutputNum {
            get {
                return this._OutputNum;
            }
            set {
                this.OnPropertyChanged("OutputNum", this._OutputNum, value);
                this._OutputNum = value;
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
        
        public global::System.Decimal? OutputWeight {
            get {
                return this._OutputWeight;
            }
            set {
                this.OnPropertyChanged("OutputWeight", this._OutputWeight, value);
                this._OutputWeight = value;
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
        
        public string DeleteFlag {
            get {
                return this._DeleteFlag;
            }
            set {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
            }
        }
        
        public string Remark {
            get {
                return this._Remark;
            }
            set {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PpmRubberStoreoutDetail._PpmRubberStoreoutDetailEntityConfiguration == null)) {
                PpmRubberStoreoutDetail._PpmRubberStoreoutDetailEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmRubberStoreoutDetail");
            }
            return PpmRubberStoreoutDetail._PpmRubberStoreoutDetailEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._BillNo,
                    this._Barcode,
                    this._OrderID,
                    this._ProductNo,
                    this._StoragePlaceID,
                    this._MaterCode,
                    this._ProcDate,
                    this._OutputNum,
                    this._PieceWeight,
                    this._OutputWeight,
                    this._RecordDate,
                    this._DeleteFlag,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._BillNo = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Barcode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._OrderID = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ProductNo = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._StoragePlaceID = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._MaterCode = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._ProcDate = this.GetDateTime(reader, 6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._OutputNum = reader.GetInt32(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._PieceWeight = reader.GetDecimal(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._OutputWeight = reader.GetDecimal(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._RecordDate = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._DeleteFlag = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._Remark = reader.GetString(12);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._BillNo = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Barcode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._OrderID = ((int)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ProductNo = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._StoragePlaceID = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._MaterCode = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._ProcDate = this.GetDateTime(row, 6);
            }
            if ((false == row.IsNull(7))) {
                this._OutputNum = ((System.Nullable<int>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._PieceWeight = ((System.Nullable<decimal>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._OutputWeight = ((System.Nullable<decimal>)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._RecordDate = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11))) {
                this._DeleteFlag = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._Remark = ((string)(row[12]));
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
            if ((false == typeof(global::Mesnac.Entity.PpmRubberStoreoutDetail).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((((this.isAttached && ((global::Mesnac.Entity.PpmRubberStoreoutDetail)(obj)).isAttached) 
                        && (this.BillNo == ((global::Mesnac.Entity.PpmRubberStoreoutDetail)(obj)).BillNo)) 
                        && (this.Barcode == ((global::Mesnac.Entity.PpmRubberStoreoutDetail)(obj)).Barcode)) 
                        && (this.OrderID == ((global::Mesnac.Entity.PpmRubberStoreoutDetail)(obj)).OrderID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _BillNo = new NBear.Common.PropertyItem("BillNo", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _OrderID = new NBear.Common.PropertyItem("OrderID", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _ProductNo = new NBear.Common.PropertyItem("ProductNo", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _ProcDate = new NBear.Common.PropertyItem("ProcDate", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _OutputNum = new NBear.Common.PropertyItem("OutputNum", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _PieceWeight = new NBear.Common.PropertyItem("PieceWeight", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _OutputWeight = new NBear.Common.PropertyItem("OutputWeight", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PpmRubberStoreoutDetail");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem BillNo {
                get {
                    if ((aliasName == null)) {
                        return _BillNo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("BillNo", _BillNo.EntityConfiguration, _BillNo.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem OutputNum {
                get {
                    if ((aliasName == null)) {
                        return _OutputNum;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OutputNum", _OutputNum.EntityConfiguration, _OutputNum.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem OutputWeight {
                get {
                    if ((aliasName == null)) {
                        return _OutputWeight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OutputWeight", _OutputWeight.EntityConfiguration, _OutputWeight.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem DeleteFlag {
                get {
                    if ((aliasName == null)) {
                        return _DeleteFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DeleteFlag", _DeleteFlag.EntityConfiguration, _DeleteFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Remark {
                get {
                    if ((aliasName == null)) {
                        return _Remark;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Remark", _Remark.EntityConfiguration, _Remark.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
