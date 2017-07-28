using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Patrol.Request.Entity
{
    /// <summary>
    /// 特巡报告查询条件映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PatrolSearchInfo
    {
        [DataMember(Name = "ReportDateStart")]
        public string ReportDateStart { get; set; }

        [DataMember(Name = "ReportDateClosed")]
        public string ReportDateClosed { get; set; }

        [DataMember(Name = "ReportStatus")]
        public string ReportStatus { get; set; }

        [DataMember(Name = "AgencyShop")]
        public string AgencyShop { get; set; }

        [DataMember(Name = "Filiale")]
        public string Filiale { get; set; }

        [DataMember(Name = "Reporter")]
        public string Reporter { get; set; }

        [DataMember(Name = "MachineType")]
        public string MachineType { get; set; }

        [DataMember(Name = "Customer")]
        public string Customer { get; set; }

        [DataMember(Name = "MachineNO")]
        public string MachineNO { get; set; }

        [DataMember(Name = "MachineStatus")]
        public string MachineStatus { get; set; }

        [DataMember(Name = "IsEmergency")]
        public string IsEmergency { get; set; }

        [DataMember(Name = "HasErrorImage")]
        public string HasErrorImage { get; set; }

        [DataMember(Name = "LocationCode")]
        public string LocationCode { get; set; }

        [DataMember(Name = "SpotCode")]
        public string SpotCode { get; set; }

        [DataMember(Name = "Remarks")]
        public string Remarks { get; set; }

        [DataMember(Name = "Paginator")]
        public PaginatorInfo Paginator { get; set; }
    }
}
