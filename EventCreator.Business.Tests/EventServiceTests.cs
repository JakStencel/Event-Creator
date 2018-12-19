using EventCreator.Business.Models;
using EventCreator.Business.Services;
using EventCreator.Data.Models;
using EventCreator.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Tests
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public void GetAll_ValidListOfEvents_ReturnListOfEvents()
        {
            //Arrange
            var validEventList = new List<Event>
            {
                new Event
                {
                    Name = "Event1"
                },
                new Event
                {
                    Name = "Event2"
                }
            };

            var validEvent = new Event { Name = "event" };
            var validEventBl = new EventBl { Name = "eventBl" };

            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(x => x.GetAll()).Returns(validEventList);

            var dataObjectMapperMock = new Mock<IDataObjectMapper>();
            dataObjectMapperMock.Setup(x => x.MapEventToEventBl(validEvent)).Returns(validEventBl);

            var eventService = new EventService(eventRepositoryMock.Object, dataObjectMapperMock.Object);

            //Act
            var result = eventService.GetAll();

            //Assert
            Assert.AreEqual(result.Count, validEventList.Count);
        }

        [TestMethod]
        public void GetEvent_IndexOfEventFromUser_ValidEvent()
        {
            //Arrange
            var validEvent = new Event { Name = "event" };
            var validEventBl = new EventBl { Name = "event" };

            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(validEvent);

            var dataObjectMapperMock = new Mock<IDataObjectMapper>();
            dataObjectMapperMock.Setup(x => x.MapEventToEventBl(validEvent)).Returns(validEventBl);

            var eventService = new EventService(eventRepositoryMock.Object, dataObjectMapperMock.Object);

            //Act
            var result = eventService.GetEvent(1);

            //Assert
            Assert.AreEqual(validEventBl, result);
        }
    }
}
