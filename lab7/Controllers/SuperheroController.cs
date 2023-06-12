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
    public class SuperheroController : ControllerBase
    {
        private readonly SuperHeroesContext _context;

        public SuperheroController(SuperHeroesContext context)
        {
            _context = context;
        }

        // GET: api/Superhero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperheroNeedParAdoption>>> GetSuperheroes()
        {
          if (_context.Superheroes == null)
          {
              return NotFound();
          }
            var superhero = await (from b in _context.Superheroes
                              .Include(b => b.Alignment)
                              .Include(b => b.Superpowers)
                              select new SuperheroNeedParAdoption()
                              {
                                  Superhero_id = b.Superhero_id,
                                  Superhero_name = b.Superhero_name,
                                  Full_name = b.Full_name,
                                  Height_cm = b.Height_cm,
                                  Weigth_kg = b.Weigth_kg,
                                  Alignment = new AlignmentExtradition()
                                  {
                                      Alignment_id = b.Alignment_id,
                                      Alignment_name = b.Alignment.Alignment_name
                                  },
                                  Superpowers = (from a in b.Superpowers
                                             select new SuperpowerExtradition()
                                             {
                                                 Superpower_id = a.Superpower_id,
                                                 Superpower_name = a.Superpower_name
                                             }).ToList()
                              }).ToListAsync();
            return superhero;
        }

        // GET: api/Superhero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperheroNeedParAdoption>> GetSuperheroAllParModel(int id)
        {
            if (_context.Superheroes == null)
            {
                return NotFound();
            }
            var superhero = await (from b in _context.Superheroes
                              .Include(b => b.Alignment)
                              .Include(b => b.Superpowers)
                                   select new SuperheroNeedParAdoption()
                                   {
                                       Superhero_id = b.Superhero_id,
                                       Superhero_name = b.Superhero_name,
                                       Full_name = b.Full_name,
                                       Height_cm = b.Height_cm,
                                       Weigth_kg = b.Weigth_kg,
                                       Alignment = new AlignmentExtradition()
                                       {
                                           Alignment_id = b.Alignment_id,
                                           Alignment_name = b.Alignment.Alignment_name
                                       },
                                       Superpowers = (from a in b.Superpowers
                                                      select new SuperpowerExtradition()
                                                      {
                                                          Superpower_id = a.Superpower_id,
                                                          Superpower_name = a.Superpower_name
                                                      }).ToList()
                                   }).FirstOrDefaultAsync(i => i.Superhero_id == id);
            if (superhero == null)
            {
                return NotFound();
            }
            return superhero;
        }

        // PUT: api/Superhero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperheroAllParModel(int id, SuperheroNeedParExtradition superheroNeedParExtradition)
        {
            if (id != superheroNeedParExtradition.Superhero_id)
            {
                return BadRequest();
            }
            var newHero = await _context.Superheroes.FindAsync(id);
            if (newHero == null)
            {
                return NotFound();
            }
            newHero.Superhero_id = superheroNeedParExtradition.Superhero_id;
            newHero.Superhero_name = superheroNeedParExtradition.Superhero_name;
            newHero.Full_name = superheroNeedParExtradition.Full_name;
            newHero.Height_cm = superheroNeedParExtradition.Height_cm;
            newHero.Weigth_kg = superheroNeedParExtradition.Weigth_kg;
            newHero.Alignment_id = superheroNeedParExtradition.Alignment_id;
     

            _context.Entry(newHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!superheroNeedParExtraditionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }

            return NoContent();
        }

        // POST: api/Superhero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SuperheroAllParModel>> PostSuperheroAllParModel(SuperheroNeedParExtradition superheroNeedParExtradition)
        {
          if (_context.Superheroes == null)
          {
              return Problem("Entity set 'SuperHeroesContext.Superheroes'  is null.");
          }
            var hero = new SuperheroAllParModel()
            {
                Superhero_id = superheroNeedParExtradition.Superhero_id,
            Superhero_name = superheroNeedParExtradition.Superhero_name,
            Full_name = superheroNeedParExtradition.Full_name,
            Height_cm = superheroNeedParExtradition.Height_cm,
            Weigth_kg = superheroNeedParExtradition.Weigth_kg,
            Alignment_id = superheroNeedParExtradition.Alignment_id
        };
            _context.Superheroes.Add(hero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (superheroNeedParExtraditionExists(hero.Superhero_id))
                {
                    return Conflict();
                }
                else
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetSuperheroAllParModel", new { id = hero.Superhero_id }, hero);
        }

        // DELETE: api/Superhero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperheroAllParModel(int id)
        {
            if (_context.Superheroes == null)
            {
                return NotFound();
            }
            var superheroAllParModel = await _context.Superheroes.FindAsync(id);
            if (superheroAllParModel == null)
            {
                return NotFound();
            }

            _context.Superheroes.Remove(superheroAllParModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool superheroNeedParExtraditionExists(int id)
        {
            return (_context.Superheroes?.Any(e => e.Superhero_id == id)).GetValueOrDefault();
        }
    }
}
