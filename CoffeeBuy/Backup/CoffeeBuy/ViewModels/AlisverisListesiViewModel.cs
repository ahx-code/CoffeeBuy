using System.Collections.Generic;
using CoffeeBuy.Models;

namespace CoffeeBuy.ViewModels
{
    public class AlisverisListesiViewModel
    {
        public List<Liste> ListeItems { get; set; }
        public decimal ListeTotal { get; set; }
    }
}