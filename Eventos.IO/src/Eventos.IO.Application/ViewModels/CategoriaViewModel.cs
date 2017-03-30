using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelectList Categorias()
        {
            return new SelectList(ListarCategorias(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListarCategorias()
        {
            var categoriaList = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel(){Id = new Guid("6c3703b1-2737-434c-bfbb-3f84f1cc91ff"), Nome = "Congresso"},
                new CategoriaViewModel(){Id = new Guid("9aaf5550-29f0-432a-81ef-591c9ef10b55"), Nome = "Meetup"},
                new CategoriaViewModel(){Id = new Guid("e9c7779e-8cb0-4bb5-aee0-196f36ceefa1"), Nome = "WorkShop"}
            };

            return categoriaList;
        }
    }
}
