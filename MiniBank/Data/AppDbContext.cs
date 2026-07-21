using Microsoft.EntityFrameworkCore;
using MiniBank.Models;

namespace MiniBank.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
    
}