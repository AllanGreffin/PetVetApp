using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetVetApp.Data;
using PetVetApp.DTOs;
using PetVetApp.Models;

namespace PetVetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetPetsByUserId")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPetsByUserId([FromQuery]Guid userId)
        {
            return await _context.Pet.Where(x => x.UserId == userId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetById(Guid? id)
        {
            var pet = await _context.Pet.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return pet;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetById(Guid? id, PetDTO petDTO)
        {
            if (id != petDTO.Id)
            {
                return BadRequest();
            }
            var pet = new Pet().ConvertFromDTO(petDTO);

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return AcceptedAtAction(null, new { Id = pet.Id });
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(PetDTO petDTO)
        {
            var pet = new Pet().ConvertFromDTO(petDTO);
            _context.Pet.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, new { id = pet.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(Guid? id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(Guid? id)
        {
            return _context.Pet.Any(e => e.Id == id);
        }
    }
}
