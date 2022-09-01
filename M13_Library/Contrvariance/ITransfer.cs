using System;

namespace M13_Library
{
   
    public interface ITransfer<in T1>
    {
        bool TransferContr(T1 get, T1 put, float amount);
    }
}
