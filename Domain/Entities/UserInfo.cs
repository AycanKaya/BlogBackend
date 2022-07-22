using System;
using Domain.Enum;

namespace Domain.Entities
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string Contry { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }

    }
}
