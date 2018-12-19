using EventCreator.Business.Models;
using EventCreator.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EventCreator.Business.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void GetAll_ValidEvent_CorrectCustomerListCount()
        {
            //Arrange
            EventBl validEventBl = new EventBl
            {
                Name = "eventBl",
                CustomerList =
                {
                    new CustomerBl
                    {
                        Age = 40,
                        UserTicket = new TicketTypeBl { Name = "normal" }
                    },
                    new CustomerBl
                    {
                        Age = 20,
                        UserTicket = new TicketTypeBl { Name = "normal" }
                    },
                    new CustomerBl
                    {
                        Age = 30,
                        UserTicket = new TicketTypeBl { Name = "vip" }
                    }
                }
            };

            var customerService = new CustomerService();

            //Act
            var result = customerService.GetAll(validEventBl);

            //Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(validEventBl.CustomerList.Any(c => c.Age == 30 && c.UserTicket.Name == "vip"));
            Assert.IsTrue(validEventBl.CustomerList.Any(c => c.Age == 20 && c.UserTicket.Name == "normal"));
            Assert.IsTrue(validEventBl.CustomerList.Any(c => c.Age == 40 && c.UserTicket.Name == "normal"));

        }
    }
}
