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
    public partial class Ppt_curvedataArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Ppt_curvedata> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Ppt_curvedata\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBa" +
        "tchUpdate=\"false\" isRelation=\"false\" mappingName=\"Ppt_curvedata\" batchSize=\"10\">" +
        "\r\n  <Properties>\r\n    <Property name=\"Barcode\" type=\"System.String\" isInherited=" +
        "\"false\" sqlDefaultValue=\"\' \'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.String\" sqlType=\"char" +
        "(18)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Curve_data\" t" +
        "ype=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false" +
        "\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isR" +
        "elationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeriali" +
        "zationIgnore=\"false\" mappingName=\"Curve_data\" mappingColumnType=\"System.String\" " +
        "sqlType=\"ntext\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"P" +
        "lan_date\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"Plan_date\" mappingColumnType=\"Syste" +
        "m.String\" sqlType=\"char(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"Plan_id\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Plan_id\" mappingColumnTy" +
        "pe=\"System.String\" sqlType=\"char(12)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r" +
        "\n    <Property name=\"Serial_id\" type=\"System.Nullable`1[System.Int32]\" isInherit" +
        "ed=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery" +
        "=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPro" +
        "perty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingN" +
        "ame=\"Serial_id\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int" +
        "\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Start_datetime\"" +
        " type=\"System.DateTime\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"Start_datetime\" mappingColumnType=\"System." +
        "DateTime\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"Mixing_Time\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fa" +
        "lse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fal" +
        "se\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProp" +
        "ertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Mixing_Time\" mapping" +
        "ColumnType=\"System.String\" sqlType=\"ntext\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"Mixing_Temp\" type=\"System.String\" isInherited=\"false\" " +
        "isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Mixing" +
        "_Temp\" mappingColumnType=\"System.String\" sqlType=\"ntext\" isPrimaryKey=\"false\" is" +
        "NotNull=\"false\" />\r\n    <Property name=\"Mixing_Power\" type=\"System.String\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"Mixing_Power\" mappingColumnType=\"System.String\" sqlType=\"ntext\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Mixing_Energy\" type=\"Sys" +
        "tem.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"Mixing_Energy\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"ntext\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Mixing" +
        "_Press\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUn" +
        "it=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"f" +
        "alse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" " +
        "isSerializationIgnore=\"false\" mappingName=\"Mixing_Press\" mappingColumnType=\"Syst" +
        "em.String\" sqlType=\"ntext\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Prope" +
        "rty name=\"Mixing_Speed\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fal" +
        "se\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fals" +
        "e\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPrope" +
        "rtyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Mixing_Speed\" mapping" +
        "ColumnType=\"System.String\" sqlType=\"ntext\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"If_Subed\" type=\"System.String\" isInherited=\"false\" isR" +
        "eadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFri" +
        "endKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" " +
        "isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"If_Subed\"" +
        " mappingColumnType=\"System.String\" sqlType=\"ntext\" isPrimaryKey=\"false\" isNotNul" +
        "l=\"false\" />\r\n    <Property name=\"Mixing_Sidetemp\" type=\"System.String\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"Mixing_Sidetemp\" mappingColumnType=\"System.String\" sqlType=\"ntext\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"SDS_postion\" type=\"Syste" +
        "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"SDS_postion\" mappingColumnType=\"System.String\" sqlType=\"" +
        "ntext\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfi" +
        "guration>")]
    public partial class Ppt_curvedata : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Ppt_curvedataEntityConfiguration;
        
        protected string _Barcode;
        
        protected string _Curve_data;
        
        protected string _Plan_date;
        
        protected string _Plan_id;
        
        protected global::System.Int32? _Serial_id;
        
        protected global::System.DateTime _Start_datetime;
        
        protected string _Mixing_Time;
        
        protected string _Mixing_Temp;
        
        protected string _Mixing_Power;
        
        protected string _Mixing_Energy;
        
        protected string _Mixing_Press;
        
        protected string _Mixing_Speed;
        
        protected string _If_Subed;
        
        protected string _Mixing_Sidetemp;
        
        protected string _SDS_postion;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Ppt_curvedata left, global::Mesnac.Entity.Ppt_curvedata right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Ppt_curvedata left, global::Mesnac.Entity.Ppt_curvedata right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string Barcode {
            get {
                return this._Barcode;
            }
            set {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }
        
        public string Curve_data {
            get {
                return this._Curve_data;
            }
            set {
                this.OnPropertyChanged("Curve_data", this._Curve_data, value);
                this._Curve_data = value;
            }
        }
        
        public string Plan_date {
            get {
                return this._Plan_date;
            }
            set {
                this.OnPropertyChanged("Plan_date", this._Plan_date, value);
                this._Plan_date = value;
            }
        }
        
        public string Plan_id {
            get {
                return this._Plan_id;
            }
            set {
                this.OnPropertyChanged("Plan_id", this._Plan_id, value);
                this._Plan_id = value;
            }
        }
        
        public global::System.Int32? Serial_id {
            get {
                return this._Serial_id;
            }
            set {
                this.OnPropertyChanged("Serial_id", this._Serial_id, value);
                this._Serial_id = value;
            }
        }
        
        public global::System.DateTime Start_datetime {
            get {
                return this._Start_datetime;
            }
            set {
                this.OnPropertyChanged("Start_datetime", this._Start_datetime, value);
                this._Start_datetime = value;
            }
        }
        
        public string Mixing_Time {
            get {
                return this._Mixing_Time;
            }
            set {
                this.OnPropertyChanged("Mixing_Time", this._Mixing_Time, value);
                this._Mixing_Time = value;
            }
        }
        
        public string Mixing_Temp {
            get {
                return this._Mixing_Temp;
            }
            set {
                this.OnPropertyChanged("Mixing_Temp", this._Mixing_Temp, value);
                this._Mixing_Temp = value;
            }
        }
        
        public string Mixing_Power {
            get {
                return this._Mixing_Power;
            }
            set {
                this.OnPropertyChanged("Mixing_Power", this._Mixing_Power, value);
                this._Mixing_Power = value;
            }
        }
        
        public string Mixing_Energy {
            get {
                return this._Mixing_Energy;
            }
            set {
                this.OnPropertyChanged("Mixing_Energy", this._Mixing_Energy, value);
                this._Mixing_Energy = value;
            }
        }
        
        public string Mixing_Press {
            get {
                return this._Mixing_Press;
            }
            set {
                this.OnPropertyChanged("Mixing_Press", this._Mixing_Press, value);
                this._Mixing_Press = value;
            }
        }
        
        public string Mixing_Speed {
            get {
                return this._Mixing_Speed;
            }
            set {
                this.OnPropertyChanged("Mixing_Speed", this._Mixing_Speed, value);
                this._Mixing_Speed = value;
            }
        }
        
        public string If_Subed {
            get {
                return this._If_Subed;
            }
            set {
                this.OnPropertyChanged("If_Subed", this._If_Subed, value);
                this._If_Subed = value;
            }
        }
        
        public string Mixing_Sidetemp {
            get {
                return this._Mixing_Sidetemp;
            }
            set {
                this.OnPropertyChanged("Mixing_Sidetemp", this._Mixing_Sidetemp, value);
                this._Mixing_Sidetemp = value;
            }
        }
        
        public string SDS_postion {
            get {
                return this._SDS_postion;
            }
            set {
                this.OnPropertyChanged("SDS_postion", this._SDS_postion, value);
                this._SDS_postion = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Ppt_curvedata._Ppt_curvedataEntityConfiguration == null)) {
                Ppt_curvedata._Ppt_curvedataEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Ppt_curvedata");
            }
            return Ppt_curvedata._Ppt_curvedataEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Barcode,
                    this._Curve_data,
                    this._Plan_date,
                    this._Plan_id,
                    this._Serial_id,
                    this._Start_datetime,
                    this._Mixing_Time,
                    this._Mixing_Temp,
                    this._Mixing_Power,
                    this._Mixing_Energy,
                    this._Mixing_Press,
                    this._Mixing_Speed,
                    this._If_Subed,
                    this._Mixing_Sidetemp,
                    this._SDS_postion};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Barcode = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Curve_data = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Plan_date = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Plan_id = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Serial_id = reader.GetInt32(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Start_datetime = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._Mixing_Time = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._Mixing_Temp = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._Mixing_Power = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._Mixing_Energy = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._Mixing_Press = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._Mixing_Speed = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._If_Subed = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._Mixing_Sidetemp = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._SDS_postion = reader.GetString(14);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Barcode = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Curve_data = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Plan_date = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Plan_id = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Serial_id = ((System.Nullable<int>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Start_datetime = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6))) {
                this._Mixing_Time = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._Mixing_Temp = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._Mixing_Power = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._Mixing_Energy = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._Mixing_Press = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._Mixing_Speed = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._If_Subed = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._Mixing_Sidetemp = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._SDS_postion = ((string)(row[14]));
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
            if ((false == typeof(global::Mesnac.Entity.Ppt_curvedata).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Ppt_curvedata)(obj)).isAttached) 
                        && (this.Barcode == ((global::Mesnac.Entity.Ppt_curvedata)(obj)).Barcode));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Curve_data = new NBear.Common.PropertyItem("Curve_data", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Plan_date = new NBear.Common.PropertyItem("Plan_date", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Plan_id = new NBear.Common.PropertyItem("Plan_id", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Serial_id = new NBear.Common.PropertyItem("Serial_id", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Start_datetime = new NBear.Common.PropertyItem("Start_datetime", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Time = new NBear.Common.PropertyItem("Mixing_Time", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Temp = new NBear.Common.PropertyItem("Mixing_Temp", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Power = new NBear.Common.PropertyItem("Mixing_Power", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Energy = new NBear.Common.PropertyItem("Mixing_Energy", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Press = new NBear.Common.PropertyItem("Mixing_Press", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Speed = new NBear.Common.PropertyItem("Mixing_Speed", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _If_Subed = new NBear.Common.PropertyItem("If_Subed", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _Mixing_Sidetemp = new NBear.Common.PropertyItem("Mixing_Sidetemp", "Mesnac.Entity.Ppt_curvedata");
            
            protected NBear.Common.PropertyItem _SDS_postion = new NBear.Common.PropertyItem("SDS_postion", "Mesnac.Entity.Ppt_curvedata");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Barcode {
                get {
                    if ((aliasName == null)) {
                        return _Barcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Curve_data {
                get {
                    if ((aliasName == null)) {
                        return _Curve_data;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Curve_data", _Curve_data.EntityConfiguration, _Curve_data.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Plan_date {
                get {
                    if ((aliasName == null)) {
                        return _Plan_date;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Plan_date", _Plan_date.EntityConfiguration, _Plan_date.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Plan_id {
                get {
                    if ((aliasName == null)) {
                        return _Plan_id;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Plan_id", _Plan_id.EntityConfiguration, _Plan_id.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Serial_id {
                get {
                    if ((aliasName == null)) {
                        return _Serial_id;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Serial_id", _Serial_id.EntityConfiguration, _Serial_id.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Start_datetime {
                get {
                    if ((aliasName == null)) {
                        return _Start_datetime;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Start_datetime", _Start_datetime.EntityConfiguration, _Start_datetime.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Time {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Time;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Time", _Mixing_Time.EntityConfiguration, _Mixing_Time.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Temp {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Temp;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Temp", _Mixing_Temp.EntityConfiguration, _Mixing_Temp.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Power {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Power;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Power", _Mixing_Power.EntityConfiguration, _Mixing_Power.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Energy {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Energy;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Energy", _Mixing_Energy.EntityConfiguration, _Mixing_Energy.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Press {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Press;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Press", _Mixing_Press.EntityConfiguration, _Mixing_Press.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Speed {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Speed;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Speed", _Mixing_Speed.EntityConfiguration, _Mixing_Speed.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem If_Subed {
                get {
                    if ((aliasName == null)) {
                        return _If_Subed;
                    }
                    else {
                        return new NBear.Common.PropertyItem("If_Subed", _If_Subed.EntityConfiguration, _If_Subed.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Mixing_Sidetemp {
                get {
                    if ((aliasName == null)) {
                        return _Mixing_Sidetemp;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Mixing_Sidetemp", _Mixing_Sidetemp.EntityConfiguration, _Mixing_Sidetemp.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SDS_postion {
                get {
                    if ((aliasName == null)) {
                        return _SDS_postion;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SDS_postion", _SDS_postion.EntityConfiguration, _SDS_postion.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}