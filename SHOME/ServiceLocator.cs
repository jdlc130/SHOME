using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOME
{
    public class ServiceLocator
    {
        public static ServiceLocator Current => _current;
        private static readonly ServiceLocator _current = new ServiceLocator();

        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private ServiceLocator() { }

        public void SetService<T>(T service)
        {
            _services.Add(typeof(T), service);
        }

        public T GetService<T>() => (T) _services[typeof(T)];
    }
}
