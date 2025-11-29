using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace WebZOO.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<WebZOOAPIContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("WebZOOAPIContext") ?? throw new InvalidOperationException("Connection string 'WebZOOAPIContext' not found.")));

                //options.UseNpgsql(builder.Configuration.GetConnectionString("WebZOOAPIContext.postgres") ?? throw new InvalidOperationException("Connection string 'WebZOOAPIContext' not found.")));
               
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
             builder.Services.AddControllers().AddNewtonsoftJson(
                 options =>
                 options.SerializerSettings.ReferenceLoopHandling 
                 = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

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
