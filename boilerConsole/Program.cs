using boilerConsole.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace boilerConsole
{
    class Program
    {
        private static IConfigurationRoot _iconfiguration;

        static void Main(string[] args)
        {

            GetAppSettingsFile();
            PrintUsers();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void PrintUsers()
        {
            var userDAL = new UserDAL(_iconfiguration);
            var listUserModel = userDAL.GetUserList();

            listUserModel.ForEach(item =>
            {
                Console.WriteLine(item.Name + ' ' + item.Surname);
            });
 
            Console.ReadKey();
        }
    }
}
