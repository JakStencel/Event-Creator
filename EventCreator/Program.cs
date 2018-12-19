using EventCreator.FileManager;
using EventCreator.Business.Models;
using EventCreator.Business.Report;
using EventCreator.Business.Services;
using EventCreator.Utility;
using EventCreator.Utility.Display;
using EventCreator.Utility.Helpers;
using System;

namespace EventCreator
{
    public class Program
    {
        private Menu _menu = new Menu();
        private CustomerService _customerService = new CustomerService();
        private EventService _eventService = new EventService();
        private TicketTypeService _ticketTypeService = new TicketTypeService();
        private ReportService _reportService = new ReportService();
        private StatisticsDisplay _statisticDisplay = new StatisticsDisplay();
        private DetailsDisplay detailsDisplayCli = new DetailsDisplay();

        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            _menu.AddCommandToTheDictionary((int)Menu.Commands.CreateAnEvent, CreateAnEvent);
            _menu.AddCommandToTheDictionary((int)Menu.Commands.ShowAllEvents, ShowAllEvents);
            _menu.AddCommandToTheDictionary((int)Menu.Commands.BuyTicket, BuyTicket);
            _menu.AddCommandToTheDictionary((int)Menu.Commands.DetailsOfChosenEvent, ShowDetailsOfChosenEvent);
            _menu.AddCommandToTheDictionary((int)Menu.Commands.Exit, Exit);

            while (true)
            {
                Console.Clear();
                _menu.PrintOutAllMenu();

                var command = IoHelper.GetIntFromUser();
                try
                {
                    _menu.RunCommand(command);
                }
                catch
                {
                    Console.WriteLine($"Invalid index was provided!!! {Environment.NewLine}to get back to menu press any key");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        private void CreateAnEvent()
        {
            var newEventBl = new EventBl
            {
                Name = IoHelper.GetStringFromUser("Enter the name of the event"),
                Date = IoHelper.GetDateOfEventFormTheUser("Enter the date od the event in the following format: 'dd.mm.yyyy'"),
                Description = IoHelper.GetStringFromUser("Enter description of the event")
            };

            int numberOfTypesOfTicketsFromUser = IoHelper.GetIntFromUser("Enter the number of types of tickets: ");

            for (int i = 1; i <= numberOfTypesOfTicketsFromUser; i++)
            {
                var newTicketBl = new TicketTypeBl
                {
                    Name = IoHelper.GetStringFromUser($"Enter the name of the ticket type number {i}: "),
                    Price = IoHelper.GetIntFromUser($"Enter the price of the ticket type number {i}: "),
                    NumberOfAvailable = IoHelper.GetIntFromUser($"Enter the number of tickets available: ")
                };

                try
                {
                    _ticketTypeService.AddTicketType(newEventBl, newTicketBl);
                }
                catch
                {
                    Console.WriteLine("Adding ticket type failed, try again!");
                    i--;
                }
            }

            try
            {
                _eventService.AddEvent(newEventBl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("press any key to go back to main menu");
                Console.ReadKey();
            }
        }

        private void ShowAllEvents()
        {
            var listOfEventsBl = _eventService.GetAll();

            foreach (var @event in listOfEventsBl)
            {
                if (!_ticketTypeService.CheckAvailbilityForEvent(@event))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ModelDisplay.DisplayEvent(@event);
                    Console.ResetColor();
                }
                else
                {
                    ModelDisplay.DisplayEvent(@event);
                }
            }
            IoHelper.GetKeyFromUser("Press any key to continue");
        }

        public void BuyTicket()
        {
            EventBl chosenEvent;
            Console.WriteLine("Choose an event on which You want to buy a ticket:");
            detailsDisplayCli.ShowAllEvents();
            int indexOfEventFromUser = IoHelper.GetIntFromUser();

            try
            {
                chosenEvent = _eventService.GetEvent(indexOfEventFromUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("press any key to go back to main menu");
                Console.ReadLine();
                return;
            }
            Console.Clear();

            var numberOfTicketsFromUser = detailsDisplayCli.GetNumerOfTickets(chosenEvent);

            for (int i = 1; i <= numberOfTicketsFromUser; i++)
            {
                Console.Clear();

                var newCustomer = detailsDisplayCli.CreateCustomer(i);
                var specifiedTicketType = detailsDisplayCli.ChooseTicketType(chosenEvent);

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
                _eventService.UpdateEvent(indexOfEventFromUser, newCustomer, specifiedTicketType);
            }
        }

        private void ShowDetailsOfChosenEvent()
        {
            Console.WriteLine($"Choose an event that details you want to display {Environment.NewLine}");
            detailsDisplayCli.ShowAllEvents();

            int indexOfEventFromUser = IoHelper.GetIntFromUser();
            Console.Clear();

            var chosenEvent = _eventService.GetEvent(indexOfEventFromUser);

            Console.WriteLine($"Below you can see the details of the chosen event: {Environment.NewLine}");

            ModelDisplay.DisplayEvent(chosenEvent);

            var eventReport = new ReportService().GenerateReport(chosenEvent);

            _statisticDisplay.ShowStatsForEvent(eventReport);

            var listOfTicketTypes = _ticketTypeService.GetAll(chosenEvent);

            Console.WriteLine($"Types of tickets with their details: {Environment.NewLine}");

            foreach (var ticketType in listOfTicketTypes)
            {
                ModelDisplay.DisplayTicketType(ticketType);
                _statisticDisplay.ShowStatsForSpecifiedTicketType(eventReport.TicketTypePoolReports[listOfTicketTypes.IndexOf(ticketType)]);
            }

            _statisticDisplay.ShowNumberOfAvailableTickets(chosenEvent);    

            Console.WriteLine($"The list of customers and types of tickets according to the chosen event:{Environment.NewLine}");
            var listOfCustomers = _customerService.GetAll(chosenEvent);

            foreach (var customer in listOfCustomers)
            {
                ModelDisplay.DisplayCustomer(customer);
            }

            var shouldExportToFile = IoHelper.GetBoolFromUser("If want to export to json file, key in 'true', otherwise key in false");
            if (shouldExportToFile)
            {
                var fileName = IoHelper.GetStringFromUser("Enter the name of the file (with extension .json):");
                new JsonFileManager().SaveFile(fileName, eventReport);
            }

            IoHelper.GetKeyFromUser("Press any key to continue");
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }

}
