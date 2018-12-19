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
    public class ReportServiceTests
    {
        EventBl validEventBl = new EventBl
        {
            Name = "eventBl",
            CustomerList =
            {
                new CustomerBl
                {
                    Age = 40,
                    UserTicket = new TicketTypeBl {Name = "normal"}
                },
                new CustomerBl
                {
                    Age = 20,
                    UserTicket = new TicketTypeBl {Name = "normal"}
                },
                new CustomerBl
                {
                    Age = 30,
                    UserTicket = new TicketTypeBl {Name = "vip"}
                }
            }
        };

        EventBl invalidEventBl = new EventBl { Name = "invalidEvent" };

        TicketTypeBl validTicketTypeBl = new TicketTypeBl
        {
            Name = "normal",
            Price = 100,
            NumberOfAvailable = 100
        };

        TicketTypeBl invalidTicketTypeBl = new TicketTypeBl
        {
            Name = "custom",
            Price = 100,
            NumberOfAvailable = 100
        };

        [TestMethod]
        public void GetAgeOfOldestParticipant_ValidEvent_HighestValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var act = reportService.GenerateReport(validEventBl).AgeOfOldestParticipant;
            var result = reportService.GetAgeOfOldestParticipant(validEventBl);

            //Assert
            Assert.AreEqual(40, act);
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void GetAgeOfOldestParticipant_InvalidEvent_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var act = reportService.GenerateReport(invalidEventBl).AgeOfOldestParticipant;
            var result = reportService.GetAgeOfOldestParticipant(invalidEventBl);

            //Assert
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, act);
        }

        [TestMethod]
        public void GetAgeOfYoungestParticipant_ValidEvent_LowestValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfYoungestParticipant(validEventBl);

            //Assert
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void GetAgeOfYoungestParticipant_InvalidEvent_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfYoungestParticipant(invalidEventBl);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAverageAge_ValidEvent_CorrectAverageValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAverageAge(validEventBl);

            //Assert
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void GetAverageAge_InvalidEvent_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAverageAge(invalidEventBl);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAgeOfOldestParticipantOfTicektType_ValidEventAndTicketType_CorrectHighestValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfOldestParticipantOfTicektType(validEventBl, validTicketTypeBl);

            //Assert
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void GetAgeOfOldestParticipantOfTicektType_ValidEventAndInvalidTicketType_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfOldestParticipantOfTicektType(validEventBl, invalidTicketTypeBl);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAgeOfYoungestParticipantOfTicektType_ValidEventAndTicketType_CorrectLowestValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfYoungestParticipantOfTicektType(validEventBl, validTicketTypeBl);

            //Assert
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void GetAgeOfYoungestParticipantOfTicektType_ValidEventAndInvalidTicketType_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAgeOfYoungestParticipantOfTicektType(validEventBl, invalidTicketTypeBl);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAverageAgeOfParticipantsOFTicketType_ValidEventAndTicketType_CorrectAverageValue()
        {
            //Assert
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAverageAgeOfParticipantsOFTicketType(validEventBl, validTicketTypeBl);

            //Assert
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void GetAverageAgeOfParticipantsOFTicketType_ValidEventAndInvalidTicketType_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetAverageAgeOfParticipantsOFTicketType(validEventBl, invalidTicketTypeBl);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetTotalIncomeForTicketType_ValidEventAndTicketType_CorrectValue()
        {
            //Assert
            var reportService = new ReportService();

            //Act
            var result = reportService.GetTotalIncomeForTicketType(validEventBl, validTicketTypeBl);

            //Assert
            Assert.AreEqual(200, result);
        }

        [TestMethod]
        public void GetTotalIncomeForTicketType_ValidEventAndInvalidTicketType_ReturnZero()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetTotalIncomeForTicketType(validEventBl, invalidTicketTypeBl);

            //Assert
            Assert.AreEqual(0, result);
        }
    }
}
