using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataBase.EntityManager
{
    public class UserInfoEntity
    {
        private string _userCD;

        public string UserCD
        {
            get { return _userCD; }
            set { _userCD = value; }
        }
        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        private string _userType;

        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
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

        public UserInfoEntity() { 
            
        }

    }
}
