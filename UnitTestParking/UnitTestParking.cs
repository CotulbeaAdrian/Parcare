using Xunit;
using Parking.Models;
using Parking.Services;

namespace UnitTestParking
{
    public class UnitTestParking
    {
        [Fact]
        public void isParkingPossible_WhenSpaceAvailable_ReturnsTrue()
        {
            var parkingLot = new ParkingLotService(1);
            Assert.True(parkingLot.IsParkingPossible());
        }

        [Fact]
        public void isParkingPossible_WhenSpaceUnavailable_ReturnsFalse()
        {
            var parkingLot = new ParkingLotService(0);
            Assert.True(!parkingLot.IsParkingPossible());
        }

        [Fact]
        public void isCarParked_WhenIsParked_ReturnsTrue()
        {
            var parkingLot = new ParkingLotService(1);
            parkingLot.ParkCar("testNumber");
            Assert.True(parkingLot.IsCarParked("testNumber"));
        }

        [Fact]
        public void isCarParked_WhenIsNotParked_ReturnsFalse()
        {
            var parkingLot = new ParkingLotService(1);
            parkingLot.ParkCar("testNumber");
            Assert.True(!parkingLot.IsCarParked("testOtherNumber"));
        }
    }
}