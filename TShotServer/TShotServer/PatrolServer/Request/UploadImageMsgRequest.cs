using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PatrolServer.Message.Request
{
    /// <summary>
    /// 上传图片  接口编号=1004
    /// </summary>
    /// 
    [DataContract]
    [XmlRoot("root")]
    public class UploadImageMsgRequest:BaseRequestMessage
    {
        /// <summary>
        /// 登录的用户名
        /// </summary>
        private string _userid;
        [DataMember]
        [XmlElement("userid")]
        public string Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        /// <summary>
        /// 图片名称
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
        /// 图片文件
        /// </summary>
        private Stream _imageFile;
        [DataMember]
        [XmlElement("imageFile")]
        public Stream ImageFile
        {
            get { return _imageFile; }
            set { _imageFile = value; }
        }


    }
}
