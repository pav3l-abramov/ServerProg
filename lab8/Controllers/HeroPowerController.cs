using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
    public class HeroPowerController : ControllerBase
    {
        private readonly SuperHeroesContext _context;

        public HeroPowerController(SuperHeroesContext context)
        {
            _context = context;
        }

        // GET: api/HeroPower
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroPowerModel>>> GetHeroPowers()
        {
          if (_context.HeroPowers == null)
          {
              return NotFound();
          }
            return await _context.HeroPowers.ToListAsync();
        }

        // GET: api/HeroPower/5
        [HttpGet("{hero_id}&{power_id}")]
        public async Task<ActionResult<HeroPowerModel>> GetHeroPowerModel(int hero_id,int power_id)
        {
          if (_context.HeroPowers == null)
          {
              return NotFound();
          }
            var heroPowerModel = await _context.HeroPowers.FindAsync(hero_id, power_id);

            if (heroPowerModel == null)
            {
                return NotFound();
            }

            return heroPowerModel;
        }

        // PUT: api/HeroPower/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{hero_id}&{power_id}")]
        [Authorize(Roles = "pavel")]
        public async Task<IActionResult> PutHeroPowerModel(int hero_id, int power_id, HeroPowerModel heroPowerModel)
        {

            if (await _context.HeroPowers.FindAsync(hero_id, power_id) == null)
            {
                return NotFound();
            }


            try
            {
                await PostHeroPowerModel(new HeroPowerModel() { Superhero_id = heroPowerModel.Superhero_id, Superpower_id = heroPowerModel.Superpower_id });
                await DeleteHeroPowerModel(hero_id, power_id);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroPowerModelExists(heroPowerModel.Superhero_id, heroPowerModel.Superpower_id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/HeroPower
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "pavel")]
        public async Task<ActionResult<HeroPowerModel>> PostHeroPowerModel(HeroPowerModel heroPowerModel)
        {
          if (_context.HeroPowers == null)
          {
              return Problem("Entity set 'SuperHeroesContext.HeroPowers'  is null.");
          }
            _context.HeroPowers.Add(heroPowerModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HeroPowerModelExists(heroPowerModel.Superhero_id, heroPowerModel.Superpower_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHeroPowerModel", new { hero_id = heroPowerModel.Superhero_id , power_id = heroPowerModel.Superpower_id }, heroPowerModel);
        }

        // DELETE: api/HeroPower/5
        [HttpDelete("{hero_id}&{power_id}")]
        [Authorize(Roles = "pavel")]
        public async Task<IActionResult> DeleteHeroPowerModel(int hero_id, int power_id)
        {
            if (_context.HeroPowers == null)
            {
                return NotFound();
            }
            var heroPowerModel = await _context.HeroPowers.FindAsync(hero_id, power_id);
            if (heroPowerModel == null)
            {
                return NotFound();
            }

            _context.HeroPowers.Remove(heroPowerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroPowerModelExists(int hero_id,int power_id)
        {
            return (_context.HeroPowers?.Any(e => e.Superhero_id == hero_id&& e.Superpower_id == power_id)).GetValueOrDefault();
        }
    }
}
