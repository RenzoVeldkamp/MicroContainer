using System;
using Dierentuin.Dieren;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dierentuin
{
    public class Startup
    {
        private string rabbitConnectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            rabbitConnectionString = configuration.GetValue<string>("Konijn");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IBus>(CreateBus);
            services.AddSingleton<IDierenProvider, DierenProvider>();
            services.AddSingleton<IAsyncDierenProvider, AsyncDierenProvider>();
        }

        private IBus CreateBus(IServiceProvider serviceProvider)
        {
            IBus bus = RabbitHutch.CreateBus(rabbitConnectionString);
            System.Diagnostics.Debug.Print($"{bus.GetHashCode()}");
            return bus;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection(); // will cause certificate issues with docker-compose // GRRRR

            app.UseMvc();
        }
    }
}
