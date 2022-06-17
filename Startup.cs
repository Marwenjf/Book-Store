using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Hosting;
using Bookstore.Models;
using Bookstore.Models.Repositories;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookstoreDbContext>(options=>{
                options.UseInMemoryDatabase("BookstoreDb");
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //services.AddSingleton<IBookstoreRepository<Author>,AuthorRepository>();
            //services.AddSingleton<IBookstoreRepository<Book>,BookRepository>();
            services.AddScoped<IBookstoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookstoreRepository<Book>, BookDbRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseMvc(route=> {
                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");
            });
        }
    }
}
