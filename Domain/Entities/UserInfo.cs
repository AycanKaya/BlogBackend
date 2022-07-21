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
        public Gender gender { get; set; }
        public string Contry { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }

    }
}
