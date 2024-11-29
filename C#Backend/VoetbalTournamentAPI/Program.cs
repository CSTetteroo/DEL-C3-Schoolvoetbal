using Microsoft.AspNetCore.Identity;
using VoetbalTournamentAPI.Data;
using VoetbalTournamentAPI.Model;

namespace VoetbalTournamentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register the PasswordHasher service
            builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddDbContext<VoetbalTournamentDataContext>();

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
