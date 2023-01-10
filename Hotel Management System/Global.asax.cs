using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Hotel_Management_System.Models;
using Hotel_Management_System.Controllers;

namespace Hotel_Management_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    public static class GlobalVariables
    {
        public static bool SurfingMode { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsValidUser { get; set; }
        public static bool IsValidPassword { get; set; }
        public static bool IsValidCustomerUser { get; set; }
        public static bool IsValidCustomerPassword { get; set; }
        public static string CookieName { get; set; }
        public static HttpCookie LoginCredentialsCookies { get; set; }
        public static Nullable<int> TID, CID, DAA, DAYSOFINTERVAL;
        public static Nullable<DateTime> DIN, DOT;
        public static int CustomerID { get; set; }
        public static int ReservationID { get; set; }
        public static string CustomerName { get; set; }
        public static string CustomerEmail { get; set; }
        public static string Room_Category { get; set; }
        //public static string Data { get; set; }
        //public static string ConfirmData()
        //{
        //    using (var HMSDB = new Hotel_Management_SystemEntities())
        //    {
        //        var CustomerInfo = HMSDB.Customers.Where(CPN => int.Parse(Data) == CPN.Customer_ID).FirstOrDefault();
        //        string CustomerPhoneNo = CustomerInfo.Customer_s_Phone_No_s.ToString();
        //        return CustomerPhoneNo;
        //    }
        //}
        public static bool LOGINAUTHENTICATION()
        {
            if (IsValidUser == true && IsValidPassword == true)
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(GlobalVariables.LoginCredentialsCookies.Name) == true)
                {
                    IsValidUser = true;
                    IsValidPassword = true;
                    return true;
                }
                else
                {
                    IsValidUser = false;
                    IsValidPassword = false;
                    return false;
                }
            }
            else if (IsValidCustomerUser == true && IsValidCustomerPassword == true)
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(GlobalVariables.LoginCredentialsCookies.Name) == true)
                {
                    IsValidCustomerUser = true;
                    IsValidCustomerUser = true;
                    return true;
                }
                else
                {
                    IsValidCustomerUser = false;
                    IsValidCustomerUser = false;
                    return false;
                }
            }
            else
            {
                IsValidUser = false;
                IsValidPassword = false;
                return false;
            }
        }
        public static string VIEWSHANDLER()
        {
            string Action;
            if (Hotel_Management_System.GlobalVariables.LOGINAUTHENTICATION() == true && Hotel_Management_System.GlobalVariables.IsAdmin == true)
            {
                Action = "Index";
            }
            else if (Hotel_Management_System.GlobalVariables.LOGINAUTHENTICATION() == true && Hotel_Management_System.GlobalVariables.IsAdmin != true)
            {
                Action = "Indexx";
            }
            else
            {
                Action = "<script>alert('Session Time Out');</script>";
            }
            return Action;
        }
    }
}
