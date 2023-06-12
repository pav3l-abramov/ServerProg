using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Models;

namespace SuperHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperpowerController : ControllerBase
    {
        private readonly SuperHeroesContext _context;

        public SuperpowerController(SuperHeroesContext context)
        {
            _context = context;
        }

        // GET: api/Superpower
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperpowerAdoption>>> GetSuperpowers()
        {
          if (_context.Superpowers == null)
          {
              return NotFound();
          }
            var powers = from a in _context.Superpowers.Include(a => a.Superheroes)
                          select new SuperpowerAdoption()
                          {
                              Superpower_id = a.Superpower_id,
                              Superpower_name = a.Superpower_name,
                              Superheroes = (from b in a.Superheroes
                                             select new SuperheroNeedParExtradition()
                                             {
                                                 Superhero_id = b.Superhero_id,
                                                 Superhero_name = b.Superhero_name,
                                                 Full_name = b.Full_name,
                                                 Height_cm = b.Height_cm,
                                                 Weigth_kg = b.Weigth_kg
                                                
                                             }).ToList()
                          };

            return await powers.ToListAsync();
        }

        // GET: api/Superpower/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperpowerAdoption>> GetSuperpowerModel(int id)
        {
          if (_context.Superpowers == null)
          {
              return NotFound();
          }


            var powers = await (from a in _context.Superpowers.Include(a => a.Superheroes)
                                select new SuperpowerAdoption()
                                {
                                    Superpower_id = a.Superpower_id,
                                    Superpower_name = a.Superpower_name,
                                    Superheroes = (from b in a.Superheroes
                                                   select new SuperheroNeedParExtradition()
                                                   {
                                                       Superhero_id = b.Superhero_id,
                                                       Superhero_name = b.Superhero_name,
                                                       Full_name = b.Full_name,
                                                       Height_cm = b.Height_cm,
                                                       Weigth_kg = b.Weigth_kg

                                                   }).ToList()
                                }).FirstOrDefaultAsync(i => i.Superpower_id == id);


            if (powers == null)
            {
                return NotFound();
            }

            return powers;
        }

        // PUT: api/Superpower/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperpowerModel(int id, SuperpowerExtradition superpowerExtradition)
        {
            if (id != superpowerExtradition.Superpower_id)
            {
                return BadRequest();
            }
            var newPower = await _context.Superpowers.FindAsync(id);
            if (newPower == null)
            {
                return NotFound();
            }
            newPower.Superpower_name = superpowerExtradition.Superpower_name;

            _context.Entry(superpowerExtradition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperpowerModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Superpower
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SuperpowerModel>> PostSuperpowerModel(SuperpowerExtradition superpowerExtradition)
        {
          if (_context.Superpowers == null)
          {
              return Problem("Entity set 'SuperHeroesContext.Superpowers'  is null.");
          }
            var power = new SuperpowerModel()
            {
                Superpower_id = superpowerExtradition.Superpower_id,
                Superpower_name = superpowerExtradition.Superpower_name
            };
            _context.Superpowers.Add(power);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SuperpowerModelExists(power.Superpower_id))
                {
                    return Conflict();
                }
                else
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetSuperpowerModel", new { id = power.Superpower_id }, power);
        }

        // DELETE: api/Superpower/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperpowerModel(int id)
        {
            if (_context.Superpowers == null)
            {
                return NotFound();
            }
            var superpowerModel = await _context.Superpowers.FindAsync(id);
            if (superpowerModel == null)
            {
                return NotFound();
            }

            _context.Superpowers.Remove(superpowerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperpowerModelExists(int id)
        {
            return (_context.Superpowers?.Any(e => e.Superpower_id == id)).GetValueOrDefault();
        }
    }
}
