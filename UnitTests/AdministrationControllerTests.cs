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
    public class AdministrationControllerTests
    {
        [Fact]
        public async void AddAdmin_IsNotAdminYet_IsAdminAndReturnedNoContent()
        {
            var list = new List<User>();
            var user = new FacebookUser() { UserId = Guid.NewGuid(),IsAdmin = false };
            list.Add(user);
            //Arrange
            var mockSet = list.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Users).Returns(mockSet.Object);
            var controller = new AdministrationController(StaticInstances.AdministrationLoggerMock.Object, context.Object);
            //Act
            var result = await controller.AddAdminAsync(user.UserId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            user.IsAdmin.Should().BeTrue();
        }
        [Fact]
        public async void RemoveAdmin_IsAdmin_IsNotAdminAndReturnedNoContent()
        {
            var list = new List<User>();
            var user = new FacebookUser() { UserId = Guid.NewGuid(), IsAdmin = true };
            list.Add(user);
            //Arrange
            var mockSet = list.AsQueryable().BuildMockDbSet();

            var context = StaticInstances.ApplicationContextMock;
            context.Setup(context => context.Users).Returns(mockSet.Object);
            var controller = new AdministrationController(StaticInstances.AdministrationLoggerMock.Object, context.Object);
            //Act
            var result = await controller.RemoveAdminAsync(user.UserId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            user.IsAdmin.Should().BeFalse();
        }
    }
}
