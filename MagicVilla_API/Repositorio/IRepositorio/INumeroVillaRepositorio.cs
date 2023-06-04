﻿using MagicVilla_API.Models;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio : IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Update(NumeroVilla numeroVilla);
    }
}
