using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase.EntityManager
{
    public class ReportHeaderEntity
    {

        private string _tshotNo;

        public string TshotNo
        {
            get { return _tshotNo; }
            set { _tshotNo = value; }
        }
        private string _reportDate;

        public string ReportDate
        {
            get { return _reportDate; }
            set { _reportDate = value; }
        }
        private string _reporter;

        public string Reporter
        {
            get { return _reporter; }
            set { _reporter = value; }
        }
        private string _reportStatus;

        public string ReportStatus
        {
            get { return _reportStatus; }
            set { _reportStatus = value; }
        }
        private string _machineType;

        public string MachineType
        {
            get { return _machineType; }
            set { _machineType = value; }
        }
        private string _machineNO;

        public string MachineNO
        {
            get { return _machineNO; }
            set { _machineNO = value; }
        }
        private float _workedTimes;

        public float WorkedTimes
        {
            get { return _workedTimes; }
            set { _workedTimes = value; }
        }
        private string _machineStatus;

        public string MachineStatus
        {
            get { return _machineStatus; }
            set { _machineStatus = value; }
        }
        private string _isEmergency;

        public string IsEmergency
        {
            get { return _isEmergency; }
            set { _isEmergency = value; }
        }
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private string _contactor1;

        public string Contactor1
        {
            get { return _contactor1; }
            set { _contactor1 = value; }
        }
        private string _contactor2;

        public string Contactor2
        {
            get { return _contactor2; }
            set { _contactor2 = value; }
        }
        private string _workNO;

        public string WorkNO
        {
            get { return _workNO; }
            set { _workNO = value; }
        }
        private string _reportUri;

        public string ReportUri
        {
            get { return _reportUri; }
            set { _reportUri = value; }
        }
        private string _isAvailable;

        public string IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
        private string _creator;

        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }
        private DateTime _createdAt;

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }
        private string _updator;

        public string Updator
        {
            get { return _updator; }
            set { _updator = value; }
        }
        private DateTime _updatedAt;

        public DateTime UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }
        public ReportHeaderEntity() { }
    }
}
