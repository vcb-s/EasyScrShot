using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload.HelpersLib
{
    public class FixedSizedQueue<T> : Queue<T>
    {
        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            while (Count > Size) Dequeue();
        }
    }
}
