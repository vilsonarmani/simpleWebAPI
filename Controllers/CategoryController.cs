using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testeef.Data;
using testeef.Models;

namespace testeef.Controllers
{
    [ApiController]
    [Route("v1/Categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet] // verbo http (por padrão é GET)
        [Route("")] //rota ja está especificada no annotation Route; O que estiver aqui é concatenado com o annotation
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context) //FromServices pega o datacontext da memória nao precisa do construtot. **
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromServices] DataContext context, //recebo do serviço um datacontext pra ser injetado
            [FromBody] Category model) //do corpo da requisição eu recebo a categoria 
        {
            if (ModelState.IsValid) //valida a categoria (aplica todas as validações do model)
            {
                context.Categories.Add(model); 
                await context.SaveChangesAsync(); //salva no banco (context)
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

    /*
     ** old construction:
        private DataContext _context;
        
        public CategoryController(DataContext context)
        {
            _context = context;
        }
     Essa forma veio para facilitar pois utilizando um annotation no argumento da função vc já tem toda a injeção de dependencia sendo utilizada diretamente
     */
}
