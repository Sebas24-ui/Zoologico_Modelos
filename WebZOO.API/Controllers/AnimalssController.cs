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
    public class AnimalssController : ControllerBase
    {
        private readonly WebZOOAPIContext _context;

        public AnimalssController(WebZOOAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Animal>>>> GetAnimal()
        {
            try
            {
                return ApiResult<List<Animal>>.Ok(await _context.Animales.ToListAsync());
            }
            catch (Exception ex)
            {
                return ApiResult<List<Animal>>.Fail(ex.Message);
            }
        }

        // GET: api/Animales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> GetAnimal(int id)
        {
            try
            {

                var animal = await _context.Animales
                    .Include(a => a.Especie)
                    .Include(a => a.Raza)
                    .FirstOrDefaultAsync(a => a.Id == id);


                if (animal == null)
                {
                    return ApiResult<Animal>.Fail("Datos no encontrados");
                }

                return ApiResult<Animal>.Ok(animal);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }
        }

        // PUT: api/Animales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> PutAnimal(int id, Animal animal)
        {

            if (id != animal.Id)
            {
                return ApiResult<Animal>.Fail("ID no coincide");
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AnimalExists(id))
                {
                    return ApiResult<Animal>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApiResult<Animal>.Fail(ex.Message);
                }
            }


            return ApiResult<Animal>.Ok(animal);



        }

        // POST: api/Animales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Animal>>> PostAnimal(Animal animal)
        {
            try
            {
                _context.Animales.Add(animal);
                await _context.SaveChangesAsync();

                return ApiResult<Animal>.Ok(animal);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }
        }

        // DELETE: api/Animales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Animal>>> DeleteAnimal(int id)
        {
            try
            {


                var animal = await _context.Animales.FindAsync(id);
                if (animal == null)
                {
                    return ApiResult<Animal>.Fail("Datos no encontrados");
                }

                _context.Animales.Remove(animal);
                await _context.SaveChangesAsync();
                return ApiResult<Animal>.Ok(animal);
            }
            catch (Exception ex)
            {
                return ApiResult<Animal>.Fail(ex.Message);
            }

        }

        private async Task<bool> AnimalExists(int id)
        {
            return await _context.Animales.AnyAsync(e => e.Id == id);
        }
    }
}
