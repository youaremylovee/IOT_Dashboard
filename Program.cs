using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI_IOT.Models;
using UI_IOT.Repository.ItemRepository;
using UI_IOT.Services;

namespace UI_IOT
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//Cors
			builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Thêm hỗ trợ cho cả API và Views
            builder.Services.AddControllersWithViews();  // Thêm support cho Views

			// Bật Runtime Compilation cho Razor Pages nếu đang phát triển
			builder.Services.AddRazorPages();

			//SQL Server
			builder.Services.AddDbContext<ItemContext>(options =>
							options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//DI
			builder.Services.AddScoped<ItemRepository>();
			builder.Services.AddScoped<ItemService>();
			builder.Services.AddSingleton(new Config(builder.Configuration));
			builder.Services.AddSingleton<NotifyService>();

			//SignalR
			builder.Services.AddSignalR();


			// Cấu hình Swagger
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.EnableAnnotations();  // Kích hoạt Swagger Annotations
			});

			var app = builder.Build();

			// Kiểm tra môi trường, bật Swagger cho môi trường phát triển
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "UI IOT API v1");
					c.RoutePrefix = "swagger"; 
				});
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();  // Cho phép truy cập các file tĩnh (CSS, JS)

			app.UseRouting();  // Cần cho điều hướng giữa Views và API

			app.UseAuthorization();

            //SignalR
            app.MapHub<ChartHub>("/chart");

            // Map API controllers và Razor Pages
            app.MapControllers();
			app.MapRazorPages();

			// Map route mặc định cho MVC Views
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
