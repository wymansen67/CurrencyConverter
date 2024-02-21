using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WpfCurrencyConverter.Data
{
    public static class ValuteLoader
    {
        /// <summary>
        /// Получает список валют из текста XML
        /// </summary>
        /// <param name="XMLText">Строка с информацией о валютах в формате XML</param>
        /// <returns>Список валют</returns>
        public static List<Valute> LoadValutes(string xmlstring)
        {
            List<Valute> valList= new List<Valute>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlstring);
            XmlNodeList valutes = doc.SelectNodes("//Valute");
            foreach (XmlNode val in valutes)
            {
                valList.Add(new Valute(Convert.ToInt32(val.SelectSingleNode("NumCode").InnerText), val.SelectSingleNode("CharCode").InnerText, Convert.ToInt32(val.SelectSingleNode("Nominal").InnerText), val.SelectSingleNode("Name").InnerText, Convert.ToDouble(val.SelectSingleNode("Value").InnerText)));
            }
            return valList;
        }

        public static DateTime LoadDate(string xmlstring)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlstring);
            DateTime dt = DateTime.Parse(doc.DocumentElement.GetAttribute("Date"));
            return dt;
        }
    }
}