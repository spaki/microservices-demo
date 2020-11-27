using MSD.ZipCode.V1.API.Model;
using System.ServiceModel;

namespace MSD.ZipCode.V1.API.Interfaces
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
