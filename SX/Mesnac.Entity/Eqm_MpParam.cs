//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.42000
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
    public partial class Eqm_MpParamArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Eqm_MpParam> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Eqm_MpParam\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatc" +
        "hUpdate=\"false\" isRelation=\"false\" mappingName=\"Eqm_MpParam\" batchSize=\"10\">\r\n  " +
        "<Properties>\r\n    <Property name=\"Param_Type\" type=\"System.Int16\" isInherited=\"f" +
        "alse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fal" +
        "se\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty" +
        "=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"" +
        "Param_Type\" mappingColumnType=\"System.Int16\" sqlType=\"smallint\" isPrimaryKey=\"tr" +
        "ue\" isNotNull=\"true\" />\r\n    <Property name=\"Param_id\" type=\"System.String\" isIn" +
        "herited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" is" +
        "Query=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInd" +
        "exProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" map" +
        "pingName=\"Param_id\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimar" +
        "yKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Param_Name\" type=\"System.St" +
        "ring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"Param_Name\" mappingColumnType=\"System.String\" sqlType=\"nvarc" +
        "har(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityCon" +
        "figuration>")]
    public partial class Eqm_MpParam : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Eqm_MpParamEntityConfiguration;
        
        protected short _Param_Type;
        
        protected string _Param_id;
        
        protected string _Param_Name;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Eqm_MpParam left, global::Mesnac.Entity.Eqm_MpParam right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Eqm_MpParam left, global::Mesnac.Entity.Eqm_MpParam right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public short Param_Type {
            get {
                return this._Param_Type;
            }
            set {
                this.OnPropertyChanged("Param_Type", this._Param_Type, value);
                this._Param_Type = value;
            }
        }
        
        public string Param_id {
            get {
                return this._Param_id;
            }
            set {
                this.OnPropertyChanged("Param_id", this._Param_id, value);
                this._Param_id = value;
            }
        }
        
        public string Param_Name {
            get {
                return this._Param_Name;
            }
            set {
                this.OnPropertyChanged("Param_Name", this._Param_Name, value);
                this._Param_Name = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Eqm_MpParam._Eqm_MpParamEntityConfiguration == null)) {
                Eqm_MpParam._Eqm_MpParamEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Eqm_MpParam");
            }
            return Eqm_MpParam._Eqm_MpParamEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Param_Type,
                    this._Param_id,
                    this._Param_Name};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Param_Type = reader.GetInt16(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Param_id = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Param_Name = reader.GetString(2);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Param_Type = ((short)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Param_id = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Param_Name = ((string)(row[2]));
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
            if ((false == typeof(global::Mesnac.Entity.Eqm_MpParam).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((this.isAttached && ((global::Mesnac.Entity.Eqm_MpParam)(obj)).isAttached) 
                        && (this.Param_Type == ((global::Mesnac.Entity.Eqm_MpParam)(obj)).Param_Type)) 
                        && (this.Param_id == ((global::Mesnac.Entity.Eqm_MpParam)(obj)).Param_id));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Param_Type = new NBear.Common.PropertyItem("Param_Type", "Mesnac.Entity.Eqm_MpParam");
            
            protected NBear.Common.PropertyItem _Param_id = new NBear.Common.PropertyItem("Param_id", "Mesnac.Entity.Eqm_MpParam");
            
            protected NBear.Common.PropertyItem _Param_Name = new NBear.Common.PropertyItem("Param_Name", "Mesnac.Entity.Eqm_MpParam");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Param_Type {
                get {
                    if ((aliasName == null)) {
                        return _Param_Type;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Param_Type", _Param_Type.EntityConfiguration, _Param_Type.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Param_id {
                get {
                    if ((aliasName == null)) {
                        return _Param_id;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Param_id", _Param_id.EntityConfiguration, _Param_id.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Param_Name {
                get {
                    if ((aliasName == null)) {
                        return _Param_Name;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Param_Name", _Param_Name.EntityConfiguration, _Param_Name.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}