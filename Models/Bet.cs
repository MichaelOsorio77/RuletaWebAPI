using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RuletaWebAPI.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public string PlayedValue { get; set; }
        public double StakeValue { get; set; }
        public string BetType { get; set; }
        [ForeignKey("FK_Roulette")]
        public int IdRoulette { get; set; }
    }
}
