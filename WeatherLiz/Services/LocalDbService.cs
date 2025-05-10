using SQLite;
using WeatherLiz.Models;

namespace WeatherLiz.Services
{
    public class LocalDbService
    {
        private readonly SQLiteAsyncConnection db;

        public LocalDbService()
        {
            db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "favoritos.db3"));
            db.CreateTableAsync<Favoritos>().Wait();
        }

        public async Task<List<Favoritos>> GetFavoritos()
        {
            return await db.Table<Favoritos>().ToListAsync();
        }

        public async Task<Favoritos> GetOneFavorito(string ciudad)
        {
           
            var ciudadTrimmed = ciudad?.Trim();

            
            return await db.Table<Favoritos>()
                .FirstOrDefaultAsync(c => c.City.ToLower() == ciudad.ToLower() );
        }

        public async Task<int> Agregar(Favoritos favorito)
        {
            var ciudadExistente = await GetOneFavorito(favorito.City);
            if (ciudadExistente == null)
            {
                return await db.InsertAsync(favorito);
            }
            else
            {
                Console.WriteLine("City is already in favorites.");
                return 0;
            }
        }

        public async Task<int> Eliminar(Favoritos ciudad)
        {
            return await db.DeleteAsync(ciudad);
        }

        public async Task UpdateFavorito(Favoritos favorito)
        {
            
            var existingFavorite = await db.Table<Favoritos>()
                                           .FirstOrDefaultAsync(f => f.City.ToLower() == favorito.City.ToLower());

            if (existingFavorite != null)
            {
               
                existingFavorite.Country = favorito.Country;
                existingFavorite.Description = favorito.Description;
                existingFavorite.Icon = favorito.Icon;
                existingFavorite.Temperature = favorito.Temperature;
                existingFavorite.Time = favorito.Time;

            
                await db.UpdateAsync(existingFavorite);
            }
        }


    }
}