using System;
using System.Collections.Generic;

namespace PokeDex.Models
{
    public partial class Types
    {
        public Types()
        {
            PokemonsTypeId1Navigation = new HashSet<Pokemons>();
            PokemonsTypeId2Navigation = new HashSet<Pokemons>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Pokemons> PokemonsTypeId1Navigation { get; set; }
        public virtual ICollection<Pokemons> PokemonsTypeId2Navigation { get; set; }
    }
}
