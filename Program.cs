using System;
using System.Xml;
using System.Xml.Schema;

namespace CabinetInfirmier // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XMLUtils.ValidateXmlFile("http://www.univ-grenoble-alpes.fr/l3miage/medical", "../../../data/xsd/cabinet.xsd", "../../../data/xml/cabinet.xml");
        }
    }
}