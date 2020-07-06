using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Webinar.Application;
using Webinar.EventStore;
using Webinar.Infrastructure;
using Webinar.Library;

namespace TravelAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var esConnection = EventStoreConnection.Create(
                Configuration["eventStore:connectionString"],
                ConnectionSettings.Create().KeepReconnecting(),
                "webinar"
            );

            services.AddSingleton(esConnection);
            services.AddSingleton<IAggregateStore, AggregateStore>();
            services.AddSingleton<BookingCommandService>();
            services.AddHostedService<EventStoreHostedService>();

            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
