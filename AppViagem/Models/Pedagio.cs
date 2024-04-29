using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppViagem.Models
{
    public class Pedagio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomePedagio { get; set; }
        public string PrecoPedagio { get; set; }
        public string EstacaoPedagio { get; set; }
    }
}
