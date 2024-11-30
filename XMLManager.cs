using System.Xml.Serialization;

namespace CabinetInfirmier;

/* An XML Manager is used to Serialize (save) / Deserialize (load) XML documents and Objects.
   It’s a generic class that must be specialized for a particular object that corresponds to the XML document.
   documentation: https://learn.microsoft.com/fr-fr/dotnet/api/system.xml.serialization.xmlserializer.deserialize?view=net-8.0
*/

public class XMLManager<T>
{
    // --- unmarshalling - désérialisation --- (XML -> Object)
    public T Load(string path)
    {
        T instance;
        using (TextReader reader = new StreamReader(path))
        {
            var xml = new XmlSerializer(typeof(T));
            instance = (T)xml.Deserialize(reader);
        }

        return instance;
    }

    // --- marshalling - sérialisation --- (Object -> XML)
    public void Save(string path, object obj)
    {
        using (TextWriter writer = new StreamWriter(path))
        {
            var xml = new XmlSerializer(typeof(T));
            xml.Serialize(writer, obj);
        }
    }

    // --- marshalling - sérialisation --- (Object -> XML) with namespaces
    public void Save(string path, object obj, XmlSerializerNamespaces ns)
    {
        using (TextWriter writer = new StreamWriter(path))
        {
            var xml = new XmlSerializer(typeof(T));
            xml.Serialize(writer, obj, ns);
        }
    }
}