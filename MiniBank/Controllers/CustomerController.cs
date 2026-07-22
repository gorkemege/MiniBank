using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniBank.Data;
using MiniBank.DTOs;
using MiniBank.Models;
using BCrypt.Net;

namespace MiniBank.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CustomerController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();

        var dtos = customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            BirthDate = c.BirthDate,
            CustomerNo = c.CustomerNo,
            RegisterDate = c.RegisterDate
        }).ToList();

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            BirthDate = dto.BirthDate,
            RegisterDate = DateOnly.FromDateTime(DateTime.Now),
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CustomerNo = "TEMP",
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var customerDto = new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            CustomerNo = customer.CustomerNo,
            Email = customer.Email,
            RegisterDate = customer.RegisterDate
        };

        return Ok(customerDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }
        
        var dto = new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            CustomerNo = customer.CustomerNo,
            Email = customer.Email,
            RegisterDate = customer.RegisterDate
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        customer.FirstName = dto.FirstName;
        customer.LastName = dto.LastName;
        customer.Email = dto.Email;

        await _context.SaveChangesAsync();

        var customerDto = new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            BirthDate = customer.BirthDate,
            CustomerNo = customer.CustomerNo,
            Email = customer.Email,
            RegisterDate = customer.RegisterDate
        };
            
        return Ok(customerDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return NoContent();
        
    }
}