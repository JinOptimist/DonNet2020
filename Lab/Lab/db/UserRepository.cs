using Lab.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Lab.db
{
    public class UserRepository : BaseRepository<User>
    {
        public override string FileName { get; } = "Users.txt";

        public User SaveUser(string login, string password)
        {
            var maxId = GetAll().Max(x => x.Id);
            var user = new User();
            user.Login = login;
            user.Password = password;
            user.Role = Role.User;
            user.Id = maxId + 1;

            var json = new JavaScriptSerializer().Serialize(user);
            using (var file = File.Open(FileName, FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }
            }

            return user;
        }
    }
}
