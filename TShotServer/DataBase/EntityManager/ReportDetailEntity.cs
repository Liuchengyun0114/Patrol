using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase.EntityManager
{
    public class ReportDetailEntity
    {
        private string _tshotNO;

        public string TshotNO
        {
            get { return _tshotNO; }
            set { _tshotNO = value; }
        }
        private int _subNO;

        public int SubNO
        {
            get { return _subNO; }
            set { _subNO = value; }
        }
        private string _locationCode;

        public string LocationCode
        {
            get { return _locationCode; }
            set { _locationCode = value; }
        }
        private string _spotCode;

        public string SpotCode
        {
            get { return _spotCode; }
            set { _spotCode = value; }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _questionLevel;

        public string QuestionLevel
        {
            get { return _questionLevel; }
            set { _questionLevel = value; }
        }
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private string _picUrl;

        public string PicUrl
        {
            get { return _picUrl; }
            set { _picUrl = value; }
        }
        private string _isSelected;

        public string IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }
        public ReportDetailEntity() { }
    }
}
