using AppViagem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppViagem.Helpers
{
    public class SQLiteDb_Pedagios
    {
        readonly SQLiteAsyncConnection conection_pedagios;

        public SQLiteDb_Pedagios(string path)
        {
            conection_pedagios = new SQLiteAsyncConnection(path);
            conection_pedagios.CreateTableAsync<Pedagio>().Wait();
        }

        public Task<int> InsertPedagio(Pedagio p)
        {
            return conection_pedagios.InsertAsync(p);
        }

        public Task<List<Pedagio>> SelectAllPedagios()
        {
            return conection_pedagios.Table<Pedagio>().ToListAsync();
        }

        public Task<int> DeletePedagio(int id)
        {
            return conection_pedagios.Table<Pedagio>().DeleteAsync(i => i.Id == id);
        }

    }
}
