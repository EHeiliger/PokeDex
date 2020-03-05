using System;
using System.Collections.Generic;

namespace PokeDex.Models
{
    public partial class Pokemons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public int RegionId { get; set; }
        public int TypeId1 { get; set; }
        public int? TypeId2 { get; set; }
        public int AttackId1 { get; set; }
        public int? AttackId2 { get; set; }
        public int? AttackId3 { get; set; }
        public int? AttackId4 { get; set; }

        public virtual Attacks AttackId1Navigation { get; set; }
        public virtual Attacks AttackId2Navigation { get; set; }
        public virtual Attacks AttackId3Navigation { get; set; }
        public virtual Attacks AttackId4Navigation { get; set; }
        public virtual Regions Region { get; set; }
        public virtual Types TypeId1Navigation { get; set; }
        public virtual Types TypeId2Navigation { get; set; }
    }
}
