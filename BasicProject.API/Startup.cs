using Autofac;
using Autofac.Extensions.DependencyInjection;
using BasicProject.API.Filters;
using BasicProject.API.Security;
using BasicProject.Domain.Interface;
using BasicProject.Infra.Context;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Repository;
using BasicProject.Service.Helpers;
using BasicProject.Service.Interface;
using BasicProject.Service.Service;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace BasicProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var t = EncryptHelper.DecryptMD5("bBGExXI8+yyUha6zzAuQUQ==");

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IHostingEnvironment env = serviceProvider.GetService<IHostingEnvironment>();

            var connectionString = string.Empty;
            connectionString = Configuration.GetConnectionString("BasicProjectContext");

            services.AddMvc()
                .AddJsonOptions(options =>
                {

                    var settings = options.SerializerSettings;

                    var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
                    {
                        DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"
                    };

                    settings.Converters.Add(dateConverter);

                });

            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .AddControllersAsServices();

            services.AddDbContext<BasicProjectContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(BasicProjectContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    });

                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BasicProject - API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });
            });

            services.AddMvc()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });


            /*ADD dependency injections resolves*/

            /*ADD Repositories*/
            services.AddTransient<IRepositoryUser, RepositoryUser>();
            services.AddTransient<IRepositoryLog, RepositoryLog>();
            services.AddTransient<IRepositoryAddress, RepositoryAddress>();
            services.AddTransient<IRepositoryUnitOfWork, RepositoryUnitOfWork>();

            /*ADD Services*/
            services.AddTransient<IServiceUser, ServiceUser>();
            services.AddTransient<IServiceLog, ServiceLog>();
            services.AddTransient<IServiceLogin, ServiceLogin>();
            services.AddTransient<IServiceAddress, ServiceAddress>();

            // Authentication
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //Authorization

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

            });


            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("pt"),
                new CultureInfo("es")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicProject - API V1");
            });
        }
    }
}
