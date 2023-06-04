using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public String Detail { get; set; }

        [Required]
        public Double Rate { get; set; } //tarifa

        [Required]
        public int Occupants { get; set; }

        [Required]
        public int Area { get; set; }

        [Required]
        public String ImageUrl { get; set; }

        public String Amenidad { get; set; }
    }
}
