using LiteCommerce.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public class CookieHelper
    {
        /// <summary>
        /// chuyển đổi tượng kiểu account về thành một json string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String AccountToCookieString(Account value)
        {
            return JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// Chuyển đối tượng string sang kiểu account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Account CookieStringToAccount(string value)
        {
            return JsonConvert.DeserializeObject<Account>(value);
        }
    }
}