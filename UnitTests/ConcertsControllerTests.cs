using ConcertTicketBookingSystemAPI.Controllers;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using ConcertTicketBookingSystemAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(int.MaxValue);
            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async void GetConcert_ExpectedClassicConcert_ReturnsExpectedConcert()
        {
            //Arrange
            var expectedConcert = CreateRandomClassicConcert();
            var mockSet = new List<Concert>
            {
                expectedConcert
            }.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetConcertAsync(expectedConcert.ConcertId);
            //Assert
            Assert.Equal(result.Value.ConcertId, expectedConcert.ConcertId);
        }
        [Fact]
        public async void GetManyLightConcerts_ExpectedFiveFirstLightConcerts_ReturnsExpectedConcerts()
        {
            var list = new List<Concert>();
            for (int step = 0; step < 10; step++) list.Add(CreateRandomClassicConcert());
            //Arrange
            var mockSet = list.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.GetManyLightConcertsAsync(new ConcertSelectParametersDto() { FromPrice = 0, UntilPrice = int.MaxValue, NextPage = 1, NeededCount = 5 });
            //Assert
            result.Value.CurrentPage.Should().Be(1);
            result.Value.PagesCount.Should().Be(2);
            result.Value.Concerts.Should().BeEquivalentTo(list.Take(5), options => options.ExcludingMissingMembers());
        }
        [Fact]
        public async void AddClassicConcert_ExpectedAddedConcert_ReturnsAddedResult()
        {
            var from = CreateRandomClassicConcert();
            from.ConcertId = 0;
            var itemToCreate = new AddConcertDto()
            {
                ClassicConcertInfo = new ClassicConcertDto
                {
                    Compositor = from.Compositor,
                    ConcertName = from.ConcertName,
                    VoiceType = from.VoiceType
                },
                ConcertType = ConcertType.ClassicConcert,
                Cost = from.Cost,
                ConcertDate = from.ConcertDate,
                IsActiveFlag = from.IsActiveFlag,
                Latitude = from.Latitude,
                Longitude = from.Longitude,
                Performer = from.Performer,
                Image = new byte[] {0, 5, 1, 20},
                ImageType = ".jpg",
                TotalCount = from.TotalCount,
            };
            var context = StaticInstances.ApplicationContextMock;
            var concertsSet = new List<ClassicConcert>()
            {
                CreateRandomClassicConcert(),
                CreateRandomClassicConcert()
            }.AsQueryable().BuildMockDbSet();
            var imagesSet = new List<Image>().AsQueryable().BuildMockDbSet();
            context.Setup(context => context.ClassicConcerts).Returns(concertsSet.Object);
            context.Setup(context => context.Images).Returns(imagesSet.Object);
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            var claims = new[]
{
                   new Claim(ClaimsIdentity.DefaultNameClaimType, Guid.NewGuid().ToString()),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(ConcertTicketBookingSystemAPI.JwtAuth.AuthOptions.GetIsAdminIdentity(Guid.NewGuid(), false));
            //Act
            var result = await controller.AddConcertAsync(itemToCreate);
            //Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            ((int)result.As<CreatedAtActionResult>().Value.GetType().GetProperty("concertId").GetValue(result.As<CreatedAtActionResult>().Value, null)).Should().Be(0);
        }
        [Fact]
        public async void ActivateConcert_UnActivatedConcert_ActivatesConcert()
        {
            var list = new List<Concert>();
            list.Add(CreateRandomClassicConcert());
            list.Last().IsActiveFlag = false;
            //Arrange
            var mockSet = list.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.ActivateConcertAsync(list.Last().ConcertId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            list.Last().IsActiveFlag.Should().BeTrue();
        }
        [Fact]
        public async void DeactivateConcert_ActivatedConcert_DeactivatesConcert()
        {
            var list = new List<Concert>();
            list.Add(CreateRandomClassicConcert());
            //Arrange
            var mockSet = list.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Concerts).Returns(mockSet.Object);
            var controller = new ConcertsController(StaticInstances.ConcertsLoggerMock.Object, context.Object, StaticInstances.EmailSenderServiceMock.Object, StaticInstances.GuidConfirmationServiceMock.Object, StaticInstances.PayPalPaymentMock.Object, StaticInstances.ConfigurationMock.Object);
            //Act
            var result = await controller.DeactivateConcertAsync(list.Last().ConcertId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            list.Last().IsActiveFlag.Should().BeFalse();
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
                ImageId = rand.Next(int.MaxValue)
            };
        }
    }
}
