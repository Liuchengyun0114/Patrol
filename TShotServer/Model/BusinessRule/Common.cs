using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.BusinessRule
{
    /// <summary>
    /// 业务通用类 定义常量
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 表主键生成器对象
        /// </summary>
        private static PatrolGenerateNORule _Genetor;

        public static PatrolGenerateNORule Genetor
        {
            get
            {
                if (_Genetor == null)
                {
                    _Genetor = new PatrolGenerateNORule();
                }
                return Common._Genetor;
            }
        }

        /// <summary>
        /// Token有效期以小时数计算
        /// </summary>
        private static Double _TokenLife = 0;

        public static Double TokenLife
        {
            get {
                if (_TokenLife <= 0)
                {
                    try
                    {
                        _TokenLife = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["tokenLife"].ToString());
    
                    }
                    catch (Exception ex)
                    {
                        _TokenLife = 24;
                        Console.WriteLine(ex.Message);
                    }
                }
                return Common._TokenLife; }
        }

        public static long GetHash() {
            string data= "HelloWorld";
            int a = 45645;
            int b = 87945;
            long hash = 0;
            Random r = new Random(DateTime.Now.Millisecond);
            char[] d = data.ToCharArray();
            for (int i = 0; i < d.Length; i++)
            {
                hash = hash * a + d[i];
                a = a * b + r.Next(10000,99999);
            }
            return Math.Abs(hash);
        }
    }
}
