using Microsoft.AspNetCore.Mvc;
using Project_Manager.API.Base_Controllers;
using Project_Manager.API.Models;
using Project_Manager.API.Models.DTO;
using Project_Manager.API.Services;

namespace Project_Manager.API.Controllers
{
    public class UserController(UserService userService) : BaseController
    {
        readonly UserService userService = userService;

        // Get User with Given Id
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                User user = await userService.GetUser(id);
                return user is null ? NotFound("User Not Found") : Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // Create New User
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(NewUserDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("User Data is Not Valid");
            }
            try
            {
                User newUser = await userService.CreateUser(user);
                if(newUser != null)
                {
                    return newUser;
                }
                return BadRequest("Something wrong happened");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update User 
        [HttpPut]
        //public async Task<ActionResult<User>> UpdateUser()

        // Delete User
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                // Check if User Exists 
                User user = await userService.GetUser(id);
                if (user is null)
                {
                     return NotFound("User doesn't Exist.");
                }
                await userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
