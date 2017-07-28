using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace PatrolServer.Message.Response
{
    /// <summary>
    /// 上传特巡  接口编号=1005
    /// </summary>
    /// 
    [DataContract]
    [XmlRoot("root")]
    public class UploadPatrolInformationMsgResponse:BaseResponseMessage
    {
        /// <summary>
        /// 文件名称，必须与入口参数的名称保持一致
        /// </summary>
        private string _patrolId;
        [DataMember]
        [XmlElement("patrolId")]
        public string PatrolId
        {
            get { return _patrolId; }
            set { _patrolId = value; }
        }
    }
}
