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
    public partial class Pmt_XLbagSetArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Pmt_XLbagSet> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Pmt_XLbagSet\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBat" +
        "chUpdate=\"false\" isRelation=\"false\" mappingName=\"Pmt_XLbagSet\" batchSize=\"10\">\r\n" +
        "  <Properties>\r\n    <Property name=\"Mater_Code\" type=\"System.String\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"Mater_Code\" mappingColumnType=\"System.String\" sqlType=\"char(13)\" isPrimaryKey" +
        "=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Mater_name\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"Mater_name\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(" +
        "40)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Set_Weight\" " +
        "type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" " +
        "isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" i" +
        "sLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyD" +
        "esc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Set_Weight\" mappingColumn" +
        "Type=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"Bag_Code\" type=\"System.String\" isInher" +
        "ited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQue" +
        "ry=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexP" +
        "roperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappin" +
        "gName=\"Bag_Code\" mappingColumnType=\"System.String\" sqlType=\"char(13)\" isPrimaryK" +
        "ey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Bag_price\" type=\"System.Nul" +
        "lable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"" +
        "false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false" +
        "\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSe" +
        "rializationIgnore=\"false\" mappingName=\"Bag_price\" mappingColumnType=\"System.Null" +
        "able`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\"" +
        " />\r\n    <Property name=\"Flag\" type=\"System.Nullable`1[System.Int32]\" isInherite" +
        "d=\"false\" sqlDefaultValue=\"(1)\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"Flag\" mappingColumnType=\"System.Nullable`1[System.Int3" +
        "2]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"Stock_flag\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" sqlDefau" +
        "ltValue=\"(1)\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"Stock_flag\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=" +
        "\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Workshop\" t" +
        "ype=\"System.Int32\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\"" +
        " isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRe" +
        "lationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializ" +
        "ationIgnore=\"false\" mappingName=\"Workshop\" mappingColumnType=\"System.Int32\" sqlT" +
        "ype=\"int\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n  </Properties>\r\n</EntityConf" +
        "iguration>")]
    public partial class Pmt_XLbagSet : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Pmt_XLbagSetEntityConfiguration;
        
        protected string _Mater_Code;
        
        protected string _Mater_name;
        
        protected global::System.Decimal? _Set_Weight;
        
        protected string _Bag_Code;
        
        protected global::System.Decimal? _Bag_price;
        
        protected global::System.Int32? _Flag;
        
        protected global::System.Int32? _Stock_flag;
        
        protected int _Workshop;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Pmt_XLbagSet left, global::Mesnac.Entity.Pmt_XLbagSet right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Pmt_XLbagSet left, global::Mesnac.Entity.Pmt_XLbagSet right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
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
        
        public global::System.Decimal? Set_Weight {
            get {
                return this._Set_Weight;
            }
            set {
                this.OnPropertyChanged("Set_Weight", this._Set_Weight, value);
                this._Set_Weight = value;
            }
        }
        
        public string Bag_Code {
            get {
                return this._Bag_Code;
            }
            set {
                this.OnPropertyChanged("Bag_Code", this._Bag_Code, value);
                this._Bag_Code = value;
            }
        }
        
        public global::System.Decimal? Bag_price {
            get {
                return this._Bag_price;
            }
            set {
                this.OnPropertyChanged("Bag_price", this._Bag_price, value);
                this._Bag_price = value;
            }
        }
        
        public global::System.Int32? Flag {
            get {
                return this._Flag;
            }
            set {
                this.OnPropertyChanged("Flag", this._Flag, value);
                this._Flag = value;
            }
        }
        
        public global::System.Int32? Stock_flag {
            get {
                return this._Stock_flag;
            }
            set {
                this.OnPropertyChanged("Stock_flag", this._Stock_flag, value);
                this._Stock_flag = value;
            }
        }
        
        public int Workshop {
            get {
                return this._Workshop;
            }
            set {
                this.OnPropertyChanged("Workshop", this._Workshop, value);
                this._Workshop = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Pmt_XLbagSet._Pmt_XLbagSetEntityConfiguration == null)) {
                Pmt_XLbagSet._Pmt_XLbagSetEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Pmt_XLbagSet");
            }
            return Pmt_XLbagSet._Pmt_XLbagSetEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Mater_Code,
                    this._Mater_name,
                    this._Set_Weight,
                    this._Bag_Code,
                    this._Bag_price,
                    this._Flag,
                    this._Stock_flag,
                    this._Workshop};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Mater_Code = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Mater_name = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Set_Weight = reader.GetDecimal(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Bag_Code = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Bag_price = reader.GetDecimal(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Flag = reader.GetInt32(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._Stock_flag = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._Workshop = reader.GetInt32(7);
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
            if ((false == row.IsNull(2))) {
                this._Set_Weight = ((System.Nullable<decimal>)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Bag_Code = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Bag_price = ((System.Nullable<decimal>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Flag = ((System.Nullable<int>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._Stock_flag = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._Workshop = ((int)(row[7]));
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
            if ((false == typeof(global::Mesnac.Entity.Pmt_XLbagSet).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((this.isAttached && ((global::Mesnac.Entity.Pmt_XLbagSet)(obj)).isAttached) 
                        && (this.Mater_Code == ((global::Mesnac.Entity.Pmt_XLbagSet)(obj)).Mater_Code)) 
                        && (this.Workshop == ((global::Mesnac.Entity.Pmt_XLbagSet)(obj)).Workshop));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Mater_Code = new NBear.Common.PropertyItem("Mater_Code", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Mater_name = new NBear.Common.PropertyItem("Mater_name", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Set_Weight = new NBear.Common.PropertyItem("Set_Weight", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Bag_Code = new NBear.Common.PropertyItem("Bag_Code", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Bag_price = new NBear.Common.PropertyItem("Bag_price", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Flag = new NBear.Common.PropertyItem("Flag", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Stock_flag = new NBear.Common.PropertyItem("Stock_flag", "Mesnac.Entity.Pmt_XLbagSet");
            
            protected NBear.Common.PropertyItem _Workshop = new NBear.Common.PropertyItem("Workshop", "Mesnac.Entity.Pmt_XLbagSet");
            
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
            
            public NBear.Common.PropertyItem Set_Weight {
                get {
                    if ((aliasName == null)) {
                        return _Set_Weight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Set_Weight", _Set_Weight.EntityConfiguration, _Set_Weight.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Bag_Code {
                get {
                    if ((aliasName == null)) {
                        return _Bag_Code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Bag_Code", _Bag_Code.EntityConfiguration, _Bag_Code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Bag_price {
                get {
                    if ((aliasName == null)) {
                        return _Bag_price;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Bag_price", _Bag_price.EntityConfiguration, _Bag_price.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Flag {
                get {
                    if ((aliasName == null)) {
                        return _Flag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Flag", _Flag.EntityConfiguration, _Flag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Stock_flag {
                get {
                    if ((aliasName == null)) {
                        return _Stock_flag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Stock_flag", _Stock_flag.EntityConfiguration, _Stock_flag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Workshop {
                get {
                    if ((aliasName == null)) {
                        return _Workshop;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Workshop", _Workshop.EntityConfiguration, _Workshop.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}