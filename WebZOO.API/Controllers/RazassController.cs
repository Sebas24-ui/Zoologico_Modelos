using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoologico_Modelos;

namespace WebZOO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazassController : ControllerBase
    {
        private readonly WebZOOAPIContext _context;

        public RazassController(WebZOOAPIContext context)
        {
            _context = context;
        }

        // GET: api/Razas
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Raza>>>> GetRaza()
        {
            try

            {
                var data = await _context.Razas.ToListAsync();
                return ApiResult<List<Raza>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Raza>>.Fail(ex.Message);
            }
        }

        // GET: api/Razas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> GetRaza(int id)
        {
            try
            {
                var raza = await _context.Razas
                    .Include(r => r.Animales)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (raza == null)
                {
                    return ApiResult<Raza>.Fail("Datos no encontrados");
                }

                return ApiResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);
            }
        }

        // PUT: api/Razas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> PutRaza(int id, Raza raza)
        {
            if (id != raza.Id)
            {
                return ApiResult<Raza>.Fail("ID no coincide");
            }

            _context.Entry(raza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await RazaExists(id))
                {
                    return ApiResult<Raza>.Fail("Datos no encontrados ");
                }
                else
                {
                    return ApiResult<Raza>.Fail(ex.Message);
                }
            }

            return ApiResult<Raza>.Ok(raza);
        }

        // POST: api/Razas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Raza>>> PostRaza(Raza raza)
        {
            try
            {
                _context.Razas.Add(raza);
                await _context.SaveChangesAsync();

                return ApiResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);

            }
        }

        // DELETE: api/Razas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Raza>>> DeleteRaza(int id)
        {
            try
            {
                var raza = await _context.Razas.FindAsync(id);
                if (raza == null)
                {
                    return ApiResult<Raza>.Fail("Datos no encontrados");
                }

                _context.Razas.Remove(raza);
                await _context.SaveChangesAsync();

                return ApiResult<Raza>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Raza>.Fail(ex.Message);
            }
        }

        private async Task<bool> RazaExists(int id)
        {
            return await _context.Razas.AnyAsync(e => e.Id == id);
        }
    }
}
