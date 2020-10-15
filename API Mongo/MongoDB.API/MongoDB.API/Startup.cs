using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.API.Data.Configuration;
using MongoDB.API.Data.Context;
using MongoDB.API.Data.Contracts;
using MongoDB.API.Data.Repository;
using MongoDB.API.NotificationHub;

namespace MongoDB.API
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

            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:4200")
                        .AllowCredentials()));

            services.AddSignalR();

            // MongoDB
            services.Configure<ClientesStoreDatabaseSettings>(
                            Configuration.GetSection(nameof(ClientesStoreDatabaseSettings)));

            services.AddSingleton<IClientesStoreDatabaseSettings>(sp =>
                            sp.GetRequiredService<IOptions<ClientesStoreDatabaseSettings>>().Value);

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClientesStoreDatabaseSettings, ClientesStoreDatabaseSettings>();
            services.AddScoped<ClienteContext>();

            //Aplicando documentação com Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Mongo DB Theos ",
                    Version = "V1",
                    Description = "Mongo com .net",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Miranda",
                        Email = "arthur.rafa10@gmail.com"
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

           //  app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ClienteHub>("/clienteHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Livraria Theos");
            });
        }
    }
}
