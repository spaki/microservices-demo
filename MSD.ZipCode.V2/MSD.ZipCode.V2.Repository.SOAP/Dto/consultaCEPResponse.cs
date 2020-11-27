using System.ServiceModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MSD.ZipCode.V2.Repository.SOAP.Dto
{
    [MessageContract(WrapperName = "consultaCEPResponse", WrapperNamespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", IsWrapped = true)]
    public class consultaCEPResponse
    {
        [MessageBodyMember(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/", Order = 0)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public enderecoERP @return;

        public consultaCEPResponse()
        {
        }

        public consultaCEPResponse(enderecoERP @return)
        {
            this.@return = @return;
        }
    }
}
