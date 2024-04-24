using Xunit;
using ParkingLot;

namespace UnitTestParking
{
    public class UnitTestParking
    {
        [Fact]
        public void parkingEntry_WhenSpaceAvailable_ReturnsTrue()
        {
            var parking = new Parking(10);
            Assert.True(parking.parkingEntry());
        }

        [Fact]
        public void parkingEntry_WhenSpaceUnavailable_ReturnsFalse()
        {
            var parking = new Parking(0);
            Assert.True(!parking.parkingEntry());
        }
    }
}