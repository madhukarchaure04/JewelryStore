using JewelryStore.API.Authorization;
using JewelryStore.API.DBModels;
using JewelryStore.API.Helpers;
using JewelryStore.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DinkToPdf.Contracts;
using DinkToPdf;
using JewelryStore.API.Entities;

namespace JewelryStore.API
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
            //Configuring the DB Dependency
            services.AddDbContext<DBContext>(context => context.UseInMemoryDatabase("store_db"));
            services.AddControllers();

            services.AddCors();
            //Configuring the application dependencies
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IStoreCalculatorService, StoreCalculatorService>();
            //Resolving dependency for multiple implementation of same interface
            services.AddScoped<PrintToFileService>();
            services.AddScoped<PrintToPrinterService>();
            services.AddScoped<ServiceResolver>(serviceProvider => key =>
            {
                switch(key)
                {
                    case PrintType.File:
                        return serviceProvider.GetService<PrintToFileService>();
                    case PrintType.Paper:
                        return serviceProvider.GetService<PrintToPrinterService>();
                    default:
                        throw new System.Exception("Print service not cofigured for " + key);
                }
            });
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.Configure<Setting>(Configuration.GetSection("Setting"));

            //Configuring Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JewelryStore API V1");
            });

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();
            app.UseMiddleware<JWTTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DBContext>();
                SetupDBIntialState(context);
            }
        }

        /// <summary>
        /// Initalizing the InMemoryDB with two dummy user
        /// </summary>
        /// <param name="context"></param>
        private void SetupDBIntialState(DBContext context)
        {
            var user1 = new User
            {
                FirstName = "Alice",
                LastName = "S.",
                Username = "Alice",
                UserType = Entities.UserType.Regular,
                Password = "Alice"
            };

            context.Users.Add(user1);

            var user2 = new User
            {
                FirstName = "Bob",
                LastName = "K.",
                Username = "Bob",
                UserType = Entities.UserType.Privileged,
                Password = "Bob"
            };

            context.Users.Add(user2);

            context.SaveChanges();
        }
    }
}
