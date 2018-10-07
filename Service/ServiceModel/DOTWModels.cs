namespace Service.ServiceModel.DOTWModels
{
    public class SearchHotelByCityViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string CityCode { get; set; }
    }

    public class MyFile
    {
        public string Name { get; set; }
        public string Extention { get; set; }
        public byte[] Contents { get; set; }
    }

    public class CityModel
    {
        public string Name { get; set; }
        public int Code { get; set; }
    }
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class result
    {

        private resultCities citiesField;

        private string successfulField;

        private string commandField;

        private ulong tIDField;

        private string ipField;

        private string sipField;

        private string dateField;

        private decimal versionField;

        private decimal elapsedTimeField;

        /// <remarks/>
        public resultCities cities
        {
            get
            {
                return this.citiesField;
            }
            set
            {
                this.citiesField = value;
            }
        }

        /// <remarks/>
        public string successful
        {
            get
            {
                return this.successfulField;
            }
            set
            {
                this.successfulField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string command
        {
            get
            {
                return this.commandField;
            }
            set
            {
                this.commandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong tID
        {
            get
            {
                return this.tIDField;
            }
            set
            {
                this.tIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ip
        {
            get
            {
                return this.ipField;
            }
            set
            {
                this.ipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sip
        {
            get
            {
                return this.sipField;
            }
            set
            {
                this.sipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal elapsedTime
        {
            get
            {
                return this.elapsedTimeField;
            }
            set
            {
                this.elapsedTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultCities
    {

        private resultCitiesCity[] cityField;

        private ushort countField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("city")]
        public resultCitiesCity[] city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultCitiesCity
    {

        private string nameField;

        private int codeField;

        private ushort runnoField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public int code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort runno
        {
            get
            {
                return this.runnoField;
            }
            set
            {
                this.runnoField = value;
            }
        }
    }


}
