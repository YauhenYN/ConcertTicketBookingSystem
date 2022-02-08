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
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            new JwtAuth.AuthOptions(Configuration);
            var googleSection = Configuration.GetSection("GoogleOAuth");
            services.AddSingleton<GoogleOAuthService>(c => new GoogleOAuthService(googleSection["clientId"], googleSection["secret"], googleSection["serverEndPoint"], googleSection["tokenEndPoint"], googleSection["refreshEndPoint"], googleSection["googleApiEndPoint"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Token", options =>
            {
                options.RequireHttpsMetadata = true;
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
            services.AddGuidConfirmationService(new TimeSpan(Configuration.GetValue<long>("EmailConfirmationTimeSpan")), 10000);
            var senderSection = Configuration.GetSection("EmailSenderSettings");
            services.AddEmailSenderService(senderSection["host"], senderSection.GetValue<int>("port"), senderSection["name"], senderSection["email"], senderSection["password"]);
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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
