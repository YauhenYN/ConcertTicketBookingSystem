using ConfirmationService;
using DAL.Interfaces;
using EmailSender;
using HttpClientHelper;
using Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuth;
using OAuth.Interfaces;
using PayPal;
using PayPalHttp;
using Sha256Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices
{
    public static class ExtensionMethods
    {
        public static void AddGuidConfirmationService(this IServiceCollection collection, int expirationSpan, int timerPeriod)
        {
            collection.AddSingleton<IConfirmationService<Guid, IUsersRepository>, ConfirmationService<Guid, IUsersRepository>>(provider =>
            new ConfirmationService<Guid, IUsersRepository>(expirationSpan, timerPeriod));
        }
        public static void AddStringConfirmationService(this IServiceCollection collection, int expirationSpan, int timerPeriod)
        {
            collection.AddSingleton<IConfirmationService<string, (IUsersRepository, IConcertsRepository)>, ConfirmationService<string, (IUsersRepository, IConcertsRepository)>>(provider =>
            new ConfirmationService<string, (IUsersRepository, IConcertsRepository)>(expirationSpan, timerPeriod));
        }
        public static void AddEmailSenderService(this IServiceCollection collection, IConfigurationSection senderSection)
        {
            collection.AddSingleton<IEmailSending>(provider =>
            new EmailSenderService(senderSection["host"], 
            senderSection.GetValue<int>("port"),
            senderSection["name"]).ConnectAndAuthenticate(senderSection["email"], senderSection["password"]));
        }
        public static void AddFacebookOAuthService(this IServiceCollection collection, IConfigurationSection facebookSection)
        {
            collection.AddSingleton<IFacebookOAuthService>(c => new FacebookOAuthService(
            c.GetRequiredService<HttpClientHelperBase>(),
            facebookSection["clientId"],
            facebookSection["secret"],
            facebookSection["serverEndPoint"],
            facebookSection["tokenEndPoint"],
            facebookSection["OAuthRedirect"],
            facebookSection["scope"]));
        }
        public static void AddGoogleOAuthService(this IServiceCollection collection, IConfigurationSection googleSection)
        {
            collection.AddSingleton<IGoogleOAuthService>(c => new GoogleOAuthService(
                c.GetRequiredService<HttpClientHelperBase>(),
                googleSection["clientId"],
                googleSection["secret"],
                googleSection["serverEndPoint"],
                googleSection["tokenEndPoint"],
                googleSection["googleApiEndPoint"],
                googleSection["OAuthRedirect"])
            );
        }
        public static void AddMicrosoftOAuthService(this IServiceCollection collection, IConfigurationSection microsoftSection)
        {
            collection.AddSingleton<IMicrosoftOAuthService>(c => new MicrosoftOAuthService(
                c.GetRequiredService<HttpClientHelperBase>(),
                microsoftSection["tenant"],
                microsoftSection["clientId"],
                microsoftSection["secret"],
                microsoftSection["serverEndPoint"],
                microsoftSection["tokenEndPoint"],
                microsoftSection["refreshEndPoint"],
                microsoftSection["OAuthRedirect"])
            );
        }
        public static void AddJwtService(this IServiceCollection collection, IConfigurationSection authOptions)
        {
            collection.AddSingleton<JwtServiceBase, JwtService>(c => new JwtService(
                authOptions["Issuer"],
                authOptions["Audience"],
                authOptions["Key"],
                authOptions.GetValue<int>("LifeTime"),
                authOptions.GetValue<int>("RefreshLifeTime")
            ));
        }
        public static void AddPayPalPaymentService(this IServiceCollection collection, IConfigurationSection paypalSection)
        {
            collection.AddSingleton<IPaymentService<HttpResponse>>(c => new PayPalPaymentService(new PayPalSetup()
            {
                CancelUrl = paypalSection["cancelUrl"],
                ReturnUrl = paypalSection["returnUrl"],
                ClientId = paypalSection["clientId"],
                Secret = paypalSection["secret"],
                Environment = paypalSection["environment"]
            }));
        }
        public static void AddHttpClientHelper(this IServiceCollection collection)
        {
            collection.AddSingleton<HttpClientHelperBase>(c => new HttpClientHelper.HttpClientHelper());
        }
        public static void AddSha256Helper(this IServiceCollection collection)
        {
            collection.AddSingleton<ISha256Helper>(c => new Sha256Helper.Sha256Helper());
        }
    }
}
