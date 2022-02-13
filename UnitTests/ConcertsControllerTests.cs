using ConcertTicketBookingSystemAPI.Controllers;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using ConcertTicketBookingSystemAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ConcertsControllerTests
    {
        [Fact]
        public async void GetConcert_UnexistingClassicConcert_NotFound()
        {
            //Arrange
            var mockSet = new List<Concert>
            {
            }.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.LoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(int.MaxValue);
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async void GetConcert_ExpectedClassicConcert_ReturnsExpectedConcert()
        {
            var expectedConcert = CreateRandomClassicConcert();

            //Arrange
            var mockSet = new List<Concert>
            {
                expectedConcert
            }.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.LoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(expectedConcert.ConcertId);
            //Assert
            Assert.Equal(result.Value.ConcertId, expectedConcert.ConcertId);
        }
        private ClassicConcert CreateRandomClassicConcert()
        {
            Random rand = new Random();
            return new ClassicConcert()
            {
                Compositor = Guid.NewGuid().ToString().Substring(5),
                ConcertDate = DateTime.Now,
                ConcertId = rand.Next(int.MaxValue),
                ConcertName = Guid.NewGuid().ToString().Substring(10),
                Cost = rand.Next(500),
                CreationTime = DateTime.Now,
                IsActiveFlag = true,
                Latitude = rand.NextDouble(),
                Longitude = rand.NextDouble(),
                LeftCount = rand.Next(500),
                Performer = Guid.NewGuid().ToString().Substring(10),
                TotalCount = rand.Next(500),
                VoiceType = Guid.NewGuid().ToString().Substring(10),
                UserId = Guid.NewGuid(),
                PreImage = new byte[100],
                PreImageType = ".jpg",
            };
        }
    }
}
