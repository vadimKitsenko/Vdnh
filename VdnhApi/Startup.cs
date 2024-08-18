using Core.Database;
using Core.Extentions;
using Core.Identity;
using Data;
using Data.Context;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using Services.Admin.Horisontal;
using Services.Admin.Vertical;

namespace Api;

public class Startup(IConfiguration configuration) : Core.Startup(configuration)
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddDbContext<CoreContext>();
        services.AddDbContext<VdnhContext>();
        services.AddDbContext<AuthContext>();
        services.AddDatabase<CoreContext>("DefaultConnection");
        services.AddDatabase<VdnhContext>("DefaultConnection");
        services.AddDatabase<AuthContext>("IdentityConnection");

        /*Закомментить, если начинается проект*/
        services.InitDefaultServices();
        services.InitAuthorization();
        services.AddScoped(typeof(IAppData<>), typeof(DataManager<>));

        services.AddScoped<IVertical, Vertical>();
        services.AddScoped<IHorisontal, Horisontal>();
    }

    public override void ConfigureResponceBuilder(IServiceCollection services)
    {
        base.ConfigureResponceBuilder(services);
    }

    public override DefaultContractResolver ConfigureJsonSerializer()
    {
        return base.ConfigureJsonSerializer();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        base.Configure(app, env);
    }

    public override void ConfigureSwagger(IApplicationBuilder app)
    {
        base.ConfigureSwagger(app);
    }
}
