using CurrencyConverter.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CurrencyConverter.Services
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ConvService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ConvService.svc или ConvService.svc.cs в обозревателе решений и начните отладку.
    public class ConvService : IConvService
    {
        readonly ServiceReference1.DailyInfoSoapClient cb = new ServiceReference1.DailyInfoSoapClient();
        public decimal ConvertCurrency(int from, int to, decimal value)
        {
            DataRowCollection rows = cb.GetCursOnDate(DateTime.Now).Tables[0].Rows;
            rows.Add(new object[] { "Российский рубль", 1, 1, 1, "RUB" });
            DataRow fromR = rows[from];
            decimal fromC = (decimal)fromR[2];
            decimal fromN = (decimal)fromR[1];
            DataRow toR = rows[to];
            decimal toC = (decimal)toR[2];
            decimal toN = (decimal)toR[1];
            return value * fromC * toN / (fromN * toC);
        }

        public List<string> GetCurrencyNames()
        {
            List<string> res = new List<string>();
            foreach (var x in cb.GetCursOnDate(DateTime.Now).Tables[0].Rows)
            {
                DataRow row = (DataRow)x;
                res.Add(row.ItemArray[0].ToString());
            }
            res.Add("Российский рубль");
            return res;
        }
    }
}
