using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatrolServer.Message
{
    public class MSUploadImage : MSBase
    {

        public MSUploadImage()
        {
            this.JSONBody = @"'function_id':'1001','return_flag':'0','return_msg':'操作成功Success'";            
        }

        protected override string Data2JSON()
        {
            return this.JSONBody;
        }

        protected override string Stream2String(Stream data)
        {
            this.Data = data.ToString();
            return this.Data;
        }

    }
}
