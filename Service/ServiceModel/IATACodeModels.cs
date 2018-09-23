using Service.ServiceModel.PublicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceModel.IATACodeModels
{
    public class IATACodeViewModel
    {
        public string Message { get; set; }
        public bool MapSuccess { get; set; }
        public bool GetSuccess { get; set; }
        public string Code { get; set; }
    }
    public class IATAResponse
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public List<IATAData> Data { get; set; }
    }
    public class IATAData
    {
        public string CityName { get; set; }
        public string AirportName { get; set; }
        public string CityCode { get; set; }
        public string AirportCode { get; set; }
    }
    public class GCMAPResponse
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public GCMAPData Data { get; set; }
    }
    public class GCMAPData
    {
        public string City { get; set; }
        public string IATACode { get; set; }
        public string GCMAPCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public class IataCodeResponse
    {
        public Error Error { get; set; }
        public bool Success { get; set; }
        public List<IataCodeData> Data { get; set; }
    }
    public class IataCodeData
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class root
    {

        private rootRequest requestField;

        private rootResponse[] responseField;

        /// <remarks/>
        public rootRequest request
        {
            get
            {
                return this.requestField;
            }
            set
            {
                this.requestField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("response", IsNullable = false)]
        public rootResponse[] response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequest
    {

        private string langField;

        private string currencyField;

        private byte timeField;

        private ulong idField;

        private string serverField;

        private string hostField;

        private ushort pidField;

        private rootRequestKey keyField;

        private rootRequestParams paramsField;

        private byte versionField;

        private string methodField;

        private rootRequestClient clientField;

        /// <remarks/>
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        public string currency
        {
            get
            {
                return this.currencyField;
            }
            set
            {
                this.currencyField = value;
            }
        }

        /// <remarks/>
        public byte time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }

        /// <remarks/>
        public ulong id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string server
        {
            get
            {
                return this.serverField;
            }
            set
            {
                this.serverField = value;
            }
        }

        /// <remarks/>
        public string host
        {
            get
            {
                return this.hostField;
            }
            set
            {
                this.hostField = value;
            }
        }

        /// <remarks/>
        public ushort pid
        {
            get
            {
                return this.pidField;
            }
            set
            {
                this.pidField = value;
            }
        }

        /// <remarks/>
        public rootRequestKey key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        public rootRequestParams @params
        {
            get
            {
                return this.paramsField;
            }
            set
            {
                this.paramsField = value;
            }
        }

        /// <remarks/>
        public byte version
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
        public string method
        {
            get
            {
                return this.methodField;
            }
            set
            {
                this.methodField = value;
            }
        }

        /// <remarks/>
        public rootRequestClient client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequestKey
    {

        private ushort idField;

        private string api_keyField;

        private string typeField;

        private byte trial_priceField;

        private object expiredField;

        private System.DateTime registeredField;

        private ushort limits_by_hourField;

        private byte limits_by_minuteField;

        private object demo_methodsField;

        private byte usage_by_hourField;

        private byte usage_by_minuteField;

        /// <remarks/>
        public ushort id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string api_key
        {
            get
            {
                return this.api_keyField;
            }
            set
            {
                this.api_keyField = value;
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public byte trial_price
        {
            get
            {
                return this.trial_priceField;
            }
            set
            {
                this.trial_priceField = value;
            }
        }

        /// <remarks/>
        public object expired
        {
            get
            {
                return this.expiredField;
            }
            set
            {
                this.expiredField = value;
            }
        }

        /// <remarks/>
        public System.DateTime registered
        {
            get
            {
                return this.registeredField;
            }
            set
            {
                this.registeredField = value;
            }
        }

        /// <remarks/>
        public ushort limits_by_hour
        {
            get
            {
                return this.limits_by_hourField;
            }
            set
            {
                this.limits_by_hourField = value;
            }
        }

        /// <remarks/>
        public byte limits_by_minute
        {
            get
            {
                return this.limits_by_minuteField;
            }
            set
            {
                this.limits_by_minuteField = value;
            }
        }

        /// <remarks/>
        public object demo_methods
        {
            get
            {
                return this.demo_methodsField;
            }
            set
            {
                this.demo_methodsField = value;
            }
        }

        /// <remarks/>
        public byte usage_by_hour
        {
            get
            {
                return this.usage_by_hourField;
            }
            set
            {
                this.usage_by_hourField = value;
            }
        }

        /// <remarks/>
        public byte usage_by_minute
        {
            get
            {
                return this.usage_by_minuteField;
            }
            set
            {
                this.usage_by_minuteField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequestParams
    {

        private string langField;

        /// <remarks/>
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequestClient
    {

        private string country_codeField;

        private string countryField;

        private decimal latField;

        private decimal lngField;

        private string ipField;

        private rootRequestClientDevice deviceField;

        private rootRequestClientAgent agentField;

        /// <remarks/>
        public string country_code
        {
            get
            {
                return this.country_codeField;
            }
            set
            {
                this.country_codeField = value;
            }
        }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public decimal lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        public decimal lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }

        /// <remarks/>
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
        public rootRequestClientDevice device
        {
            get
            {
                return this.deviceField;
            }
            set
            {
                this.deviceField = value;
            }
        }

        /// <remarks/>
        public rootRequestClientAgent agent
        {
            get
            {
                return this.agentField;
            }
            set
            {
                this.agentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequestClientDevice
    {

        private string typeField;

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootRequestClientAgent
    {

        private string browserField;

        private string versionField;

        private string osField;

        private string platformField;

        /// <remarks/>
        public string browser
        {
            get
            {
                return this.browserField;
            }
            set
            {
                this.browserField = value;
            }
        }

        /// <remarks/>
        public string version
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
        public string os
        {
            get
            {
                return this.osField;
            }
            set
            {
                this.osField = value;
            }
        }

        /// <remarks/>
        public string platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rootResponse
    {

        private string codeField;

        private string nameField;

        /// <remarks/>
        public string code
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
    }


}
