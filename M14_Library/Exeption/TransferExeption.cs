﻿using System;
using System.Collections.Generic;
using System.Text;

namespace M14_Library
{
    internal class TransferExeption : KeyNotFoundException
    {
        public TransferExeption(string message) : base(message) { }
    }


    internal class MyExeption : Exception
    {
        public MyExeption(string message) : base(message) { }
    }
}
