using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppViagem.Models
{
    public class Viagem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string OrigemViagem { get; set; }
        public string DestinoViagem { get; set; }
        public string DistanciaViagem { get; set; }
        public string RendimentoViagem { get; set; }
        public string CombustivelViagem { get; set; }

        public string DescViagem { get; set; }

        public string LitroViagem { get; set; }
        public string TotalViagem { get; set; }
        
    }
}
