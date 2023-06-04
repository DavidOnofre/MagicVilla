using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaCreateDto
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public String Detail { get; set; }

        public Double Rate { get; set; } //tarifa

        public int Occupants { get; set; }

        public int Area { get; set; }

        public String ImageUrl { get; set; }

        public String Amenidad { get; set; }
    }
}
