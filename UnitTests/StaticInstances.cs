using EmailSender;
using Microsoft.EntityFrameworkCore;
using Moq;
using DAL;
using BLL.Interfaces;

namespace UnitTests
{
    public static class StaticInstances
    {
        public static Mock<ApplicationContext> ApplicationContextMock { get => new Mock<ApplicationContext>(() => new ApplicationContext(new DbContextOptions<ApplicationContext>())); }
        public static Mock<EmailSenderService> EmailSenderServiceMock { get => new Mock<EmailSenderService>(() => new EmailSenderService("", 0, "")); }
        public static Mock<IAdministrationService> AdministrationServiceMock { get => new Mock<IAdministrationService>(); }
        public static Mock<ICommonUsersService> CommonUsersService { get => new Mock<ICommonUsersService>(); }
    }
}
