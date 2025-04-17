using System.Xml.Serialization;

namespace InventSys.Domain.Tools
{
    public static class XmlTools
    {
        public static T? Deserializar<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xml);
            return (T?)serializer.Deserialize(stringReader);
        }
        public static string Serializar<T>(T objeto)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, objeto);
            return stringWriter.ToString();
        }
    }
}
