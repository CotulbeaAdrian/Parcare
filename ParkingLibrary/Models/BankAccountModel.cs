using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Models;

public class BankAccountModel
{
    public required string Name { get; set; }
    public required List<string> CarNumber { get; set; }
    public required float Balance { get; set; }
}

