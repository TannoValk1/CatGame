using System;

namespace CatGame.Core.Domain
{
    public enum CatType
    {
        Lightning, Water, Fire, Earth, Air, Blood, Demonic
    }

    public enum CatStatus
    {
        Dead, Alive, GuardingPlanet
    }

    public class Cat
    {
        public Guid ID { get; set; }
        public string CatName { get; set; }
        public int CatHealth { get; set; }
        public int CatXP { get; set; }
        public int CatXPNextLevel { get; set; }
        public int CatLevel { get; set; }
        public CatType CatType { get; set; }
        public CatStatus CatStatus { get; set; }
        public int PrimaryAttackPower { get; set; }
        public string PrimaryAttackName { get; set; }
        public int SecondaryAttackPower { get; set; }
        public string SecondaryAttackName { get; set; }
        public int SpecialAttackPower { get; set; }
        public string SpecialAttackName { get; set; }
        public DateTime CatWasBorn { get; set; }
        public DateTime CatDied { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
