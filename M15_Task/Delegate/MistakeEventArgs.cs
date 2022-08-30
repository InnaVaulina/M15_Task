using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace M15_Task
{
    public class MistakeEventArgs
    {
        string mistakeMessage;
        public string MistakeMessage { get { return mistakeMessage; } }

        public MistakeEventArgs(string message) { mistakeMessage = message; }
    }

    public delegate void MistakeMessageHendler(AVMUser user, MistakeEventArgs e);
}
