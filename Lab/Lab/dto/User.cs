using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.dto
{
    public class User : BaseDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}. Log: {Login}. Pass: {Password}. Role: {Role.ToString()}";
        }
    }

    public enum Role
    {
        User = 1,
        Admin = 2,
        Moderator = 42
    }
}
