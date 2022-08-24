using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webeasy.Models;

namespace Webeasy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WebeasyContext dbContext;

        public AuthController(WebeasyContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<User> SignInAsync(string email, string password)
        {
            var user = dbContext.Users.FirstOrDefault(x=> x.Email == email && x.Password == password);

            if(user == null)
            {
                return new User();
            }
            return user;
        }
    }
}
