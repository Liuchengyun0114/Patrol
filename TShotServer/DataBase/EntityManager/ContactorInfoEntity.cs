using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase.EntityManager
{
    public class ContactorInfoEntity
    {
        public List<string> ColumnsName = new List<string>{ "ContactNO", "ContactorType", "ContactorName" };
        private string _contactNO;

        public string ContactNO
        {
            get { return _contactNO; }
            set { _contactNO = value; }
        }

        private string _contactorType;

        public string ContactorType
        {
            get { return _contactorType; }
            set { _contactorType = value; }
        }
        private string _contactorName;

        public string ContactorName
        {
            get { return _contactorName; }
            set { _contactorName = value; }
        }
        private string _contactType;

        public string ContactType
        {
            get { return _contactType; }
            set { _contactType = value; }
        }
        private string _contaction;

        public string Contaction
        {
            get { return _contaction; }
            set { _contaction = value; }
        }

        public ContactorInfoEntity() {
        }
    }
}
