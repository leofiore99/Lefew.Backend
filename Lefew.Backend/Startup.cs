using System.IO;
using Lefew.Application.Interfaces;
using Lefew.Application.Services;
using Lefew.DependencyResolver;
using Lefew.Domain.RepositoryInterfaces;
using Lefew.Repositories.Base;
using Lefew.Repositories.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;

namespace Lefew.Backend
{
    public class Startup
    {
        private readonly Container container = ContainerFactory.Container;

        private IConfigurationRoot _config;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IntegrateSimpleInjector(services);

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Lefew")));

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Lefew",
                        Version = "v1",
                        Description = "API REST pertencente ao sistema Lefew",
                        Contact = new Contact
                        {
                            Name = "Leonardo Fiore",
                            Url = "https://www.linkedin.com/in/leonardo-fiore/"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            #region Cors
            //Aplicar o Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            _config = builder.Build();

            InitializeContainer(app);

            //Aplica o Cors
            app.UseCors("MyPolicy");

            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Lefew");
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Configurações
            container.RegisterSingleton(typeof(IConfigurationRoot), _config);
            container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Services (Presentation)
            //container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            container.Register<IDistributorAppService, DistributorAppService>(Lifestyle.Scoped);

            // Repositories
            // Admin
            container.Register<IDistributorRepository, DistributorRepository>(Lifestyle.Scoped);
            container.Register<IProspectRepository, ProspectRepository>(Lifestyle.Scoped);

            //DbContexts
            //TODO: Verificar se está sendo realizado o dispose.
            container.Register(() =>
            {
                var options = new DbContextOptionsBuilder<Context>();
                options.UseSqlServer(Configuration.GetConnectionString("Lefew"));
                return new Context(options.Options);
            }, Lifestyle.Scoped);

            // Automapper
            //container.RegisterSingleton(() => container.GetInstance<MapperProvider>().GetMapper());

            // Cross-wire ASP.NET services (if any). For instance:
            container.CrossWire<ILoggerFactory>(app);
        }
    }
}
