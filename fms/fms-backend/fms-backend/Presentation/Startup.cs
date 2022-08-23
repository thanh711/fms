using BusinessLogicLayer.BLLService;
using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Presentation
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presentation", Version = "v1" });
            });
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICampusService, CampusService>();
            services.AddSingleton<IAreaService, AreaService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IAssetService, AssetService>();
            services.AddSingleton<ITroubleService, TroubleService>();
            services.AddSingleton<IWorkflowService, WorkflowService>();
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<IChecklistTemplateService, ChecklistTemplateService>();
            services.AddSingleton<IChecklistService, ChecklistService>();
            services.AddSingleton<IChecklistTypeService, ChecklistTypeService>();
            services.AddSingleton<ITechnicalReportService, TechnicalReportService>();
            services.AddSingleton<IWarehouseService, WarehouseService>();
            services.AddSingleton<IMapService, MapService>();
            services.AddSingleton<INotifyService, NotifyService>();

            services.AddSingleton<IUserBLLService, UserBLLService>();
            services.AddSingleton<ICampusBLL, CampusBLL>();
            services.AddSingleton<IAreaBLL, AreaBLL>();
            services.AddSingleton<ILocationBLL, LocationBLL>();
            services.AddSingleton<IRoleBLL, RoleBLL>();
            services.AddSingleton<ICategoryBLL, CategoryBLL>();
            services.AddSingleton<IAssetBLL, AssetBLL>();
            services.AddSingleton<ITroubleBLL, TroubleBLL>();
            services.AddSingleton<IWorkflowBLL, WorkflowBLL>();
            services.AddSingleton<IChecklistBLL, ChecklistBLL>();
            services.AddSingleton<ITechnicalReportBLL, TechnicalReportBLL>();
            services.AddSingleton<IWarehouseBLL, WarehouseBLL>();
            services.AddSingleton<IMapBLL, MapBLL>();
            services.AddSingleton<INotifyBLL, NotifyBLL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
