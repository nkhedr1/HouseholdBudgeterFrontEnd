using HouseholdBudgeterFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HouseholdBudgeterFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
       
        [HttpGet]
        public ActionResult RegisterNewUser()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RegisterNewUser(RegisterNewUserBindingModel model)
        {
            var url = "http://localhost:55669/api/Account/Register";

            var email = model.Email;
            var password = model.Password;
            var confirmPassword = model.ConfirmPassword;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("Email", email));
            parameters.Add(new KeyValuePair<string, string>("Password", password));
            parameters.Add(new KeyValuePair<string, string>("ConfirmPassword", confirmPassword));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<RegisterNewUserBindingModel>(data);

                return RedirectToAction(nameof(HomeController.Index));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<APIModelError>(data);
                foreach (var state in result.ModelState)
                {
                    foreach (var x in state.Value)
                    {
                        ModelState.AddModelError("", x);
                    }
                }

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            throw new Exception("Status not recongnized");
        }

        public ActionResult InternalServiceError()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginBindingModel model)
        {
            var url = "http://localhost:55669/token";

            var userName = model.Email;
            var password = model.Password;
            var grantType = "password";

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("username", userName));
            parameters.Add(new KeyValuePair<string, string>("password", password));
            parameters.Add(new KeyValuePair<string, string>("grant_type", grantType));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var response = httpClient.PostAsync(url, encodedValues).Result;


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<LoginData>(data);

                var cookie = new HttpCookie("MyCookie",
                    result.AccessToken);

                Response.Cookies.Add(cookie);

                return View("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);
                ModelState.AddModelError("", result.ErrorDescription);
                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordBindingModel model)
        {
                       
            var url = "http://localhost:55669/api/Account/ChangePassword";

            var oldPassword = model.OldPassword;
            var newPassword = model.NewPassword;
            var confirmPassword = model.ConfirmPassword;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("OldPassword", oldPassword));
            parameters
                .Add(new KeyValuePair<string, string>("NewPassword", newPassword));
            parameters
                .Add(new KeyValuePair<string, string>("ConfirmPassword", confirmPassword));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<ChangePasswordBindingModel>(data);

                return RedirectToAction("Index", "Home");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);
                ModelState.AddModelError("", "You need to log in for this action");
                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordBindingModel model)
        {

            var url = "http://localhost:55669/api/Account/ForgotPassword";

            var email = model.Email;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("email", email));
         
            var encodedValues = new FormUrlEncodedContent(parameters);
         
            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<ForgotPasswordBindingModel>(data);

                return RedirectToAction("ResetPassword", "Home");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }
  
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordBindingModel model)
        {

            var url = "http://localhost:55669/api/Account/ResetPassword";

            var email = model.Email;
            var password = model.Password;
            var confirmPassword = model.ConfirmPassword;
            var code = model.Code;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("email", email));
            parameters
               .Add(new KeyValuePair<string, string>("password", password));
            parameters
          .Add(new KeyValuePair<string, string>("confirmPassword", confirmPassword));
            parameters
          .Add(new KeyValuePair<string, string>("code", code));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<ResetPasswordBindingModel>(data);

                return RedirectToAction("Login", "Home");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        //public ActionResult TokenAuthentication(string url, string view)
        //{
        //    var cookie = Request.Cookies["MyCookie"];

        //    if (cookie == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    var token = cookie.Value;

        //    var requestUrl = url;

        //    var httpClient = new HttpClient();

        //    httpClient.DefaultRequestHeaders.Add("Authorization",
        //        $"Bearer {token}");

        //    var data = httpClient.GetStringAsync(requestUrl).Result;
        //    return View("view");
        //}
    }
}