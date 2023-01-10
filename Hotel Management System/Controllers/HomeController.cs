using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hotel_Management_System.Models;
using System.Net;
using System.Security.Cryptography.Xml;

namespace Hotel_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeControllerPagesCookiesDestroyer();
            if (GlobalVariables.SurfingMode != false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        public ActionResult Guest()
        {
            GlobalVariables.SurfingMode = true;
            HomeControllerPagesCookiesDestroyer();
            return RedirectToAction("", "Home/Index");
        }

        public ActionResult About()
        {
            HomeControllerPagesCookiesDestroyer();
            ViewBag.Message = "Here Below you will find details about us, about our Hotel!!!.";
            return View();
        }

        public ActionResult Contact()
        {
            HomeControllerPagesCookiesDestroyer();
            return View();
        }

        public ActionResult Login()
        {
            HomeControllerPagesCookiesDestroyer();
            if (GlobalVariables.SurfingMode != false)
            {
                return View();
            }
            else if(GlobalVariables.SurfingMode != true)
            {
                return View();
            }
            else
            {
                ViewData["GuestAlert"] = "<script>alert('Kindly Please Login/Signup Create your account as our Customer first then your Room Booking will Proceed!!!')</script>";
                return RedirectToAction("", "Home/Login");////Work-from-Here!!!!!
            }
        }
        [HttpPost]
        public ActionResult Login(Employee Login)
        {
            using (var HMSDB = new Hotel_Management_SystemEntities())
            {
                GlobalVariables.IsValidCustomerUser = HMSDB.Customers.Any(login => Login.Username == login.Username);
                GlobalVariables.IsValidCustomerPassword = HMSDB.Customers.Any(login => Login.Password == login.Password);
                GlobalVariables.IsValidUser = HMSDB.Employees.Any(login => Login.Username == login.Username);
                GlobalVariables.IsValidPassword = HMSDB.Employees.Any(login => Login.Password == login.Password);
                if (GlobalVariables.IsValidUser == true && GlobalVariables.IsValidPassword == true)
                {
                       var LoginCredentials = HMSDB.Employees.Where(
                                    login => Login.Username == login.Username && Login.Password == login.Password).FirstOrDefault();
                    if (LoginCredentials != null && ModelState.IsValid == true)
                    {
                        GlobalVariables.IsAdmin = LoginCredentials.Employee_Department.Contains("Admin-Owner") ||
                            LoginCredentials.Employee_Department.Contains("Manager") || LoginCredentials.Employee_Department.Contains("Co-Manager") ||
                            LoginCredentials.Employee_Department.Contains("Owner");
                        if (GlobalVariables.IsAdmin == true && GlobalVariables.IsValidUser == true && GlobalVariables.IsValidPassword == true)
                        {
                            GlobalVariables.CookieName = HttpUtility.UrlEncode(Hotel_Management_System.Controllers.CryptoEngine.Encrypt("LNCTS", Hotel_Management_System.Controllers.CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies = new HttpCookie(GlobalVariables.CookieName);
                            GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(LoginCredentials.Username, CryptoEngine.PPP()), CryptoEngine.Encrypt(LoginCredentials.Username, CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(LoginCredentials.Password, CryptoEngine.PPP()), CryptoEngine.Encrypt(LoginCredentials.Password, CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies.Expires = DateTime.Now.AddMinutes(1000); 
                            GlobalVariables.LoginCredentialsCookies.HttpOnly = true;
                            GlobalVariables.SurfingMode = true;
                            HttpContext.Response.Cookies.Add(GlobalVariables.LoginCredentialsCookies);
                            return RedirectToAction("Index", "EMPLOYEES_VIEW");
                        }
                        else if (GlobalVariables.IsAdmin != true && GlobalVariables.IsValidUser == true && GlobalVariables.IsValidPassword == true)
                        {
                            GlobalVariables.CookieName = HttpUtility.UrlEncode(Hotel_Management_System.Controllers.CryptoEngine.Encrypt("LNCTS", Hotel_Management_System.Controllers.CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies = new HttpCookie(GlobalVariables.CookieName);
                            GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(LoginCredentials.Username, CryptoEngine.PPP()), CryptoEngine.Encrypt(LoginCredentials.Username, CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(LoginCredentials.Password, CryptoEngine.PPP()), CryptoEngine.Encrypt(LoginCredentials.Password, CryptoEngine.PPP()));
                            GlobalVariables.LoginCredentialsCookies.Expires = DateTime.Now.AddMinutes(1000);
                            GlobalVariables.LoginCredentialsCookies.HttpOnly = true;
                            GlobalVariables.SurfingMode = true;
                            HttpContext.Response.Cookies.Add(GlobalVariables.LoginCredentialsCookies);
                            return RedirectToAction("Indexx", "CUSTOMERS_VIEW");
                        }
                        else
                        {
                            ViewBag.Errormsg = "Login Failed";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errormsg = "Login Failed";
                        return View();
                    }
                }
                else if (GlobalVariables.IsValidCustomerUser == true && GlobalVariables.IsValidCustomerPassword == true)
                {
                    var CustomersLoginCredentials = HMSDB.Customers.Where(
                                 login => Login.Username == login.Username && Login.Password == login.Password).FirstOrDefault();
                    if (CustomersLoginCredentials != null && ModelState.IsValid == true)
                    {
                        GlobalVariables.CookieName = HttpUtility.UrlEncode(Hotel_Management_System.Controllers.CryptoEngine.Encrypt("LNCTS", Hotel_Management_System.Controllers.CryptoEngine.PPP()));
                        GlobalVariables.LoginCredentialsCookies = new HttpCookie(GlobalVariables.CookieName);
                        GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(CustomersLoginCredentials.Username, CryptoEngine.PPP()), CryptoEngine.Encrypt(CustomersLoginCredentials.Username, CryptoEngine.PPP()));
                        GlobalVariables.LoginCredentialsCookies.Values.Add(CryptoEngine.Encrypt(CustomersLoginCredentials.Password, CryptoEngine.PPP()), CryptoEngine.Encrypt(CustomersLoginCredentials.Password, CryptoEngine.PPP()));
                        GlobalVariables.LoginCredentialsCookies.Expires = DateTime.Now.AddMinutes(1000);
                        GlobalVariables.LoginCredentialsCookies.HttpOnly = true;
                        GlobalVariables.SurfingMode = true;
                        HttpContext.Response.Cookies.Add(GlobalVariables.LoginCredentialsCookies);
                        GlobalVariables.CustomerID = CustomersLoginCredentials.Customer_ID;
                        GlobalVariables.CustomerName = CustomersLoginCredentials.Customer_First_name;
                        GlobalVariables.CustomerEmail = CustomersLoginCredentials.Customer_Email_Address;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Errormsg = "Login Failed";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Errormsg = "Login Failed";
                    return View();
                }
            }
        }
        public ActionResult Logout()
        {
            GlobalVariables.LoginCredentialsCookies.Expires = DateTime.Now.AddMinutes(-1);
            GlobalVariables.LoginCredentialsCookies.HttpOnly = true;
            System.Web.HttpContext.Current.Response.Cookies.Set(GlobalVariables.LoginCredentialsCookies); GlobalVariables.CookieName = null;
            GlobalVariables.IsAdmin = false; GlobalVariables.IsValidUser = false; GlobalVariables.IsValidPassword = false;
            return RedirectToAction("", "Home/Login");
        }
        public void HomeControllerPagesCookiesDestroyer()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() != true)
            {
                if (System.Web.HttpContext.Current.Response.Cookies.Count != 0 || System.Web.HttpContext.Current.Request.Cookies.Count != 0)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Cookies.Count; i++)
                    {
                        HttpCookie cookies = new HttpCookie(System.Web.HttpContext.Current.Request.Cookies.GetKey(i).ToString());
                        cookies.Expires = DateTime.Now.AddMinutes(-1);
                        cookies.HttpOnly = true;
                        System.Web.HttpContext.Current.Response.Cookies.Set(cookies);
                    }
                    //GlobalVariables.GuestMode = false;
                }
            }
        }
    }
    static class CryptoEngine
    {
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        //public static string Decrypt(string input, string key)
        //{
        //    byte[] inputArray = Convert.FromBase64String(input);
        //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        //    tripleDES.Mode = CipherMode.ECB;
        //    tripleDES.Padding = PaddingMode.PKCS7;
        //    ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        //    tripleDES.Clear();
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}
        public static string PPP()
        {
            byte[] bytes = new byte[16];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            string D = Convert.ToBase64String(bytes);
            return D;
        }
    }
}