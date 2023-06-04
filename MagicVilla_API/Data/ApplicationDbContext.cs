using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_API.Data
{

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }


        //alimentar con datos a la base de datos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                    new Villa()
                    {
                        Id = 1,
                        Name = "Villa Real",
                        Detail = "Villa con vista al mar",
                        ImageUrl = "",
                        Occupants = 5,
                        Area = 100,
                        Rate = 15,
                        Amenidad = "",
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    },
                    new Villa
                    {
                        Id = 2,
                        Name = "Villa Alta Cumbre",
                        Detail = "Villa en la cordillera de los Andes",
                        ImageUrl = "",
                        Occupants = 5,
                        Area = 100,
                        Rate = 15,
                        Amenidad = "",
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    }
                );
        }

    }
}
