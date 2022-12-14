using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ncorep.Configurations;
using ncorep.Data;
using ncorep.Extensions;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Services;
using Newtonsoft.Json;

namespace ncorep;

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
        services.ConfigureSqlConnection(Configuration);
        services.AddAuthentication();
        services.ConfigureIdentity();
        services.AddAuthorization();
        services.AddAutoMapper(typeof(AutoMapperInitializer));
        services.ServiceInjection();
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericGenericRepository<>));
        services.AddControllers().AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}