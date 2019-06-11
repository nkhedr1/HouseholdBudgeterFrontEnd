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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(int id, CreateCategoryBindingModel model)
        {

            var url = $"http://localhost:55669/api/Category/CreateCategory/{id}";
            var name = model.Name;
            var description = model.Description;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("name", name));
            parameters
               .Add(new KeyValuePair<string, string>("description", description));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<CreateCategoryBindingModel>(data);

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["notOwnerError"] = "You do not belong to this household or this household does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult EditCategory(int id, int householdId)
        {
            var url = $"http://localhost:55669/api/Category/EditCategory/{householdId}/{id}";

            var httpClient = new HttpClient();

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = httpClient.GetStringAsync(url).Result;

                var result = JsonConvert.DeserializeObject<CreateCategoryBindingModel>(data);

                return View(result);
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["notOwnerError"] = "You do not belong to this household or this household does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpPost]
        public ActionResult EditCategory(int id, CreateCategoryBindingModel model)
        {

            var url = $"http://localhost:55669/api/Category/EditCategory/{householdId}/{id}";

            var name = model.Name;
            var description = model.Description;

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("name", name));
            parameters
               .Add(new KeyValuePair<string, string>("description", description));

            var encodedValues = new FormUrlEncodedContent(parameters);

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<CreateCategoryBindingModel>(data);

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["notOwnerError"] = "You do not belong to this household or this household does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ViewAllHouseholdCategories(int id)
        {
            var url = $"http://localhost:55669/api/Category/ViewAllHouseholdCategories/{id}";

            var httpClient = new HttpClient();

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = httpClient.GetStringAsync(url).Result;

                var result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(data);

                return View(result);
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["viewCategoryError"] = "You are not a member of this household";

                return View();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["viewCategoryError"] = "Household not found";

                return View();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");

        }
    }
}