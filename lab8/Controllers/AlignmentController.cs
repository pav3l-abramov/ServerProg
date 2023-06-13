using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Models;

namespace SuperHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlignmentController : ControllerBase
    {
        private readonly SuperHeroesContext _context;

        public AlignmentController(SuperHeroesContext context)
        {
            _context = context;
        }

        // GET: api/Alignment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlignmentAdoption>>> GetAlignments()
        {
          if (_context.Alignments == null)
          {
              return NotFound();
          }
            var aligment = from b in _context.Alignments
                            .Include(b => b.Superheroes)
                            select new AlignmentAdoption()
                            {
                                Alignment_id = b.Alignment_id,
                                Alignment_name = b.Alignment_name,
                                Superheroes = (from a in b.Superheroes
                                         select new SuperheroNeedParExtradition()
                                         {
                                             Superhero_id = a.Superhero_id,
                                             Superhero_name = a.Superhero_name,
                                             Full_name = a.Full_name,
                                             Height_cm = a.Height_cm,
                                             Weigth_kg = a.Weigth_kg
                                         }).ToList()
                            };

            return await aligment.ToListAsync();
        }

        // GET: api/Alignment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlignmentAdoption>> GetAlignmentModel(int id)
        {
          if (_context.Alignments == null)
          {
              return NotFound();
          }
            var aligment = await (from b in _context.Alignments
                            .Include(b => b.Superheroes)
                                  select new AlignmentAdoption()
                                  {
                                      Alignment_id = b.Alignment_id,
                                      Alignment_name = b.Alignment_name,
                                      Superheroes = (from a in b.Superheroes
                                                     select new SuperheroNeedParExtradition()
                                                     {
                                                         Superhero_id = a.Superhero_id,
                                                         Superhero_name = a.Superhero_name,
                                                         Full_name = a.Full_name,
                                                         Height_cm = a.Height_cm,
                                                         Weigth_kg = a.Weigth_kg
                                                     }).ToList()
                                  }).FirstOrDefaultAsync(b => b.Alignment_id == id);

            if (aligment == null)
            {
                return NotFound();
            }

            return aligment;
        }

        // PUT: api/Alignment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "pavel")]
        public async Task<IActionResult> PutAlignmentModel(int id, AlignmentExtradition alignmentExtradition)
        {
            if (id != alignmentExtradition.Alignment_id)
            {
                return BadRequest();
            }
            var newAligment = await _context.Alignments.FindAsync(id);
            if (newAligment == null) 
            {
                return NotFound(); 
            }
            newAligment.Alignment_name = alignmentExtradition.Alignment_name;

            _context.Entry(newAligment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlignmentModelExists(id))
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

        // POST: api/Alignment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "pavel")]
        public async Task<ActionResult<AlignmentModel>> PostAlignmentModel(AlignmentExtradition alignmentExtradition)
        {
          if (_context.Alignments == null)
          {
              return Problem("Entity set 'SuperHeroesContext.Alignments'  is null.");
          }
            var aligment = new AlignmentModel()
            {
                Alignment_id = alignmentExtradition.Alignment_id,
                Alignment_name = alignmentExtradition.Alignment_name
            };
            _context.Alignments.Add(aligment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlignmentModelExists(aligment.Alignment_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlignmentModel", new { id = aligment.Alignment_id }, aligment);
        }

        // DELETE: api/Alignment/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "pavel")]
        public async Task<IActionResult> DeleteAlignmentModel(int id)
        {
            if (_context.Alignments == null)
            {
                return NotFound();
            }
            var alignmentModel = await _context.Alignments.FindAsync(id);
            if (alignmentModel == null)
            {
                return NotFound();
            }

            _context.Alignments.Remove(alignmentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlignmentModelExists(int id)
        {
            return (_context.Alignments?.Any(e => e.Alignment_id == id)).GetValueOrDefault();
        }
    }
}
