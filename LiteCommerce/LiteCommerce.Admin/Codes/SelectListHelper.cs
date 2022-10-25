using LiteCommerce.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Codes
{
    public class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list; 
        }
        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }
            return list;
        }
    }
}