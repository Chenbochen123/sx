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
    public partial class PptAlarmArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PptAlarm> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PptAlarm\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatchUp" +
        "date=\"false\" isRelation=\"false\" mappingName=\"PptAlarm\" batchSize=\"10\">\r\n  <Prope" +
        "rties>\r\n    <Property name=\"ObjId\" type=\"System.Int32\" isInherited=\"false\" isRea" +
        "dOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriend" +
        "Key=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isI" +
        "ndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ObjId\" mappi" +
        "ngColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"true\" /" +
        ">\r\n    <Property name=\"Barcode\" type=\"System.String\" isInherited=\"false\" isReadO" +
        "nly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendK" +
        "ey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIn" +
        "dexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Barcode\" mapp" +
        "ingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" isNotN" +
        "ull=\"false\" />\r\n    <Property name=\"AlarmStr\" type=\"System.String\" isInherited=\"" +
        "false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fa" +
        "lse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPropert" +
        "y=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=" +
        "\"AlarmStr\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(200)\" isPrimaryKe" +
        "y=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"OperTime\" type=\"System.Nulla" +
        "ble`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"OperTime\" mappingColumnType=\"System.Nullab" +
        "le`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\"" +
        " />\r\n    <Property name=\"EquipCode\" type=\"System.String\" isInherited=\"false\" isR" +
        "eadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFri" +
        "endKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" " +
        "isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"EquipCode" +
        "\" mappingColumnType=\"System.String\" sqlType=\"char(5)\" isPrimaryKey=\"false\" isNot" +
        "Null=\"false\" />\r\n    <Property name=\"Remark\" type=\"System.String\" isInherited=\"f" +
        "alse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fal" +
        "se\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty" +
        "=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"" +
        "Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"" +
        "false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" " +
        "isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguratio" +
        "n>")]
    public partial class PptAlarm : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PptAlarmEntityConfiguration;
        
        protected int _ObjId;
        
        protected string _Barcode;
        
        protected string _AlarmStr;
        
        protected global::System.DateTime? _OperTime;
        
        protected string _EquipCode;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PptAlarm left, global::Mesnac.Entity.PptAlarm right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PptAlarm left, global::Mesnac.Entity.PptAlarm right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjId {
            get {
                return this._ObjId;
            }
            set {
                this.OnPropertyChanged("ObjId", this._ObjId, value);
                this._ObjId = value;
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
        
        public string AlarmStr {
            get {
                return this._AlarmStr;
            }
            set {
                this.OnPropertyChanged("AlarmStr", this._AlarmStr, value);
                this._AlarmStr = value;
            }
        }
        
        public global::System.DateTime? OperTime {
            get {
                return this._OperTime;
            }
            set {
                this.OnPropertyChanged("OperTime", this._OperTime, value);
                this._OperTime = value;
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
            if ((PptAlarm._PptAlarmEntityConfiguration == null)) {
                PptAlarm._PptAlarmEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PptAlarm");
            }
            return PptAlarm._PptAlarmEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjId,
                    this._Barcode,
                    this._AlarmStr,
                    this._OperTime,
                    this._EquipCode,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Barcode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._AlarmStr = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._OperTime = this.GetDateTime(reader, 3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._EquipCode = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Remark = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._DeleteFlag = reader.GetString(6);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Barcode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._AlarmStr = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._OperTime = this.GetDateTime(row, 3);
            }
            if ((false == row.IsNull(4))) {
                this._EquipCode = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Remark = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._DeleteFlag = ((string)(row[6]));
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
            if ((false == typeof(global::Mesnac.Entity.PptAlarm).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PptAlarm)(obj)).isAttached) 
                        && (this.ObjId == ((global::Mesnac.Entity.PptAlarm)(obj)).ObjId));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjId = new NBear.Common.PropertyItem("ObjId", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _AlarmStr = new NBear.Common.PropertyItem("AlarmStr", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _OperTime = new NBear.Common.PropertyItem("OperTime", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _EquipCode = new NBear.Common.PropertyItem("EquipCode", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PptAlarm");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.PptAlarm");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem ObjId {
                get {
                    if ((aliasName == null)) {
                        return _ObjId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ObjId", _ObjId.EntityConfiguration, _ObjId.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem AlarmStr {
                get {
                    if ((aliasName == null)) {
                        return _AlarmStr;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AlarmStr", _AlarmStr.EntityConfiguration, _AlarmStr.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OperTime {
                get {
                    if ((aliasName == null)) {
                        return _OperTime;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OperTime", _OperTime.EntityConfiguration, _OperTime.PropertyConfiguration, aliasName);
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
