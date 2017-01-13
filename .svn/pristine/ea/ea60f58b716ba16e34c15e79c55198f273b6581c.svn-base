using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestAndLearn.Domain;
using TestAndLearn.Domain.Models;
//using TestAndLearn.Domain.Models;


namespace TestAndLearn.Web.Controllers
{
    public class CalendarApiController : BaseApiController
    {
        private ICalendarRepository _calendarrepository;
        public CalendarApiController(ICalendarRepository calendarrepository)
        {
        _calendarrepository = calendarrepository;
        }
        
        [HttpGet]
        public CalendarModel[] GetCalendarData([FromUri] String start, String end, int client)
        {
            return (_calendarrepository.GetCalendarData(start,end,client));
        
        }

    }
}