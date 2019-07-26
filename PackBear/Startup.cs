using System;
using PackBear.Consumers;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackBear.Adaptor;
using PackBear.Adaptor.Interface;
using PackBear.Gateway;
using PackBear.Gateway.Interface;
using PackBear.Messages;
using PackBear.Player;
using PackBear.Player.Interface;
using PackBear.UseCases.GetRandomCard;
using PackBear.UseCases.GetRandomCard.Interface;
using PackBear.UseCases.GetStartingCard;
using PackBear.UseCases.GetStartingCard.Interface;

namespace PackBear
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddUseCases(services);
            AddConsumers(services);
            AddStartingStats(services);

            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                IRabbitMqHost host = cfg.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"));
                    h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"));
                });

                SetEndPoints(cfg, host, provider);
            }));

            AddGateways(services);
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            AddRequestClients(services);
            AddAdaptors(services);

            services.AddSingleton<IHostedService, BusService>();
        }

        private static void SetEndPoints(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            SetEndpointForRequestStartingCard(cfg, host, provider);
        }

        private static void SetEndpointForRequestStartingCard(IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "RequestStartingCard", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));

                e.Consumer<RequestStartingCardConsumer>(provider);
                EndpointConvention.Map<IRequestStartingCard>(e.InputAddress);
            });
        }

        private static void AddRequestClients(IServiceCollection services)
        {
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestStartingCard>());
        }

        private static void AddConsumers(IServiceCollection services)
        {
            services.AddScoped<RequestStartingCardConsumer>();
            services.AddMassTransit(x => { x.AddConsumer<RequestStartingCardConsumer>(); });
        }

        private static void AddGateways(IServiceCollection services)
        {
            services.AddSingleton<IPackGateway, InMemoryPackGateway>();
        }

        private static void AddAdaptors(IServiceCollection services)
        {
            services.AddScoped<IPublishMessageAdaptor, PublishMessageMassTransitAdaptor>();
            services.AddScoped<IJsonDeserializeAdaptor, JsonConvertDeserializeAdaptor>();
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetRandomCard, GetRandomCard>();
            services.AddScoped<IGetStartingCard, GetStartingCard>();
        }

        private static void AddStartingStats(IServiceCollection services)
        {
            services.AddSingleton<IStartingStats, StartingStats>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}