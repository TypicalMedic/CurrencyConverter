using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CurrencyConverter.Contracts
{
    [ServiceContract(Namespace = "http://fahrenheittocelsiusconversion.azurewebsites.net/")]
    public interface IConvService
    {
        [OperationContract]
        decimal ConvertCurrency(int from, int to, decimal value);

        [OperationContract]
        List<string> GetCurrencyNames();
    }
}
