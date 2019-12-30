using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.DbModels;
using NewCommunity.Common;
using NewCommunity.Hubs;
using NewCommunity.Middleware;
using Newtonsoft.Json.Serialization;
using Service.Implement;
using Service.ServiceInterface;

namespace NewCommunity
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
            AddCors(services);

            var settingsSection = Configuration.GetSection("ApplicationSettings");
            var settings = settingsSection.Get<ApplicationSettings>();
            services.Configure<ApplicationSettings>(settingsSection);

            AddJWT(services, settings);
            DependencyInjection(services, settings);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; //new PascalCase()
            });
            services.AddSignalR();
            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Converters = new DefaultContractResolver();
            //});
        }
        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:8081")
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }
        private static void AddJWT(IServiceCollection services, ApplicationSettings settings)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JWT_Secret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        private static void DependencyInjection(IServiceCollection services, ApplicationSettings settings)
        {
            services.AddDbContext<CommunityContext>(options =>
                options.UseSqlServer(settings.IdentityConnection));
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            //app.UseCors(builder => builder
            //  .AllowAnyHeader()
            //  .AllowAnyMethod()
            //  .SetIsOriginAllowed((host) => true)
            //  .AllowCredentials()
            //);
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();//先驗證、再授權

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/api/chatHub");
            });

        }
    }
}
