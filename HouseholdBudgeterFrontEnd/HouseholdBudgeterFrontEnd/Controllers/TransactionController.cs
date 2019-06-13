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
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTransaction(int id, int householdId)
        {
            var url = $"http://localhost:55669/api/Transaction/CreateTransaction/{householdId}/{id}";

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

                var result = JsonConvert.DeserializeObject<CreateTransactionViewModel>(data);

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
                TempData["userMessage"] = "You do not belong to this household or this household does not exist";

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View("Error");
            }

            throw new Exception("Status not recongnized");
        }

        [HttpPost]
        public ActionResult CreateTransaction(int id, int householdId, FormCollection formData)
        {

            var url = $"http://localhost:55669/api/Transaction/CreateTransaction/{householdId}/{id}";
            var title = formData["Title"];
            var description = formData["Description"];
            var amount = formData["Amount"].ToString();
            var date = formData["Date"].ToString();
            var categoryId = formData["Category"].ToString();

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("title", title));
            parameters
               .Add(new KeyValuePair<string, string>("description", description));
            parameters
              .Add(new KeyValuePair<string, string>("amount", amount));
            parameters
              .Add(new KeyValuePair<string, string>("date", date));
            parameters
              .Add(new KeyValuePair<string, string>("categoryId", categoryId));

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

                TempData["userMessage"] = "Category created";

                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
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
        public ActionResult ViewBankAccountTransactions(int id)
        {
            var url = $"http://localhost:55669/api/Transaction/ViewBankAccountTransactions/{id}";

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

                var result = JsonConvert.DeserializeObject<List<TransactionViewModel>>(data);

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
        public ActionResult EditTransaction(int id, int householdId)
        {
            var url = $"http://localhost:55669/api/Transaction/EditTransaction/{householdId}/{id}";

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

                var result = JsonConvert.DeserializeObject<TransactionViewModel>(data);

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
        public ActionResult EditTransaction(int id, int householdId, FormCollection formData)
        {

            var url = $"http://localhost:55669/api/Transaction/EditTransaction/{householdId}/{id}";

            var title = formData["Title"];
            var description = formData["Description"];
            var amount = formData["Amount"].ToString();
            var date = formData["Date"].ToString();
            var categoryId = formData["Category"].ToString();

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("title", title));
            parameters
               .Add(new KeyValuePair<string, string>("description", description));
            parameters
              .Add(new KeyValuePair<string, string>("amount", amount));
            parameters
              .Add(new KeyValuePair<string, string>("date", date));
            parameters
              .Add(new KeyValuePair<string, string>("categoryId", categoryId));

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

                var result = JsonConvert.DeserializeObject<TransactionViewModel>(data);

                TempData["userMessage"] = "Transaction edited";

                return RedirectToAction("ViewAllHouseholdAccounts", "BankAccount", new { id = householdId });
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
    }
}