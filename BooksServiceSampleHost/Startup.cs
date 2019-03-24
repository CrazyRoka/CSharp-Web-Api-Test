using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksServices.Models;
using BooksServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace BooksServiceSampleHost
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
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddScoped<IBookChaptersService, DBBookChaptersService>();
            services.AddScoped<SampleChapters>();

            services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BooksConnection")));

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments("../docs/BooksServiceSample.xml");
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Books Service API",
                    Version = "v1",
                    Description = "Sample Service for learning Web Api"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, SampleChapters sampleChapters, BooksContext booksContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Chapter Services"));

            bool created = booksContext.Database.EnsureCreated();
            if (created)
            {
                await sampleChapters.CreateSampleChaptersAsync();
            }
        }
    }
}
