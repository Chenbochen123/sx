//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
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
    public partial class Eqm_KongTiaoRecordArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Eqm_KongTiaoRecord> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Eqm_KongTiaoRecord\" isReadOnly=\"false\" isAutoPreLoad=\"false\"" +
        " isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"Eqm_KongTiaoRecord\" batch" +
        "Size=\"10\">\r\n  <Properties>\r\n    <Property name=\"Serialid\" type=\"System.Int32\" is" +
        "Inherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"Serialid\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey" +
        "=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"INO\" type=\"System.Nullable`1[Sy" +
        "stem.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCon" +
        "tained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelation" +
        "Key=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationI" +
        "gnore=\"false\" mappingName=\"INO\" mappingColumnType=\"System.Nullable`1[System.Int3" +
        "2]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"EquipNO\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"EquipNO\" mappingColumnType=\"System." +
        "String\" sqlType=\"varchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pr" +
        "operty name=\"PosName\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false" +
        "\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\"" +
        " isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropert" +
        "yDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"PosName\" mappingColumnT" +
        "ype=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"Lastdate\" type=\"System.Nullable`1[System.DateTime]\" is" +
        "Inherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" " +
        "isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isI" +
        "ndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" m" +
        "appingName=\"Lastdate\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sql" +
        "Type=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"L" +
        "astfac\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUn" +
        "it=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"f" +
        "alse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" " +
        "isSerializationIgnore=\"false\" mappingName=\"Lastfac\" mappingColumnType=\"System.St" +
        "ring\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"WX_reason\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fals" +
        "e\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false" +
        "\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProper" +
        "tyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"WX_reason\" mappingColu" +
        "mnType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"fa" +
        "lse\" />\r\n    <Property name=\"WX_money\" type=\"System.Nullable`1[System.Decimal]\" " +
        "isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false" +
        "\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" i" +
        "sIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\"" +
        " mappingName=\"WX_money\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sq" +
        "lType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"P" +
        "seron\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"Pseron\" mappingColumnType=\"System.Stri" +
        "ng\" sqlType=\"varchar(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Proper" +
        "ty name=\"Memo\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCom" +
        "poundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazy" +
        "Load=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"" +
        "false\" isSerializationIgnore=\"false\" mappingName=\"Memo\" mappingColumnType=\"Syste" +
        "m.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </" +
        "Properties>\r\n</EntityConfiguration>")]
    public partial class Eqm_KongTiaoRecord : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Eqm_KongTiaoRecordEntityConfiguration;
        
        protected int _Serialid;
        
        protected global::System.Int32? _INO;
        
        protected string _EquipNO;
        
        protected string _PosName;
        
        protected global::System.DateTime? _Lastdate;
        
        protected string _Lastfac;
        
        protected string _WX_reason;
        
        protected global::System.Decimal? _WX_money;
        
        protected string _Pseron;
        
        protected string _Memo;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Eqm_KongTiaoRecord left, global::Mesnac.Entity.Eqm_KongTiaoRecord right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Eqm_KongTiaoRecord left, global::Mesnac.Entity.Eqm_KongTiaoRecord right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int Serialid {
            get {
                return this._Serialid;
            }
            set {
                this.OnPropertyChanged("Serialid", this._Serialid, value);
                this._Serialid = value;
            }
        }
        
        public global::System.Int32? INO {
            get {
                return this._INO;
            }
            set {
                this.OnPropertyChanged("INO", this._INO, value);
                this._INO = value;
            }
        }
        
        public string EquipNO {
            get {
                return this._EquipNO;
            }
            set {
                this.OnPropertyChanged("EquipNO", this._EquipNO, value);
                this._EquipNO = value;
            }
        }
        
        public string PosName {
            get {
                return this._PosName;
            }
            set {
                this.OnPropertyChanged("PosName", this._PosName, value);
                this._PosName = value;
            }
        }
        
        public global::System.DateTime? Lastdate {
            get {
                return this._Lastdate;
            }
            set {
                this.OnPropertyChanged("Lastdate", this._Lastdate, value);
                this._Lastdate = value;
            }
        }
        
        public string Lastfac {
            get {
                return this._Lastfac;
            }
            set {
                this.OnPropertyChanged("Lastfac", this._Lastfac, value);
                this._Lastfac = value;
            }
        }
        
        public string WX_reason {
            get {
                return this._WX_reason;
            }
            set {
                this.OnPropertyChanged("WX_reason", this._WX_reason, value);
                this._WX_reason = value;
            }
        }
        
        public global::System.Decimal? WX_money {
            get {
                return this._WX_money;
            }
            set {
                this.OnPropertyChanged("WX_money", this._WX_money, value);
                this._WX_money = value;
            }
        }
        
        public string Pseron {
            get {
                return this._Pseron;
            }
            set {
                this.OnPropertyChanged("Pseron", this._Pseron, value);
                this._Pseron = value;
            }
        }
        
        public string Memo {
            get {
                return this._Memo;
            }
            set {
                this.OnPropertyChanged("Memo", this._Memo, value);
                this._Memo = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Eqm_KongTiaoRecord._Eqm_KongTiaoRecordEntityConfiguration == null)) {
                Eqm_KongTiaoRecord._Eqm_KongTiaoRecordEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Eqm_KongTiaoRecord");
            }
            return Eqm_KongTiaoRecord._Eqm_KongTiaoRecordEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Serialid,
                    this._INO,
                    this._EquipNO,
                    this._PosName,
                    this._Lastdate,
                    this._Lastfac,
                    this._WX_reason,
                    this._WX_money,
                    this._Pseron,
                    this._Memo};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Serialid = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._INO = reader.GetInt32(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._EquipNO = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._PosName = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Lastdate = this.GetDateTime(reader, 4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Lastfac = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._WX_reason = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._WX_money = reader.GetDecimal(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._Pseron = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._Memo = reader.GetString(9);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Serialid = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._INO = ((System.Nullable<int>)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._EquipNO = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._PosName = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Lastdate = this.GetDateTime(row, 4);
            }
            if ((false == row.IsNull(5))) {
                this._Lastfac = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._WX_reason = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._WX_money = ((System.Nullable<decimal>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._Pseron = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._Memo = ((string)(row[9]));
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
            if ((false == typeof(global::Mesnac.Entity.Eqm_KongTiaoRecord).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Eqm_KongTiaoRecord)(obj)).isAttached) 
                        && (this.Serialid == ((global::Mesnac.Entity.Eqm_KongTiaoRecord)(obj)).Serialid));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Serialid = new NBear.Common.PropertyItem("Serialid", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _INO = new NBear.Common.PropertyItem("INO", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _EquipNO = new NBear.Common.PropertyItem("EquipNO", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _PosName = new NBear.Common.PropertyItem("PosName", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _Lastdate = new NBear.Common.PropertyItem("Lastdate", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _Lastfac = new NBear.Common.PropertyItem("Lastfac", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _WX_reason = new NBear.Common.PropertyItem("WX_reason", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _WX_money = new NBear.Common.PropertyItem("WX_money", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _Pseron = new NBear.Common.PropertyItem("Pseron", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            protected NBear.Common.PropertyItem _Memo = new NBear.Common.PropertyItem("Memo", "Mesnac.Entity.Eqm_KongTiaoRecord");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Serialid {
                get {
                    if ((aliasName == null)) {
                        return _Serialid;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Serialid", _Serialid.EntityConfiguration, _Serialid.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem INO {
                get {
                    if ((aliasName == null)) {
                        return _INO;
                    }
                    else {
                        return new NBear.Common.PropertyItem("INO", _INO.EntityConfiguration, _INO.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem EquipNO {
                get {
                    if ((aliasName == null)) {
                        return _EquipNO;
                    }
                    else {
                        return new NBear.Common.PropertyItem("EquipNO", _EquipNO.EntityConfiguration, _EquipNO.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PosName {
                get {
                    if ((aliasName == null)) {
                        return _PosName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PosName", _PosName.EntityConfiguration, _PosName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Lastdate {
                get {
                    if ((aliasName == null)) {
                        return _Lastdate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Lastdate", _Lastdate.EntityConfiguration, _Lastdate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Lastfac {
                get {
                    if ((aliasName == null)) {
                        return _Lastfac;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Lastfac", _Lastfac.EntityConfiguration, _Lastfac.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem WX_reason {
                get {
                    if ((aliasName == null)) {
                        return _WX_reason;
                    }
                    else {
                        return new NBear.Common.PropertyItem("WX_reason", _WX_reason.EntityConfiguration, _WX_reason.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem WX_money {
                get {
                    if ((aliasName == null)) {
                        return _WX_money;
                    }
                    else {
                        return new NBear.Common.PropertyItem("WX_money", _WX_money.EntityConfiguration, _WX_money.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Pseron {
                get {
                    if ((aliasName == null)) {
                        return _Pseron;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Pseron", _Pseron.EntityConfiguration, _Pseron.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Memo {
                get {
                    if ((aliasName == null)) {
                        return _Memo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Memo", _Memo.EntityConfiguration, _Memo.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
