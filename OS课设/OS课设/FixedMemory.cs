using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class FixedMemory
    {
        public FixedMemory(int sum, int block)
        {
            Sum = sum;
            Block = block;
            for (int i = 0; i < Count; i++)
            {
                memoryInfo[i] = true;
            }
        }

        public bool AddNewProcess(FixedProcess fp)
        {
            if (fp.Size > Block)
                return false;
            if (Available <= 0)
                return false;
            for (int i = 0; i < Count; i++)
            {
                if (memoryInfo[i])
                {
                    memoryInfo[i] = false;
                    fp.Position = i;
                    allProcesses.Add(fp);
                    break;
                }
            }
            return true;
        }

        public bool RemoveProcessByName(string name)
        {
            FixedProcess tmp = null;
            foreach (FixedProcess fp in allProcesses)
            {
                if (fp.ProcessName.Equals(name))
                {
                    tmp = fp;
                    break;
                }
            }
            if (tmp == null)
                return false;
            allProcesses.Remove(tmp);
            memoryInfo[tmp.Position] = true;
            return true;
        }

        public void PrintMemoryInfo()
        {
            Console.WriteLine("内存信息:");
            for (int i = 0; i < Count; i++)
            {
                Console.Write("进程块" + i + (memoryInfo[i] ? "可用" : "不可用") + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("进程信息:");
            foreach (FixedProcess fp in allProcesses)
            {
                Console.WriteLine("进程名: " + fp.ProcessName + ", 大小: " + fp.Size + ", 位置: " + fp.Position);
            }
        }
        public int Sum
        {
            get => _sum;
            set => _sum = value;
        }

        public int Block
        {
            get => _block;
            set => _block = value;
        }

        public int Count
        {
            get => Sum / Block;
        }

        public int Available
        {
            get => Count - allProcesses.Count;
        }

        private int _sum;
        private int _block;
        private Dictionary<int, bool> memoryInfo = new Dictionary<int, bool>();
        private HashSet<FixedProcess> allProcesses = new HashSet<FixedProcess>();
    }
}
