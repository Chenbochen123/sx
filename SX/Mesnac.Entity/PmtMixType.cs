//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.296
//
//     �Դ��ļ��ĸ��Ŀ��ܻᵼ�²���ȷ����Ϊ���������
//     �������ɴ��룬��Щ���Ľ��ᶪʧ��
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity {
    using System;
    using System.Xml.Serialization;
    using NBear.Common;
    
    
    [System.SerializableAttribute()]
    public partial class PmtMixTypeArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PmtMixType> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PmtMixType\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatch" +
        "Update=\"false\" isRelation=\"false\" mappingName=\"PmtMixType\" batchSize=\"10\">\r\n  <P" +
        "roperties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"false\" i" +
        "sReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isF" +
        "riendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false" +
        "\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ObjID\" " +
        "mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"tr" +
        "ue\" />\r\n    <Property name=\"MixName\" type=\"System.String\" isInherited=\"false\" is" +
        "ReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFr" +
        "iendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\"" +
        " isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"MixName\"" +
        " mappingColumnType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" i" +
        "sNotNull=\"false\" />\r\n    <Property name=\"MixCubage\" type=\"System.Nullable`1[Syst" +
        "em.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCon" +
        "tained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelation" +
        "Key=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationI" +
        "gnore=\"false\" mappingName=\"MixCubage\" mappingColumnType=\"System.Nullable`1[Syste" +
        "m.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pr" +
        "operty name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnTyp" +
        "e=\"System.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\"" +
        " />\r\n    <Property name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" sq" +
        "lDefaultValue=\"\'0\'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false" +
        "\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" i" +
        "sIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\"" +
        " mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" is" +
        "PrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>" +
        "")]
    public partial class PmtMixType : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PmtMixTypeEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _MixName;
        
        protected global::System.Decimal? _MixCubage;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PmtMixType left, global::Mesnac.Entity.PmtMixType right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PmtMixType left, global::Mesnac.Entity.PmtMixType right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string MixName {
            get {
                return this._MixName;
            }
            set {
                this.OnPropertyChanged("MixName", this._MixName, value);
                this._MixName = value;
            }
        }
        
        public global::System.Decimal? MixCubage {
            get {
                return this._MixCubage;
            }
            set {
                this.OnPropertyChanged("MixCubage", this._MixCubage, value);
                this._MixCubage = value;
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
            if ((PmtMixType._PmtMixTypeEntityConfiguration == null)) {
                PmtMixType._PmtMixTypeEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PmtMixType");
            }
            return PmtMixType._PmtMixTypeEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._MixName,
                    this._MixCubage,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._MixName = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._MixCubage = reader.GetDecimal(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Remark = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._DeleteFlag = reader.GetString(4);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._MixName = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._MixCubage = ((System.Nullable<decimal>)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Remark = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._DeleteFlag = ((string)(row[4]));
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
            if ((false == typeof(global::Mesnac.Entity.PmtMixType).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PmtMixType)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.PmtMixType)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.PmtMixType");
            
            protected NBear.Common.PropertyItem _MixName = new NBear.Common.PropertyItem("MixName", "Mesnac.Entity.PmtMixType");
            
            protected NBear.Common.PropertyItem _MixCubage = new NBear.Common.PropertyItem("MixCubage", "Mesnac.Entity.PmtMixType");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PmtMixType");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.PmtMixType");
            
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
            
            public NBear.Common.PropertyItem MixName {
                get {
                    if ((aliasName == null)) {
                        return _MixName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MixName", _MixName.EntityConfiguration, _MixName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem MixCubage {
                get {
                    if ((aliasName == null)) {
                        return _MixCubage;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MixCubage", _MixCubage.EntityConfiguration, _MixCubage.PropertyConfiguration, aliasName);
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