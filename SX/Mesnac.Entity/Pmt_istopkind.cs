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
    public partial class Pmt_istopkindArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Pmt_istopkind> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<EntityConfiguration xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" name=""Mesnac.Entity.Pmt_istopkind"" isReadOnly=""false"" isAutoPreLoad=""false"" isBatchUpdate=""false"" isRelation=""false"" mappingName=""Pmt_istopkind"" batchSize=""10"">
  <Properties>
    <Property name=""Ikind_Code"" type=""System.String"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""Ikind_Code"" mappingColumnType=""System.String"" sqlType=""char(4)"" isPrimaryKey=""true"" isNotNull=""true"" />
    <Property name=""Ikind_name"" type=""System.String"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""Ikind_name"" mappingColumnType=""System.String"" sqlType=""nvarchar(30)"" isPrimaryKey=""false"" isNotNull=""false"" />
  </Properties>
</EntityConfiguration>")]
    public partial class Pmt_istopkind : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Pmt_istopkindEntityConfiguration;
        
        protected string _Ikind_Code;
        
        protected string _Ikind_name;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Pmt_istopkind left, global::Mesnac.Entity.Pmt_istopkind right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Pmt_istopkind left, global::Mesnac.Entity.Pmt_istopkind right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string Ikind_Code {
            get {
                return this._Ikind_Code;
            }
            set {
                this.OnPropertyChanged("Ikind_Code", this._Ikind_Code, value);
                this._Ikind_Code = value;
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
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Pmt_istopkind._Pmt_istopkindEntityConfiguration == null)) {
                Pmt_istopkind._Pmt_istopkindEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Pmt_istopkind");
            }
            return Pmt_istopkind._Pmt_istopkindEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Ikind_Code,
                    this._Ikind_name};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Ikind_Code = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Ikind_name = reader.GetString(1);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Ikind_Code = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Ikind_name = ((string)(row[1]));
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
            if ((false == typeof(global::Mesnac.Entity.Pmt_istopkind).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Pmt_istopkind)(obj)).isAttached) 
                        && (this.Ikind_Code == ((global::Mesnac.Entity.Pmt_istopkind)(obj)).Ikind_Code));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Ikind_Code = new NBear.Common.PropertyItem("Ikind_Code", "Mesnac.Entity.Pmt_istopkind");
            
            protected NBear.Common.PropertyItem _Ikind_name = new NBear.Common.PropertyItem("Ikind_name", "Mesnac.Entity.Pmt_istopkind");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Ikind_Code {
                get {
                    if ((aliasName == null)) {
                        return _Ikind_Code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Ikind_Code", _Ikind_Code.EntityConfiguration, _Ikind_Code.PropertyConfiguration, aliasName);
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
        }
    }
}
