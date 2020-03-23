using Lab.db;
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
        private static UserRepository _userRepository;
        private static BoobsRepository _boobsRepository;
        private static UiConsole _uiConsole;

        static void Main(string[] args)
        {
            _userRepository = new UserRepository();
            _boobsRepository = new BoobsRepository();
            _uiConsole = new UiConsole();


            User user = null;
            do
            {
                Console.WriteLine("Login or Registration?");
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Registration");
                Console.WriteLine("Press 3 to create bra");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        user = Login();
                        break;
                    case "2":
                        user = CreateUser();
                        break;
                    case "3":
                        CreateBra();
                        break;
                }
            }
            while (user == null);

            Console.WriteLine($"User role {user.Role}");
            if (user.Role == Role.Admin)
            {
                var users = _userRepository.GetAll();
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
                _userRepository.Delete(userForDelte.Id);
            }
        }

        public static User CreateUser()
        {
            Console.WriteLine("Hi what is you name?");
            var login = Console.ReadLine();
            var password = _uiConsole.GetPassword();

            return _userRepository.SaveUser(login, password);
        }

        public static void CreateBra()
        {
            Console.WriteLine("Enter size?");
            var size = (BoobsSize)int.Parse(Console.ReadLine());
            _boobsRepository.SaveBoobs(size);
        }
       

        public static User Login()
        {
            Console.WriteLine("Enter login");
            var login = Console.ReadLine();
            var password = _uiConsole.GetPassword();

            var users = _userRepository.GetAll();
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
    }
}
