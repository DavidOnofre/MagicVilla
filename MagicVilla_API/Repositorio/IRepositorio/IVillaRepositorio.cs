﻿using MagicVilla_API.Models;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IVillaRepositorio : IRepositorio<Villa>
    {
        Task<Villa> Update(Villa villa);
    }
}
