using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using System.Collections.Concurrent;
using System.Net;

namespace WebApiGeoLite2Country.Services
{
    // Se registra como Singleton en Program.cs: builder.Services.AddSingleton<IGeoIpCache, GeoIpCache>();
    public class GeoIpCacheService : IGeoIpCacheService
    {
        public GeoIpCacheService() 
        {
            // Ruta hacia donde tienes guardado tu archivo .mmdb
            // Nota: Asegúrate de que el archivo se copie al directorio de salida
            // https://github.com/P3TERX/GeoLite.mmdb/blob/main/README.md
            string dbPath = Path.Combine(AppContext.BaseDirectory, @"DataBase\GeoLite2-Country.mmdb");

            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"No se encontró la base de datos de GeoIP en: {dbPath}");
            }

            // Inicializamos el lector de MaxMind
            _reader = new DatabaseReader(dbPath);

        }

        private readonly DatabaseReader _reader;

        public CountryResponse ObtenerPais(string ipAddress)
        {
            CountryResponse response = new CountryResponse();            
            response = _reader.Country(ipAddress);            
            return response;
        }
    }
}
