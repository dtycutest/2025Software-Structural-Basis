using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace WebApplicationOrder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 根据环境变量合并配置文件
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{自定义环境变量}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            // 获取配置变量
            /*ConnectionStrings connectionStrings = new();
            configurationRoot.GetSection("ConnectionStrings")
                             .Bind(connectionStrings);*/
            //string connectionStrings = "Server=localhost;Port=3306;User Id=root;Password=123456;Database=ordermanager";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextPool<自定义.OrdermanagerContext>(option =>
                option.UseMySql("Server=localhost;Port=3306;User Id=root;Password=123456;Database=ordermanager", ServerVersion.Parse("8.0.26-mysql")), poolSize: 1);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
