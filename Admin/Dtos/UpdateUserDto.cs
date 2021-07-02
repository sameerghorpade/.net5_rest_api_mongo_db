using System;

namespace Admin.Dtos
{
     public record UpdateUserDto
    {
        //only allowed to change below properties
        public string Password { get; init; } 
        public string FirstName { get; init; } 
        public string LastName { get; init; } 
    }
}