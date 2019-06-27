using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class DynamicMemory
    {
        public DynamicMemory(int sum)
        {
            list = new AssistList(sum);
        }

        public bool AddProcess(DynamicProcess dp, AllocationWay way)
        {
            if (list.AddNewProcess(dp, way))
            {
                hs.Add(dp);
                return true;
            }
            return false;
        }

        public bool RemoveProcessByName(string name)
        {
            DynamicProcess tmp = null;
            foreach (DynamicProcess dp in hs)
            {
                if (dp.ProcessName.Equals(name))
                {
                    tmp = dp;
                    break;
                }
            }
            if (tmp == null)
                return false;
            if (list.RemoveProcess(tmp))
            {
                hs.Remove(tmp);
                return true;
            }
            return false;
        }

        public void PrintInfo()
        {
            list.PrintInfo();
            Console.WriteLine("进程信息:");
            foreach (DynamicProcess dp in hs)
            {
                Console.WriteLine($"进程名: {dp.ProcessName}, 进程首地址: {dp.Begin}, 进程所占内存大小: {dp.Size}");
            }
        }
        private AssistList list;
        private HashSet<DynamicProcess> hs = new HashSet<DynamicProcess>();
    }
}
