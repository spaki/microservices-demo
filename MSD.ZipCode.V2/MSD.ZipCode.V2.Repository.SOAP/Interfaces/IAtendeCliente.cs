using MSD.ZipCode.V2.Repository.SOAP.Dto;
using System.ServiceModel;

namespace MSD.ZipCode.V2.Repository.SOAP.Interfaces
{
    [ServiceContract(Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public interface IAtendeCliente
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [return: MessageParameter(Name = "return")]
        consultaCEPResponse consultaCEP(consultaCEP request);
    }
}
