//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
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
    public partial class PmtMixingModelArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PmtMixingModel> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PmtMixingModel\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isB" +
        "atchUpdate=\"false\" isRelation=\"false\" mappingName=\"PmtMixingModel\" batchSize=\"10" +
        "\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"" +
        "false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fal" +
        "se\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty" +
        "=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"" +
        "ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotN" +
        "ull=\"true\" />\r\n    <Property name=\"ModelCode\" type=\"System.String\" isInherited=\"" +
        "false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fa" +
        "lse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPropert" +
        "y=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=" +
        "\"ModelCode\" mappingColumnType=\"System.String\" sqlType=\"varchar(10)\" isPrimaryKey" +
        "=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"TemCode\" type=\"System.String\"" +
        " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
        "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
        "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
        "\" mappingName=\"TemCode\" mappingColumnType=\"System.String\" sqlType=\"varchar(10)\" " +
        "isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ActionCode\" type=" +
        "\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" is" +
        "Contained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelat" +
        "ionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializati" +
        "onIgnore=\"false\" mappingName=\"ActionCode\" mappingColumnType=\"System.String\" sqlT" +
        "ype=\"varchar(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"TempValue\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadO" +
        "nly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendK" +
        "ey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIn" +
        "dexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"TempValue\" ma" +
        "ppingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryK" +
        "ey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"PresValue\" type=\"System.Nul" +
        "lable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"" +
        "false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false" +
        "\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSe" +
        "rializationIgnore=\"false\" mappingName=\"PresValue\" mappingColumnType=\"System.Null" +
        "able`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\"" +
        " />\r\n    <Property name=\"RotaValue\" type=\"System.Nullable`1[System.Decimal]\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"RotaValue\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlT" +
        "ype=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Pow" +
        "erValue\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"PowerValue\" mapp" +
        "ingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey" +
        "=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"EnerValue\" type=\"System.Nulla" +
        "ble`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fa" +
        "lse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" " +
        "isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeri" +
        "alizationIgnore=\"false\" mappingName=\"EnerValue\" mappingColumnType=\"System.Nullab" +
        "le`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n    <Property name=\"TimeValue\" type=\"System.Nullable`1[System.Decimal]\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"TimeValue\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlTyp" +
        "e=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remar" +
        "k\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" " +
        "sqlType=\"varchar(500)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"RecordTime\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" " +
        "sqlDefaultValue=\"getdate()\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContaine" +
        "d=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"" +
        "false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore" +
        "=\"false\" mappingName=\"RecordTime\" mappingColumnType=\"System.Nullable`1[System.Da" +
        "teTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Prop" +
        "erty name=\"SeqIdx\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" is" +
        "ReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFr" +
        "iendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\"" +
        " isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"SeqIdx\" " +
        "mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"" +
        "false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PmtMixingModel : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PmtMixingModelEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _ModelCode;
        
        protected string _TemCode;
        
        protected string _ActionCode;
        
        protected global::System.Decimal? _TempValue;
        
        protected global::System.Decimal? _PresValue;
        
        protected global::System.Decimal? _RotaValue;
        
        protected global::System.Decimal? _PowerValue;
        
        protected global::System.Decimal? _EnerValue;
        
        protected global::System.Decimal? _TimeValue;
        
        protected string _Remark;
        
        protected global::System.DateTime? _RecordTime;
        
        protected global::System.Int32? _SeqIdx;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PmtMixingModel left, global::Mesnac.Entity.PmtMixingModel right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PmtMixingModel left, global::Mesnac.Entity.PmtMixingModel right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string ModelCode {
            get {
                return this._ModelCode;
            }
            set {
                this.OnPropertyChanged("ModelCode", this._ModelCode, value);
                this._ModelCode = value;
            }
        }
        
        public string TemCode {
            get {
                return this._TemCode;
            }
            set {
                this.OnPropertyChanged("TemCode", this._TemCode, value);
                this._TemCode = value;
            }
        }
        
        public string ActionCode {
            get {
                return this._ActionCode;
            }
            set {
                this.OnPropertyChanged("ActionCode", this._ActionCode, value);
                this._ActionCode = value;
            }
        }
        
        public global::System.Decimal? TempValue {
            get {
                return this._TempValue;
            }
            set {
                this.OnPropertyChanged("TempValue", this._TempValue, value);
                this._TempValue = value;
            }
        }
        
        public global::System.Decimal? PresValue {
            get {
                return this._PresValue;
            }
            set {
                this.OnPropertyChanged("PresValue", this._PresValue, value);
                this._PresValue = value;
            }
        }
        
        public global::System.Decimal? RotaValue {
            get {
                return this._RotaValue;
            }
            set {
                this.OnPropertyChanged("RotaValue", this._RotaValue, value);
                this._RotaValue = value;
            }
        }
        
        public global::System.Decimal? PowerValue {
            get {
                return this._PowerValue;
            }
            set {
                this.OnPropertyChanged("PowerValue", this._PowerValue, value);
                this._PowerValue = value;
            }
        }
        
        public global::System.Decimal? EnerValue {
            get {
                return this._EnerValue;
            }
            set {
                this.OnPropertyChanged("EnerValue", this._EnerValue, value);
                this._EnerValue = value;
            }
        }
        
        public global::System.Decimal? TimeValue {
            get {
                return this._TimeValue;
            }
            set {
                this.OnPropertyChanged("TimeValue", this._TimeValue, value);
                this._TimeValue = value;
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
        
        public global::System.DateTime? RecordTime {
            get {
                return this._RecordTime;
            }
            set {
                this.OnPropertyChanged("RecordTime", this._RecordTime, value);
                this._RecordTime = value;
            }
        }
        
        public global::System.Int32? SeqIdx {
            get {
                return this._SeqIdx;
            }
            set {
                this.OnPropertyChanged("SeqIdx", this._SeqIdx, value);
                this._SeqIdx = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PmtMixingModel._PmtMixingModelEntityConfiguration == null)) {
                PmtMixingModel._PmtMixingModelEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PmtMixingModel");
            }
            return PmtMixingModel._PmtMixingModelEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._ModelCode,
                    this._TemCode,
                    this._ActionCode,
                    this._TempValue,
                    this._PresValue,
                    this._RotaValue,
                    this._PowerValue,
                    this._EnerValue,
                    this._TimeValue,
                    this._Remark,
                    this._RecordTime,
                    this._SeqIdx};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._ModelCode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._TemCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ActionCode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._TempValue = reader.GetDecimal(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._PresValue = reader.GetDecimal(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._RotaValue = reader.GetDecimal(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._PowerValue = reader.GetDecimal(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._EnerValue = reader.GetDecimal(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._TimeValue = reader.GetDecimal(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._Remark = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._RecordTime = this.GetDateTime(reader, 11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._SeqIdx = reader.GetInt32(12);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._ModelCode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._TemCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ActionCode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._TempValue = ((System.Nullable<decimal>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._PresValue = ((System.Nullable<decimal>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._RotaValue = ((System.Nullable<decimal>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._PowerValue = ((System.Nullable<decimal>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._EnerValue = ((System.Nullable<decimal>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._TimeValue = ((System.Nullable<decimal>)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._Remark = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._RecordTime = this.GetDateTime(row, 11);
            }
            if ((false == row.IsNull(12))) {
                this._SeqIdx = ((System.Nullable<int>)(row[12]));
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
            if ((false == typeof(global::Mesnac.Entity.PmtMixingModel).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PmtMixingModel)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.PmtMixingModel)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _ModelCode = new NBear.Common.PropertyItem("ModelCode", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _TemCode = new NBear.Common.PropertyItem("TemCode", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _ActionCode = new NBear.Common.PropertyItem("ActionCode", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _TempValue = new NBear.Common.PropertyItem("TempValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _PresValue = new NBear.Common.PropertyItem("PresValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _RotaValue = new NBear.Common.PropertyItem("RotaValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _PowerValue = new NBear.Common.PropertyItem("PowerValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _EnerValue = new NBear.Common.PropertyItem("EnerValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _TimeValue = new NBear.Common.PropertyItem("TimeValue", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _RecordTime = new NBear.Common.PropertyItem("RecordTime", "Mesnac.Entity.PmtMixingModel");
            
            protected NBear.Common.PropertyItem _SeqIdx = new NBear.Common.PropertyItem("SeqIdx", "Mesnac.Entity.PmtMixingModel");
            
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
            
            public NBear.Common.PropertyItem ModelCode {
                get {
                    if ((aliasName == null)) {
                        return _ModelCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ModelCode", _ModelCode.EntityConfiguration, _ModelCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem TemCode {
                get {
                    if ((aliasName == null)) {
                        return _TemCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TemCode", _TemCode.EntityConfiguration, _TemCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ActionCode {
                get {
                    if ((aliasName == null)) {
                        return _ActionCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ActionCode", _ActionCode.EntityConfiguration, _ActionCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem TempValue {
                get {
                    if ((aliasName == null)) {
                        return _TempValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TempValue", _TempValue.EntityConfiguration, _TempValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PresValue {
                get {
                    if ((aliasName == null)) {
                        return _PresValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PresValue", _PresValue.EntityConfiguration, _PresValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RotaValue {
                get {
                    if ((aliasName == null)) {
                        return _RotaValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RotaValue", _RotaValue.EntityConfiguration, _RotaValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PowerValue {
                get {
                    if ((aliasName == null)) {
                        return _PowerValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PowerValue", _PowerValue.EntityConfiguration, _PowerValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem EnerValue {
                get {
                    if ((aliasName == null)) {
                        return _EnerValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("EnerValue", _EnerValue.EntityConfiguration, _EnerValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem TimeValue {
                get {
                    if ((aliasName == null)) {
                        return _TimeValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TimeValue", _TimeValue.EntityConfiguration, _TimeValue.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem RecordTime {
                get {
                    if ((aliasName == null)) {
                        return _RecordTime;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RecordTime", _RecordTime.EntityConfiguration, _RecordTime.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SeqIdx {
                get {
                    if ((aliasName == null)) {
                        return _SeqIdx;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SeqIdx", _SeqIdx.EntityConfiguration, _SeqIdx.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
