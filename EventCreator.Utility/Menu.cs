using System;
using System.Collections.Generic;

namespace EventCreator.Utility
{
    public class Menu
    {
        public enum Commands { CreateAnEvent = 1, ShowAllEvents, BuyTicket, DetailsOfChosenEvent, Exit }
        public enum UserCommand { Register = 1, Login, BuyTicket, ShowHistoryOfBuying }

        private Dictionary<int, Action> _menuActionDictionary
            = new Dictionary<int, Action>();


        public void AddCommandToTheDictionary(int nameOfCommand, Action action)
        {
            _menuActionDictionary.Add(nameOfCommand, action);
        }

        public void RemoveCommandFromTheDictionary(int nameOfCommand)
        {
            _menuActionDictionary.Remove(nameOfCommand);
        }

        public void RunCommand(int nameOfCommand)
        {
            _menuActionDictionary[nameOfCommand]();
        }

        public void PrintOutAllMenu()
        {
            Console.WriteLine("Choose an option from below:");
            foreach (var command in _menuActionDictionary.Keys)
            {
                Commands order = (Commands)Enum.ToObject(typeof(Commands), command);
                Console.WriteLine($"{command}) To {order}, please key in '{command}'");
            }
        }

        public void WriteOutMenuForUser()
        {
            Console.WriteLine("Choose an option from below:");
            foreach (var command in _menuActionDictionary.Keys)
            {
                UserCommand order = (UserCommand)Enum.ToObject(typeof(UserCommand), command);
                Console.WriteLine($"{command}) To {order}, please key in '{command}'");
            }
        }
    }
}
