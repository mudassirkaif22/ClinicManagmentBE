using ClinicManagment.Data;
using ClinicManagment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ClinicManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : Controller
    {
        private readonly ClinicInfoDBContext dbcontext;
        public ClinicController(ClinicInfoDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinic()
        {
            if (dbcontext.Clinics == null)
            {
                return NotFound();
            }
            return Ok(dbcontext.Clinics.ToList());
        }

        
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<Clinic>> GetClinic(int id)
        {
            if (dbcontext.Clinics == null)
            {
                return NotFound();
            }
            var clinic = await dbcontext.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return clinic;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Clinic>> AddClinic(Clinic clinic)
        {
            dbcontext.Clinics.Add(clinic);
            await dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClinic), new { id = clinic.ID }, clinic);

        }

        [HttpPut]
        [Route("Update/{id}")]

        public async Task<ActionResult> UpdateClinic(int id, Clinic clinic)
        {
            if (id != clinic.ID)
            {
                return BadRequest();
            }

            dbcontext.Entry(clinic).State = EntityState.Modified;
            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public async Task<ActionResult> DeleteClinic(int id)
        {
            if (dbcontext.Clinics == null)
            {
                return NotFound();
            }
            var room = await dbcontext.Clinics.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            dbcontext.Clinics.Remove(room);
            await dbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}