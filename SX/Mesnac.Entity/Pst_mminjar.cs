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
    public partial class Pst_mminjarArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Pst_mminjar> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Pst_mminjar\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatc" +
        "hUpdate=\"false\" isRelation=\"false\" mappingName=\"Pst_mminjar\" batchSize=\"10\">\r\n  " +
        "<Properties>\r\n    <Property name=\"Jar_id\" type=\"System.Int64\" isInherited=\"false" +
        "\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" i" +
        "sFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fal" +
        "se\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Jar_i" +
        "d\" mappingColumnType=\"System.Int64\" sqlType=\"bigint\" isPrimaryKey=\"true\" isNotNu" +
        "ll=\"true\" />\r\n    <Property name=\"Mater_barcode\" type=\"System.String\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"Mater_barcode\" mappingColumnType=\"System.String\" sqlType=\"varchar(15)\" isPri" +
        "maryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Stock_date\" type=\"Syst" +
        "em.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isConta" +
        "ined=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKe" +
        "y=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgn" +
        "ore=\"false\" mappingName=\"Stock_date\" mappingColumnType=\"System.String\" sqlType=\"" +
        "char(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Mater_c" +
        "ode\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"\' \'\" isReadOnly=\"" +
        "false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"f" +
        "alse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPr" +
        "opertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Mater_code\" mappin" +
        "gColumnType=\"System.String\" sqlType=\"char(13)\" isPrimaryKey=\"false\" isNotNull=\"f" +
        "alse\" />\r\n    <Property name=\"Equip_code\" type=\"System.String\" isInherited=\"fals" +
        "e\" sqlDefaultValue=\"\' \'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"" +
        "false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fal" +
        "se\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"f" +
        "alse\" mappingName=\"Equip_code\" mappingColumnType=\"System.String\" sqlType=\"char(5" +
        ")\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Shift_id\" type" +
        "=\"System.Nullable`1[System.Int16]\" isInherited=\"false\" isReadOnly=\"false\" isComp" +
        "oundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyL" +
        "oad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"f" +
        "alse\" isSerializationIgnore=\"false\" mappingName=\"Shift_id\" mappingColumnType=\"Sy" +
        "stem.Nullable`1[System.Int16]\" sqlType=\"smallint\" isPrimaryKey=\"false\" isNotNull" +
        "=\"false\" />\r\n    <Property name=\"In_Time\" type=\"System.String\" isInherited=\"fals" +
        "e\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"In_" +
        "Time\" mappingColumnType=\"System.String\" sqlType=\"char(19)\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"Real_num\" type=\"System.Nullable`1[Syst" +
        "em.Int32]\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\" isCompou" +
        "ndUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoa" +
        "d=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"fal" +
        "se\" isSerializationIgnore=\"false\" mappingName=\"Real_num\" mappingColumnType=\"Syst" +
        "em.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"Real_weight\" type=\"System.Nullable`1[System.Decimal]\" " +
        "isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false" +
        "\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" i" +
        "sIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\"" +
        " mappingName=\"Real_weight\" mappingColumnType=\"System.Nullable`1[System.Decimal]\"" +
        " sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name" +
        "=\"Handle_name\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCom" +
        "poundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazy" +
        "Load=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"" +
        "false\" isSerializationIgnore=\"false\" mappingName=\"Handle_name\" mappingColumnType" +
        "=\"System.String\" sqlType=\"char(5)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
        "  <Property name=\"Used_weigh\" type=\"System.Nullable`1[System.Decimal]\" isInherit" +
        "ed=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery" +
        "=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPro" +
        "perty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingN" +
        "ame=\"Used_weigh\" mappingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"" +
        "decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Used_Fla" +
        "g\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"Used_Flag\" mappingColumnType=\"System.Strin" +
        "g\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property na" +
        "me=\"Clear_Flag\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCo" +
        "mpoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLaz" +
        "yLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=" +
        "\"false\" isSerializationIgnore=\"false\" mappingName=\"Clear_Flag\" mappingColumnType" +
        "=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
        "  <Property name=\"Shift_Classid\" type=\"System.Nullable`1[System.Int32]\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"Shift_Classid\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType" +
        "=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Ware_num\" " +
        "type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" is" +
        "CompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isL" +
        "azyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDes" +
        "c=\"false\" isSerializationIgnore=\"false\" mappingName=\"Ware_num\" mappingColumnType" +
        "=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"Ware_type\" type=\"System.String\" isInherited=\"fal" +
        "se\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false" +
        "\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"" +
        "false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Wa" +
        "re_type\" mappingColumnType=\"System.String\" sqlType=\"varchar(20)\" isPrimaryKey=\"f" +
        "alse\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class Pst_mminjar : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Pst_mminjarEntityConfiguration;
        
        protected long _Jar_id;
        
        protected string _Mater_barcode;
        
        protected string _Stock_date;
        
        protected string _Mater_code;
        
        protected string _Equip_code;
        
        protected global::System.Int16? _Shift_id;
        
        protected string _In_Time;
        
        protected global::System.Int32? _Real_num;
        
        protected global::System.Decimal? _Real_weight;
        
        protected string _Handle_name;
        
        protected global::System.Decimal? _Used_weigh;
        
        protected string _Used_Flag;
        
        protected string _Clear_Flag;
        
        protected global::System.Int32? _Shift_Classid;
        
        protected global::System.Int32? _Ware_num;
        
        protected string _Ware_type;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Pst_mminjar left, global::Mesnac.Entity.Pst_mminjar right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Pst_mminjar left, global::Mesnac.Entity.Pst_mminjar right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public long Jar_id {
            get {
                return this._Jar_id;
            }
            set {
                this.OnPropertyChanged("Jar_id", this._Jar_id, value);
                this._Jar_id = value;
            }
        }
        
        public string Mater_barcode {
            get {
                return this._Mater_barcode;
            }
            set {
                this.OnPropertyChanged("Mater_barcode", this._Mater_barcode, value);
                this._Mater_barcode = value;
            }
        }
        
        public string Stock_date {
            get {
                return this._Stock_date;
            }
            set {
                this.OnPropertyChanged("Stock_date", this._Stock_date, value);
                this._Stock_date = value;
            }
        }
        
        public string Mater_code {
            get {
                return this._Mater_code;
            }
            set {
                this.OnPropertyChanged("Mater_code", this._Mater_code, value);
                this._Mater_code = value;
            }
        }
        
        public string Equip_code {
            get {
                return this._Equip_code;
            }
            set {
                this.OnPropertyChanged("Equip_code", this._Equip_code, value);
                this._Equip_code = value;
            }
        }
        
        public global::System.Int16? Shift_id {
            get {
                return this._Shift_id;
            }
            set {
                this.OnPropertyChanged("Shift_id", this._Shift_id, value);
                this._Shift_id = value;
            }
        }
        
        public string In_Time {
            get {
                return this._In_Time;
            }
            set {
                this.OnPropertyChanged("In_Time", this._In_Time, value);
                this._In_Time = value;
            }
        }
        
        public global::System.Int32? Real_num {
            get {
                return this._Real_num;
            }
            set {
                this.OnPropertyChanged("Real_num", this._Real_num, value);
                this._Real_num = value;
            }
        }
        
        public global::System.Decimal? Real_weight {
            get {
                return this._Real_weight;
            }
            set {
                this.OnPropertyChanged("Real_weight", this._Real_weight, value);
                this._Real_weight = value;
            }
        }
        
        public string Handle_name {
            get {
                return this._Handle_name;
            }
            set {
                this.OnPropertyChanged("Handle_name", this._Handle_name, value);
                this._Handle_name = value;
            }
        }
        
        public global::System.Decimal? Used_weigh {
            get {
                return this._Used_weigh;
            }
            set {
                this.OnPropertyChanged("Used_weigh", this._Used_weigh, value);
                this._Used_weigh = value;
            }
        }
        
        public string Used_Flag {
            get {
                return this._Used_Flag;
            }
            set {
                this.OnPropertyChanged("Used_Flag", this._Used_Flag, value);
                this._Used_Flag = value;
            }
        }
        
        public string Clear_Flag {
            get {
                return this._Clear_Flag;
            }
            set {
                this.OnPropertyChanged("Clear_Flag", this._Clear_Flag, value);
                this._Clear_Flag = value;
            }
        }
        
        public global::System.Int32? Shift_Classid {
            get {
                return this._Shift_Classid;
            }
            set {
                this.OnPropertyChanged("Shift_Classid", this._Shift_Classid, value);
                this._Shift_Classid = value;
            }
        }
        
        public global::System.Int32? Ware_num {
            get {
                return this._Ware_num;
            }
            set {
                this.OnPropertyChanged("Ware_num", this._Ware_num, value);
                this._Ware_num = value;
            }
        }
        
        public string Ware_type {
            get {
                return this._Ware_type;
            }
            set {
                this.OnPropertyChanged("Ware_type", this._Ware_type, value);
                this._Ware_type = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Pst_mminjar._Pst_mminjarEntityConfiguration == null)) {
                Pst_mminjar._Pst_mminjarEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Pst_mminjar");
            }
            return Pst_mminjar._Pst_mminjarEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Jar_id,
                    this._Mater_barcode,
                    this._Stock_date,
                    this._Mater_code,
                    this._Equip_code,
                    this._Shift_id,
                    this._In_Time,
                    this._Real_num,
                    this._Real_weight,
                    this._Handle_name,
                    this._Used_weigh,
                    this._Used_Flag,
                    this._Clear_Flag,
                    this._Shift_Classid,
                    this._Ware_num,
                    this._Ware_type};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Jar_id = reader.GetInt64(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Mater_barcode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Stock_date = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Mater_code = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Equip_code = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Shift_id = reader.GetInt16(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._In_Time = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._Real_num = reader.GetInt32(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._Real_weight = reader.GetDecimal(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._Handle_name = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._Used_weigh = reader.GetDecimal(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._Used_Flag = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._Clear_Flag = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._Shift_Classid = reader.GetInt32(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._Ware_num = reader.GetInt32(14);
            }
            if ((false == reader.IsDBNull(15))) {
                this._Ware_type = reader.GetString(15);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Jar_id = ((long)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Mater_barcode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Stock_date = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Mater_code = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Equip_code = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Shift_id = ((System.Nullable<short>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._In_Time = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._Real_num = ((System.Nullable<int>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._Real_weight = ((System.Nullable<decimal>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._Handle_name = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._Used_weigh = ((System.Nullable<decimal>)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._Used_Flag = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._Clear_Flag = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._Shift_Classid = ((System.Nullable<int>)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._Ware_num = ((System.Nullable<int>)(row[14]));
            }
            if ((false == row.IsNull(15))) {
                this._Ware_type = ((string)(row[15]));
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
            if ((false == typeof(global::Mesnac.Entity.Pst_mminjar).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Pst_mminjar)(obj)).isAttached) 
                        && (this.Jar_id == ((global::Mesnac.Entity.Pst_mminjar)(obj)).Jar_id));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Jar_id = new NBear.Common.PropertyItem("Jar_id", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Mater_barcode = new NBear.Common.PropertyItem("Mater_barcode", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Stock_date = new NBear.Common.PropertyItem("Stock_date", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Mater_code = new NBear.Common.PropertyItem("Mater_code", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Equip_code = new NBear.Common.PropertyItem("Equip_code", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Shift_id = new NBear.Common.PropertyItem("Shift_id", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _In_Time = new NBear.Common.PropertyItem("In_Time", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Real_num = new NBear.Common.PropertyItem("Real_num", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Real_weight = new NBear.Common.PropertyItem("Real_weight", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Handle_name = new NBear.Common.PropertyItem("Handle_name", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Used_weigh = new NBear.Common.PropertyItem("Used_weigh", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Used_Flag = new NBear.Common.PropertyItem("Used_Flag", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Clear_Flag = new NBear.Common.PropertyItem("Clear_Flag", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Shift_Classid = new NBear.Common.PropertyItem("Shift_Classid", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Ware_num = new NBear.Common.PropertyItem("Ware_num", "Mesnac.Entity.Pst_mminjar");
            
            protected NBear.Common.PropertyItem _Ware_type = new NBear.Common.PropertyItem("Ware_type", "Mesnac.Entity.Pst_mminjar");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Jar_id {
                get {
                    if ((aliasName == null)) {
                        return _Jar_id;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Jar_id", _Jar_id.EntityConfiguration, _Jar_id.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mater_barcode {
                get {
                    if ((aliasName == null)) {
                        return _Mater_barcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mater_barcode", _Mater_barcode.EntityConfiguration, _Mater_barcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Stock_date {
                get {
                    if ((aliasName == null)) {
                        return _Stock_date;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Stock_date", _Stock_date.EntityConfiguration, _Stock_date.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mater_code {
                get {
                    if ((aliasName == null)) {
                        return _Mater_code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mater_code", _Mater_code.EntityConfiguration, _Mater_code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Equip_code {
                get {
                    if ((aliasName == null)) {
                        return _Equip_code;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Equip_code", _Equip_code.EntityConfiguration, _Equip_code.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Shift_id {
                get {
                    if ((aliasName == null)) {
                        return _Shift_id;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Shift_id", _Shift_id.EntityConfiguration, _Shift_id.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem In_Time {
                get {
                    if ((aliasName == null)) {
                        return _In_Time;
                    }
                    else {
                        return new NBear.Common.PropertyItem("In_Time", _In_Time.EntityConfiguration, _In_Time.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Real_num {
                get {
                    if ((aliasName == null)) {
                        return _Real_num;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Real_num", _Real_num.EntityConfiguration, _Real_num.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Real_weight {
                get {
                    if ((aliasName == null)) {
                        return _Real_weight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Real_weight", _Real_weight.EntityConfiguration, _Real_weight.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Handle_name {
                get {
                    if ((aliasName == null)) {
                        return _Handle_name;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Handle_name", _Handle_name.EntityConfiguration, _Handle_name.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Used_weigh {
                get {
                    if ((aliasName == null)) {
                        return _Used_weigh;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Used_weigh", _Used_weigh.EntityConfiguration, _Used_weigh.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Used_Flag {
                get {
                    if ((aliasName == null)) {
                        return _Used_Flag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Used_Flag", _Used_Flag.EntityConfiguration, _Used_Flag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Clear_Flag {
                get {
                    if ((aliasName == null)) {
                        return _Clear_Flag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Clear_Flag", _Clear_Flag.EntityConfiguration, _Clear_Flag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Shift_Classid {
                get {
                    if ((aliasName == null)) {
                        return _Shift_Classid;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Shift_Classid", _Shift_Classid.EntityConfiguration, _Shift_Classid.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Ware_num {
                get {
                    if ((aliasName == null)) {
                        return _Ware_num;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Ware_num", _Ware_num.EntityConfiguration, _Ware_num.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Ware_type {
                get {
                    if ((aliasName == null)) {
                        return _Ware_type;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Ware_type", _Ware_type.EntityConfiguration, _Ware_type.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
