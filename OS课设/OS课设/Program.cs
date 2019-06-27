using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class Program
    {
        private static void MainMenu()
        {
            Console.WriteLine("菜单");
            Console.WriteLine("1.固定分配");
            Console.WriteLine("2.可变分配");
            Console.WriteLine("0.退出");
        }
        static void Main(string[] args)
        {
            while (true)
            {
                MainMenu();
                Console.Write("请输入你的选择:");
                int select = int.Parse(Console.ReadLine() ?? "0");
                switch (select)
                {
                    case 1:
                        FixedAllocation();
                        break;
                    case 2:
                        DynamicAllocation();
                        break;
                    case 0:
                        Console.WriteLine("感谢使用");
                        return;
                    default:
                        Console.WriteLine("重新选择");
                        break;
                }
            }
        }

        private static void FixedAllocation()
        {
            Console.Write("输入内存总大小:");
            int sum = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("输入每块内存的大小:");
            int block = int.Parse(Console.ReadLine() ?? "0");
            FixedMemory fm = new FixedMemory(sum, block);
            while (true)
            {
                Console.WriteLine("选择要进行的操作:");
                Console.WriteLine("1.查看当前内存状态");
                Console.WriteLine("2.移除指定进程");
                Console.WriteLine("3.增加新的进程");
                Console.WriteLine("0.返回上一层");
                int select = int.Parse(Console.ReadLine() ?? "0");
                switch (select)
                {
                    case 1:
                        fm.PrintMemoryInfo();
                        break;
                    case 2:
                        Console.WriteLine("输入要移除的进程名字:");
                        string name1 = Console.ReadLine();
                        Console.WriteLine(fm.RemoveProcessByName(name1) ? "移除成功!" : "未找到指定进程!");
                        break;
                    case 3:
                        Console.WriteLine("输入要增加的进程名字:");
                        string name2 = Console.ReadLine();
                        Console.WriteLine("输入该进程所占内存大小:");
                        int Size = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine(fm.AddNewProcess(new FixedProcess(name2, Size)) ? "增加成功!" : "要求内存过大!");
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("输入有误, 重新输入");
                        break;
                }
            }

        }

        private static void DynamicAllocation()
        {        
            Console.Write("输入总内存大小:");
            int sum = int.Parse(Console.ReadLine() ?? "0");
            DynamicMemory dm = new DynamicMemory(sum);
            while (true)
            {
                Console.WriteLine("选择要进行的操作:");
                Console.WriteLine("1.查看当前内存状态");
                Console.WriteLine("2.移除指定进程");
                Console.WriteLine("3.增加新的进程");
                Console.WriteLine("0.返回上一层");
                int select = int.Parse(Console.ReadLine() ?? "0");
                switch (select)
                {
                    case 1:
                        dm.PrintInfo();
                        break;
                    case 2:
                        Console.WriteLine("输入要移除的进程名字:");
                        string name1 = Console.ReadLine();
                        Console.WriteLine(dm.RemoveProcessByName(name1) ? "移除成功!" : "未找到指定进程!");
                        break;
                    case 3:
                        Console.WriteLine("输入要增加的进程名字:");
                        string name2 = Console.ReadLine();
                        Console.WriteLine("输入该进程所占内存大小:");
                        int Size = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("选择分配内存方式:");
                        Console.WriteLine("1.首次适应算法");
                        Console.WriteLine("2.循环首次适应算法");
                        Console.WriteLine("3.最佳适应算法");
                        int sel = int.Parse(Console.ReadLine() ?? "1");
                        AllocationWay way = AllocationWay.首次适应算法;
                        switch (sel)
                        {
                            case 1:
                                way = AllocationWay.首次适应算法;
                                break;
                            case 2:
                                way = AllocationWay.循环首次适应算法;
                                break;
                            case 3:
                                way = AllocationWay.最佳适应算法;
                                break;
                        }
                        Console.WriteLine(dm.AddProcess(new DynamicProcess(name2, Size), way) ? "增加成功!" : "内存空间不足!");
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("输入有误, 重新输入");
                        break;
                }
            }
        }
    }
}
