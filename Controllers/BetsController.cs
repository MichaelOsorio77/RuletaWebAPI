using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaWebAPI.Data;
using RuletaWebAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RuletaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : Controller
    {
        private readonly RouletteContext _context;

        public BetsController(RouletteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bet>>> BetDetails()
        {
            List<Bet> BetsList = await _context.BetItems.ToListAsync();
            return View(BetsList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Bet>>> BetDetailsByRoulette(int id)
        {
            var betsItem = await _context.BetItems.Where(z => z.IdRoulette == id).ToListAsync();
            if (betsItem == null)
            {

                return NotFound();
            }
            ViewData["Header"] = "Bets List";
            ViewData["Bets"] = betsItem;
            ViewData["idRoulette"] = id;
            return View(betsItem);
        }
        [HttpPost("{idRoulette}")]
        public async Task<ActionResult<Bet>> CreateBetByRoulette(int idRoulette, Bet item)
        {
            item.IdRoulette = idRoulette;
            if (item.StakeValue > 10000)
            {
                return BadRequest("La apuesta debe ser menor a 10000 dólares.");
            }
            if (item.BetType == "Color")
            {
                if (!item.PlayedValue.Contains("Red") || !item.PlayedValue.Contains("Black"))
                {
                    return BadRequest("Los colores apostados solo pueden ser Rojo o Negro");
                }
            }
            if (item.BetType == "Number")
            {
                Int16 BetValue;
                if (Int16.TryParse(item.PlayedValue, out BetValue))
                {
                    if (BetValue > 36 || BetValue < 0)
                    {
                        return BadRequest("La apuesta solo es de 0 a 36");
                    }
                }
                else
                {
                    return BadRequest("La apuesta no es numérica");
                }
            }
            _context.BetItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        [HttpPut("{idRoulette}")]
        public async Task<ActionResult<IEnumerable<Bet>>> CloseBetByRoulette(int idRoulette)
        {
            List<Bet> BetList = await _context.BetItems.Where(z => z.IdRoulette == idRoulette).ToListAsync();
            if (BetList == null)
            {
                return BadRequest("No se encontró ruleta a cerrar");
            }
            Random random = new Random();
            int winingNumber = random.Next(0,37);
            foreach (Bet bet in BetList)
            {
                if (Int16.Parse(bet.PlayedValue) == winingNumber)
                {
                    bet.State = "Win";
                    bet.ObtainedValue = (bet.StakeValue * 5).ToString();
                }
                else if ((winingNumber % 2 == 0 && Int16.Parse(bet.PlayedValue) % 2 == 0)
                    || (winingNumber % 2 != 0 && Int16.Parse(bet.PlayedValue) % 2 != 0))
                {
                    bet.State = "Win";
                    bet.ObtainedValue = (bet.StakeValue * 1.8).ToString();
                }
                else
                {
                    bet.State = "Lose";
                }
                _context.Entry(bet).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            Roulette roulette = await _context.RouletteItems.FindAsync(idRoulette);
            roulette.State = "Closed";
            roulette.WinningNumber = winingNumber;
            _context.Entry(roulette).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return BetList;
        }
    }
}
