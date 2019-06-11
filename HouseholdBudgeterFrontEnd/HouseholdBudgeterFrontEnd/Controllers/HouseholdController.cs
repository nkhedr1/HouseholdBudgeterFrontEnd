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
    public class HouseholdController : Controller
    {
        // GET: Household
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateHousehold()
        {
            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateHousehold(CreateHouseholdBindingModel model)
        {

            var url = "http://localhost:55669/api/Household/CreateHousehold";

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
                return RedirectToAction("Login");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.PostAsync(url, encodedValues).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<CreateHouseholdBindingModel>(data);

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
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
        public ActionResult EditHousehold(int id)
        {
            
            var url = $"http://localhost:55669/api/Household/EditHousehold/{id}";

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

                var result = JsonConvert.DeserializeObject<CreateHouseholdBindingModel>(data);

                return View(result);
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

        [HttpPost]
        public ActionResult EditHousehold(int id, CreateHouseholdBindingModel model)
        {

            var url = $"http://localhost:55669/api/Household/EditHousehold/{id}";

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

                var result = JsonConvert.DeserializeObject<CreateHouseholdBindingModel>(data);

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
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
        public ActionResult ViewHouseholdMemebers(int id)
        {
            var url = $"http://localhost:55669/api/Household/ViewHouseholdMembers/{id}";

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

                var result = JsonConvert.DeserializeObject<List<MemberViewModel>>(data);

                return View(result);
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
        public ActionResult InviteUserToHousehold(int id)
        {
            var url = $"http://localhost:55669/api/Household/InviteToHousehold/{id}";

            var httpClient = new HttpClient();

            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = httpClient.GetStringAsync(url).Result;

                var result = JsonConvert.DeserializeObject<List<MemberViewModel>>(data);

                return View(result);
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

        [HttpPost]
        public ActionResult InviteUserToHousehold(InviteUser model)
        {

            var url = $"http://localhost:55669/api/Household/InviteToHousehold";

            var email = model.Email;
            var householdId = model.Id.ToString();

            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();

            parameters
               .Add(new KeyValuePair<string, string>("email", email));
            parameters
               .Add(new KeyValuePair<string, string>("householdId", householdId));

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

                var result = JsonConvert.DeserializeObject<InviteUser>(data);

                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
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
        public ActionResult JoinHousehold(int id)
        {

            var url = $"http://localhost:55669/api/Household/JoinHousehold/{id}";

            var httpClient = new HttpClient();
           
            var cookie = Request.Cookies["MyCookie"];

            if (cookie == null)
            {
                return RedirectToAction("Login");
            }

            var token = cookie.Value;

            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {token}");

            var response = httpClient.GetAsync(url).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var data = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<InviteUser>(data);

                return RedirectToAction("Index", "Home");
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
        public ActionResult ViewMyCreatedHouseholds()
        {
            var url = $"http://localhost:55669/api/Household/ViewMyCreatedHouseholds";

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

                var result = JsonConvert.DeserializeObject<List<HouseholdViewModel>>(data);

                return View(result);
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
        public ActionResult ViewHouseholdsJoined()
        {
            var url = $"http://localhost:55669/api/Household/ViewHouseholdsJoined";

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

                var result = JsonConvert.DeserializeObject<List<HouseholdViewModel>>(data);

                return View(result);
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
        public ActionResult LeaveHousehold(int id)
        {

            var url = $"http://localhost:55669/api/Household/LeaveHousehold/{id}";

           
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
                return RedirectToAction("Index", "Home");
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
        public ActionResult DeleteHousehold(int id)
        {

            var url = $"http://localhost:55669/api/Household/DeleteHousehold/{id}";


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
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationError>(data);
                return RedirectToAction("ViewMyCreatedHouseholds", "Household");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");

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