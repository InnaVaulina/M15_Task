using System;
using System.Collections.Generic;
using System.Text;

namespace M13_Library
{
    public interface IAccount
    {
        string AccountNumber { get; }

        DateTime TimeCreate { get; }

        DateTime TimeClose { get; }

        float Balance { get; }

        bool CloseAccount();

        IClient Client { get; }

        string ToString();
    }
}
