using ClinicManagment.Data;
using ClinicManagment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : Controller
    {
        private readonly ClinicInfoDBContext _dbContext;

        public SignupController(ClinicInfoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Signup>>> GetSignups()
        {
            var signups = await _dbContext.Signups.ToListAsync();

            if (signups == null || !signups.Any())
            {
                return NotFound();
            }

            return Ok(signups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Signup>> GetSignup(int id)
        {
            var signup = await _dbContext.Signups.FindAsync(id);

            if (signup == null)
            {
                return NotFound();
            }

            return Ok(signup);
        }

        [HttpPost]
        public async Task<ActionResult<Signup>> AddSignup(Signup signup)
        {
            if (string.IsNullOrEmpty(signup.name) || string.IsNullOrEmpty(signup.Email) || string.IsNullOrEmpty(signup.Password))
            {
                return BadRequest("Name, Email, and Password are required fields.");
            }

            _dbContext.Signups.Add(signup);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSignup), new { id = signup.Id }, signup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSignup(int id, Signup signup)
        {
            if (id != signup.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(signup).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSignup(int id)
        {
            var signup = await _dbContext.Signups.FindAsync(id);

            if (signup == null)
            {
                return NotFound();
            }

            _dbContext.Signups.Remove(signup);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool SignupExists(int id)
        {
            return _dbContext.Signups.Any(s => s.Id == id);
        }
    }
}
