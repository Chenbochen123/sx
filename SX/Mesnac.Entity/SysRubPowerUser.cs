//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
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
    public partial class SysRubPowerUserArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.SysRubPowerUser> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.SysRubPowerUser\" isReadOnly=\"false\" isAutoPreLoad=\"false\" is" +
        "BatchUpdate=\"false\" isRelation=\"false\" mappingName=\"SysRubPowerUser\" batchSize=\"" +
        "10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited" +
        "=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNo" +
        "tNull=\"true\" />\r\n    <Property name=\"RubCode\" type=\"System.String\" isInherited=\"" +
        "false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fa" +
        "lse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPropert" +
        "y=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=" +
        "\"RubCode\" mappingColumnType=\"System.String\" sqlType=\"char(4)\" isPrimaryKey=\"fals" +
        "e\" isNotNull=\"false\" />\r\n    <Property name=\"RubType\" type=\"System.String\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"RubType\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryK" +
        "ey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"WorkBarcode\" type=\"System.S" +
        "tring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained" +
        "=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"f" +
        "alse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=" +
        "\"false\" mappingName=\"WorkBarcode\" mappingColumnType=\"System.String\" sqlType=\"var" +
        "char(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteF" +
        "lag\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=" +
        "\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fals" +
        "e\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isS" +
        "erializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.St" +
        "ring\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property" +
        " name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCom" +
        "poundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazy" +
        "Load=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"" +
        "false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"Sys" +
        "tem.String\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n " +
        " </Properties>\r\n</EntityConfiguration>")]
    public partial class SysRubPowerUser : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _SysRubPowerUserEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _RubCode;
        
        protected string _RubType;
        
        protected string _WorkBarcode;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.SysRubPowerUser left, global::Mesnac.Entity.SysRubPowerUser right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.SysRubPowerUser left, global::Mesnac.Entity.SysRubPowerUser right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string RubCode {
            get {
                return this._RubCode;
            }
            set {
                this.OnPropertyChanged("RubCode", this._RubCode, value);
                this._RubCode = value;
            }
        }
        
        public string RubType {
            get {
                return this._RubType;
            }
            set {
                this.OnPropertyChanged("RubType", this._RubType, value);
                this._RubType = value;
            }
        }
        
        public string WorkBarcode {
            get {
                return this._WorkBarcode;
            }
            set {
                this.OnPropertyChanged("WorkBarcode", this._WorkBarcode, value);
                this._WorkBarcode = value;
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
        
        public string Remark {
            get {
                return this._Remark;
            }
            set {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((SysRubPowerUser._SysRubPowerUserEntityConfiguration == null)) {
                SysRubPowerUser._SysRubPowerUserEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.SysRubPowerUser");
            }
            return SysRubPowerUser._SysRubPowerUserEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._RubCode,
                    this._RubType,
                    this._WorkBarcode,
                    this._DeleteFlag,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._RubCode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._RubType = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._WorkBarcode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._DeleteFlag = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Remark = reader.GetString(5);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._RubCode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._RubType = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._WorkBarcode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._DeleteFlag = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Remark = ((string)(row[5]));
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
            if ((false == typeof(global::Mesnac.Entity.SysRubPowerUser).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.SysRubPowerUser)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.SysRubPowerUser)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.SysRubPowerUser");
            
            protected NBear.Common.PropertyItem _RubCode = new NBear.Common.PropertyItem("RubCode", "Mesnac.Entity.SysRubPowerUser");
            
            protected NBear.Common.PropertyItem _RubType = new NBear.Common.PropertyItem("RubType", "Mesnac.Entity.SysRubPowerUser");
            
            protected NBear.Common.PropertyItem _WorkBarcode = new NBear.Common.PropertyItem("WorkBarcode", "Mesnac.Entity.SysRubPowerUser");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.SysRubPowerUser");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.SysRubPowerUser");
            
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
            
            public NBear.Common.PropertyItem RubCode {
                get {
                    if ((aliasName == null)) {
                        return _RubCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RubCode", _RubCode.EntityConfiguration, _RubCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RubType {
                get {
                    if ((aliasName == null)) {
                        return _RubType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RubType", _RubType.EntityConfiguration, _RubType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem WorkBarcode {
                get {
                    if ((aliasName == null)) {
                        return _WorkBarcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("WorkBarcode", _WorkBarcode.EntityConfiguration, _WorkBarcode.PropertyConfiguration, aliasName);
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
        }
    }
}
