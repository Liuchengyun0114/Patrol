using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;

namespace PatrolServer
{
    public class CommonInfo
    {

        /// <summary>
        /// 上传图片临时存储文件夹
        /// </summary>
        private static string _DefaultPassword = String.Empty;

        public static string DefaultPassword
        {
            get
            {
                if (CommonInfo._DefaultPassword == String.Empty)
                {
                    CommonInfo._DefaultPassword = System.Configuration.ConfigurationManager.AppSettings["defaultPassword"].ToString();
                }
                return CommonInfo._DefaultPassword;
            }
        }

        /// <summary>
        /// 上传图片临时存储文件夹
        /// </summary>
        private static string _ImageTempUrl = String.Empty;

        public static string ImageTempUrl
        {
            get
            {
                if (CommonInfo._ImageTempUrl == String.Empty)
                {
                    CommonInfo._ImageTempUrl = System.Configuration.ConfigurationManager.AppSettings["imageTempUrl"].ToString();
                }
                return CommonInfo._ImageTempUrl;
            }
        }

        /// <summary>
        /// 上传特巡成功后将临时文件夹移动到此存储文件夹
        /// </summary>
        private static string _ImageSaveUrl = String.Empty;

        public static string ImageSaveUrl
        {
            get
            {
                if (CommonInfo._ImageSaveUrl == String.Empty)
                {
                    CommonInfo._ImageSaveUrl = System.Configuration.ConfigurationManager.AppSettings["imageSaveUrl"].ToString();
                }
                return CommonInfo._ImageSaveUrl;
            }
        }
        /// <summary>
        /// 启动服务前的环境初始化,包括上传图片临时文件夹、存储文件夹的创建
        /// </summary>
        /// <returns>true=创建成功,false=创建失败</returns>
        public static bool Init()
        {
            bool success = false;
            try
            {
                if (!System.IO.Directory.Exists(ImageTempUrl))
                {
                    Directory.CreateDirectory(ImageTempUrl);
                }
                if (!System.IO.Directory.Exists(ImageSaveUrl))
                {
                    Directory.CreateDirectory(ImageSaveUrl);
                }
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

    }
}
