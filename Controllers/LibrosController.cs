using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDePrueba.Context;
using WebApiDePrueba.Entities;
using WebApiDePrueba.Models;

namespace WebApiDePrueba.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>>  Get()
        {
            return await context.Libros.Include(e => e.Autor).ToListAsync();
        }

        [HttpGet("{id}", Name = "GetLibro")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
 
            if (libro == null)
            {
                return NotFound();
            }

            LibroDTO libroDTO = mapper.Map<LibroDTO>(libro);

            return libroDTO;
        }


        [HttpGet("search/{searchValue}", Name = "SearchLibro")]
        public ActionResult<IEnumerable<LibroDTO>> Get(string searchValue)
        {
            var libros =  context.Libros.Where(l => searchValue.Contains(l.Titulo)).ToList();
            var librosDTO = mapper.Map<List<LibroDTO>>(libros);
            return librosDTO;
        }

        [HttpDelete("{id}")]
        public ActionResult<LibroDTO> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            context.Remove(autor);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<LibroDTO> Put(int id, [FromBody] Libro value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<LibroDTO>(libro);
            return new CreatedAtRouteResult("GetLibro", new { id = libro.Id }, autorDTO);
        }

    }
}
