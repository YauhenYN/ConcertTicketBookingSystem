using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ConcertTicketBookingSystemAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            new JwtAuth.AuthOptions(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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
