using System;
using Admin.Dtos;
using Admin.Entities;

namespace Admin.Helper
{
    public static class Extensions
    {
        public static UserDto AsUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                CreatedDate = user.CreatedDate,
                Age = user.Age
            };
        }
    }
}