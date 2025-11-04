using CoreWCF;
using System.Runtime.Serialization;

namespace SoapWcfCore
{
    [ServiceContract(Name = "MathService", Namespace = "http://example.com/services")]
    public interface IMathService
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        int AddWithError(int x, int y);
    }

    public class MathService : IMathService
    {
        public int Add(int x, int y) => x + y;

        public int AddWithError(int x, int y)
        {
            var fault = new MathFault
            {
                ErrorCode = "NIL_NUMBER",
                Message = "Nil numbers are not allowed"
            };

            throw new FaultException<MathFault>(fault, new FaultReason(fault.Message));
        }
    }
}


[DataContract]
public class MathFault
{
    [DataMember]
    public string ErrorCode { get; set; }

    [DataMember]
    public string Message { get; set; }
}
