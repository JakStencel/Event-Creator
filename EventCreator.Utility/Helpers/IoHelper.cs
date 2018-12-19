using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Utility.Helpers
{
    public static class IoHelper
    {
        public static ConsoleKeyInfo GetKeyFromUser(string message)
        {
            Console.Write(message);
            return Console.ReadKey();
        }

        public static string GetStringFromUser(string message)
        {
            Console.WriteLine(message);
            string inputFromUser = Console.ReadLine();
            return CheckStringFromUSer(inputFromUser);
        }

        public static string GetStringFromUser(List<string> messageList)
        {
            Console.WriteLine(String.Join(Environment.NewLine, messageList));
            string inputFromUser = Console.ReadLine();
            return CheckStringFromUSer(inputFromUser);
        }

        public static bool GetBoolFromUser(string message)
        {
            bool isBool;
            Console.WriteLine(message);
            while(!bool.TryParse(Console.ReadLine(), out isBool))
            {
                Console.WriteLine("Please, enter boolean value (true/false):");
            }
            return isBool;
        }

        public static string CheckStringFromUSer(string inputFromUser)
        {
            while (string.IsNullOrWhiteSpace(inputFromUser))
            {
                Console.WriteLine("Do not leave this field empty! Please, insert a text/characters");
                inputFromUser = Console.ReadLine();
            }
            return inputFromUser;
        }

        public static DateTime GetDateOfEventFormTheUser(string message)
        {
            Console.WriteLine(message);

            DateTime dateOfTheEvent;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                           System.Globalization.DateTimeStyles.None, out dateOfTheEvent))
            {
                Console.WriteLine("You have entered the wrong format of date");
            }
            return dateOfTheEvent;
        }

        public static int GetIntFromUser(string message)
        {
            Console.WriteLine(message);
            return ReturnInt();
        }

        public static int GetIntFromUser()
        {
            return ReturnInt();
        }

        public static int ReturnInt()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please, insert a number!");
            }
            return number;
        }
    }
}
