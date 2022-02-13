using ConcertTicketBookingSystemAPI.Controllers;
using ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService;
using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.CustomServices.PayPal;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public static class StaticInstances
    {
        public static Mock<ILogger<ConcertsController>> LoggerMock { get; } = new Mock<ILogger<ConcertsController>>();
        public static Mock<ApplicationContext> ApplicationContextMock { get; } = new Mock<ApplicationContext>(() => new ApplicationContext(new DbContextOptions<ApplicationContext>()));
        public static Mock<EmailSenderService> EmailSenderServiceMock { get; } = new Mock<EmailSenderService>(() => new EmailSenderService("", 0, ""));
        public static Mock<GuidConfirmationService> GuidConfirmationServiceMock { get; } = new Mock<GuidConfirmationService>(() => new GuidConfirmationService(new TimeSpan(10000), 10000));
        public static Mock<PayPalPayment> PayPalPaymentMock { get; } = new Mock<PayPalPayment>(() => new PayPalPayment(new PayPalSetup() { CancelUrl= "", ClientId = "", Environment = "sandbox", ReturnUrl = "", Secret = ""}));
        public static Mock<IConfiguration> ConfigurationMock { get; } = new Mock<IConfiguration>();
    }
}
