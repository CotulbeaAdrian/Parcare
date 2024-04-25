using Xunit;
using Parking.Models;
using Parking.Services;

namespace UnitTestParking;

public class UnitTestParking
{
    [Fact]
    public void isParkingPossible_WhenSpaceAvailable_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);

        //Act
        var result = sut.IsParkingPossible();

        //Asserts
        Assert.True(result);
    }

    [Fact]
    public void isParkingPossible_WhenSpaceUnavailable_ReturnsFalse()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(0, 2, accounts);

        //Act
        var result = sut.IsParkingPossible();

        //Asserts
        Assert.False(result);
    }

    [Fact]
    public void isCarParked_WhenIsParked_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);

        //Act
        sut.ParkCar("testNumber", DateTime.Now);
        var result = sut.IsCarParked("testNumber");

        //Asserts
        Assert.True(result);
    }

    [Fact]
    public void isCarParked_WhenIsNotParked_ReturnsFalse()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);

        //Act
        sut.ParkCar("testNumber", DateTime.Now);
        var result = sut.IsCarParked("testOtherNumber");

        //Asserts
        Assert.False(result);
    }

    [Fact]
    public void IsPaymentReceived_WhenUnder1h_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 10) ;
        sut.ParkCar("123", DateTime.Now);

        //Act
        sut.PayForParking("123");
        var result = sut.IsPaymentReceived("123");

        //Asserts
        Assert.True(result);
    }

    [Fact]
    public void IsPaymentReceived_WhenPaymentSuccessful_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 1000);
        sut.ParkCar("123", DateTime.Now.AddHours(-10));

        //Act
        sut.PayForParking("123");
        var result = sut.IsPaymentReceived("123");

        //Asserts
        Assert.True(result);
    }

    [Fact]
    public void IsPaymentReceived_WhenBalanceTooLow_ReturnsFalse()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 0);
        sut.ParkCar("123", DateTime.Now.AddDays(-1).AddHours(-10).AddMinutes(-30));

        //Act
        sut.PayForParking("123");
        var result = sut.IsPaymentReceived("123");

        //Asserts
        Assert.False(result);
    }

    [Fact]
    public void ExitParking_WhenPaymentSuccessful_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 1000);
        sut.ParkCar("123", DateTime.Now.AddHours(-10));

        //Act
        sut.PayForParking("123");
        sut.ExitParking("123");

        //Asserts
        Assert.True(sut.CarLeftParking("123"));
    }

    [Fact]
    public void ExitParking_WhenPaymentSuccessfulAtBarrier_ReturnsTrue()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 1000);
        sut.ParkCar("123", DateTime.Now.AddHours(-10));

        //Act
        sut.ExitParking("123");

        //Asserts
        Assert.True(sut.CarLeftParking("123"));
    }

    [Fact]
    public void ExitParking_WhenPaymentFailed_ReturnsFalse()
    {
        //Arrange
        var accounts = new BankAccountService();
        var sut = new ParkingLotService(1, 2, accounts);
        accounts.Add("Tom", new List<string> { "123" }, 0);
        sut.ParkCar("123", DateTime.Now.AddDays(-1).AddHours(-10).AddMinutes(-30));

        //Act
        sut.PayForParking("123");
        sut.ExitParking("123");

        //Asserts
        Assert.False(sut.CarLeftParking("123"));
    }
}