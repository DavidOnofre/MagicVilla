using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //el id es incremental
        public int Id { get; set; }

        public String Name { get; set; }

        public String Detail { get; set; }

        [Required]
        public Double Rate { get; set; } //tarifa

        public int Occupants { get; set; }

        public int Area { get; set; }

        public String ImageUrl { get; set; }

        public String Amenidad { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
