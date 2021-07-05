using System;

namespace Admin.Entities
{
    public record User
    {
        public Guid Id { get; init; } 
        public string UserName { get; init; } 
        public string Password { get; init; } 
        public string Email { get; init; } 
        public string FirstName { get; init; } 
        public string LastName { get; init; } 
        public int Age { get; init; } 
        public DateTimeOffset CreatedDate { get; set; }     
    }
}