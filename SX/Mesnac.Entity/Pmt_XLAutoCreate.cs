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
    public partial class Pmt_XLAutoCreateArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Pmt_XLAutoCreate> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<EntityConfiguration xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" name=""Mesnac.Entity.Pmt_XLAutoCreate"" isReadOnly=""false"" isAutoPreLoad=""false"" isBatchUpdate=""false"" isRelation=""false"" mappingName=""Pmt_XLAutoCreate"" batchSize=""10"">
  <Properties>
    <Property name=""Mater_Code"" type=""System.String"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""Mater_Code"" mappingColumnType=""System.String"" sqlType=""char(13)"" isPrimaryKey=""true"" isNotNull=""true"" />
    <Property name=""Mater_name"" type=""System.String"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""Mater_name"" mappingColumnType=""System.String"" sqlType=""nvarchar(50)"" isPrimaryKey=""false"" isNotNull=""false"" />
  </Properties>
</EntityConfiguration>")]
    public partial class Pmt_XLAutoCreate : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Pmt_XLAutoCreateEntityConfiguration;
        
        protected string _Mater_Code;
        
        protected string _Mater_name;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Pmt_XLAutoCreate left, global::Mesnac.Entity.Pmt_XLAutoCreate right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Pmt_XLAutoCreate left, global::Mesnac.Entity.Pmt_XLAutoCreate right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string Mater_Code {
            get {
                return this._Mater_Code;
            }
            set {
                this.OnPropertyChanged("Mater_Code", this._Mater_Code, value);
                this._Mater_Code = value;
            }
        }
        
        public string Mater_name {
            get {
                return this._Mater_name;
            }
            set {
                this.OnPropertyChanged("Mater_name", this._Mater_name, value);
                this._Mater_name = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Pmt_XLAutoCreate._Pmt_XLAutoCreateEntityConfiguration == null)) {
                Pmt_XLAutoCreate._Pmt_XLAutoCreateEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Pmt_XLAutoCreate");
            }
            return Pmt_XLAutoCreate._Pmt_XLAutoCreateEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Mater_Code,
                    this._Mater_name};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Mater_Code = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Mater_name = reader.GetString(1);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Mater_Code = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Mater_name = ((string)(row[1]));
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
            if ((false == typeof(global::Mesnac.Entity.Pmt_XLAutoCreate).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Pmt_XLAutoCreate)(obj)).isAttached) 
                        && (this.Mater_Code == ((global::Mesnac.Entity.Pmt_XLAutoCreate)(obj)).Mater_Code));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Mater_Code = new NBear.Common.PropertyItem("Mater_Code", "Mesnac.Entity.Pmt_XLAutoCreate");
            
            protected NBear.Common.PropertyItem _Mater_name = new NBear.Common.PropertyItem("Mater_name", "Mesnac.Entity.Pmt_XLAutoCreate");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Mater_Code {
                get {
                    if ((aliasName == null)) {
                        return _Mater_Code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mater_Code", _Mater_Code.EntityConfiguration, _Mater_Code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mater_name {
                get {
                    if ((aliasName == null)) {
                        return _Mater_name;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mater_name", _Mater_name.EntityConfiguration, _Mater_name.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}