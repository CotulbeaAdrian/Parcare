using Parking.Application.Services.Interfaces;
using Parking.Application.Services;

namespace ParkingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IBankAccountService, BankAccountService>();
            int totalSlots = 10;
            int price = 5;
            var serviceProvider = builder.Services.BuildServiceProvider();
            ParkingLotService parkingLotService = new(totalSlots, price, serviceProvider.GetRequiredService<IBankAccountService>() );
            builder.Services.AddScoped<IParkingLotService, ParkingLotService>(parkingLotService);
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
