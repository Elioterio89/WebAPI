using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Cache;
using WebAPI.Data;
using WebAPI.Model;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TalentoController : ControllerBase
    {
        private readonly DbContextClass _context;
       // private readonly ICacheService _cacheService;
        public TalentoController(DbContextClass context)
        {
            _context = context;
            //_cacheService = cacheService;
        }
        [HttpGet]
        [Route("TalentoLista")]
        public async Task<ActionResult<IEnumerable<Talento>>> Get()
        {
            var talentoCache = new List<Talento>();

                var talento = await _context.Talentos.ToListAsync();
                if (talento.Count > 0)
                {
                    talentoCache = talento;
                  
                }
           // }
            return talentoCache;
        }
        [HttpGet]
        [Route("talentoDetalhe")]
        public async Task<ActionResult<Talento>> Get(int id)
        {
            var talentoCache = new Talento();
            var talentoCacheList = new List<Talento>();
            //talentoCacheList = _cacheService.GetData<List<talento>>("talento");
            //talentoCache = talentoCacheList.Find(x => x.talentoId == id);
            //if (talentoCache == null)
            //{
                talentoCache = await _context.Talentos.FindAsync(id);
           // }
            return talentoCache;
        }
        [HttpPost]
        [Route("Criartalento")]
        public async Task<ActionResult<Talento>> POST(Talento talento)
        {
            _context.Talentos.Add(talento);
            await _context.SaveChangesAsync();
           // _cacheService.RemoveData("talento");
            return CreatedAtAction(nameof(Get), new
            {
                id = talento.Id
            }, talento);
        }
        [HttpPost]
        [Route("Deletetalento")]
        public async Task<ActionResult<IEnumerable<Talento>>> Delete(int id)
        {
            var talento = await _context.Talentos.FindAsync(id);
            if (talento == null)
            {
                return NotFound();
            }
            _context.Talentos.Remove(talento);
          //  _cacheService.RemoveData("talento");
            await _context.SaveChangesAsync();
            return await _context.Talentos.ToListAsync();
        }
        [HttpPost]
        [Route("Updatetalento")]
        public async Task<ActionResult<IEnumerable<Talento>>> Update(int id, Talento talento)
        {
            if (id != talento.Id)
            {
                return BadRequest();
            }
            var talentoData = await _context.Talentos.FindAsync(id);
            if (talentoData == null)
            {
                return NotFound();
            }
           
            talentoData.Email = talento.Email;
            talentoData.Nome = talento.Nome;
            talentoData.Curriculo = talento.Curriculo;
         
            await _context.SaveChangesAsync();
            return await _context.Talentos.ToListAsync();
        }
    }
}