using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public record CustomersIndexViewModel
    {
        public IList<Customer> Customers { get; set; }
    }
}