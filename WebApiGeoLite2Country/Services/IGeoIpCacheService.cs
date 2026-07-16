using MaxMind.GeoIP2.Responses;

namespace WebApiGeoLite2Country.Services
{
    public interface IGeoIpCacheService
    {
        CountryResponse ObtenerPais(string ipAddress);
    }
}
