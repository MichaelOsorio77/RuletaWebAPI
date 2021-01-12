using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaWebAPI.Data;
using RuletaWebAPI.Models;

namespace RuletaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoulettesController : Controller
    {
        private readonly RouletteContext _context;
        public RoulettesController(RouletteContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> RouletteDetails()
        {
            List<Roulette> RoulettesList = await _context.RouletteItems.ToListAsync();
            ViewData["Header"] = "Roulettes List";
            ViewData["Roulette"] = RoulettesList;
            return RoulettesList;
        }        
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRouletteById(int id)
        {
            var rouletteItem = await _context.RouletteItems.FindAsync(id);
            if (rouletteItem == null)
            {

                return NotFound();
            }           

            return rouletteItem;
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateRoulette()
        {
            Roulette item = new Roulette();
            item.CreationDate = DateTime.Now;
            item.State = "Created";
            _context.RouletteItems.Add(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Roulette>> OpenRoulette(int id)
        {
            Roulette item = await _context.RouletteItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            item.State = "Open";
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
