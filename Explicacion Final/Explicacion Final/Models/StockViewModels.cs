
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explicacion_Final.Models
{
    public class StockViewModels
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
    }
}