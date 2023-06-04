using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto { Id= 1, Name ="Vista desde la piscina", Occupants = 10, Area = 100},
            new VillaDto { Id= 2, Name ="Vista desde la playa", Occupants = 20, Area = 200}
        };

    }
}
