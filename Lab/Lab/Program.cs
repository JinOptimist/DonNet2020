using Lab.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Lab
{
    class Program
    {
        public const string UserFileName = "Users.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Login or Registration?");
            Console.WriteLine("Press 1 to Login");
            Console.WriteLine("Press 2 to Registration");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    CreateUser();
                    break;
            }

            Console.ReadLine();
        }

        public static List<User> GetAllUsers()
        {
            var jss = new JavaScriptSerializer();
            var lines = File.ReadAllLines(UserFileName);
            var usersFast = lines.Select(x => jss.Deserialize<User>(x)).ToList();
            return usersFast;
        }

        public static void CreateUser()
        {
            Console.WriteLine("Hi what is you name?");
            var login = Console.ReadLine();

            Console.WriteLine("Enter Passwrod");
            var password = Console.ReadLine();

            var user = new User();
            user.Login = login;
            user.Password = password;
            user.Id = 1;

            var json = new JavaScriptSerializer().Serialize(user);
            using (var file = File.Open(UserFileName, FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }
            }
        }

        public static void Login()
        {
            Console.WriteLine("Enter login");
            var login = Console.ReadLine();
            Console.WriteLine("Enter passwrod");
            var password = Console.ReadLine();

            var users = GetAllUsers();
            var realUser = users.FirstOrDefault(x =>
                x.Login == login
                && x.Password == password);

            if (realUser == null)
            {
                Console.WriteLine("Bad login or pass");
            }
            else
            {
                Console.WriteLine($"Hi {realUser.Login}");
            }
        }
    }
}
