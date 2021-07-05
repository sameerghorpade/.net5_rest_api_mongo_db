using System;
using Microsoft.AspNetCore.Mvc;
using Admin.Repository;
using System.Linq;
using System.Collections.Generic;
using Admin.Dtos;
using Admin.Entities;
using System.Threading.Tasks;
using Admin.Helper;

namespace Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            try
            {
                var users = (await _repository.GetAllUsersAsync()).Select(x=> x.AsUserDto());
                return  Ok(users);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<UserDto>> GetUsersByIdAsync(Guid Id)
        {
            try
            {
                //this will return notfound since each time the in mem users list is fetched it will have new guid
                var user = await _repository.GetUserByIdAsync(Id);
                if(user is null) return NotFound();
                return  user.AsUserDto();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUsersByEmailAsync(string email)
        {
            try
            {
                var user = await _repository.GetUserByEmailAsync(email);
                if(user is null) return NotFound();
                return  user.AsUserDto();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> GetUsersByCredentialsAsync(LoginCredentialDto loginCredentials)
        {
            try
            {
                var user = await _repository.GetUserByCredentialsAsync(loginCredentials.UserName, loginCredentials.Password);
                if(user is null) return NotFound();
                return  user.AsUserDto();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto newUser)
        {
            try
            {
                User user = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Age = newUser.Age,
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                    CreatedDate = DateTimeOffset.Now
                };
                 await _repository.CreateUserAsync(user);
                if(user is null) return NotFound();
                //below will call and get the result from get user
                //suppresser added for async names in startup.cs
                return CreatedAtAction(nameof(GetUsersAsync),new { Id = user.Id}, user.AsUserDto());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUserAsync(Guid Id)
        {
            try
            {
                return await _repository.DeleteUserAsync(Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(Guid Id, UpdateUserDto userDto)
        {
            try
            {
                 var existingUser = await _repository.GetUserByIdAsync(Id);
                 if(existingUser is null) return NoContent();

                 User updatedUser = existingUser with
                 {
                     FirstName = userDto.FirstName,
                     LastName = userDto.LastName,
                     Password = userDto.Password
                 };

                 await _repository.UpdateUserAsync(updatedUser);
                 
                 return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }

}