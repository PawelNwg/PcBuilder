using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface ISessionManager
    {
        byte[] Get(string key);

        void Set(string name, byte[] value);

        void Clear();

        T TryGet<T>(string key);
    }
}