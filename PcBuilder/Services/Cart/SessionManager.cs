using Microsoft.AspNetCore.Http;
using PcBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Cart
{
    public class SessionManager : ISessionManager
    {
        private ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void Clear()
        {
            _session.Clear();
        }

        public byte[] Get(string key)
        {
            _session.TryGetValue(key, out byte[] value);
            return value;
        }

        public void Set(string name, byte[] value)
        {
            _session.Set(name, value);
        }

        public T TryGet<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}