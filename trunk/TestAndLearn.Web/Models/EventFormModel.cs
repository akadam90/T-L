using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Web.Models
{
    public class EventFormModel
    {
        public int ClientId { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}