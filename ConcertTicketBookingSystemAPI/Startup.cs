using System;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ConcertTicketBookingSystemAPI.CustomServices;
using ConcertTicketBookingSystemAPI.CustomServices.OAuth;
using ConcertTicketBookingSystemAPI.CustomServices.PayPal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConcertTicketBookingSystemAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("credentials.json");
            builder.AddConfiguration(configuration);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddCors();
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            new JwtAuth.AuthOptions(Configuration);
            var googleSection = Configuration.GetSection("GoogleOAuth");
            services.AddSingleton<GoogleOAuthService>(c => new GoogleOAuthService(
                googleSection["clientId"],
                googleSection["secret"],
                googleSection["serverEndPoint"],
                googleSection["tokenEndPoint"],
                googleSection["googleApiEndPoint"],
                googleSection["OAuthRedirect"])
            );
            var facebookSection = Configuration.GetSection("FacebookOAuth");
            services.AddSingleton<FacebookOAuthService>(c => new FacebookOAuthService(
                facebookSection["clientId"],
                facebookSection["secret"],
                facebookSection["serverEndPoint"],
                facebookSection["tokenEndPoint"],
                facebookSection["OAuthRedirect"],
                facebookSection["scope"])
            );
            var microsoftSection = Configuration.GetSection("MicrosoftOAuth");
            services.AddSingleton<MicrosoftOAuthService>(c => new MicrosoftOAuthService(
                microsoftSection["tenant"],
                microsoftSection["clientId"],
                microsoftSection["secret"],
                microsoftSection["serverEndPoint"],
                microsoftSection["tokenEndPoint"],
                microsoftSection["refreshEndPoint"],
                microsoftSection["OAuthRedirect"])
            );
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtAuth.AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = JwtAuth.AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = JwtAuth.AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConcertTicketBookingSystemAPI", Version = "v1" });
            });
            services.AddGuidConfirmationService(Configuration.GetValue<int>("EmailConfirmationTimeSpan"), 10000);
            services.AddStringConfirmationService(Configuration.GetValue<int>("EmailConfirmationTimeSpan"), 10000);
            var senderSection = Configuration.GetSection("EmailSenderSettings");
            services.AddEmailSenderService(
                senderSection["host"],
                senderSection.GetValue<int>("port"),
                senderSection["name"],
                senderSection["email"],
                senderSection["password"]
            );
            var paypalSection = Configuration.GetSection("PayPal");
            services.AddSingleton<PayPalPayment>(c => new PayPalPayment(new PayPalSetup()
            {
                CancelUrl = paypalSection["cancelUrl"],
                ReturnUrl = paypalSection["returnUrl"],
                ClientId = paypalSection["clientId"],
                Secret = paypalSection["secret"],
                Environment = paypalSection["environment"]
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConcertTicketBookingSystemAPI v1"));
            }
            else if (env.IsProduction())
            {
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None
            });
            app.UseRouting();
            app.UseCors(options =>
            {
                options.WithOrigins(Configuration["RedirectUrl"], Configuration.GetSection("PayPal")["thenRedirectedTo"]);
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowCredentials();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
