using Models;
using Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add repository as service
            var connectionString = new ConnectionString(@"Data Source=MELCHOR;Initial Catalog=HumanResourcesDB;User ID=sa;Password=***");
            builder.Services.AddSingleton(connectionString);
            builder.Services.AddScoped<IMobileRepository, DBRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}