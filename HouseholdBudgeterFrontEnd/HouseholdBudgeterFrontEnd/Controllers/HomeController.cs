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

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //Read the response
                var data = response.Content.ReadAsStringAsync().Result;

                //Convert the data back into an object
                var result = JsonConvert.DeserializeObject<RegisterNewUserBindingModel>(data);
            }



            return View("Index");
        }
    }
}