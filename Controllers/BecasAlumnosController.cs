using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alfa.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace Alfa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BecasAlumnosController : ControllerBase
    {
        private readonly AlfaContext _context;


        public BecasAlumnosController(AlfaContext context)
        {
            _context = context;
        }

        // GET: api/BecasAlumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BecasAlumno>>> GetBecasAlumnos()
        {

            var muestro = await _context.BecasAlumnos.Select(datos => new
            {
                datos.Id,
                NombreAlumno = datos.IdAlumnoNavigation.Nombre,
                NombreBeca = datos.IdBecasNavigation.Nombre,
                TiopoBeca = datos.IdBecasNavigation.IdTipoNavigation.Nombre


            }).ToListAsync();



            return Ok(muestro);
        }

        // GET: api/BecasAlumnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BecasAlumno>> GetBecasAlumno(int id)
        {
            var becasAlumno = await _context.BecasAlumnos.Select(datos => new
            {
                datos.Id,
                datos.IdAlumno,
                NombreAlumno = datos.IdAlumnoNavigation.Nombre,
                NombreBeca = datos.IdBecasNavigation.Nombre,
                TiopoBeca = datos.IdBecasNavigation.IdTipoNavigation.Nombre


            }).FirstOrDefaultAsync(x => x.Id == id);


           


            if (becasAlumno == null)
            {
                return NotFound();
            }

            return Ok(becasAlumno);
        }

        // PUT: api/BecasAlumnos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBecasAlumno(int id, BecasAlumno becasAlumno)
        {
            if (id != becasAlumno.Id)
            {
                return BadRequest();
            }

            _context.Entry(becasAlumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BecasAlumnoExists(id))
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



        // POST: api/BecasAlumnos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomResponse>> PostBecasAlumno(BecasAlumno becasAlumno)
        {
            CustomResponse cr = new CustomResponse();
            List<BecasAlumno> comparacion = await _context.BecasAlumnos.Where(x => x.IdAlumno == becasAlumno.IdAlumno).ToListAsync();//vecas registradas para ese alumno
            List<Beca> tiposbecas = await _context.Becas.Include("IdTipoNavigation").ToListAsync();



            cr.status = 200;

            if (comparacion.Count() == 2)
            {
                cr.message = "Excedido numero de becas";
                cr.status = 406;
            }
            if (comparacion.Count() == 1)
            {
                int beca1 = tiposbecas.Where(x => x.Id == comparacion.First().IdBecas).First().IdTipoNavigation.Id;
                int beca2 = tiposbecas.Where(x => x.Id == becasAlumno.IdBecas).First().IdTipoNavigation.Id;

                if (beca1 == beca2)
                {
                    cr.message = "No permite el mismo tipo de becas";
                    cr.status = 407;
                }

            }
            if (comparacion.Count() == 0)
            {

                int beca2 = tiposbecas.Where(x => x.Id == becasAlumno.IdBecas).First().IdTipoNavigation.Id;

                if (beca2 == 3)
                {
                    cr.message = "No procede su solicitud ya que debe contar por lo menos debe agregar una beca de tipo Cultural o Deportiva para poder agregar una Educativa";
                    cr.status = 408;
                }

            }
            if (cr.status == 200)
            {

                _context.BecasAlumnos.Add(becasAlumno);
                await _context.SaveChangesAsync();
            }






            return cr;
        }

        // DELETE: api/BecasAlumnos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBecasAlumno(int id)
        {
            var becasAlumno = await _context.BecasAlumnos.FindAsync(id);
            if (becasAlumno == null)
            {
                return NotFound();
            }

            _context.BecasAlumnos.Remove(becasAlumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BecasAlumnoExists(int id)
        {
            return _context.BecasAlumnos.Any(e => e.Id == id);
        }
    }
}
