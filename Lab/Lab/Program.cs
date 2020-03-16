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
            User user = null;
            do
            {
                Console.WriteLine("Login or Registration?");
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Registration");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        user = Login();
                        break;
                    case "2":
                        user = CreateUser();
                        break;
                }
            }
            while (user == null);

            Console.WriteLine($"User role {user.Role}");
            if (user.Role == Role.Admin)
            {
                var users = GetAllUsers();
                WriteUsers(users);
            }

            Console.ReadLine();
        }

        public static void WriteUsers(List<User> users)
        {
            users.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("if whish remove Enter x");
            var option = Console.ReadLine();
            if (option == "x")
            {
                Console.WriteLine("Enter Id users to delete");
                var id = int.Parse(Console.ReadLine());

                var userForDelte = users.FirstOrDefault(x => x.Id == id);
                DeleteUser(userForDelte);
            }
        }

        public static void DeleteUser(User userForDelte)
        {
            var users = GetAllUsers();
            users = users
                .Where(x => x.Id != userForDelte.Id).ToList();

            var jss = new JavaScriptSerializer();

            using (var file = File.Create(UserFileName))
            {
                using (var sw = new StreamWriter(file))
                {
                    users
                        .ForEach(user =>
                            sw.WriteLine(
                                jss.Serialize(user)
                            )
                        );
                }
            }
        }

        public static List<User> GetAllUsers()
        {
            var jss = new JavaScriptSerializer();
            var lines = File.ReadAllLines(UserFileName);
            var usersFast =
                lines.Select(line => jss.Deserialize<User>(line))
                .ToList();

            return usersFast;
        }

        public static User CreateUser()
        {
            Console.WriteLine("Hi what is you name?");
            var login = Console.ReadLine();
            var password = GetPassword();

            var maxId = GetAllUsers().Max(x => x.Id);
            var user = new User();
            user.Login = login;
            user.Password = password;
            user.Role = Role.User;
            user.Id = maxId + 1;

            var json = new JavaScriptSerializer().Serialize(user);
            using (var file = File.Open(UserFileName, FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }
            }

            return user;
        }

        public static User Login()
        {
            Console.WriteLine("Enter login");
            var login = Console.ReadLine();
            var password = GetPassword();

            var users = GetAllUsers();
            var realUser = users.FirstOrDefault(user =>
                user.Login == login
                && user.Password == password);

            if (realUser == null)
            {
                Console.WriteLine("Bad login or pass");
            }
            else
            {
                Console.WriteLine($"Hi {realUser.Login}");
            }

            return realUser;
        }

        public static string GetPassword()
        {
            Console.WriteLine("Enter passwrod");
            string pass = "";
            do
            {
                var key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace
                    && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace
                        && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            return pass;
        }
    }
}
