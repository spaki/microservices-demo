﻿using System.ServiceModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MSD.ZipCode.V1.API.Model
{
    [MessageContract(WrapperName = "consultaCEP", WrapperNamespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", IsWrapped = true)]
    public class consultaCEP
    {
        [MessageBodyMember(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", Order = 0)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string cep;
        
        public consultaCEP(string cep)
        {
            this.cep = cep;
        }
    }
}
