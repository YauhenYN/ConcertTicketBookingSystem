using ConcertTicketBookingSystemAPI.Controllers;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ConcertsControllerTests
    {
        [Fact]
        public async void GetConcertClassic_UnexistingType_BadRequest()
        {
            //Arrange
            var controller = new ConcertsController(StaticInstances.LoggerMock.Object, StaticInstances.ApplicationContextMock.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(1, ConcertType.PartyConcert + 4);
            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async void GetConcert_UnexistingClassicConcert_NotFound()
        {
            //Arrange
            var mockSet = new List<ClassicConcert>
            {
            }.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.ClassicConcerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.LoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(int.MaxValue, ConcertType.ClassicConcert);
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
