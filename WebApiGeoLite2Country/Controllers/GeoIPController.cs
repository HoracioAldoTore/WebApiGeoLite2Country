using MaxMind.GeoIP2.Responses;
using Microsoft.AspNetCore.Mvc;
using WebApiGeoLite2Country.Services;

namespace WebApiGeoLite2Country.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoIPController : ControllerBase
    {
        public GeoIPController(IGeoIpCacheService geoIpCacheService)
        {
            _geoIpCacheService = geoIpCacheService;
        }

        private IGeoIpCacheService _geoIpCacheService;

        // URL: GET /GeoIP/GetCountry?ip=10.2.10.9
        // URL: GET http://localhost:52581/GeoIP/GetCountry?ip=186.0.154.98 //Argentina - AR
        // URL: GET http://localhost:52581/GeoIP/GetCountry?ip=181.188.132.167 //Bolivia - BO
        // URL: GET http://localhost:52581/GeoIP/GetCountry?ip=95.165.132.88 //Russia - RU
        [HttpGet("GetCountry")]
        public ActionResult<string> Get(string ip)
        {
            ActionResult<string> result = new ActionResult<string>("");           
            try
            {
                CountryResponse pais = _geoIpCacheService.ObtenerPais(ip);
                string msg = $"{pais.Country.Name} - {pais.Country.IsoCode}";
                result = Ok(msg);
            }
            catch (MaxMind.GeoIP2.Exceptions.AddressNotFoundException)
            {
                result =  $"{{\"error\": \"{ip} no encontrada en la base de datos.\"}}";
                result = NotFound(result);
            }
            catch (Exception ex)
            {
                result = $"{{\"error\": \"{ex.Message}\"}}";
                result = BadRequest(result);
            }

            return result;
        }
    }
}

