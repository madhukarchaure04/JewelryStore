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
            //Configuring the appication dependencies
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IStoreCalculatorService, StoreCalculatorService>();
            services.Configure<Setting>(Configuration.GetSection("Setting"));
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
