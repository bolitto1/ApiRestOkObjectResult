using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<OkObjectResult> CrearClientes(Clientes clientes)
        {
            if (string.IsNullOrEmpty(clientes.Identificacion)) {
                return Ok(new
                {
                    procesoExitoso = false,
                    mensaje = "El campo identificacion es requerido"
                });
            }

            var clientes1 = await _context.Clientes.FirstOrDefaultAsync(c=>c.Identificacion == clientes.Identificacion);
            if (clientes1 != null)
            {
                return Ok(new
                {
                    procesoExitoso = false,
                    mensaje = "Ya existe un cliente con dicha identificacion",
                });
            }
            try {
                _context.Clientes.Add(clientes);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    procesoExitoso = true,
                    id = clientes.Id
                });

            }
            catch (Exception ex) {
                return Ok(new
                {
                    procesoExitoso = false,
                    mensaje ="Error en la base de datos"
                });
            }

            return Ok(clientes);
        }


        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return Ok(new
                {
                    mensaje = "No existe cliente con dicha identificacion",
                    procesoExitoso = false
                });
            }
            return Ok(clientes);
        }


        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
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



        // DELETE: api/Clientes/5
        [HttpDelete("{id?}")]
        public async Task<OkObjectResult> DeleteClientes(string id)
        {
            if (id == null) {
                return Ok(new
                {
                    mensaje = "El campo ID es requerido",
                    procesoExitoso = false
                });
            }
            var clientes = await _context.Clientes.FindAsync(Convert.ToInt32( id));
            if (clientes == null)
            {
                return Ok(new
                {
                    mensaje = "El ID del cliente no es valido",
                    procesoExitoso = false
                });
            }
            if (clientes.Estado == false)
            {
                return Ok(new
                {
                    mensaje = "El cliente ya se encuentra inactivo",
                    procesoExitoso = false
                });
            }
            else
            {
                clientes.EstadoDesc = "Inactivo";
                clientes.Estado = false;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    procesoExitoso = true,
                    id = clientes.Id
                });
            }
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
