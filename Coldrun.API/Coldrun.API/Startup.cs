using Coldrun.BLL.Interfaces.Truck;
using Coldrun.BLL.Services.Truck;
using Coldrun.DAL.Context;
using Coldrun.DAL.Interfaces.Truck;
using Coldrun.DAL.Repositories.Truck;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.API
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
            services.AddControllers();
            services.AddSwaggerGen();

            // For Entity Framework  
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));
            //Initialize Services DI
            services.AddScoped<ITruckService, TruckService>();

            //Initialize Repositories DI
            services.AddScoped<ITruckRepository, TruckRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

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
