//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34011
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
    public partial class EqmSapSparePartArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.EqmSapSparePart> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.EqmSapSparePart\" isReadOnly=\"false\" isAutoPreLoad=\"false\" is" +
        "BatchUpdate=\"false\" isRelation=\"false\" mappingName=\"EqmSapSparePart\" batchSize=\"" +
        "10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited" +
        "=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNo" +
        "tNull=\"true\" />\r\n    <Property name=\"ReceiveNo\" type=\"System.String\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"ReceiveNo\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(100)\" isPrimar" +
        "yKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ReceiveDate\" type=\"System" +
        ".Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"ReceiveDate\" mappingColumnType=\"Syst" +
        "em.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNul" +
        "l=\"false\" />\r\n    <Property name=\"SparePartCode\" type=\"System.String\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"SparePartCode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(100)\" isP" +
        "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"SparePartModel\" type" +
        "=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" i" +
        "sContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRela" +
        "tionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializat" +
        "ionIgnore=\"false\" mappingName=\"SparePartModel\" mappingColumnType=\"System.String\"" +
        " sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Propert" +
        "y name=\"StoreInNum\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\"" +
        " isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" i" +
        "sFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fal" +
        "se\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Store" +
        "InNum\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" i" +
        "sPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ReceiveUser\" type=" +
        "\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" is" +
        "Contained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelat" +
        "ionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializati" +
        "onIgnore=\"false\" mappingName=\"ReceiveUser\" mappingColumnType=\"System.String\" sql" +
        "Type=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property na" +
        "me=\"RecordDate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" is" +
        "ReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFr" +
        "iendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\"" +
        " isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RecordDa" +
        "te\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" is" +
        "PrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"Syste" +
        "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarc" +
        "har(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteF" +
        "lag\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=" +
        "\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fals" +
        "e\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isS" +
        "erializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.St" +
        "ring\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Propertie" +
        "s>\r\n</EntityConfiguration>")]
    public partial class EqmSapSparePart : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _EqmSapSparePartEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _ReceiveNo;
        
        protected global::System.DateTime? _ReceiveDate;
        
        protected string _SparePartCode;
        
        protected string _SparePartModel;
        
        protected global::System.Decimal? _StoreInNum;
        
        protected string _ReceiveUser;
        
        protected global::System.DateTime? _RecordDate;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.EqmSapSparePart left, global::Mesnac.Entity.EqmSapSparePart right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.EqmSapSparePart left, global::Mesnac.Entity.EqmSapSparePart right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string ReceiveNo {
            get {
                return this._ReceiveNo;
            }
            set {
                this.OnPropertyChanged("ReceiveNo", this._ReceiveNo, value);
                this._ReceiveNo = value;
            }
        }
        
        public global::System.DateTime? ReceiveDate {
            get {
                return this._ReceiveDate;
            }
            set {
                this.OnPropertyChanged("ReceiveDate", this._ReceiveDate, value);
                this._ReceiveDate = value;
            }
        }
        
        public string SparePartCode {
            get {
                return this._SparePartCode;
            }
            set {
                this.OnPropertyChanged("SparePartCode", this._SparePartCode, value);
                this._SparePartCode = value;
            }
        }
        
        public string SparePartModel {
            get {
                return this._SparePartModel;
            }
            set {
                this.OnPropertyChanged("SparePartModel", this._SparePartModel, value);
                this._SparePartModel = value;
            }
        }
        
        public global::System.Decimal? StoreInNum {
            get {
                return this._StoreInNum;
            }
            set {
                this.OnPropertyChanged("StoreInNum", this._StoreInNum, value);
                this._StoreInNum = value;
            }
        }
        
        public string ReceiveUser {
            get {
                return this._ReceiveUser;
            }
            set {
                this.OnPropertyChanged("ReceiveUser", this._ReceiveUser, value);
                this._ReceiveUser = value;
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
        
        public string Remark {
            get {
                return this._Remark;
            }
            set {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
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
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((EqmSapSparePart._EqmSapSparePartEntityConfiguration == null)) {
                EqmSapSparePart._EqmSapSparePartEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.EqmSapSparePart");
            }
            return EqmSapSparePart._EqmSapSparePartEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._ReceiveNo,
                    this._ReceiveDate,
                    this._SparePartCode,
                    this._SparePartModel,
                    this._StoreInNum,
                    this._ReceiveUser,
                    this._RecordDate,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._ReceiveNo = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._ReceiveDate = this.GetDateTime(reader, 2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._SparePartCode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._SparePartModel = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._StoreInNum = reader.GetDecimal(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._ReceiveUser = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._RecordDate = this.GetDateTime(reader, 7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._Remark = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._DeleteFlag = reader.GetString(9);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._ReceiveNo = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._ReceiveDate = this.GetDateTime(row, 2);
            }
            if ((false == row.IsNull(3))) {
                this._SparePartCode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._SparePartModel = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._StoreInNum = ((System.Nullable<decimal>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._ReceiveUser = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._RecordDate = this.GetDateTime(row, 7);
            }
            if ((false == row.IsNull(8))) {
                this._Remark = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._DeleteFlag = ((string)(row[9]));
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
            if ((false == typeof(global::Mesnac.Entity.EqmSapSparePart).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.EqmSapSparePart)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.EqmSapSparePart)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _ReceiveNo = new NBear.Common.PropertyItem("ReceiveNo", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _ReceiveDate = new NBear.Common.PropertyItem("ReceiveDate", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _SparePartCode = new NBear.Common.PropertyItem("SparePartCode", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _SparePartModel = new NBear.Common.PropertyItem("SparePartModel", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _StoreInNum = new NBear.Common.PropertyItem("StoreInNum", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _ReceiveUser = new NBear.Common.PropertyItem("ReceiveUser", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.EqmSapSparePart");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.EqmSapSparePart");
            
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
            
            public NBear.Common.PropertyItem ReceiveNo {
                get {
                    if ((aliasName == null)) {
                        return _ReceiveNo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ReceiveNo", _ReceiveNo.EntityConfiguration, _ReceiveNo.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ReceiveDate {
                get {
                    if ((aliasName == null)) {
                        return _ReceiveDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ReceiveDate", _ReceiveDate.EntityConfiguration, _ReceiveDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SparePartCode {
                get {
                    if ((aliasName == null)) {
                        return _SparePartCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SparePartCode", _SparePartCode.EntityConfiguration, _SparePartCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SparePartModel {
                get {
                    if ((aliasName == null)) {
                        return _SparePartModel;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SparePartModel", _SparePartModel.EntityConfiguration, _SparePartModel.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StoreInNum {
                get {
                    if ((aliasName == null)) {
                        return _StoreInNum;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoreInNum", _StoreInNum.EntityConfiguration, _StoreInNum.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ReceiveUser {
                get {
                    if ((aliasName == null)) {
                        return _ReceiveUser;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ReceiveUser", _ReceiveUser.EntityConfiguration, _ReceiveUser.PropertyConfiguration, aliasName);
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
        }
    }
}
