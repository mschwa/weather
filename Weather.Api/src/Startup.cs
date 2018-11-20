using System;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Weather.Api.Infrastructure.Configuration;
using Weather.Api.Models;
using Weather.Api.Services;
using ConfigurationBinder = Weather.Api.Infrastructure.Configuration.ConfigurationBinder;

namespace Weather.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddLogging(Configure);
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IRestClientFactory, RestClientFactory>();
            services.AddTransient<ICachingService, CachingService>();
            services.AddTransient<IResourceRetrievalService<CityAndWeatherData>, CityAndWeatherDataService>();
            services.AddTransient<IResourceRetrievalService<TimeZoneData>, TimeZoneDataService>();
            services.AddTransient<IResourceRetrievalService<ElevationData>, ElevationDataService>();
            services.AddTransient<IConfigurationBinder, ConfigurationBinder>();

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        private void Configure(ILoggingBuilder obj)
        {
            obj.AddConsole();
            obj.AddDebug();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}