using Expenses.Data;
using Expenses.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Expenses
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression();

            services.AddScoped<StoreDataContext, StoreDataContext>();
            
            services.AddTransient<ExpenseRepository, ExpenseRepository>();
            services.AddTransient<PaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<RevenueRepository, RevenueRepository>();
            services.AddTransient<UserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();
        }
    }
}
