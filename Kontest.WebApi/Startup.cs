using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontest.Data;
using Kontest.Infrastructure.Interfaces;
using Kontest.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Kontest.Service.AutoMapper;
using Kontest.Service.Interfaces;
using Kontest.Service.Implementations;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Kontest.WebApi.CustomJsonConverters;

namespace Kontest.WebApi
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
            services.AddDbContext<KontestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson(op =>
            {
                op.SerializerSettings.Converters.Add(new EnumToKeyValuePairConverter());
                op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                op.SerializerSettings.DateFormatString = "dd-MM-yyyy";
            });

            services.AddMvcCore(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                config.Filters.Add(new AuthorizeFilter(policy)); //global authorize filter
            }).AddAuthorization();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<KontestDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "Kontest.Api";
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5100")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            services.AddTransient<DbInit>();

            // Add user service
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<ApplicationRole>, RoleManager<ApplicationRole>>();

            // Add repository and unit of work
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            // Add kontest services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IUserOrganizationService, UserOrganizationService>();
            services.AddTransient<IOrganizationCategoryService, OrganizationCategoryService>();
            services.AddTransient<IOrganizationRequestService, OrganizationRequestService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
