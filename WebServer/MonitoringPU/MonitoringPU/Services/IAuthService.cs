using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringPU.Services
{
    public interface IAuthService<T>
    {
        Task<T> Login(string user, string password);
    }
}
