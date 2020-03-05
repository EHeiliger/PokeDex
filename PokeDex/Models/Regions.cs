using System;
using System.Collections.Generic;

namespace PokeDex.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Pokemons = new HashSet<Pokemons>();
        }

        public int Id { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Pokemons> Pokemons { get; set; }
    }
}
