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
    public partial class Pmt_ikindArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Pmt_ikind> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Pmt_ikind\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatchU" +
        "pdate=\"false\" isRelation=\"false\" mappingName=\"Pmt_ikind\" batchSize=\"10\">\r\n  <Pro" +
        "perties>\r\n    <Property name=\"Mkind_code\" type=\"System.String\" isInherited=\"fals" +
        "e\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Mki" +
        "nd_code\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false" +
        "\" isNotNull=\"false\" />\r\n    <Property name=\"Ikind_code\" type=\"System.String\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"Ikind_code\" mappingColumnType=\"System.String\" sqlType=\"char(2)\" isPri" +
        "maryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Ikind_name\" type=\"Syst" +
        "em.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isConta" +
        "ined=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKe" +
        "y=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgn" +
        "ore=\"false\" mappingName=\"Ikind_name\" mappingColumnType=\"System.String\" sqlType=\"" +
        "varchar(16)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Mem_" +
        "note\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"Mem_note\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"varchar(30)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Proper" +
        "ties>\r\n</EntityConfiguration>")]
    public partial class Pmt_ikind : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Pmt_ikindEntityConfiguration;
        
        protected string _Mkind_code;
        
        protected string _Ikind_code;
        
        protected string _Ikind_name;
        
        protected string _Mem_note;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Pmt_ikind left, global::Mesnac.Entity.Pmt_ikind right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Pmt_ikind left, global::Mesnac.Entity.Pmt_ikind right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string Mkind_code {
            get {
                return this._Mkind_code;
            }
            set {
                this.OnPropertyChanged("Mkind_code", this._Mkind_code, value);
                this._Mkind_code = value;
            }
        }
        
        public string Ikind_code {
            get {
                return this._Ikind_code;
            }
            set {
                this.OnPropertyChanged("Ikind_code", this._Ikind_code, value);
                this._Ikind_code = value;
            }
        }
        
        public string Ikind_name {
            get {
                return this._Ikind_name;
            }
            set {
                this.OnPropertyChanged("Ikind_name", this._Ikind_name, value);
                this._Ikind_name = value;
            }
        }
        
        public string Mem_note {
            get {
                return this._Mem_note;
            }
            set {
                this.OnPropertyChanged("Mem_note", this._Mem_note, value);
                this._Mem_note = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Pmt_ikind._Pmt_ikindEntityConfiguration == null)) {
                Pmt_ikind._Pmt_ikindEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Pmt_ikind");
            }
            return Pmt_ikind._Pmt_ikindEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Mkind_code,
                    this._Ikind_code,
                    this._Ikind_name,
                    this._Mem_note};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Mkind_code = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Ikind_code = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Ikind_name = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Mem_note = reader.GetString(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Mkind_code = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Ikind_code = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Ikind_name = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Mem_note = ((string)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.Pmt_ikind).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Pmt_ikind)(obj)).isAttached) 
                        && base.Equals(obj));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Mkind_code = new NBear.Common.PropertyItem("Mkind_code", "Mesnac.Entity.Pmt_ikind");
            
            protected NBear.Common.PropertyItem _Ikind_code = new NBear.Common.PropertyItem("Ikind_code", "Mesnac.Entity.Pmt_ikind");
            
            protected NBear.Common.PropertyItem _Ikind_name = new NBear.Common.PropertyItem("Ikind_name", "Mesnac.Entity.Pmt_ikind");
            
            protected NBear.Common.PropertyItem _Mem_note = new NBear.Common.PropertyItem("Mem_note", "Mesnac.Entity.Pmt_ikind");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Mkind_code {
                get {
                    if ((aliasName == null)) {
                        return _Mkind_code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mkind_code", _Mkind_code.EntityConfiguration, _Mkind_code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Ikind_code {
                get {
                    if ((aliasName == null)) {
                        return _Ikind_code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Ikind_code", _Ikind_code.EntityConfiguration, _Ikind_code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Ikind_name {
                get {
                    if ((aliasName == null)) {
                        return _Ikind_name;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Ikind_name", _Ikind_name.EntityConfiguration, _Ikind_name.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mem_note {
                get {
                    if ((aliasName == null)) {
                        return _Mem_note;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mem_note", _Mem_note.EntityConfiguration, _Mem_note.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}