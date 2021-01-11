using Microsoft.EntityFrameworkCore;
using RuletaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuletaWebAPI.Data
{
    public class RouletteContext : DbContext
    {
        public RouletteContext(DbContextOptions<RouletteContext> options) : base(options) { }
        public DbSet<Roulette> RouletteItems { get; set; }
        public DbSet<Bet> BetItems { get; set; }
    }
}
