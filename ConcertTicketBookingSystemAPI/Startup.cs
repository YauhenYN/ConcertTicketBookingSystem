using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using DAL;
using System.Text;
using BLL.Configurations;
using DAL.Interfaces;
using DAL.Repositories;
using BLL.Services;
using BLL.Interfaces;

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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConcertTicketBookingSystemAPI", Version = "v1" });
            });

            //Configuration
            services.Configure<BaseLinksConf>(options => Configuration.GetSection("BaseLinks").Bind(options));
            services.Configure<FacebookOAuthConf>(options => Configuration.GetSection("FacebookOAuth").Bind(options));
            services.Configure<GoogleOAuthConf>(options => Configuration.GetSection("GoogleOAuth").Bind(options));
            services.Configure<MicrosoftOAuthConf>(options => Configuration.GetSection("MicrosoftOAuth").Bind(options));
            services.Configure<PayPalConf>(options => Configuration.GetSection("PayPal").Bind(options));

            //Authentication
            var authOptions = Configuration.GetSection("AuthOptions");
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = authOptions["Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions["Key"])),
                        ValidateIssuerSigningKey = true
                    };
                });

            //CustomServices
            services.AddJwtService(authOptions);
            services.AddGoogleOAuthService(Configuration.GetSection("GoogleOAuth"));
            services.AddFacebookOAuthService(Configuration.GetSection("FacebookOAuth"));
            services.AddMicrosoftOAuthService(Configuration.GetSection("MicrosoftOAuth"));
            services.AddGuidConfirmationService(Configuration.GetValue<int>("EmailConfirmationTimeSpan"), 10000);
            services.AddStringConfirmationService(Configuration.GetValue<int>("EmailConfirmationTimeSpan"), 10000);
            services.AddEmailSenderService(Configuration.GetSection("EmailSenderSettings"));
            services.AddPayPalPaymentService(Configuration.GetSection("PayPal"));
            services.AddHttpClientHelper();
            services.AddSha256Helper();

            //DAL
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IAdditionalImagesRepository, AdditionalImagesRepository>();
            services.AddScoped<IClassicConcertsRepository, ClassicConcertsRepository>();
            services.AddScoped<IConcertsRepository, ConcertsRepository>();
            services.AddScoped<IFacebookUsersRepository, FacebookUsersRepository>();
            services.AddScoped<IGoogleUsersRepository, GoogleUsersRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();
            services.AddScoped<IMicrosoftUsersRepository, MicrosoftUsersRepository>();
            services.AddScoped<IOpenAirConcertsRepository, OpenAirConcertsRepository>();
            services.AddScoped<IPartyConcertsRepository, PartyConcertsRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPromoCodesRepository, PromoCodesRepository>();

            //BLL
            services.AddScoped<IActionsService, ActionsService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IConcertPaymentService, ConcertPaymentService>();
            services.AddScoped<ICommonConcertsService, ConcertsService>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            services.AddScoped<IFacebookOAuthService, FacebookOAuthService>();
            services.AddScoped<IGoogleOAuthService, GoogleOAuthService>();
            services.AddScoped<ICommonImagesService, ImageService>();
            services.AddScoped<IMicrosoftOAuthService, MicrosoftOAuthService>();
            services.AddScoped<IPersonalizationService, PersonalizationService>();
            services.AddScoped<ICommonPromoCodesService, PromoCodesService>();
            services.AddScoped<ICommonTicketsService, TicketsService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<ICommonUsersService, UsersService>();
            services.AddScoped<IUserInfoService, UsersService>();
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
                options.WithOrigins(Configuration.GetSection("BaseLinks")["FrontUrl"], Configuration.GetSection("PayPal")["thenRedirectedTo"]);
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
