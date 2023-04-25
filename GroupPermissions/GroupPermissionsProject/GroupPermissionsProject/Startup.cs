using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Core.Interfaces;
using GroupPermissionsProject.Infrastructure.Data;
using GroupPermissionsProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPermissionsProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // CORS Policy Name 
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add database connection
            services.AddDbContext<GroupPermissionsProjectContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity with custom settings [user & usermanager]
            services.AddIdentity<ApplicationUser, IdentityRole>()
                //.AddUserManager<ApplicationUserManager>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GroupPermissionsProjectContext>()
                .AddDefaultTokenProviders();




            // Enable Dual Authentication [cookies & brearer]
            services.AddAuthentication()
              .AddCookie(options =>
              {
                  options.SlidingExpiration = true;
              })
              .AddJwtBearer(options =>
              {

                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                      RequireExpirationTime = false,
                      ValidateLifetime = true,
                  };
              });







            // Allow CORS origin              
            var list = new List<string>();
            Configuration.GetSection("AllowedHosts").Bind(list);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins(list.ToArray()).SetIsOriginAllowed(x => _ = true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            // Allow special chars for Identity Username [email chars]
            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
                options.User.RequireUniqueEmail = true;
            });


            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddTransient<IPermissionsRepository, PermissionsRepository>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Configure JSON to ignore reference loop
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            // Enable MVC compatibility
            services.AddMvc(options =>
            {
                // Add authorize global filter using dual authorization mode [cookie based identity & bearer]
                var defaultPolicy = new AuthorizationPolicyBuilder(new[] { IdentityConstants.ApplicationScheme, JwtBearerDefaults.AuthenticationScheme })
                        .RequireAuthenticatedUser()
                        .Build();
                //options.Filters.Add(new AuthorizeFilter(defaultPolicy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    // Filter out 3rd party controllers
                    var assemblyName = ((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerTypeInfo.Assembly.GetName().Name;
                    var currentAssemblyName = GetType().Assembly.GetName().Name;
                    return currentAssemblyName == assemblyName;
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Group Permissions Project API",
                    Description = "Group Permissions Project API functions documentation"
                });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

                //// Configuring Swagger with JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});


            app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/docs.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/docs.json", "Group Permissions Project V1");
                c.RoutePrefix = "docs";
            });
        }
    }
}
