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
            /*WebeasyContext we = new WebeasyContext();
            var fname = user.FirstName;
            var lname = user.LastName;
            var Mobile = user.Mobile;
            var Cnic = user.Cnic;
            var Email = user.Email;
            var Password = user.Password;
            */
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);

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
