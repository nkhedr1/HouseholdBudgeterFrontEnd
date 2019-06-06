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

        //[HttpGet]
        //public ActionResult ViewAllHouseholdCategories(int id)
        //{
        //    var url = $"http://localhost:55669/api/Category/ViewAllHouseholdCategories/{id}";

        //    var httpClient = new HttpClient();

        //    var cookie = Request.Cookies["MyCookie"];

        //    if (cookie == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    var token = cookie.Value;

        //    httpClient.DefaultRequestHeaders.Add("Authorization",
        //        $"Bearer {token}");

        //    var response = httpClient.GetAsync(url).Result;

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {

        //        var data = httpClient.GetStringAsync(url).Result;

        //        var result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(data);

        //        return View(result);
        //    }

        //    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //    {
        //        var data = response.Content.ReadAsStringAsync().Result;
        //        var result = JsonConvert.DeserializeObject<AuthenticationError>(data);

        //        return View();
        //    }

        //    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        //    {
        //        return View("Error");
        //    }

        //    throw new Exception("Status not recongnized");

        //}
    }
}