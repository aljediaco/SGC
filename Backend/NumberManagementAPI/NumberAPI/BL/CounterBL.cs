using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberAPI.DTOs;
using NumberAPI.Models;

namespace NumberAPI.BL
{
	public class CounterBL
	{
		private readonly AppDbContext _context;

		public CounterBL(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<CounterRecordDTO>> GetCounter()
		{
			var records = await _context.CounterRecords
			.OrderBy(x => x.Id)
			.Select(x => new CounterRecordDTO
			{
				Id = x.Id,
				NumberValue = x.NumberValue,
				SavedDate = x.SavedDate.ToString("dd/MM/yyyy HH:mm:ss")
			})
			.ToListAsync();

			return records;
		}

		public async Task<CounterRecordDTO> GetCounterId(int id)
		{
			var record = await _context.CounterRecords.FindAsync(id);

			if (record == null)
				return null;

			var dto = new CounterRecordDTO
			{
				Id = record.Id,
				NumberValue = record.NumberValue,
				SavedDate = record.SavedDate.ToString("dd/MM/yyyy HH:mm:ss")
			};

			return dto;
		}

		public async Task<int> SaveCounter(int CounterValue)
		{
			var record = new CounterRecord
			{
				NumberValue = CounterValue,
				SavedDate = DateTime.UtcNow
			};

			_context.Entry(record).State = EntityState.Added;
			//_context.CounterRecords.Add(record);
			return await _context.SaveChangesAsync();
		}
	}
}
