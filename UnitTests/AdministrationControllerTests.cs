using PL.Controllers;
using DAL.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UnitTests
{
    public class AdministrationControllerTests
    {
        [Fact]
        public async void AddAdmin_IsNotAdminYet_IsAdminAndReturnedNoContent()
        {
            var user = new FacebookUser() { UserId = Guid.NewGuid(), IsAdmin = false };
            var currentUser = new GoogleUser() { UserId = Guid.NewGuid(), IsAdmin = true };
            //Arrange
            var usersService = StaticInstances.CommonUsersService;
            usersService.Setup(s => s.IsExistsAsync(user.UserId)).Returns(Task.FromResult(true));
            usersService.Setup(s => s.IsAdminAsync(currentUser.UserId)).Returns(Task.FromResult(true));
            var controller = new AdministrationController(StaticInstances.AdministrationServiceMock.Object, usersService.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = Helper.GetClaimsPrincipal(currentUser.UserId, "admin");
            //Act
            var result = await controller.AddAdminAsync(user.UserId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
        //[Fact]
        //public async void RemoveAdmin_IsAdmin_IsNotAdminAndReturnedNoContent()
        //{
        //    //var list = new List<User>();
        //    //var user = new FacebookUser() { UserId = Guid.NewGuid(), IsAdmin = true };
        //    //list.Add(user);
        //    ////Arrange
        //    //var mockSet = list.AsQueryable().BuildMockDbSet();

        //    //var context = StaticInstances.ApplicationContextMock;
        //    //context.Setup(context => context.Users).Returns(mockSet.Object);
        //    //var controller = new AdministrationController(StaticInstances.AdministrationLoggerMock.Object, context.Object);
        //    ////Act
        //    //var result = await controller.RemoveAdminAsync(user.UserId);
        //    ////Assert
        //    //result.Should().BeOfType<NoContentResult>();
        //    //user.IsAdmin.Should().BeFalse();
        //}
    }
}
