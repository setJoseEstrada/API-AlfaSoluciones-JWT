using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alfa.Models;
using Microsoft.AspNetCore.Authorization;

namespace Alfa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BecasController : ControllerBase
    {
        private readonly AlfaContext _context;

        public BecasController(AlfaContext context)
        {
            _context = context;
        }

        // GET: api/Becas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beca>>> GetBecas()
        {
            return await _context.Becas.ToListAsync();
        }

        // GET: api/Becas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beca>> GetBeca(int id)
        {
            var beca = await _context.Becas.FindAsync(id);

            if (beca == null)
            {
                return NotFound();
            }

            return beca;
        }

        // PUT: api/Becas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeca(int id, Beca beca)
        {
            if (id != beca.Id)
            {
                return BadRequest();
            }

            _context.Entry(beca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BecaExists(id))
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

        // POST: api/Becas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beca>> PostBeca(Beca beca)
        {
            _context.Becas.Add(beca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeca", new { id = beca.Id }, beca);
        }

        // DELETE: api/Becas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeca(int id)
        {
            var beca = await _context.Becas.FindAsync(id);
            if (beca == null)
            {
                return NotFound();
            }

            _context.Becas.Remove(beca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BecaExists(int id)
        {
            return _context.Becas.Any(e => e.Id == id);
        }
    }
}
