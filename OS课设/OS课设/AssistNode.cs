using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS课设
{
    class AssistNode: IComparable<AssistNode>
    {
        public AssistNode(int begin, int length)
        {
            Begin = begin;
            Length = length;
            Last = null;
            Next = null;
        }

        public int Begin
        {
            get;
            set;
        }

        public int Length
        {
            get;
            set;
        }

        public AssistNode Last
        {
            get;
            set;
        }

        public AssistNode Next
        {
            get;
            set;
        }


        public int CompareTo(AssistNode other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Length.CompareTo(other.Length);
        }
    }
}
