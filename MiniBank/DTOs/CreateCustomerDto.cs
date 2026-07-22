namespace MiniBank.DTOs;

public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly BirthDate { get; set; }
}