using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Threading.Tasks
{
    class ParallelOptions
    {
        public int MaxDegreeOfParallelism { get; set; }
    }

    class Parallel
    {
        public static void Invoke(ParallelOptions options, params Action[] actions)
        {
        }
    }
}
