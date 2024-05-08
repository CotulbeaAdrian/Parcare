﻿using Microsoft.AspNetCore.Mvc;
using Parking.Application.Services;
using System;
using Parking.Application.Services.Interfaces;
using Parking.Application.Repository.Interfaces;

namespace Parking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingLotController : ControllerBase
{
    private IParkingLotService _parkingLotService;
    private readonly IBankAccountRepository _bankAccountRepository;

    public ParkingLotController(IParkingLotService parkingLotService, IBankAccountRepository bank)
    {
        _bankAccountRepository = bank;
        _parkingLotService = parkingLotService;
    }

    [HttpPost("{carNumber}/in")]
    public IActionResult EnterParking(string carNumber)
    {
        //Act
        _parkingLotService.ParkCar(carNumber, DateTime.Now);
        //
        bool success = _parkingLotService.IsCarParked(carNumber);
        if (success)
            return Ok("Car parked successfully.");
        return BadRequest("Parking lot is full.");
    }

    [HttpPost("{carNumber}/payment")]
    public IActionResult Pay(string carNumber)
    {
        _parkingLotService.PayForParking(carNumber);
        bool success = _parkingLotService.IsPaymentReceived(carNumber);
        if (success)
            return Ok("Payment successful.");
        return BadRequest("Payment failed. Car not parked or insufficient balance.");
    }

    [HttpPost("{carNumber}/out")]
    public IActionResult Leave(string carNumber)
    {
        _parkingLotService.ExitParking(carNumber);
        bool success = _parkingLotService.CarLeftParking(carNumber);
        if (success)
            return Ok("Car left parking lot successfully.");
        return BadRequest("Car not found in parking lot.");
    }
}