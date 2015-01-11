using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henge3D
{
    public class ObjectPair
    {
        public object Object1;
        public object Object2;

        public ObjectPair(object o1, object o2)
        {
            Object1 = o1;
            Object2 = o2;
        }

        public ObjectPair()
        {
            Object1 = null;
            Object2 = null;
        }

        public bool IsObj1OfType<T>()
        {
            if (Object1 is T)
                return true;
            return false;
        }

        public bool IsObj2OfType<T>()
        {
            if (Object2 is T)
                return true;
            return false;
        }

        public T CastObj1ToType<T>()
        {
            return (T)Object1;
        }

        public T CastObj2ToType<T>()
        {
            return (T)Object2;
        }
    }
}
