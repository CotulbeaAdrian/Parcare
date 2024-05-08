using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Parking.Application.Services;

namespace Parking.IntegrationTests;

public class ParkingIntegrationTests
{
    [Fact]
    public async Task EnterParking_WhenParkingAvailable_ReturnsOk()
    {
        // Arrange
        var carNumber = "123";
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://localhost:7032"),
        };

        // Act
        var response = await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Car parked successfully.", await response.Content.ReadAsStringAsync());

        await client.PostAsync($"/api/parkinglot/{carNumber}/payment", null);
        await client.PostAsync($"/api/parkinglot/{carNumber}/out", null);
    }

    [Fact]
    public async Task EnterParking_WhenParkingUnavailable_ReturnsBadRequest()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");

        var carNumber = "notassigned";

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/in", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Parking lot is full.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PayForParking_WhenPaymentSuccessful_ReturnsOk()
    {
        // Arrange
        var carNumber = "123";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");
        await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/payment", null);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Payment successful.", await response.Content.ReadAsStringAsync());
        await client.PostAsync($"/api/Parkinglot/{carNumber}/out", null);
    }

    [Fact]
    public async Task PayForParking_WhenPaymentUnsuccessful_ReturnsBadRequest()
    {
        // Arrange
        var carNumber = "car2";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");
        await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/payment", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Payment failed. Car not parked or insufficient balance.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task LeaveParking_WhenLeavingSuccessful_ReturnsOk()
    {
        // Arrange
        var carNumber = "123";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");
        await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);
        await client.PostAsync($"/api/parkinglot/{carNumber}/payment", null);

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/out", null);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Car left parking lot successfully.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task LeaveParking_WhenLeavingUnsuccessful_BadRequest()
    {
        // Arrange
        var carNumber = "car2";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");
        await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/out", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Car not found in parking lot.", await response.Content.ReadAsStringAsync());
    }
}
