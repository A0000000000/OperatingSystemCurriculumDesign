using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class AssistList
    {
        public AssistList(int sum)
        {
            Sum = sum;
            Head = new AssistNode(0, Sum);
            _size = 1;
        }
        public bool AddNewProcess(DynamicProcess dp, AllocationWay way)
        {
            if (Size < 1)
                return false;
            bool flag = false;
            switch (way)
            {
                case AllocationWay.首次适应算法:
                    flag = FirstFit(dp);
                    break;
                case AllocationWay.循环首次适应算法:
                    flag = LoopFirstFit(dp);
                    break;
                case AllocationWay.最佳适应算法:
                    flag = BestMatch(dp);
                    break;
            }
            return flag;
        }
        public bool RemoveProcess(DynamicProcess dp)
        {
            AssistNode node = new AssistNode(dp.Begin, dp.Size);
            if (node.Begin + node.Length <= Head.Begin)
            {
                node.Next = Head;
                Head.Last = node;
                Head = node;
            }
            else
            {
                AssistNode tmp = Head;
                for (int i = 0; i < Size; i++)
                {
                    if (i != Size - 1)
                    {
                        if (((tmp.Begin + tmp.Length) <= node.Begin) && ((node.Begin + node.Length) <= tmp.Next.Begin))
                        {
                            node.Next = tmp.Next;
                            node.Next.Last = node;
                            node.Last = tmp;
                            tmp.Next = node;
                            break;
                        }
                    }
                    else
                    {
                        if ((tmp.Begin + tmp.Length) <= node.Begin)
                        {
                            tmp.Next = node;
                            node.Last = tmp;
                            break;
                        }
                    }
                    tmp = tmp.Next;
                }
            }
            _size++;
            RepairList();
            return true;
        }
        public void PrintInfo()
        {
            Console.WriteLine("空闲内存地址:");
            AssistNode tmp = Head;
            while (tmp != null)
            {
                Console.WriteLine($"起始地址: {tmp.Begin}, 大小: {tmp.Length}");
                tmp = tmp.Next;
            }
        }
        public void RepairList()
        {
            AssistNode tmp = Head;
            for (int i = 0; i < _size - 1;)
            {
                if ((tmp.Begin + tmp.Length) == tmp.Next.Begin)
                {
                    tmp.Length += tmp.Next.Length;
                    if (tmp.Next?.Next != null)
                    {
                        tmp.Next.Next.Last = tmp;
                    }
                    if (tmp.Next != null)
                    {
                        tmp.Next = tmp.Next.Next;
                    }
                    _size--;
                }
                else
                {
                    i++;
                    tmp = tmp.Next;
                }
            }
        }
        private bool FirstFit(DynamicProcess dp)
        {
            bool flag = false;
            AssistNode tmp = Head;
            for (int i = 0; i < Size; i++)
            {
                LastVisit = i;
                LastVisitNode = tmp;
                if (tmp.Length > dp.Size)
                {
                    flag = true;
                    dp.Begin = tmp.Begin;
                    tmp.Begin += dp.Size;
                    tmp.Length -= dp.Size;
                    break;
                }
                else if (tmp.Length == dp.Size)
                {
                    flag = true;
                    dp.Begin = tmp.Begin;
                    if (i == 0)
                    {
                        Head = Head.Next;
                    }
                    else
                    {
                        tmp.Next.Last = tmp.Last;
                        tmp.Last.Next = tmp.Next;
                    }
                    _size--;
                    break;
                }
                else
                {
                    tmp = tmp.Next;
                }
            }
            return flag;
        }

        private bool LoopFirstFit(DynamicProcess dp)
        {
            bool flag = false;
            AssistNode tmp = LastVisitNode ?? Head;
            for (int i = LastVisit; i < Size + LastVisit; i++)
            {
                LastVisit = i % Size;
                if (tmp.Length > dp.Size)
                {
                    flag = true;
                    dp.Begin = tmp.Begin;
                    tmp.Begin += dp.Size;
                    tmp.Length -= dp.Size;
                    break;
                }
                else if (tmp.Length == dp.Size)
                {
                    flag = true;
                    dp.Begin = tmp.Begin;
                    if (i == 0)
                    {
                        Head = Head.Next;
                    }
                    else
                    {
                        tmp.Next.Last = tmp.Last;
                        tmp.Last.Next = tmp.Next;
                    }
                    _size--;
                    break;
                }
                else
                {
                    tmp = tmp.Next ?? Head;
                }
            }
            return flag;
        }

        private bool BestMatch(DynamicProcess dp)
        {
            AssistNode bs = null;
            SortedSet<AssistNode> ss = Sort();
            foreach (AssistNode an in ss)
            {
                if (an.Length >= dp.Size)
                {
                    bs = an;
                    break;
                }

            }
            if (bs == null)
                return false;
            if (bs.Length == dp.Size)
            {
                dp.Begin = bs.Begin;
                bs.Next.Last = bs.Last;
                bs.Last.Next = bs.Next;
                _size--;
            }
            else
            {
                dp.Begin = bs.Begin;
                bs.Begin += dp.Size;
                bs.Length -= dp.Size;
            }
            return true;
            /*
            AssistNode tmp = Head;
            AssistNode bs = null;
            int DIF = int.MaxValue;
            for (int i = 0; i < Size; i++)
            {
                if (tmp.Length >= dp.Size)
                {
                    if (tmp.Length - dp.Size < DIF)
                    {
                        DIF = tmp.Length - dp.Size;
                        bs = tmp;
                    }
                }
                tmp = tmp.Next;
            }
            if (bs == null)
                return false;
            if (DIF == 0)
            {
                dp.Begin = bs.Begin;
                bs.Next.Last = bs.Last;
                bs.Last.Next = bs.Next;
                _size--;
            }
            else
            {
                dp.Begin = bs.Begin;
                bs.Begin += dp.Size;
                bs.Length -= dp.Size;
            }
            return true;*/
        }

        private SortedSet<AssistNode> Sort()
        {
            SortedSet<AssistNode> ss = new SortedSet<AssistNode>();
            AssistNode node = Head;
            while (node != null)
            {
                ss.Add(node);
                node = node.Next;
            }
            return ss;
        }

        public int Size
        {
            get => _size;
        }

        private int LastVisit = 0;
        private AssistNode LastVisitNode = null;
        private int _size;
        private int Sum;
        private AssistNode Head;
    }
}
