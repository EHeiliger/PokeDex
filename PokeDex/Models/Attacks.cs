using System;
using System.Collections.Generic;

namespace PokeDex.Models
{
    public partial class Attacks
    {
        public Attacks()
        {
            PokemonsAttackId1Navigation = new HashSet<Pokemons>();
            PokemonsAttackId2Navigation = new HashSet<Pokemons>();
            PokemonsAttackId3Navigation = new HashSet<Pokemons>();
            PokemonsAttackId4Navigation = new HashSet<Pokemons>();
        }

        public int Id { get; set; }
        public string Attack { get; set; }

        public virtual ICollection<Pokemons> PokemonsAttackId1Navigation { get; set; }
        public virtual ICollection<Pokemons> PokemonsAttackId2Navigation { get; set; }
        public virtual ICollection<Pokemons> PokemonsAttackId3Navigation { get; set; }
        public virtual ICollection<Pokemons> PokemonsAttackId4Navigation { get; set; }
    }
}
