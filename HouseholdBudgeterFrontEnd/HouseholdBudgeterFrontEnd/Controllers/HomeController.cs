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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
                ViewBag.ErrorMessage = result;
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

            return RedirectToAction(nameof(HomeController.Index));
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

            var data = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<LoginData>(data);

            // Session["Token"] = result.AccessToken;

            var cookie = new HttpCookie("MyFavoriteCookie",
                result.AccessToken);

            Response.Cookies.Add(cookie);

            return View("Index");
        }
    }
}