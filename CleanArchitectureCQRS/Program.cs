using Autofac;
using Autofac.Extensions.DependencyInjection;
using CleanArchitecture.Application.DependencyInjection;
using CleanArchitecture.Application.DomainEventsHandler.Company;
using CleanArchitecture.Domain.DomainEvents.Company;
using CleanArchitecture.Infrastructure.DependencyInjection;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitectureCQRS.API.IntegrationEventHandler;
using Contracts.HR.Company;
using EventBus;
using EventBus.Abstractions;
using EventBusService.AzureBusService;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureCQRS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            if (builder.Configuration.GetSection("AzureEventBus").Exists())
            {
                var eventBusEnabled = builder.Configuration.GetValue<bool>("AzureEventBus:Enabled");


                if (eventBusEnabled)
                {
                    var connectionString = builder.Configuration.GetValue<string>("AzureEventBus:ConnectionString")
                        ?? throw new Exception("Connection for Azure not found.");

                    var topicName = builder.Configuration.GetValue<string>("AzureEventBus:TopicName")
                        ?? throw new Exception("TopicName for Azure not found.");

                    var subscription = builder.Configuration.GetValue<string>("AzureEventBus:Subscription")
                        ?? throw new Exception("Subscription for Azure not found.");

                    builder.Services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                    {
                        return new ServiceBusPersisterConnection(connectionString);
                    });


                    builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

                    builder.Services.AddTransient<CompanyCreatedIntegrationEventHandler>();

                    IServiceCollection serviceCollection = builder.Services.AddSingleton<IEventBus, EventBusInstance>(sp =>
                    {
                        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                        var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                        var logger = sp.GetRequiredService<ILogger<EventBusInstance>>();

                        var eventBus = new EventBusInstance(
                            serviceBusPersisterConnection, logger, topicName, 
                            subscription, eventBusSubcriptionsManager, iLifetimeScope);

                        eventBus.Subscribe<CompanyCreatedIntegrationEvent, CompanyCreatedIntegrationEventHandler>();

                        return eventBus;
                    });
                }
            }

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
