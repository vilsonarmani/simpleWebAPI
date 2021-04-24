using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeef.Data;
using testeef.Models;

namespace testeef.Controllers
{
    [ApiController]
    [Route("v1/Products")]
    public class ProductController : ControllerBase
    {
        [HttpGet] 
        [Route("")] 
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context) //FromServices pega o datacontext da memória nao precisa do construtot. **
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")] 
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id) 
        {
            //no trecho (x => x.Category) esta enviando o Category ao inves de Category.Id, pois queremos trazer por referencia o título e o Id da categoria
            var product = await context.Products.Include(x => x.Category)
                .AsNoTracking() //BOAPRATICA: faz com que EF nao crie proxies dos objetos
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")] 
        public async Task<ActionResult<List<Product>>> GetBycategory([FromServices] DataContext context, int id) //pegar os produtos dada uma determinada categoria
        {            
            var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking() 
                .Where(x => x.Id == id)
                .ToListAsync();

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context, //recebo do serviço um datacontext pra ser injetado
            [FromBody] Product model) //do corpo da requisição eu recebo a categoria 
        {
            if (ModelState.IsValid) //valida a categoria (aplica todas as validações do model)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync(); //salva no banco (context)
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
