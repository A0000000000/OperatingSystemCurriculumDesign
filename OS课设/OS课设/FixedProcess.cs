using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class FixedProcess
    {
        public FixedProcess(string processName, int size)
        {
            ProcessName = processName;
            Size = size;
        }

        public string ProcessName
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int Position
        {
            get;
            set;
        }
    }
}
