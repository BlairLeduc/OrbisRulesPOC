using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RulesAPI.Actions;
using RulesAPI.Conditions;
using RulesAPI.Database;
using RulesAPI.Services;

namespace RulesAPI
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
            services.AddDbContext<RuleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TriggerConnectionString")));

            services.AddSingleton<IConditionProcessor, CheckForStatusChangeCondition>();
            services.AddSingleton<IConditionProcessor, CheckForTagsAddedCondition>();
            services.AddScoped<IConditionService, ConditionService>();

            services.AddSingleton<IActionProcessor, InitiateDocusignSigningCeremonyAction>();
            services.AddSingleton<IActionProcessor, SendEmailAction>();
            services.AddScoped<IActionService, ActionService>();

            services.AddScoped<IRuleService, RuleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RuleContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

    }
}
