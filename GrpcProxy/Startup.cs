using System;
using AuthService;
using CustomerService;
using GrpcProxy.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            //Load Clients Grpc Services EndPoints
            services.AddGrpcClient<Authentication.AuthenticationClient>(o =>
            {
                o.Address = new Uri("https://localhost:5001");
                /*o.ChannelOptionsActions.Add(options =>
                {
                    CallCredentials callCredentials = CallCredentials.FromInterceptor((context, metadata) =>
                    {
                        metadata.Add("Authorization", $"{appSettings.TokenAuth}");
                        return Task.CompletedTask;
                    });

                    options.Credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);
                });*/
            });
            services.AddGrpcClient<CustomerContract.CustomerContractClient>(o => o.Address = new Uri("https://localhost:5002"));

            //Load Redis Cache
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("RedisAddress");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Force HTTPS connection with server
            app.UseHttpsRedirection();
            app.UseRouting();

            //Load Middleware to Validate Server access
            app.UseMiddleware<ValidateAccessMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                //Map EndPoints that are going to comunicate with services
                endpoints.MapGrpcService<AuthenticationEndPoint>();
                endpoints.MapGrpcService<CustomerEndPoint>();
            });
        }
    }
}
