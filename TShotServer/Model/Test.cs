using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Controller;
using Model.BusinessRule;
using Model.EntityManager;

namespace Model
{
    public class Test
    {
        public static void Main() {
            Console.WriteLine("数据库读取启动");
            Common.GetHash();
            string[] dtt = { "QCR00000", "QCR00001", "QCR00002", "QCR00003", "QCR00004", "QCR00005", "QCR00006" };
            for (int a = 0; a < dtt.Length; a++)
            {
                string reporter = dtt[a];
                for (int i = 0; i < 20; i++)
                {
                    ////测试特巡报告头部
                    TestPatrolReportHeader(reporter);

                }
                
            }
            ////测试点检部位
            //TestPatrolSpotParts();

            ////测试用户信息
            //TestPatrolUserInfo();

            ////测试用户信息
            //TestPatrolReportDetail();

            ////测试数据字典信息
            //TestPatrolCodeMst();

            //测试sql查询
            //PatrolEntity.getPatrolList("","");
            PatrolEntity.getPatrolHeader("PRN2017071800001");
            PatrolEntity.getPatrolDetail("PRN2017071800001");
            //PatrolUserInfoRule.SelectAll();

            ////SQLHelper o = new SQLHelper();
            ////SQLHelper d = new SQLHelper();

            ////PatrolSpotPartsHelper con = new PatrolSpotPartsHelper();
            ////PatrolSpotParts search = new PatrolSpotParts();
            ////search.ID = "root";
            ////PatrolSpotParts psp = con.Select(search).First();
            ////psp.Name = "HelloWorld";
            ////PatrolSpotParts pnew = new PatrolSpotParts();
            ////pnew.Name = "新数据";
            ////con.Update(psp, pnew);

            ////PatrolGenerateNORule pg = new PatrolGenerateNORule();
            
            
            ////string n = pg.GenerateNO("fds");

            //PatrolGenerateNOHelper pn = new PatrolGenerateNOHelper();
            ////PatrolGenerateNO p = new PatrolGenerateNO();
            ////p.PrefixCode = "fds";
            ////p = pn.Select(p);
            ////pg.Delete(p);
            //PatrolGenerateNO p1 = new PatrolGenerateNO();
            //p1.PrefixCode = "fds";
            //PatrolGenerateNO p2 = pn.Select(p1);

            //PatrolGenerateNO p3 = pn.Copy(p2);
            //p3.MaxID = 100;


            //Hashtable t = new Hashtable();
            //t.Add("CurrentID", 1);
            //t.Add("maxid",100);

            //pn.Update(p2, t);
            //SearchInfo s = new SearchInfo();
            //s.CreateSearchInfo(t);
            //pn.SearchByCondition(s);

            Console.Read();
        
        
        }

        private static void SetValue(Hashtable update)
        {

            foreach (var item in update.Keys)
            {
                Console.WriteLine(item.ToString());
            }

        }

        public static PatrolGenerateNORule rule = new PatrolGenerateNORule();


        /// <summary>
        /// 测试数据字典信息
        /// </summary>
        public static void TestPatrolCodeMst()
        {
            PatrolCodeMstHelper ph = new PatrolCodeMstHelper();

            PatrolCodeMst target = new PatrolCodeMst();

            target.CodeCD = "PN00001";
            target.CodeTypeCD = "CTD0001";
            target.CodeTypeName = "LC00001";
            target.CodeName = "LC00001";
            target.CodeValue = "1";
            target.SortCD = 1;

            ph.Insert(target);
            List<PatrolCodeMst> list = ph.SelectAll();
            foreach (PatrolCodeMst item in list)
            {
                Console.WriteLine(item.CodeName);
            }
            ph.Delete(target);
        }

        /// <summary>
        /// 测试特巡报告头部
        /// </summary>
        public static void TestPatrolReportHeader(string report) {
            PatrolReportHeaderHelper ph = new PatrolReportHeaderHelper();
            PatrolReportHeader target = new PatrolReportHeader();
            target.PatrolNO = rule.GenerateNO("PRN");

            target.Contaction1 = "13876486456";
            target.Contaction2 = "15687894851";
            target.ContactorName1 = "王猛";
            target.ContactorName2 = "天龙";
            target.ContactorType1 = "0";
            target.ContactorType2 = "1";
            target.ContactType1 = "1";
            target.ContactType2 = "0";
            target.CreatedAt = DateTime.Now;
            target.Creator = "Admin";
            target.IsAvailable = "1";
            target.IsEmergency = "0";
            target.MakerCD = "01";
            target.MachineNO = "001859";
            target.MachineStatus = "0";
            target.MachineType = "101";
            target.Remarks = "备注信息";
            target.ReportDate = "20170706";
            target.Reporter = report;
            target.ReportStatus = "0";
            target.ReportUri = "http://www.baidu.com";
            target.UpdatedAt = DateTime.Now;
            target.Updator = "admin";
            target.WorkedTimes = new Random().Next(100,500);
            target.WorkNO = DateTime.Now.Millisecond.ToString();

            bool issure = ph.Insert(target);
            if (issure) {
                TestPatrolReportDetail(target.PatrolNO);
            }

            //查询
            //PatrolReportHeader s = ph.Select(target);
        }

        /// <summary>
        /// 测试特巡报告详情
        /// </summary>
        public static void TestPatrolReportDetail(string patrolno)
        {
            PatrolReportDetailHelper ph = new PatrolReportDetailHelper();

            PatrolReportDetail target = new PatrolReportDetail();

            target.PatrolNO = patrolno;
            target.SubNO = 0;
            target.LocationCode = "SP0002";
            target.SpotCode = "SP0002001";
            target.Status = "1";
            target.QuestionLevel = "1";
            target.Remarks = "铭牌点检信息";
            target.PicUrl = "http://www.baidu.com";
            target.IsSelected = "1";
            target.IsImportant = "0";

            ph.Insert(target);
            //List<PatrolReportDetail> list = ph.SelectAll();
            //foreach (PatrolReportDetail item in list)
            //{
            //    Console.WriteLine(item.PatrolNO);
            //}        
        }

        /// <summary>
        /// 测试点检部位信息
        /// </summary>
        public static void TestPatrolSpotParts() { 

            PatrolSpotPartsHelper ph = new PatrolSpotPartsHelper();

            List<PatrolSpotParts> list = ph.SelectAll();
            foreach (PatrolSpotParts item in list)
            {
                Console.WriteLine("id:" + item.ID + " --- name:" + item.Name);
            }
        
        }
        
        /// <summary>
        /// 测试用户数据信息
        /// </summary>
        public static void TestPatrolUserInfo()
        {

            PatrolUserInfoHelper ph = new PatrolUserInfoHelper();

            PatrolUserInfo entity = new PatrolUserInfo();
            entity.UserCD = "123456";
            entity.UserPassword = "888888";
            entity.Token = "xxxxx";
            entity.IsAdmin = "1";
            entity.IsAvailable = "1";
            entity.SearchRange = "0";
            entity.CreatedAt = DateTime.Now;
            entity.Creator = "admin";
            entity.UpdatedAt = DateTime.Now;
            entity.Updator = "admin";

            ph.Insert(entity);
            List<PatrolUserInfo> list = ph.SelectAll();
            foreach (PatrolUserInfo item in list)
            {
                Console.WriteLine(item.UserCD);
            }

        }
    }
}
