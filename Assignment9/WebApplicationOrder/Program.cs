using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace WebApplicationOrder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ���ݻ��������ϲ������ļ�
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{�Զ��廷������}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            // ��ȡ���ñ���
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

            builder.Services.AddDbContextPool<�Զ���.OrdermanagerContext>(option =>
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
