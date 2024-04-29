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
        var carNumber = "12";
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

        for (int i = 1;i<=3;i++)
            await client.PostAsync($"/api/parkinglot/{i}/in", null);

        var carNumber = "123";

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/in", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Parking lot is full.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PayForParking_WhenPaymentPossible_ReturnsOk()
    {
        // Arrange
        var carNumber = "12";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7032");
        await client.PostAsync($"/api/Parkinglot/{carNumber}/in", null);

        // Act
        var response = await client.PostAsync($"/api/parkinglot/{carNumber}/payment", null);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Payment successful.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PayForParking_WhenPaymentNotPossible_ReturnsBadRequest()
    {
        // Arrange
        var carNumber = "1234";
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
    public async Task LeaveParking_WhenLeavingPossible_ReturnsOk()
    {
        // Arrange
        var carNumber = "12";
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
    public async Task LeaveParking_WhenLeavingNotPossible_BadRequest()
    {
        // Arrange
        var carNumber = "1234";
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
