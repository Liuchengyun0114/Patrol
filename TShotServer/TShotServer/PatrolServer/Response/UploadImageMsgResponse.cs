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
    /// 上传图片  接口编号=1004
    /// </summary>
    /// 
    [DataContract]
    [XmlRoot("root")]
    public class UploadImageMsgResponse:BaseResponseMessage
    {
        /// <summary>
        /// 文件名称，必须与入口参数的名称保持一致
        /// </summary>
        private string _imageName;
        [DataMember]
        [XmlElement("imageName")]
        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        /// <summary>
        /// 对应图片的地址
        /// </summary>
        [DataMember]
        private string _picUrl;
        [XmlElement("picUrl")]
        public string PicUrl
        {
            get { return _picUrl; }
            set { _picUrl = value; }
        }
    }
}
