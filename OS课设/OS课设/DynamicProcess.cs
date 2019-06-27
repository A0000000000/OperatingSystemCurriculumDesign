using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class DynamicProcess
    {
        public DynamicProcess(string processName, int size)
        {
            ProcessName = processName;
            Size = size;
        }

        public string ProcessName
        {
            get;
            set;
        }
        public int Begin
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }
    }
}
