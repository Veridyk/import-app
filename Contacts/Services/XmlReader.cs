using ContactsApplication.Configuration;
using ContactsApplication.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ContactsApplication.Services
{
    public sealed class XmlReader: IXmlReader
    {
        public XmlReader()
        {

        }

        public T? Read<T>(string path)
        {
            T? data = default;
            var serializer = new XmlSerializer(typeof(T));
            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                var content = serializer.Deserialize(reader);
                if(content != null)
                {
                    data = (T)content;
                }
            }

            return data;
        }

        public void Write<T>(string path, T data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using Stream writer = new FileStream(path, FileMode.Create);
            serializer.Serialize(writer, data);
            writer.Close();
        }
    }
}
