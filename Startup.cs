using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Converter;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using Storee.Repository.Implement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Storee
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
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.Cookie.Name = ".YourApp.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
			});
			services.AddLogging();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",
					Title = "Store",


				});
			});
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new DateConverter());
			});
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICountryService, CountryService>();
			services.AddScoped<ICountryRepository, CountryRepository>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderItemService, OrderItemService>();
			services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			services.AddScoped<IPPriceService, PPriceService>();
			services.AddScoped<IPPriceRepository, PPriceRepository>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IServiceHome, ServiceHome>();
			services.AddScoped<IRepositoryHome, RepositoryHome>();
			services.Configure<StripeSettings>(Configuration.GetSection("StripeSettings"));
			services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("conn")));
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseSession();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseSwagger(); app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Store"));
		}
	}
}
