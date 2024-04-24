using Xunit;
using Parking.Models;
using Parking.Services;

namespace UnitTestParking;

public class UnitTestParking
{
    [Fact]
    public void isParkingPossible_WhenSpaceAvailable_ReturnsTrue()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        Assert.True(parkingLot.IsParkingPossible());
    }

    [Fact]
    public void isParkingPossible_WhenSpaceUnavailable_ReturnsFalse()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(0, 2, accounts);
        Assert.False(parkingLot.IsParkingPossible());
    }

    [Fact]
    public void isCarParked_WhenIsParked_ReturnsTrue()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        parkingLot.ParkCar("testNumber", DateTime.Now);
        Assert.True(parkingLot.IsCarParked("testNumber"));
    }

    [Fact]
    public void isCarParked_WhenIsNotParked_ReturnsFalse()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        parkingLot.ParkCar("testNumber", DateTime.Now);
        Assert.False(parkingLot.IsCarParked("testOtherNumber"));
    }

    [Fact]
    public void IsPaymentReceived_WhenUnder1h_ReturnsTrue()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 10) ;
        parkingLot.ParkCar("123", DateTime.Now);
        parkingLot.PayForParking("123");
        Assert.True(parkingLot.IsPaymentReceived("123"));
    }

    [Fact]
    public void IsPaymentReceived_WhenPaymentSuccessful_ReturnsTrue()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 1000);
        parkingLot.ParkCar("123", DateTime.Now.AddHours(-10));
        parkingLot.PayForParking("123");
        Assert.True(parkingLot.IsPaymentReceived("123"));
    }

    [Fact]
    public void IsPaymentReceived_WhenBalanceTooLow_ReturnsFalse()
    {
        var accounts = new BankAccountService();
        var parkingLot = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 0);
        parkingLot.ParkCar("123", DateTime.Now.AddDays(-1).AddHours(-10).AddMinutes(-30));
        parkingLot.PayForParking("123");
        Assert.False(parkingLot.IsPaymentReceived("123"));
    }
}