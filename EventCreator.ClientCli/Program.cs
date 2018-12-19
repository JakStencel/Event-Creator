using EventCreator.Business.Models;
using EventCreator.Business.Services;
using EventCreator.Utility;
using EventCreator.Utility.Display;
using EventCreator.Utility.Helpers;
using System;

namespace EventCreator.ClientCli
{
    internal class Program
    {
        private Menu _menu = new Menu();
        private Menu _menuForLoggedClient = new Menu();
        private UserService _userService = new UserService();
        private EventService _eventService = new EventService();
        private CustomerService _customerService = new CustomerService();
        private TicketTypeService _ticketTypeService = new TicketTypeService();
        private bool displayCommandsForLoggedUsers = false;
        private UserBl activeUser;

        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            _menu.AddCommandToTheDictionary((int)Menu.UserCommand.Register, Register);
            _menu.AddCommandToTheDictionary((int)Menu.UserCommand.Login, LogIn);
            _menuForLoggedClient.AddCommandToTheDictionary((int)Menu.UserCommand.BuyTicket, BuyTicket);
            _menuForLoggedClient.AddCommandToTheDictionary((int)Menu.UserCommand.ShowHistoryOfBuying, ShowHistoryOfBuying);

            while (true)
            {
                int command;
                if (displayCommandsForLoggedUsers)
                {
                    Console.Clear();
                    _menuForLoggedClient.WriteOutMenuForUser();
                    command = IoHelper.GetIntFromUser();

                    try
                    {
                        _menuForLoggedClient.RunCommand(command);
                    }
                    catch
                    {
                        IoHelper.GetKeyFromUser("Run into an error!!! To get back to menu press any key");
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    _menu.WriteOutMenuForUser();
                    command = IoHelper.GetIntFromUser();

                    try
                    {
                        _menu.RunCommand(command);
                    }
                    catch
                    {
                        IoHelper.GetKeyFromUser("Run into an error!!! To get back to menu press any key");
                        continue;
                    }
                }
            }
        }

        private void Register()
        {
            var newUser = new UserBl
            {
                Login = IoHelper.GetStringFromUser("Enter your login: "),
                Password = IoHelper.GetStringFromUser("Enter your password: ")
            };

            _userService.AddUserToUserList(newUser);
            Console.WriteLine("Succesfully registered!!! Log in to buy ticket!");
            IoHelper.GetKeyFromUser("Press any key to go back to the menu...");
        }

        private void LogIn()
        {
            var loginFromUser = IoHelper.GetStringFromUser("Enter your login: ");
            UserBl chosenUser;

            try
            {
                chosenUser = _userService.GetUser(loginFromUser);
                activeUser = chosenUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                IoHelper.GetKeyFromUser("Press any key to go back to the menu...");
                return;
            }

            var passwordFromUser = IoHelper.GetStringFromUser("Enter your password: ");

            while (!_userService.CheckPassword(chosenUser, passwordFromUser))
            {
                Console.WriteLine("You entered the wrong password! ");
                passwordFromUser = IoHelper.GetStringFromUser("Enter your password again: ");
            };

            Console.WriteLine($"Your password is correct{Environment.NewLine}You are logged in!");
            displayCommandsForLoggedUsers = true;
            Console.ReadKey();
        }

        private void BuyTicket()
        {
            var detailsDisplay = new DetailsDisplay();
            EventBl chosenEvent;

            Console.WriteLine("Choose an event on which You want to buy a ticket:");
            detailsDisplay.ShowAllEvents();
            int indexOfEventFromUser = IoHelper.GetIntFromUser();

            try
            {
                chosenEvent = _eventService.GetEvent(indexOfEventFromUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("press any key to go back to main menu...");
                Console.ReadLine();
                return;
            }
            Console.Clear();

            var numberOfTicketsFromUser = detailsDisplay.GetNumerOfTickets(chosenEvent);

            for (int i = 1; i <= numberOfTicketsFromUser; i++)
            {
                Console.Clear();

                var newCustomer = detailsDisplay.CreateCustomer(i);
                var specifiedTicketType = detailsDisplay.ChooseTicketType(chosenEvent);

                if (!_ticketTypeService.CheckAvailbilityOfSpecifiedTypeOfTicket(specifiedTicketType))
                {
                    Console.WriteLine($"The type of the ticket you have chosen is unavailable, " +
                                        $"{Environment.NewLine}declare customer one more time and choose another type of ticket");
                    Console.ReadKey();
                    i--;
                    continue;
                }

                _customerService.AddTicketToCustomer(newCustomer, specifiedTicketType);
                _ticketTypeService.DecreaseNumberOfAvailableTickets(specifiedTicketType);
                //_customerService.AddCustomer(chosenEvent, newCustomer);

                var soldTicket = new TicketBl()
                {
                    ChosenEvent = chosenEvent, 
                    Customer = newCustomer,
                    TypeOfTicket = specifiedTicketType      
                };
                _userService.AddSoldTicket(activeUser, soldTicket);
                _userService.UpdateUser(indexOfEventFromUser, newCustomer, specifiedTicketType, activeUser, soldTicket);
            }
        }

        private void ShowHistoryOfBuying()
        {
            var user = _userService.GetUser(activeUser.Login);
            Console.WriteLine($"Hisotry of purchased tickets:{Environment.NewLine}");
            foreach (var ticket in user.PurchasedTickets)
            {
                Console.WriteLine($"The name of the chosen event: {ticket.ChosenEvent.Name} {Environment.NewLine}" +
                                  $"The type of the ticket: {ticket.TypeOfTicket.Name} {Environment.NewLine}" +
                                  $"The price of the ticket: {ticket.TypeOfTicket.Price} {Environment.NewLine}");
            }
            IoHelper.GetKeyFromUser("Press any key...");
        }
    }
}
