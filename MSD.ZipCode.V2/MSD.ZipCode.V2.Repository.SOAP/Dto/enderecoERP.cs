using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MSD.ZipCode.V2.Repository.SOAP.Dto
{
    [Serializable()]
    [XmlType(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public class enderecoERP
    {
        private string bairroField;

        private string cepField;

        private string cidadeField;

        private string complemento2Field;

        private string endField;

        private string ufField;

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string bairro
        {
            get
            {
                return this.bairroField;
            }
            set
            {
                this.bairroField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string cidade
        {
            get
            {
                return this.cidadeField;
            }
            set
            {
                this.cidadeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string complemento2
        {
            get
            {
                return this.complemento2Field;
            }
            set
            {
                this.complemento2Field = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string end
        {
            get
            {
                return this.endField;
            }
            set
            {
                this.endField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string uf
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }
    }
}
