using Assignment.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment
{
    public class Startup
    {
        public Startup(IConfiguration Config) { this.Config = Config; }
        public IConfiguration Config { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExpenseDbContext>(op => op.UseSqlServer(this.Config.GetConnectionString("expense")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            //, ExpenseDbContext db
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //if (!db.ExpenseCategories.Any())
            //{
            //    ExpenseCategory c1 = new ExpenseCategory { CategoryName = "Uber" };
            //    c1.DailyExpenses.Add(new DailyExpense { ExpenseAmount = 4500000.00M});
            //    db.ExpenseCategories.Add(c1);
            //    db.SaveChanges();
            //}
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
