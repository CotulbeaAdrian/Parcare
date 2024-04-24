using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcare
{
    internal class Parking
    {
        private int availableSlots;
        private DateTime entryTime;

        public Parking(int totalSlots) {
            availableSlots = totalSlots;
        }

        public bool parkingEntry()
        {
            if (availableSlots > 0)
            {
                entryTime = DateTime.Now;
                Console.WriteLine($"Parking entered at {entryTime}");
                return true;
            }
            else
            {
                Console.WriteLine($"There are no empty slots at the time. Please come back later!");
                return false;
            }
        }

    }
}
