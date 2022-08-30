using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    /// <summary>
    /// выбор представления данных клиента
    /// </summary>
    /// <typeparam name="T">AVMClients и производные </typeparam>
    internal interface IAVMSelectClient<out T>
    {
        T SelectClientAVM(User user, Client client);
    }
}
