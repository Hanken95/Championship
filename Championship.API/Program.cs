using Microsoft.EntityFrameworkCore;
using Championship.Data.Data;
using Championship.API.Extensions;
using Championchip.Core.Repositories;
using Championship.Data.Repositories;
using Championship.Services;
using Service.Contracts;


namespace Championship.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ChampionshipContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ChampionshipContext") ?? throw new InvalidOperationException("Connection string 'ChampionshipContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(TournamentMappings));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.ConfigureCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAny");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
