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
    public partial class PptProcedureArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PptProcedure> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<EntityConfiguration xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""Mesnac.Entity.PptProcedure"" isReadOnly=""false"" isAutoPreLoad=""false"" isBatchUpdate=""false"" isRelation=""false"" mappingName=""PptProcedure"" batchSize=""10"">
  <Properties>
    <Property name=""ObjID"" type=""System.Int32"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""ObjID"" mappingColumnType=""System.Int32"" sqlType=""int"" isPrimaryKey=""true"" isNotNull=""true"" />
    <Property name=""ProcedureName"" type=""System.String"" isInherited=""false"" isReadOnly=""false"" isCompoundUnit=""false"" isContained=""false"" isQuery=""false"" isFriendKey=""false"" isLazyLoad=""false"" isRelationKey=""false"" isIndexProperty=""false"" isIndexPropertyDesc=""false"" isSerializationIgnore=""false"" mappingName=""ProcedureName"" mappingColumnType=""System.String"" sqlType=""varchar(20)"" isPrimaryKey=""false"" isNotNull=""false"" />
  </Properties>
</EntityConfiguration>")]
    public partial class PptProcedure : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PptProcedureEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _ProcedureName;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PptProcedure left, global::Mesnac.Entity.PptProcedure right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PptProcedure left, global::Mesnac.Entity.PptProcedure right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string ProcedureName {
            get {
                return this._ProcedureName;
            }
            set {
                this.OnPropertyChanged("ProcedureName", this._ProcedureName, value);
                this._ProcedureName = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PptProcedure._PptProcedureEntityConfiguration == null)) {
                PptProcedure._PptProcedureEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PptProcedure");
            }
            return PptProcedure._PptProcedureEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._ProcedureName};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._ProcedureName = reader.GetString(1);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._ProcedureName = ((string)(row[1]));
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
            if ((false == typeof(global::Mesnac.Entity.PptProcedure).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PptProcedure)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.PptProcedure)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.PptProcedure");
            
            protected NBear.Common.PropertyItem _ProcedureName = new NBear.Common.PropertyItem("ProcedureName", "Mesnac.Entity.PptProcedure");
            
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
            
            public NBear.Common.PropertyItem ProcedureName {
                get {
                    if ((aliasName == null)) {
                        return _ProcedureName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ProcedureName", _ProcedureName.EntityConfiguration, _ProcedureName.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
