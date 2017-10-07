using chat_server.Controllers;
using chat_server.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace chat_server
{
    public sealed class Startup
    {
        private const string Version = "v1";

//        private readonly IConfiguration configuration;
//        
//        public Startup(IConfiguration configuration) => this.configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = Api.Name, Version = Version}); });
            services.AddSingleton<ISessionRepository>(_ => new InMemorySessions());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc()
                .UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Api.Name} {Version}"); });
        }
    }
}