using EventCreator.Business.Models;
using EventCreator.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Tests
{
    [TestClass]
    public class TicketTypeServiceTests
    {
        EventBl validEvent = new EventBl
        {
            TicketTypeList =
            {
                new TicketTypeBl
                {
                    Name = "normal",
                    Price = 120,
                    NumberOfAvailable = 120
                },
                new TicketTypeBl
                {
                    Name = "vip",
                    Price = 240,
                    NumberOfAvailable = 100
                },
                new TicketTypeBl
                {
                    Name = "super vip",
                    Price = 260,
                    NumberOfAvailable = 80
                }
            }
        };

        [TestMethod]
        public void GetAll_ValidEvent_ValidListOfTickets()
        {
            //Arrange
            var ticketTypeService = new TicketTypeService();

            //Act
            var result = ticketTypeService.GetAll(validEvent);

            //Assert
            Assert.AreEqual(validEvent.TicketTypeList, result);
        }

        [TestMethod]
        public void GetAll_InvalidEvent_Null()
        {
            //Arrange
            var invalidEvent = new EventBl() { TicketTypeList = null };
            var ticketTypeService = new TicketTypeService();

            //Act
            var result = ticketTypeService.GetAll(invalidEvent);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetNumberOfTicketsAvailable_ValidEvent_CorrectValue()
        {
            //Arrange
            var ticketTypeService = new TicketTypeService();

            //Act
            var result = ticketTypeService.GetNumberOfTicketsAvailable(validEvent);

            //Assert
            Assert.AreEqual(300, result);
        }
    }
}
