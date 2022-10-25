using LiteCommerce.BussinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CommonApiController : Controller
    {
        // GET: CommonApi
        public JsonResult CountryApi()
        {
            List<Country> countryData  = new List<Country>();
            countryData = DataService.ListCountries();
            return Json(countryData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CityApi(String CountryName)
        {
            List<City> cityData = new List<City>();
            cityData = DataService.ListCities(CountryName);
            return Json(cityData, JsonRequestBehavior.AllowGet);
        }
    }
}