using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase {
        [HttpGet]
        public IActionResult GetAll() {
            IActionResult   respuesta;
            try{
                List<Pizza>     entityList;

                entityList  = BD.GetAll();
                respuesta   = Ok(entityList);
            }catch{
                Console.WriteLine("No se han podido cargar las pizzas");
                respuesta = Ok();
            }
            return respuesta;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            IActionResult   respuesta = null;
            try{
                Pizza           entity;

                entity = BD.GetById(id);
                if (entity == null) {
                    respuesta = NotFound();
                } else {
                    respuesta = Ok(entity);
                }
            }catch{
                Console.WriteLine("No se ha podido cargar la pizza");
                respuesta = Ok();
            }

            return respuesta;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza) {
            try{
                int             intRowsAffected;
                intRowsAffected = BD.Insert(pizza);
                return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
            }catch{
                Console.WriteLine("No se a podido Crear una pizza");
                return Ok();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza) {
            IActionResult   respuesta = null;
            try{
                Pizza           entity;
                int             intRowsAffected;

                if (id != pizza.Id) {
                    respuesta =  BadRequest();
                } else {
                    entity = BD.GetById(id);
                    if (entity == null){
                        respuesta = NotFound();
                    } else {
                        intRowsAffected = BD.UpdateById(pizza);
                        if (intRowsAffected > 0){
                            respuesta = Ok(pizza);
                        } else {
                            respuesta = NotFound();
                        }
                    }
                }
            }catch{
                Console.WriteLine("No se ha podido actualizar la pizza");
                respuesta = Ok();
            }
            return respuesta;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            IActionResult   respuesta = null;
            try{
                Pizza           entity;
                int             intRowsAffected;
                
                entity = BD.GetById(id);
                if (entity == null){
                    respuesta = NotFound();
                } else {
                    intRowsAffected = BD.DeleteById(id);
                    if (intRowsAffected > 0){
                        respuesta = Ok(entity);
                    } else {
                        respuesta = NotFound();
                    }
                } 
            }catch{
                Console.WriteLine("No se ha podido borrar la pizza");
                respuesta=Ok();
            }
            return respuesta;
        }
    }
}
