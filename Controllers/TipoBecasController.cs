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
    public class TipoBecasController : ControllerBase
    {
        private readonly AlfaContext _context;

        public TipoBecasController(AlfaContext context)
        {
            _context = context;
        }

        // GET: api/TipoBecas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoBeca>>> GetTipoBecas()
        {
            return await _context.TipoBecas.ToListAsync();
        }

        // GET: api/TipoBecas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoBeca>> GetTipoBeca(int id)
        {
            var tipoBeca = await _context.TipoBecas.FindAsync(id);

            if (tipoBeca == null)
            {
                return NotFound();
            }

            return tipoBeca;
        }

        // PUT: api/TipoBecas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoBeca(int id, TipoBeca tipoBeca)
        {
            if (id != tipoBeca.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoBeca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoBecaExists(id))
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

        // POST: api/TipoBecas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoBeca>> PostTipoBeca(TipoBeca tipoBeca)
        {
            _context.TipoBecas.Add(tipoBeca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoBeca", new { id = tipoBeca.Id }, tipoBeca);
        }

        // DELETE: api/TipoBecas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoBeca(int id)
        {
            var tipoBeca = await _context.TipoBecas.FindAsync(id);
            if (tipoBeca == null)
            {
                return NotFound();
            }

            _context.TipoBecas.Remove(tipoBeca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoBecaExists(int id)
        {
            return _context.TipoBecas.Any(e => e.Id == id);
        }
    }
}
