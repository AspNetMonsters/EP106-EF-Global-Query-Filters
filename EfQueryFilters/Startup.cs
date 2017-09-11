using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfQueryFilters.Data;
using GenFu;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfQueryFilters
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
            var connection =
                @"Server=(localdb)\mssqllocaldb;Database=EfQueryFilters.EmployeeContext;Trusted_Connection=True;";
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            //services.AddTransient<EmployeeContext>(serviceProvider =>
            //{
            //    DbContextOptionsBuilder<EmployeeContext> options = new DbContextOptionsBuilder<EmployeeContext>();
            //    options.UseSqlServer(connection);
            //    var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            //    int companyId;
            //    if (contextAccessor.HttpContext == null)
            //    {
            //        companyId = 0;
            //    }
            //    else
            //    {
            //        companyId = Convert.ToInt32(contextAccessor.HttpContext.GetRouteValue("CompanyID"));
            //    }
            //    return new EmployeeContext(companyId, options.Options);
            //});


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EmployeeContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            //Initialize and seed the database if it is empty
            dbContext.Database.EnsureCreated();
            if (!dbContext.Employees.Any())
            {
                A.Configure<Employee>()
                    .Fill(e => e.Id, 0)
                    .Fill(e => e.IsDeleted)
                    .WithRandom(new[]{true, true, true, false}) //Gives us a distribution of approx 25% deleted employees
                    .Fill(e => e.CompanyId).WithRandom(new[] {1, 2, 3}) //3 different companies
                    .Fill(e => e.Title).AsPersonTitle();


            
                var randomEmployees = A.ListOf<Employee>(500);

                dbContext.Employees.AddRange(randomEmployees);
                dbContext.SaveChanges();
            }

        }
    }
}
