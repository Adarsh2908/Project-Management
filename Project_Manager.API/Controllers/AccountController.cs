using Microsoft.AspNetCore.Mvc;
using Project_Manager.API.Base_Controllers;
using Project_Manager.API.Models;
using Project_Manager.API.Services;

namespace Project_Manager.API.Controllers
{
    public class AccountController(UserService userService, AccountService accountService) : BaseController
    {
        private readonly UserService _userService = userService;
        private readonly AccountService _accountService = accountService;

        // Register User
        [HttpPost("RegisterUser/{id}")]
        public async Task<ActionResult<UserCredentials>> RegisterUser(int id)
        {
            try
            {
                if(_userService.GetUser(id) is null) 
                {
                    return StatusCode(StatusCodes.Status404NotFound,"User Does not Exist");
                }
                if(_userService.IsUserVerified(id) is null) 
                {
                    return StatusCode(StatusCodes.Status403Forbidden,"Email is Not Verified.");
                }
                
            }
            catch (Exception ex)
            {

            }
        }

    }
}
