namespace MiniBank.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CustomerNo { get; set; }
    public string HashedPassword { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly RegisterDate { get; set; }
    
}