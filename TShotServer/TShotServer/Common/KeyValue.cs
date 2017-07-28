using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatrolServer.Common
{
    /// <summary>
    /// 键值对对象
    /// </summary>
    public class KeyValue<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
    }
}
