using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain.Infrastructure.Repositories
{
   public class CalendarRepository:ICalendarRepository
    {

       public CalendarModel[] GetCalendarData(String start, String end,int client)
       {

           using (var connection = new TestAndLearnEntitiesConnection()) {

               CalendarModel[] data;
               int i = 0;
               DateTime startDate = DateTime.Parse(start);
               DateTime endDate = DateTime.Parse(end);

               var calendarEvents = (from e in connection.Events
                             where e.StartDate >= startDate && e.EndDate <= endDate && e.ClientId==client
                             select new CalendarModel()
                             {
                                 id = "Event"+e.EventId,
                                 title = e.Description,
                                 start = e.StartDate.ToString(),
                                 end = e.EndDate.ToString(),
                                 url = "",
                                 pillar = 0
                              }).ToList();

               var calendarTests = (from t in connection.Tests
                                    where t.StartDate >= startDate && t.EndDate <= endDate && t.ClientId==client && t.TestStatusId==2
                                    select new CalendarModel()
                                    {
                                        id = "Test"+t.TestId,
                                        title = t.Name,
                                        start = t.StartDate.ToString(),
                                        end = t.EndDate.ToString(),
                                        url = "",
                                        pillar = t.PillarId

                                    }).ToList();



               data = new CalendarModel[calendarEvents.Count + calendarTests.Count];

               foreach (CalendarModel ev in calendarEvents)
               {
                   data[i] = ev;
                   i++;
               }
               
               foreach (CalendarModel tests in calendarTests)
               {
                   if (tests.pillar == 1)
                       tests.backgroundColor = "#7b4173";
                   else if (tests.pillar == 2)
                       tests.backgroundColor = "#a55194";
                   else if (tests.pillar == 3)
                       tests.backgroundColor = "#ce6dbd";
                   else if (tests.pillar == 4)
                       tests.backgroundColor = "#de9ed6";
                   else if (tests.pillar == null)
                       tests.backgroundColor = "#7f7f7f";
                   data[i] = tests;
                   i++;
               
               }
               
               return data;
           };

           

       }
    }
}
