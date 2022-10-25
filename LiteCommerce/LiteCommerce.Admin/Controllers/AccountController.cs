using LiteCommerce.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{

    public class AccountController : Controller
    {
        [AllowAnonymous]
        // GET: Account
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Login(String userName = "", String password = "")
        {
            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(userName, CryptHelper.Md5(password));
                if (account == null)
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập bị sai");
                    return View();
                }

                //Ghi nhận cookie cho phiên đăng nhập
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (Request.HttpMethod == "POST")
            {
                var account = CookieHelper.CookieStringToAccount(User.Identity.Name);
                var oldAccount = AccountService.Authorize(account.Email, CryptHelper.Md5(oldPassword));
                if(oldAccount == null)
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                }

                if (!newPassword.Equals(confirmNewPassword))
                {
                    ModelState.AddModelError("", "Mật khẩu nhập lại không khớp");
                }
                if(ModelState.IsValid)
                {
                    AccountService.ChangePassword(account.Email, CryptHelper.Md5(newPassword));
                    return RedirectToAction("Login", "Account");
                }else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
                
        }
    }      
}