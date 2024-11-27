namespace CatGame.Models.Cats
{

    public class CatCreateViewModel
    {
        public Guid? ID { get; set; }
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
        public DateTime? CatDied { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<TitanImageViewModel> Image { get; set; } = new List<CatImageViewModel>();

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
