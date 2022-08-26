using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webeasy.Models;

namespace Webeasy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WebeasyContext dbContext;

        public UserController(WebeasyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<User>> GetAsync()
        {
            return await this.dbContext.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]User user)
        {
            var userRecord=dbContext.Users.FirstOrDefaultAsync(dbdata=> dbdata.Email == user.Email);
            if (userRecord == null)
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest("Email Already Exist");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetSingleUserId")]
        public async Task<IActionResult> GetByIdAsync(int id) { 

            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("Id Not Found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] User user)
        {

            var data = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.FirstName = user.FirstName;
                data.LastName = user.LastName;
                data.Mobile = user.Mobile;
                data.Password = user.Password;
                data.Cnic = user.Cnic;
                data.Email = user.Email;
                await dbContext.SaveChangesAsync();
                return Ok(data);
            }
            return NotFound("User not found");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            var data = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                dbContext.Remove(data);
                await dbContext.SaveChangesAsync();
                return Ok(data);
            }
            return NotFound("User not found");
        }

    }
}
