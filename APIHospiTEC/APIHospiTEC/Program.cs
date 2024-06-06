
using APIHospiTEC.Services;
using Npgsql;

namespace APIHospiTEC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //Connection with the DB
            builder.Services.AddSingleton<PostgreSqlService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            builder.AllowAnyOrigin() // Permite solicitudes desde cualquier origen
                  .AllowAnyMethod() // Métodos HTTP permitidos
                  .AllowAnyHeader()); // Encabezados permitidos

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
