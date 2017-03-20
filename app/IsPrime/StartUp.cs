
using System;
using Bivouac.Abstractions;
using Bivouac.Events;
using Bivouac.Middleware;
using Burble.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IsPrime
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddStatusEndpointServices("IsPrime");
            services.AddServerLoggingServices();

            services.AddTransient<IHttpServerEventCallback>(CreateHttpServerEventCallback);
            services.AddTransient<IHttpClientEventCallback>(CreateHttpClientEventCallback);

            services.AddMvc();
        }

        public static void Configure(IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseServerLoggingMiddleware();
            app.UseStatusEndpointMiddleware();

            app.UseMvc();
        }

        private static IHttpServerEventCallback CreateHttpServerEventCallback(IServiceProvider serviceProvider)
        {
            var requestIdGetter = serviceProvider.GetService<IGetRequestId>();
            var correlationIdGetter = serviceProvider.GetService<IGetCorrelationId>();

            return new IdentifyingHttpServerEventCallback(
               requestIdGetter,
               correlationIdGetter,
               new JsonHttpServerEventCallback(Console.WriteLine));
        }

        private static IHttpClientEventCallback CreateHttpClientEventCallback(IServiceProvider serviceProvider)
        {
            return new JsonHttpClientEventCallback(Console.WriteLine);
        }
    }
}