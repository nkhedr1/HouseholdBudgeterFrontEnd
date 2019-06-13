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
    public class BankAccountController : Controller
    {
        // GET: BankAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateBankAccount()
        {
            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateBankAccount(int id, BankAccountBindingModel model)
        {

            var url = $"http://localhost:55669/api/BankAccount/CreateBankAccount/{id}";
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

                var result = JsonConvert.DeserializeObject<BankAccountBindingModel>(data);

                TempData["userMessage"] = "Bank Account created";

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
                TempData["userMessage"] = "You are not the owner of this household or this household does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ViewAllHouseholdAccounts(int id)
        {
            var url = $"http://localhost:55669/api/BankAccount/ViewAllHouseholdBankAccounts/{id}";

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

                var result = JsonConvert.DeserializeObject<List<BankAccountViewModel>>(data);

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

        [HttpGet]
        public ActionResult EditBankAccount(int id, int householdId)
        {
            var url = $"http://localhost:55669/api/BankAccount/EditBankAccount/{householdId}/{id}";

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

                var result = JsonConvert.DeserializeObject<BankAccountViewModel>(data);

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
                TempData["userMessage"] = "You are not the owner of this household or this household or Bank Account does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpPost]
        public ActionResult EditBankAccount(int id, int householdId, BankAccountViewModel model)
        {

            var url = $"http://localhost:55669/api/BankAccount/EditBankAccount/{householdId}/{id}";

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

                var result = JsonConvert.DeserializeObject<BankAccountViewModel>(data);

                TempData["userMessage"] = "Bank Account edited";

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
                TempData["userMessage"] = "You are not the owner of this household or this household or Bank Accountdoes not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult DeleteBankAccount(int id, int householdId)
        {

            var url = $"http://localhost:55669/api/BankAccount/DeleteBankAccount/{householdId}/{id}";

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
                TempData["userMessage"] = "Account deleted";
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["userMessage"] = "You are not the owner of this household";
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["userMessage"] = "Household or account does not exist";
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");

            }


            throw new Exception("Status not recongnized");
        }

        [HttpGet]
        public ActionResult ManuallyUpdateBankAccountBalance(int id, int householdId)
        {
            var url = $"http://localhost:55669/api/BankAccount/ManuallyUpdateBankAccountBalance/{householdId}/{id}";

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
                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["userMessage"] = "You are not the owner of this household";
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["userMessage"] = "This household or Bank Account does not exist";

                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpPost]
        public ActionResult ManuallyUpdateBankAccountBalance(int id, int householdId, BankAccountViewModel model)
        {

            var url = $"http://localhost:55669/api/BankAccount/ManuallyUpdateBankAccountBalance/{householdId}/{id}";

            var balance = model.Balance.ToString();

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("balance", balance));

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

                var data = httpClient.GetStringAsync(url).Result;

                var result = JsonConvert.DeserializeObject<BankAccountViewModel>(data);

                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["userMessage"] = "You are not the owner of this household";
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                TempData["userMessage"] = "This household or Bank Account does not exist";

                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }
    }
}