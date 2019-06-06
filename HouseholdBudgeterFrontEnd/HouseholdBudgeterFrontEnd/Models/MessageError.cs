using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class MessageError
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}