using Parking.Application.Services.Interfaces;
using Parking.Application.Services;
using Parking.Application.Context;
using Parking.Application.Context.Interfaces;
using Parking.Application.Repository.Interfaces;
using Parking.Application.Repository;
using Parking.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ParkingApi
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.;
            builder.Services.AddScoped<IContext> (serviceProvider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("SQLAuth");
                return new Context(connectionString);
            });
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLAuth")));
            builder.Services.AppConfigSettingsServices(builder.Configuration);
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<IParkingRepository, ParkingRepository>();
            builder.Services.AddScoped<IBankAccountService, BankAccountService>();
            builder.Services.AddScoped<IParkingLotService, ParkingLotService>();
            builder.Services.AddControllers();



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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
