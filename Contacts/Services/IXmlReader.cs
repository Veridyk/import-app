using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApplication.Services
{
    public interface IXmlReader
    {
        T? Read<T>(string path);
        void Write<T>(string path, T value);
    }
}
