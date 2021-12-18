using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Avaliacao.Data.Context;
using Microsoft.EntityFrameworkCore;
using Avaliacao.Domain.Interfaces.Repositories;
using Avaliacao.Data.Repositories;
using Avaliacao.Domain.Interfaces.Services;
using Avaliacao.Domain.Services;

namespace Avaliacao.API
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
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Default"))
               // options => options.UseSqlServer("Server=(localdb)\v14.0;Initial Catalog=Avaliacao;Integrated Security=true")

            );
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IGeralRepo, GeralRepo>();
            services.AddScoped<ISexoRepo, SexoRepo>();
            services.AddScoped<ISexoService, SexoService>();
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Avaliacao.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avaliacao.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
