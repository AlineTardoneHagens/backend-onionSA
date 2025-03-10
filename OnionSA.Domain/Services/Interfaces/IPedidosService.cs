﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionSA.Domain.Services.Interfaces
{
    public interface IPedidosService
    {
        Task<object> CalculaVendasPorRegiao();
        Task<object> CalculaVendasPorProduto();
        Task<object> ListaPedidos();
    }
}
