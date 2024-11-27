namespace CatGame.Models.Cats
{
    public class CatImageViewModel
    {
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? CatID { get; set; }
    }
}
