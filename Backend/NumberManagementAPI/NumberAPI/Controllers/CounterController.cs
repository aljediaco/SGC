using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberAPI.BL;
using NumberAPI.DTOs;
using NumberAPI.Models;
using System;
using System.Diagnostics.Metrics;

[ApiController]
[Route("api/[controller]")]
public class CounterController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly CounterBL _counterBL;

	public CounterController(AppDbContext context, CounterBL counterBL)
	{
		_context = context;
		_counterBL = counterBL;
	}

	[HttpPost("save")]
	public async Task<IActionResult> Save([FromBody] SaveCounterRequest request)
	{
		if (request == null)
			return BadRequest("Request inválido.");

		if (request.CounterValue < 0)
			return BadRequest("El contador no puede ser negativo.");

		var record = await _counterBL.SaveCounter(request.CounterValue);

		return Ok(new
		{
			message = "Counter saved successfully",
			data = record
		});
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var records = await _counterBL.GetCounter();
		return Ok(records);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var record = await _counterBL.GetCounterId(id);
		return Ok(record);
	}
}