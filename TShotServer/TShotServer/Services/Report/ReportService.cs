using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Net;
using Model;
using Model.BusinessRule;
using PatrolServer.Services.Report.Request;
using PatrolServer.Services.Report.Response;
using PatrolServer.Services.Report.Request.Entity;
using Model.EntityManager;
using System.ServiceModel.Channels;

namespace PatrolServer.Services.Report
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ReportService”。
    public class ReportService : IReportService
    {        
        /// <summary>
        /// 根据随机种子生成一个随机文件名,确保唯一
        /// </summary>
        /// <param name="seed">随机种子</param>
        /// <returns>日期+随机数+Guid</returns>
        public static string GenerateFileName(int seed)
        {
            Random r = new Random(seed);
            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string guid = Guid.NewGuid().ToString();

            return date + time + r.Next(10000, 100000).ToString() + guid;
        }
        /// <summary>
        /// 根据随机种子生成一个随机文件名,确保唯一
        /// </summary>
        /// <param name="seed">随机种子</param>
        /// <returns>日期+随机数+Guid</returns>
        public static string GenerateDirName(int seed)
        {
            Random r = new Random(seed);
            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            return date + time + r.Next(10000000, 100000000).ToString();
        }

        /// <summary>
        /// 上传特巡图片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stream UploadImage(Stream data)
        {
            ResUploadImage response = new ResUploadImage();
            MemoryStream source = new MemoryStream();
            //FileStream fst = new FileStream("uploaddata.txt",FileMode.Open,FileAccess.ReadWrite);
            try
            {
                data.CopyTo(source);
                source.Position = 0;
                ReqUploadImage req = new ReqUploadImage();

                List<MultipartParser> list = new List<MultipartParser>();
                MultipartParser parser = new MultipartParser(source, 0);
                while (parser.Success)
                {
                    list.Add(parser);
                    int startPoint = parser.EndPoint + 2;
                    source.Position = startPoint;
                    parser = new MultipartParser(source, startPoint);                    
                }
                //保存图片
                List<Response.Entity.PicInfo> imageList = new List<Response.Entity.PicInfo>();
                if (list != null && list.Count > 0)
                {
                    string dirName = GenerateDirName(DateTime.Now.Millisecond);

                    req.user_id = Encoding.UTF8.GetString(list[0].FileContents);
                    req.token = Encoding.UTF8.GetString(list[1].FileContents);

                    Console.WriteLine(req.user_id);
                    Console.WriteLine(req.token);
                    if (req.user_id != null && req.user_id != String.Empty && req.token != null && req.token != String.Empty)
                    {

                        for (int i = 2; i < list.Count; i++)
                        {
                            MultipartParser item = list[i];
                            Console.WriteLine(item.Filename +" --- " + item.FileContents.Length);
                            Response.Entity.PicInfo image = new Response.Entity.PicInfo();
                            string filename = Save2File(dirName, item.FileContents);
                            image.image_name = item.Filename;
                            image.pic_url = filename;
                            imageList.Add(image);
                        }

                        response.model_list = imageList;
                        response.dir_name = dirName;
                        response.SetSuccess();
                    }
                }

                #region 注释
                ////重置流位置
                //source.Position = 0;
                //BinaryReader br = new BinaryReader(source, Encoding.UTF8);

                //byte[] dataSource = source.ToArray();

                //int intBoundary = 0;
                ////索引值
                //for (int i = 0; i < dataSource.Length - 2; i++)
                //{
                //    if (dataSource[i] == 13 && dataSource[i + 1] == 10)
                //    {
                //        intBoundary = i + 1;
                //        break;
                //    }
                //}

                ////获得分隔符
                //source.Position = 0;
                //byte[] boundary = br.ReadBytes(intBoundary + 1);

                ////头部数据
                //int bodyStart = 0;
                //for (int i = intBoundary + 1; i < dataSource.Length - 2; i++)
                //{
                //    if (dataSource[i] == 13 && dataSource[i + 1] == 10 && dataSource[i + 2] == 13 && dataSource[i + 3] == 10)
                //    {
                //        bodyStart = i + 3;
                //        break;
                //    }
                //}

                ////解析头部数据
                //source.Position = intBoundary + 1;
                //byte[] hdata = br.ReadBytes(bodyStart - 4 - intBoundary);

                //string sdata = Encoding.UTF8.GetString(hdata);

                ////获取下一个分隔符位置 来取得数据体范围
                //int bodyEnd = 0;
                //source.Position = bodyStart + 1;

                ////最后一个分隔符多了一个\r\n
                //long dataEnd = dataSource.Length - 2 - boundary.Length; //最后一个分隔符 除去
                //for (int i = bodyStart + 1; i < dataEnd; i++)
                //{
                //    byte[] temp = br.ReadBytes(boundary.Length);
                //    if (temp == boundary)
                //    {
                //        bodyEnd = i - 1;
                //        break;
                //    }
                //}
                ////表单数据体
                //source.Position = bodyStart + 1;
                //byte[] formData = br.ReadBytes(bodyEnd - bodyStart);
                //int nextStart = bodyEnd + boundary.Length + 1;
                
                ////图片数据
                //getImageData(source, nextStart);

                #endregion
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            source.Close();
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResUploadImage));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;
        }
        
        /// <summary>
        /// 上传特巡报告
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stream UploadPatrolInformation(Stream data)
        {
            #region 查找数据
            ResUploadPatrolInformation response = new ResUploadPatrolInformation();
            bool success = false;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqUploadPatrolInformation));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqUploadPatrolInformation request = new ReqUploadPatrolInformation();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqUploadPatrolInformation;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        #region **********（此处加入代码） 根据查询条件 加入业务逻辑代码*************
                        // 身份验证
                        if (request.user_id != String.Empty && request.token != String.Empty)
                        {
                            PatrolReportHeader header = ReportService.getHeader(request);
                            List<PatrolReportDetail> detailList = ReportService.getDetailList(request, header, request.dir_name);

                            success = PatrolEntity.InsertPatrol(header, detailList);
                            if (success)
                            {
                                //将点检图片存到正式文件夹
                                if (request.dir_name != null && request.dir_name != String.Empty)
                                {
                                    string saveDir = CommonInfo.ImageSaveUrl + "/" + request.dir_name;
                                    string tempDir = CommonInfo.ImageTempUrl + "/" + request.dir_name;
                                    if (Directory.Exists(tempDir))
                                    {
                                        if (success)
                                        {
                                            //将临时文件夹拷贝到正式文件夹
                                            Directory.Move(tempDir, saveDir);
                                        }
                                        else {
                                            //删除临时文件夹内容
                                            Directory.Delete(tempDir, true);
                                        }
                                    }
                                }
                            }
                            success = true;
                            //事务成功完成
                            response.patrol_id = request.patrol_id;
                        }
                        #endregion
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResUploadPatrolInformation));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion

        }

        public Stream ShowInfo()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes("Report Test"));
        }

        #region 辅助方法

        /// <summary>
        /// 取得特巡报告头部数据
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static PatrolReportHeader getHeader(ReqUploadPatrolInformation source) {
            PatrolReportHeader ret = new PatrolReportHeader();

            //前端获取
            ret.Province = source.province;
            ret.City = source.city;
            ret.Address = source.address;
            ret.Contaction1 = source.phone_number1;
            ret.Contaction2 = source.phone_number2;
            ret.ContactorName1 = source.contactor_name1;
            ret.ContactorName2 = source.contactor_name2;
            ret.ContactorType1 = source.phone_type_code1;
            ret.ContactorType2 = source.phone_type_code2;
            ret.ContactType1 = source.contact_type_code1;
            ret.ContactType2 = source.contact_type_code2;

            DateTime date = DateTime.Now;
            ret.Creator = source.user_id;
            ret.IsEmergency = source.is_urgent;
            ret.MachineNO = source.model_id;
            ret.MachineStatus = source.model_status_code;
            ret.MachineType = source.model_code;
            ret.Remarks = source.remarks;
            ret.Reporter = source.user_id;
            ret.ReportUri = "";
            ret.WorkedTimes = Convert.ToDouble(source.worked_times);

            //自动设置
            ret.PatrolNO = PatrolReportHeaderRule.GenerateNO();
            ret.IsAvailable = "1";
            ret.MakerCD = "01";
            ret.ReportStatus = "1";
            ret.ReportDate = date.ToString("yyyyMMdd");
            ret.CreatedAt = date;

            return ret;
        }

        /// <summary>
        /// 取得特巡点检详细信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static List<PatrolReportDetail> getDetailList(ReqUploadPatrolInformation source,PatrolReportHeader header,String dirName)
        {
            List<PatrolReportDetail> list = new List<PatrolReportDetail>();

            if (source != null && source.check_list != null && source.check_list.Count > 0 && header != null)
            {
                for (int i = 0; i < source.check_list.Count; i++)
                {
                    PicInfo item = source.check_list[i];
                    PatrolReportDetail instance = new PatrolReportDetail();
                    //前端获取
                    instance.IsImportant = item.is_important;
                    instance.LocationCode = item.location_code;
                    instance.SpotCode = item.spot_code;
                    instance.Status = item.spot_status_code;
                    instance.Remarks = item.part_remarks;
                    instance.QuestionLevel = item.level_code;
                    instance.PicUrl = dirName + "/"+ item.pic_url;
                    Console.WriteLine(instance.PicUrl);
                    //自动设置
                    instance.PatrolNO = header.PatrolNO;
                    instance.SubNO = i;
                    instance.IsSelected = "0";

                    //加入列表
                    list.Add(instance);
                }
            }
            return list;
        }

        private static bool getImageData(MemoryStream source, int start)
        {
            BinaryReader br = new BinaryReader(source, Encoding.UTF8);

            byte[] dataSource = source.ToArray();

            try
            {
                //设置流位置
                source.Position = start;
                int intBoundary = 0;
                for (int i = start; i < dataSource.Length - 2; i++)
                {
                    if (dataSource[i] == 13 && dataSource[i + 1] == 10)
                    {
                        intBoundary = i + 1;
                        break;
                    }
                }

                //获得分隔符
                source.Position = start;
                byte[] boundary = br.ReadBytes(intBoundary + 1);

                //头部数据
                int bodyStart = 0;
                for (int i = start + intBoundary + 1; i < dataSource.Length - 2; i++)
                {
                    if (dataSource[i] == 13 && dataSource[i + 1] == 10 && dataSource[i + 2] == 13 && dataSource[i + 3] == 10)
                    {
                        bodyStart = i + 3;
                        break;
                    }
                }

                //解析头部数据
                source.Position = start + intBoundary + 1;
                byte[] hdata = br.ReadBytes(bodyStart - 4 - intBoundary + 1); //减去\r\n\r\n
                string sdata = Encoding.UTF8.GetString(hdata);

                //获取下一个分隔符位置 来取得数据体范围
                int bodyEnd = 0;
                source.Position = start + bodyStart + 1;

                //最后一个分隔符多了一个\r\n
                long dataEnd = dataSource.Length - 2 - boundary.Length; //最后一个分隔符 除去
                for (int i = bodyStart + 1; i < dataEnd; i++)
                {
                    byte[] temp = br.ReadBytes(boundary.Length);
                    if (temp == boundary)
                    {
                        bodyEnd = i - 1;
                        break;
                    }
                }

                //如果取得数据,最后一个文件块
                if (bodyEnd == 0)
                {
                    bodyEnd = (int)dataEnd;
                }

                //处理数据体
                source.Position = start + bodyStart + 1;
                byte[] dataBuffer = br.ReadBytes(bodyEnd - bodyStart + 1);
                //取得文件名
                string fileinfo = Save2File("Helloworld",dataBuffer);


                int nextStart = (int)source.Position + boundary.Length + 1;
                if (bodyEnd != dataEnd)
                {
                    getImageData(source, nextStart);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 字节数组存储为文件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Save2File(string dirname,byte[] data) {
            string fileName = GenerateFileName(DateTime.Now.Millisecond);
            string fileExt = ".jpg";
            string fileFullName = fileName + fileExt;
            string tempDir = CommonInfo.ImageTempUrl + "/" + dirname;
            try
            {
                //创建临时文件夹，以日期为名称
                if (!Directory.Exists(tempDir))
                {
                    Console.WriteLine("新创建" + tempDir);
                    Directory.CreateDirectory(tempDir);
                }
                FileStream fs = new FileStream(tempDir + "/" + fileFullName, FileMode.Create, FileAccess.ReadWrite);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }
            Console.WriteLine("图片保存成功");
            return fileFullName;
        
        }


        #endregion



        public Stream UploadImageOptions(Stream data)
        {
            return null;
        }

        public Stream UploadPatrolInformationOptions(Stream data)
        {
            return null;
        }
    }
}
