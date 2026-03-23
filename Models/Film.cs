
namespace SinemaDunyasi.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Yonetmen { get; set; }
        public string? Tur { get; set; }
        public int Yil { get; set; }
        public double Puan { get; set; }
        public bool VizyondaMi { get; set; }
        public string? AfisUrl { get; set; } 
    }
}