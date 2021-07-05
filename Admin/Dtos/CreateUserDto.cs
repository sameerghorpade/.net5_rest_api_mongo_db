using System;

namespace Admin.Dtos
{
     public record CreateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; init; } 
        public string Password { get; init; } 
        public string Email { get; init; } 
        public string FirstName { get; init; } 
        public string LastName { get; init; } 
        public int Age { get; init; } 
        public DateTimeOffset CreatedDate { get; init; }
    }
}