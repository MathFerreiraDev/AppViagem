using AppViagem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppViagem.Helpers
{
    public class SQLiteDb_Viagens
    {
        readonly SQLiteAsyncConnection conection_viagens;

        public SQLiteDb_Viagens(string path)
        {
            conection_viagens = new SQLiteAsyncConnection(path);
            conection_viagens.CreateTableAsync<Viagem>().Wait();
        }

        public Task<int> InsertViagem(Viagem v)
        {
            return conection_viagens.InsertAsync(v);
        }

        public Task<List<Viagem>> SelectAllViagens()
        {

            return conection_viagens.Table<Viagem>().ToListAsync();
        }

        public Task<int> DeleteViagem(int id)
        {
            return conection_viagens.Table<Viagem>().DeleteAsync(i => i.Id == id);
        }
    }
}
